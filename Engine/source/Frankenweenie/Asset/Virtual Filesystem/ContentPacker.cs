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
        private static Dictionary<string, Stream> Streams;
        
        public static void Pack(string directory)
        {
            string[] allfiles = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            for(int i = 0; i < allfiles.Length; i++)
                allfiles[i] = Path.GetRelativePath(directory, allfiles[i]);
            foreach(var f in allfiles)
                Console.WriteLine(f);
            string filename = directory + ".pak";
            if(File.Exists(filename))
                File.Delete(filename);
            ZipFile.CreateFromDirectory(directory, filename);
            Serializer.Serialize<string[]>(allfiles, directory + ".areg");
        }
        
        public static void Unpack(string directory)
        {
            string pak  = directory + ".pak";
            string reg = directory + ".areg";
            
            Streams = Get(Serializer.Deserialize<string[]>(reg), pak);
        }

        private static Stream GetStream(string file)
        {
            if(!Streams.ContainsKey(file))
                throw new Exception($"File{file}  not found");
            return Streams[file];
        }

        private static Dictionary<string,Stream>  Get(string[] files, string file)
        {
            Dictionary<string, Stream> streams = new Dictionary<string, Stream>();
            using (var zip = ZipFile.OpenRead(file))
            {
                foreach(var entry in files)
                {
                    ZipArchiveEntry e = zip.GetEntry(entry);
                    if (e == null)
                    {
                        throw new Exception("File not found");
                    }
                    var stream = e.Open();
                    streams.Add(entry, stream);
                }
            }

            return streams;
        }
    }
}
