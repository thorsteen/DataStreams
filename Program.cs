using System;

namespace DataStreams
{
    class Program
    {
        HashFunctions hashFunctions = new HashFunctions();
        static void Main(string[] args)
        {
            Tests tests = new Tests();
            tests.testMultiplyShiftHashingFunction();
            tests.testMultiplyModPrimeHashingFunction();
            
            HashTabel hash = new HashTabel(20);

            hash.Add(1, 22);
            hash.Add(2, 12);
            hash.Add(3, 10);
            hash.Set(1,10);
            hash.Increment(2,1);
            int one = hash.Find(1);
            int two = hash.Find(2);
            int three = hash.Find(3);
            hash.Remove(2);
            Console.WriteLine(one);
            Console.WriteLine(two);
            Console.WriteLine(three);
        }
    }
}
