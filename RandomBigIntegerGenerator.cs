using System;
using System.Numerics;

namespace DataStreams{
    static class RandomBigIntegerGenerator{
        static Random rand = new Random();

        // returns a evenly distributed random BigInteger from 0 to N - 1.
        public static BigInteger randomBigInteger(BigInteger N) {
            BigInteger result = 0;
            do {
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rand.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);
            return result - 1;
        }
    }
}