## What is Operator Overloading



    Operator overloading in C# allows you to define how operators behave for instances of your own classes. This means you can customize the behavior of operators such as `+`, `-`, `*`, `/`, `==`, `!=`, and others for your own types.

### Normal Addition vs. Concatenation:

The behavior of the `+` operator depends on the context and the types involved. For numeric types, it performs addition, while for string types, it performs concatenation. However, with operator overloading, you can define custom behaviors for the `+` operator based on the types you are working with.

In this example , we are demonstrating the use of the `+` operator for both normal addition and string concatenation.

```csharp
using System;
    public class Program
    {
        static void Main(string[] args)
        {
        int x=10,y=20;
        string s1 = "Mohammed", s2 = "Abu-Hadhoud";
        //The '+' operator is used for normal addition
        int z = x + y;
        //The '+' operator is used for concatonation
        string s3 = s1 + ' ' +  s2;
        Console.WriteLine($"The '+' operator is used for normal addition resulting z = {z}");
        Console.WriteLine($"The '+' operator is used for concatonation rsulting s3 = {s3}");
        Console.ReadKey();
    }
    }
```

Explanation:

1. You have two integers `x` and `y` representing numeric values, and you use the `+` operator for normal addition, calculating the sum and storing it in the variable `z`.
2. You have two strings `s1` and `s2` representing names. You use the `+` operator for string concatenation, combining the two strings along with a space in the middle, and storing the result in the variable `s3`.
3. You use `Console.WriteLine` to print the results to the console, indicating whether the `+` operator is used for normal addition or concatenation.
4. Finally, `Console.ReadKey()` is used to wait for a key press before the console window is closed.

When you run this program, it will output:

The '+' operator is used for normal addition resulting z = 30 
The '+' operator is used for concatenation resulting s3 = Mohammed Abu-Hadhoud 

This demonstrates the dual nature of the `+` operator in C#, where its behavior depends on the types of the operands. For numeric types, it performs addition, and for string types, it performs concatenation.
