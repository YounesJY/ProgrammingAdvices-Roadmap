using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 44, 22, 55, 666, 9, -6, 345, 11, 3, 3 };

        //numbers.Sort();
        //Console.WriteLine($"Sorted in Ascending Order: {string.Join(", ", numbers)}");

        //numbers.Reverse();
        //Console.WriteLine($"Sorted in Descending Order: {string.Join(", ", numbers)}");

        //Console.WriteLine($"Sorted Ascending with LINQ: {string.Join(", ", numbers.OrderBy(n => n))}");
        //Console.WriteLine($"Sorted Descending with LINQ: {string.Join(", ", numbers.OrderByDescending(n => n))}");

        Console.WriteLine($"Even Numbers: {string.Join(", ", numbers.Where(n => n % 2 == 0))}");
        Console.WriteLine(numbers.Where(n => n % 2 == 0));

        Console.ReadKey();
    }
}
