using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Entity
    {
        public int id;
        public Vector2 position;
        public List<Component> Components = new List<Component>();

        public void add<T>() where T : Component
        {
            Component Component = (T)Activator.CreateInstance<T>();
            Component.entity = this;
            Components.Add(Component);
        }

        public void add(Component Component)
        {
            Component.entity = this;
            Components.Add(Component);
        }
        
        public T get<T>() where T : Component => (T)Components.FirstOrDefault(c => c.GetType() == typeof(T));
        
        public void remove<T>() where T : Component => Components.Remove(get<T>());

    }
}

