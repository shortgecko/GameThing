using Frankenweenie;
using GameScene = Game.Game;

namespace Runner
{
    class Program
    {
        private static readonly int Width = 160;
        private static readonly int Height = 200;

        static void Main(string[] args)
        {
            Engine.Config = new Config("Game Thing", "Assets", true, Width * 4, Height * 4, true);
            Engine.Run(new Manager());
        }
    }
}
