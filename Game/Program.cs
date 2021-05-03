using Frankenweenie;
using System.Collections.Generic;
using MonoGame.Framework.Utilities.Deflate;
using System.IO;
using Microsoft.Xna.Framework;
using System;

namespace Game
{
    public delegate void Function(Entity e);

    public class Person
    {
        public string Name;
    }

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


        private static Config LoadConfig()
        {
            if(!File.Exists("engine-config"))
            {
                Config config = new Config("Frankenweenie Engine", "Assets", true, 1920, 1080, true);
                Serializer.Serialize<Config>(config, "engine-config");
            }

            return Serializer.Deserialize<Config>("engine-config");
        }

        private static void Run()
        {

            Config config = LoadConfig();
#if DEBUG
            Engine.Run(ref config, Scenes, "Game");
#else
            Engine.RunWithLogging(ref config, Scenes, "Game");
#endif
        }

        private static void Main()
        {
            Run();
        }

    }
}