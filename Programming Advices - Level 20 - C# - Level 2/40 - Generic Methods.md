## Generic Methods

Here's a complete C# program that includes the `Utility` class with the generic `Swap` method and demonstrates its usage:

```csharp
using System;
public class Utility
{
    public static T Swap<T>(ref T first, ref T second)
    {
        T temp = first;
        first = second;
        second = temp;
        return temp;
    }
}
class Program
{
    static void Main()
    {
        // Usage with integers
        int a = 5, b = 10;
        Console.WriteLine($"Before swap: a = {a}, b = {b}");
        Utility.Swap(ref a, ref b);
        Console.WriteLine($"After swap: a = {a}, b = {b}");
        Console.WriteLine();
        // Usage with strings
        string x = "Hello", y = "World";
        Console.WriteLine($"Before swap: x = {x}, y = {y}");
        Utility.Swap(ref x, ref y);
        Console.WriteLine($"After swap: x = {x}, y = {y}");
    }
}
```

In the `Utility` class, the `Swap` method is declared as a static method that takes two parameters (`first` and `second`) by reference using the `ref` keyword. The method uses a temporary variable (`temp`) to perform the swap operation.

In the usage section, you demonstrate how this generic `Swap` method can be used with both integers (`int`) and strings (`string`). The method is called with the `ref` parameters, and after the call, the values of the variables are swapped.

This is a great illustration of the flexibility and type safety provided by generics in C#. The same `Swap` method can be used for different types without the need for code duplication.

In this program:

1. The `Utility` class is defined with the generic `Swap` method.
2. The `Program` class contains the `Main` method, which demonstrates the usage of the `Swap` method with both integers and strings.
3. The program prints the values before and after the swap to illustrate the effect of the `Swap` method.

When you run this program, you should see output similar to the following:

```csharp
Before swap: a = 5, b = 10
After swap: a = 10, b = 5
Before swap: x = Hello, y = World
After swap: x = World, y = Hello
```

This confirms that the `Swap` method is working as expected, swapping the values of the variables for both integers and string
