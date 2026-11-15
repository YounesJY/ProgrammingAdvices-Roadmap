using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 44, 22, -55, 666, 9, -6, 345, 11, 3, 3 };

        Console.WriteLine($"List contains 9: {numbers.Contains(9)}");
        Console.WriteLine($"List contains negative numbers: {numbers.Exists(n => n < 0)}");
        Console.WriteLine($"First negative number: {numbers.Find(n => n < 0)}");
        Console.WriteLine($"All negative numbers: {string.Join(", ", numbers.FindAll(n => n < 0))}");
        Console.WriteLine($"Any numbers greater than 100: {numbers.Any(n => n > 100)}");

        Console.ReadKey();
    }
}
