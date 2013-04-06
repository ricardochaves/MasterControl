using Styx.Plugins;
using Styx.WoWInternals;
using Styx.CommonBot;
using Styx.Common;
using System;
using System.IO;
using System.Collections.Generic;
//using System.Timers;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Styx.MemoryManagement;


namespace MasterControl
{
    class MasterControl : HBPlugin
    {

        const string SECURETY_KEY = "3kl4j3lk5n3lk3j43kl4j34n3,m4n34k34hj3l4h34nm3,.n43";

        WSBot.WSBot b = new WSBot.WSBot();
        WSBot.estDados d = new WSBot.estDados();
        WSBot.estSessao Sessao = new WSBot.estSessao();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        Timer times;
        String wowpath;
        public static Boolean isInit = false;

        DateTime lastUpdate;
        DateTime startTime;

        private string zona;
        private string subzona;

        private Dictionary<string, string> data;

        private Dictionary<string, string> status = new Dictionary<string, string>();

        #region Construtor

        public MasterControl()
        {
        }

        #endregion

        #region Overrides
        public override string Author
        {
            get { return "BaltazarChaves"; }
        }

        public override string Name
        {
            get { return "MasterControl"; }
        }

        public override void Pulse()
        {
            //PARA TESTAR SE O PULSE ESTÁ RODANDO
            //Logging.Write(string.Format("[MasterControl]: pulse"));
            updater();
        }

        public override System.Version Version
        {
            get { return new System.Version(1, 1, 0); }
        }

        public override void OnButtonPress()
        {
            new FormSettings().ShowDialog();

        }

        public override bool WantButton { get { return true; } }
        public override string ButtonText { get { return "Settings"; } }

        public override void Initialize()
        {
            
            if (MasterControl.isInit == true)
            {
                return;
            }

            //VINCULANDO ENVENTOS DO BOT
            Styx.CommonBot.BotEvents.OnBotStopped += onStop;
            Styx.CommonBot.BotEvents.OnBotStarted += onStart;
            //Styx.CommonBot.BotEvents.Profile.OnNewOuterProfileLoaded
            //Styx.CommonBot.BotEvents

            //LEVANTANDO QUAIS EVENTOS ELE VAI PEGAR NO JOGO.
            //AQUI VAMOS PEGAR TUDO O QUE QUEREMOS.
            Lua.Events.AttachEvent("GUILDBANKFRAME_OPENED", GbankUpdate);
            Lua.Events.AttachEvent("GUILDBANK_UPDATE_MONEY", GbankUpdate);
            //Lua.Events.AttachEvent("LOOT_OPENED", LOOTOPENED);
            //Lua.Events.AttachEvent("LOOT_SLOT_CLEARED", LOOTSLOTCLEARED);
            Lua.Events.AttachEvent("ZONE_CHANGED", ZONECHANGED);
            Lua.Events.AttachEvent("ZONE_CHANGED_INDOORS", ZONECHANGEDINDOORS);
            Lua.Events.AttachEvent("ZONE_CHANGED_NEW_AREA", ZONECHANGEDNEWAREA);
            
            Lua.Events.AttachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;


            //CONTROLES DE TEMPO, TENTAR ENTENDER DEPOIS
            lastUpdate = DateTime.Now;
            startTime = DateTime.Now;
            
            //IniciaSessao();
            MasterControl.isInit = true;
            Logging.Write(string.Format("[MasterControl]: Version {0} Loaded.", Version.ToString()));

            var profile = Styx.CommonBot.BotManager.Current;
            Logging.Write(string.Format("[MasterControl]: Styx.CommonBot.BotManager.Current: {0}.", profile));
            
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;
            Styx.CommonBot.BotEvents.Player.OnMobKilled -= onMobkill;

            Lua.Events.DetachEvent("GUILDBANKFRAME_OPENED", GbankUpdate);
            Lua.Events.DetachEvent("GUILDBANK_UPDATE_MONEY", GbankUpdate);
            //Lua.Events.DetachEvent("LOOT_OPENED", LOOTOPENED);
            //Lua.Events.DetachEvent("LOOT_SLOT_CLEARED", LOOTSLOTCLEARED);
            Lua.Events.DetachEvent("CHAT_MSG_LOOT", CHATMSGLOOT);

            Lua.Events.DetachEvent("ZONE_CHANGED", ZONECHANGED);
            Lua.Events.DetachEvent("ZONE_CHANGED_INDOORS", ZONECHANGEDINDOORS);
            Lua.Events.DetachEvent("ZONE_CHANGED_NEW_AREA", ZONECHANGEDNEWAREA);


            AppDomain.CurrentDomain.ProcessExit -= CurrentDomainProcessExit;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainUnhandledException;

            

            Styx.CommonBot.BotEvents.OnBotStopped -= onStop;
            Styx.CommonBot.BotEvents.OnBotStarted -= onStart;

            times.Dispose();
            MasterControl.isInit = false;

            FinalizaSessao();

            //Logging.Write("[MasterControl]: Session Finished - ID: {0}: ", Sessao.id);
            
            
        }

