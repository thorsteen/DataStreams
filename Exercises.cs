using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DataStreams{
    public static class Exercises{
        public static void exercise1(){
            int n = 1000000;
            int l = 16;
            Tests tests = new Tests(n, l);

            Console.WriteLine("Exercise 1:\n");

            Console.WriteLine("multiply-shift hashing");
            tests.TestRunningTimeOfHashFunction(HashFunctions.MultiplyShiftHashing);

            Console.WriteLine("\nmultiply-mod-prime hashing");
            tests.TestRunningTimeOfHashFunction(HashFunctions.MultiplyModPrimeHashing);
            
            Console.WriteLine("\n4-Universal hashing");
            tests.TestRunningTimeOf4UniHashFunction();

            Console.WriteLine("\n\n");
        }


        public static void exercise3(){
            int n = 10000000;

            Console.WriteLine("Exercise 3:\n");


            Tests tests;
            
            try{
                for(int l = 1; l < 64; l++) {
                    Console.WriteLine("Using l: " + l + "\n");
                    
                    tests = new Tests(n, l);
                    
                    Console.WriteLine("Multiply-shift hashing:");
                    tests.TestSquraedSums(HashFunctions.MultiplyShiftHashing);
                    
                    Console.WriteLine("\nMultiply-mod-prime hashing:");
                    tests.TestSquraedSums(HashFunctions.MultiplyModPrimeHashing);

                    Console.WriteLine("\n\n");
                }
            } catch(Exception e){
                Console.WriteLine(e);
            }

            Console.WriteLine("\n\n");
        }

        public static void exercise7(){
            int n = 100000;
            int l = 13;
            int t = 10;

            Tests tests = new Tests(n, l);
            
            Console.WriteLine("Exercise 7:\n");
            Console.WriteLine("Running on datastream with 2^"+l.ToString()+" different keys and n = "+n.ToString());
            Console.WriteLine("Count Sketch with t = "+t.ToString());

            ulong S = tests.TestSquraedSums(HashFunctions.MultiplyShiftHashing);
            
            ulong[] estimates  = tests.TestExperimentsWithCountSketch(t);
            ulong[] sortedEstimates = new ulong[estimates.Length];
            Array.Copy(estimates, sortedEstimates, estimates.Length);
            Array.Sort(sortedEstimates);

            using(StreamWriter countSketchEstimatesFile = new StreamWriter("Count-sketch_sorted_estimates.csv", true)){
                for(int i = 0; i < sortedEstimates.Length; i++){
                    countSketchEstimatesFile.WriteLine((i + 1) + ", " + sortedEstimates[i]);
                }
            }

            Console.WriteLine("Mean squared error: " + CountSketch.meanSquaredError(estimates, S).ToString());
            Console.WriteLine("2S^2/m: " + (2*Math.Pow((double) S,2))/(double)(1<<t));
            Console.WriteLine("E[X] = "+ CountSketch.mean(estimates, S).ToString());

            using(StreamWriter mediansFile = new StreamWriter("medians.csv", true)){
                for(int i = 0; i < 9; i++){
                    ulong[] G = new ulong[11];
                    for(int j = 0; j < 11; j++){
                        G[j] = estimates[(i * 11) + j];
                    }
                    mediansFile.WriteLine((i + 1) + ", " + CountSketch.median(G));
                }
            }
            Console.WriteLine("csv files of experiment made");

        }

        public static void exercise8()
        {
            int n = 100000;
            int l = 13;

            List<int> ts = new List<int>(){3,5,10};
            
            Tests tests = new Tests(n, l);
            
            Console.WriteLine("Exercise 8:\n");
            Stopwatch stopWatch = new Stopwatch();
            
            Console.WriteLine("Running on datastream with 2^"+l.ToString()+" different keys and n = "+n.ToString());
            foreach (var t in ts)
            {
                Console.WriteLine("Count Sketch with t = "+t.ToString());
                stopWatch.Reset();
                stopWatch.Start();
                ulong[] estimates  = tests.TestExperimentsWithCountSketch(t);
                stopWatch.Stop();
                Console.WriteLine("Running time: " + (stopWatch.Elapsed/100).ToString());
                Console.WriteLine("Hash with chaining with l = "+l.ToString());
                ulong S = tests.TestSquraedSums(HashFunctions.MultiplyShiftHashing);
                
                Console.WriteLine("Stats");
                Console.WriteLine("------");
                Console.WriteLine("Mean squared error: " + CountSketch.meanSquaredError(estimates, S).ToString());
                Console.WriteLine("2S^2/m: " + (2*Math.Pow((double) S,2))/(double)(1<<t));
                Console.WriteLine("E[X] = "+ CountSketch.mean(estimates, S).ToString());
                
                ulong[] sortedEstimates = new ulong[estimates.Length];
                Array.Copy(estimates, sortedEstimates, estimates.Length);
                Array.Sort(sortedEstimates);
                
                using(StreamWriter countSketchEstimatesFile = new StreamWriter("Count-sketch_sorted_estimates_t"+t.ToString()+".csv", true)){
                    for(int i = 0; i < sortedEstimates.Length; i++){
                        countSketchEstimatesFile.WriteLine((i + 1) + ", " + sortedEstimates[i]);
                    }
                }
                Console.WriteLine("csv files of experiment made \n");
            }

        }
        
    }
}