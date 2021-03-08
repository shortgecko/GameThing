using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pinecorn;
using Microsoft.Xna.Framework;

namespace Game
{
    public class LevelLoader
    {
        public OgmoLevel OgmoLevel;

        public LevelLoader(string filepath)
        {
            string realPath = Asset.TitlePath(filepath);
            OgmoLevel = new OgmoLevel(File.OpenRead(realPath));
            GetEntityLayer("entity");
            Level.FG_Tiles = GetTilemap("fg_tiles");
        }

        public Tilemap GetTilemap(string tilemap)
        {
            var layer = OgmoLevel.Data.Get(tilemap);
            var data = gridToInt( layer.grid);
            for(int x = 0;x < layer.gridCellsX; x++)
                for(int y = 0; y < layer.gridCellsY; y++)
                {
                    if(data[x+y * layer.gridCellsX] > -1)
                    {
                        Level.Solids.Add(new Hitbox(x * 8, y * 8, 8,8));
                    }
                }
            return new Tilemap(data, layer.gridCellsX, layer.gridCellsY);
        }
    
        public int[] gridToInt(string[] grid)
        {
            int[] data = new int[grid.Length];
            for(int i = 0; i < grid.Length; i++)
            {
                if(grid[i] == "0")
                {
                    data[i] = -1;
                }
                else
                {
                    data[i] = 1;
                }
            }
            return data;
    }

        public void GetEntityLayer(string entitylayer)
        {
            var layer = OgmoLevel.Data.Get(entitylayer);
            foreach(OgmoEntity entity in layer.entities)
            {
                Engine.CurrentScene().World.Entities.Add(EntityManager.Create(entity.name,new Vector2(entity.x, entity.y), entity.width, entity.height,entity.values));
            }
        }
    }
}