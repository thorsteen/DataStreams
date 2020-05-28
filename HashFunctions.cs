using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace DataStreams
{
    public class HashFunctions
    {
        private ulong a0;
        private BigInteger a1;
        private BigInteger a2;
        private BigInteger a3;
        private BigInteger a4;
        private BigInteger b;
        private int l;
        private int q;
        private List<BigInteger> ass;
        private BigInteger m;

        private BigInteger p;

        public HashFunctions(int l, ulong a0, BigInteger a1, BigInteger a2, BigInteger a3, BigInteger a4)
        {
            this.l = l;
            this.m = 2 << l;
            this.a0 = a0;
            this.a1 = a1;
            this.a2 = a2;
            this.a3 = a3;
            this.a4 = a4;
            this.ass = new List<BigInteger> {a1, a2, a3, a4};
            b = BigInteger.Parse("111421382162224021710128121165");
            q = 89;
            p = ((1 << q) - 1);
        }

        public ulong MultiplyShiftHashing(ulong x)
        {
            ulong y = ((a0 * x) >> (64 - l));
            return y;
        }

        public ulong MultiplyModPrimeHashing(ulong x)
        {
            BigInteger temp;
            BigInteger y;
            temp = a2 * x + b;
            y = (temp & p) + (temp >> q);
            if (y >= p)
            {
                y -= p;
            }

            return (ulong) (y - ((y >> l) << l));
        }

        public ulong FourUniversal(ulong x)
        {
            BigInteger y = a1;

            for (int i = 3 - 2; i < 4; i++)
            {
                y = y * x + ass[i];
                y = (y & p) + (y >> q);
            }

            if (y >= p)
            {
                y -= p;
            }

            return (ulong) (y - ((y >> l) << l));
        }

        public Tuple<ulong, ulong> CountSketchHash(ulong x, ulong g_x)
        {
            ulong h_x = g_x >> this.l;

            ulong s_x = 1 - 2 * (g_x / (ulong) m);

            Tuple<ulong, ulong> temp = new Tuple<ulong, ulong>(h_x, s_x);

            return temp;
        }
    }
}