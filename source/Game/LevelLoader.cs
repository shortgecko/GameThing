using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using System;
using Pinecorn;
using System.Collections.Generic;

namespace Game
{
    public class LevelLoader
    {

        public LevelLoader()
        {
            var data = Asset.LoadFile("Levels/Level01.json");
            ParseTilemap(data);
            ParseEntityLayer(data);
        }

        public void ParseEntityLayer(string data)
        {
            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;

            JsonElement layers = root.GetProperty("layers");

            foreach(var layer in layers.EnumerateArray())
            {
                if(layer.GetProperty("name").GetString() == "entity")
                {
                    foreach(var entity in layer.GetProperty("entities").EnumerateArray())
                    {
                        var id = entity.GetProperty("id").GetInt32();

                        var x = entity.GetProperty("x").GetInt32();
                        var y = entity.GetProperty("y").GetInt32();

                        var entt = Factory.Player(Engine.CurrentScene().World);
                        entt.Position = new Vector2(x * 6,y * 6);
                        Engine.CurrentScene().World.AddEntity(entt);
                    }
                }
            }

            
        }

        public void ParseTilemap(string data)
        {
            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;

            int width = root.GetProperty("width").GetInt32() / 8;
            int height =  root.GetProperty("height").GetInt32() / 8;

            var tileData = new int[width * width];

            JsonElement layers = root.GetProperty("layers");

            var tileLayer = layers[0];

            var grid = tileLayer.GetProperty("grid");

            for(int x=0; x < width; x++)
                for(int y = 0; y <height; y++)
                {
                    if(grid[x+y * width].GetString() == "1")
                    {
                        tileData[x+y* width] = 1;
                        Level.Solids.Add(new Hitbox(x * 48, y * 48, 48,48));
                    }
                    else
                        tileData[x+y* width] = -1;

                }

           Level.FG_Tiles = new Tilemap(tileData, width, height);
        }

    }
}