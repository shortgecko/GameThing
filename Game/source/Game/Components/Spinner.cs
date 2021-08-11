using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    [Pooled]
    public class Spinner : Component
    {
        public Parameters Parameters;

        public static Entity Create(Vector2 position, Parameters Parameters)
        {
            Entity entity = new Entity();
            entity.Position = position;
            var spinner = entity.Add<Spinner>();
            spinner.Parameters = Parameters;
            return entity;
        }


        public override void Initialize()
        {
            Texture2D Texture = Content.CreateTexture(Parameters.Width, Parameters.Height, Color.White);
            int width = Parameters.Width / Texture.Width;
            int height = Parameters.Height / Texture.Height;

            Entity.Add(new Trigger(Entity.Position.X, Entity.Position.Y, Parameters.Width, Parameters.Height)
            {
                OnTriggerEnter = (Entity entity) =>
                {
                    Player player = entity.Get<Player>();
                    if (player != null)
                    {
                        player.Entity.Position = player.Start;
                    }
                },
            });


            for (int x = 0; x < width; x++)
                for(int y = 0; y < height; y++)
                {
                    var sprite = Entity.Add<Sprite>();
                    sprite.Texture = Texture;
                    //sprite.DrawOffset = new Vector2(x * 8, y * 8);
                    sprite.LayerDepth = 100f;
                }
        }

        bool done = false;
        public override void Update()
        {
            if(!done)
            {
                Logger.Log(World.All<Trigger>().Count, false);
                done = true;
            }
            base.Update();
        }


    }

}