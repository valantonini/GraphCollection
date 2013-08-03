using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace GraphCollection.test
{
    /**
     * Test based on graph at:
     * http://en.wikipedia.org/wiki/Pather's_algorithm
     * 
     * Added node 7 connected to 4 with a value of 2 for an additional test
     **/

    [TestFixture]
    public class TestGraph
    {
        private Graph<int> _graph;

        // ReSharper disable InconsistentNaming
        readonly GraphNode<int> one = new GraphNode<int>(1);
        readonly GraphNode<int> two = new GraphNode<int>(2);
        readonly GraphNode<int> three = new GraphNode<int>(3);
        readonly GraphNode<int> four = new GraphNode<int>(4);
        readonly GraphNode<int> five = new GraphNode<int>(5);
        readonly GraphNode<int> six = new GraphNode<int>(6);
        readonly GraphNode<int> seven = new GraphNode<int>(7);
        // ReSharper restore InconsistentNaming

        [SetUp]
        public void SetUp()
        {
            _graph = new Graph<int>();
            

            _graph.AddNode(one);
            _graph.AddNode(two);
            _graph.AddNode(three);
            _graph.AddNode(four);
            _graph.AddNode(five);
            _graph.AddNode(six);
            _graph.AddNode(seven);

            _graph.AddUndirectedEdge(one, two, 7);
            _graph.AddUndirectedEdge(one, three, 9);
            _graph.AddUndirectedEdge(one, six, 14);

            _graph.AddUndirectedEdge(two, three, 10);
            _graph.AddUndirectedEdge(two, four, 15);


            _graph.AddUndirectedEdge(three, four, 11);
            _graph.AddUndirectedEdge(three, six, 2);

            _graph.AddUndirectedEdge(four, five, 6);
            _graph.AddUndirectedEdge(four, seven, 2);

            _graph.AddUndirectedEdge(five, six, 9);
        }
        
        [Test]
        public void ShouldGetDistance()
        {
            _graph.GetCost(two, four).ShouldBe(15);
            _graph.GetCost(five, six).ShouldBe(9);
        }

        [Test]
        public void ShouldRemove()
        {
            _graph.Remove(one).ShouldBe(true);
            _graph.Contains(one).ShouldBe(false);
        }

        [Test]
         public void ShouldFindShortestPath1()
        {
            var pather = new Pather<int>(_graph);
            var path = pather.CalculateShortesPath(one, five).ToArray();
            
            path[0].ShouldBe(one);
            path[1].ShouldBe(two);
            path[2].ShouldBe(three);
            path[3].ShouldBe(six);
            path[4].ShouldBe(five);
        }

        [Test]
        public void ShouldFindShortestPath2()
        {
            var pather = new Pather<int>(_graph);
            var path = pather.CalculateShortesPath(one, seven).ToArray();
            
            path[0].ShouldBe(one);
            path[1].ShouldBe(two);
            path[2].ShouldBe(four);
            path[3].ShouldBe(seven);
        }

        [Test]
        public void ShouldFindShortestPath3()
        {
            var pather = new Pather<int>(_graph);
            var path = pather.CalculateShortesPath(six, seven).ToArray();
            
            path[0].ShouldBe(six);
            path[1].ShouldBe(three);
            path[2].ShouldBe(four);
            path[3].ShouldBe(seven);
        }

        [Test]
        public void ShouldDetectNoPath()
        {
            var nine = new GraphNode<int>(9);
            _graph.AddNode(nine);

            var pather = new Pather<int>(_graph);
            var path = pather.CalculateShortesPath(one, nine).ToArray();
            
            path.Count().ShouldBe(0);
        }

        [Test]
        public void ShouldPathToNeighbour()
        {
            var pather = new Pather<int>(_graph);
            var path = pather.CalculateShortesPath(one, two).ToArray();
           
            path.Count().ShouldBe(2);
            
            path[0].ShouldBe(one);
            path[1].ShouldBe(two);
        }
    }
}
