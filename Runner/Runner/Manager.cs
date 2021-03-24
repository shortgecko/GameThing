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
        public static int INTSTATE;

        protected override void Initialize()
        {
            World.Add(Player.Create());
        }

        protected override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && State == States.Editor)
            {
                Scenes[(int)State].Leave();
                State = States.Game;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Escape) && State == States.Game)
            {
                Scenes[(int)State].Leave();
                State = States.Editor;
            }

            Scenes[(int)State].Begin();
        }

        protected override void Render()
        {
            Scenes[(int)State].Draw();
            
        }

    }
};