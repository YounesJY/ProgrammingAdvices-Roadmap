## Custom Attribute

In C#, a custom attribute is <mark>a user-defined metadata that you can apply to elements in your code</mark>, such as classes, methods, properties, or parameters. Attributes provide a way to add declarative information to your code, which can be used by the runtime, tools, or other code to perform specific actions or make decisions.

To define a custom attribute, you <mark>create a class that inherits from the `System.Attribute` class or one of its derived classes</mark>. The attribute class can then be applied to various elements in your code using square brackets.

Here's a simple example of a custom attribute:

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class MyCustomAttribute : Attribute
{
    public string Description { get; }
    public MyCustomAttribute(string description)
    {
        Description = description;
    }
}
[MyCustom("This is a class attribute")]
class MyClass
{
    [MyCustom("This is a method attribute")]
    public void MyMethod()
    {
        // Method implementation
    }
}
```

In this example, `MyCustomAttribute` is a custom attribute that takes a description as a parameter. The attribute can be applied to classes and methods. The `AttributeUsage` attribute is used to specify where the custom attribute can be applied (`AttributeTargets.Class` and `AttributeTargets.Method` in this case) and whether it can be applied multiple times to the same element (`AllowMultiple = true`).

You can then use reflection or other mechanisms to access and utilize the information provided by these custom attributes at runtime. Custom attributes are often used for various purposes such as code generation, documentation, or influencing the behavior of frameworks and libraries.

> ***<mark><u>Note When we study reflection we will explain it</u>.</mark>***
