using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AuctionWrapper;
using AuctionWrapper.Auction;
using CommonBehaviors.Actions;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.CommonBot.Frames;
using Styx.CommonBot.Profiles;
using Styx.Plugins;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Action = Styx.TreeSharp.Action;

namespace RBCAH
{
    class SalesmanBase : BotBase
    {
        public static Helpers.States State;
        private static bool _cancelling;
        private static bool _searching;
        private static bool _posting;
        private static int _waitSeconds;
        private static bool _searchDone;
        private static Queue<uint> _sellQueue;
        private static Composite _root;

        public override void Pulse()
        {
            AuctionHouse.Pulse();
        }
        private static void Reset()
        {
            State = Helpers.States.Cancel;
            _cancelling = false;
            _searching = false;
            _posting = false;
            _waitSeconds = 0;
            _searchDone = false;
            _sellQueue = null;
            AuctionHouse.SellQueue.Clear();
            Cancel.CancelCheckQueue.Clear();
            Cancel.CancelDone -= Cancel_CancelDone;
            AuctionHouse.SellDone -= AuctionHouse_SellDone;
            AuctionHouse.SearchDone -= AuctionHouse_SearchDone;
        }
        public override System.Windows.Forms.Form ConfigurationForm
        {
            get
            {
                if (!_initialized)
                    Initialize();
                return new SalesmanForm();
            }
        }
        public override string Name
        {
            get { return "Salesman Lite"; }
        }

        public override Composite Root
        {
            get
            {
                return _root ?? (_root = CreateCustomBehavior());
            }
        }
        public override void Start()
        {
            ProfileManager.LoadEmpty();
            AuctionHouse.AttachEvents();
        }
        public override void Stop()
        {
            AuctionHouse.DetachEvents();
        }
        public override bool RequirementsMet
        {
            get
            {
                return true;
            }
        }
        public override PulseFlags PulseFlags
        {
            get { return PulseFlags.All; }
        }

        private static Composite CreateCustomBehavior()
        {
            return new PrioritySelector(
                new Decorator(ret => State == Helpers.States.Cancel, GetCancelBehavior()),
                new Decorator(ret => State == Helpers.States.GetMail, GetMailBehavior()),
                new Decorator(ret => State == Helpers.States.GotoAh, GetGotoAhBehavior()),
                new Decorator(ret => State == Helpers.States.SellOnAh, GetSellOnAhBehavior()),
                new Decorator(ret => State == Helpers.States.Wait, GetWaitBehavior())
                );
        }

        private static Composite GetMailBehavior()
        {
            return new Sequence(
                new DecoratorContinue(ret => !MailFrame.Instance.IsVisible, Helpers.InteractWithObject(ret => Helpers.NearestMailbox)),
                new DecoratorContinue(ret => (MailFrame.Instance.HasNewMail || Helpers.TakableMailCount > 0) && StyxWoW.Me.FreeNormalBagSlots > 0,
                    new Sequence(
                        new Action(delegate { MailFrame.Instance.OpenAllMail(); }),
                        new DecoratorContinue(ret => MailFrame.Instance.HasNewMail,
                            new Sequence(
                                new Wait(5, ret => false, new ActionAlwaysSucceed()),
                                new Action(delegate { Lua.DoString("CheckInbox()"); })
                                )
                            )
                        )
                    ),
                new Action(delegate
                {
                    Helpers.CacheMailboxInfo();
                    MailFrame.Instance.Close();
                    ChangeState(Helpers.States.GotoAh);
                })
                );
        }

