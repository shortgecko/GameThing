using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Frankenweenie
{
    public static class Window
    {
        public static int Width => Engine.Instance.Window.ClientBounds.Width;
        public static int Height => Engine.Instance.Window.ClientBounds.Height;
        public static Point Position => Engine.Instance.Window.Position;
        public static string Title => Engine.Instance.Window.Title;
        public static List<Action> ResizeActions = new List<Action>();

        public static void SetSize(int width, int height)
        {
            Engine.Device.PreferredBackBufferWidth = width;
            Engine.Device.PreferredBackBufferHeight = height;
            Engine.Device.ApplyChanges();
        }

        public static void SetPosition(Point position)
        {
            Engine.Instance.Window.Position = position;
        }
    }
}