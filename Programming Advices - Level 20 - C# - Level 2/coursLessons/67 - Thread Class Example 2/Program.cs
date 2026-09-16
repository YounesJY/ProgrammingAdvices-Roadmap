using System;
using System.Net;
using System.Threading;

class Program
{
    static void DownloadAndPrint(string url)
    {
        string content;

        using (WebClient client = new WebClient())
        {
            // Simulate some work by adding a delay
            Thread.Sleep(100);

            // Download the content of the web page
            content = client.DownloadString(url);
        }

        Console.WriteLine($"{url}: {content.Length} characters downloaded");
    }
    static void Main()
    {
        /*
            [REMEMBER , Delagate -> Code as paramater]
            that thread already receives a function/delegate as paramter
            and we give it a function, a lambda funtion, an anounymous function
            and this function/code can be anything 
        */

        Thread[] tasks =
        {
            new Thread(() => DownloadAndPrint("https://www.cnn.com")),
            new Thread(() => DownloadAndPrint("https://www.amazon.com")),
            new Thread(() => DownloadAndPrint("https://www.ProgrammingAdvices.com"))
        };

        ThreadStart start = () => DownloadAndPrint("https://www.cnn.com");
        ThreadStart start2 = () => { DownloadAndPrint("https://www.cnn.com"); };
        ThreadStart start3 = delegate () { DownloadAndPrint("https://www.cnn.com"); };
        ThreadStart start4 = new ThreadStart(delegate () { DownloadAndPrint("https://www.cnn.com"); });

        Console.WriteLine("Starting threads...");
        for (int i = 1; i <= tasks.Length; i++)
        {
            tasks[i].Start();
            Console.WriteLine($"Thread {i} started...");
        }

        foreach (Thread task in tasks)
            task.Join();

        Console.WriteLine("\nDone all threads finished execution.");
        Console.ReadKey();
    }
}
