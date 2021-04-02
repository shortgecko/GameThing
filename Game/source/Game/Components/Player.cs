using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Player : Component
    {
        private const int speed = 512;
        private const int gravity = 100;
        private Mover mover;

        public static Entity Create()
        {
            Entity player = new Entity();
            player.add<Player>();
            player.add(new Hitbox(0, 0,8,8));
            player.add<Mover>();
            player.Name = "player";
            return player;
        }


        public override void Initialize()
        {

            mover = entity.get<Mover>();
        }


        public override void Update()
        {
            mover.Move.X = Input.Horizontal.GetAxis() * speed * Engine.Delta;
            mover.Move.Y = Input.Vertical.GetAxis() * speed * Engine.Delta;

        }

        public override void Render()
        {
            Drawer.Rect(new Rectangle((int)entity.position.X, (int)entity.position.Y, 8,8), Color.Red);
        }
    }

}