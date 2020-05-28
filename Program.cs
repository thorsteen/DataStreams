using System;

namespace DataStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 30; i++)
            {
                Tests tests = new Tests(1000000,i);
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
