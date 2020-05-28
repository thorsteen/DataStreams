using System;
using System.Collections.Generic;
using System.Numerics;

namespace DataStreams
{

    public class HashTabel
    {
        private readonly int size;
        private readonly bool prime;
        public readonly LinkedList<Tuple<ulong,int>>[] items;
        private HashFunctions hashFunctions;
        private readonly bool FourU;

        public HashTabel(int size, bool Prime, bool FourU, HashFunctions hashFunctions)
        {
            this.size = size;
            items = new LinkedList<Tuple<ulong,int>>[size];
            this.prime = Prime;
            this.FourU = FourU;
            this.hashFunctions = hashFunctions;
        }

        private int GetArrayPosition(ulong key)
        {
            int position;

            if (prime)
            {
                position = (int) (hashFunctions.MultiplyModPrimeHashing(key) % (ulong) size);
            }
            else if (FourU)
            {
                position = (int) (hashFunctions.FourUniversal(key) % (ulong) size);
            }
            else
            {
                position = (int) (hashFunctions.MultiplyShiftHashing(key) % (ulong) size);
            }
            return position;
        }

        public int Get(ulong key)
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
            int foundValue = default;
            bool itemFound = false;
            int position = GetArrayPosition(key);
            LinkedList<Tuple<ulong, int>> linkedList = GetLinkedList(position);
            Tuple<ulong, int> foundItem = default;
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
                foundValue = foundItem.Item2;
                linkedList.Remove(foundItem);
            }
            Add(key,foundValue + d);
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