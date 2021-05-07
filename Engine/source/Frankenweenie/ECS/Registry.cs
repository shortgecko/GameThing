using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private List<T> Components;
        private List<T> ToRemove;

        public List<T> Adding => ToAdd;
        public List<T> Removing => ToRemove;

        public Registry()
        {
            ToAdd = new List<T>();
            Components = new List<T>();
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
                    Components.Add(component);
                ToAdd.Clear();
            }

            if(ToRemove.Count > 1)
            {
                foreach (T component in ToRemove)
                    Components.Remove(component);
                ToRemove.Clear();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        public List<T> ToList()
        {
            return Components;
        }

        public void Clear()
        {
            foreach (T component in Components)
                Remove(component);
            UpdateList();
        }

        public int Count
        {
            get
            {
                return Components.Count;
            }
        }

        public T this[int i]
        {
            get
            {
                return Components[i];
            }
        }
    }
}