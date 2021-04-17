using System;
using System.IO;

namespace Frankenweenie
{
    public static class ErrorLog
    {
        public static readonly string Path = "ERROR_LOG.txt";
        public static readonly string Header = "--------------------------------------------------------------------------------";
        private static string[] Lines;
        public static void Log(Exception e)
        {
            var realPath = $"{Engine.Directory}/{Path}";
            if (File.Exists(realPath))
            {
                Lines = File.ReadAllLines(realPath);
            }
            using (StreamWriter writer = new StreamWriter(realPath))
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