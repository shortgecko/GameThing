using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System.Collections.Generic;

namespace Game
{
    public class Level : Scene
    {
        public static List<Hitbox> Solids = new List<Hitbox>();
        public float LevelTime;

        public Texture2D Background;

        public enum States
        {
            Playing,
            Paused,
            Editor
        };

        public States State = States.Playing;

        public override void Initialize()
        {
            Engine.RenderTarget.Width = 160;
            Engine.RenderTarget.Height = 200;
            Engine.RenderTarget.Scale = 4f;


            Entity entity = Player.Create();
            entity.position.Y = 100;
            World.Add(entity);

            //var entity2 = Player.Create();
            //entity2.position.Y = 300;
            //World.AddEntity(entity2);

            base.Initialize();
        }

        public override void Load()
        {
            Background = Asset.Texture("Graphics/background.png");
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update()
        {
            switch(State)
            {
                case States.Playing:
                    base.Update();
                    break;
                case States.Paused:
                    break;
                case States.Editor:
                    Editor.GameLayer.Update();
                    break;
            }
            Camera.Update();
            LevelTime += Engine.Delta;
        }

        public override void Render()
        {
            base.Render();
            if (State == States.Editor)
                Editor.GameLayer.Render();
        }
    }
}
