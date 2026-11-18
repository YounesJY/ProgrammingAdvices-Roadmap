using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        HashSet<int> numbers = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(n => n % 2 == 0);


        Console.WriteLine("Even Numbers:");
        foreach (var number in evenNumbers)
            Console.WriteLine(number);

        var numbersGreaterThanFive = numbers.Where(n => n > 5);
        
        Console.WriteLine("Numbers Greater Than 5:");
        foreach (var number in numbersGreaterThanFive)
            Console.WriteLine(number);

        Console.ReadKey();
    }
}