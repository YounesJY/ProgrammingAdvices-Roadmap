using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}


class Program
{
    static void Main()
    {
        Person person = new Person { Name = "Mohammed Abu-Hadhoud", Age = 46 };


        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream("person.bin", FileMode.Create))
        {
            formatter.Serialize(stream, person);
        }

        using (FileStream stream = new FileStream("person.bin", FileMode.Open))
        {
            Person deserializedPerson = (Person)formatter.Deserialize(stream);
            Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            Console.ReadKey();
        }
    }
}
