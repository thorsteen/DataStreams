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

        public ulong TestSquraedSums(Func<ulong, int, ulong> hashFunction) {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ulong sum = SquaredSums.CalculatingSquaredSums(stream, hashFunction, l);
            stopWatch.Stop();
            TimeSpan time = stopWatch.Elapsed;
            Console.WriteLine("sum: " + sum);
            Console.WriteLine("Running time: " + time.ToString());
            return sum;
        }

        public ulong[] TestExperimentsWithCountSketch(int t){

            int numberOfExperiments = 100;

            ulong[] estimates = new ulong[numberOfExperiments];

            for(int i = 0; i < numberOfExperiments; i++) {
                BigInteger p = (1UL << 89) - 1UL;
                BigInteger a0 = RandomBigIntegerGenerator.randomBigInteger(p);
                BigInteger a1 = RandomBigIntegerGenerator.randomBigInteger(p);
                BigInteger a2 = RandomBigIntegerGenerator.randomBigInteger(p);
                BigInteger a3 = RandomBigIntegerGenerator.randomBigInteger(p);
                List<BigInteger> list_of_as = new List<BigInteger>(){a0, a1, a2, a3};
                long[] C = CountSketch.countSketch(stream, t, HashFunctions.FourUniversalHashing, 
                                                   HashFunctions.CountSketchHashfunctions, list_of_as);
                estimates[i] = CountSketch.estimate(C);
            }
            
            return estimates;
        }
    }
}