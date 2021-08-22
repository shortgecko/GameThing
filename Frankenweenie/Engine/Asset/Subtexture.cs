using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Subtexture
    {
        private Texture2D Texture;
        private Rectangle Frame;

        public Subtexture(Texture2D texture, Rectangle frame)
        {
            Texture = texture;
            Frame = frame;
        }

        public Subtexture(Rectangle frame)
        {
            Frame = frame;
        }

    }
}
