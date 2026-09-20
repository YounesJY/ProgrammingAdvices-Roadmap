using System;

class Program
{
    /*
        A .NET pre-defined delegate with 16 overloads :)
    */
    // Define a Func delegate for squaring a number
    static Func<int, int> square = SquareMethod;

    // Define a method that squares a number
    static int SquareMethod(int x)
    {
        return x * x;
    }

    static void Main()
    {
        // Use the square Func to square the number 5
        int result = square(5);

        // Print the result
        Console.WriteLine($"The square of 5 is: {result}");
        Console.ReadKey();
    }
}
