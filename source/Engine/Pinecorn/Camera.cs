using Microsoft.Xna.Framework;

namespace Pinecorn
{
    public class Camera
    {
        public Rectangle Bounds;
        public Camera()
        {
            Bounds = new Rectangle(0,0,Engine.Config.Width,Engine.Config.Height);
        }

        public Camera(Rectangle bounds)
        {
            this.Bounds = bounds;
        }
        public Matrix Transform { get; private set; }

        public void Move(int x = 0, int y =0)
        {
            Bounds.X += x;
            Bounds.Y += y;
        }

        public void Follow(Entity entity)
        {                
            Bounds.X += (int)entity.position.X;
            Bounds.Y += (int)entity.position.Y;
        }

        public void Update()
        {
            Transform = Matrix.CreateTranslation(Bounds.X, Bounds.Y,0);
        }

    }
}