using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using AuctionWrapper.Auction;
using Styx;
using Styx.Common;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWCache;

namespace AuctionWrapper
{
    public static class AuctionHouse
    {
        public static event SearchDoneHandler SearchDone;
        public delegate void SearchDoneHandler(SearchDoneArgs ca);
        public static event SellDoneHandler SellDone;
        public delegate void SellDoneHandler(SellDoneArgs ca);

        public static OwnAuctions OwnAuctions = new OwnAuctions();
        public static AuctionPage AuctionPage = new AuctionPage();
        public static Queue<AuctionCreate> SellQueue = new Queue<AuctionCreate>();
        private static bool _selling;
        private static string _searchString;
        private static uint _searchId;
        private static int _page;
        private static int _auctionsCount;
        private static List<AuctionEntry> _auctions; 
        private static bool CanSearch { get { return Lua.GetReturnVal<int>("return CanSendAuctionQuery()", 0) == 1; } }
        private static bool UseAuctionMultisellUpdated;
        private static bool UseAuctionMultisellFailed;
        private static Stopwatch searchDelay = new Stopwatch();
        private static Stopwatch sellDelay = new Stopwatch();

        /// <summary>
        /// Some functions need a pulse. Call this in your plugin pulse.
        /// </summary>
        public static void Pulse()
        {
            Cancel.Pulse();
            if (searchDelay.IsRunning && searchDelay.ElapsedMilliseconds > 1500)
            {
                searchDelay.Reset();
                AuctionItemsListUpdatedSearch();
            }
            if (sellDelay.IsRunning && sellDelay.ElapsedMilliseconds > 1000)
            {
                sellDelay.Reset();
                SellQueueHandler();
            }
        }

