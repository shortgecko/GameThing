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

        public StateMachine StateMachine;
        public Texture2D Texture;
        public Mover Mover;
        public float Speed = 48f;
        public float Gravity = 32f;

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

            if(!Mover.OnGround)
                Mover.MoveY = Gravity * Engine.DeltaTime;
        }


        public override void Render()
        {
           Asset.DrawRectangle(new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 8,8), Color.Red);
        }
    }
}
