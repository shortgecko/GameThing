using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Frankenweenie
{
    public static class Logger
    {
        private static List<string> Logs = new List<string>();
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
                Logs.Add(logLine);
            Console.WriteLine(logLine);
        }

        public static void Log() => Logger.Log("***", false);
        
        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.WriteLine(Title);
                writer.WriteLine(Header);
                foreach(String Log in Logs)
                {
                    writer.WriteLine(Log);
                }
                writer.Close();
            }
        }
    }
}