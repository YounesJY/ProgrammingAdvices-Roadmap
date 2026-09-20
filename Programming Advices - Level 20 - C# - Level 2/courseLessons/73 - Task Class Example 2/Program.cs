using System;
using System.Net;
using System.Threading.Tasks;

class Program
{
    static async Task DownloadAndPrintAsync(string url)
    {
        string content;

        using (WebClient client = new WebClient())
        {
            // Simulate some work by adding a delay
            await Task.Delay(100);

            content = await client.DownloadStringTaskAsync(url);
        }

        Console.WriteLine($"{url}: {content.Length} characters downloaded");
    }

    static async Task Main()
    {
     
        Console.WriteLine("Starting tasks...");

        Task task1 = DownloadAndPrintAsync("https://www.cnn.com");
        Console.WriteLine("Task 1 started...");

        Task task2 = DownloadAndPrintAsync("https://www.amazon.com");
        Console.WriteLine("Task 2 started...");

        Task task3 = DownloadAndPrintAsync("https://www.ProgrammingAdvices.com");
        Console.WriteLine("Task 3 started...\n");

        // Wait for all tasks to complete
        await Task.WhenAll(task1, task2, task3);

        // Print a message indicating that all tasks have finished execution
        Console.WriteLine("\nDone, all tasks finished execution.");
        Console.ReadKey();
    }
}
