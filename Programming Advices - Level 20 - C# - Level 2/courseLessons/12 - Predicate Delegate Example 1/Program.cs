using System;

class Program
{
    // Define a Func delegate for squaring a number
    static Predicate<int> IsEvenPredicate = IsEven;
    /*
        This is a workaround if you ever want to :)
    static Func<int, bool> IsEvenPredicate = IsEven;
    */

    static bool IsEven(int x)
    {
        return (x % 2 == 0);
    }

    static void Main()
    {
        Console.WriteLine($"Is Number 5  Even ?  {IsEvenPredicate(5)}");
        Console.ReadKey();
    }
}
