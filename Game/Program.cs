using Frankenweenie;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        private static readonly int Width = 320;
        private static readonly int Height = 180;

        private static SceneManager Scenes = new SceneManager()
        {
            Scenes = new()
            {
                { "Splash", new Splash()},
                { "Game" , new Game() },
            },
        };

        private static void Main()
        {
            Config Config = new Config("Game Thing", "Assets", true, Width * 4, Height * 4, Scenes, true);
            Engine.Run(ref Config, "Game");
        }
    }
}