        /// <summary>
        /// Attaches the events that the wrapper uses. Call this on your plugin Initialize.
        /// </summary>
        public static void AttachEvents()
        {
            Lua.Events.AttachEvent("AUCTION_HOUSE_SHOW", AuctionHouseShown);
            //Lua.Events.AttachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionItemsListUpdatedSearch);
            Lua.Events.AttachEvent("AUCTION_MULTISELL_UPDATE", AuctionMultisellUpdated);
            Lua.Events.AttachEvent("AUCTION_MULTISELL_FAILURE", AuctionMultisellFailed);
        }
        /// <summary>
        /// Detaches the events that the plugin uses. You will most likely not need to use this.
        /// </summary>
        public static void DetachEvents()
        {
            Lua.Events.DetachEvent("AUCTION_HOUSE_SHOW", AuctionHouseShown);
            //Lua.Events.DetachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionItemsListUpdatedSearch);
            Lua.Events.DetachEvent("AUCTION_MULTISELL_UPDATE", AuctionMultisellUpdated);
            Lua.Events.DetachEvent("AUCTION_MULTISELL_FAILURE", AuctionMultisellFailed);
        }
        /// <summary>
        /// Gets a value indicating whether the Auction House window is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public static bool IsVisible { get { return Styx.CommonBot.Frames.AuctionFrame.Instance.IsVisible; } }
        #region search
        private static void StartSearch(string searchString, uint id)
        {
            _page = 0;
            _auctionsCount = 0;
            _searchString = searchString;
            _searchId = id;
            _auctions = new List<AuctionEntry>();
            ////Lua.Events.DetachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionItemsListUpdatedSearch);
            //Lua.Events.AttachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionItemsListUpdatedSearch);
            string lua = String.Format("QueryAuctionItems(\"{0}\",nil,nil,nil,nil,nil,{1},nil,nil,nil)", _searchString, _page);
            Logging.Write("Seaching for {0}", searchString);
            Lua.DoString(lua);
            searchDelay.Restart();
        }
        /// <summary>
        /// Starts searching the Auction House. The event SearchDone will be triggered with the results when the search is over.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        public static void StartSearch(string searchString)
        {
            StartSearch(searchString, 0);
        }
        /// <summary>
        /// Starts searching the Auction House. The event SearchDone will be triggered with the results when the search is over.
        /// </summary>
        /// <param name="id">The item id.</param>
        public static void StartSearch(uint id)
        {
            var name = GetItemName(id);
            StartSearch(name, id);
        }
        private static void SearchNextPage()
        {
            if (!CanSearch)
            {
                Thread.Sleep(50);
            }
            string lua = String.Format("QueryAuctionItems(\"{0}\",nil,nil,nil,nil,nil,{1},nil,nil,nil)", _searchString, _page);
            Lua.DoString(lua);
            searchDelay.Restart();
        }
        private static IEnumerable<AuctionEntry> GetCurrentPage()
        {
            return AuctionPage;
        }
        private static void AuctionItemsListUpdatedSearch()
        {
            _auctionsCount = Lua.GetReturnVal<int>("return GetNumAuctionItems(\"list\")", 1);
            Logging.Write("Showing results {0}-{1}, there is {2} results", _page*50, (_page+1)*50, _auctionsCount);
            _auctions.AddRange(GetCurrentPage());
            _page++;
            if (_page*50 < _auctionsCount)
            {
                Logging.Write("Searching next page");
                SearchNextPage();
            }
            else
            {
                var results = new SearchDoneArgs(_auctions, _searchString, _searchId);
                Logging.Write("Search has completed for {0}", _searchString);
                if(SearchDone != null)
                    SearchDone(results);
                //Lua.Events.DetachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionItemsListUpdatedSearch);
            }
        }
        #endregion search
        #region sell
        /// <summary>
        /// Add a <see cref="AuctionCreate" /> item to the queue of items to sell. The queue will be handled automatically.
        /// </summary>
        /// <param name="auctionInfo">The auction info.</param>
        public static void AddSell(AuctionCreate auctionInfo)
        {
            SellQueue.Enqueue(auctionInfo);
            if (!_selling && IsVisible)
                Sell(SellQueue.Dequeue());
        }
        private static void Sell(AuctionCreate auctionInfo)
        {
            Logging.Write("Selling {0} ({1}) x {2}", auctionInfo.Name, auctionInfo.StackSize, auctionInfo.Stacks);
            if (Helpers.InBagCount(auctionInfo.Id) < auctionInfo.StackSize)
            {
                Logging.Write("Not enough items to post {0}. StackSize is {1} and I only have {2}", auctionInfo.Name, auctionInfo.StackSize, Helpers.InBagCount(auctionInfo.Id));
                SellQueueHandler();
                return;
            }
            if (Helpers.InBagCount(auctionInfo.Id) < auctionInfo.StackSize * auctionInfo.Stacks)
            {
                auctionInfo.Stacks = (int) Math.Floor((decimal) Helpers.InBagCount(auctionInfo.Id)/auctionInfo.StackSize);
            }
            _selling = true;
            var addItemLua =
                "for b=0,4 do " +
                "  for s=1,GetContainerNumSlots(b) do " +
                "   local id=GetContainerItemID(b,s) " +
                "	if id=={0} then " +
                "	  PickupContainerItem(b,s) " +
                "	  ClickAuctionSellItemButton() " +
                "     AuctionFrameAuctions.duration={1} " +
                "	  ClearCursor() " +
                "	  return 1 " +
                "	end " +
                "  end " +
                "end " +
                "return 0";
            //                                                  int values of the enum are 12,24,48. The lua takes 1,2,3 as arguments.
            addItemLua = string.Format(addItemLua, auctionInfo.Id, (int) auctionInfo.Duration/12);
            var sellItemLua = string.Format("StartAuction({0},{1},{2},{3},{4})", auctionInfo.MinBid.TotalCopper,
                                            auctionInfo.BuyoutPrice.TotalCopper, (int) auctionInfo.Duration/12,
                                            auctionInfo.StackSize, auctionInfo.Stacks);
            UseAuctionMultisellUpdated = false;
            UseAuctionMultisellFailed = false;
            //Lua.Events.DetachEvent("AUCTION_MULTISELL_UPDATE", AuctionMultisellUpdated);
            //Lua.Events.DetachEvent("AUCTION_MULTISELL_FAILURE", AuctionMultisellFailed);
            //Lua.Events.DetachEvent("AUCTION_OWNED_LIST_UPDATE", AuctionOwnedListUpdated);
            if (auctionInfo.Stacks > 1)
            {
                UseAuctionMultisellUpdated = true;
                UseAuctionMultisellFailed = true;
                //Lua.Events.AttachEvent("AUCTION_MULTISELL_UPDATE", AuctionMultisellUpdated);
                //Lua.Events.AttachEvent("AUCTION_MULTISELL_FAILURE", AuctionMultisellFailed);
            }
            if (auctionInfo.Stacks == 1)
            {
                sellDelay.Restart();
                UseAuctionMultisellFailed = true;
                //Lua.Events.AttachEvent("AUCTION_OWNED_LIST_UPDATE", AuctionOwnedListUpdated);
            }
            Lua.DoString(addItemLua);
            Lua.DoString(sellItemLua);
        }
        private static void AuctionMultisellUpdated(object sender, LuaEventArgs args)
        {
            Logging.Write("UseAuctionMultisellUpdated: {0}", UseAuctionMultisellUpdated);
            if (!UseAuctionMultisellUpdated)
                return;
            int i = Convert.ToInt32((double)args.Args[0]);
            int count = Convert.ToInt32((double)args.Args[1]);
            Logging.Write("Posting {0}/{1}", i, count);
            if (i >= count)
            {
                sellDelay.Restart();
            }
        }
        private static void SellQueueHandler()
        {
            if (SellQueue.Count == 0)
            {
                Logging.Write("Queue is empty!");
                //Lua.Events.DetachEvent("AUCTION_MULTISELL_UPDATE", AuctionOwnedListUpdated);
                _selling = false;
                var results = new SellDoneArgs(true);
                if (SellDone != null)
                    SellDone(results);
            }
            else
            {
                Logging.Write("Selling next item in queue.");
                Sell(SellQueue.Dequeue());
            }
        }
        private static void AuctionMultisellFailed(object sender, LuaEventArgs args)
        {
            if (!UseAuctionMultisellFailed)
                return;
            //Lua.Events.DetachEvent("AUCTION_MULTISELL_UPDATE", AuctionMultisellUpdated);
            //Lua.Events.DetachEvent("AUCTION_MULTISELL_FAILURE", AuctionMultisellFailed);
            Logging.Write("Failed posting {0} on AH.", _searchString);
            sellDelay.Restart();
        }
        #endregion sell
        private static string GetItemName(uint id) 
        {
            WoWCache.InfoBlock cache = null;
            while (cache == null)
            {
                cache = StyxWoW.Cache[CacheDb.Item].GetInfoBlockById(id);
            }
            return StyxWoW.Memory.ReadString(cache.ItemSparse.Name, Encoding.UTF8);
        }
        private static void AuctionHouseShown(object sender, LuaEventArgs args)
        {
            if (SellQueue.Count > 0)
                Sell(SellQueue.Dequeue());
        }

    }
    public class SearchDoneArgs : EventArgs
    {
        public SearchDoneArgs(List<AuctionEntry> auctions, string searchString, uint id)
        {
            Auctions = auctions;
            SearchId = id;
            SearchString = searchString;
        }
        /// <summary>
        /// Gets the result from searching as a list of <see cref="Auction" />.
        /// </summary>
        /// <value>
        /// List of <see cref="Auction" />
        /// </value>
        public List<AuctionEntry> Auctions { get; private set; }

        public uint SearchId { get; private set; }
        public string SearchString { get; private set; } 
    }
    public class SellDoneArgs : EventArgs
    {

        private bool success;

        public SellDoneArgs(bool success)
        {
            this.success = success;
        }

        /// <summary>
        /// Was the sell successful?.
        /// </summary>
        /// <returns>List of <see cref="Auction" /></returns>
        public bool Success()
        {
            return success;
        }
    }
    public class PageShownArgs : EventArgs
    {
        public AuctionPage CurrentPage { get { return new AuctionPage(); } }
    }
}
