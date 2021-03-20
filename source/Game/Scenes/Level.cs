using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
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
        private Camera Camera = new Camera();
        protected override void Initialize()
        {
            //Engine.RenderTarget.Width = 160;
            //Engine.RenderTarget.Height = 200;
            Engine.RenderTarget.Scale.X = 4f;
            Engine.RenderTarget.Scale.Y = 4f;
            Entity entity = Player.Create();
            World.Add(entity);

            base.Initialize();
        }

        protected override void Load()
        {
            Background = Asset.Texture("Graphics/background.png");
        }

        protected override void Update()
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
            LevelTime += Engine.Delta;
        }

        protected override void Render()
        {
            base.Render();
            if (State == States.Editor)
                Editor.GameLayer.Render();
        }
    }
}
