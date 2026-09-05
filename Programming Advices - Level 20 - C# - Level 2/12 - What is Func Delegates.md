### **What is Func Delegates?**

In simple words: it's a shortcut for normal delegate..

The `Func` delegate is a predefined delegate type in C# that represents a method that takes zero or more input parameters and returns a value. It's part of the `System` namespace and is often used to define and pass around functions or methods that return a value.  

The `Func` delegate is defined in various forms, depending on the number of input parameters and the return value:  

- `Func<TResult>`: Represents a method that takes no parameters and returns a result of type `TResult`.  

- `Func<T, TResult>`: Represents a method that takes one parameter of type `T` and returns a result of type `TResult`.  

- `Func<T1, T2, TResult>`: Represents a method that takes two parameters of types `T1` and `T2` and returns a result of type `TResult`.  

- So on, `Func<T1, T2, ..., Tn, TResult>` for methods with n parameters.  

Here are a few examples of how to use the `Func` delegate:  

1. `Func<int, int>`: Represents a method that takes an `int` as input and returns an `int`.

2. `Func<string, int, bool>`: Represents a method that takes a `string` and an `int` as input and returns a `bool`.

3. `Func<double, double, double, double>`: Represents a method that takes three `double` parameters and returns a `double`.
