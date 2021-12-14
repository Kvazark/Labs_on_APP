using System;
using System.Threading;

namespace ParallelAndAsynchr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Task4();
           
        }

        public static void Task2()
        {
            Philosopher.Run();
        }

        public static void Task4()
        {
           
            Smokers.Run();
            
        }
        
    }
}

