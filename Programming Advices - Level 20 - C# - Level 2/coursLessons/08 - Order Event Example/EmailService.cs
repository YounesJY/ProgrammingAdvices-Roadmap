using System;

public class EmailService
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
        Console.WriteLine($"----------Email Service--------");
        Console.WriteLine($"Email Service Object Received a new order event");
        Console.WriteLine($"Order ID     : {e.OrderID} .");
        Console.WriteLine($"Orider Price : {e.OrderTotalPrice} .");
        Console.WriteLine($"Email        : {e.ClientEmail} .");
        Console.WriteLine($"\nSend an email");
        Console.WriteLine($"-------------------------------");
        /*
            here you write the code to send the email to the client 
        ................
        */
        Console.WriteLine();
    }
}
