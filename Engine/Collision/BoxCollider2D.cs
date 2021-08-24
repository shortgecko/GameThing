using Frankenweenie;
using Microsoft.Xna.Framework;
using System;

namespace  Frankenweenie
{
    public class BoxCollider2D : Collider2D
    {
        public bool TriggerOverlap = false;

        public BoxCollider2D(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override CollisionData Check(Point offset, Collider2D other)
        {
            BoxCollider2D box = new BoxCollider2D(X + offset.X, Y + offset.Y, Width, Height);
            if(box.Intersects(other))
            {
                return new CollisionData(true, other.Entity, offset);
            }

            return null;
        }

    }

}