using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] numbersArray = numbers.ToArray();

        Console.WriteLine($"Array elements: {string.Join(", ", numbersArray)}");
        Console.ReadKey();
    }
}
