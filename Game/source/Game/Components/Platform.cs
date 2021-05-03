using Frankenweenie;
using Microsoft.Xna.Framework;
using System;

namespace Game
{
    public class Platform : Component
    {
        private float speed = -100f;
        Mover Mover;
        private Parameters Parameters;
        private float Distance;

        private enum States
        {
            Normal,
            Moving,
        }

        public Platform(Parameters parameters)
        {
            Parameters = parameters;
        }

        private StateMachine<States> StateMachine;

        public static Entity Create(Parameters p)
        {
            Entity e = new Entity();
            e.Add<Mover>();
            e.Add(new Hitbox(0, 0, 16, 8));
            e.Add(new Platform(p));
            return e;
        }

   
        public override void Initialize()
        {
            Mover = Entity.Get<Mover>();
            Mover.Mask = Mover.Masks.Solids;
            StateMachine = new StateMachine<States>();
            Entity.Add(StateMachine);
            Distance = Vector2.Distance(Entity.Position, Parameters.Nodes[0]);
            StateMachine.Add(States.Normal, null, Normal, null);
            StateMachine.Add(States.Moving, null, Move, null);
            StateMachine.Set(States.Moving);
        }

        public override void Update()
        {
            
        }

        public void Normal()
        {
            if (Mover.Collision(new Point(0, 1), Mover.Masks.All))
                StateMachine.Set(States.Moving);
        }

        void Move()
        {
            if (Distance > 0)
            {
                Mover.Move.Y += Distance;
                Distance -= speed;
            }
        }

        public override void Render()
        {
            Drawer.Rect(Utils.RectF(Entity.Position, new Vector2(Parameters.Width, Parameters.Height)), Color.Yellow);
        }

        public override void Removed()
        {
            Parameters = null;
        }
    }
}
