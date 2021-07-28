using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class Drawer
    {
        public static SpriteBatch Batch { get; set; }
        private static Rectangle hollowRect;
        private static Texture2D EmptyTexture;

        public Drawer()
        {
            EmptyTexture = Content.Empty;
        }

        public static void Rect(Rectangle rect, Color c, float LayerDepth)
        {
            Drawer.Batch.Draw(EmptyTexture, rect, c, 0f, Vector2.Zero, SpriteEffects.None, LayerDepth);
        }

        public static void Rect(Rectangle rect)
        {
            Color c = Color.White;
            Drawer.Batch.Draw(EmptyTexture, rect, c);
        }

        public static void HollowRectangle(Rectangle input, int t, float depth, Color c)
        {
            hollowRect.X = input.X;
            hollowRect.Y = input.Y;
            hollowRect.Width = input.Width;
            hollowRect.Height = t;
            Rect(hollowRect, c, depth);

            hollowRect.Y += input.Height - t;
            Rect(hollowRect, c,depth);

            hollowRect.Y = input.Y;
            hollowRect.Width = t;
            hollowRect.Height = input.Height;
            Rect(hollowRect, c,depth);

            hollowRect.X += input.Width - t;
            Rect(hollowRect, c,depth);
        }

        public static void String(SpriteFont font, string text, Vector2 positon, Color color)
        {
            Batch.DrawString(font, text, positon, color);
        }


    }
}