        #endregion

        //private void LOOTOPENED(object sender, LuaEventArgs args)
        //{

        //    int qtd;

        //    qtd = Lua.GetReturnVal<int>("local value = GetNumLootItems(); return value", 0);
            
        //    Logging.Write("[MasterControl]: LOOT_OPENED - {0}", qtd);
        //}
        //private void LOOTSLOTCLEARED(object sender, LuaEventArgs args)
        //{
        //    Logging.Write("[MasterControl]: LOOT_SLOT_CLEARED");
        //}

        void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FinalizaSessao();
        }

        private void CurrentDomainProcessExit(object sender, EventArgs e)
        {
            FinalizaSessao();
        }

        private void CHATMSGLOOT(object sender, LuaEventArgs args)
        {

            WSBot.estLoot l = new WSBot.estLoot();

            //PEGANDO ITEM ID
            string item;
            item = args.Args[0].ToString();
            item = item.Substring(item.IndexOf("item:") + 5, item.Length - (item.IndexOf("item:") + 5));
            item = item.Substring(0, item.IndexOf(":"));

            l.idItem = Convert.ToInt32(item);

            //PEGANDO QUANTIDADE DO LOOT
            string qtd;
            string tempQTD;

            qtd = args.Args[0].ToString();
            tempQTD = qtd.Substring(qtd.IndexOf("|h|r") + 4, qtd.Length - (qtd.IndexOf("|h|r") + 4));
            if (tempQTD.Substring(0, 1) == "x")
            {
                qtd = tempQTD.Substring(1, tempQTD.IndexOf(".") - 1);
            }
            else
            {
                qtd = "1";
            }

            l.qtd = Convert.ToInt32(qtd);
            l.data = DateTime.Now;

            b.IncluirLoot(Sessao, l, SECURETY_KEY);

        }

        private void GbankUpdate(object sender, LuaEventArgs args)
        {
            data["gbankmoney"] = Convert.ToUInt32(Styx.WoWInternals.Lua.GetReturnVal<Int32>("return GetGuildBankMoney()", 0)).ToString();
        }

        private void IniciaSessao()
        {

            if (Sessao.id == 0)
            {
                
                Sessao = new WSBot.estSessao();
                Sessao.nome = Styx.StyxWoW.Me.Name.ToString();
                Sessao.dtInicio = DateTime.Now;
                Sessao.versao = Version.ToString();
                Sessao.server = Styx.StyxWoW.Me.RealmName.ToString();
                Sessao.lvl = Styx.StyxWoW.Me.Level.ToString();
                Sessao.dirWoW = wowpath = Styx.StyxWoW.Memory.Process.MainModule.FileName.Substring(0, Styx.StyxWoW.Memory.Process.MainModule.FileName.Length - 8);
                
                try
                {
                    Sessao.id = b.IniciaSessao(Sessao, SECURETY_KEY);
                }
                catch (System.Web.Services.Protocols.SoapException ex)
                {
                    Logging.Write(string.Format("[MasterControl]: ERRO: {0}", ex.Message));
                    return;
                }
                
                Logging.Write(string.Format("[MasterControl]: New Session Begins - ID: {0}", Sessao.id));
            }
        }

