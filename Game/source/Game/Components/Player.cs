using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game
{

    public class Player : Component
    {
        
        public enum States
        { 
            Normal,
            WallClimb
        };

        public static Entity Create(Vector2 Position)
        {
            Entity player = new Entity();
            player.Position = Position;
            player.Add(new Hitbox(0, 0, 8, 8));
            player.Add<Player>();
            player.Add<Mover>();
            player.Add<Sprite>();
            player.Add<StateMachine<States>>();
            return player;
        }

        private Mover Mover;
        private float Speed = 200f;
        private float gravity = 100f;
        private float jumpForce = -1800f;
        private Timer coyoteTimer;
        private float coyoteTime = 0.14f;
        private StateMachine<States> StateMachine;
        private bool jumping = false;

        public override void Initialize()
        {
            var sprite = Entity.Get<Sprite>();
            var hitbox = Entity.Get<Hitbox>();
            sprite.Texture = Content.CreateTexture(hitbox.Width, hitbox.Height, Color.Red);
            sprite.LayerDepth = 2;
            Mover = Entity.Get<Mover>();
            Entity.Add(coyoteTimer = new());

            StateMachine = Entity.Get<StateMachine<States>>();
            StateMachine.Add(States.Normal, null, Normal, null);
            StateMachine.Add(States.WallClimb, null, WallClimb, null);
        }

        public bool Grounded
        {
            get
            {
                return Mover.Collision(new Point(0, 1));
            }
        }
        
        public void Normal()
        {
            Mover.Move.X = Speed * Input.Horizontal;

            if(Input.Horizontal != 0)
            {
                Mover.Move.X += 100f;
            }

            if (Grounded)
            {
                coyoteTimer.Start(coyoteTime);
            }

            if (Input.Jump.Pressed && coyoteTimer.Duration > 0)
            {
                Mover.Move.Y = jumpForce;
                Mover.Move.X = Input.Horizontal * Math.Abs(jumpForce);
            }

            //if(jumping)
            //{
            //    Mover.Move.Y = jumpForce * 
            //    Mover.Move.X = Input.Horizontal * Math.Abs(jumpForce);
            //}

            //if(Input.WallClimb.Pressed)
            //{
            //    int x = (int)Input.Horizontal;
            //    if (Mover.Collision(new Point(x, 0)))
            //    {
            //        StateMachine.Set(States.WallClimb);
            //    }
            //}

        }

        public void WallClimb()
        {
            Mover.Move.Y = Speed * Input.Vertical;
            //if (Input.WallClimb.Released)
            //{
            //    StateMachine.Set(States.Normal);
            //}

            if(Input.Jump)
            {
                Mover.Move.X += Math.Abs(jumpForce) * Input.Horizontal;
                Mover.Move.Y = -200;
            }
        }

        public override void Update()
        {
            Gravity(150);
        }

        private void Gravity(float maxGravity)
        {
            if(gravity <= maxGravity)
            {
                Mover.Move.Y += gravity;
            }
            if(Input.Vertical == 1 && gravity <= maxGravity)
            {
                Mover.Move.Y++;
            }
        }
    }
}