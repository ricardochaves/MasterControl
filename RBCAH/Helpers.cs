using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using AuctionWrapper;
using AuctionWrapper.Auction;
using CommonBehaviors.Actions;
using Styx;
using Styx.Common;
using Styx.Pathing;
using Styx.TreeSharp;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using Action = Styx.TreeSharp.Action;

namespace RBCAH
{
    public static class Helpers
    {
        public static List<AuctionOwn> CachedOwnedAuctions = new List<AuctionOwn>();
        private static readonly Stopwatch Throttle = new Stopwatch();
        public delegate WoWUnit UnitDelegate(object context);
        public delegate WoWGameObject GameObjectDelegate(object context);
        public delegate WoWPoint PointDelegate(object context);
        public delegate float FloatDelegate(object context);
        public delegate int IntDelegate(object context);
        public delegate bool BoolDelegate(object context);

        public static WoWUnit NearestAuctioneer
        {
            get { return ObjectManager.GetObjectsOfType<WoWUnit>().OrderBy(u => u.Distance).FirstOrDefault(u => u.IsAuctioneer); }
        }
        public static WoWGameObject NearestMailbox
        {
            get { return ObjectManager.GetObjectsOfType<WoWGameObject>().OrderBy(o => o.Distance).FirstOrDefault(o => o.SubType == WoWGameObjectType.Mailbox); }
        }
        public static WoWUnit NearestBanker
        {
            get { return ObjectManager.GetObjectsOfType<WoWUnit>().OrderBy(u => u.Distance).FirstOrDefault(u => u.IsBanker); }
        }
        public static WoWGameObject NearestGBank
        {
            get
            {
                var gbank = ObjectManager.GetObjectsOfType<WoWGameObject>().OrderBy(o => o.Distance).FirstOrDefault(o => o.SubType == WoWGameObjectType.GuildBank);
                return gbank;
            }
        }
        private static IEnumerable<WoWPoint> Ahs
        {
            get
            {
                return new[] {  new WoWPoint(-8363.244, 656.073, 97.15247),   // Stormwind - Dwarven District
                                new WoWPoint(-8835.385, 651.4479, 98.02765),  // Stormwind - Trade District
                                new WoWPoint(1925.794, -4224.325, 36.80094),  // Orgrimmar - Valley of Wisdom
                                new WoWPoint(2030.897, -4678.779, 28.9152),   // Orgrimmar - Valley of Honor
                                new WoWPoint(1614.572, -4421.972, 15.78702),  // Orgrimmar - Valley of Strength
                                new WoWPoint(1557.007, 280.031, -60.77499),   // Undercity - Canals (South-West)
                                new WoWPoint(1634.915, 278.7403, -60.77527),  // Undercity - Canals (North-West)
                                new WoWPoint(1634.411, 201.7141, -60.77528),  // Undercity - Canals (North-East)
                                new WoWPoint(1556.527, 201.8852, -60.77608),  // Undercity - Canals (South-East)
                                new WoWPoint(9626.768, -7135.504, 14.3236),   // Silvermoon City - The Bazaar
                                new WoWPoint(9694.326, -7496.006, 15.74432),  // Silvermoon City - The Royal Exchange
                                new WoWPoint(-1218.521, 95.25391, 132.805),   // Thunder Bluff
                                new WoWPoint(-4952.459, -916.8439, 504.2617), // Ironforge
                                new WoWPoint(9844.307, 2312.401, 1317.815),   // Darnassus
                                new WoWPoint(-3978.678, -11710.29, -137.2647),// The Exodar
                                new WoWPoint(5911.756, 727.8286, 641.199),    // Dalaran
                             };
            }
        }
        public static WoWPoint NearestAh
        {
            get
            {
                var nearestAh = Ahs.OrderBy(b => b.Distance(StyxWoW.Me.Location)).FirstOrDefault();
                return nearestAh;
            }
        }
        public static int InBagCount(uint itemID)
        {
            return AuctionWrapper.Helpers.InBagCount(itemID);
        }
        public static int MaxStackSize(uint itemID)
        {
            var lua = string.Format("return GetItemInfo({0})", itemID);
            return Lua.GetReturnVal<int>(lua, 7);
        }
        public static bool IsAuctionInvoice(uint mailIndex)
        {
            var lua = string.Format("invoice,_=GetInboxInvoiceInfo({0}) if invoice then return 1 else return 0 end", mailIndex);
            var invoice = Lua.GetReturnVal<int>(lua, 0) == 1;
            Logging.Write("Mail index {0} was {1}an invoice.", mailIndex, invoice ? "" : "not ");
            return invoice;
        }

