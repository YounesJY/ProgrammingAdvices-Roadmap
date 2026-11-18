using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<string> fruits = new HashSet<string> { "Apple", "Banana", "Cherry" };


        Console.WriteLine($"Hashset Item Count = {fruits.Count.ToString()}");
        fruits.Remove("Banana");

        Console.WriteLine($"Hashset Item Count after removing Banana = {fruits.Count.ToString()}");
        foreach (string fruit in fruits)
            Console.WriteLine(fruit);

        fruits.Clear();
        Console.WriteLine($"Hashset Item Count after clear = {fruits.Count.ToString()}");

        Console.ReadKey();
    }
}