using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public static class SceneLoader
    {
        public static Scene Load(string path)
        {
            Scene scene = new Scene();
            scene.Name = path;
            World.Clear();
            var OgmoLevel = Content.LoadLevel(path);
            var EntityLayer = OgmoLevel["Entity"];

            foreach(var layer in OgmoLevel.Data.layers)
            {
                if(layer.data != null)
                {

                }
            }

            foreach(var e in EntityLayer.entities)
            {
                Entity entity = EntityInstancer.Get(e.name);
                entity.Position = new Microsoft.Xna.Framework.Vector2(e.x, e.y);
                entity.Parameters = new Parameters(e);
                World.Add(entity);
            }
            return scene;
        }
    }
}
