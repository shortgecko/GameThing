using System;
using System.Collections.Generic;
using System.IO;

namespace Frankenweenie
{
    public static class Logger
    {
        private static List<string> Logs = new List<string>();
        private static string Path = Asset.Path("log.txt");
        private static string Title;

        public static void Initialize()
        {
            Title = "LOG " + Engine.Config.WindowTitle.ToUpper();
            Console.WriteLine(Title);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        }

        public static void Log(object log)
        {
            var logLine = "[" + Title + "]" + log.ToString();
            Logs.Add(logLine);
            Console.WriteLine(logLine);
        }

        public static void Log()
        {
            var log = "***";
            var logLine = "[" + Title + "]" + log.ToString();
            Logs.Add(logLine);
            Console.WriteLine(logLine);
        }

        public static void Save()
        {

            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.WriteLine(Title);
                writer.WriteLine("-------------------------------------------------------------------------------------------------");
                foreach (var log in Logs)
                {
                    writer.WriteLine(log);
                }
                writer.Close();
            }
        }
    }
}