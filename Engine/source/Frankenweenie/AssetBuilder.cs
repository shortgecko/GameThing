using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Frankenweenie
{
    public class AssetBuilder
    {
        public static void Build(string contentDirectory, string filename)
        {
            ZipFile.CreateFromDirectory(contentDirectory, filename + ".pak");
            var zip = ZipFile.Open(contentDirectory, ZipArchiveMode.Read);
        }



     
    }
}

