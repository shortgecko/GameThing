using Frankenweenie;
using Microsoft.Xna.Framework;
using System;

namespace  Frankenweenie
{
    public class Hitbox : Collider
    {
        public bool TriggerOverlap = false;

        public Hitbox(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override CollisionData Check(Point offset, Collider other)
        {
            Hitbox box = new Hitbox(X + offset.X, Y + offset.Y, Width, Height);
            
            if(box.Intersects(other))
            {
                return new CollisionData(true, other.Entity, offset);
            }

            return null;
        }

    }

}