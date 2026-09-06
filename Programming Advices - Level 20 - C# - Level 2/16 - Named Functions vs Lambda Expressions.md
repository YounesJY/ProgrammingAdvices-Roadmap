## Named Functions vs Lambda Expressions, when to use and which is faster?

### **Named Functions vs. Lambda Expressions**

#### Let's talk about when to use named functions and when to use lambda expressions, and also touch on the performance aspect.

#### 

#### **Named Functions:**

1.Readability and Reusability:

- Use named functions <mark>when the logic is complex or when the operation needs to be reused in multiple places</mark>. Named functions <mark>can enhance code readability and maintainability</mark>.

```csharp
int Square(int num)
{
    return num * num;
}    

int result = Square(5);
Console.WriteLine("Square of number: " + result);
```

2 .Clear Intent:

- If <mark>the function's purpose is clear from its name</mark>, using a named function is often a good choice. It <mark>makes your code self-documenting</mark>.



#### Lambda Expressions:

- Conciseness:
  - Use lambda expressions for short, simple operations, especially <mark>when the logic is straightforward and doesn't need a separate named function</mark>. They shine in scenarios where brevity is essential.

```csharp
// Lambda expression for squaring a number
Func<int, int> square = (int num) => num * num;
// Usage
int result = square(5);
Console.WriteLine("Square of number: " + result);
```

- <mark>Inline Usage</mark>: If the function is used inline, for example, <mark>in LINQ queries or event handling</mark>, lambda expressions can be more convenient.

```csharp
// Using lambda in LINQ
var evenNumbers = numbers.Where(n => n % 2 == 0);
// Using lambda in event handling
button.Click += (sender, e) => Console.WriteLine("Button clicked!");
```

### Performance Considerations:

    <u>In terms of performance</u>, <mark>the difference</mark> between named functions and lambda expressions <mark>is usually negligible</mark>. Both can be optimized by the compiler. The choice between them should primarily be based on readability, maintainability, and code organization.  

    In terms of performance, the difference between using a lambda expression and a declared function (method) is usually negligible. Both approaches can be optimized by the compiler, and <mark>**the generated IL (Intermediate Language) code may end up being quite similar**</mark>.

    The choice between a lambda expression and a declared function often <mark>depends on factors</mark> like readability, code organization, and <mark>whether the function is used in a single location or needs to be reused in multiple places</mark>.

    In simple cases, using a lambda expression with the `Func<int, int>` type is concise and suitable for a simple operation like squaring a number. If your logic becomes more complex or you need to reuse the operation in multiple places, <mark><u>declaring a separate method might make your code more modular and maintainable</u></mark>.

    Ultimately, for performance considerations, the difference between these two approaches is likely to be minimal. Choose the one that fits best with your coding style and the overall structure of your program.  

### Conclusion:

- Named functions: Ideal for complex logic, reuse, and when code readability is crucial.
- Lambda expressions: Great for concise, one-off operations, and when brevity is a priority.

Remember, <mark>the performance difference is often minimal</mark>, and your choice should be driven by the specific needs of your code and the principles of clean and maintainable coding.



What's the relation between Using and unmanaged code and IDisposable Iface an GC??? ?



what's related to utlising Unmanaged code inside/with Managed code ??
