using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphCollection
{
    public class Dijkstra<T>
    {
        private readonly Graph<T> _graph;
        private GraphNode<T> _startNode;
        private List<GraphNode<T>> _unvisited;

        public GraphNode<T> FinishNode { get; set; }

        public GraphNode<T> StartNode
        {
            get { return _startNode; }
            set
            {
                _startNode = value;
                _startNode.Distance = 0;
                _unvisited.Remove(_startNode);
            }
        }

        public bool FinishVisited
        {
            get { return HasBeenVisited(FinishNode); }
        }

        public Dijkstra(Graph<T> graph)
        {
            _graph = graph;
        }
        
        public void MarkAsVisited(GraphNode<T> node)
        {
            node.Visited = true;
            _unvisited.Remove(node);
        }

        public bool HasBeenVisited(GraphNode<T> node)
        {
            return node.Visited;
        }

        public void SetDistance(GraphNode<T> node, int distance)
        {
            node.Distance = distance;
        }

        public IEnumerable<GraphNode<T>> GetUnvisitedNeighbours(GraphNode<T> node)
        {
            return node.Neighbors
                       .Where(x => !x.Visited);
        }

        public void SetDistanceIfSmaller(GraphNode<T> node, int distance)
        {
            if (node.Distance < distance) { return; }
            SetDistance(node, distance);
        }

        public GraphNode<T> GetSmallestDistancedNeighbour(GraphNode<T> node)
        {
            var candidates = node.Neighbors
                                 .Where(x => x.Distance <= node.Distance &&
                                             x != StartNode)
                                 .ToList();

            var smallest = candidates.FirstOrDefault();
            foreach (var graphNode in candidates)
            {
                // ReSharper disable PossibleNullReferenceException
                if (graphNode.Distance < smallest.Distance)
                    smallest = graphNode;
                // ReSharper restore PossibleNullReferenceException
            }

            return smallest;
        }

        public int GetEdgeDistance(GraphNode<T> node1, GraphNode<T> node2)
        {
            return _graph.GetCost(node1, node2);
        }

        public void Reset()
        {
            _unvisited = new List<GraphNode<T>>();

            for (int i = 0; i < _graph.Nodes.Count(); i++)
            {
                var graphNode = _graph.Nodes.ElementAt(i);

                if (graphNode.Neighbors.Count == 0) continue;

                graphNode.Distance = int.MaxValue;
                graphNode.Visited = false;
                _unvisited.Add(graphNode);
            }
        }

        public GraphNode<T> NextSmallest()
        {
            var smallest = _unvisited.FirstOrDefault();
            foreach (var graphNode in _unvisited)
            {
                if (graphNode.Distance < smallest.Distance)
                    smallest = graphNode;
            }
            return smallest;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var distancedNode in _graph.Nodes)
            {
                stringBuilder.AppendLine(String.Format("{0} : distance = {1} visited = {2}", distancedNode.Distance,
                                                       distancedNode.Value, HasBeenVisited(distancedNode)));
            }
            return stringBuilder.ToString();
        }
    }
}