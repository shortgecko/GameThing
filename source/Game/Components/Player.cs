using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System;

namespace Game
{
    public class Player : Component
    {
        
        public static Entity Create(Vector2 Pos)
        {
            Entity e = new Entity();
            e.Add<Player>();
            e.Add<StateMachine>();
            e.Add<Mover>();
            e.Add(new Hitbox(0,0,8,8));
            e.Position = Pos;
            return e;
        }

        private StateMachine StateMachine;
        private Mover Mover;
        private float Speed = 128f;
        private float Gravity = 32f;
        private const float JumpForce = -400f;
        private const float JumpBufferMax = 0.8f;
        private const float MinJumpVarMult = 1.15f;
        private const float MaxJumpVarMult = 1.75f;
        private float  JumpBuffer = 0f;


        public override void Initialize()
        {
            StateMachine = Entity.Get<StateMachine>();
            Mover = Entity.Get<Mover>();
            StateMachine.AddState(0,null,NormalUpdate,null);
        }
        public void NormalUpdate()
        {
            //Regular moving
            Mover.MoveX = Input.Horizontal.GetAxis() * Speed * Engine.DeltaTime;

            Mover.MoveY = Gravity * Engine.DeltaTime;

            if(Input.Jump.Pressed() && Mover.OnGround)
            {
                JumpBuffer += Engine.DeltaTime;
            }

            if(Input.Jump.Released())
            {
                if(JumpBuffer <= JumpBufferMax)
                {
                    Mover.MoveY = JumpForce * MinJumpVarMult *Engine.DeltaTime;
                }
                else
                {
                    Mover.MoveY = JumpForce * MaxJumpVarMult * Engine.DeltaTime;
                }
                JumpBuffer = 0f;
            }
            Console.WriteLine(JumpBuffer);
        }


        public override void Render()
        {
           Asset.DrawRectangle(new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 8,8), Color.Red);
        }
    }
}
