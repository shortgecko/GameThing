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
        private List<T> ToAdd;
        private List<T> Active;
        private List<T> ToRemove;

        public List<T> Adding => ToAdd;
        public List<T> Removing => ToRemove;

        public Registry()
        {
            ToAdd = new List<T>();
            Active = new List<T>();
            ToRemove = new List<T>();
        }

        public void Add(T component)
        {
            ToAdd.Add(component);
        }

        public void Remove(T component)
        {
            ToRemove.Add(component);
        }

        public void UpdateList()
        {
            if(ToAdd.Count > 1)
            {
                foreach (T component in ToAdd)
                    Active.Add(component);
                ToAdd.Clear();
            }

            if(ToRemove.Count > 1)
            {
                foreach (T component in ToRemove)
                    Active.Remove(component);
                ToRemove.Clear();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Active.GetEnumerator();
        }

        public List<T> ToList()
        {
            return Active;
        }

        public void Clear()
        {
            foreach (T component in Active)
                Remove(component);
        }

        public int Count
        {
            get
            {
                return Active.Count;
            }
        }

        public T this[int i]
        {
            get
            {
                return Active[i];
            }
        }
    }
}