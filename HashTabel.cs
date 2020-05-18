using System;
using System.Collections.Generic;

namespace DataStreams
{

    public class HashTabel
    {
        private readonly int size;
        private readonly LinkedList<Tuple<int,int>>[] items;

        public HashTabel(int size)
        {
            this.size = size;
            items = new LinkedList<Tuple<int,int>>[size];
        }

        protected int GetArrayPosition(int key)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }

        public int Find(int key)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<int, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<int,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    return item.Item2;
                }
            }

            return default(int);
        }
        
        public void Set(int key, int value)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<int, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<int,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    Remove(item.Item1);
                    Add(item.Item1,item.Item2);
                }
            }
        }

        public void Add(int key, int value)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<int, int>> linkedList = GetLinkedList(position);
            Tuple<int, int> item = new Tuple<int, int>( key, value );
            linkedList.AddLast(item);
        }
        
        public void Increment(int key, int d)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<int, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<int,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    Remove(item.Item1);
                    Add(item.Item1,item.Item2+d);
                }
            }
        }

        public void Remove(int key)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<int, int>> linkedList = GetLinkedList(position);
            bool itemFound = false;
            Tuple<int, int> foundItem = default(Tuple<int, int>);
            foreach (Tuple<int,int> item in linkedList)
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

        protected LinkedList<Tuple<int, int>> GetLinkedList(int position)
        {
            LinkedList<Tuple<int, int>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<Tuple<int, int>>();
                items[position] = linkedList;
            }

            return linkedList;
        }
    }
}