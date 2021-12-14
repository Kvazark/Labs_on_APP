using System;

namespace ParallelAndAsynchr
{
    public static class ConsoleHelper
    {
        public static object LockObject = new object();
        public static void WriteToConsole(string info)
        {
            lock(LockObject)
            {
                Console.WriteLine(info + " : " );
            }
        }
    }
}