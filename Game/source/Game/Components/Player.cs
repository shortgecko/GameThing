using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game
{
    [Pooled]   
    public class Player : Component
    {
        private Vector2 startPos;
        private Mover mover;
        private const int speed = 240;
        private int normalGravity = 20;
        private StateMachine<States> StateMachine;
        private bool grounded;

        private const int jumpForce = -900;
        private const int hJumpForce = 140;
        private Timer coyoteTimer;
        private float coyoteTime = 0.3f;
        private Timer jumpInputTimer;
        private float jumpInputTime = 0.3f;
        private bool jumped = false;

        private bool wallTouch = false;
        private bool updated = false;

        private enum States
        {
            Normal,
            Wall,
        };

        public static Entity Create()
        {
            Entity player = new Entity();
            player.add(new Hitbox(0, 0, 8, 8));
            player.add<Player>();
            player.add<Mover>();
            player.add<StateMachine<States>>();
            return player;
        }

        public override void Initialize()
        {
            mover = Entity.get<Mover>();
            Entity.add(coyoteTimer = new());
            Entity.add(jumpInputTimer = new());
            startPos = Entity.Position;
            StateMachine = Entity.get<StateMachine<States>>();
            StateMachine.Add(States.Normal, null, NormalState, null);
            StateMachine.Add(States.Wall, null, WallState, null);
        }


        private void NormalState()
        {
            if (!grounded)
                mover.Move.Y = normalGravity;
            else
                coyoteTimer.Start(coyoteTime);

            mover.Move.X = Input.Horizontal * speed;

            if (Input.Jump.Pressed)
            {
                updated = true;
                jumpInputTimer.Start(jumpInputTime);
            }
            
            if (jumpInputTimer.Duration > 0 && coyoteTimer.Duration > 0 && !jumped)
            {
                mover.Move.Y = jumpForce;
                mover.Move.X = Input.Horizontal * hJumpForce;
                jumped = true;
                coyoteTimer.Clear();
                jumpInputTimer.Clear();
            }

            if (Input.Jump.Released)
                jumped = false;

            if (mover.Move.Y < 0)
            {
                mover.Move.Y *= 0.5f;
            }

            if (Input.WallClimb && wallTouch)
            {
                StateMachine.Set(States.Wall);
            }

        }

        private void WallState()
        {
            if (Input.WallClimb.Released)
                StateMachine.Set(States.Normal);
            Logger.Log((bool)Input.WallClimb);
            mover.Move.Y = Input.Vertical * speed;
        }

        public override void Update()
        {
            if(!new Rectangle(0,0,320,180).Contains(Entity.Position) || Input.TempRestart)
                Entity.Position = startPos;
            grounded = mover.collision(new Point(0, 1), Mover.Masks.All);
            wallTouch = mover.collision(new Point(Math.Sign(mover.Move.X), 0), Mover.Masks.All);
        }
        public override void Render()
        {
            Drawer.Rect(Utils.RectF(Entity.Position,8 ,8), Color.Red);
        }

    }
}