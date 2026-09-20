using System;

public class OrderEventArgs : EventArgs
{
    public int OrderID { get;  }
    public int OrderTotalPrice { get; }
    public string ClientEmail { get; }


    public OrderEventArgs (int orderID, int orderTotalPrice, string clientEmail)
    { 
        this.OrderID = orderID; 
        this.OrderTotalPrice = orderTotalPrice;
        this.ClientEmail = clientEmail;
    }
}