        public static Composite GotoPoint(PointDelegate point, FloatDelegate range)
        {
            return new PrioritySelector(
                new Decorator(ret => point(ret).Distance(StyxWoW.Me.Location) <= range(ret), new ActionAlwaysSucceed()),
                new Sequence(
                    new DecoratorContinue(
                        ret => point(ret).Distance(StyxWoW.Me.Location) > range(ret),
                        new Action(ret => { Navigator.MoveTo(point(ret)); return RunStatus.Failure; })
                        ),
                    new Action(delegate { Navigator.Clear(); })
                    )
                );
        }

        public static Composite Wait(WaitGetTimeoutDelegate waitTime)
        {
            return new WaitContinue(waitTime, ret => false, new ActionAlwaysSucceed());
        }

        public static Composite GotoPoint(PointDelegate point)
        {
            return GotoPoint(point, range => 0);
        }
        public static Composite InteractWithUnit(UnitDelegate unit)
        {
            return new Sequence(
                new DecoratorContinue(ret => unit(ret).Distance > unit(ret).InteractRange, GotoPoint(ret => unit(ret).Location, ret => unit(ret).InteractRange)),
                new DecoratorContinue(ret => StyxWoW.Me.MovementInfo.IsMoving, new Action(delegate { WoWMovement.MoveStop(); })),
                new Sequence(
                    new Action(ret => unit(ret).Interact()),
                    new Wait(TimeSpan.FromMilliseconds(700), ret => false, new ActionAlwaysSucceed())
                    )
                );
        }
        public static Composite InteractWithObject(GameObjectDelegate wowObject)
        {
            return new Sequence(
                new DecoratorContinue(ret => wowObject(ret).Distance > wowObject(ret).InteractRange, GotoPoint(ret => wowObject(ret).Location, ret => wowObject(ret).InteractRange)),
                new DecoratorContinue(ret => StyxWoW.Me.MovementInfo.IsMoving, new Action(delegate { WoWMovement.MoveStop(); })),
                new Sequence(
                    new Action(ret => wowObject(ret).Interact()),
                    new Wait(TimeSpan.FromMilliseconds(700), ret => false, new ActionAlwaysSucceed())
                    )
                );
        }
        public static int NeedAuctions(AhItem ahItem)
        {
            var currentlyOn = CachedOwnedAuctions.Where(a => a.Id == ahItem.ItemId && a.StackSize == ahItem.StackSize);
            return ahItem.SellAll ? 200000 : ahItem.Stacks - currentlyOn.Count();
        }

        public static void ChangeProfile(string profilePath, string botbase)
        {
            var bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = false;
            bw.WorkerReportsProgress = false;
            bw.DoWork += bw_DoWork;
            if (!bw.IsBusy)
                bw.RunWorkerAsync(new[] { profilePath, botbase });
        }

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var arguments = e.Argument as string[];
            var profilePath = arguments[0];
            var botbase = arguments[1].ToLowerInvariant();
            foreach (var botBase in Styx.CommonBot.BotManager.Instance.Bots)
            {
                if (botBase.Key.ToLowerInvariant().Contains(botbase) || botbase.Contains(botBase.Key.ToLowerInvariant()))
                    Styx.CommonBot.BotManager.Instance.SetCurrent(botBase.Value);
            }
            Thread.Sleep(1500);
            Styx.CommonBot.Profiles.ProfileManager.LoadNew(profilePath);
            Styx.CommonBot.TreeRoot.Start();
        }

        public static int CachedTakableMailCount;
        public static int TakableMailCount
        {
            get
            {
                var inbox = Styx.CommonBot.Frames.MailFrame.Instance.GetAllMails().Count(m => m.Copper > 0 || m.ItemCount > 0);
                return inbox;
            }
        }

        public static int CachedMailsNotInbox;
        public static int MailsNotInbox
        {
            get
            {
                var totalMailCount = Lua.GetReturnVal<int>("return GetInboxNumItems()", 1);
                var notInbox = totalMailCount - Styx.CommonBot.Frames.MailFrame.Instance.MailCount;
                return notInbox;
            }
        }

        public static void CacheMailboxInfo()
        {
            CachedMailsNotInbox = MailsNotInbox;
            CachedTakableMailCount = TakableMailCount;
        }

        public static Money MyMoney
        {
            get { return new Money(Lua.GetReturnVal<int>("return GetMoney()", 0)); }
        }

        public enum States
        {
            Cancel,
            GetMail,
            GotoAh,
            SellOnAh,
            Wait,
        }
    }
}
