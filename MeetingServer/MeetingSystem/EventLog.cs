using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MeetingSystem
{
    public static class EventLog
    {
        public static string FilePath { get; set; }
        private static object obj=new object();
        public static void Write(string format, params object[] arg)
        {
            Write(string.Format(format, arg));
        }

        public static void Write(string message) {
            lock (obj)
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = Directory.GetCurrentDirectory();
                }
                string filename = FilePath +
                    string.Format("\\{0:yyyy}\\{0:MM}\\{0:yyyy-MM-dd}.txt", DateTime.Now);
                FileInfo finfo = new FileInfo(filename);
                if (finfo.Directory.Exists == false)
                {
                    finfo.Directory.Create();
                }
                string writeString = string.Format("{0:yyyy/MM/dd HH:mm:ss} {1}",
                    DateTime.Now, message) + Environment.NewLine;
                File.AppendAllText(filename, writeString, Encoding.Unicode);
            }
      }


    }
}
