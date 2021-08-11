using Microsoft.Xna.Framework;
using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;


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
        public Masks Mask = Masks.All;
        public Action<Entity> OnCollide;
        public Action<Entity> OnCollideX;
        public Action<Entity> OnCollideY;
        private bool m_Grounded = false;
        public bool Grounded => m_Grounded;

        private List<Component> Hitboxes;
        private List<Component> Triggers;

        public override void Initialize()
        {
            Hitboxes = World.All<Hitbox>();
            Triggers = World.All<Trigger>();
            Hitbox = Entity.Get<Hitbox>();
        }

        bool j = false;


        public override void Update()
        {
            Hitbox.X = (int)Math.Floor(Entity.Position.X);
            Hitbox.Y = (int)Math.Ceiling(Entity.Position.Y);
            CheckTriggers();
            CheckX();
            CheckY();

            if(Keyboard.GetState().IsKeyDown(Keys.J))
            {
                j = true;
            }
        }




        private bool Check(Point offset, Hitbox other) => Hitbox.Check(offset, Hitbox, other);
        private bool CheckActor(Point offset, Hitbox other)
        {
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

        public bool Collision(Point offset)
        {
            return Collision(offset, Mover.Masks.All);
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

        private int Thing(double  thing)
        {

            if(thing % 1 == 0)
            {
                if(j)
                {
                    Logger.Log("whole" + thing);
                }
                return (int)thing;
            }
            else
            {
                return (int)thing + 1;
            }
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

        private bool _CheckSolids(Point offset)
        {
            Hitbox hitbox = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);

            int left = (int)Thing(Math.Clamp(Math.Floor((double)hitbox.X / Level.Tiles.CellSize.X), 0, Level.Tiles.Width));
            int right = (int)Thing(Math.Clamp(Math.Ceiling((double)hitbox.Right / Level.Tiles.CellSize.X), 0, Level.Tiles.Width));
            int top = (int)Thing(Math.Clamp(Math.Floor((double)hitbox.Y / Level.Tiles.CellSize.Y), 0, Level.Tiles.Height));
            int bottom = (int)Thing(Math.Clamp(Math.Ceiling((double)hitbox.Bottom / Level.Tiles.CellSize.Y), 0, Level.Tiles.Height));

            if(j)
            {
                Logger.Log(Entity.Position.X);
                Logger.Log(left);
            }

            for (int x = left; x < right; x++)
                for (int y = top; y < bottom; y++)
                {
                    if (Level.Tiles[x, y] != -1)
                        return true;
                    
                }
            return false;
        }

        private void CheckX()
        {
            int sign = Math.Sign(Move.X);
            Move.X = (float)Math.Round(Move.X);
  
            while (Move.X != 0)
            {
                if (!Collision(new Point(sign, 0),Mask))
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    Move.X = 0;
                    if (OnCollide != null)
                        OnCollide.Invoke(Hitbox.Entity);
                    if (OnCollideX != null)
                        OnCollideX.Invoke(Hitbox.Entity);
                    break;
                }
            }
        }

        private void CheckY()
        {
            while (Move.Y != 0)
            {
                int sign = Math.Sign(Math.Round(Move.Y));
                Move.Y = (float)Math.Round(Move.Y);

                if (!Collision(new Point(0, sign), Mask))
                {
                    Entity.Position.Y += sign * Engine.Delta;
                    Move.Y -= sign;
                }
                else
                {
                    Move.Y = 0;
                    if (OnCollide != null)
                        OnCollide.Invoke(Hitbox.Entity);
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