using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class SceneManager
    {
        public Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>();

        public void Add(Scene scene)
        {
            if (Scenes.ContainsKey(scene.Name))
                throw new Exception("Cannot add two scenes of the same name to scene manager");
            Scenes.Add(scene.Name, scene);
        }

        public void Dispose(Scene scene)
        {
            if (!Scenes.ContainsKey(scene.Name))
                throw new Exception($"Scene {scene.Name} doesn't exist");
            Scenes.Remove(scene.Name);
        }

        public void Dispose(string scene)
        {
            if (!Scenes.ContainsKey(scene))
                throw new Exception($"Scene {scene} doesn't exist");
            Scenes.Remove(scene);
        }

        public Scene this[string name]
        {
            get
            {
                if(!Scenes.ContainsKey(name))
                    throw new Exception($"Scene {name} does not exist!");

                return Scenes[name];
            }
        }


    }
}
