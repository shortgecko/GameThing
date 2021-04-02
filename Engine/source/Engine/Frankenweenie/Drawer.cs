using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class Drawer
    {
        public static SpriteBatch Batch;
        private static Rectangle HollowRect;
        private static Texture2D EmptyTexture;

        public Drawer()
        {
            EmptyTexture = Asset.Empty();
        }

        public static void Rect(Rectangle rect, Color c)
        {
            Drawer.Batch.Draw(EmptyTexture, rect, c);
        }
        public static void HollowRectangle(int x, int y, int w, int h, int t, Color c)
        {
            HollowRect.X = x;
            HollowRect.Y = y;
            HollowRect.Width = w;
            HollowRect.Height = t;
            Rect(HollowRect, c);

            HollowRect.Y += h - t;
            Rect(HollowRect, c);

            HollowRect.Y = y;
            HollowRect.Width = t;
            HollowRect.Height = h;
            Rect(HollowRect, c);

            HollowRect.X += w - t;
            Rect(HollowRect, c);
        }
    }
}
