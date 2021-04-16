using Microsoft.Xna.Framework;
using Frankenweenie;
using System;

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


        public override void Update()
        {
            CheckX();
            CheckY();

        }

        private bool Check(Point offset, Hitbox other)
        {
            Hitbox = Entity.Get<Hitbox>();
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
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

        public bool collision(Point offset, Masks mask)
        {
            switch (mask)
            {
                case Masks.Solids:
                    if (CheckSolids(offset))
                        return true;
                    break;
                case Masks.Actors:
                    if (CheckActors(offset))
                        return true;
                    break;
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
            if (Level.Solids.Count > 1)
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
            if (Level.Actors.Count > 1)
            {
                for (int i = 0; i < Level.Actors.Count; i++)
                {
                    var hitbox = Level.Actors[i];
                    if (hitbox != null)
                    {
                        if (Check(offset, hitbox))
                        {
                            if (hitbox != this.Hitbox)
                                return true;
                        }
                    }
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
                if (!collision(new Point(sign, 0), Masks.All))
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    Move.X = 0;
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
                //Logger.Log(Move.Y);
                //Logger.Log($"sign {sign}");
                if (!collision(new Point(0, sign), Masks.All))
                {
                    Entity.Position.Y += sign * Engine.Delta;
                    Move.Y -= sign;
                }
                else
                {
                    Move.Y = 0;
                    break;
                }
            }
        }

    }
}
