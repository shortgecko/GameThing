using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Parameters
    {
        private Dictionary<string, object> Values = new Dictionary<string, object>();
        public int Width;
        public int Height;
        public List<Vector2> Nodes = new List<Vector2>();

        public Parameters(OgmoEntity entity)
        {
            Width = entity.width;
            Height = entity.height;

            if(entity.nodes != null)
            {
                foreach (OgmoVector node in entity.nodes)
                {
                    Nodes.Add(new Vector2(node.x, node.y));
                }
            }
        }
        
        public T Get<T>(string value)
        {
            if (Values.ContainsKey(value))
                return (T)Values[value];
            throw new Exception("Paremeter does not exist");
        }
    }
}
