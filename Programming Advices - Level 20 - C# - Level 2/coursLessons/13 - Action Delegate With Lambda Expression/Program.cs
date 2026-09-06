using System;

class Program
{
    static void Main()
    {
        Action parameterlessAction = () => Console.WriteLine("This is a parameterless action.");
        Action<int> actionWithIntParameter = (x) => Console.WriteLine($"Action with int parameter: {x}");
        Action<string, int> actionWithMultipleParameters = (str, num) => Console.WriteLine($"Action with string and int parameters: {str}, {num}");

        parameterlessAction();
        actionWithIntParameter(42);
        actionWithMultipleParameters("Hello, World!", 100);

        Console.ReadKey();
    }
}
