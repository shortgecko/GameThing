﻿using Microsoft.Xna.Framework;
using Pinecorn;
using System;

namespace Game
{
    public class Mover : Component
    {
        public Vector2 Velocity;
        public Hitbox Hitbox;

        public float MoveX = 0f;
        public float MoveY = 0f;
        public bool OnGround = false;

        public override void Update()
        {
            
            this.Velocity.X = MoveX;
            this.Velocity.Y = MoveY;

            Hitbox = Entity.Get<Hitbox>();

            foreach(var solid in Level.Solids)
            { 
               if(CheckX(solid))
                    this.Velocity.X = 0;
                if(CheckY(solid))
                    this.Velocity.Y = 0;                   
            }

            if(MoveY != 0 && Velocity.Y == 0)
            {
                OnGround = true;
            }
            else
                OnGround = false;

            Entity.Position += Velocity;
            Velocity = Vector2.Zero;
            MoveX = 0;
            MoveY = 0;
        }

        public bool CheckX(Hitbox solid)
        {
            return ((this.Velocity.X > 0 && this.Hitbox.IsTouchingLeft(solid, this)) || (this.Velocity.X < 0 & this.Hitbox.IsTouchingRight(solid, this)));
        }

        public bool CheckY(Hitbox solid)
        {
            return ((this.Velocity.Y > 0 && this.Hitbox.IsTouchingTop(solid, this)) || (this.Velocity.Y < 0 & this.Hitbox.IsTouchingBottom(solid, this)));
        }


        public bool HitboxIntersects(Hitbox box,Point offset)
        {
            return new Rectangle(Hitbox.Bounds.X + offset.X, Hitbox.Bounds.Y + offset.Y, Hitbox.Bounds.Width, Hitbox.Bounds.Height).Intersects(box.Bounds);
        }

        public void CollisionCheckX(Hitbox solid)
        {
            if(HitboxIntersects(solid, new Point((int)MoveX * Math.Sign((int)MoveX),0)))
            {
                 this.Velocity.X = 0;
            }
        }
        public void CollisionCheckY(Hitbox solid)
        {
            if(HitboxIntersects(solid, new Point(0,(int)MoveY)))
            {
                //  this.Velocity.Y = 0;
            }
        }



    }
}