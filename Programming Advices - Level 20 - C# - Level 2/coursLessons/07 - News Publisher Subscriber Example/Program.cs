using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        NewsPublisher publisher = new NewsPublisher();
        NewsSubscriber subscriber1 = new NewsSubscriber("Subscriber 1");
        NewsSubscriber subscriber2 = new NewsSubscriber("Subscriber 2");

        /*
            Events are Callbacks ?  
        */

        subscriber1.Subscribe(publisher);
        subscriber2.Subscribe(publisher);


        publisher.PublishNews("Breaking News", "A significant event just happened!");
        publisher.PublishNews("Tech Update", "New gadgets are hitting the market.");

        // Unsubscribe a subscriber (e.g., subscriber1)
        subscriber1.UnSubscribe(publisher);
        publisher.PublishNews("Weather Forecast", "Expect sunny weather for the weekend.");

        // Unsubscribe another subscriber (e.g., subscriber2)
        subscriber2.UnSubscribe(publisher);
        publisher.PublishNews("Final Edition", "Last news update for today.");
        
        
        Console.ReadLine();
    }
}
