using System;
using System.IO;

namespace Ink_Canvas_Better.Helpers
{
    class Log
    {
        public static string LogFilePath = Path.Combine("Logs", "Log.txt");

        public static void NewLog(string str)
        {
            WriteLog(str, LogType.Info);
        }

        public static void WriteLogToFile(string str)
        {
            WriteLog(str, LogType.Info);
        }

        public static void WriteLogToFile(string str, LogType logType = LogType.Info)
        {
            WriteLog(str, logType);
        }

        private static void WriteLog(string str, LogType logType)
        {
            string strLogType = "Info";
            switch (logType)
            {
                case LogType.Event:
                    strLogType = "Event";
                    break;
                case LogType.Trace:
                    strLogType = "Trace";
                    break;
                case LogType.Error:
                    strLogType = "Error";
                    break;
            }
            try
            {
                var file = App.RootPath + LogFilePath;
                if (!Directory.Exists(App.RootPath))
                {
                    Directory.CreateDirectory(App.RootPath);
                }
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(string.Format("{0} [{1}] {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strLogType, str));
                sw.Close();
            }
            catch { }
        }


        public enum LogType
        {
            Info,
            Trace,
            Error,
            Event
        }
    }
}
