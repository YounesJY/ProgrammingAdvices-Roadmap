using System;

class Program
{
    // Regular methods to be used with Action delegates
    static void ParameterlessMethod()
    {
        Console.WriteLine("This is a parameterless action.");
    }
    static void ActionWithIntParameterMethod(int x)
    {
        Console.WriteLine($"Action with int parameter: {x}");
    }
    static void ActionWithMultipleParametersMethod(string str, int num)
    {
        Console.WriteLine($"Action with string and int parameters: {str}, {num}");
    }

    static void Main()
    {
        // Declare and initialize Action delegates with regular methods
        Action parameterlessAction = ParameterlessMethod;
        Action<int> actionWithIntParameter = ActionWithIntParameterMethod;
        Action<string, int> actionWithMultipleParameters = ActionWithMultipleParametersMethod;

        // Invoking the actions
        parameterlessAction();
        actionWithIntParameter(42);
        actionWithMultipleParameters("Hello, World!", 100);
        Console.ReadKey();
    }
}
