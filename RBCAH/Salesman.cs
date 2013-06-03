using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using AuctionWrapper;
using Styx.WoWInternals;

namespace RBCAH
{
    [XmlRoot("Salesman", Namespace = "", IsNullable = false)]
    public class Salesman
    {
        public Salesman()
        {

            Instance = this;
            Instance.AhItems = new List<AhItem>();
            Instance.BindingAhItems = new SortableBindingList<AhItem>();
            Instance.BindingAhItems = new SortableBindingList<AhItem>();
            Instance.CycleMax = 15;
            Instance.CycleMin = 5;
            Instance.DoneBehaviorValue = DoneBehavior.WaitAndRepeat;
            Instance.LoadProfileBotbase = "";
            Instance.LoadProfilePath = "";
            Instance.MinPriceBehaviorValue = MinPriceBehavior.SkipPosting;
            Instance.UndercutFixedAmountTotalCopper = 550;
            Instance.UndercutPercentage = 1;
            Instance.UndercutUsePercentage = true;
            Instance.UndercutStacksizeTypeValue = UndercutStacksizeType.Same;
        }

        [XmlArray("AhItems"), XmlArrayItem("AhItems", typeof(AhItem))]
        public List<AhItem> AhItems { get; set; }

        [XmlIgnore]
        public SortableBindingList<AhItem> BindingAhItems { get; set; }

        [XmlIgnore]
        public UMJ UMJ { get; set; }
        [XmlIgnore]
        public static Salesman Instance { get; set; }

        [XmlIgnore]
        public DoneBehavior DoneBehaviorValue;

        public int DoneBehaviorNumValue
        {
            get { return (int)DoneBehaviorValue; }
            set
            {
                switch (value)
                {
                    case 0:
                        DoneBehaviorValue = DoneBehavior.WaitAndRepeat;
                        break;
                    case 1:
                        DoneBehaviorValue = DoneBehavior.LoadProfile;
                        break;
                    case 2:
                        DoneBehaviorValue = DoneBehavior.Shutdown;
                        break;
                }
            }
        }

        public string LoadProfilePath { get; set; }
        public string LoadProfileBotbase { get; set; }
        public int CycleMin { get; set; }
        public int CycleMax { get; set; }
        public bool UndercutUsePercentage { get; set; }
        public decimal UndercutPercentage { get; set; }

        [XmlIgnore]
        public Money UndercutFixedAmount;

        public int UndercutFixedAmountTotalCopper
        {
            get { return UndercutFixedAmount.TotalCopper; }
            set { UndercutFixedAmount = new Money(value); }
        }

        [XmlIgnore]
        public MinPriceBehavior MinPriceBehaviorValue;

        public int MinPriceBehaviorNumValue
        {
            get { return (int)MinPriceBehaviorValue; }
            set
            {
                switch (value)
                {
                    case 0:
                        MinPriceBehaviorValue = MinPriceBehavior.SkipPosting;
                        break;
                    case 1:
                        MinPriceBehaviorValue = MinPriceBehavior.IgnoreCheaper;
                        break;
                    case 2:
                        MinPriceBehaviorValue = MinPriceBehavior.UseFallback;
                        break;
                    case 3:
                        MinPriceBehaviorValue = MinPriceBehavior.Undercut;
                        break;
                    case 4:
                        MinPriceBehaviorValue = MinPriceBehavior.UseMinimum;
                        break;
                }
            }
        }

        [XmlIgnore]
        public static string CharacterName
        {
            get
            {
                string name;
                try
                {
                    name = Lua.GetReturnVal<string>("return GetUnitName(\"player\")", 0);
                }
                catch (Exception)
                {
                    name = "Johndoe";
                }
                return name;
            }
        }

        [XmlIgnore]
        public static string RealmName
        {
            get
            {
                string realm;
                try
                {
                    realm = Lua.GetReturnVal<string>("return GetRealmName()", 0);
                }
                catch (Exception)
                {
                    realm = "Whimsyshire";
                }
                return realm;
            }
        }

        [XmlIgnore]
        public UndercutStacksizeType UndercutStacksizeTypeValue;

        public int UndercutStacksizeTypeNumValue
        {
            get { return (int)UndercutStacksizeTypeValue; }
            set
            {
                switch (value)
                {
                    case 0:
                        UndercutStacksizeTypeValue = UndercutStacksizeType.Same;
                        break;
                    case 1:
                        UndercutStacksizeTypeValue = UndercutStacksizeType.Bigger;
                        break;
                    case 2:
                        UndercutStacksizeTypeValue = UndercutStacksizeType.Smaller;
                        break;
                    case 3:
                        UndercutStacksizeTypeValue = UndercutStacksizeType.Any;
                        break;
                }
            }
        }

        public static string ExeFolder { get { return Path.GetDirectoryName(Application.ExecutablePath); } }
        public static string SettingsFolder { get { return Path.Combine(ExeFolder, "Settings", "Salesman"); } }
        public static string SettingsLocation { get { return Path.Combine(SettingsFolder, string.Format("Settings-{0}[{1}].xml", CharacterName, RealmName)); } }

        public static void Load()
        {
            try
            {
                Instance = ObjectXMLSerializer<Salesman>.Load(SettingsLocation);
                Instance.BindingAhItems = new SortableBindingList<AhItem>();
                Instance.AhItems.ForEach(Instance.BindingAhItems.Add);
            }
            catch (Exception)
            {
                new Salesman();
            }
        }
        public static void Save()
        {
            var folder = Path.GetDirectoryName(SettingsLocation);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            ObjectXMLSerializer<Salesman>.Save(Instance, SettingsLocation);
        }

        public static bool IsInitialized;
        public static void Initialize()
        {
            Load();
            Instance.UMJ = new UMJ();
            IsInitialized = true;
        }
        public enum DoneBehavior
        {
            WaitAndRepeat = 0,
            LoadProfile = 1,
            Shutdown = 2,
        }
        public enum MinPriceBehavior
        {
            SkipPosting = 0,
            IgnoreCheaper = 1,
            UseFallback = 2,
            Undercut = 3,
            UseMinimum = 4,
        }
        public enum UMJ_DownloadFrequencyType
        {
            Hour = 0,
            Day = 1,
            Week = 2,
        }
        public enum UndercutStacksizeType
        {
            Same = 0,
            Bigger = 1,
            Smaller = 2,
            Any = 3,
        }
    }
}
