using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        int[] array = new int[] { 1, 2, 2, 3, 4, 4, 5 };

        // HashSet<int> uniqueNumbers = new HashSet<int>(array);
        HashSet<int> uniqueNumbers = array.Distinct().ToHashSet();
        foreach (int number in uniqueNumbers)
            Console.WriteLine(number);

        Console.ReadKey();
    }
}
