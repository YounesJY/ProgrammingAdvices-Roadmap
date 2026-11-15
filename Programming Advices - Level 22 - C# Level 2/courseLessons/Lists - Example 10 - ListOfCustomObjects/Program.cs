using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main()
    {
        List<Person> people = new List<Person>
        {
            new Person("Alice", 30),
            new Person("Bob", 25),
            new Person("Charlie", 35),
            new Person("David", 40),
            new Person("Eve", 29),
            new Person("Frank", 31),
            new Person("Grace", 24),
            new Person("Helen", 27)
        };


        Console.WriteLine("Current state of the people list:");
        foreach (Person person in people)
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

        Person foundPerson = people.Find(p => p.Name == "David");
        if (foundPerson != null)
            Console.WriteLine($"Found Person: Name: {foundPerson.Name}, Age: {foundPerson.Age}");

        Person searchResult = people.FirstOrDefault(p => p.Name == "Alice");
        if (searchResult != null)
        {
            searchResult.Age = 31;
            Console.WriteLine($"Updated Alice's age to {searchResult.Age}");
        }

        List<Person> peopleOver30 = people.FindAll(p => p.Age > 30);
        Console.WriteLine("People over 30:");
        foreach (var person in peopleOver30)
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

        bool containsAlice = people.Any(p => p.Name == "Alice");
        Console.WriteLine($"List contains a person named 'Alice': {containsAlice}");

        bool existsOver40 = people.Exists(p => p.Age > 40);
        Console.WriteLine($"Exists person over 40: {existsOver40}");

        people.RemoveAll(p => p.Age < 30);
        Console.WriteLine("Removed people under the age of 30.");

        Console.WriteLine("Current state of the people list:");
        foreach (Person person in people)
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

        Console.ReadKey();
    }
}
