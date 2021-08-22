using System;
using System.IO;

namespace Frankenweenie
{
    public static class ErrorLog
    {
        public static readonly string Path = $"{Engine.AssemblyDirectory}/{Path}/error_log.txt";
        public static readonly string Header = "--------------------------------------------------------------------------------";
        private static string[] Lines;
        public static void Log(Exception e)
        {
            if (File.Exists(Path))
                Lines = File.ReadAllLines(Path);

            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.WriteLine("ERROR LOG");
                writer.WriteLine(Header);
                writer.WriteLine("ERROR " + DateTime.Now);
                writer.WriteLine(e);

                if (Lines != null)
                {
                    foreach (string Line in Lines)
                    {
                        if (Line != Header && Line != "ERROR LOG")
                            writer.WriteLine(Line);
                    }
                }

                writer.Close();
            }

        }
    }
}