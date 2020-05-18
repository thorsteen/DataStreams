using System;
using System.Numerics;

namespace DataStreams
{
    
    class HashFunctions
    {

        private BigInteger a1;
        private BigInteger a2;
        private BigInteger b;
        private int l;
        private int q;

        private BigInteger p;
        public HashFunctions(){
            l = 34;
            a1 = BigInteger.Parse("189121246254233103121");
            a2 = BigInteger.Parse("2542203721011110892402349387");
            b = BigInteger.Parse("111421382162224021710128121165");
            q = 89;
            p = ((1 << q) - 1);
        }

        public BigInteger multiplyShiftHashing(BigInteger x){  
            return (a1 * x) >> (64 - l);
        }

        public BigInteger multiplyModPrimeHashing(BigInteger x){
            BigInteger temp;
            BigInteger y;
            temp = a2 * x + b;
            y = (temp & p) + (temp >> q);
            if (y >= p) {
                y -= p;
            }

            return (y - ((y >> l) << l));
        }
    }
}
