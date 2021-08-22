using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Frankenweenie
{
    public class Serializer
    {
        public static void Serialize<T>(T obj, string filepath)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(filepath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static T Deserialize<T>(string filepath)
        {
            string value = File.ReadAllText(filepath);
            JsonSerializer serializer = new JsonSerializer();
            var output = JsonConvert.DeserializeObject<T>(value);
            if(output != null)
                return output;
            throw new Exception("Could not deserialize file");
        }

        public static T DeserializeString<T>(string input)
        {
            JsonSerializer serializer = new JsonSerializer();
            var output = JsonConvert.DeserializeObject<T>(input);
            if (output != null)
                return output;
            throw new Exception("Could not deserialize string");
        }
    }
}
