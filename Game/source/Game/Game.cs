using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game.Editor;
using Microsoft.Xna.Framework.Content;
using System;
using System.Xml;
using IO = System.IO;


namespace Game
{
    public class Game : Scene
    {
        public float LevelTime;


        public static void Load(string path)
        {
            OgmoLevel level = new OgmoLevel(TitleContainer.OpenStream($"assets/levels/{path}"));
            var tileLayer = level["Solids"];
            var tileLayerData = level.GridToTileLayer(tileLayer);     
            Level.Tiles = new Tilemap(tileLayerData, tileLayer.gridCellsX, tileLayer.gridCellsY);
            for (int x = 0; x < Level.Tiles.Width; x++)
                for (int y = 0; y < Level.Tiles.Height; y++)
                {
                    if (Level.Tiles.Data[x + y * Level.Tiles.Width] > -1)
                        Level.Solids.Add(new Hitbox(x * 8, y * 8, 8, 8));
                }
            var entityLayer = level["Entities"];

            World.Clear();
            foreach(OgmoEntity Entity in entityLayer.entities)
            {
                var entity = EntityManager.Create(Entity.name);
                entity.position = new Vector2(Entity.x, Entity.y);
                var mover = entity.get<Mover>();
                if(mover != null)
                    Level.Actors.Add(mover.Hitbox);
                World.Add(entity);
            }

            level = null;
            tileLayer = null;
            tileLayerData = null;
            entityLayer = null;
          
        }

        protected override void Initialize()
        {
            Engine.RenderTarget.Scale = new Vector2(4f, 4f);
            Engine.Size(320 * 4, 180 * 4);
            if (ImGuiLayer.get<Cmd>() == null)
                ImGuiLayer.add<Cmd>();
            Load("test.json");
            base.Initialize();
        }

        protected override void Load()
        {

        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Render()
        {
            base.Render();
            Level.Tiles.Render();
        }

    }
}
