using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, string> fruitsCategory = new Dictionary<string, string>
        {
            { "Apple", "Tree" },
            { "Banana", "Herb" },
            { "Cherry", "Tree" },
            { "Strawberry", "Bush" },
            { "Raspberry", "Bush" }
        };
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>
        {
            { "Apple", 5 },
            { "Banana", 2 },
            { "Orange", 7 }
        };

        var groupedFruits = fruitsCategory.GroupBy(kpv => kpv.Value);
        foreach (var group in groupedFruits)
        {
            Console.WriteLine($"Category: {group.Key}");
            foreach (var fruit in group)
                Console.WriteLine($" - {fruit.Key}");
        }


        var sortedFilteredFruits = fruitBasket
            .Where(kpv => kpv.Value > 3)
            .OrderBy(kpv => kpv.Key)
            .Select(kpv => new { kpv.Key, kpv.Value });

        Console.WriteLine("Sorted and Filtered Fruits:");
        foreach (var fruit in sortedFilteredFruits)
            Console.WriteLine($"Fruit: {fruit.Key}, Quantity: {fruit.Value}");

        Console.ReadKey();
    }
}
