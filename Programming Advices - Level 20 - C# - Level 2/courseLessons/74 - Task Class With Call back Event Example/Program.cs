using System;
using System.Threading.Tasks;

public class CustomEventArgs : EventArgs
{
    public int Parameter1 { get; }
    public string Parameter2 { get; }


    public CustomEventArgs(int param1, string param2)
    {
        Parameter1 = param1;
        Parameter2 = param2;
    }
}

class Program
{
    /* 
            Define a costume delegate for the callback
        public delegate void CallbackEventHandler(object sender, CustomEventArgs e);
    */

    // [Define - CREATE] an event based on the delegate
    public static event EventHandler<CustomEventArgs> CallbackEvent;

    static async Task PerformAsyncOperation(EventHandler<CustomEventArgs> callback)
    {
        Console.WriteLine("Doing some other work on PerformAsyncOperation...");

        // Simulate an asynchronous operation
        await Task.Delay(2000);
        Console.WriteLine("[Done]");

        // Create event arguments with two parameters
        callback?.Invoke(null, new CustomEventArgs(42, "Hello from event"));
    }

    // Event handler for the CallbackEvent
    static void OnCallbackReceived(object sender, CustomEventArgs e)
    {
        Console.WriteLine($"Event received: Parameter 1 - {e.Parameter1}, Parameter 2 - {e.Parameter2}");
    }

    static async Task Main()
    {
        // Subscribe to the event
        CallbackEvent += OnCallbackReceived;

        // Create and run a Task for the asynchronous operation, passing CallbackEvent as a parameter
        Task performTask = PerformAsyncOperation(CallbackEvent);

        // Do some other work while waiting for the task to complete
        Console.WriteLine("Doing some other work...");

        // Wait for the task to complete
        await performTask;

        Console.WriteLine("Done!");
        Console.ReadKey();
    }
}
