using System;
using System.Collections.Generic;
using System.Numerics;

namespace DataStreams
{

    public class HashTabel
    {
        private readonly int size;
        private readonly bool prime;
        private readonly LinkedList<Tuple<ulong,int>>[] items;
        HashFunctions hashFunctions = new HashFunctions();

        public HashTabel(int size, bool Prime)
        {
            this.size = size;
            items = new LinkedList<Tuple<ulong,int>>[size];
            this.prime = Prime;
        }

        private int GetArrayPosition(ulong key)
        {
            int position;

            if (prime)
            {
                position = hashFunctions.multiplyModPrimeHashing(key) % size;
            }
            else
            {
                position = hashFunctions.multiplyShiftHashing(key) % size;
            }

            return Math.Abs(position);
        }

        public int Find(ulong key)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Item1.Equals(key))
                {
                    return item.Item2;
                }
            }

            return default(int);
        }
        
        public void Set(ulong key, int value)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    Remove(item.Item1);
                    Add(item.Item1,item.Item2);
                }
            }
        }

        public void Add(ulong key, int value)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            Tuple<ulong, int> item = new Tuple<ulong, int>( key, value );
            linkedList.AddLast(item);
        }
        
        public void Increment(ulong key, int d)
        {
            int position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            foreach (Tuple<ulong,int> item in linkedList)
            {
                if (item.Equals(key))
                {
                    Remove(item.Item1);
                    Add(item.Item1,item.Item2+d);
                }
            }
        }

        public void Remove(ulong key)
        {
            int position = GetArrayPosition(key);
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

        protected LinkedList<Tuple<ulong, int>> GetLinkedList(int position)
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