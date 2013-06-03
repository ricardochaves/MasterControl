using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Styx.Common;
using Styx.WoWInternals;

namespace AuctionWrapper.Auction
{
    public class AuctionShown : AuctionEntry
    {
        private int _index;
        /// <summary>
        /// Get an <see cref="AuctionShown" /> object from the returnvalues when requesting info from lua. You probably shouldn't use this.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static AuctionShown FromGetAuctionItemInfo(string[] values, int index)
        {
            try
            {
                return new AuctionShown
                {
                    _index = index,
                    BuyoutPrice = new Money(int.Parse(values[9])),
                    CanUse = values[4] == "1",
                    StackSize = int.Parse(values[2]),
                    CurrentBid = new Money(int.Parse(values[10])),
                    HighestBidder = values[11],
                    Id = uint.Parse(values[14]),
                    MinBid = new Money(int.Parse(values[7])),
                    MinIncrement = new Money(int.Parse(values[8])),
                    Name = values[0],
                    Owner = values[12],
                    Quality = (Quality)int.Parse(values[3]),
                    RequiredLevel = int.Parse(values[5]),
                };
            }
            catch (Exception e)
            {
                Logging.Write(e.ToString());
                return null;
            }
        }
        /// <summary>
        /// Bids the specified bid amount.
        /// </summary>
        /// <param name="bidAmount">The bid amount.</param>
        /// <exception cref="System.Exception">Bid amount can not be lower than MinBid.</exception>
        public void Bid(Money bidAmount)
        {
            if (bidAmount < MinBid)
                throw new Exception("Bid amount can not be lower than MinBid.");
            if (bidAmount >= BuyoutPrice)
                throw new Exception("Bid amount can not be higher than or equal to BuyoutPrice. Use Buyout() instead if you want to buyout.");
            var lua = string.Format("PlaceAuctionBid(\"list\", {0}, {1})", _index+1, bidAmount.TotalCopper);
            Lua.DoString(lua);
        }
        /// <summary>
        /// Buyouts this item.
        /// </summary>
        public void Buyout()
        {
            var lua = string.Format("PlaceAuctionBid(\"list\", {0}, {1})", _index+1, BuyoutPrice.TotalCopper);
            Lua.DoString(lua);
        }
    }
}
