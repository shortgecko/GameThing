using Microsoft.Xna.Framework;
using Frankenweenie;
using System;

namespace Game
{
    public class Mover : Component
    {
        public Hitbox hitbox;

        public float moveX = 0f;
        public float moveY = 0f;
        public bool onGround = false;
        public Level Level;

        public override void Initialize()
        {
            Level = (Level)Engine.Scene;
        }

        public override void Update()
        {
            hitbox = entity.get<Hitbox>();

            foreach (var solid in Level.Solids)
            {
                if (collideAt(solid, new Point((int)moveX, 0)))
                {

                }
            }

            
            onGround = true;
            entity.position.X += moveX;
            //entity.position.Y += moveY;

            moveY = 0f;
            moveY = 0f;
        }

        public bool collideAt(Hitbox solid, Point offset)
        {
            return new Rectangle((int)entity.position.X + offset.X, (int)entity.position.Y + offset.Y, hitbox.Bounds.Width, hitbox.Bounds.Height).Intersects(solid.Bounds);
        }

        public void checkX(Hitbox solid)
        {
            int sign = Math.Sign(moveX);
        }

    }
}

