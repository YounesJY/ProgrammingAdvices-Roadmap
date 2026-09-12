## Operator Overloading Example

    This code defines a `Point` class to represent 2D points and demonstrates operator overloading in C# by overloading the `+`, `-`, `==`, and `!=` operators.

```csharp
using System;
class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    // Overloading the + operator for point addition
    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }
    // Overloading the == operator for fraction equality
    public static bool operator == (Point p1, Point p2)
    {
        return (p1.X == p2.X) && (p1.Y == p2.Y);
    }
    // Overloading the != operator for fraction equality
    public static bool operator !=(Point p1, Point p2)
    {
        return (p1.X != p2.X) || (p1.Y != p2.Y);
    }
    // Overriding ToString for better readability
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
class Program
{
    static void Main()
    {
        Point point1 = new Point(1, 2);
        Point point2 = new Point(3, 4);
        // Using the overloaded + operator for point addition
        Point point3 = point1 + point2;
        // Using the overloaded + operator for point addition
        Point point4 = point1 - point2;
        Console.WriteLine($"Point1 : {point1.ToString()}");
        Console.WriteLine($"Point2 : {point2.ToString()}");
        Console.WriteLine($"Point3 is the result of point1 + point2: {point3.ToString()}");
        Console.WriteLine($"Point4 is the result of point1 - point2: {point4.ToString()}");
        // Using the overloaded == operator for point equality
        if (point1 == point2)
            Console.WriteLine("Using == : Yes, Point1 = Point2");
        else
            Console.WriteLine("Using == : No, Piont1 does not equal Point2");
        // Using the overloaded != operator for point inequality
        if (point1 != point2)
            Console.WriteLine("Using != : Yes, Piont1 does not equal Point2");
        else
            Console.WriteLine("Using != : No, Piont1 = Point2");
        Console.ReadKey();
    }
}
```

Here's an explanation of the code:

`Point` **Class:**

- The `Point` class has two properties, `X` and `Y`, representing the coordinates of a 2D point.
- The `+` operator is overloaded for point addition.
- The `-` operator is overloaded for point subtraction.
- The `==` operator is overloaded for point equality.
- The `!=` operator is overloaded for point inequality.
- The `ToString` method is overridden to provide a custom string representation of the point.

`Program` **Class:**

- - In the `Main` method, two `Point` objects (`point1` and `point2`) are created with different coordinates.
  - The overloaded `+` operator is used to add `point1` and `point2`, and the result is stored in `point3`.
  - The overloaded `-` operator is used to subtract `point2` from `point1`, and the result is stored in `point4`.
  - The program then prints the original points and the results of the addition and subtraction.
  - Finally, the overloaded `==` and `!=` operators are used to check for equality and inequality between `point1` and `point2`, and the results are printed accordingly.

When you run this program, it will output the coordinates of points, the results of point addition and subtraction, and whether the points are equal or not.

**Note:** you can apply the same concept for most of other operators.

Also note that not all operators can be overloaded in C#, we will discuss this in the next lesson.
