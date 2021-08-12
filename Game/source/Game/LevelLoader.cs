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
            
            Entity tilemap = Tilemap.Create(tileLayerData, tileLayer.gridCellsX, tileLayer.gridCellsY, new Point(8,8), new Tileset("graphics/tileset.png", 8, 8));
            World.Add(tilemap);
            
            OgmoLayer background = Level["Background"]; ;
            //global::Game.Level.BgTiles = new Tilemap(background.GridToTileLayer(), background.gridCellsX, background.gridCellsY, new Point(8, 8));
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

            //Autotiler.Tile(global::Game.Level.Tiles);
            Autotiler.Tile(tilemap.Get<Tilemap>());
            tileLayer = null;
            tileLayerData = null;
            entityLayer = null;
            background = null;
            triggerLayer = null;
        }
    }
}
