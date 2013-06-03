using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionWrapper;

namespace RBCAH
{
    public class UMJItem
    {
        public uint ItemId { get; private set; }
        public string ItemName { get { return ItemInfo["name"]; } }
        public Money MarketPrice { get { return new Money(Int32.Parse(ItemInfo["market"])); } }
        public DateTime LastSeen { get { return DateTime.Parse(ItemInfo["lastseen"]); } }
        public Money AvgPrice
        {
            get
            {
                string value;
                if (ItemInfo.TryGetValue("marketaverage", out value))
                    return new Money(Int32.Parse(value));
                return MarketPrice;
            }
        }
        private Dictionary<string, string> ItemInfo;

        public UMJItem(Dictionary<string, string> itemInfo, uint itemId)
        {
            ItemInfo = itemInfo;
            ItemId = itemId;
        }
    }
}
