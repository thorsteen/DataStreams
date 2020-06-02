using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            int n = 10000000;
            int t = 30;

            Tests tests = new Tests(n, t);
            
            Console.WriteLine("Exercise 7:\n");

            // Beregn den eksakte værdi af S for stømmen
            ulong S = tests.TestSquraedSumsValue(HashFunctions.MultiplyShiftHashing);
            
            Console.WriteLine("Exact hashing sum^2 value S: "+S.ToString());
            
            ulong sum_1 = 0;
            ulong sum_2 = 0;
            
            ulong[] estimates  = tests.TestExperimentsWithCountSketch(t);
            
            Console.WriteLine();
            foreach (var estimate in estimates)
            {
                sum_1 += estimate;
                sum_2 += estimate - S;
            }

            double MSE = Math.Pow((int) sum_2, 2) / 100;
            double mu =  sum_1 / 100;
            double var = Math.Cbrt(MSE);
            double S_var = (2 * Math.Pow((int) S, 2)) / (1UL << t);
            
            Console.WriteLine("MSE for Count-Sketch:"+MSE.ToString());
            Console.WriteLine("E[X] = "+mu.ToString());
            Console.WriteLine("Var[X] = "+var.ToString());
            Console.WriteLine("2S^2/m = "+S_var.ToString());
            
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
                Console.WriteLine("Running time: " + stopWatch.Elapsed.ToString());
                
                Console.WriteLine("Hash with chaining with l = "+t.ToString());
                tests.TestSquraedSums(HashFunctions.MultiplyModPrimeHashing);
            }

        }
        
    }
}