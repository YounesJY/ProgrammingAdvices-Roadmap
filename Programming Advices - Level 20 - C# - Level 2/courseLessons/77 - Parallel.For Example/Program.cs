using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Define the number of iterations
        int numberOfIterations = 10;

        // Use Parallel.For to execute the loop in parallel
        /*
            ParallelLoopResult parallelLoopResult =  Parallel.For(0, numberOfIterations, (i) =>
            {
                Console.WriteLine($"Executing iteration {i} on thread {Task.CurrentId}");
                // Simulate some work
                Task.Delay(1000).Wait();
            });
            ParallelLoopResult parallelLoopResult =  Parallel.For(0, numberOfIterations, delegate (int i)
            {
                Console.WriteLine($"Executing iteration {i} on thread {Task.CurrentId}");
                // Simulate some work
                Task.Delay(1000).Wait();
            });
        */

        ParallelLoopResult parallelLoopResult = Parallel.For(0, numberOfIterations, NewMethod);
        Console.WriteLine("All iterations completed.");

        /*
        Thread[] threads = new Thread[10];
        for (int i = 0; i < numberOfIterations; i++)
            threads[i] = new Thread(new ThreadStart(() => NewMethod(i)));

        for (int i = 0; i < numberOfIterations; i++)
            threads[i].Start();

        for (int i = 0; i < numberOfIterations; i++)
            threads[i].Join();
        */

        Console.WriteLine("All iterations completed.");
        Console.ReadKey();
    }

    private static void NewMethod(int i)
    {
        Console.WriteLine($@"Executing iteration {i} on thread [{Task.CurrentId} - {Thread.CurrentThread.ManagedThreadId}]");
        // Simulate some work
        Task.Delay(1000).Wait();
    }
}
