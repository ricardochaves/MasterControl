using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RBCAH
{
    static class Resources
    {
        private static string hbFolder
        {
            get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); }
        }
        private static string resourcesFolder
        {
            get { return Path.Combine(hbFolder, "Bots", "Salesman", "Resources"); }
        }
        private static string _logoFile { get { return Path.Combine(resourcesFolder, "bothaven.png"); } }
        private static Image _logo;
        public static Image Logo
        {
            get { return _logo ?? (_logo = Bitmap.FromFile(_logoFile)); }
        }
        private static string _backgroundFile { get { return Path.Combine(resourcesFolder, "classy_fabric.png"); } }
        private static Image _background;
        public static Image BackgroundTile
        {
            get { return _background ?? (_background = Bitmap.FromFile(_backgroundFile)); }
        }
        public static Color HeaderColor
        {
            get { return Color.FromArgb(0, 95, 171); }
        }
        public static Color BodyColor
        {
            get { return Color.FromArgb(0, 157, 220); }
        }
        public static Color PrimaryBackColor
        {
            get { return Color.FromArgb(30, 30, 30); }
        }
        public static Color SecondaryBackColor
        {
            get { return Color.FromArgb(45, 45, 45); }
        }
        private static string _blankButtonFile { get { return Path.Combine(resourcesFolder, "blank-button.png"); } }
        private static Image _blankButton;
        public static Image BlankButton
        {
            get { return _blankButton ?? (_blankButton = Bitmap.FromFile(_blankButtonFile)); }
        }
        private static string _blankButtonClickedFile { get { return Path.Combine(resourcesFolder, "blank-button-clicked.png"); } }
        private static Image _blankButtonClicked;
        public static Image BlankButtonClicked
        {
            get { return _blankButtonClicked ?? (_blankButtonClicked = Bitmap.FromFile(_blankButtonClickedFile)); }
        }
        private static string _blankButtonMouseoverFile { get { return Path.Combine(resourcesFolder, "blank-button-mouseover.png"); } }
        private static Image _blankButtonMouseover;
        public static Image BlankButtonMouseover
        {
            get { return _blankButtonMouseover ?? (_blankButtonMouseover = Bitmap.FromFile(_blankButtonMouseoverFile)); }
        }
        private static string _programIconFile { get { return Path.Combine(resourcesFolder, "icon.ico"); } }
        private static Icon _programIcon;
        public static Icon ProgramIcon
        {
            get { return _programIcon ?? (_programIcon = Icon.ExtractAssociatedIcon(_programIconFile)); }
        }
        private static string _umjFile { get { return Path.Combine(resourcesFolder, "umj.xml"); } }
        private static string _umj;
        public static string DefaultUMJValues
        {
            get { return _umj ?? (_umj = File.ReadAllText(_umjFile)); }
        }
        private static Font _normalTextFont;
        public static Font NormalTextFont
        {
            get { return _normalTextFont ?? (_normalTextFont = new Font("Tahoma", 10.0f)); }
        }

        private static Font _headerFont;
        public static Font HeaderFont
        {
            get { return _headerFont ?? (_headerFont = new Font("Tahoma", 15.0f)); }
        }
    }
}
