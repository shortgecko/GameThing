using System;
using System.Collections.Generic;
using System.IO;
using System.Text;



namespace Frankenweenie
{
    public static class Logger
    {
        private static StringBuilder Logs = new StringBuilder();
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

        public static void Log(object log, bool addToFile = true, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            String logLine = $"[{Title}] {log.ToString()}";
            if (addToFile)
                Logs.Append(logLine + "\n");
            Console.WriteLine(logLine);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(object log, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            String logLine = $"[{Title}] {log.ToString()}";
            Console.WriteLine(logLine);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log() => Logger.Log("**", false);
        
        public static void Save()
        {
            using (StreamWriter Writer = new StreamWriter(Path))
            {
                Writer.WriteLine(Title);
                Writer.WriteLine(Header);
                Writer.Write(Logs.ToString());
                Writer.Close();
            }
        }

    }
}