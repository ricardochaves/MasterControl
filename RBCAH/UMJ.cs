using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using AuctionWrapper;

namespace RBCAH
{
    public class UMJ
    {
        public DateTime LastUpdated;
        public Dictionary<uint, UMJItem> Items;
        private static WebClient _webClient = new WebClient();
        public static bool NotifyDownloadComplete;
        public static bool IsDownloading;
        public UMJ()
        {
            if (File.Exists(FilePath))
                LoadUMJFile(FilePath);
            else
                LoadUMJFile();
        }

        public static void ApplyPricesToAuctionList()
        {
            double minMultiplier = 1;
            double maxMultiplier = 1;
            foreach (var ahItem in Salesman.Instance.AhItems)
            {
                UMJItem item;
                if (!ahItem.Manual && Salesman.Instance.UMJ.Items.TryGetValue(ahItem.ItemId, out item))
                {
                    var price = item.MarketPrice;
                    ahItem.MinPrice = minMultiplier * price;
                    ahItem.MaxPrice = maxMultiplier * price;
                }
            }
            Salesman.Instance.BindingAhItems = new SortableBindingList<AhItem>();
            Salesman.Instance.AhItems.ForEach(Salesman.Instance.BindingAhItems.Add);
            Salesman.Save();
        }

        public static AhItem UMJToAhItem(UMJItem umjItem, int stackSize)
        {
            var ahItem = new AhItem();
            double minMultiplier = 1;
            double maxMultiplier = 1;
            Money minPrice = umjItem.MarketPrice;
            Money maxPrice = umjItem.MarketPrice;
            minPrice = new Money((int)(minMultiplier * umjItem.MarketPrice.TotalCopper));
            maxPrice = new Money((int)(maxMultiplier * umjItem.MarketPrice.TotalCopper));
            ahItem.Name = umjItem.ItemName;
            ahItem.ItemId = umjItem.ItemId;
            ahItem.Cancel = true;
            ahItem.Duration = AhItem.Durations.HalfDay;
            ahItem.MaxPrice = maxPrice;
            ahItem.MinPrice = minPrice;
            ahItem.SellAll = true;
            ahItem.StackSize = stackSize;
            return ahItem;
        }

        public string[] ItemNames
        {
            get
            {
                var itemNames = new string[Items.Count];
                int i = 0;
                foreach (var umjItem in Items)
                {
                    itemNames[i] = umjItem.Value.ItemName;
                    i++;
                }
                return itemNames;
            }
        }

        public static string FilePath
        {
            get { return Path.Combine(Salesman.SettingsFolder, string.Format("TUJ-{0}.xml", Salesman.RealmName)); }
        }
        public static string TempFilePath
        {
            get { return Path.Combine(Salesman.SettingsFolder, string.Format("TUJ-{0}2.xml", Salesman.RealmName)); }
        }
        public void LoadUMJFile(string fileName = null)
        {
            var items = new Dictionary<uint, UMJItem>();
            XElement xElement = XElement.Parse(Resources.DefaultUMJValues);
            LastUpdated = DateTime.Parse(xElement.FirstAttribute.Value);
            var xElements = xElement.Element("realm").Elements();
            foreach (var element in xElements)
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                foreach (var attribute in element.Attributes())
                {
                    item.Add(attribute.Name.ToString(), attribute.Value);
                }
                foreach (var itemInfo in element.Elements())
                {
                    item.Add(itemInfo.Name.ToString(), itemInfo.Value);
                }
                var itemId = UInt32.Parse(item["id"]);
                items.Add(itemId, new UMJItem(item, itemId));
            }
            Items = items;
        }
        public uint GetItemId(string itemName)
        {
            if (Items == null || !Items.Any())
                return 0;
            var item = Items.FirstOrDefault(i => i.Value.ItemName.ToLowerInvariant().Equals(itemName.ToLowerInvariant()));
            if (item.Value != null)
                return item.Key;
            item = Items.FirstOrDefault(i => i.Value.ItemName.ToLowerInvariant().Contains(itemName.ToLowerInvariant()));
            if (item.Value != null)
                return item.Key;
            return 0;
        }
    }
}
