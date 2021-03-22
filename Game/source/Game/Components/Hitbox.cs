using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Game
{
    public class Hitbox : Component
    {
        private float PX;
        private float PY;

        private int Width;
        private int Height;

        public Rectangle Bounds;

        public Hitbox(float posX, float posY, int width, int height)
        {
            PX = posX;
            PY = posY;

            Width = width;
            Height = height;

            Bounds = new Rectangle((int)posX, (int)posY, width,height);

        }


        public override void Update()
        {
            Bounds = new Rectangle((int)entity.position.X + (int)PX, (int)entity.position.Y + (int)PY, Width, Height);
        }


    }

}
