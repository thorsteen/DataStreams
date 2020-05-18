using System.Collections.Generic;
using System;
using System.Numerics;
using System.Diagnostics;

namespace DataStreams {
    class Tests {

        HashFunctions hashFunctions = new HashFunctions();
        private List<BigInteger> listStream = new List<BigInteger>();
        private int n, l;

        public Tests() {
            this.n = 10000000;
            this.l = 10000;
            IEnumerable <Tuple <ulong , int>> stream = CreateStreams.CreateStream(n, l);
            foreach(var item in stream) {
                this.listStream.Add(item.Item1);
            }
        }
        public void testMultiplyShiftHashingFunction() {
            Console.WriteLine("Multiply shift hashing:");
            BigInteger sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in this.listStream)
            {
                sum += hashFunctions.multiplyShiftHashing(item);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void testMultiplyModPrimeHashingFunction() {
            Console.WriteLine("Multiply mod prime hashing:"); 
            BigInteger sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach(var item in this.listStream){
                sum += hashFunctions.multiplyModPrimeHashing(item);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }
    }
}