        private void FinalizaSessao()
        {
            if (Sessao.id != 0)
            {
                b.FinalizaSessao(Sessao, DateTime.Now, SECURETY_KEY);
                Sessao = new WSBot.estSessao();
            }
            
        }

        #region Funções Especiais

        private void AtualizaZonas()
        {
            //Returns the "official" name of the zone or instance in which the player is located. This name matches that seen in the Who, Guild, and Friends UIs when reporting character locations. It may differ from those the default UI displays in other locations (GetZoneText() and GetMinimapZoneText()), especially if the player is in an instance: e.g. this function returns "The Stockade" when the others return "Stormwind Stockade".
            zona = Lua.GetReturnVal<string>("local value = GetRealZoneText(); return value", 0);
            
            //Returns the name of the minor area in which the player is located. Subzones are named regions within a larger zone or instance: e.g. the Valley of Trials in Durotar, the Terrace of Light in Shattrath City, or the Njorn Stair in Utgarde Keep.
            subzona = Lua.GetReturnVal<string>("local value = GetSubZoneText(); return value", 0);

        }
        private string RetornaZoneName()
        {
            return zona;
        }
        private string RetornaSubZoneName()
        {
            return subzona;
        }

        #endregion


        #region onX
        private void onMobkill(BotEvents.Player.MobKilledEventArgs args)
        {
        }
        private void onStart(EventArgs args)
        {

            //Guardando a data inicial, remover no futuro.
            startTime = DateTime.Now;

            Styx.CommonBot.BotEvents.Player.OnPlayerDied += onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp += onLevel;

            AtualizaZonas();
            IniciaSessao();
            
        }
        private void onStop(EventArgs args)
        {

            Styx.CommonBot.BotEvents.Player.OnPlayerDied -= onDead;
            Styx.CommonBot.BotEvents.Player.OnLevelUp -= onLevel;

            Logging.Write(string.Format("[MasterControl]: Session Stoped - ID: {0}", Sessao.id));

            FinalizaSessao();
            
            Sessao = new WSBot.estSessao();

        }
        private void onDead()
        {

            WSBot.estMorte m = new WSBot.estMorte();

            using (Styx.StyxWoW.Memory.AcquireFrame())
            {
                m.mortes = Convert.ToInt32(Styx.CommonBot.GameStats.Deaths);
                m.morteshora = Convert.ToInt32(Styx.CommonBot.GameStats.DeathsPerHour);
                m.dt = DateTime.Now;
                m.RealZoneText = RetornaZoneName();
                m.SubZoneText = RetornaSubZoneName();                
            }

            b.IncluiNovaMorte(Sessao, m, SECURETY_KEY);
            
            Logging.Write("[MasterControl]: Morto!");

            //VERIFICAR O SCREENSHOT
            //if (MasterControlSettings.Instance.scDied) screenie();

        }
        private void onLevel(BotEvents.Player.LevelUpEventArgs args)
        {

            WSBot.estLevelUp lvl = new WSBot.estLevelUp();

            lvl.lvl = Convert.ToInt32(Styx.StyxWoW.Me.Level);
            lvl.data = DateTime.Now;

            b.IncluirLevelUp(Sessao, lvl, SECURETY_KEY);

            Logging.Write("[MasterControl]: Level up!");

            if (!status.ContainsValue("Level up!"))
                status.Add("message", "Level up!");
            
            //if (MasterControlSettings.Instance.scLevel) screenie();
        }
        private void ZONECHANGED(object sender, LuaEventArgs args)
        {
            AtualizaZonas();
        }
        private void ZONECHANGEDINDOORS(object sender, LuaEventArgs args)
        {
            AtualizaZonas();
        }
        private void ZONECHANGEDNEWAREA(object sender, LuaEventArgs args)
        {
            AtualizaZonas();
        }

