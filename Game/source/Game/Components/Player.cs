using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Player : Component
    {
        private const int speed = 240;
        private const int jumpForce = /*-1200*/ -200;
        private const int hJumpForce = 120;
        private int normalGravity = 40;
        private float gravity;
        private Mover mover; 
        private Timer coyoteTimer;
        private float coyoteTime = 0.3f;
        private Timer jumpTimer;
        private float jumpTime = 0.8f;
        private bool grounded;

        public static Entity Create()
        {
            Entity player = new Entity();
            player.add(new Hitbox(0, 0, 8, 8));
            player.add<Player>();
            player.add<Mover>();
            return player;
        }

        public override void Initialize()
        {
            mover = entity.get<Mover>();
            entity.add(coyoteTimer = new Timer());
            entity.add(jumpTimer = new Timer());
        }

        public override void Update()
        {
            grounded = mover.collision(new Point(0, 1), Mover.Masks.All);
            mover.Move.X = Input.Horizontal.GetAxis() * speed * Engine.Delta;
            if (grounded)
                coyoteTimer.Start(coyoteTime);
            bool coyote = coyoteTimer.Duration > 0;
            if (Input.Jump.Pressed() && coyote)
            {
                mover.Move.Y = jumpForce * Engine.Delta;
                mover.Move.X = Input.Horizontal.GetAxis() * hJumpForce * Engine.Delta;
                jumpTimer.Start(jumpTime);
            }

            if (mover.Move.Y < 0 && jumpTimer.Duration == 0)
            {
                mover.Move.Y *= 0.5f;
            }

            if (!grounded)
                mover.Move.Y = normalGravity * Engine.Delta;
        }

        public override void Render()
        {
            Drawer.Rect(new Rectangle((int)entity.position.X, (int)entity.position.Y, 8,8), Color.Red);
        }
    }

}