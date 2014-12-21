using System.Collections;
using System.Collections.Generic;

namespace GraphCollection
{
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private readonly List<T> _innerList = new List<T>();
        private readonly IComparer<T> _comparer;

        public int Count
        {
            get { return _innerList.Count; }
        }

        public PriorityQueue()
        {
            _comparer = Comparer<T>.Default;
        }

        public PriorityQueue(IComparer<T> comparer)
        {
            _comparer = comparer;
        }
        
        public void Push(T item)
        {
            _innerList.Add(item);
        }

        public void Sort()
        {
            _innerList.Sort(_comparer);
        }

        public T Pop()
        {
            if (_innerList.Count <= 0)
            {
                return default(T);
            }

            Sort();
            var item = _innerList[0];
            _innerList.RemoveAt(0);
            return item;
        }

        public bool Contains(T item)
        {
            return _innerList.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}