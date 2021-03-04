using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System;

namespace Game
{
    public class Player : Component
    {
        private Vector2 Position = Vector2.Zero;
        private float Speed = 48f;
        public float Gravity = 1f;

        public float JumpForce = -72f;
        public float JumpTimeMult = 15f;
        public float JumpGravity = 5f;
        private Texture2D texture;
        private Mover mover;
        private StateMachine stateMachine {get;set;}
        private Timer Timer;
 
        public Player() //component
        {
            texture = Asset.Texture("player02.png");
        }

        public override void Initialize()
        {
           mover = Entity.Get<Mover>();
           mover.Hitbox = new Hitbox(0,0,48,48);
           stateMachine = Entity.Get<StateMachine>();
           Timer = Entity.Get<Timer>();
           stateMachine.AddState(1,NormalUpdate);
           stateMachine.State = 1;

        }
        public void NormalUpdate()
        {
            if(mover.OnGround == false)
                mover.MoveY = Gravity;

            Speed += Engine.DeltaTime;
            mover.MoveX = Input.Horizontal.GetAxis() * Speed * Engine.DeltaTime;
            if(mover.OnGround && Input.Jump.Pressed())
            {
               Timer.Start();
            }

            if(Timer.Get() >= 0.2f)
            {
                if(Input.Jump.Released())
                {
                    mover.MoveY = (JumpForce + (-Timer.Get() * JumpTimeMult) * Engine.DeltaTime);
                    Timer.Stop();
                    Gravity = JumpGravity;
                }
            }

            Timer.Log();

        }

        public override void Render()
        {
            Drawer.Batch.Draw(texture, new Vector2(this.Entity.Position.X, this.Entity.Position.Y), Color.White);
        }
    }
}
