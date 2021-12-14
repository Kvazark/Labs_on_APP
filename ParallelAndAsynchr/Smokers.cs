using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelAndAsynchr
{
    public class Smokers
    {
        private static Mutex Mutex = new Mutex(false);

        static ManualResetEvent tobacco = new ManualResetEvent(false);
        static ManualResetEvent paper = new ManualResetEvent(false);
        static ManualResetEvent matches = new ManualResetEvent(false);

        private static int tobaccoCount = 0;
        private static int paperCount = 0;
        private static int matchesCount = 0;

        public static void Run()
        {
            Thread servant = new Thread(Servant);
            Thread smokersTobacco = new Thread(SmokersTobacco);
            Thread smokersPaper = new Thread(SmokersPaper);
            Thread smokersMatches = new Thread(SmokersMatches);

            
            smokersTobacco.Start();
            smokersPaper.Start();
            smokersMatches.Start();
            servant.Start();
        }

        static void Servant()
        {
            while (true)
            {
                Mutex.WaitOne();
                int random = new Random().Next(1, 11);

                if (random > 1 && random < 5)
                {
                    paperCount++;
                    matchesCount++;
                    Console.WriteLine(
                        $"Слуга взял бумагу и спички и положил их на стол.\nНа столе лежит {paperCount} шт. бумаги и {matchesCount} шт. спичек");
                    Thread.Sleep(new Random().Next(500, 3000));

                    tobacco.Set();
                }
                else if (random >= 5 && random < 8)
                {
                    tobaccoCount++;
                    matchesCount++;
                    Console.WriteLine(
                        $"Слуга взял табак и спички и положил их на стол.\nНа столе лежит {tobaccoCount} шт. табака и {matchesCount} шт. спичек");
                    Thread.Sleep(new Random().Next(500, 3000));

                    paper.Set();
                }
                else if (random >= 8 && random < 11)
                {
                    tobaccoCount++;
                    paperCount++;
                    Console.WriteLine(
                        $"Слуга взял табак и бумагу, положил их на стол.\nНа столе лежит {tobaccoCount} шт. табака и {paperCount} шт. бумаги");
                    Thread.Sleep(new Random().Next(500, 3000));

                    matches.Set();
                }

                Mutex.ReleaseMutex();
            }
            
        }

        static void SmokersTobacco()
        {
            while (true)
            {
                tobacco.WaitOne();
                Mutex.WaitOne();

                Console.WriteLine("Курильщик с табаком начал скручивать сигарету, взяв бумагу и спички со стола.\n");
                paperCount--;
                matchesCount--;

                Mutex.ReleaseMutex();
                tobacco.Reset();

                Thread.Sleep(new Random().Next(100, 1000));
                Console.WriteLine("Курильщик с табаком закурил.\n");
                Thread.Sleep(new Random().Next(1000, 5000));
                Console.WriteLine("Курильщик с табаком выкурил свою сигарету.\n");
               
            }
            
        }

        static void SmokersPaper()
        {
            while (true)
            {
                paper.WaitOne();
                Mutex.WaitOne();
                
                Console.WriteLine("Курильщик с бумагой начал скручивать сигарету, взяв табак и спички со стола.\n");
                tobaccoCount--;
                matchesCount--;
                
                Mutex.ReleaseMutex();
                paper.Reset();
                
                Thread.Sleep(new Random().Next(100, 1000));
                Console.WriteLine("Курильщик с бумагой закурил.\n");
                Thread.Sleep(new Random().Next(1000, 5000));
                Console.WriteLine("Курильщик с бумагой выкурил свою сигарету.\n");
            }
            
        }

        static void SmokersMatches()
        {
            while (true)
            {
                matches.WaitOne();
                Mutex.WaitOne();
                
                tobaccoCount--;
                paperCount--;
                Console.WriteLine("Курильщик сo спичками начал скручивать сигарету, взяв табак и бумагу со стола.\n");
                
                Mutex.ReleaseMutex();
                matches.Reset();
                
                Thread.Sleep(new Random().Next(100, 1000));
                Console.WriteLine("Курильщик со спичками закурил.\n");
                Thread.Sleep(new Random().Next(1000, 5000));
                Console.WriteLine("Курильщик со спичками выкурил свою сигарету.\n");
            }
            
        }
    }
}