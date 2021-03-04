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
            new LevelLoader();
            base.Initialize();
        }

        public override void Load()
        {
           
        }

        public override void Unload()
        {

        }

        public override void Update()
        {
            Input.Update();
            base.Update();
        }

        public override void Render()
        {
            base.Render();
            Level.Render();
        }
    }
}
