using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        HashSet<string> names = new HashSet<string>
        {
            "Alice",
            "Bob",
            "Charlie",
            "Daisy",
            "Ethan",
            "Fiona"
        };
        var namesStartingWithC = names.Where(name => name.StartsWith("C"));


        Console.WriteLine("Names Starting with C:");
        foreach (var name in namesStartingWithC)
            Console.WriteLine(name);

        var namesLongerThanFour = names.Where(name => name.Length > 4);

        Console.WriteLine("Names Longer Than Four Characters:");
        foreach (var name in namesLongerThanFour)
            Console.WriteLine(name);

        Console.ReadKey();
    }
}