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
        public Vector2 Position;
        public Registry<Component> Components = new Registry<Component>();

        public void Add<T>() where T : Component, new()
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
        
        public T Get<T>() where T : Component
        {
            return (T)World.All<T>().FirstOrDefault(c => c.Entity == this);
        }

        public bool Contains<T>() where T :Component
        {
            return Get<T>() != null;
        }

        public void Remove<T>() where T : Component
        {
            Components.Remove(Get<T>());
        } 

    }
}
