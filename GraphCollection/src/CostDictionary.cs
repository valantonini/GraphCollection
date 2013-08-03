using System.Collections;
using System.Collections.Generic;

namespace GraphCollection
{
    public class CostDictionary<T>  : IEnumerable<T>
    {
        readonly Dictionary<T, int> _items = new Dictionary<T, int>();
  
        public int Count
        {
            get { return _items.Count; }
        }

        public void Add(T item, int cost)
        {
            _items.Add(item, cost);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public void AddCost(T item, int cost)
        {
            _items[item] = cost;
        }

        public int CostTo(T item)
        {
            return _items[item];
        }

        public bool Contains(T item)
        {
            return _items.ContainsKey(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _items.Keys).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}