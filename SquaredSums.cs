using System;
using System.Collections.Generic;

namespace DataStreams{
    public class SquaredSums{
        public static ulong CalculatingSquaredSums(IEnumerable<Tuple<ulong, int>> stream, Func<ulong, int, ulong> hashFunction, int size) {
            
            HashTabel hash = new HashTabel(size, hashFunction);
            ulong sum = 0;
            
            foreach (var item in stream)
            {
                hash.Increment(item.Item1, item.Item2);
            }

            for (ulong i = 0; i < (1UL << size); i++)
            {
                if (hash.items[i] != null)
                {
                    foreach (Tuple<ulong, int> item in hash.items[i])
                    {
                        sum += (ulong)(item.Item2 * item.Item2);
                    }
                }
            }
            return sum;
        }
    }
}