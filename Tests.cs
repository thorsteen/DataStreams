using System.Collections.Generic;
using System;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStreams
{
    class Tests
    {
        private  IEnumerable<Tuple<ulong, int>> stream;
        private int n, l;

        public Tests(int n, int l)
        {
            this.n = n;
            this.l = l;
            
            stream = CreateStreams.CreateStream(n, l);
        }

        public void TestRunningTimeOfHashFunction(Func<ulong, int, ulong> hashFunction)
        {
            ulong sum = 0;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in stream)
            {
                sum += hashFunction(item.Item1, l);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("sum from hash function: " + sum);
            Console.WriteLine("Running time: " + ts.ToString());
        }

        public void TestSquraedSums(Func<ulong, int, ulong> hashFunction) {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ulong sum = SquaredSums.CalculatingSquaredSums(stream, hashFunction, l);
            stopWatch.Stop();
            TimeSpan time = stopWatch.Elapsed;
            Console.WriteLine("sum: " + sum);
            Console.WriteLine("Running time: " + time.ToString());
        }

        public void TestCountSketch(){

        }
    }
}