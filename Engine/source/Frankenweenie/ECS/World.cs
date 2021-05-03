using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        private static Registry<Entity> Entities = new Registry<Entity>();
        public static Dictionary<Type, List<Component>> ComponentRegistry = new Dictionary<Type, List<Component>>();
        
        public static int Count => Entities.Count;

        public static List<Component> All<T>() where T : Component
        {
            Type t = typeof(T);
            if (!ComponentRegistry.ContainsKey(t))
                ComponentRegistry.Add(t, new List<Component>());
            return ComponentRegistry[t];
        }

        static void AddToComponentRegistry(Component c)
        {
            Type t = c.GetType();
            if(!ComponentRegistry.ContainsKey(t))
            {
                ComponentRegistry.Add(t, new List<Component>());
            }
            ComponentRegistry[t].Add(c);
        }

        static void RemoveFromComponentRegistry(Component c)
        {
            Type t = c.GetType();
            if(ComponentRegistry.ContainsKey(t))
                ComponentRegistry[t].Remove(c);
        }

        public static void Add(Entity Entity)
        {
            Entity.Components.UpdateList();
            foreach (Component c in Entity.Components)
            {
                AddToComponentRegistry(c);
            }

            foreach (Component c in Entity.Components)
            {
                c.Initialize();
            }

            World.Entities.Add(Entity);
        }

        public static void Remove(Entity entity)
        {
            entity.Components.UpdateList();

            foreach (Component c in entity.Components)
            {
                c.Removed();
                Pooler.EntityRemoved(c);
                entity.Components.Remove(c);
                RemoveFromComponentRegistry(c);
            }
            
            World.Entities.Remove(entity);
        }

        public static void Update()
        {
            World.Entities.UpdateList();

            foreach(Entity Entity in Entities)
            {
                Entity.Components.UpdateList();

                foreach(Component Component in Entity.Components)
                {
                    Component.Update();
                }
                
            }

        }

        public static void Render()
        {
            foreach (Entity entity in Entities)
            {
                foreach (Component component in entity.Components)
                {
                    component.Render();
                }

            }

        }

        public static void Clear() => Entities.Clear();
    }

}