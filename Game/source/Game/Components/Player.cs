using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game
{

    public class Player : Component
    {
        
        public enum States
        { 
            Normal,
            WallClimb
        };

        public static Entity Create(Vector2 Position)
        {
            Entity player = new Entity();
            player.Position = Position;
            player.Add(new Hitbox(0, 0, 8, 8));
            player.Add<Player>();
            player.Add<Mover>();
            player.Add<Sprite>();
            player.Add<StateMachine<States>>();
            return player;
        }

        private Mover Mover;
        private StateMachine<States> StateMachine;
        public Vector2 Start;

        private float Speed = 80f;
        private float MaxRun = 100f;
        private float RunAccelerate = 113f;
        private float RunDeccelerate = 400f;

        public override void Initialize()
        {
            Start = Entity.Position;
            var sprite = Entity.Get<Sprite>();
            var hitbox = Entity.Get<Hitbox>();
            sprite.Texture = Content.CreateTexture(hitbox.Width, hitbox.Height, Color.Red);
            sprite.LayerDepth = 100f;
  
            Mover = Entity.Get<Mover>();


            StateMachine = Entity.Get<StateMachine<States>>();
            StateMachine.Add(States.Normal, null, Normal, null);
            StateMachine.Add(States.WallClimb, null, WallClimb, null);

        }

        public bool Grounded
        {
            get
            {
                return Mover.Collision(new Point(0, 1));
            }
        }
        
        public void Normal()
        {

        }


        public void WallClimb()
        {

        }

        public override void Update()
        {


            Logger.Log(Mover.Move.X);

        }

    }
}