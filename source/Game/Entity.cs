using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class Entity
    {
        public List<Component> Components = new List<Component>();
        public Vector2 position;
        public string Name;

        public int ID;

        public Entity()
        {

        }

        public void add(Component component)
        {
            component.entity = this;
            Components.Add(component);
        }
        public void add<T>() where T: Component
        {
            if(get<T>() == null)
            {
                Component component = (T)Activator.CreateInstance<T>();
                component.entity = this;
                Components.Add(component);
            }
        }

        public T get<T>() where T : Component
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