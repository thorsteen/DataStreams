using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace DataStreams
{
    public static class HashFunctions
    {
        private static ulong a_Shift = ulong.Parse("5280936406978265813"); // Odd 64-bit integer
        private static BigInteger a_ModPrime = BigInteger.Parse("293328263200296718983912661"); // 88-bit integer, but less than 2^89 - 1
        private static BigInteger b_ModPrime = BigInteger.Parse("20497230284481308183101738"); // 88-bit integer, but less than  2^89 - 1
        private static int q = 89, b = 89;
        private static BigInteger p = (((BigInteger)1 << q) - 1); //Mersenne prime 2^89 - 1

        /*
            Inputs: key x, and l where 0 < l < 64
        */
        public static ulong MultiplyShiftHashing(ulong x, int l)
        {
            return (a_Shift * x) >> (64 - l);
        }

        public static ulong MultiplyModPrimeHashing(ulong x, int l)
        {
            BigInteger temp;
            BigInteger y;
            temp = a_ModPrime * x + b_ModPrime;
            y = (temp & p) + (temp >> q);
            if (y >= p)
            {
                y -= p;
            }

            return (ulong) (y - ((y >> l) << l));
        }

        public static BigInteger FourUniversalHashing(ulong x, List<BigInteger> list_of_as)
        {
            int q_universal = 4; // Since we are implementing a 4-universal hash function
            BigInteger y = list_of_as[q_universal - 1];

            for (int i = q_universal - 2; i >= 0; i--)
            {
                y = y * x + list_of_as[i];
                y = (y & p) + (y >> b);
            }

            if (y >= p)
            {
                y -= p;
            }
            return y;
        }

        /*
            Inputs: 
                    For t we have that 0 =< t =< 64.
        */ 
        public static Tuple<ulong, int> CountSketchHashfunctions(BigInteger g_x, int t)
        {
            BigInteger h_x = ((g_x >> t) << t);

            BigInteger b_x = g_x >> (b - 1);

            int s_x = 1 - 2 * (int) b_x;

            return Tuple.Create((ulong) h_x, s_x);
        }
    }
}