        private static AuctionOwn[] _cachedOwnAuctions;
        private static Composite GetCancelBehavior()
        {
            return new Sequence(
                new DecoratorContinue(ret => Helpers.NearestAuctioneer == null, Helpers.GotoPoint(ret => Helpers.NearestAh, ret => 5)),
                new DecoratorContinue(ret => !AuctionFrame.Instance.IsVisible, Helpers.InteractWithUnit(ret => Helpers.NearestAuctioneer)),
                new DecoratorContinue(ret => State == Helpers.States.Cancel,
                    new Action(delegate
                    {
                        if (!_cancelling)
                        {
                            Cancel.CancelDone += Cancel_CancelDone;
                            Logger.Write("Starting Cancel");
                            using (StyxWoW.Memory.AcquireFrame())
                            {
                                _cachedOwnAuctions = AuctionHouse.OwnAuctions.ToArray();
                                foreach (var item in Salesman.Instance.AhItems.Where(i => i.Cancel))
                                {
                                    if (_cachedOwnAuctions.Any(a => a.Id == item.ItemId))
                                    {
                                        _cancelling = true;
                                        Cancel.AddToQueue(item.ItemId);
                                    }
                                }
                            }
                            if (!_cancelling)
                            {
                                Logger.Write("There was no auctions to scan for undercut!");
                                Cancel_CancelDone(null, EventArgs.Empty);
                            }
                        }
                        return RunStatus.Failure;
                    })
                )
            );
        }
        private static Composite GetGotoAhBehavior()
        {
            return new Sequence(
                Helpers.GotoPoint(ret => Helpers.NearestAh, ret => 5),
                new Action(delegate
                {
                    _searching = false;
                    ChangeState(Helpers.States.SellOnAh);
                })
                );
        }
        private static Composite GetSellOnAhBehavior()
        {
            return new Sequence(
                new DecoratorContinue(ret => !AuctionFrame.Instance.IsVisible, Helpers.InteractWithUnit(ret => Helpers.NearestAuctioneer)),
                new Action(delegate
                {
                    GetAuctionSearchQueue();
                })
                );
        }

        private static Composite GetWaitBehavior()
        {
            return new Sequence(
                Helpers.Wait(() => _waitSeconds),
                new Action(delegate { Styx.Helpers.KeyboardManager.AntiAfk(); }),
                new Action(delegate { ChangeState(Helpers.States.Cancel); })
                    );
        }

        private static Money CalculateUndercutPrice(Money money)
        {
            var undercutPercentageValue = (100 - Salesman.Instance.UndercutPercentage) / 100;
            var buyout = Salesman.Instance.UndercutUsePercentage
             ? money * undercutPercentageValue
             : money - Salesman.Instance.UndercutFixedAmount;
            return buyout;
        }

