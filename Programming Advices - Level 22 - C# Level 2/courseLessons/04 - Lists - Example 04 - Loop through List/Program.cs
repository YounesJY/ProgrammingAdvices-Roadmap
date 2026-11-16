using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };


        Console.WriteLine($"Number of items in the list: {numbers.Count}");
        Console.WriteLine("Displaying list elements using a for loop:");
        for (int i = 0; i < numbers.Count; i++)
            Console.WriteLine(numbers[i]);


        Console.WriteLine("Displaying list elements using a foreach loop:");
        foreach (int number in numbers)
            Console.WriteLine(number);

        Console.WriteLine("Displaying list elements using List.ForEach:");
        numbers.ForEach(number => Console.WriteLine(number));

        Console.ReadKey();
    }
}
