using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Frankenweenie;
using System.Xml;
using System.Xml.Serialization;

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

        public static LevelData Load(string path)
        {
            var stream = TitleContainer.OpenStream(path);
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LevelData));
            var data = (LevelData)serializer.Deserialize(stream);
            stream.Close();
            return data;
        }

        public void Save(string path)
        {
            using (var writer = new System.IO.StreamWriter(Asset.Path(path)))
            {
                var serializer = new XmlSerializer(typeof(LevelData));
                serializer.Serialize(writer, this);
                Logger.Log("[SAVE PATH] " + Asset.Path(path));
                writer.Flush();
            }
        }
    }
}
