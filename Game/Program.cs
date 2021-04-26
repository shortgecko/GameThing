using Frankenweenie;
using System.Collections.Generic;

namespace Game
{
    public class Program
    {
        private static SceneManager Scenes = new SceneManager()
        {
            Scenes = new Dictionary<string, Scene>()
            {
                { "Splash", new Splash() },
                { "Game" , new Game() },
            },
        };

        private static void Main()
        {
            Config config = Serializer.Deserialize<Config>("engine-config");
            Engine.Run(ref config, Scenes,"Game");
        }
    }
}