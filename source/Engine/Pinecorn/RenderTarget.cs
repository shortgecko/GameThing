using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinecorn
{
    public class RenderTarget
    {
        public Vector2 Position;
        public int Width;
        public int Height;
        public float Scale;
        public RenderTarget2D Target;

        public RenderTarget(Vector2 position, int width, int height, float scale)
        {
           Position = position; Width = width; Height = height; Scale = scale;
           Target = new RenderTarget2D(Engine.Device.GraphicsDevice, width, height);
        }

    }
}