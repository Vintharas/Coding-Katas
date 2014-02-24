using System;
using System.Runtime.Remoting;
using NUnit.Framework;

namespace ImplementSimplyLinkedList
{

    /*
     *  Implement a Simply Linked List, i.e., a list of interconnected nodes where each node has a reference to the next node
     *  and where the head and tail nodes are known.
     * 
     *      - Linked list nodes
     *      - Head/Tail nodes
     *      - Add items (front and end)
     *      - Remove items (front, end, by value)
     *      - Enumerate through the linked list
     *      - Spatial and runtime complexity?
     *      - What is a LinkedList bad/good for?
     * 
     * 
     */


    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public LinkedList()
        {
            
        }

        public LinkedList(params T[] initialItems)
        {
            foreach (var initialItem in initialItems)
                AddFirst(initialItem);
        }

        public void AddFirst(T value)
        {
            if (IsEmpty())
            {
                Head = new Node<T> { Value = value };
                Tail = Head;
            }
            else
            {
                var newFirstNode = new Node<T>
                                        {
                                            Value = value,
                                            Next = Tail
                                        };
                Head = newFirstNode;
            }
        }

        private bool IsEmpty()
        {
            return Head == null;
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; } 
    }


    [TestFixture]
    public class LinkedListTests
    {

        [Test]
        public void Head_WhenHavingAnEmptyList_ShouldBeNull() // The Head and Tail nodes are probably an implementation detail :)
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            // Act
            var head = linkedList.Head;
            // Assert
            Assert.That(head, Is.Null);
        }

        [Test]
        public void Tail_WhenHavingAnEmptyList_ShouldBeNull()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            // Act
            var tail = linkedList.Tail;
            // Assert
            Assert.That(tail, Is.Null);
        }

        [Test]
        public void AddFirst_WhenHavingAnEmptyList_ShouldAddANodeAtTheBeginningOftheLinkedList()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            // Act
            linkedList.AddFirst(1);
            // Assert
            Assert.That(linkedList.Head.Value, Is.EqualTo(1));
            Assert.That(linkedList.Tail.Value, Is.EqualTo(1));
            Assert.That(linkedList.Head == linkedList.Tail);
        }

        [Test]
        public void AddFirst_WhenHavingAListWithAnItem_ShouldAddANodeAtTheBeginningOfTheLinkedList()
        {
            // Arrange
            var linkedList = new LinkedList<int>(1);
            // Act
            linkedList.AddFirst(2);
            // Assert
            Assert.That(linkedList.Head.Value, Is.EqualTo(2));
            Assert.That(linkedList.Tail.Value, Is.EqualTo(1));
        }

        [Test]
        public void AddFirst_WhenHavingAListWithSeveralItems_ShouldAddANodeAtTheBeginningOfTheLinkedList()
        {
            // Arrange
            var linkedList = new LinkedList<int>(1, 2, 3, 4);
            // Act
            linkedList.AddFirst(10);
            // Assert
            Assert.That(linkedList.Head.Value, Is.EqualTo(10));
            Assert.That(linkedList.Tail.Value, Is.EqualTo(1));
            // this test is a false positive :)
        }




        
    }
}
