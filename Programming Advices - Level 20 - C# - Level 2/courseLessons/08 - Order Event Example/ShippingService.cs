using System;

public class ShippingService
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
        Console.WriteLine($"---------Shipping Service-------");
        Console.WriteLine($"Shipping Service Object Received a new order event");
        Console.WriteLine($"Order ID     : {e.OrderID} .");
        Console.WriteLine($"Orider Price : {e.OrderTotalPrice} .");
        Console.WriteLine($"Email        : {e.ClientEmail} .");
        Console.WriteLine($"\nHandel Shipping");
        Console.WriteLine($"--------------------------------");
        /*
            here you write the code to handel shipping to the client 
        ..................
        */
        Console.WriteLine();
    }
}
