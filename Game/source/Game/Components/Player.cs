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
        private Facing Facing;
        private const int speed = 240;
        private const int vSpeed = -20;
        private int normalGravity = 20;
        private StateMachine<States> StateMachine;
        private bool grounded { get { return mover.collision(new Point(0, 1), Mover.Masks.All); } }
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
                    return mover.collision(new Point(Facing, 0), Mover.Masks.All);
                else
                {
                    if (mover.collision(new Point(1, 0), Mover.Masks.All))
                        return true;
                    else if (mover.collision(new Point(-1, 0), Mover.Masks.All))
                        return true;
                }
                return false;
            }
        }
        private const float climbSpeed = 180;

        private enum States
        {
            Normal,
            Wall,
        };

        public static Entity Create()
        {
            Entity player = new Entity();
            player.Add(new Hitbox(0, 0, 8, 8));
            player.Add<Player>();
            player.Add<Mover>();
            player.Add<StateMachine<States>>();
            return player;
        }

        public override void Initialize()
        {
            mover = Entity.Get<Mover>();
            Entity.Add(coyoteTimer = new());
            Entity.Add(jumpInputTimer = new());
            Entity.Add(Facing = new Facing());
            Entity.Add(varJumpTimer = new Timer());
            startPos = Entity.Position;
            StateMachine = Entity.Get<StateMachine<States>>();
            StateMachine.Add(States.Normal, null, NormalState, null);
            StateMachine.Add(States.Wall, null, WallState, null);
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
                //Logger.Log();
                jumped = false;
            }
        }

        private void WallState()
        {
            if (Input.WallClimb.Released)
            {
                
            }
            
            StateMachine.Set(States.Normal);

        }

        bool a = false;
        public override void Update()
        {
            if (Input.Jump.Pressed)
            {
                World.Entities.Add(new Entity());
            }

            Logger.Log(World.Entities.Count);

            if (!new Rectangle(0,0,320,180).Contains(Entity.Position) || Input.TempRestart)
                Entity.Position = startPos;
        }
        public override void Render()
        {            
            Drawer.Rect(Utils.RectF(Entity.Position, 8 ,8), Color.Red);
        }


    }
}