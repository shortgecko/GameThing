using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class VirtualRenderTarget
    {
        public int Width
        {
            get
            {
                return Target.Width;
            }
        }
        public int Height
        {
            get
            {
                return Target.Height;
            }
        }

        public RenderTarget2D Target;
        public Vector2 Position = Vector2.Zero;
        public Rectangle SourceRectangle;
        public Color Color = Color.White;
        public float Rotation = 0f;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = new Vector2(1, 1);
        public SpriteEffects Effects = SpriteEffects.None;
        public float LayerDepth = 0f;

        public VirtualRenderTarget()
        {
            SourceRectangle = new Rectangle(0, 0, Engine.Config.Width, Engine.Config.Height);
            Target = new RenderTarget2D(Engine.Device.GraphicsDevice, Engine.Config.Width, Engine.Config.Height);
        }

        public VirtualRenderTarget(Vector2 position, int width, int height, float scale)
        {
            Position = position;
            Scale.X = scale;
            Target = new RenderTarget2D(Engine.Device.GraphicsDevice, width, height);

        }

        public VirtualRenderTarget(Vector2 position, int width, int height, Vector2 scale)
        {
            Position = position;
            Scale = scale;
            Target = new RenderTarget2D(Engine.Device.GraphicsDevice, width, height);

        }

        public void Set(int width, int height)
        {
            Target = new RenderTarget2D(Engine.Device.GraphicsDevice, width, height);
        }

        public void Render()
        {
            Drawer.Batch.Draw(Target, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

    }
}