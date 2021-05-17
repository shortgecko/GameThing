using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Frankenweenie
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "External JSON format")]

    public struct OgmoVector
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "External JSON format")]
    public class OgmoEntity
    {
        public string name { get; set; }
        public int id { get; set; }
        public string _eid { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int originX { get; set; }
        public int originY { get; set; }
        public OgmoVector[] nodes { get; set; }

    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "External JSON format")]
    public class OgmoLayer
    {
        public string name { get; set; }
        public string _eid { get; set; }
        public int offsetX { get; set; }
        public int offsetY { get; set; }
        public int gridCellWidth { get; set; }
        public int gridCellHeight { get; set; }
        public int gridCellsX { get; set; }
        public int gridCellsY { get; set; }
        public string tileset { get; set; }
        public int[] data { get; set; }
        public int exportMode { get; set; }
        public int arrayMode { get; set; }
        public string[] grid { get; set; }
        public OgmoEntity[] entities { get; set; }

        public int[] GridToTileLayer()
        {
            var layer = this;
            int[] data = new int[layer.grid.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (layer.grid[i] != "0")
                    data[i] = int.Parse(layer.grid[i]);
                else
                    data[i] = -1;

            }

            return data;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "External JSON format")]
    public class OgmoLevelData
    {
        public string ogmoVersion { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int offsetX { get; set; }
        public int offsetY { get; set; }
        public Dictionary<string, string> values { get; set; }
        public List<OgmoLayer> layers { get; set; }
    }

    public class OgmoLevel
    {
        public OgmoLevelData Data { get; set; }
        public Vector2 LevelSize { get; protected set; }
        public Vector2 TileSize { get; protected set; }
        public Vector2 LevelPixelSize { get; protected set; }

        public OgmoLevel(Stream fs)
        {

            using StreamReader streamReader = new StreamReader(fs);
            using JsonTextReader jsonTextReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();
            Data = serializer.Deserialize<OgmoLevelData>(jsonTextReader);
            OgmoLayer firstLayer = Data.layers[0];
            LevelSize = new Vector2(firstLayer.gridCellsX, firstLayer.gridCellsY);
            TileSize = new Vector2(firstLayer.gridCellWidth, firstLayer.gridCellHeight);
            LevelPixelSize = LevelSize * TileSize;
        }

        public OgmoLayer this[string name]
        {
            get
            {
                foreach (OgmoLayer layer in Data.layers)
                {
                    if (layer.name == name)
                        return layer;
                }

                throw new Exception($"Layer {name} does not exist!");
            }
        }


    }
}