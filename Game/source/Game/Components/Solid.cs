using System;
using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Game
{
    [Pooled]
    public class Solid : Component
    {
        private List<Component> Actors = new List<Component>();
        private List<Mover> Riding = new List<Mover>();
        public  Vector2 Move;
        private Hitbox Hitbox;
        private Trigger Trigger;
        public  List<Component> Hitboxes;

        public override void Initialize()
        {
            Actors = World.All<Mover>();
            Hitbox = Entity.Get<Hitbox>();
            base.Initialize();
        }
        
        private bool Check(Point offset, Hitbox other) => Hitbox.Check(offset, Hitbox, other);

    }
}
