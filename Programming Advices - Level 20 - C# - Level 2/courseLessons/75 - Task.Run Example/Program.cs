using System;
using System.Threading;
using System.Threading.Tasks;

class RunMultipleTasks
{
    static void DownloadFile(string TaskName)
    {
        Console.WriteLine($"{TaskName}: Started!");
        Thread.Sleep(5000); // Simulate long-running operation
        Console.WriteLine($"{TaskName}: Completed!");
    }

    static async Task Main(string[] args)
    {
        // Define long-running tasks
        Task[] tasks = {
            Task.Run(() => DownloadFile("Downloading File 1")),
            Task.Run(() => DownloadFile("Downloading File 2"))
        };

        // Wait for both tasks to finishk
        await Task.WhenAll(tasks);

        // Display execution time for each task
        Console.WriteLine($"All Tasks has been completed");
        Console.ReadKey();
    }
}
