using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game
{
     
    public class Player : Component
    {

        private Vector2 startPos;
        private Mover mover;
        private Facing Facing;
        private const int speed = 240;
        private const int vSpeed = -20;
        private int normalGravity = 20;
        private StateMachine<States> StateMachine = new StateMachine<States>();
        private bool grounded { get { return mover.Collision(new Point(0, 1), Mover.Masks.All); } }
        private const int jumpForce = -800;
        private const int hJumpForce = 140;
        private Timer coyoteTimer;
        private float coyoteTime = 0.3f;
        private Timer jumpInputTimer;
        private float jumpInputTime = 0.3f;
        private bool jumped = false;
        private Timer varJumpTimer = new Timer();
        private float varJumpTime = 0.2f;
        private bool WallCheck
        {
            
            get
            { 
                if (Facing != 0)
                    return mover.Collision(new Point(Facing, 0), Mover.Masks.All);
                else
                {
                    if (mover.Collision(new Point(1, 0), Mover.Masks.All))
                        return true;
                    else if (mover.Collision(new Point(-1, 0), Mover.Masks.All))
                        return true;
                }
                return false;
            }
        }
        private const float climbSpeed = 180;
        private Sprite Sprite;
        Vector2 StartPosition;
        public bool Die = false;

        private enum States
        {
            Player,
            Wall,
        };

        public static Entity Create()
        {
            Entity player = new Entity();
            player.Name = "Player";
            player.Add(new Hitbox(0, 0, 8, 8));
            player.Add<Player>();
            player.Add<Mover>();
            player.Add<Sprite>();
            player.Add<StateMachine<States>>();
            return player;
        }

        public override void Initialize()
        {
            StartPosition = Entity.Position;
            mover = Entity.Get<Mover>();
            Sprite = Entity.Get<Sprite>();
            Sprite.Texture = Content.CreateTexture(8, 8, Color.Red);
            

            Entity.Add(coyoteTimer = new());
            Entity.Add(jumpInputTimer = new());
            Entity.Add(Facing = new Facing());
            Entity.Add(varJumpTimer = new Timer());
            
            Entity.Add(StateMachine);


            startPos = Entity.Position;
            StateMachine.Add(States.Player, null, NormalState, null);
            StateMachine.Add(States.Wall, null, WallState, null);
            StateMachine.Set(States.Player);
        }

        bool done = false;

        public override void Update()
        {
            /*Logger.Log($"Count {World.All<Player>().Count}");
            Logger.Log(World.All<Player>()[0].Entity == Entity);
            var player = Entity.Get<Sprite>();
            Entity.Remove(player);*/


            if (Die)
            {
                Logger.Log("death");
                Entity.Position = StartPosition;
                Die = false;
            }
        }


        private void NormalState()
        {
            if (!grounded)
                mover.Move.Y += normalGravity;
            Jump();

            if (Input.WallClimb)
            {
                if(WallCheck)
                    StateMachine.Set(States.Wall);
            }

        }

        private void Jump()
        {
            if (grounded)
                coyoteTimer.Start(coyoteTime);

            mover.Move.X = Input.Horizontal * speed;

            if (Input.Jump.Pressed)
            {
                jumpInputTimer.Start(jumpInputTime);
            }

            if (Input.Jump.Released)
                jumped = false;

            if (jumpInputTimer.Duration > 0 && coyoteTimer.Duration > 0 && !jumped)
            {
                mover.Move.Y = jumpForce;
                mover.Move.X += Input.Horizontal * hJumpForce;
                coyoteTimer.Clear();
                jumpInputTimer.Clear();
                varJumpTimer.Start(varJumpTime);
                jumped = true;
            }

            if (varJumpTimer.Duration < 0 && jumped)
            {
                mover.Move.Y *= 2;

                jumped = false;
            }
        }

        private void WallState()
        {
            if (Input.WallClimb.Released)
            {
                
            }
            
            StateMachine.Set(States.Player);

        }


        public override void Removed()
        {
            Entity.Remove<Player>();
        }


    }
}