using System.Collections.Generic;
using System;
using System.Numerics;
using System.Diagnostics;

namespace DataStreams {
    class Tests {

        private int n = 20; 
        private int l = 500;

        HashFunctions hashFunctions = new HashFunctions();
        public void testMultiplyShiftHashingFunction() {

            IEnumerator <Tuple <ulong , int>> stream = CreateStreams.CreateStream(n, l).GetEnumerator();
            Console.WriteLine("Multiply shift hashing:");
            BigInteger sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while(stream.MoveNext()) {
                ulong key = stream.Current.Item1;
                sum +=hashFunctions.multiplyShiftHashing((BigInteger)key);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void testMultiplyModPrimeHashingFunction() {
            IEnumerator <Tuple <ulong , int>> stream = CreateStreams.CreateStream(n, l).GetEnumerator();
            Console.WriteLine("Multiply mod prime hashing:"); 
            BigInteger sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while(stream.MoveNext()) {
                ulong key = stream.Current.Item1;
                sum += hashFunctions.multiplyModPrimeHashing((BigInteger)key);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }
    }
}