using NUnit.Framework;
using Shouldly;

namespace GraphCollection.Tests
{
    [TestFixture]
    public class TestGraphNode
    {
        [Test]
        public void ShouldAddNodes()
        {
            var node1 = new GraphNode<string>("one");
            var node2 = new GraphNode<string>("two");
            var node3 = new GraphNode<string>("three");

            node1.AddNeighbour(node2, 2);
            node2.AddNeighbour(node3, 4);

            node1.IsNeighbourOf(node2).ShouldBe(true);
            node2.IsNeighbourOf(node1).ShouldBe(true);
            node2.IsNeighbourOf(node3).ShouldBe(true);
            node1.IsNeighbourOf(node3).ShouldBe(false);
            node3.IsNeighbourOf(node1).ShouldBe(false);
        }
    }
}
