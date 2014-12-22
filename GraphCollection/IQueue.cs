namespace GraphCollection
{
    public interface IPriorityQueue<T>
    {
        void Push(T item);
        T Pop();
        bool Contains(T item);
    }
}
