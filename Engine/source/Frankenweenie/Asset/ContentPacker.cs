using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO.Compression;
using System.IO.Compression;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Frankenweenie
{
    public class ContentPacker
    {
        internal struct ContentPack
        {
            public string Name;
            public List<string> Registry;

            //public static ContentPacker Create(string name, string directory)
            //{
            //    Directory.GetFiles(name);
            //    Logger.Log
            //}
        }


        public static void Pack(string directory, string output)
        {
            ContentPack contentPack;
            
            ZipFile.CreateFromDirectory(directory, output);
        }

        public static string W(string file, string get)
        {
            using (var zip = ZipFile.OpenRead(file))
            {
                ZipArchiveEntry e = zip.GetEntry(get);
                if (e == null)
                {
                    throw new Exception("File not found");
                }
                var stream = e.Open();
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                return text;
            }
        }
    }
}
