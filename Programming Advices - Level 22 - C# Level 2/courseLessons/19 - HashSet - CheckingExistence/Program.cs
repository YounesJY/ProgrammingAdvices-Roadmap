using System;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        HashSet<string> fruits = new HashSet<string> { "Apple", "Banana", "Cherry" };


        if (fruits.Contains("Apple"))
            Console.WriteLine("Apple is in the HashSet");
        else
            Console.WriteLine("Apple is not in the HashSet");

        if (fruits.Contains("Orange"))
            Console.WriteLine("Orange is in the HashSet");        
        else

            Console.WriteLine("Orange is not in the HashSet");

        Console.ReadKey();
    }
}
