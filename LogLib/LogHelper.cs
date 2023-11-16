using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
    public static class LogHelper
    {
        static log4net.ILog Log;
        static bool CanLog = false;

        static LogHelper()
        {
            try
            {
                Log = log4net.LogManager.GetLogger("RunLogger");
                CanLog = true;
            }
            catch
            {
                CanLog = false;
            }
        }

        public static void Debug(string message, bool debug = true)
        {
            if (CanLog)
            {
                Log.Debug(message);
            }

            if (debug)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        public static void Error(string message, bool debug = true)
        {
            if (CanLog)
            {
                Log.Error(message);
            }

            if (debug)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        public static void Info(string message, bool debug = true)
        {
            if (CanLog)
            {
                Log.Info(message);
            }

            if (debug)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        public static void Warn(string message, bool debug = true)
        {
            if (CanLog)
            {
                Log.Warn(message);
            }

            if (debug)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }
    }
}
