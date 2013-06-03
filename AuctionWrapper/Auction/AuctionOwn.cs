using System;
using System.Collections.Generic;
using System.Linq;
using Styx.Common;
using Styx.WoWInternals;

namespace AuctionWrapper.Auction
{
    /// <summary>
    /// Representing one of your own auctions.
    /// </summary>
    public class AuctionOwn : Auction
    {
        // No constructor
        internal AuctionOwn() { }
        /// <summary>
        /// The quality of the item.
        /// </summary>
        public Quality Quality;
        private int _index;
        /// <summary>
        /// Can the item be used by the current character?
        /// </summary>
        public bool CanUse;
        /// <summary>
        /// The required level to use the item.
        /// </summary>
        public int RequiredLevel;
        /// <summary>
        /// The minimum increment required on a new bid (compared to the current bid).
        /// </summary>
        public Money MinIncrement;
        /// <summary>
        /// The current bid price on the auction.
        /// </summary>
        public Money CurrentBid;
        /// <summary>
        /// Gets the current bid per item. 0 if the auction is sold.
        /// </summary>
        /// <value>
        /// The current bid per item.
        /// </value>
        public Money CurrentBidPerItem { get { return Sold ? Money.Zero : CurrentBid / StackSize; } }
        /// <summary>
        /// Gets the min bid per item. 0 if the auction is sold.
        /// </summary>
        /// <value>
        /// The min bid per item.
        /// </value>
        public new Money MinBidPerItem { get { return Sold ? Money.Zero : MinBid / StackSize; } }
        /// <summary>
        /// Gets the buyout price per item. 0 if the auction is sold.
        /// </summary>
        /// <value>
        /// The buyout price per item.
        /// </value>
        public new Money BuyoutPricePerItem { get { return Sold ? Money.Zero : BuyoutPrice / StackSize; } }
        /// <summary>
        /// The name of the highest bidder on the auction.
        /// </summary>
        public string HighestBidder;
        /// <summary>
        /// Has the auction already been sold? (Only valid for own auctions)
        /// </summary>
        public bool Sold;
        /// <summary>
        /// Returns an object of the <see cref="Auction" /> class from an array of values from the GetAuctionItemInfo lua function.
        /// </summary>
        /// <param name="values">Values from the GetAuctionItemInfo lua function.</param>
        /// <param name="index">The index it's showed in the list in WoW Client </param>
        /// <returns></returns>
        public static AuctionOwn FromGetAuctionItemInfo(string[] values, int index)
        {
            try
            {
                return new AuctionOwn
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
                    Quality = (Quality)int.Parse(values[3]),
                    RequiredLevel = int.Parse(values[5]),
                    Sold = values[13] == "1"
                };
            }
            catch (Exception e)
            {
                Logging.Write(e.ToString());
                return null;
            }
        }
        /// <summary>
        /// Cancels the auction.
        /// </summary>
        public void CancelAuction()
        {
            var lua = string.Format("CancelAuction({0})", _index + 1);
            Lua.DoString(lua);
        }
        public override string ToString()
        {
            var header = string.Format("{0} ({1})\n-----------\n", Name, Id);
            var quality = string.Format("Quality: {0}", Quality);
            var count = string.Format("Count: {0}\n", StackSize);
            var buyout = string.Format("Buyout Price: {0} ({1} each)\n", BuyoutPrice, BuyoutPricePerItem);
            var currentBid = string.Format("Current Bid: {0} ({1} each) - by {2}\n", CurrentBid, CurrentBidPerItem, HighestBidder);
            var minBid = string.Format("Min Bid: {0} ({1} each)\n", MinBid, MinBidPerItem);
            var minIncrement = string.Format("Min increment: {0}\n", MinIncrement);
            var canUse = string.Format("Can Use: {0} (Required level: {1})\n", CanUse, RequiredLevel);
            var sold = string.Format("Sold: {0}\n", Sold);
            return header + quality + count + buyout + currentBid + minBid + minIncrement + canUse + sold;
        }
    }
}
