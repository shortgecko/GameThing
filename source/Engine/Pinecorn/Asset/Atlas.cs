using System.Xml;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Pinecorn
{
    public class Atlas
    {
        private XmlDocument AtlasDef { get; set; }
        public Texture2D Source { get; set; }

        private Dictionary<string, Subtexture> Subtextures = new Dictionary<string, Subtexture>();

        public Atlas(string atlasDef)
        {
            AtlasDef = Asset.LoadXml(atlasDef);
            Load();
        }

        public Atlas(XmlDocument atlasDef)
        {
            AtlasDef = atlasDef;
            Load();
        }

        private void Load()
        {
            XmlElement TextureAtlas = AtlasDef["TextureAtlas"];
            var filepath = TextureAtlas.Attributes["imagePath"].Value;
            Source = Asset.Texture("Graphics/" + filepath);
            var subtextures = TextureAtlas.GetElementsByTagName("sprite");

            foreach(XmlElement subtexture in subtextures)
            {
                var name = subtexture.Attributes["n"].Value;
                var x = subtexture.Attributes["x"].Value;
                var y = subtexture.Attributes["y"].Value;
                var w = subtexture.Attributes["w"].Value;
                var h = subtexture.Attributes["h"].Value;
                Subtextures.Add(name,new Subtexture(new Rectangle(int.Parse(x), int.Parse(y), int.Parse(w), int.Parse(h))));
               
            }
            
        }

        public void Debug()
        {
            System.Console.WriteLine(Subtextures.Count);
        }
    }
}
