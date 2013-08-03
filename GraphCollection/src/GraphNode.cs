namespace GraphCollection
{
    public class GraphNode<T>
    {
        public T Value { get; set; }
        public int Distance { get; set; }
        public bool Visited { get; set; }

        private CostDictionary<GraphNode<T>> _neighbors;
        public CostDictionary<GraphNode<T>> Neighbors
        {
            get { return _neighbors ?? (_neighbors = new CostDictionary<GraphNode<T>>()); }
        }

        public GraphNode(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}