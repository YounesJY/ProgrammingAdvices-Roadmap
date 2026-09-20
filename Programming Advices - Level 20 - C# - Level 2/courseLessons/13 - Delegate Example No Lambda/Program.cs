using System;

class Program
{
    delegate int Operation(int x, int y);
    static void ExecuteOperation(int x, int y, Operation operation)
    {
        Console.WriteLine($"Result: {operation(x, y)}");
    }

    // static int Add(int x, int y) => x + y;
    static int Add(int x, int y)
    {
        return x + y;
    }

    // static int Sub(int x, int y) => x - y;
    static int Sub(int x, int y)
    {
        return x - y;
    }

    static void Main()
    {
        ExecuteOperation(10, 20, Add);
        ExecuteOperation(10, 20, Sub);

        Console.ReadLine();
    }
}
