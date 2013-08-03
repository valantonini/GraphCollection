GraphCollection
===============

A graph collection with Dijkstra algorithm for C#. See my blog for more information http://www.arakawa.asia/graphs-and-pathing-in-csharp/

Graph built on the information based on the information @ http://msdn.microsoft.com/en-us/library/ms379574(v=vs.80).aspx
Dijkstra's algorithm based on the wikipedia entry @ http://en.wikipedia.org/wiki/Dijkstra's_algorithm
Special thanks to Ikegami Atsuko http://cleo.ci.seikei.ac.jp/~atsuko/

Example usage (to graph the image @ http://en.wikipedia.org/wiki/Dijkstra's_algorithm and perform the shortest path example from node 1 to node 5):

Graph<int> _graph = new Graph<int>();

var one = new GraphNode<int>(1);
var two = new GraphNode<int>(2);
var three = new GraphNode<int>(3);
var four = new GraphNode<int>(4);
var five = new GraphNode<int>(5);
var six = new GraphNode<int>(6);

_graph.AddNode(one);
_graph.AddNode(two);
_graph.AddNode(three);
_graph.AddNode(four);
_graph.AddNode(five);
_graph.AddNode(six);

_graph.AddUndirectedEdge(one, two, 7);
_graph.AddUndirectedEdge(one, three, 9);
_graph.AddUndirectedEdge(one, six, 14);

_graph.AddUndirectedEdge(two, three, 10);
_graph.AddUndirectedEdge(two, four, 15);


_graph.AddUndirectedEdge(three, four, 11);
_graph.AddUndirectedEdge(three, six, 2);

_graph.AddUndirectedEdge(four, five, 6);

_graph.AddUndirectedEdge(five, six, 9);


var pather = new Pather<int>(_graph);
var path = pather.CalculateShortesPath(one, five).ToArray();