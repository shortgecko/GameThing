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
            Hitbox = entity.get<Hitbox>();
            Hitbox.X = (int)entity.position.X;
            Hitbox.Y = (int)entity.position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Intersects(box, other);
        }

        private bool Intersects(Hitbox hitbox,Hitbox other)
        {            
            return other.Left < hitbox.Right &&
                hitbox.Left < other.Right &&
                other.Top < hitbox.Bottom &&
                hitbox.Top < other.Bottom;
        }

        public bool collision(Point offset, Masks mask)
        {
            switch(mask)
            {
                case Masks.Solids:
                    if(Level.Solids.Count > 1)
                    {
                        foreach (Hitbox hitbox in Level.Solids)
                        {
                            if (Check(offset, hitbox))
                            {
                                if (hitbox != this.Hitbox)
                                    return true;
                            }
                        }
                    }
                    break;
                case Masks.Actors:
                    if (Level.Actors.Count > 1)
                    {
                        foreach (Hitbox hitbox in Level.Actors)
                        {
                            if (Check(offset, hitbox))
                            {
                                if (hitbox != this.Hitbox)
                                    return true;
                            }
                        }
                    }
                    break;
                case Masks.All:
                    if(Level.Solids.Count > 1)
                    {
                        foreach (Hitbox hitbox in Level.Solids)
                        {
                            if (Check(offset, hitbox))
                            {
                                if (hitbox != this.Hitbox)
                                    return true;
                            }
                        }
                    }
                    if(Level.Actors.Count > 1)
                    {
                        foreach (Hitbox hitbox in Level.Actors)
                        {
                            if (Check(offset, hitbox))
                            {
                                if (hitbox != this.Hitbox)
                                    return true;
                            }
                        }
                    }
                break;

            }

            return false;
        }

        private void CheckX()
        {
            int sign = Math.Sign(Move.X);
            Move.X = (float)Math.Round(Move.X);

            while (Move.X != 0)
            {
                if (!collision(new Point(sign, 0),Masks.All))
                {
                    entity.position.X += sign;
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

            while(Move.Y != 0)
            {
                if (!collision(new Point(0, sign), Masks.All))
                {
                    entity.position.Y += sign;
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

