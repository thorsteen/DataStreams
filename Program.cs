using System;

namespace DataStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            Tests tests = new Tests();
            tests.testMultiplyShiftHashingFunction();
            tests.testMultiplyModPrimeHashingFunction();
        }
    }
}
