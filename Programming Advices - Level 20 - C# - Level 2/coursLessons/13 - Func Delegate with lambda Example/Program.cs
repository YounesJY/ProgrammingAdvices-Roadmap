using System;

class Program
{
    /*
        // [Instead of using a user-defined delegate]
        
        delegate int Operation(int x, int y);
        static void ExecuteOperation(int x, int y, Operation operation)
        {
            int result = operation(x, y); // Invoke the provided delegate
            Console.WriteLine("Result: " + result);
        }
    */

    // [Use this]
    static void ExecuteOperation(int x, int y, Func<int, int, int> Operation)
    {
        int result = Operation(x, y);
        Console.WriteLine("Result: " + result);
    }


    static void Main()
    {
        // [Solution 01]
        Func<int, int, int> Add = (x, y) => x + y;
        Func<int, int, int> Sub = (x, y) => x - y;

        ExecuteOperation(10, 20, Add);
        ExecuteOperation(10, 20, Sub);



        // [Solution 02]
        Operation AddOperation = (x, y) => x + y;
        Operation SubOperation = (x, y) => x - y;

        ExecuteOperation(10, 20, AddOperation);
        ExecuteOperation(10, 20, SubOperation);



        // [Solution 03]
        ExecuteOperation(10, 20, (Func<int, int, int>)((x, y) => x + y));
        ExecuteOperation(10, 20, (Func<int, int, int>)((x, y) => x - y));
        ExecuteOperation(10, 20, (Operation)((x, y) => x + y));
        ExecuteOperation(10, 20, (Operation)((x, y) => x - y));


        ExecuteOperation(10, 20, (x, y) => x + y);
        Console.ReadLine();
    }
}
