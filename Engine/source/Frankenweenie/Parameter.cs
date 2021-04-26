using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Parameters
    {
        private Dictionary<string, object> Values;

        public Parameters(OgmoEntity entity)
        {
            Values.Add("width", entity.width);
            Values.Add("height", entity.height);
        }
        
        public T Get<T>(string value)
        {
            if (Values.ContainsKey(value))
                return (T)Values[value];
            throw new Exception("Paremeter does not exist");
        }
    }
}
