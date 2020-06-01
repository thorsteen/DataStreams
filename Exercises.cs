using System;

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
            int n = 1000000;
            int l = 30;

            Tests tests = new Tests(n, l);
            
            Console.WriteLine("Exercise 7:\n");

            // Beregn den eksakte værdi af S for stømmen
            Console.WriteLine("Multiply-shift hashing:");
            tests.TestSquraedSums(HashFunctions.MultiplyShiftHashing);

            for(int i = 0; i < 100; i++){

            }


        }


    }
}