using Microsoft.Xna.Framework;
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

        public T Add<T>() where T : Component, new()
        {
            Component Component = Pooler.Create<T>();
            Add(Component);
            return (T)Component;
        }

        public void Add(Component Component)
        { 
            Component.Entity = this;
            Components.Add(Component);
        }
        
        public T Get<T>() where T : Component
        {
            var components = World.All<T>();
            foreach(Component component in components)
            {
                if (component.Entity == this)
                {
                    return (T)component;
                }
            }
            return null;
        }

        public bool Contains<T>() where T :Component
        {
            return Get<T>() != null;
        }

        public void Remove<T>() where T : Component
        {
            Component component = Get<T>();
            Remove(component);
        }

        public void Remove(Component component)
        {          
            Components.Remove(component);
        }

        public void Clear()
        {
            foreach (Component component in Components)
                Remove(component);
        }

    }
}