        #endregion

        //private void screenie()
        //{
        //    Logging.Write("[MasterControl]: Screenshot requested");
        //    Lua.Events.AttachEvent("SCREENSHOT_SUCCEEDED", uploadscreen);
        //    Lua.DoString("TakeScreenshot()");
        //}

        //private void send(Dictionary<string, string> datax, Boolean onTimer)
        //{
        //    if (!onTimer && MasterControlSettings.Instance.pdebug) Logging.Write(string.Format("[bC]: start sending data {0}", on));
        //    if (MasterControl.on && data["server"] != "" && data["apikey"] != "")
        //    {
        //        if (!onTimer && MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: start sending data on"));

        //        if (MasterControlSettings.Instance.pdebug) Logging.Write("[bC]: send server: " + data["server"]);


        //        try
        //        {
        //            string[] sendParam = new string[datax.Count + 1];
        //            string[] sendVal = new string[datax.Count + 1];
        //            int i = 0;
        //            foreach (var pair in datax)
        //            {
        //                sendParam[i] = pair.Key;
        //                sendVal[i] = pair.Value;
        //                i++;
        //            }
        //            sendParam[i] = "status";
        //            sendVal[i] = JSON.JsonEncode(status);
        //            status.Clear();

        //            string response = "";

        //            data["chat_message"] = "";
        //            data["chat_type"] = "";
        //            data["chat_from"] = "";
        //            data["screen"] = "";
        //            //data["runningtime"] = "";


        //            d.xp = data["xp"];
        //            d.xp_needed = data["xp_needed"];
        //            d.name = data["name"];
                    
        //            b.RecebeDados(d);

        //            if (!onTimer && MasterControlSettings.Instance.debug) response = Util.HttpPost("http://localhost/bc/index.php?r=api/mongo", sendParam, sendVal);
        //            else if (!onTimer && MasterControlSettings.Instance.ownurl.Length > 7) response = Util.HttpPost(MasterControlSettings.Instance.ownurl, sendParam, sendVal);
        //            else response = Util.HttpPost("http://buddyc.eu/index.php?r=api/mongo", sendParam, sendVal);


        //            if (!onTimer && MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: sent V{0}", Version.ToString()));
        //            if (!onTimer && MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: res {0}", response));

        //            if (response != "")
        //            {
        //                Hashtable resJson = (Hashtable)JSON.JsonDecode(response);

        //                switch ((string)resJson["response"])
        //                {
        //                    case "":
        //                        break;
        //                    case "success":
        //                        break;
        //                    case "killwow":
        //                        Logging.Write(string.Format("[bC]:  {0}", "Kill Wow requested!"));
        //                        Styx.StyxWoW.Memory.Process.Kill();
        //                        break;
        //                    case "screen":
        //                        screenie();
        //                        break;
        //                    case "chat":
        //                        sendChat((string)resJson["type"], (string)resJson["message"], (string)resJson["to"]);
        //                        break;
        //                    case "stophb":
        //                        Styx.CommonBot.TreeRoot.Stop();
        //                        break;
        //                    case "starthb":
        //                        Styx.CommonBot.TreeRoot.Start();
        //                        break;
        //                    default:
        //                        Logging.Write(string.Format("[bC]:  {0}", response));
        //                        break;

        //                }
        //                if (resJson.ContainsKey("version"))
        //                {
        //                    Logging.Write(string.Format("[bC]: version: {0}", resJson["version"]));
        //                }
        //            }
        //        }
        //        catch (System.InvalidCastException e)
        //        {
        //            if (MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: error cast {0}", e.Message));
        //        }
        //        catch (Exception e)
        //        {
        //            if (MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: error send {0}", e.Message));
        //        }


