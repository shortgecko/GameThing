using System;
using System.Collections.Generic;
using System.IO;

namespace Pinecorn
{
    public static class Logger
    {
        private static List<string> Logs = new List<string>();
        private static string Path = Asset.Path("log.txt");
        private static string Title;

        public static void Initialize()
        {
            Title = "LOG " + Engine.Config.AssetDirectory;
        }

        public static void Log(object log)
        {
            Logs.Add("[" + Title+ "]" + log.ToString());
        }

        public static void Save()
        {

            using(StreamWriter writer = new StreamWriter(Path))
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