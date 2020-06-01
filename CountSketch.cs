using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace DataStreams {
    public class CountSketch {

        public long[] countSketch(IEnumerable<Tuple<ulong, int>> stream, int t, Func<ulong, List<BigInteger>, BigInteger> fourUniverrsal, Func<BigInteger, int, Tuple<ulong, int>> CountSketchHashfunctions, List<BigInteger> list_of_as){
            ulong m = (1UL << t);
            long[] C = new long[m];

            foreach(var item in stream){
                BigInteger g_x = fourUniverrsal(item.Item1, list_of_as);
                Tuple<ulong, int> h_x_and_s_x = CountSketchHashfunctions(g_x, t);
                C[h_x_and_s_x.Item1] = C[h_x_and_s_x.Item1] + h_x_and_s_x.Item2 * item.Item2;
            }
            return C; 
        }


        public ulong estimate(long[] C){
            ulong X = 0;
            
            for (int y = 0; y < C.Length; y++){
                X += (ulong)(C[y] * C[y]);
            }

            return X;
        }

    }
}