using System;
using System.Threading;


class Program
{
    static int sharedCounter = 0;
    static object lockObject = new object();

    static void IncrementCounter()
    {
        for (int i = 0; i < 100000; i++)
        {
            // Use lock to synchronize access to the shared counter
            lock (lockObject)
            {
                sharedCounter++;
            }
        }
    }

    static void Main()
    {
        // Create two threads that increment a shared counter
        Thread t1 = new Thread(IncrementCounter);
        Thread t2 = new Thread(IncrementCounter);

        /*
        [REMEMBER] without a proper sync, each thread READS & WRITE a diff value
        */

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Final Counter Value: " + sharedCounter);
        Console.ReadKey();
    }
}