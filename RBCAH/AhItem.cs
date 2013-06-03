using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using AuctionWrapper;

namespace RBCAH
{
    public class AhItem : INotifyPropertyChanged
    {
        private static bool _isSettingId;
        private uint _itemId;
        [XmlAttribute]
        public uint ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;
                NotifyPropertyChanged("ItemId");
                // Let the list be initialized before applying this logic.
                if (Salesman.IsInitialized)
                {
                    _isSettingId = true;
                    var umjItem = Salesman.Instance.UMJ.Items[_itemId];
                    Name = umjItem.ItemName;
                    MinPriceTotal = (int)(umjItem.AvgPrice.TotalCopper * ((decimal)(100 - 30) / 100));
                    MaxPriceTotal = (int)(umjItem.AvgPrice.TotalCopper * ((decimal)(100 + 10) / 100));
                    _isSettingId = false;
                }
            }
        }

        private bool _cancel;
        [XmlAttribute]
        public bool Cancel
        {
            get { return true; }
            set
            {
                _cancel = true;
                NotifyPropertyChanged("Cancel");
            }
        }

        private string _name;
        [XmlAttribute]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
                if (!_isSettingId && Salesman.IsInitialized)
                {
                    var id = Salesman.Instance.UMJ.GetItemId(_name);
                    if (id == 0)
                    {
                        uint.TryParse(_name, out id);
                    }
                    if (id != 0)
                        ItemId = id;
                }
            }
        }
        [XmlIgnoreAttribute]
        public Money MinPrice;
        [XmlIgnoreAttribute]
        public Money MaxPrice;
        [XmlAttribute]
        public int MaxPriceTotal
        {
            get { return MaxPrice.TotalCopper; }
            set
            {
                MaxPrice.TotalCopper = value;
                NotifyPropertyChanged("MaxPriceTotal");
            }
        }
        [XmlAttribute]
        public int MinPriceTotal
        {
            get { return MinPrice.TotalCopper; }
            set
            {
                MinPrice.TotalCopper = value;
                NotifyPropertyChanged("MinPriceTotal");
            }
        }
        [XmlAttribute]
        public int DurationInt
        {
            get { return (int)Duration; }
            set
            {
                switch (value)
                {
                    case 12:
                        Duration = Durations.HalfDay;
                        break;
                    case 24:
                        Duration = Durations.OneDay;
                        break;
                    case 48:
                        Duration = Durations.TwoDays;
                        break;
                    default:
                        Duration = Durations.HalfDay;
                        break;
                }
                NotifyPropertyChanged("DurationInt");
            }
        }

        [XmlIgnore]
        public string DurationString
        {
            get
            {
                switch (Duration)
                {
                    case Durations.HalfDay:
                        return "12h";
                    case Durations.OneDay:
                        return "24h";
                    case Durations.TwoDays:
                        return "48h";
                    default:
                        return "???";
                }
            }
            set
            {
                switch (value)
                {
                    case "12h":
                        Duration = Durations.HalfDay;
                        break;
                    case "24h":
                        Duration = Durations.OneDay;
                        break;
                    case "48h":
                        Duration = Durations.TwoDays;
                        break;
                    default:
                        Duration = Durations.HalfDay;
                        break;
                }
                NotifyPropertyChanged("DurationString");
            }
        }

        private Durations _duration;
        [XmlIgnoreAttribute]
        public Durations Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                //NotifyPropertyChanged("Duration");
            }
        }

        private int _stacks;
        [XmlAttribute]
        public int Stacks
        {
            get { return _stacks; }
            set
            {
                _stacks = value;
                NotifyPropertyChanged("Stacks");
            }
        }

        private int _stackSize;
        [XmlAttribute]
        public int StackSize
        {
            get { return _stackSize; }
            set
            {
                _stackSize = value;
                NotifyPropertyChanged("StackSize");
            }
        }

        private bool _sellAll;
        [XmlAttribute]
        public bool SellAll
        {
            get { return _sellAll; }
            set
            {
                _sellAll = value;
                NotifyPropertyChanged("SellAll");
                NotifyPropertyChanged("Stacks");
            }
        }

        private bool _manual;
        [XmlAttribute]
        public bool Manual
        {
            get { return _manual; }
            set
            {
                _manual = value;
                NotifyPropertyChanged("Manual");
            }
        }

        public int WithdrawAmount { get { return StackSize * Stacks; } }

        public AhItem(string name, uint itemId, int minPrice, int maxPrice, bool cancel, Durations duration, int stacks, int stackSize, bool sellAll)
        {
            Name = !name.Trim().Equals("") ? name : itemId.ToString(CultureInfo.InvariantCulture);
            ItemId = itemId;
            MinPrice = new Money(minPrice);
            MaxPrice = new Money(maxPrice);
            Cancel = cancel;
            Duration = duration;
            Stacks = stacks;
            StackSize = stackSize;
            SellAll = sellAll;
        }
        public AhItem(string name, uint itemId, Money minPrice, Money maxPrice, bool cancel, Durations duration, int stacks, int stackSize, bool sellAll)
        {
            Name = !name.Trim().Equals("") ? name : itemId.ToString(CultureInfo.InvariantCulture);
            ItemId = itemId;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Cancel = cancel;
            Duration = duration;
            Stacks = stacks;
            StackSize = stackSize;
            SellAll = sellAll;
        }

        public AhItem()
        {
            MinPrice = new Money(0);
            MaxPrice = new Money(0);
            Duration = Durations.OneDay;
        }
        public enum Durations
        {
            TwoDays = 48,
            OneDay = 24,
            HalfDay = 12,
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
