namespace First_Last_List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class FirstLastList<T> : IFirstLastList<T>
        where T : IComparable<T>
    {
        private readonly LinkedList<T> orderOfAppearence;
        private readonly OrderedBag<LinkedListNode<T>> minOrder;
        private readonly OrderedBag<LinkedListNode<T>> maxOreder;

        public FirstLastList()
        {
            this.orderOfAppearence = new LinkedList<T>();
            this.minOrder = new OrderedBag<LinkedListNode<T>>(
                Comparer<LinkedListNode<T>>.Create((node1, node2) => node1.Value.CompareTo(node2.Value)));
            this.maxOreder = new OrderedBag<LinkedListNode<T>>(
                Comparer<LinkedListNode<T>>.Create((node1, node2) => -node1.Value.CompareTo(node2.Value)));
        }

        public int Count
        {
            get
            {
                return this.orderOfAppearence.Count;
            }
        }

        public void Add(T element)
        {
            var node = this.orderOfAppearence.AddLast(element);
            this.minOrder.Add(node);
            this.maxOreder.Add(node);
        }

        public IEnumerable<T> First(int count)
        {
            this.ValidateCount(count);

            return this.orderOfAppearence.Take(count);
        }

        public IEnumerable<T> Last(int count)
        {
            this.ValidateCount(count);

            return this.orderOfAppearence.Reverse().Take(count);
        }

        public IEnumerable<T> Min(int count)
        {
            this.ValidateCount(count);

            return this.minOrder.Select(i => i.Value).Take(count);
        }

        public IEnumerable<T> Max(int count)
        {
            this.ValidateCount(count);

            return this.maxOreder.Select(i => i.Value).Take(count);
        }

        public void Clear()
        {
            this.orderOfAppearence.Clear();
            this.minOrder.Clear();
            this.maxOreder.Clear();
        }

        public int RemoveAll(T element)
        {
            var linkedListNode = new LinkedListNode<T>(element);
            var nodes = this.minOrder.Range(linkedListNode, true, linkedListNode, true);
            var total = nodes.Count;
            foreach (var node in nodes)
            {
                this.orderOfAppearence.Remove(node);
            }

            this.minOrder.RemoveAllCopies(linkedListNode);
            this.maxOreder.RemoveAllCopies(linkedListNode);

            return total;
        }

        private void ValidateCount(int count)
        {
            if (count > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}