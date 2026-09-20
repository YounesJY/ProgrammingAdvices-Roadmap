using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;


[Serializable]
public class Person : IDisposable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Dispose()
    {
        Console.WriteLine("IDisposable invoked !");
    }
}


class Program
{
    static void Main()
    {
        using (Person person = new Person { Name = "Mohammed Abu-Hadhoud", Age = 46 })
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Person));


            using (TextWriter writer = new StreamWriter("person.xml"))
            {
                serializer.Serialize(writer, person);
            }

            using (TextReader reader = new StreamReader("person.xml"))
            {
                Person deserializedPerson = (Person)serializer.Deserialize(reader);
                Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            }
        }
    }
}

