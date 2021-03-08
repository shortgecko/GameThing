using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System;

namespace Game
{
    public class Player : Component
    {
        
        public static Entity Create(Vector2 Pos, Vector2 spawn)
        {
            Entity e = new Entity();
            e.Add<Player>();
            e.Get<Player>().Spawn = spawn;
            e.Add<StateMachine>();
            e.Add<Mover>();
            e.Add(new Hitbox(0,0,8,8));
            e.Position = Pos;
            return e;
        }

        public Vector2 Spawn;
        private StateMachine StateMachine;
        private Mover Mover;
        private float Speed = 128f;
        private float Gravity = 32f;
        private const float JumpForce = -600f;
        private const float JumpBufferMax = 0.3f;
        private const float MinJumpVarMult = 1.15f;
        private const float MaxJumpVarMult = 2.25f;
        private float  JumpBuffer = 0f;
        private bool CanJump = false;
        public Timer CoyoteTimer;
        public float CoyoteJumpGrace = 0.03f;
        public override void Initialize()
        {
            StateMachine = Entity.Get<StateMachine>();
            Mover = Entity.Get<Mover>();
            StateMachine.AddState(0,null,NormalUpdate,null);
            Entity.Add(CoyoteTimer = new Timer());
        }
        public void NormalUpdate()
        {
            //Regular moving
            Mover.MoveX = Input.Horizontal.GetAxis() * Speed * Engine.Delta;

            Mover.MoveY = Gravity * Engine.Delta;

            JumpGrace();

            if(Input.Jump.Pressed() && CanJump)
            {
                JumpBuffer += Engine.Delta;
            }

            if(Input.Jump.Released() && CanJump)
            {
                if(JumpBuffer <= JumpBufferMax)
                {
                    Mover.MoveY = JumpForce * MinJumpVarMult * Engine.Delta;
                    Logger.Log("SMALL JUMP");
                }
                else
                {
                    Mover.MoveY = JumpForce * MaxJumpVarMult * Engine.Delta;
                    Logger.Log("BIG JUMP");
                }
                JumpBuffer = 0f;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Entity.Position = Spawn;
            }
        }

        public void JumpGrace()
        {
            if(Mover.OnGround)
            {
                CoyoteTimer.Start(CoyoteJumpGrace);
            }

            CanJump = CoyoteTimer.Duration > 0;
        }


        public override void Render()
        {
           Asset.DrawRectangle(new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 8,8), Color.Red);
        }
    }
}
