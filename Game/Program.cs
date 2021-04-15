using Frankenweenie;

namespace Game
{
    public class Program
    {
        private static readonly int Width = 160;
        private static readonly int Height = 200;

        private static void Main()
        {
            Engine.Config = new Config("Game Thing", "Assets", true, Width * 4, Height * 4, true);
            Engine.Run(new Game());
        }
    }
}