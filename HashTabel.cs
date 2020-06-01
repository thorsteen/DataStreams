using System;
using System.Collections.Generic;

namespace DataStreams
{

    public class HashTabel
    {
        private readonly int size;
        public readonly LinkedList<Tuple<ulong,int>>[] items;
        private Func<ulong, int, ulong> hashFunction;

        public HashTabel(int size, Func<ulong, int, ulong> hashFunction)
        {
            this.size = size;
            items = new LinkedList<Tuple<ulong,int>>[(1UL << size)];
            this.hashFunction = hashFunction;
        }

        private ulong GetArrayPosition(ulong key)
        {
            return hashFunction(key, this.size);
        }

        public int Get(ulong key)
        {
            ulong position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Item1.Equals(key))
                {
                    return item.Item2;
                }
            }
            return default;
        }
        
        public void Set(ulong key, int value)
        {
            ulong position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    Remove(item.Item1);
                    Add(item.Item1, item.Item2);
                }
            }
        }

        public void Add(ulong key, int value)
        {
            ulong position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            Tuple<ulong, int> item = new Tuple<ulong, int>( key, value );
            linkedList.AddLast(item);
        }
        
        public void Increment(ulong key, int d)
        {
            ulong position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            
            bool itemFound = false;
            Tuple<ulong, int> foundItem = default;

            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Item1.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            int value = default;
            if (itemFound)
            {
                value = foundItem.Item2;
                linkedList.Remove(foundItem);
            }
            Add(key, value + d);
        }

        public void Remove(ulong key)
        {
            ulong position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            Tuple<ulong, int> foundItem = default(Tuple<ulong, int>);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            if (itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }

        protected LinkedList<Tuple<ulong, int>> GetLinkedList(ulong position)
        {
            LinkedList<Tuple<ulong, int>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<Tuple<ulong, int>>();
                items[position] = linkedList;
            }

            return linkedList;
        }
    }
}