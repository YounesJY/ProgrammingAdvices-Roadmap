using System;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        HashSet<string> fruits = new HashSet<string>();


        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Cherry");
        fruits.Add("Apple");
        fruits.Add("Apple");
        fruits.Add("Apple");

        foreach (string fruit in fruits)
            Console.WriteLine(fruit);
        Console.ReadKey();
    }
}