        //        if (!onTimer && MasterControlSettings.Instance.pdebug) Logging.Write(string.Format("[bC]: end sending data"));
        //    }
        //    else
        //    {
        //        if (!on)
        //            times.Dispose();

        //        if (data["server"] == "" || data["apikey"] == "")
        //            Logging.Write(string.Format("[bC]: Error server or apikey not set " + data["server"]));
        //    }

        //}

        private void sendChat(string type, string message, string to)
        {
            Lua.DoString("SendChatMessage(\"" + message + "\", \"" + type + "\", nil, \"" + to + "\");");
            Logging.Write(string.Format("[bC]: send Chat channel: {0} Message: {1} To:{2}", type, message, to));

        }

        //private void uploadscreen(object sender, LuaEventArgs args)
        //{
        //    Lua.Events.DetachEvent("SCREENSHOT_SUCCEEDED", uploadscreen);
        //    //Logging.Write(string.Format("[bC]: Screenshot Version {0}  {1}", Version.ToString(), BuddyConSettings.Instance.scripturl.Length));
        //    //Logging.Write("[bC]: Screenshot upload");
        //    var directory = new DirectoryInfo(wowpath + "\\Screenshots\\");
        //    string ret = "";
        //    if (MasterControlSettings.Instance.scripturl.Length > 15)
        //        ret = Util.PostToFtp(directory + Util.GetLatestWritenFileFileInDirectory(directory).ToString());
        //    else
        //        ret = Util.PostToImgur(directory + Util.GetLatestWritenFileFileInDirectory(directory).ToString(), "e6b704a473bc5894e03ff99db649e825");

        //    data["screen"] = ret;
        //    status.Add("message", "Screenshot sent");
        //    Logging.Write("[MasterControl]: Screenshot sent");
        //    send(data, false);

        //}

        private void updater()
        {
            //ATUALIZA A CADA 15 SEGUNDOS
            if (lastUpdate.AddSeconds(15) < DateTime.Now)
            {
                getData();
                //send(data, false);
                lastUpdate = DateTime.Now;
            }
        }

        //private void timesUpdater(object sender)
        //{
        //    Dictionary<string, string> dat = (Dictionary<string, string>)sender;
        //    send(dat, true);
        //    /*
        //    if (lastUpdate.AddSeconds(25) < DateTime.Now)
        //    {
        //        if (BuddyConSettings.Instance.pdebug) Logging.Write(string.Format("[bC]: times start"));
        //        if (BuddyConSettings.Instance.debug) Logging.Write(string.Format("[bC]: start update"));

        //        Dictionary<string, string> dat =getData();
        //        send(dat);
        //        if (BuddyConSettings.Instance.pdebug) Logging.Write(string.Format("[bC]: times end"));
        //        lastUpdate = DateTime.Now;
        //    }*/
        //}

