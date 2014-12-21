using System.Linq;

namespace GraphCollection.Tests
{
    internal static class GraphNodeTestExensions
    {
        public static bool IsNeighbourOf<T>(this GraphNode<T> node1, GraphNode<T> node2)
        {
            return node1.Neighbours.Any(n => n.GraphNode == node2);
        }
    }
}