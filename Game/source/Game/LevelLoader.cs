using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;
using Game.LevelWriter;

namespace Game
{
    public static class LevelLoader
    {
        public static OgmoLevel Level;

        public static void Reload()
        {
            World.Clear();
            OgmoLayer entityLayer = Level["Entities"];

            foreach (OgmoEntity ogmoEntity in entityLayer.entities)
            {
                Vector2 Position = new Vector2(ogmoEntity.x, ogmoEntity.y);
                Factory.Entity(ogmoEntity.name, Position, new Parameters(ogmoEntity));
            }

            OgmoLayer triggerLayer = Level["Triggers"];

            foreach (OgmoEntity ogmoEntity in triggerLayer.entities)
            {
                Entity Entity = Factory.Trigger(ogmoEntity.name, new Point(ogmoEntity.x, ogmoEntity.y), new Parameters(ogmoEntity));
                World.Add(Entity);
            }
        }

        public static void Load(string path)
        {
            World.Clear();
            Level = Content.LoadOgmo($"levels/{path}");
            OgmoLayer tileLayer = Level["Solids"];
            int[] tileLayerData = tileLayer.GridToTileLayer();
            global::Game.Level.Tiles = new Tilemap(tileLayerData, tileLayer.gridCellsX, tileLayer.gridCellsY, new Point(8,8));
            for (int x = 0; x < global::Game.Level.Tiles.Width; x++)
                for (int y = 0; y < global::Game.Level.Tiles.Height; y++)
                {
                    if (global::Game.Level.Tiles[x,y] != -1)
                    {
                        global::Game.Level.Solids.Add(new Hitbox(x * 8, y * 8, 8, 8));
                    }
                }
            
            OgmoLayer background = Level["Background"]; ;
            global::Game.Level.BgTiles = new Tilemap(background.GridToTileLayer(), background.gridCellsX, background.gridCellsY, new Point(8, 8));
            OgmoLayer entityLayer = Level["Entities"];

            foreach (OgmoEntity ogmoEntity in entityLayer.entities)
            {
                Vector2 Position = new Vector2(ogmoEntity.x, ogmoEntity.y);
                Factory.Entity(ogmoEntity.name, Position, new Parameters(ogmoEntity));
            }

            OgmoLayer triggerLayer = Level["Triggers"];

            foreach (OgmoEntity ogmoEntity in triggerLayer.entities)
            {
                Entity Entity = Factory.Trigger(ogmoEntity.name, new Point(ogmoEntity.x, ogmoEntity.y), new Parameters(ogmoEntity));
                World.Add(Entity);
            }

            Autotiler.Tile(global::Game.Level.Tiles);
            tileLayer = null;
            tileLayerData = null;
            entityLayer = null;
            background = null;
            triggerLayer = null;
        }
    }
}
