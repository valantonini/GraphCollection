using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphCollection
{
    public class Graph<T> : IEnumerable<T>
    {
        public List<GraphNode<T>> Nodes { get; private set; }

        public Graph() : this(null) { }

        public Graph(List<GraphNode<T>> nodes)
        {
            Nodes = nodes ?? new List<GraphNode<T>>();
        }

        public void AddNode(GraphNode<T> node)
        {
            Nodes.Add(node);
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.AddCost(to, cost);
        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.AddCost(to, cost);
            to.Neighbors.AddCost(@from, cost);
        }

        public int GetCost(GraphNode<T> from, GraphNode<T> to)
        {
            return from.Neighbors.CostTo(to);
        }

        public bool Contains(GraphNode<T> node)
        {
            return Nodes.Contains(node);
        }

        public bool Remove(GraphNode<T> node)
        {
            return Nodes.Remove(node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Nodes.Select(graphNode => graphNode.Value).GetEnumerator();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var node in Nodes)
            {
                stringBuilder.Append(String.Format("Node: {0} Neighbours: ", node));

                foreach (var node1 in node.Neighbors)
                {
                    stringBuilder.Append(node1 + " ");
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
