using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Program 
    {
        public static void Main()
        {
            SceneManager sceneManager = new SceneManager();
            sceneManager.Add(SceneLoader.Load("level.json"));
            Config config = Serializer.Deserialize<Config>("engine-config");
            Engine.Run(ref config, sceneManager);
        }
    }
}
