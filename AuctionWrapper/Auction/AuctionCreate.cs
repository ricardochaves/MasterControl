using System.Collections.Generic;
using System.IO;
using System.Linq;
using Styx.Common;
using Styx.WoWInternals;

namespace AuctionWrapper.Auction
{
    /// <summary>
    /// An representation of an auction to be posted on Auction House.
    /// </summary>
    public class AuctionCreate : Auction
    {
        /// <summary>
        /// The duration the auction should be posted at.
        /// </summary>
        public Duration Duration;

        /// <summary>
        /// The amount of stacks to post.
        /// </summary>
        public int Stacks;

        public new Money MinBid { get { return MinBidPerItem * StackSize; }}
        public new Money BuyoutPrice { get { return BuyoutPricePerItem * StackSize; }}
        public new Money MinBidPerItem { get; set; }
        public new Money BuyoutPricePerItem { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionCreate" /> class.
        /// </summary>
        /// <param name="ae">An <see cref="AuctionEntry" /> object to copy elements from.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="stackSize">Size of the stack.</param>
        /// <param name="stacks">The number stacks.</param>
        public AuctionCreate(AuctionEntry ae, Duration duration, int stackSize, int stacks)
        {
            BuyoutPricePerItem = ae.BuyoutPricePerItem;
            Duration = duration;
            Id = ae.Id;
            MinBidPerItem = ae.MinBidPerItem;
            Name = ae.Name;
            StackSize = stackSize;
            Stacks = stacks;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionCreate" /> class.
        /// </summary>
        /// <param name="id">The item id.</param>
        /// <param name="name">The item name.</param>
        /// <param name="buyoutPricePerItem">The buyout price per item.</param>
        /// <param name="bidPricePerItem">The bid price per item.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="stackSize">Size of the stack.</param>
        /// <param name="stacks">The number stacks.</param>
        public AuctionCreate(uint id, string name, Money buyoutPricePerItem, Money bidPricePerItem, Duration duration, int stackSize, int stacks)
        {
            BuyoutPricePerItem = buyoutPricePerItem;
            Duration = duration;
            Id = id;
            MinBidPerItem = bidPricePerItem;
            Name = name;
            StackSize = stackSize;
            Stacks = stacks;
        }
    }
}
