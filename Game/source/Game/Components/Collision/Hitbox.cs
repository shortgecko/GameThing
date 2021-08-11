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

        public static bool Check(Point offset, Hitbox Hitbox, Hitbox other)
        { 
            Entity Entity = Hitbox.Entity;
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Hitbox.Intersects(box, other);
        }

    }

}