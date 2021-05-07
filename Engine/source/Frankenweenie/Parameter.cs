using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Parameters
    {
        public int Height;
        public int Width;
        private Dictionary<string, object> Values = new Dictionary<string, object>();

        public Parameters(OgmoEntity Entity)
        {
            Width = Entity.width;
            Height = Entity.height;
        }
        
        public T Get<T>(string value)
        {
            if (Values.ContainsKey(value))
                return (T)Values[value];
            throw new Exception("Paremeter does not exist");
        }
    }
}
