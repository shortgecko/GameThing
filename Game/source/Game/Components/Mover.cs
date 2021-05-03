using Microsoft.Xna.Framework;
using Frankenweenie;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Mover : Component
    {
        public enum Masks
        {
            Solids,
            Actors,
            All
        };

        public Hitbox Hitbox;
        public Vector2 Move;
        public bool OnGround = false;
        public Masks Mask = Masks.All;
        public Action OnCollide;
        public Action<int> OnCollideX;
        public Action<int> OnCollideY;

        List<Component> Hitboxes;

        public override void Initialize()
        {
            Hitboxes = World.All<Hitbox>();
            Hitbox = Entity.Get<Hitbox>();
        }

        public override void Update()
        {

            CheckX();
            CheckY();

        }

        private bool Check(Point offset, Hitbox other)
        { 
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Intersects(box, other);
        }
        private bool CheckActor(Point offset, Hitbox other)
        {
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
            other.X = (int)other.Entity.Position.X;
            other.Y = (int)other.Entity.Position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Intersects(box, other);
        }

        private bool Intersects(Hitbox hitbox, Hitbox other)
        {
            return other.Left < hitbox.Right &&
                hitbox.Left < other.Right &&
                other.Top < hitbox.Bottom &&
                hitbox.Top < other.Bottom;
        }

        public bool Collision(Point offset, Masks mask)
        {
            switch (mask)
            {
                case Masks.Solids:
                    return CheckSolids(offset);
                case Masks.Actors:
                    return CheckActors(offset);
                case Masks.All:
                    if (CheckSolids(offset))
                        return true;
                    if (CheckActors(offset))
                        return true;
                    break;

            }

            return false;
        }

        private bool CheckSolids(Point offset)
        {

            {
                
                for (int i = 0; i < Level.Solids.Count; i++)
                {
                    var hitbox = Level.Solids[i];
                    if (Check(offset, hitbox))
                    {
                        if (hitbox != this.Hitbox)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool CheckActors(Point offset)
        {
            for (int i = 0; i < Hitboxes.Count; i++)
            {
                var hitbox = (Hitbox)Hitboxes[i];
                if (CheckActor(offset, hitbox))
                {
                    if (hitbox != this.Hitbox)
                        return true;
                }
            }

            return false;
        }

        private void CheckX()
        {
            int sign = Math.Sign(Move.X);
            Move.X = (float)Math.Round(Move.X);

            while (Move.X != 0)
            {
                if (!Collision(new Point(sign, 0), Mask))
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    if (OnCollide != null)
                        OnCollide.Invoke();
                    if (OnCollideX != null)
                        OnCollideX.Invoke(sign);
                    break;
                }
            }
        }

        private void CheckY()
        {
            int sign = Math.Sign(Math.Round(Move.Y));
            Move.Y = (float)Math.Round(Move.Y);
            while (Move.Y != 0)
            {
                if (!Collision(new Point(0, sign), Mask))
                {
                    Entity.Position.Y += sign * Engine.Delta;
                    Move.Y -= sign;
                }
                else
                {
                    Move.Y = 0;
                    if (OnCollide != null)
                        OnCollide.Invoke();
                    if (OnCollideY != null)
                        OnCollideY.Invoke(sign);
                    break;
                }
            }
        }

    }
}
