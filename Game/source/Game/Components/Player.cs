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
            Normal
        }

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

        public override void Initialize()
        {
            var sprite = Entity.Get<Sprite>();
            sprite.Texture = Content.CreateTexture(8, 8, Color.Red);
            sprite.LayerDepth = 2;
            Mover = Entity.Get<Mover>();
        }

        public override void Update()
        {
            Mover.Move.X = Speed * Input.Horizontal;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Mover.Collision(new Point(0, 1), Mover.Masks.All))
            {
                //Logger.Log("something");
                Mover.Move.Y = jumpForce;
                Mover.Move.X = Input.Horizontal * 500f;
            }
            Gravity(125);
          
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