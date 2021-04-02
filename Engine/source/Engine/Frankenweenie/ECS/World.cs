using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenweenie
{
    public class World
    {
        public static Dictionary<int, List<object>> Components = new Dictionary<int, List<object>>();
        public static Dictionary<Type, Bucket> Buckets = new Dictionary<Type, Bucket>();
        public static void Create(int id) => Components.Add(id, new List<object>());
        public static void Destroy(int id) => Components.Remove(id);
        public static void Clear() => Components.Clear();

    }

}