using System;

class Program
{
    /*
        A user-defined delegate 
    */
    /*
        Define a delegate type for squaring a number
        Create an instance of the SquareDelegate and associate it with the SquareMethod
    */
    delegate int SquareDelegate(int x);
    static SquareDelegate square = new SquareDelegate(SquareMethod);

    // Define a method that squares a number
    static int SquareMethod(int x)
    {
        return x * x;
    }

    static void Main()
    {
        // Use the square delegate to square the number 5
        int result = square(5);

        // Print the result
        Console.WriteLine($"The square of 5 is:{result}");
        Console.ReadKey();
    }
}
