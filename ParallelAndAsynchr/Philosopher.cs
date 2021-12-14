using System;
using System.IO;
using System.Threading;

namespace ParallelAndAsynchr
{
    public class Philosopher
    {
        private static int COUNT=5;
        private int num;
        
        static Semaphore _PhilosopherWait = new Semaphore(2, 2);

        private bool[] _forks;
        
        Thread thread;
        
        public Philosopher(int i,ref bool[] forks)
        {
            num = i;
            _forks = forks;
            thread = new Thread(Eat);
            thread.Start();
        }

        public static void Run()
        {
            bool[] _forks = new bool[] {true, true, true, true, true};
            while (true)
            {
                for (int k = 0; k < COUNT; k++)
                {
                    for (int i = k; i < COUNT; i++)
                    {
                        Philosopher philosopher = new Philosopher(i,ref _forks);
                    }
                    Thread.Sleep(1000);
                }
                
            }
        }

        public void Eat()
        {
            int leftFork = num;
            int rightFork = num - 1;
            if (num == 0)
            {
                rightFork = COUNT - 1;
            }
            
            Console.WriteLine("Philosopher number " + num + " is thinking");

            if (_forks[leftFork] == true && _forks[rightFork] == true)
            {
                _forks[leftFork] = false;
                _forks[rightFork] = false;
                _PhilosopherWait.WaitOne();
                Console.WriteLine("Philosopher number " + num + " is eating");
                
                Thread.Sleep(100);
                Console.WriteLine("Philosopher number " + num + " put forks and begun thinking");
                _PhilosopherWait.Release();
                _forks[leftFork] = true;
                _forks[rightFork] = true;

            }
        }
    }
}