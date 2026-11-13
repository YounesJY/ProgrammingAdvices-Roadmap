using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


        numbers.Remove(5);
        Console.WriteLine($"After removing 5: {string.Join(", ", numbers)}");

        numbers.RemoveAt(0);
        Console.WriteLine($"After removing first element: {string.Join(", ", numbers)}");

        numbers.RemoveAll((n) => n % 2 == 0);
        Console.WriteLine($"After removing all even numbers: {string.Join(", ", numbers)}");

        numbers.Clear();
        Console.WriteLine($"After clearing the list, count: {numbers.Count}");

        Console.ReadKey();
    }
}
