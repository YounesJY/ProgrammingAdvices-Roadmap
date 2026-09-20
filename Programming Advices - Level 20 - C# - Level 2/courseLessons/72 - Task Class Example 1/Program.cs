using System;
using System.Threading.Tasks; //The namespace is part of the Task Parallel Library (TPL) 


class Program
{
    static async Task<int> PerformAsyncOperationAsync()
    {
        // Simulate an asynchronous operation
        Console.WriteLine("Doing work on PerformAsyncOperation...");
        Console.WriteLine("Doing work on PerformAsyncOperation...");
        Console.WriteLine("Doing work on PerformAsyncOperation...");

        await Task.Delay(2);
        Console.WriteLine("[DONE] work on PerformAsyncOperation...");
        return 42;
    }
    static async Task Main()
    {
        // Create and run an asynchronous task
        Task<int> resultTask = PerformAsyncOperationAsync();

        // Do some other work while waiting for the task to complete
        for (int i = 0; i < 100; i++)
            Console.WriteLine("[Switch] -> Doing some other work on main...");


        // Wait for the task to complete and retrieve the result
        int result = await resultTask;
        /*
            is await same as join() for threads ? 
        */

        Console.WriteLine($"Result: {result}");
        Console.ReadKey();
    }
}
