using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Frankenweenie
{
    public static class Logger
    {
        private static StringBuilder StringBuilder = new StringBuilder();
        private static string Path = $"{Engine.AssemblyDirectory}/Log.txt";
        private static string Title;

        #region Header
        private const string Header = "------------------------------------------------------------------------------------------------------------------";
        #endregion

        public static void Initialize()
        {      
            Title = $"Log {Window.Title}";
            Console.WriteLine(Title);
            Console.WriteLine(Header);
        }

        public static void Log(object log, bool addToFile = true)
        {
            string logLine = $"[{Title}] {log.ToString()}";
            if (addToFile)
                StringBuilder.Append(logLine + "\n");
            Console.WriteLine(logLine);
        }

        public static void Log() => Logger.Log("***", false);
        
        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.WriteLine(Title);
                writer.WriteLine(Header);
                string logs = StringBuilder.ToString();
                writer.WriteLine(logs);
                writer.Close();
            }
        }
    }
}