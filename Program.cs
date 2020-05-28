using System;

namespace DataStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100000;
            Console.WriteLine("Test for size n ="+n.ToString());
            for (int i = 1; i < 30; i++)
            {
                Console.WriteLine("Size l: "+ i.ToString());
                Tests tests = new Tests(n,i);
                //Console.WriteLine("Hash function tests");
                //tests.TestMultiplyShiftHashingFunction();
                //tests.TestMultiplyModPrimeHashingFunction();
                //tests.Test4UHashFunction();
                Console.WriteLine("HashTabel tests");
                tests.TestHashTabelPrime();
                tests.TestHashTabelShift();
                tests.TestHashTabel4UHash();
            }
        }
    }
}
