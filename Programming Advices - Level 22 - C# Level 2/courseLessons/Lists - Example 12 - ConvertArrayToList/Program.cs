using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] numbersArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> numbersList = new List<int>(numbersArray);


        Console.WriteLine($"List elements: {string.Join(", ", numbersList)}");
        Console.ReadKey();
    }
}
