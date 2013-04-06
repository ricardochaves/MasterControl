using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Styx.Helpers;
using Styx.Common;
using Styx;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;



namespace MasterControl
{
    public class MasterControlSettings : Settings
    {

        private static MasterControlSettings _instance;

        public MasterControlSettings()
            : base(Path.Combine(Path.Combine(Styx.Helpers.GlobalSettings.SettingsDirectory, "Settings"), string.Format("MasterControlSettings_{0}.xml", StyxWoW.Me.Name)))
        {

        }

        public static MasterControlSettings Instance { get { return _instance ?? (_instance = new MasterControlSettings()); } }


        [Setting]
        [Category("Padrão")]
        [DisplayName("API Key")]
        public string apikey { get; set; }

        [Setting]
        [Category("Padrão")]
        [DefaultValue("")]
        [Description("Url to your own hosting script should be like http://www.buddyc.eu/index.php?r=api")]
        [DisplayName("Own Hosting URL")]
        public string ownurl { get; set; }

        [Setting]
        [Category("General")]
        [DisplayName("Upload Script")]
        [DefaultValue("")]
        [Description("Url to your upload script on your webhost. If not set the defualt Image hoster imgur will be used")]
        public string scripturl { get; set; }

        [Setting]
        [Category("General")]
        [DefaultValue("")]
        [DisplayName("Prowl Api Key")]
        public string prowlapi { get; set; }

        [Setting]
        [Category("General")]
        [DefaultValue("")]
        [DisplayName("Android Device Key")]
        public string androidapi { get; set; }

        [Setting]
        [Category("Debug")]
        [DefaultValue(true)]
        [DisplayName("Perfomance Debug")]
        [Description("If you get freezes or lags you can turn this on. It will show more messages if lags or freezes occour everytime X happens give this infomation to the author")]
        public Boolean pdebug { get; set; }

        [Setting]
        [Category("Debug")]
        [DefaultValue(true)]
        [DisplayName("Debug")]
        [Description("NEVER USE THIS! ONLY FOR THE DEV!")]
        public Boolean debug { get; set; }

        #region Category: Notifications

        [Setting]
        [Category("Screenshot On")]
        [DefaultValue(false)]
        [DisplayName("Chat")]
        public Boolean scChat { get; set; }

        [Setting]
        [Category("Screenshot On")]
        [DefaultValue(false)]
        [DisplayName("Died")]
        public Boolean scDied { get; set; }

        [Setting]
        [Category("Screenshot On")]
        [DefaultValue(false)]
        [DisplayName("Level up")]
        public Boolean scLevel { get; set; }

        [Setting]
        [Category("Notificate On Mobile")]
        [DefaultValue(false)]
        [DisplayName("Level up")]
        public Boolean notfLevel { get; set; }

        [Setting]
        [Category("Notificate On Mobile")]
        [DefaultValue(false)]
        [DisplayName("Died")]
        public Boolean notfDied { get; set; }

        [Setting]
        [Category("Notificate On Mobile(Chat)")]
        [DefaultValue(true)]
        [DisplayName("Say")]
        public Boolean notfSay { get; set; }

        [Setting]
        [Category("Notificate On Mobile(Chat)")]
        [DefaultValue(true)]
        [DisplayName("Whisper")]
        public Boolean notfWhisper { get; set; }

        [Setting]
        [Category("Notificate On Mobile(Chat)")]
        [DefaultValue(true)]
        [DisplayName("BG")]
        public Boolean notfBG { get; set; }

        [Setting]
        [Category("Notificate On Mobile(Chat)")]
        [DefaultValue(true)]
        [DisplayName("Guild")]
        public Boolean notfGuild { get; set; }

        [Setting]
        [Category("Notificate On Mobile(Chat)")]
        [DefaultValue(true)]
        [DisplayName("Raid")]
        public Boolean notfRaid { get; set; }

        [Setting]
        [Category("Notificate On Mobile")]
        [DefaultValue(true)]
        [DisplayName("Start")]
        public Boolean notfStart { get; set; }

        [Setting]
        [Category("Notificate On Mobile")]
        [DefaultValue(true)]
        [DisplayName("Stop")]
        public Boolean notfStop { get; set; }
        #endregion


    }
}
