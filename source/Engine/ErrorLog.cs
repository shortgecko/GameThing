using System;
using System.Collections.Generic;
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
            var realPath = Asset.Path(Path);
            if(File.Exists(realPath))
            {
                Lines = File.ReadAllLines(realPath);
            }
             using(StreamWriter writer = new StreamWriter(realPath))
            {
                writer.WriteLine("ERROR LOG");
                writer.WriteLine(Header);
                writer.WriteLine("ERROR " + DateTime.Now);
                writer.WriteLine(e);

                if(Lines != null)
                {
                    foreach(var line in Lines)
                    {
                        if(line != Header && line != "ERROR LOG")
                            writer.WriteLine(line);
                    }
                }

                writer.Close();
            }

        }
    }
}