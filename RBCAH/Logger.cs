using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Styx.Common;

namespace RBCAH
{
    public static class Logger
    {
        static Color bodyColor = Color.FromRgb(249, 239, 109);
        static Color errorColor = Color.FromRgb(170, 0, 0);
        public static void Write(string format, params object[] args)
        {
            Logging.Write(bodyColor, format, args);
        }
        public static void WriteVerbose(string format, params object[] args)
        {
            Logging.WriteVerbose(bodyColor, format, args);
        }
        public static void WriteDiagnostic(string format, params object[] args)
        {
            Logging.WriteDiagnostic(bodyColor, format, args);
        }
        public static void WriteError(string format, params object[] args)
        {
            Logging.Write(errorColor, format, args);
        }
    }
}
