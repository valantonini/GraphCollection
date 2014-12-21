using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace GraphCollection.Tests
{
    [TestFixture]
    public class TestDijkstra
    {
        [Test]
        public void ShouldFindShortesPathWhen1PathExists()
        {
            //http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
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

            path[0].Value.ShouldBe(1);
            path[1].Value.ShouldBe(3);
            path[2].Value.ShouldBe(6);
            path[3].Value.ShouldBe(5);
        } 
        [Test]
        public void ShouldFindShortesPathWhen2PathExists()
        {
            //2 paths exist
            //O A B D T
            //O A B E D T

            // ReSharper disable InconsistentNaming
            var O = new GraphNode<string>("O");
            var A = new GraphNode<string>("A");
            var B = new GraphNode<string>("B");
            var C = new GraphNode<string>("C");
            var D = new GraphNode<string>("D");
            var E = new GraphNode<string>("E");
            var F = new GraphNode<string>("F");
            var T = new GraphNode<string>("T");
            // ReSharper restore InconsistentNaming

            O.AddNeighbour(A, 2);
            O.AddNeighbour(B, 5);
            O.AddNeighbour(C, 4);

            A.AddNeighbour(F, 12);
            A.AddNeighbour(D, 7);
            A.AddNeighbour(B, 2);

            B.AddNeighbour(D, 4);
            B.AddNeighbour(E, 3);
            B.AddNeighbour(C, 1);

            C.AddNeighbour(E, 4);

            D.AddNeighbour(T, 5);
            D.AddNeighbour(E, 1);

            E.AddNeighbour(T, 7);
            
            F.AddNeighbour(T, 3);

            var graph = new List<GraphNode<string>> { O, A, B, C, D, E, F, T };

            var dijkstra = new Dijkstra<string>(graph);
            var path = dijkstra.FindShortestPathBetween(O, T);


            //The other alternate path
            //path[0].Value.ShouldBe("O");
            //path[1].Value.ShouldBe("A");
            //path[2].Value.ShouldBe("B");
            //path[3].Value.ShouldBe("E");
            //path[4].Value.ShouldBe("D");
            //path[5].Value.ShouldBe("T");

            path[0].Value.ShouldBe("O");
            path[1].Value.ShouldBe("A");
            path[2].Value.ShouldBe("B");
            path[3].Value.ShouldBe("D");
            path[4].Value.ShouldBe("T");

        }

        [Test]
        public void ShouldKnowWhenNoPathEsists()
        {
            var one = new GraphNode<int>(1);
            var two = new GraphNode<int>(2);
            var three = new GraphNode<int>(3);
            var four = new GraphNode<int>(4);
            var five = new GraphNode<int>(5);
            var six = new GraphNode<int>(6);

            var seven = new GraphNode<int>(7);

            one.AddNeighbour(six, 14);
            one.AddNeighbour(three, 9);
            one.AddNeighbour(two, 7);

            two.AddNeighbour(three, 10);
            two.AddNeighbour(four, 15);

            three.AddNeighbour(six, 2);
            three.AddNeighbour(four, 11);

            four.AddNeighbour(five, 6);

            five.AddNeighbour(six, 9);

            var graph = new List<GraphNode<int>> { one, two, three, four, five, six, seven };

            var dijkstra = new Dijkstra<int>(graph);
            var path = dijkstra.FindShortestPathBetween(one, seven);

            path.Count.ShouldBe(0);
        } 
    }
}
