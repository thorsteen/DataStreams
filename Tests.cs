using System.Collections.Generic;
using System;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStreams
{
    class Tests
    {
        private HashFunctions hashFunctions;
        private List<ulong> listStream = new List<ulong>();
        private int n, l;
        private int size;

        public Tests(int n, int l)
        {
            this.n = n;
            this.l = l;
            this.size = 1 << this.l;
            hashFunctions = new HashFunctions(
                this.l,
                ulong.Parse("2542203721011"),
                BigInteger.Parse("2542203721011110892402349387"),
                BigInteger.Parse("5542203721011110892402349387"),
                BigInteger.Parse("3542203721012340892402349387"),
                BigInteger.Parse("8542203721043210892402349387")
            );
            IEnumerable<Tuple<ulong, int>> stream = CreateStreams.CreateStream(n, l);
            foreach (var item in stream)
            {
                this.listStream.Add(item.Item1);
            }
        }

        public void TestMultiplyShiftHashingFunction()
        {
            Console.WriteLine("Multiply shift hashing:");
            BigInteger sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in this.listStream)
            {
                //Console.WriteLine(item);
                //Console.WriteLine(hashFunctions.MultiplyShiftHashing(item));
                sum += hashFunctions.MultiplyShiftHashing(item);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void TestMultiplyModPrimeHashingFunction()
        {
            Console.WriteLine("Multiply mod prime hashing:");
            ulong sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in this.listStream)
            {
                //Console.WriteLine(item);
                //Console.WriteLine(hashFunctions.MultiplyModPrimeHashing(item));
                sum += hashFunctions.MultiplyModPrimeHashing(item);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void Test4UHashFunction()
        {
            Console.WriteLine("4 Universal Hashing:");
            ulong sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in this.listStream)
            {
                //Console.WriteLine(item);
                //Console.WriteLine(hashFunctions.FourUniversal(item));
                sum += hashFunctions.FourUniversal(item);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void TestHashTabelPrime()
        {
            HashTabel hash = new HashTabel(size, true, false, hashFunctions);
            int sum = 0;
            List<ulong> keys = new List<ulong>();
            IEnumerable<Tuple<ulong, int>> stream = CreateStreams.CreateStream(this.n, this.l);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in stream)
            {
                hash.Increment(item.Item1, item.Item2);
            }

            for (int i = 0; i < size; i++)
            {
                if (hash.items[i] != null)
                {
                    foreach (Tuple<ulong, int> item in hash.items[i])
                    {
                        sum += item.Item2 * item.Item2;
                    }
                }
            }


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum^2 from hash tabel w/ prime function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void TestHashTabelShift()
        {
            HashTabel hash = new HashTabel(size, false, false, hashFunctions);
            int sum = 0;
            List<ulong> keys = new List<ulong>();
            IEnumerable<Tuple<ulong, int>> stream = CreateStreams.CreateStream(this.n, this.l);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in stream)
            {
                hash.Increment(item.Item1, item.Item2);
            }

            for (int i = 0; i < size; i++)
            {
                if (hash.items[i] != null)
                {
                    foreach (Tuple<ulong, int> item in hash.items[i])
                    {
                        sum += item.Item2 * item.Item2;
                    }
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum^2 from hash tabel w/ shift function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void TestHashTabel4UHash()
        {
            HashTabel hash = new HashTabel(size, false, true, hashFunctions);
            int sum = 0;
            List<ulong> keys = new List<ulong>();
            IEnumerable<Tuple<ulong, int>> stream = CreateStreams.CreateStream(this.n, this.l);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in stream)
            {
                hash.Increment(item.Item1, item.Item2);
            }

            for (int i = 0; i < size; i++)
            {
                if (hash.items[i] != null)
                {
                    foreach (Tuple<ulong, int> item in hash.items[i])
                    {
                        sum += item.Item2 * item.Item2;
                        
                    }
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum^2 from hash tabel w/ four universal function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }
    }
}