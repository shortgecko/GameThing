using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;

namespace Game
{
    public class Sandbox : Scene
    {
        public override void Initialize()
        {
            Engine.RenderTarget.Width = 320;
            Engine.RenderTarget.Height = 180;
            Engine.RenderTarget.Scale = 6f;
            var level = new LevelLoader();
            level = null;
            Engine.Collect();
            base.Initialize();
        }

        public override void Load()
        {
           base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
            Level.Render();
        }
    }
}
