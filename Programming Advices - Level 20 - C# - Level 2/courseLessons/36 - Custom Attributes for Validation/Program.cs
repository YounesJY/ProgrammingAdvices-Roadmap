using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class RangeAttribute : Attribute
{
    public int Min { get; }
    public int Max { get; }
    public string ErrorMessage { get; set; }


    public RangeAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }
}

public class Person
{
    public string Name { get; set; }

    [Range(18, 99, ErrorMessage = "Age must be between 18 and 99.")]
    public int Age { get; set; }

    [Range(20, 30, ErrorMessage = "Experience must be between 20 and 30.")]
    public int Experience { get; set; }
}

public class ValidationExample
{
    static bool ValidatePerson(Person person)
    {
        Type type = typeof(Person);

        foreach (var property in type.GetProperties())
        {
            if (Attribute.IsDefined(property, typeof(RangeAttribute)))
            {
                var rangeAttribute = (RangeAttribute)Attribute.GetCustomAttribute(property, typeof(RangeAttribute));
                int value = (int)property.GetValue(person);

                if (value < rangeAttribute.Min || value > rangeAttribute.Max)
                {
                    Console.WriteLine($"Validation failed for property '{property.Name}': {rangeAttribute.ErrorMessage}");
                    return false;
                }
            }
        }

        return true;
    }
    public static void Main()
    {
        Person person = new Person { Age = 125, Name = "Mohammed Abu-Hadhoud", Experience = 15 };

        /*
            Quetion:
            let's imagine a real work class with +20 fields, how you can actually manager to validate them ?
        should you use some logic on setters ? well then you have to edit +20 property,
        then it might be better to have centerlized logic and shared among all of them, yeah ?
        maybe this will work , maybe not ?
        so [Validation Attribue vs Shared/Centerilzed logic] ?
        */

        if (ValidatePerson(person))
            Console.WriteLine("Person is valid.");
        else
            Console.WriteLine("Validation failed.");

        Console.ReadKey();
    }
}
