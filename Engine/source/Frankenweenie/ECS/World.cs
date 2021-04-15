using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        public static List<Entity> Entities = new List<Entity>();
        //public static Dictionary<Type, Bucket> Buckets = new Dictionary<Type, Bucket>();

        public static void Add(Entity entity)
        {
            for (int i = 0; i < entity.Components.Count; i++)
            {
                var component = entity.Components[i];
                component.entity = entity;
                component.Initialize();
            }

            World.Entities.Add(entity);
        }

        public static void Remove(Entity entity) => World.Entities.Remove(entity);

        public static void Update()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = 0; j < Entities[i].Components.Count; j++)
                {
                    Entities[i].Components[j].Update();
                }
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

        public static void Clear() => World.Entities.Clear();
    }

}