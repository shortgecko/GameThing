using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Frankenweenie
{
    public static class EntityInstancer
    {
        private static Dictionary<string, Type> Types;

        static EntityInstancer()
        {
            Types = new Dictionary<string, Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Entity)))
                {
                    Types.Add(type.Name, type);
                }
            }
        }

        public static Entity Get(string name)
        {
            Type type = Types[name];
            Entity entity = (Entity)Activator.CreateInstance(type);
            return entity;
        }
    }
}
