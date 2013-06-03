using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Styx.WoWInternals;

namespace AuctionWrapper
{
    /// <summary>
    /// Represents a search query.
    /// </summary>
    public class SearchQuery
    {
        /// <summary>
        /// Number of pages. If this is zero, it's because the Search Query wasn't created properly (Auction House window needs to be open).
        /// </summary>
        public int Pages;

        public static event PageShownHandler PageShown;
        public delegate void PageShownHandler(PageShownArgs ca);

        private bool _searching;
        /// <summary>
        /// Gets the search string.
        /// </summary>
        /// <value>
        /// The search string.
        /// </value>
        public string SearchString { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQuery" /> class. Auction House window must be open for the page count to properly update.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        public SearchQuery(string searchString)
        {
            SearchString = searchString;
            Lua.Events.AttachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionPageShown);
            Search(SearchString, 0);
        }
        /// <summary>
        /// Searches for the search string and shows that page number.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        public void ShowPage(int pageNumber)
        {
            _searching = true;
            Lua.Events.AttachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionPageShown);
            Search(SearchString, pageNumber);
        }
        private void Search(string searchString, int page)
        {
            var lua = string.Format("QueryAuctionItems(\"{0}\",nil,nil,nil,nil,nil,{1},nil,nil,nil)", searchString, page);
            Lua.DoString(lua);
        }

        private void AuctionPageShown(object sender, LuaEventArgs args)
        {
            Lua.Events.DetachEvent("AUCTION_ITEM_LIST_UPDATE", AuctionPageShown);
            var count = Lua.GetReturnVal<int>("return GetNumAuctionItems(\"list\")", 1);
            Pages = Convert.ToInt32(Math.Ceiling((double) count/50));
            if (_searching)
            {
                _searching = false;
                var results = new PageShownArgs();
                PageShown(results);
            }
        }
    }
}
