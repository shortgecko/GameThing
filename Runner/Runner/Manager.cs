using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Frankenweenie;
using Game.Editor;
using Game;
namespace Runner
{
    public class Manager : Scene
    {
        private Scene[] Scenes =
        {
            new Game.Game(),
            new LevelEditor(),
        };

        private enum States
        {
            Game = 0,
            Editor = 1,
        };

        private States State = (States)1;

        protected override void Initialize()
        {
            World.Add(Player.Create());
        }

        protected override void Update()
        {
            Scenes[(int)State].Begin();
        }

        protected override void Render()
        {
            Scenes[(int)State].Draw();
        }

    }
};