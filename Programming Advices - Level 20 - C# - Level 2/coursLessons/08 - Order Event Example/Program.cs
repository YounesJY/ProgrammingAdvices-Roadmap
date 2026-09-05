using System;


class Program
{
    static void Main(string[] args)
    {
        var order = new Order();
        EmailService emailService = new EmailService();
        SMSService smsService = new SMSService();
        ShippingService shippingService = new ShippingService();


        emailService.Subscribe(order);
        smsService.Subscribe(order);
        shippingService.Subscribe(order);
        //shippingService.UnSubscribe(order);

        order.Create(10, 540, "Ahmed@gmail.com");
        //order.Create(11, 300, "Ali@gmail.com");

        Console.ReadLine();
    }
}
