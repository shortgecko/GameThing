using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml;

namespace Frankenweenie
{
    public class Atlas
    {
        private XmlDocument AtlasDef { get; set; }
        public VTexture Source { get; set; }

        private Dictionary<string, Subtexture> Subtextures = new Dictionary<string, Subtexture>();

        public Atlas(string atlasDef)
        {
            AtlasDef = Content.LoadXml(atlasDef);
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
            Source = Content.LoadTexture("Graphics/" + filepath);
            var subtextures = TextureAtlas.GetElementsByTagName("sprite");

            foreach (XmlElement subtexture in subtextures)
            {
                string name = subtexture.Attributes["n"].Value;
                int x = int.Parse(subtexture.Attributes["x"].Value);
                int y = int.Parse(subtexture.Attributes["y"].Value);
                int w = int.Parse(subtexture.Attributes["w"].Value);
                int h = int.Parse(subtexture.Attributes["h"].Value);
                Subtextures.Add(name, new Subtexture(new Rectangle(x, w, w, h)));
            }

        }
    }
}
