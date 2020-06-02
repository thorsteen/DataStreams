using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace DataStreams {
    public class CountSketch {

        public static long[] countSketch(IEnumerable<Tuple<ulong, int>> stream, int t, Func<ulong, List<BigInteger>, BigInteger> fourUniverrsal, Func<BigInteger, int, Tuple<ulong, int>> CountSketchHashfunctions, List<BigInteger> list_of_as){
            ulong m = (1UL << t);
            long[] C = new long[m];

            foreach(var item in stream){
                BigInteger g_x = fourUniverrsal(item.Item1, list_of_as);
                Tuple<ulong, int> h_x_and_s_x = CountSketchHashfunctions(g_x, t);
                C[h_x_and_s_x.Item1] = C[h_x_and_s_x.Item1] + h_x_and_s_x.Item2 * item.Item2;
            }
            return C; 
        }


        public static ulong estimate(long[] C){
            ulong X = 0;
            
            for (int y = 0; y < C.Length; y++){
                X += (ulong)(C[y] * C[y]);
            }

            return X;
        }

        public static float meanSquaredError(ulong[] estimates, ulong S){
            ulong sum = 0;
            for(int i = 0; i < 100; i++){
                ulong error = estimates[i] - S;
                sum += error * error;
            }

            return sum / (float) 100;
        }

        public static ulong median(ulong[] estimates){
            Array.Sort(estimates);
            return estimates[5];
        }

    }
}