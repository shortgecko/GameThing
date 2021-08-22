using System;
using System.Collections.Generic;
using System.Reflection;

namespace Frankenweenie
{
    public static class Pooler
    {
        private static Dictionary<Type, Queue<Component>> Pools = new();

        static Pooler()
        {
            foreach (var type in Assembly.GetEntryAssembly().GetTypes())
            {
                if (type.GetCustomAttributes(typeof(Pooled), false).Length > 0)
                {
                    if (!typeof(Component).IsAssignableFrom(type))
                        throw new Exception("Type '" + type.Name + "' cannot be Pooled because it doesn't derive from Component");
                    else if (type.GetConstructor(Type.EmptyTypes) == null)
                        throw new Exception("Type '" + type.Name + "' cannot be Pooled because it doesn't have a parameterless constructor");
                    else
                    {
                        Pools.Add(type, new Queue<Component>(10));
                    }
                }
            }
        }

        public static T Create<T>() where T : Component, new()
        {
            var type = typeof(T);
            if (!Pools.ContainsKey(type))
                return new T();
            var queue = Pools[type];
            if (queue.Count == 0)
                return new T();
            else
            {
                Logger.Log("from pool");
                return queue.Dequeue() as T;
            }

        }

        public static void EntityRemoved(Component c)
        {
            var type = c.GetType();
            if(Pools.ContainsKey(type))
                Pools[type].Enqueue(c);
        }

    }
    public class Pooled : Attribute {}
}
