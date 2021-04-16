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
        public Vector2 Position;
        public List<Component> Components = new List<Component>();

        public void add<T>() where T : Component, new()
        {
            Component Component = Pooler.Create<T>();
            Component.Entity = this;
            Components.Add(Component);
        }

        public void Add(Component Component)
        { 
            Component.Entity = this;
            Components.Add(Component);
        }
        
        public T Get<T>() where T : Component => (T)Components.FirstOrDefault(c => c.GetType() == typeof(T));
        
        public void remove<T>() where T : Component => Components.Remove(Get<T>());

    }
}

