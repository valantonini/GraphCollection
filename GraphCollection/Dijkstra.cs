using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GraphCollection
{
    public class Dijkstra<T>
    {
        private readonly List<GraphNode<T>> _graph;
        private IPriorityQueue<GraphNode<T>> _unvistedNodes;
 
        public Dijkstra(IEnumerable<GraphNode<T>> graph)
        {
            _graph = graph.ToList();
        }

        [SuppressMessage("ReSharper", "TooWideLocalVariableScope")]
        public IList<GraphNode<T>> FindShortestPathBetween(GraphNode<T> start, GraphNode<T> finish)
        {
            PrepareGraphForDijkstra();
            start.TentativeDistance = 0;

            //does declaring them here this Save heap allocation in while?
            GraphNode<T> next;
            GraphNode<T> smallest;
            int newTentativeDistance;
            
            var current = start;

            while (true)
            {
                foreach (var neighbour in current.Neighbours.Where(x => !x.GraphNode.Visited))
                {
                    newTentativeDistance = current.TentativeDistance + neighbour.Distance;
                    if (newTentativeDistance < neighbour.GraphNode.TentativeDistance)
                    {
                        neighbour.GraphNode.TentativeDistance = newTentativeDistance;
                    }
                }

                current.Visited = true;

                next = _unvistedNodes.Pop();
                if (next == null || next.TentativeDistance == int.MaxValue)
                {
                    if (finish.TentativeDistance == int.MaxValue)
                    {
                        return new List<GraphNode<T>>();//no path
                    }
                    finish.Visited = true;
                    break;
                }

                smallest = next;
                current = smallest;
            }

            return DeterminePathFromWeightedGraph(start, finish);
        }

        private static List<GraphNode<T>> DeterminePathFromWeightedGraph(GraphNode<T> start, GraphNode<T> finish)
        {
            var current = finish;
            var path = new List<GraphNode<T>> {current};
            var currentTentativeDistance = finish.TentativeDistance;

            while (true)
            {
                if (current == start)
                {
                    break;
                }

                foreach (var neighbour in current.Neighbours.Where(x => x.GraphNode.Visited))
                {
                    if (currentTentativeDistance - neighbour.Distance == neighbour.GraphNode.TentativeDistance)
                    {
                        current = neighbour.GraphNode;
                        path.Add(current);
                        currentTentativeDistance -= neighbour.Distance;
                        break;
                    }
                }
            }
            path.Reverse();
            return path;
        }

        private void PrepareGraphForDijkstra()
        {
            _unvistedNodes = new PriorityQueue<GraphNode<T>>(new CompareNeighbour<T>());
            _graph.ForEach(x =>
            {
                x.Visited = false;
                x.TentativeDistance = int.MaxValue;
                _unvistedNodes.Push(x);
            });
        }
    }

    class CompareNeighbour<T> : IComparer<GraphNode<T>>
    {
        public int Compare(GraphNode<T> x, GraphNode<T> y)
        {
            if (x.TentativeDistance > y.TentativeDistance)
            {
                return 1;
            }
            if (x.TentativeDistance < y.TentativeDistance)
            {
                return -1;
            }
            return 0;
        }
    }
}