        private static void AuctionHouse_SearchDone(SearchDoneArgs ca)
        {
            Lua.DoString("AuctionFrameAuctions.duration=3");
            AuctionHouse.SearchDone -= AuctionHouse_SearchDone;
            var id = ca.SearchId;
            var myName = Lua.GetReturnVal<string>("return GetUnitName(\"player\")", 0);
            var auctions = ca.Auctions.Where(a => a.Id == id && a.BuyoutPrice > 0).OrderByDescending(a => a.BuyoutPricePerItem);
            var ahItems = Salesman.Instance.AhItems.Where(a => a.ItemId == id).OrderBy(a => a.SellAll);
            var inBagCount = (decimal)Helpers.InBagCount(ca.SearchId);
            Logger.WriteVerbose("Searching for {0} gave {1} auctions.", ca.SearchString, ca.Auctions.Count);
            foreach (var ahItem in ahItems)
            {
                IOrderedEnumerable<AuctionEntry> filteredAuctions;
                AhItem item = ahItem;
                switch (Salesman.Instance.UndercutStacksizeTypeValue)
                {
                    case Salesman.UndercutStacksizeType.Bigger:
                        filteredAuctions = auctions.Where(a => a.StackSize >= item.StackSize).OrderByDescending(a => a.BuyoutPricePerItem);
                        break;
                    case Salesman.UndercutStacksizeType.Smaller:
                        filteredAuctions = auctions.Where(a => a.StackSize <= item.StackSize).OrderByDescending(a => a.BuyoutPricePerItem);
                        break;
                    case Salesman.UndercutStacksizeType.Same:
                        filteredAuctions = auctions.Where(a => a.StackSize == item.StackSize).OrderByDescending(a => a.BuyoutPricePerItem);
                        break;
                    default:
                        filteredAuctions = auctions;
                        break;
                }
                var cheapest = filteredAuctions.FirstOrDefault();
                var needAuctions = Helpers.NeedAuctions(ahItem);
                var needAmount = needAuctions * ahItem.StackSize;
                if (needAmount > inBagCount)
                {
                    var maxAmount = (int)Math.Floor(inBagCount / ahItem.StackSize);
                    Logger.Write(
                        "I don't have enough {0} to post {1} stacks of {2} (total: {4}, I only have {5}). I am posting {3} stacks instead.",
                        ca.SearchString, needAuctions, ahItem.StackSize, maxAmount, needAmount, inBagCount);
                    needAuctions = maxAmount;
                    needAmount = needAuctions * ahItem.StackSize;
                }
                inBagCount -= needAmount;
                if (needAuctions > 0)
                {
                    Money buyout;
                    Money bid;
                    // If ignore cheaper is enabled, find the cheapest that is either my own auction or has an undercut price higher than minimum price
                    if (Salesman.Instance.MinPriceBehaviorValue == Salesman.MinPriceBehavior.IgnoreCheaper)
                    {
                        if (cheapest != null)
                        {
                            var oldCheapestPrice = cheapest.BuyoutPricePerItem;
                            cheapest = filteredAuctions.FirstOrDefault(
                                a => CalculateUndercutPrice(a.BuyoutPricePerItem) >= ahItem.MinPrice ||
                                 myName.ToLowerInvariant().Equals(a.Owner.ToLowerInvariant()));
                            if (cheapest != null && oldCheapestPrice.TotalCopper != cheapest.BuyoutPricePerItem.TotalCopper)
                            {
                                var undercutOld = CalculateUndercutPrice(oldCheapestPrice);
                                Logger.Write(
                                    "{0}'s cheapest price was {1}, which would make my undercut price {2}. My minimum price on this auction is {3} - " +
                                    "ignoring any auction that would bring my price lower than that. Now the cheapest auction is {4}",
                                    ahItem.Name, oldCheapestPrice, undercutOld, ahItem.MinPrice,
                                    cheapest.BuyoutPricePerItem);
                            }
                        }
                    }

                    // Post at max (fallback) price if no other auctions were found.
                    if (cheapest == null)
                    {
                        buyout = new Money(ahItem.MaxPriceTotal);
                        bid = buyout * 0.95;
                    }
                    else
                    {
                        // Post for same price if the cheapest auction was my own.
                        var myAuction = myName.ToLowerInvariant().Equals(cheapest.Owner.ToLowerInvariant());
                        if (myAuction)
                        {
                            buyout = cheapest.BuyoutPricePerItem;
                            bid = cheapest.MinBidPerItem;
                        }
                        else
                        {
                            buyout = CalculateUndercutPrice(cheapest.BuyoutPricePerItem);
                            bid = CalculateUndercutPrice(cheapest.MinBidPerItem);
                        }
                        // My undercut price is lower than minimum price
                        if (buyout.TotalCopper < ahItem.MinPriceTotal)
                        {
                            switch (Salesman.Instance.MinPriceBehaviorValue)
                            {
                                case Salesman.MinPriceBehavior.SkipPosting:
                                    Logger.Write(
                                        "{0}'s cheapest price was {1}, which would make my undercut price {2}. My minimum price on this auction is {3}, so skipping auction.",
                                        ahItem.Name, cheapest.BuyoutPricePerItem, buyout, ahItem.MinPrice);
                                    continue;
                                case Salesman.MinPriceBehavior.UseFallback:
                                    Logger.Write(
                                        "{0}'s cheapest price was {1}, which would make my undercut price {2}. My minimum price on this auction is {3}, so using fallback price of {4}.",
                                        ahItem.Name, cheapest.BuyoutPricePerItem, buyout, ahItem.MinPrice, ahItem.MaxPrice);
                                    buyout = ahItem.MaxPrice;
                                    bid = buyout * 0.95;
                                    break;
                                case Salesman.MinPriceBehavior.UseMinimum:
                                    Logger.Write(
                                        "{0}'s cheapest price was {1}, which would make my undercut price {2}. My minimum price on this auction is {3}, posting at minimum price.",
                                        ahItem.Name, cheapest.BuyoutPricePerItem, buyout, ahItem.MinPrice);
                                    buyout = ahItem.MinPrice;
                                    bid = buyout * 0.95;
                                    break;
                            }
                        }
                    }
                    var duration = Duration.TwelveHours;
                    switch (ahItem.Duration)
                    {
                        case AhItem.Durations.HalfDay:
                            duration = Duration.TwelveHours;
                            break;
                        case AhItem.Durations.OneDay:
                            duration = Duration.TwentyFourHours;
                            break;
                        case AhItem.Durations.TwoDays:
                            duration = Duration.FortyEightHours;
                            break;
                    }
                    var createAuction = new AuctionCreate(id, ca.SearchString, buyout, bid, duration,
                                  ahItem.StackSize, needAuctions);
                    if (!_posting)
                    {
                        _posting = true;
                        AuctionHouse.SellDone += AuctionHouse_SellDone;
                    }
                    Logger.WriteVerbose("Adding to sell queue: {0}", createAuction);
                    AuctionHouse.AddSell(createAuction);
                }
            }
            if (_sellQueue.Any())
            {
                AuctionHouse.SearchDone += AuctionHouse_SearchDone;
                AuctionHouse.StartSearch(_sellQueue.Dequeue());
            }
            else
            {
                _searchDone = true;
                if (!AuctionHouse.SellQueue.Any() && !_posting)
                {
                    AuctionHouse_SellDone(new SellDoneArgs(true));
                }
            }
        }

