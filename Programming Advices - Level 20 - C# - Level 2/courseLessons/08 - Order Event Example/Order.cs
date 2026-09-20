using System;

public class Order
{
    // public  EventHandler<OrderEventArgs> OnOrderCreated; not safe
    public event EventHandler<OrderEventArgs> OnOrderCreated;

    public void Create(int orderID, int orderTotalPrice, string clientEmail)
    {
        Console.WriteLine("New Order created; now will notify eveyone by raising the event.\n");
        OnOrderCreated?.Invoke(this, new OrderEventArgs(orderID, orderTotalPrice, clientEmail));
        /*
            if (OnOrderCreated != null)
                OnOrderCreated(this, new OrderEventArgs(orderID, orderTotalPrice, clientEmail));
        */
    }
}
