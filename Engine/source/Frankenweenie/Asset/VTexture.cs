using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Frankenweenie
{
    public class VTexture : IDisposable
    {
        [NonSerialized]
        public Texture2D Texture;
        public string TextureName;

        public int Width => Texture.Width;
        public int Height => Texture.Height;

        public VTexture(Texture2D texture)
        {
            Texture = texture;
            TextureName = texture.Name;
        }

        public void Dispose()
        {
            Texture.Dispose();
        }

    }

}
