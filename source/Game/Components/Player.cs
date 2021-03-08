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
        private float Speed = 144f;
        private float Gravity = 80f;
        private const float JumpForce = -800f;
        private const float JumpBufferMax = 0.3f;
        private const float MinJumpVarMult = 1.15f;
        private const float MaxJumpVarMult = 2.25f;
        private float  JumpBuffer = 0f;
        private bool CanJump = false;
        private Timer CoyoteTimer;
        private Timer Jumput;
        private float CoyoteJumpGrace = 0.03f;
        private float JumputGrace = 0.02f;
        private bool DoJump = false;
        public override void Initialize()
        {
            StateMachine = Entity.Get<StateMachine>();
            Mover = Entity.Get<Mover>();
            StateMachine.AddState(0,null,NormalUpdate,null);
            Entity.Add(CoyoteTimer = new Timer());
            Entity.Add(Jumput = new Timer());
        }
        public void NormalUpdate()
        {
            //Regular moving
            Mover.MoveX = Input.Horizontal.GetAxis() * Speed * Engine.Delta;

            Mover.MoveY = Gravity * Engine.Delta;

            JumpGrace();

            if(Input.Jump.Pressed())
            {
                Jumput.Start(JumputGrace);
                JumpBuffer += Engine.Delta;
            }

            if(DoJump)
            {
                if(JumpBuffer <= JumpBufferMax)
                {
                    Mover.MoveY = JumpForce * MinJumpVarMult * Engine.Delta;
                    Logger.Log("SMALL JUMP");
                }
                else
                {
                    Mover.MoveY = JumpForce * MaxJumpVarMult * Engine.Delta;
                    // Mover.MoveX = 100f;
                    Logger.Log("BIG JUMP");
                }
                JumpBuffer = 0f;
                Gravity += Engine.Delta;
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
            DoJump = Input.Jump.Released() && CanJump || Mover.OnGround && Jumput.Duration > 0;
        }


        public override void Render()
        {
           Asset.DrawRectangle(new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 8,8), Color.Red);
        }
    }
}
