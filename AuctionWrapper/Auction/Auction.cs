namespace AuctionWrapper.Auction
{
    /// <summary>
    /// Represents an auction.
    /// </summary>
    public class Auction
    {
        // No constructor
        internal Auction(){}
        /// <summary>
        /// The item ID of the item.
        /// </summary>
        public uint Id;
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name;
        /// <summary>
        /// The stacksize of the auction.
        /// </summary>
        public int StackSize;
        /// <summary>
        /// The minimum bid price on the auction.
        /// </summary>
        public Money MinBid;
        /// <summary>
        /// Gets the min bid per item.
        /// </summary>
        /// <value>
        /// The min bid per item.
        /// </value>
        public Money MinBidPerItem { get { return MinBid / StackSize; } }
        /// <summary>
        /// The buyout price of the auction.
        /// </summary>
        public Money BuyoutPrice;
        /// <summary>
        /// Gets the buyout price per item.
        /// </summary>
        /// <value>
        /// The buyout price per item.
        /// </value>
        public Money BuyoutPricePerItem { get { return BuyoutPrice/StackSize; } }
    }

    /// <summary>
    /// Represents the different qualities of an item. They each have an int value ranging from 0-7 (poor-heirloom).
    /// </summary>
    public enum Quality
    {
        /// <summary>
        /// Poor quality (grey).
        /// </summary>
        Poor = 0,
        /// <summary>
        /// Common quality (white).
        /// </summary>
        Common = 1,
        /// <summary>
        /// Uncommon quality (green).
        /// </summary>
        Uncommon = 2,
        /// <summary>
        /// Rare quality (blue).
        /// </summary>
        Rare = 3,
        /// <summary>
        /// Epic quality (purple).
        /// </summary>
        Epic = 4,
        /// <summary>
        /// Legendary quality (orange).
        /// </summary>
        Legendary = 5,
        /// <summary>
        /// Artifact quality (golden yellow).
        /// </summary>
        Artifact = 6,
        /// <summary>
        /// Heirloom quality (golden yellow).
        /// </summary>
        Heirloom = 7
    }
    /// <summary>
    /// Represents the 3 different durations an auction can be created with.
    /// </summary>
    public enum Duration
    {
        TwelveHours = 12,
        TwentyFourHours = 24,
        FortyEightHours = 48
    }
}
