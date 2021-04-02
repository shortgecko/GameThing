using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public static class Entity
    {  
        public static void add(int id, object component)
        {
            var components = World.Components[id];
            components.Add(component);
            Type type = component.GetType();
            addToBucket(type, id);
        }
        public static void add<T>(int id) where T : struct
        {
            var component = (T)Activator.CreateInstance<T>();
            var components = World.Components[id];
            components.Add(component);
            Type type = component.GetType();
            addToBucket(type, id);
        }
        private static void addToBucket(Type type, int id)
        {

            if (World.Buckets.ContainsKey(type))
            {
                World.Buckets[type].Entities.Add(id);
            }
            else
            {
                Bucket bucket = new Bucket();
                bucket.Type = type;
                bucket.Entities.Add(id);
                World.Buckets.Add(type, bucket);
            }
        }
        public static T get<T>(int id) where T : struct
        {
            return (T)World.Components[id].FirstOrDefault(c => c.GetType() == typeof(T));
        }
        public static void remove<T>(int id) where T : struct
        {
            var component = get<T>(id);
            World.Components[id].Remove(component);
        }
    }
}

