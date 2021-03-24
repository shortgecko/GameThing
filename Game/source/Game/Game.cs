using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Editor;

namespace Game
{
    public class Game : Scene
    {
        public float LevelTime;       
        private void Load(string path)
        {
            var data = LevelData.Load(path);
            foreach(EntityData entityData in data.EntityData)
            {
                Entity entity = EntityManager.Create(entityData.Name);
                entity.position = entityData.Position;
                World.Add(entity);
            }
        }
        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(4f, 4f);
            base.Initialize();
        }

    }
}
