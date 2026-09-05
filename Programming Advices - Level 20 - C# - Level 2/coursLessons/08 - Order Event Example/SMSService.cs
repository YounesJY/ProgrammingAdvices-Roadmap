using System;

public class SMSService
{
    public void Subscribe(Order order)
    {
        order.OnOrderCreated += HandleNewOrder;
    }

    public void UnSubscribe(Order order)
    {
        order.OnOrderCreated -= HandleNewOrder;
    }

    public void HandleNewOrder(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"------------SMS Service--------");
        Console.WriteLine($"SMS Service Object Received a new order event");
        Console.WriteLine($"Order ID     : {e.OrderID} .");
        Console.WriteLine($"Orider Price : {e.OrderTotalPrice} .");
        Console.WriteLine($"Email        : {e.ClientEmail} .");
        Console.WriteLine($"\nSend SMS");
        Console.WriteLine($"--------------------------------");
        /*
            here you write the code to send the SMS to the client 
        ..................
        */
        Console.WriteLine();
    }
}
