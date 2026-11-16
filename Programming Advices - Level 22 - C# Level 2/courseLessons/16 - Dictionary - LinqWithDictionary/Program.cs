using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>
        {
            { "Apple", 5 },
            { "Banana", 2 },
            { "Orange", 7 }
        };


        var fruitInfo = fruitBasket.Select(kpv => new { kpv.Key, kpv.Value });
        Console.WriteLine("Transformed Items:");
        foreach (var item in fruitInfo)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var filteredFruit = fruitBasket.Where(kpv => kpv.Value > 3);
        Console.WriteLine("Filtered Items >3:");
        foreach (var item in filteredFruit)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var sortedByQuantity = fruitBasket.OrderBy(kpv => kpv.Value);
        Console.WriteLine("Sorted Items:");
        foreach (var item in sortedByQuantity)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var sortedByQuantityDesc = fruitBasket.OrderByDescending(kpv => kpv.Value);
        Console.WriteLine("Sorted Items Desc:");
        foreach (var item in sortedByQuantityDesc)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        int totalQuantity = fruitBasket.Sum(kpv => kpv.Value);
        Console.WriteLine($"Total Quantity: {totalQuantity}");

        Console.ReadKey();
    }
}
