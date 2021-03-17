using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinecorn
{
    public class Config
    {
        public string WindowTitle{ get; set; }
        public string AssetDirectory { get; set; }
        public bool FixedTimeStep { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsMouseVisible {get;set;}

        public Config(string windowTitle, string assetDirectory, bool fixedTimestep, int width, int height, bool isMouseVisible=false)
        {
            WindowTitle = windowTitle;
            AssetDirectory = assetDirectory;
            FixedTimeStep = fixedTimestep;
            Width = width;
            Height = height;
            IsMouseVisible = isMouseVisible;
        }
        public Config()
        {

        }
    }
}
