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
        private const int jumpForce = -900;
        private const int hJumpForce = 140;
        private int normalGravity = 20;
        private Mover mover;
        private Timer coyoteTimer;
        private float coyoteTime = 0.3f;
        private Timer jumpInputTimer;
        private float jumpInputTime = 0.3f;
        private bool grounded;
        private bool jumped = false;
        private Vector2 startPos;
        private bool canCoyote = false;
        private StateMachine<States> StateMachine;
        
        
        private enum States
        {
            Normal
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
        float timer = 0;
        public override void Initialize()
        {
            mover = entity.get<Mover>();
            entity.add(coyoteTimer = new());
            entity.add(jumpInputTimer = new());
            startPos = entity.position;
            StateMachine = entity.get<StateMachine<States>>();
            StateMachine.add(States.Normal, InnitState, UpdateState, EndState);
        }

        private void InnitState()
        {
            Logger.Log("Initialize State");
        }
        private void UpdateState()
        {
            timer += Engine.Delta;
            Logger.Log("Updating...");

            if (timer > 20)
                StateMachine.End();    
        }
        private void EndState()
        {
            Logger.Log("End");
        }
        private void NormalState()
        {
            grounded = mover.collision(new Point(0, 1), Mover.Masks.All);

            if (!grounded)
                mover.Move.Y = normalGravity;
            else
                coyoteTimer.Start(coyoteTime);

            mover.Move.X = Input.Horizontal * speed;
            canCoyote = coyoteTimer.Duration > 0;

            if (Input.Jump.Pressed)
            {
                jumpInputTimer.Start(jumpInputTime);
            }

            if (jumpInputTimer.Duration > 0 && canCoyote && !jumped)
            {
                mover.Move.Y = jumpForce;
                mover.Move.X = Input.Horizontal * hJumpForce;
                jumped = true;
            }

            if (Input.Jump.Released)
                jumped = false;

            if (mover.Move.Y < 0 /*&& jumpTimer.Duration == 0*/)
            {
                mover.Move.Y *= 0.5f;
            }
        }
        public override void Update()
        {
            if(!new Rectangle(0,0,320,180).Contains(entity.position) || Input.TempRestart)
                entity.position = startPos;
        }

        public override void Render()
        {
            Drawer.Rect(Utils.RectF(entity.position,8 ,8), Color.Red);
        }

    }
}