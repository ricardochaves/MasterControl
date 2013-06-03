using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionWrapper.Auction;
using Styx.Common;
using Styx.WoWInternals;

namespace AuctionWrapper
{
    /// <summary>
    /// Represents the current page shown in Auction House browse window. Information about each element accessed is grabbed as you access it. It's not stored, so always up to date!
    /// </summary>
    public class AuctionPage : IEnumerable<AuctionShown>
    {
        /// <summary>
        /// Gets the <see cref="AuctionShown" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="AuctionShown" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AuctionShown this[int index]
        {
            get { return GetAuction(index); }
        }
        private AuctionShown GetAuction(int index)
        {
            var lua = string.Format("return GetAuctionItemInfo(\"list\", {0})", index+1);
            var values = Lua.GetReturnValues(lua);
            return values == null || values[0] == "nil" ? null : AuctionShown.FromGetAuctionItemInfo(values.ToArray(), index);
        }
        public IEnumerator<AuctionShown> GetEnumerator()
        {
            var count = 50;
            var auctions = new List<AuctionShown>();
            for (int i = 0; i < count; i++)
            {
                var auction = GetAuction(i);
                if (auction != null)
                    auctions.Add(auction);
            }
            auctions.Reverse();
            return auctions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
