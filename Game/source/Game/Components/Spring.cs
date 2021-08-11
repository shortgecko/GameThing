using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Spring : Component
    {
        private Mover Mover;

        public static Entity Create(Vector2 position)
        {
            Entity entity = new Entity();
            entity.Position = position;
            entity.Add<Mover>();
            entity.Add<Sprite>();
            entity.Add(new Hitbox(0, 0, 8, 8));
            entity.Add<Spring>();
            return entity;

        }

        public override void Initialize()
        {
            var sprite = Entity.Get<Sprite>();
            sprite.Texture = Content.CreateTexture(8, 8, Color.Green);
            sprite.LayerDepth = 100f;
            Mover = Entity.Get<Mover>();

        }

        public override void Update()
        {

        }

    }
}
