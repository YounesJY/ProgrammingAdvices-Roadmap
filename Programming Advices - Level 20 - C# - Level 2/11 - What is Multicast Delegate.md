## What is <mark>Multicast Delegate</mark>?

You already know it :-) , many methods can subscribe to a delegate, and when delegate is called it will call all subscribers, this is called **multicast delegate**.

### **Multicast Delegate:**

In C#, a multicast delegate is a special type of delegate that can <mark>**reference multiple methods** **and invoke them in a single call**</mark>.

Delegates are used to encapsulate and reference methods, and a multicast delegate extends this concept by allowing you to combine multiple method references into a single delegate object.

You can create a multicast delegate by using the `+=` and `-=` operators to add or remove method references to the delegate. When you invoke a multicast delegate, it will call all the referenced methods in the order they were added.

Here's a simple example:

```csharp
using System;
public delegate void MyDelegate(string message);
class Program
{
    static void Main()
    {
        MyDelegate myDelegate = Method1;
        myDelegate += Method2;
        myDelegate("Hello, world!");
        myDelegate -= Method1;
        myDelegate("Another message.");
    }
    static void Method1(string message)
    {
        Console.WriteLine("Method1: " + message);
    }
    static void Method2(string message)
    {
        Console.WriteLine("Method2: " + message);
    }
}  
```

In this example, the `MyDelegate` delegate is a multicast delegate that references both `Method1` and `Method2`. When you invoke the delegate with `myDelegate("Hello, world!");`, both `Method1` and `Method2` are called, and their output is displayed.

<mark>You can also use multicast delegates for scenarios like event handling</mark>, where <u>*multiple event handlers need to be called when an event is raised*</u>. 

> <mark>Multicast delegates</mark> are <u>***commonly used in C#***</u> for implementing <mark>the observer pattern and event-driven programming</mark>.
