using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Frankenweenie;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace Game.Editor
{

    public class EntityData
    {
        public string Name;
        public Vector2 Position;
    }

    public class LevelData
    {
        public string Name = "UNTITLED";
        public int ID;
        public List<EntityData> EntityData = new List<EntityData>();
        public int[,] TileData;

        public static LevelData Load(string path)
        {
            LevelData data;
            var stream = TitleContainer.OpenStream("Assets/Levels/" + path);
            using (StreamReader file = new StreamReader(stream))
            {
                data = JsonConvert.DeserializeObject<LevelData>(file.ReadToEnd());
            }
            stream.Close();
            return data;
        }

        public void Save(string path)
        {
            var filepath = Asset.Path($"Assets/Levels/{path}");
            JsonSerializer serializer = new JsonSerializer();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            File.WriteAllText(filepath, data);
                
        }
    }
}
