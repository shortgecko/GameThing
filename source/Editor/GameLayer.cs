using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game.Editor
{
    public class GameLayer
    {
        private static bool Loaded = false;
        private static void Initialize()
        {
           
        }

        public static void Update()
        {
            foreach(Entity Entity in Engine.Scene.World.Entities)
            {
                Gizmo.Position = MouseInput.Position;
                Entity.position = Gizmo.Position;
            }
        }

        public static void Render()
        {
            Gizmo.Render();
        }
        public static void Run()
        {
            if(!Loaded)
            {
                Initialize();
                Loaded = true;
            }

            Update();
            Render();
        }

    }
}
