using Microsoft.Xna.Framework;
using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;


namespace Frankenweenie
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
        public Action<CollisionData> OnCollide;
        public Action<CollisionData> OnCollideX;
        public Action<CollisionData> OnCollideY;
        private bool m_Grounded = false;
        public bool Grounded => m_Grounded;

        private List<Component> Hitboxes;
        private List<Component> Tilemaps;
        private List<Component> Triggers;

        public override void Initialize()
        {
            Hitboxes = World.All<Collider>();
            Triggers = World.All<Trigger>();
            Tilemaps = World.All<Tilemap>();
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

        public CollisionData Check(Point offset)
        {
            foreach (Hitbox collider in Hitboxes)
            {
                CollisionData collisionData = Hitbox.Check(offset,collider);
                if (collisionData != null)
                    return collisionData;
            }


            foreach (Tilemap collider in Tilemaps)
            {
                CollisionData collisionData = collider.Check(offset, Hitbox);
                if (collisionData != null)
                    return collisionData;
            }

            return null;

        }


        private bool _CheckSolids(Point offset)
        {
            //Hitbox hitbox = new Hitbox(Hitbox.X + offset.X, Hitbox.Y + offset.Y, Hitbox.Width, Hitbox.Height);

            //int left = (int)Thing(Math.Clamp(Math.Floor((double)hitbox.X / Level.Tiles.CellSize.X), 0, Level.Tiles.Width));
            //int right = (int)Thing(Math.Clamp(Math.Ceiling((double)hitbox.Right / Level.Tiles.CellSize.X), 0, Level.Tiles.Width));
            //int top = (int)Thing(Math.Clamp(Math.Floor((double)hitbox.Y / Level.Tiles.CellSize.Y), 0, Level.Tiles.Height));
            //int bottom = (int)Thing(Math.Clamp(Math.Ceiling((double)hitbox.Bottom / Level.Tiles.CellSize.Y), 0, Level.Tiles.Height));

            //if(j)
            //{
            //    Logger.Log(Entity.Position.X);
            //    Logger.Log(left);
            //}

            //for (int x = left; x < right; x++)
            //    for (int y = top; y < bottom; y++)
            //    {
            //        if (Level.Tiles[x, y] != -1)
            //            return true;
                    
            //    }
            return false;
        }

        private void CheckX()
        {
            int sign = Math.Sign(Move.X);
            Move.X = (float)Math.Round(Move.X);
  
            while (Move.X != 0)
            {
                CollisionData collisionData = Check(new Point(sign, 0));
                if (collisionData == null)
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    Move.X = 0;
                    if (OnCollide != null)
                        OnCollide.Invoke(collisionData);
                    if (OnCollideX != null)
                        OnCollideX.Invoke(collisionData);
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

                CollisionData collisionData = Check(new Point(0, sign));
                if (collisionData == null)
                {
                    Entity.Position.Y += sign * Engine.Delta;
                    Move.Y -= sign;
                }
                else
                {
                    Move.Y = 0;
                    if (OnCollide != null)
                        OnCollide.Invoke(collisionData);
                    if (OnCollideY != null)
                        OnCollideY.Invoke(collisionData);
                    break;
                }
            }
        }

        public void CheckTriggers()
        {
            foreach(var triggerRef in Triggers)
            {
                var trigger = (Trigger)triggerRef;

                if (Hitbox.Intersects(trigger))
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