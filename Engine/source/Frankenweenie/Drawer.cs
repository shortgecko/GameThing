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

        public static void Rect(Rectangle rect, Color c)
        {
            Drawer.Batch.Draw(EmptyTexture, rect, c);
        }

        public static void Rect(Rectangle rect)
        {
            Color c = Color.White;
            Drawer.Batch.Draw(EmptyTexture, rect, c);
        }

        public static void HollowRectangle(Rectangle input, int t, Color c)
        {
            hollowRect.X = input.X;
            hollowRect.Y = input.Y;
            hollowRect.Width = input.Width;
            hollowRect.Height = t;
            Rect(hollowRect, c);

            hollowRect.Y += input.Height - t;
            Rect(hollowRect, c);

            hollowRect.Y = input.Y;
            hollowRect.Width = t;
            hollowRect.Height = input.Height;
            Rect(hollowRect, c);

            hollowRect.X += input.Width - t;
            Rect(hollowRect, c);
        }

        public static void String(SpriteFont font, string text, Vector2 positon, Color color)
        {
            Batch.DrawString(font, text, positon, color);
        }


    }
}
