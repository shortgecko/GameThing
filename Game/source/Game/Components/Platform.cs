using Frankenweenie;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Game
{
    [Pooled]
    public class Platform : Component
    {
        private float Speed = -100f;
        Solid  Mover;
        private Parameters Parameters;
        private float Distance;

        private enum States
        {
            Normal,
            Moving,
        }

        private StateMachine<States> StateMachine;

        public static Entity Create(Vector2 position, Parameters p)
        {
            Entity e = new Entity();
            e.Position = position;
            e.Add<Solid>();
            e.Add(new Hitbox(0, 0, 16, 8));
            var platform = e.Add<Platform>();
            platform.Parameters = p;
            return e;
        }

   
        public override void Initialize()
        {
           Mover = Entity.Get<Solid>();
           StateMachine = new StateMachine<States>();
           Entity.Add(StateMachine);
           Distance = Vector2.Distance(Entity.Position, Parameters.Nodes[0]);
           StateMachine.Add(States.Normal, null, Normal, null);
           StateMachine.Add(States.Moving, null, Move, null);
           StateMachine.Set(States.Normal);
        }

        public override void Update()
        {
            Move();
        }
        void Normal()
        {
            // if(Mover.Collision(new Point(0, -1), Mover.Masks.Actors))
            // {
            //     StateMachine.Set(States.Moving);
            // }
        }
        

        void Move()
        {
            if (Distance > 0)
            {
                Mover.Move.X = + Distance;
                Distance -= Speed;
            }
        }

        public override void Render()
        {
            Drawer.Rect(Utils.RectF(Entity.Position, new Vector2(Parameters.Width, Parameters.Height)), Color.Yellow);
        }

        public override void Removed()
        {
            Entity.Position = Vector2.Zero;
            Parameters = null;
        }
    }
}
