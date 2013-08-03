using System.Collections.Generic;

namespace GraphCollection
{
    public class Pather<T>
    {
        private readonly Dijkstra<T> _dijkstra;

        public Pather(Graph<T> graph)
        {
            _dijkstra = new Dijkstra<T>(graph);
        }

        public IEnumerable<GraphNode<T>> CalculateShortesPath(GraphNode<T> start, GraphNode<T> finish)
        {
            _dijkstra.Reset();
            _dijkstra.StartNode = start;
            _dijkstra.FinishNode = finish;
            CalculateNeighboursDistanceFor(start);
            return DeterminePath(start, finish);
        }

        private void CalculateNeighboursDistanceFor(GraphNode<T> current)
        {
            foreach (var neighbour in _dijkstra.GetUnvisitedNeighbours(current))
            {
                var tentativeDistance = _dijkstra.GetEdgeDistance(current, neighbour) + current.Distance;

                _dijkstra.SetDistanceIfSmaller(neighbour, tentativeDistance);
            }

            _dijkstra.MarkAsVisited(current);

            if (_dijkstra.FinishVisited) return;

            var nextSmallest = _dijkstra.NextSmallest();
            
            if(nextSmallest!= null)
                CalculateNeighboursDistanceFor(nextSmallest);
        }

        private IEnumerable<GraphNode<T>> DeterminePath(GraphNode<T> start, GraphNode<T> finish)
        {
            var path = new List<GraphNode<T>> {finish};
            
            var next = _dijkstra.GetSmallestDistancedNeighbour(finish);
            while (next != default(GraphNode<T>))
            {
                path.Add(next);
                next = _dijkstra.GetSmallestDistancedNeighbour(next);
            }

            path.Add(start);

            if(!IsValid(path)) return new List<GraphNode<T>>();

            path.Reverse();
            return path;
        }

        public bool IsValid(List<GraphNode<T>> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                if (!path[i].Neighbors.Contains(path[i + 1])) return false;
            }

            return true;
        }
    }
}