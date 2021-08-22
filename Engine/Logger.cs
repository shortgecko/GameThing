using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ImGuiNET;



namespace Frankenweenie
{
    public class Logger : ImGuiElement
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
            Logs.Append(Title);            Logs.Append(Header);

        }

        public override void Draw()
        {
            ////ImGui.SetNextWindowPos(new System.Numerics.Vector2(0, Window.Height - 500), 0);
            ////ImGui.SetNextWindowSize(new System.Numerics.Vector2(500, 500));
            //ImGui.Begin("Logs");
            ////ImGui.Text(Logs.ToString());
            //ImGui.End();
        }

        public static void Log(object log, bool addToFile = true, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            String logLine = $"[{Title}] {log.ToString()}";
            if (addToFile)
                Logs.Append(logLine + "\n");
            Console.WriteLine(logLine);
            //Console.ForegroundColor = ConsoleColor.White;
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