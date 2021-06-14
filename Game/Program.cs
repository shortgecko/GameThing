using Frankenweenie;
using System.Collections.Generic;
using MonoGame.Framework.Utilities.Deflate;
using System.IO;
using Microsoft.Xna.Framework;
using System;

namespace Game
{

    public class Program
    {
        #region GameStuff
        private static SceneManager Scenes = new SceneManager()
        {
            Scenes = new Dictionary<string, Scene>()
            {
                { "Splash", new Splash() },
                { "Game" , new Game() },
                {"Text", new GUI()},
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

            Config config = Serializer.Deserialize<Config>(Path.Combine(Engine.AssemblyDirectory + "engine-config"));
#if DEBUG
            Engine.Run(ref config, Scenes, "Game");
          //  Engine.Run(ref config, Scenes, "Game");
#else
            Engine.RunWithLogging(ref config, Scenes, "Game");
#endif
        }
        #endregion


        private static void Main()
        {
            string[] allfiles = Directory.GetFiles("assets", "*.*", SearchOption.AllDirectories);
            foreach(var s in allfiles)
            {
                Console.WriteLine($"thing {s}");
            }
            Run();
        }

    }
}