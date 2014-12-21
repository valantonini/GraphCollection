using System.Collections.Generic;

namespace GraphCollection
{
    public interface IPriorityQueue<T> : IEnumerable<T>
    {
        void Push(T item);
        T Pop();
        bool Contains(T item);
    }
}
