using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Game
{
    public class Splash : Scene
    {
        private float Timer;
        private const float Len = 1.5f;
        private Texture2D Logo;

        protected override void Load()
        {
            Logo = Content.Texture("graphics/logo.png");
        }

        protected override void Update()
        {
            Timer += Engine.Delta;

            if (Timer > Len)
            {
                Engine.Set(Engine.SceneManager["Game"]);
            }

        }

        protected override void Unload() => Logo.Dispose();

        protected override void Render() => Drawer.Batch.Draw(Logo, new Rectangle(0, 0, 320 * 4, 180 * 4), Color.Red);
    }
}
