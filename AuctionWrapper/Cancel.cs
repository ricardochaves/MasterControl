using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using AuctionWrapper.Auction;
using Styx.Common;

namespace AuctionWrapper
{
    /// <summary>
    /// A helper class to check for undercuts and cancel if you've been undercut.
    /// </summary>
    public static class Cancel
    {
        public static Queue<uint> CancelCheckQueue = new Queue<uint>();
        private static Dictionary<uint, AuctionEntry> CancelDict = new Dictionary<uint, AuctionEntry>();
        private static bool _searching;
        public static event EventHandler CancelDone = delegate { };
        private static Stopwatch CancelDoneTimer = new Stopwatch();
        private static int AuctionCount;
        /// <summary>
        /// Adds the item to queue for scanning undercut.
        /// </summary>
        /// <param name="itemID">The item ID.</param>
        public static void AddToQueue(uint itemID)
        {
            Thread.Sleep(50);
            if (!CancelDict.ContainsKey(itemID))
                CancelCheckQueue.Enqueue(itemID);
            if (!_searching)
                StartSearch(CancelCheckQueue.Dequeue());
        }

        internal static void Pulse()
        {
            if (CancelDoneTimer.IsRunning && CancelDoneTimer.ElapsedMilliseconds > 2000)
            {
                var newAuctionCount = AuctionHouse.OwnAuctions.AuctionsCount;
                Logging.Write("I now have {0} auctions. On the last check I had {1}", newAuctionCount, AuctionCount);
                if (AuctionCount == newAuctionCount || newAuctionCount == 0)
                {
                    Logging.Write("Done cancelling!");
                    CancelDoneTimer.Reset();
                    AuctionCount = AuctionHouse.OwnAuctions.AuctionsCount;
                    CancelDict = new Dictionary<uint, AuctionEntry>();
                    CancelDone(null, EventArgs.Empty);
                }
                else if (AuctionCount != newAuctionCount)
                {
                    CancelDoneTimer.Restart();
                    AuctionCount = newAuctionCount;
                    Logging.Write("Still cancelling, checking again in 2 seconds.");
                }
            }
        }

        private static void StartSearch(uint itemID)
        {
            _searching = true;
            AuctionHouse.SearchDone += AuctionHouse_SearchDone;
            AuctionHouse.StartSearch(itemID);
        }

        private static void AuctionHouse_SearchDone(SearchDoneArgs ca)
        {
            var cheapest = ca.Auctions.OrderBy(a => a.BuyoutPricePerItem.TotalCopper).FirstOrDefault();
            if (cheapest != null && !CancelDict.ContainsKey(cheapest.Id))
            {
                CancelDict.Add(cheapest.Id, cheapest);
            }
            AuctionHouse.SearchDone -= AuctionHouse_SearchDone;
            Logging.Write("Still need to scan {0} items.", CancelCheckQueue.Count);
            if (CancelCheckQueue.Count > 0)
            {
                var itemToScan = CancelCheckQueue.Dequeue();
                while (CancelDict.ContainsKey(itemToScan))
                {
                    if (CancelCheckQueue.Count > 0)
                        itemToScan = CancelCheckQueue.Dequeue();
                    else
                    {
                        CancelTheQueue();
                        return;
                    }
                }
                StartSearch(itemToScan);
            }
            else
            {
                CancelTheQueue();
            }
        }
        private static void CancelTheQueue()
        {
            _searching = false;
            using (Styx.StyxWoW.Memory.AcquireFrame())
            {
                foreach (var ownAuction in AuctionHouse.OwnAuctions)
                {
                    if (CancelDict.ContainsKey(ownAuction.Id))
                    {
                        var cheapest = CancelDict[ownAuction.Id];
                        if (ownAuction.Id == cheapest.Id && !ownAuction.Sold && ownAuction.BuyoutPricePerItem > cheapest.BuyoutPricePerItem)
                            ownAuction.CancelAuction();
                    }
                }
            }
            AuctionCount = AuctionHouse.OwnAuctions.AuctionsCount;
            CancelDoneTimer.Restart();
        }
    }
}
