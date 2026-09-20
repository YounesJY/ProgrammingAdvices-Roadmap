using System;

class Program
{
    // Define a Func delegate for squaring a number using a lambda expression
    static Func<int, int> square = (x) => x * x;

    static void Main()
    {
        // Use the square Func to square the number 5
        // Print the result
        Console.WriteLine($"The square of 5 is: {square(5)}");
        Console.ReadKey();
    }
}
