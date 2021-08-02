using Microsoft.Xna.Framework;
using Frankenweenie;
using System;
using System.Collections.Generic;

namespace Game
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

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
        public Action<Entity> OnCollideX;
        public Action<Entity> OnCollideY;

        List<Component> Hitboxes;
        List<Component> Triggers;

        public override void Initialize()
        {
            Hitboxes = World.All<Hitbox>();
            Triggers = World.All<Trigger>();
            Hitbox = Entity.Get<Hitbox>();
        }


        public override void Update()
        {
            CheckTriggers();
            CheckX();
            CheckY();
        }

        private bool Check(Point offset, Hitbox other) => Hitbox.Check(offset, Hitbox, other);
        private bool CheckActor(Point offset, Hitbox other)
        {
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
            other.X = (int)other.Entity.Position.X;
            other.Y = (int)other.Entity.Position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Hitbox.Intersects(box, other);
        }

        private bool CheckActor(Point offset, Rectangle other)
        {
            Hitbox.X = (int)Entity.Position.X;
            Hitbox.Y = (int)Entity.Position.Y;
            var box = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);
            return Hitbox.Intersects(box, other);
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


        public bool Collision(Point offset, Point moveAmount, Masks mask)
        {
            switch (mask)
            {
                case Masks.Solids:
                    return CheckSolids(offset, moveAmount);
                case Masks.Actors:
                    return CheckActors(offset);
                case Masks.All:
                    if (CheckSolids(offset, moveAmount))
                        return true;
                    if (CheckActors(offset))
                        return true;
                    break;

            }
                
            return false;
        }

         public int RoundUp(int input, int offset)
         {
            int r = input & offset;
            if (r == 0)
                return input;
            return input + (8 - r);
         }
        private bool CheckSolids(Point offset, Point amount)
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

            return false;
        }

        private void CheckX()
        {
            int sign = Math.Sign(Move.X);
            Move.X = (float)Math.Round(Move.X);
  
            while (Move.X != 0)
            {
                if (!Collision(new Point(sign, 0), Move.ToPoint(),Mask))
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    if (OnCollide != null)
                        OnCollide.Invoke();
                    if (OnCollideX != null)
                        OnCollideX.Invoke(Hitbox.Entity);
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
                if (!Collision(new Point(0, sign), Move.ToPoint(), Mask))
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
                        OnCollideY.Invoke(Hitbox.Entity);
                    break;
                }
            }
        }

        public void CheckTriggers()
        {
            foreach(var triggerRef in Triggers)
            {
                var trigger = (Trigger)triggerRef;

                if (Collider.Intersects(Hitbox, trigger))
                {
                    if (trigger.OnTriggerEnter != null)
                        trigger.OnTriggerEnter.Invoke(Entity);
                    Hitbox.TriggerOverlap = true;
                }
                else
                {

                    if(Hitbox.TriggerOverlap)
                    {
                        if (trigger.OnTriggerLeave != null)
                            trigger.OnTriggerLeave.Invoke(Entity);
                        Hitbox.TriggerOverlap = false;
                    }

                }
            }
        }

    }
}