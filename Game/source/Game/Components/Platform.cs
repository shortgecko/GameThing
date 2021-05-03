using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Platform : Component
    {
        float speed = -100f;
        Mover mover;
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

        private StateMachine<States> StateMachine = new StateMachine<States>();

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
            Entity.Add(StateMachine);
            mover = Entity.Get<Mover>();
            mover.Mask = Mover.Masks.Solids;
            Distance = Vector2.Distance(Entity.Position, Parameters.Nodes[0]);
            StateMachine.Add(States.Normal, null, Normal, null);
            StateMachine.Add(States.Moving, null, Move, null);
            StateMachine.Set(States.Normal);
        }

        public override void Update()
        {
           
        }

        public void Normal()
        {
            if (mover.Collision(new Point(0, -1), Mover.Masks.All))
                StateMachine.Set(States.Moving);
        }

        void Move()
        {
            if (Distance > 0)
            {
                mover.Move.Y -= Distance;
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
