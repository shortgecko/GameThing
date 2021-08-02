using Frankenweenie;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Game
{
    [Pooled]
    public class Platform : Component
    {
        private float Speed = 100f;
        Mover  Mover;
        private Parameters Parameters;
        private float Distance = 48;

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
            e.Add<Mover>();
            e.Add(new Hitbox(0, 0, 16, 8));
            var platform = e.Add<Platform>();
            platform.Parameters = p;
            return e;
        }

   
        public override void Initialize()
        {
           Mover = Entity.Get<Mover>();
        }

        public override void Update()
        {
            //Mover.Move.X -= 200f;
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
