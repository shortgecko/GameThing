using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Frankenweenie
{
    public class Registry<T> : IEnumerable<T>
    { 
        private List<T> toAdd = new List<T>();
        private List<T> active = new List<T>();
        private List<T> toRemove = new List<T>();

        public int Count => active.Count;

        public void Add(T t)
        {
            toAdd.Add(t);
        }

        public void Remove(T t )
        {
            toRemove.Add(t);
        }

        public void Clear()
        {
            foreach(var t in active)
            {
                Remove(t);
            }
        }

        public void Update()
        {
            if(toAdd.Count > 1)
            {
                foreach (var t in toAdd)
                {
                    active.Add(t);
                }

                toAdd.Clear();
            }

            if (toRemove.Count > 1)
            {
                foreach (var t in toRemove)
                {
                    active.Remove(t);
                }

                toRemove.Clear();
            }

        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var a = active.GetEnumerator();
            return a;
        }

    }
}
