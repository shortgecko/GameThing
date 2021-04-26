using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        public static List<Entity> Entities = new List<Entity>();
        private static List<Entity> toRemove = new List<Entity>();
        private static Dictionary<Type, List<Component>> TypeCollection = new Dictionary<Type, List<Component>>();

        public static void AddToTypeCollection(Type t, Component c)
        {

            if (TypeCollection.ContainsKey(t))
                TypeCollection[t].Add(c);
            else
            {
                List<Component> components = new List<Component>();
                components.Add(c);
                TypeCollection.Add(t, components);
            }
        }
        public static List<Component> All<T>() where T : Component
        {
            return TypeCollection[typeof(T)];
        }
        public static void Add(Entity entity)
        {
            for (int i = 0; i < entity.Components.Count; i++)
            {
                var component = entity.Components[i];
                component.Entity = entity;
                component.Initialize();
            }

            World.Entities.Add(entity);
        }

        public static void Remove(Entity entity)
        {
            for (int j = 0; j < entity.Components.Count; j++)
            {
                var component = entity.Components[j];
                component.Removed();
                Pooler.EntityRemoved(component);
            }

            toRemove.Add(entity);
        }

        public static void Update()
        {            
            for (int i = 0; i < World.Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {                 
                    Entities[i].Components[j].Update();
                }
            }
            foreach (Entity e in toRemove)
                Entities.Remove(e);
            if(toRemove.Count > 0)
            {
                toRemove.Clear();
            }
        }

        public static void Render()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {
                    Entities[i].Components[j].Render();
                }
            }
        }

        public static void Clear()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                var entity = Entities[i];
                Remove(entity);
            }
        }
    }

}