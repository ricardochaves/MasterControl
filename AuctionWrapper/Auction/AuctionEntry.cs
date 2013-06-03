using Styx.Common;

namespace AuctionWrapper.Auction
{
    /// <summary>
    /// A class with basic attributes for an auction already posted on the Auction House.
    /// </summary>
    public class AuctionEntry : Auction
    {
        // No constructor
        internal AuctionEntry(){}
        /// <summary>
        /// The quality of the item.
        /// </summary>
        public Quality Quality;
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
        /// Gets the current bid per item.
        /// </summary>
        /// <value>
        /// The current bid per item.
        /// </value>
        public Money CurrentBidPerItem { get { return CurrentBid / StackSize; } }
        /// <summary>
        /// The name of the highest bidder on the auction.
        /// </summary>
        public string HighestBidder;
        /// <summary>
        /// The owner of the auction.
        /// </summary>
        public string Owner;
        /// <summary>
        /// Returns an object of the <see cref="Auction" /> class from an array of values from the GetAuctionItemInfo lua function.
        /// </summary>
        /// <param name="values">Values from the GetAuctionItemInfo lua function.</param>
        /// <returns></returns>
        public static AuctionEntry FromGetAuctionItemInfo(string[] values)
        {
            try
            {
                return new AuctionEntry
                {
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
            catch (System.Exception e)
            {
                Logging.Write(e.ToString());
                return null;
            }
        }
        public override string ToString()
        {
            var header = string.Format("{0} ({1}) - posted by {2}\n-----------\n", Name, Id, Owner);
            var quality = string.Format("Quality: {0}", Quality);
            var count = string.Format("Count: {0}\n", StackSize);
            var buyout = string.Format("Buyout Price: {0} ({1} each)\n", BuyoutPrice, BuyoutPricePerItem);
            var currentBid = string.Format("Current Bid: {0} ({1} each) - by {2}\n", CurrentBid, CurrentBidPerItem, HighestBidder);
            var minBid = string.Format("Min Bid: {0} ({1} each)\n", MinBid, MinBidPerItem);
            var minIncrement = string.Format("Min increment: {0}\n", MinIncrement);
            var canUse = string.Format("Can Use: {0} (Required level: {1})\n", CanUse, RequiredLevel);
            return header + quality + count + buyout + currentBid + minBid + minIncrement + canUse;
        }
    }
}
