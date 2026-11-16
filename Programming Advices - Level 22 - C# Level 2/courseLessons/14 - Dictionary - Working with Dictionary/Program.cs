using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>();

        fruitBasket.Add("Apple", 5);
        fruitBasket.Add("Banana", 2);
        //the following commented line will casue an error 'System.ArgumentException: 'An item with the same key has already been added.' because they key is repeated.
        //fruitBasket.Add("Banana", 5);
        fruitBasket.Add("Orange", 12);

        // Accessing and updating elements
        fruitBasket["Apple"] = 10;
        // Assignment to a key that does not exist will add a new key-value pair
        fruitBasket["ships"] = 10;

        Console.WriteLine("Dictionary Content:");
        foreach (KeyValuePair<string, int> item in fruitBasket)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        // Removing an element
        fruitBasket.Remove("Banana");

        Console.WriteLine("Dictionary Content after removing Banana:");
        foreach (KeyValuePair<string, int> item in fruitBasket)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        Console.ReadKey();
    }
}
