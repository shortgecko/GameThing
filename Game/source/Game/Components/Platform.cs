using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Frankenweenie;
using System;

namespace Game
{
    public class Platform : Component
    {
        float speed = -100f;
        Mover mover;
        private Hitbox hitbox;
        private Parameters parameters;
        private float distance;
        private Vector2 direction;
        private enum States
        {
            Normal,
            Moving,
        }
        private StateMachine<States> StateMachine = new StateMachine<States>();

        public static Entity Create(Parameters p)
        {
            Entity j = new Entity();
            j.Add<Mover>();
            j.Add(new Hitbox(0, 0, 16, 8));
            j.Add<Platform>();
            j.Get<Platform>().parameters = p;
            return j;
        }

   
        public override void Initialize()
        {
            Entity.Add(StateMachine);

            mover = Entity.Get<Mover>();
            hitbox = Entity.Get<Hitbox>();
            mover.Mask = Mover.Masks.Solids;

            Vector2 end = parameters.Nodes[0];
            distance = Vector2.Distance(Entity.Position, end);
            StateMachine.Add(States.Normal, null, Normal, null);
            StateMachine.Add(States.Moving, null, Move, null);
            StateMachine.Set(States.Normal);
        }

        public override void Update()
        {
            Logger.Log(StateMachine.State);
        }

        public void Normal()
        {
            if (mover.collision(new Point(0, -1), Mover.Masks.All))
                StateMachine.Set(States.Moving);
        }

        void Move()
        {
            if (distance > 0)
            {
                mover.Move.Y -= distance;
                distance -= speed;
            }
        }

        public override void Render()
        {
            Drawer.Rect(Utils.RectF(Entity.Position, new Vector2(16,8)), Color.Yellow);
        }
    }
}
