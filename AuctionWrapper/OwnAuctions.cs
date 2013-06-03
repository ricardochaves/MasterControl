using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionWrapper.Auction;
using Styx.WoWInternals;

namespace AuctionWrapper
{
    /// <summary>
    /// Represents the page showing your own auctions. Information about each element accessed is grabbed as you access it. It's not stored, so always up to date!
    /// </summary>
    public class OwnAuctions : IEnumerable<AuctionOwn>
    {
        /// <summary>
        /// Gets the <see cref="AuctionOwn" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="AuctionOwn" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AuctionOwn this [int index]
        {
            get { return GetAuction(index); }
        }
        private AuctionOwn GetAuction(int index)
        {
            var lua = string.Format("return GetAuctionItemInfo(\"owner\", {0})", index+1);
            return AuctionOwn.FromGetAuctionItemInfo(Lua.GetReturnValues(lua).ToArray(), index);
        }
        public IEnumerator<AuctionOwn> GetEnumerator()
        {
            var count = Lua.GetReturnVal<int>("return GetNumAuctionItems(\"owner\")", 0);
            var auctions = new List<AuctionOwn>();
            using (Styx.StyxWoW.Memory.AcquireFrame())
            {
                for (int i = 0; i < count; i++)
                {
                    auctions.Add(GetAuction(i));
                }
            }
            auctions.Reverse();
            return auctions.GetEnumerator();
        }
            
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Gets the auctions count. If possible, use this instead of LINQ Count().
        /// </summary>
        /// <value>
        /// The auctions count.
        /// </value>
        public int AuctionsCount
        {
            get { return Lua.GetReturnVal<int>("return GetNumAuctionItems(\"owner\")", 0); }
        }
    }
}
