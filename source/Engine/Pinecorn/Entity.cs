using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pinecorn
{
    public class Entity
    {
        public List<Component> Components = new List<Component>();
        public Vector2 Position;
        public World World;

        public string Name;

        public int ID;

        public Entity()
        {

        }

        public void Add(Component component)
        {
            component.Entity = this;
            Components.Add(component);
        }
        public void Add<T>() where T: Component
        {
            if(Get<T>() == null)
            {
                Component component = (T)Activator.CreateInstance<T>();
                component.Entity = this;
                Components.Add(component);
            }
        }

        public T Get<T>() where T : Component
        {
            var comp = Components.FirstOrDefault(c => c.GetType() == typeof(T));//--searches the components for a type = to the type that was passed in

            if (comp != null)
            {
                return (T)comp; //returns if the component <T> exists
            }

            return null;//returns null if the entity with the component <T> doesn't exist
        }

        public void RemoveComponenet<T>() where T : Component
        {
            var comp = Components.FirstOrDefault(c => c.GetType() == typeof(T));//searches the components for a type = to the type that was passed in

            if (comp != null)
            {
                Components.Remove(comp); //removes the component if the component <T> exists
            }
#if DEBUG
            else
            {
                throw new Exception("Component does not exist, and therefore cannot be removed!");
            }
#endif


        }
    }
}