using System;

namespace DataStreams
{
    class HashFunctions
    {
        
        public ulong multiShiftHashing(ulong x){  
            // We generate "a" as a random odd uint64 number.
            Random  rnd = new  Random();
            ulong a = 0UL;
            Byte[] b = new  Byte [8];
            rnd.NextBytes(b);
            for(int i = 0; i < 7; ++i) {
                a = (a << 8) + (ulong)b[i];
            }
            a = (a << 8) + ((ulong)b[7] | 1);

            // We generate "l" to be a positive integer less than 64.
            int l = rnd.Next(1, 64);

            return (a*x) >> (64-l);
        }

         public ulong muliplyModPrimeHashing(ulong x){  
            
            ulong p = (ulong)Math.Pow(2,89) - 1; 

            Random  rnd = new  System.Random ();

            // We generate "a" as a random. We have for a that: 0 <= a < p
            ulong a = 0UL;
            Byte[] rndBytes = new  Byte [11];
            rnd.NextBytes(rndBytes);
            for(int i = 0; i < 11; ++i) {
                a = (a << 8) + (ulong)rndBytes[i];
            }

            // We generate "b" as a random. We have for b that: 0 <= b < p
            ulong b = 0UL;
            rnd.NextBytes(rndBytes);
            for(int i = 0; i < 11; ++i) {
                b = (a << 8) + (ulong)rndBytes[i];
            }

            // We generate "l" to be a positive integer less than 64.
            int l = rnd.Next(1, 64);
        
            // we need to use exercise 2.7 and 2.8
            return ((a * x + b) % p) % (ulong)Math.Pow(2, l);

        }
    }
}
