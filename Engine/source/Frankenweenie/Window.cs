using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public static class Window
    {
        public static int Width
        {
            get
            {
                return Engine.Instance.Window.ClientBounds.Width;
            }
        }
        public static int Height
        {
            get
            {
                return Engine.Instance.Window.ClientBounds.Height;
            }
        }
        public static Point Position
        {
            get
            {
                return Engine.Instance.Window.Position;
            }
        }
        public static string Title
        {
            get
            {
                return Engine.Instance.Window.Title;
            }
        }

        public static void Size(int width, int height)
        {
            Engine.Device.PreferredBackBufferWidth = width;
            Engine.Device.PreferredBackBufferHeight = height;
            Engine.Device.ApplyChanges();
        }
    }
}