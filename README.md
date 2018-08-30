**GraphCollection**
===============

[![Build Status](https://travis-ci.org/valantonini/GraphCollection.svg?branch=master)](https://travis-ci.org/valantonini/GraphCollection)

Completely re-written to be simpler and hopefully increase performance. Currently only supports bi-directional nodes.

##Example usage  
How to graph [the example on wikipedia](http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)

        var one = new GraphNode<int>(1);
        var two = new GraphNode<int>(2);
        var three = new GraphNode<int>(3);
        var four = new GraphNode<int>(4);
        var five = new GraphNode<int>(5);
        var six = new GraphNode<int>(6);
        
        one.AddNeighbour(six, 14);
        one.AddNeighbour(three, 9);
        one.AddNeighbour(two, 7);
        
        two.AddNeighbour(three, 10);
        two.AddNeighbour(four, 15);
        
        three.AddNeighbour(six, 2);
        three.AddNeighbour(four, 11);
        
        four.AddNeighbour(five, 6);
        
        five.AddNeighbour(six, 9);
        
        var graph = new List<GraphNode<int>> {one, two, three, four, five, six};
        
        var dijkstra = new Dijkstra<int>(graph);
        var path = dijkstra.FindShortestPathBetween(one, five);

Special thanks to Ikegami Atsuko http://cleo.ci.seikei.ac.jp/~atsuko/

Please visit my blog at http://www.arakawa.asia