        private Dictionary<string, string> getData()
        {
            try
            {
                
                using (Styx.StyxWoW.Memory.AcquireFrame())
                {

                    if (Sessao.id != 0)
                    {
                        WSBot.estDetalhe d = new WSBot.estDetalhe();

                        d.lvl = Convert.ToInt32(Styx.StyxWoW.Me.Level).ToString();

                        d.runningtime = (DateTime.Now - startTime).TotalSeconds.ToString();
                        d.xp = Convert.ToUInt32(Styx.StyxWoW.Me.Experience).ToString();
                        d.xp_needed = Convert.ToUInt32(Styx.StyxWoW.Me.NextLevelExperience).ToString();
                        d.xph = Convert.ToUInt32(Styx.CommonBot.GameStats.XPPerHour).ToString();
                        d.timetolevel = Convert.ToUInt32(Styx.CommonBot.GameStats.TimeToLevel.TotalSeconds).ToString();

                        d.kills = Convert.ToUInt32(Styx.CommonBot.GameStats.MobsKilled).ToString();
                        d.killsh = Convert.ToUInt32(Styx.CommonBot.GameStats.MobsPerHour).ToString();

                        d.honor = Convert.ToUInt32(Styx.CommonBot.GameStats.HonorGained).ToString();
                        d.honorh = Convert.ToUInt32(Styx.CommonBot.GameStats.HonorPerHour).ToString();
                        d.bgwin = Convert.ToUInt32(Styx.CommonBot.GameStats.BGsWon).ToString();
                        d.bglost = Convert.ToUInt32(Styx.CommonBot.GameStats.BGsLost).ToString();
                        var prof = Styx.CommonBot.Profiles.ProfileManager.CurrentProfile.Name;
                        
                        d.gold = Convert.ToUInt32(Styx.StyxWoW.Me.Copper).ToString();
                        //d.nodeh = JSON.JsonEncode(Bots.Gatherbuddy.GatherbuddyBot.NodeCollectionCount);

                        d.RealZoneText = RetornaZoneName();
                        d.SubZoneText = RetornaSubZoneName();

                        b.IncluirDetalhe(Sessao, d, SECURETY_KEY);
                    }
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                if (MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: getting error {0} stack: {1}", e.Message, e.StackTrace));
            }
            //}
            return data;
        }

        //#region Chat
        //private void Chatter(string message, string author = "", string type = "")
        //{
        //    Logging.Write(string.Format("[bC]: Chat: From: {1}  Message: {0} Type: {2}", message, author, type));
        //    data["chat_message"] = message;
        //    data["chat_type"] = type;
        //    data["chat_from"] = author;

        //    if (
        //        (MasterControlSettings.Instance.notfSay && type == "CHAT_MSG_SAY") ||
        //        (MasterControlSettings.Instance.notfGuild && type == "CHAT_MSG_GUILD") ||
        //        (MasterControlSettings.Instance.notfBG && (type == "CHAT_MSG_BATTLEGROUND" || type == "CHAT_MSG_BATTLEGROUND_LEADER")) ||
        //        (MasterControlSettings.Instance.notfRaid && (type == "CHAT_MSG_RAID" || type == "CHAT_MSG_RAID_LEADER")) ||
        //        (MasterControlSettings.Instance.notfWhisper && type == "CHAT_MSG_WHISPER")
        //        )
        //    {
        //        if (MasterControlSettings.Instance.debug) Logging.Write("[bC]: send notification");
        //        Util.sendToProwl("Chat", string.Format("[bC]: Chat: From: {1}  Message: {0} Type: {2}", message, author, type), data["name"], data["server"]);
        //        if (MasterControlSettings.Instance.androidapi.Length > 10) Util.SendNotification(MasterControlSettings.Instance.androidapi, "{\"from\":\"" + author + "\", \"message\":\"" + message + "\", \"type\":\"" + type + "\"}");
        //    }
        //    //if (MasterControlSettings.Instance.scChat) screenie();
        //    //send(data);
        //}
        //private void BNetWhisper(object sender, LuaEventArgs args)
        //{
        //    if (MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: Chat: From: {1}  Message: {0} Type: {2}", (string)args.Args[0], args.Args[1], args.ToString()));
        //}

        //private void GMResponse(object sender, LuaEventArgs args)
        //{
        //    if (MasterControlSettings.Instance.debug) Logging.Write(string.Format("[bC]: Chat: From: {1}  Message: {0} Type: {2}", (string)args.Args[0], args.Args[1], args.ToString()));
        //}

        //private void Chat_Raid(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);
        //}

        //private void Chat_BG(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Emote(Styx.CommonBot.Chat.ChatAuthoredEventArgs e)
        //{

        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Party(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Whisper(Styx.CommonBot.Chat.ChatWhisperEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Yell(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Say(Styx.CommonBot.Chat.ChatLanguageSpecificEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}

        //private void Chat_Guild(Styx.CommonBot.Chat.ChatGuildEventArgs e)
        //{
        //    Thread thread = new Thread(delegate() { Chatter(e.Message, e.Author, e.EventName); });
        //    thread.Start();
        //    //Chatter(e.Message, e.Author, e.EventName);        
        //}
        //#endregion
    }
}