        private static void GetAuctionSearchQueue()
        {
            if (!_searching)
            {
                AuctionHouse.SearchDone += AuctionHouse_SearchDone;
                _sellQueue = new Queue<uint>();
                Logger.Write("Starting Post");
                var auctionsGroupedById = Salesman.Instance.AhItems.GroupBy(a => a.ItemId);
                using (StyxWoW.Memory.AcquireFrame())
                {
                    foreach (var item in auctionsGroupedById)
                    {
                        // Check each itemID if an auction is needed, and break if one is found to go to next itemID.
                        foreach (var ahItem in item)
                        {
                            if (Helpers.NeedAuctions(ahItem) > 0 && Helpers.InBagCount(ahItem.ItemId) >= ahItem.StackSize && ahItem.StackSize > 0)
                            {
                                _sellQueue.Enqueue(ahItem.ItemId);
                                _posting = false;
                                _searching = true;
                                _searchDone = false;
                                break;
                            }
                        }
                    }
                }
                if (_searching)
                    AuctionHouse.StartSearch(_sellQueue.Dequeue());
                else
                {
                    Logger.Write("Everything was up and running as supposed on AH");
                    _searchDone = true;
                    AuctionHouse_SellDone(new SellDoneArgs(true));
                }
            }

        }

        private static void AuctionHouse_SellDone(SellDoneArgs ca)
        {
            AuctionHouse.SellDone -= AuctionHouse_SellDone;
            _posting = false;
            if (_searchDone)
            {
                CacheOwnAuctions();
                // Helpers.CacheGBankCounts(); ???
                if ((Helpers.CachedMailsNotInbox > 0 || Helpers.CachedTakableMailCount > 0) && StyxWoW.Me.FreeNormalBagSlots > 2)
                {
                    ChangeState(Helpers.States.GetMail);
                }
                else
                {
                    switch (Salesman.Instance.DoneBehaviorValue)
                    {
                        case Salesman.DoneBehavior.WaitAndRepeat:
                            _waitSeconds = new Random().Next(Salesman.Instance.CycleMin * 60, Salesman.Instance.CycleMax * 60);
                            Logger.Write("Going to wait for {0} seconds", _waitSeconds);
                            ChangeState(Helpers.States.Wait);
                            break;
                        case Salesman.DoneBehavior.Shutdown:
                            Lua.DoString("ForceQuit()");
                            break;
                        case Salesman.DoneBehavior.LoadProfile:
                            Helpers.ChangeProfile(Salesman.Instance.LoadProfilePath, Salesman.Instance.LoadProfileBotbase);
                            break;
                    }
                }
            }
        }
        private static void Cancel_CancelDone(object sender, EventArgs e)
        {
            CacheOwnAuctions();
            ChangeState(Helpers.States.GetMail);
            Cancel.CancelDone -= Cancel_CancelDone;
            _cancelling = false;
        }

        private static void ChangeState(Helpers.States state)
        {
            State = state;
            Logger.Write("Switching state to {0}", state);
            Styx.Pathing.Navigator.Clear();
            TreeRoot.StatusText = string.Format("Current state: {0}", state);
        }
        private static void CacheOwnAuctions()
        {
            Helpers.CachedOwnedAuctions.Clear();
            foreach (var ownAuction in AuctionHouse.OwnAuctions)
            {
                if (!ownAuction.Sold)
                    Helpers.CachedOwnedAuctions.Add(ownAuction);
            }
        }

        private static bool _initialized;
        public override void Initialize()
        {
            if (_initialized)
                return;
            BotEvents.OnBotStarted += OnBotStarted;
            Logger.Write("Initializing");
            Salesman.Initialize();
            _initialized = true;
        }

        private void OnBotStarted(EventArgs args)
        {
            Reset();
        }
    }
}
