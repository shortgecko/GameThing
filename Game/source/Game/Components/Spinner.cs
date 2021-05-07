using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;
namespace Game
{
    public class Spinner : Component
    {
        private Mover Mover;
        private Sprite Sprite;
        private Hitbox Hitbox;

        public static Entity Create()
        {
            Entity e = new Entity();
            e.Add<Mover>();
            e.Add<Spinner>();
            e.Add(new Hitbox(0, 0, 10, 8));
            e.Add<Sprite>();
            return e;
        }

        public override void Initialize()
        {
            Hitbox = Entity.Get<Hitbox>();
            Sprite = Entity.Get<Sprite>();
            Sprite.Texture = Content.LoadTexture("graphics/spinner.png");
            Sprite.Origin = new Vector2(Sprite.Texture.Width, Sprite.Texture.Height);
            Mover = Entity.Get<Mover>();
            Action<Entity> OnCollide = (Entity entity) =>
            {
                if (entity != null)
                {
                    Player player = Entity.Get<Player>();
                    if (player != null)
                    {
                        player.Die = true;
                    }
                }
            };

            Mover.OnCollideY = OnCollide;
            Mover.OnCollideY = OnCollide;
        }

        public override void Update()
        {
            Sprite.Rotation += 0.1f;
        }



    }

}
