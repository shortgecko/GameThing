using Microsoft.Xna.Framework;
using Frankenweenie;
using System;

namespace Game
{
    public class Mover : Component
    {
        public Hitbox Hitbox;
        public Vector2 Move;
        public bool OnGround = false;


        public override void Update()
        {
            Hitbox = entity.get<Hitbox>();
            Hitbox.X = (int)entity.position.X;
            Hitbox.Y = (int)entity.position.Y;

            CheckX();
            CheckY();

        }

        private bool Check(Point offset, Hitbox other)
        {
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

        private bool collisionAt(Point offset)
        {
            foreach(Hitbox hitbox in Level.Solids)
            {
                if(Check(offset, hitbox))
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
            int move = (int)Move.X;

            if(move != 0)
            {
                if(!collisionAt(new Point(sign, 0)))
                {
                    entity.position.X += sign;
                    move -= sign;
                }
            }
        }

        private void CheckY()
        {
            int sign = Math.Sign(Move.Y);
            int move = (int)Move.Y;

            if (move != 0)
            {
                if(sign < 0)
                {
                    //need to check grounded as well
                    if (!collisionAt(new Point(0, sign)))
                    {
                        entity.position.Y += sign;
                        move -= sign;
                    }

                    if (collisionAt(new Point(0, 1)))
                        OnGround = true;
                    else
                        OnGround = false;
                }
                else
                {
                    if (!collisionAt(new Point(0, sign)))
                    {
                        entity.position.Y += sign;
                        move -= sign;
                        OnGround = false;
                    }
                    else
                        OnGround = true;
                }


            }
        }

    }
}

