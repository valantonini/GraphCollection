using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace GraphCollection.Tests
{
    [TestFixture]
    public class TestPriorityQueue
    {
        private IPriorityQueue<int> _priorityQueue;
        
        [SetUp]
        public void Setup()
        {
            _priorityQueue = new PriorityQueue<int>();    
        }

        [Test]
        public void ShouldOrderPushedItems()
        {
            _priorityQueue.Push(5);
            _priorityQueue.Push(3);
            _priorityQueue.Push(7);
            _priorityQueue.Push(2);
            _priorityQueue.Push(6);
            _priorityQueue.Push(1);
            _priorityQueue.Push(4);

            _priorityQueue.Pop().ShouldBe(1);
            _priorityQueue.Pop().ShouldBe(2);
            _priorityQueue.Pop().ShouldBe(3);
            _priorityQueue.Pop().ShouldBe(4);
            _priorityQueue.Pop().ShouldBe(5);
            _priorityQueue.Pop().ShouldBe(6);
            _priorityQueue.Pop().ShouldBe(7);
        }

        [Test]
        public void ShouldKnowIfItemExists()
        {
            _priorityQueue.Push(2);
            _priorityQueue.Push(1);
            _priorityQueue.Push(3);

            _priorityQueue.Contains(1).ShouldBe(true);
            _priorityQueue.Contains(2).ShouldBe(true);
            _priorityQueue.Contains(3).ShouldBe(true);
            _priorityQueue.Contains(4).ShouldBe(false);
        }


        [Test]
        public void ShouldMaintainOrderedQueue()
        {
            IPriorityQueue<ReferenceTypeWrapper> priorityQueue = new PriorityQueue<ReferenceTypeWrapper>(new CompareReferenceTypeWrapper());

            var one = new ReferenceTypeWrapper(1);
            var two = new ReferenceTypeWrapper(2);
            var three = new ReferenceTypeWrapper(3);
            var four = new ReferenceTypeWrapper(4);

            priorityQueue.Push(three);
            priorityQueue.Push(two);
            priorityQueue.Push(four);
            priorityQueue.Push(one);

            one.Value = 8;
            two.Value = 7;
            three.Value = 6;
            four.Value = 5;

            priorityQueue.Pop().Value.ShouldBe(5);
            priorityQueue.Pop().Value.ShouldBe(6);
            priorityQueue.Pop().Value.ShouldBe(7);
            priorityQueue.Pop().Value.ShouldBe(8);
        }
    }

    class ReferenceTypeWrapper
    {
        public int Value { get; set; }

        public ReferenceTypeWrapper(int value)
        {
            Value = value;
        } 
    }

    class CompareReferenceTypeWrapper : IComparer<ReferenceTypeWrapper> 
    {
        public int Compare(ReferenceTypeWrapper x, ReferenceTypeWrapper y)
        {
            if (x.Value > y.Value)
            {
                return 1;
            }
            if (x.Value < y.Value)
            {
                return -1;
            }
            return 0;
        }
    }
}
