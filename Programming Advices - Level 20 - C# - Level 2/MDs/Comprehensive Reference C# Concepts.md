# Comprehensive Reference: Delegates, Events, and Advanced C# Concepts

> **For:** Markdet Application – Mds Resume  
> **Purpose:** Solidify understanding of C# delegates, events, generics, LINQ, and advanced features after a 2-month gap.  
> **Topics Covered:** Method overloading, delegates (definition, evolution, usage), events (publish/subscribe, thread safety, patterns), generics (C# vs Java), covariance/contravariance, advanced C# features (reflection, expression trees, etc.), custom vs built-in delegates, Func/Action/EventHandler.

---

## Table of Contents

1. [Method Overloading](#1-method-overloading)  
2. [Delegates – The Foundation](#2-delegates--the-foundation)  
3. [Delegates in Java (Functional Interfaces)](#3-delegates-in-java-functional-interfaces)  
4. [Lambda Evolution – From Named Methods to Lambdas](#4-lambda-evolution--from-named-methods-to-lambdas)  
5. [Delegates vs Functions – Code as a Parameter](#5-delegates-vs-functions--code-as-a-parameter)  
6. [Passing Data Between Forms (WinForms)](#8-passing-data-between-forms-winforms)  
7. [Delegates vs Events – The Key Difference](#9-delegates-vs-events--the-key-difference)  
8. [What Exactly Is a Delegate?](#10-what-exactly-is-a-delegate)  
9. [Generics in C# (vs Java)](#11-generics-in-c-vs-java)  
10. [Covariance and Contravariance](#12-covariance-and-contravariance)  
11. [Advanced Core Concepts (Reflection, Expression Trees, etc.)](#13-advanced-core-concepts)  
12. [Event Publish/Subscribe – Deep Dive](#14-event-publishsubscribe--deep-dive)  
13. [Thread Safety – Why Copy to a Local Variable?](#15-thread-safety--why-copy-to-a-local-variable)  
14. [Custom Delegate vs Built-in (Action, Func, EventHandler)](#19-custom-delegate-vs-built-in-action-func-eventhandler)  
15. #20-func-equivalents-for-custom-delegates

---

## 1. Method Overloading

**Definition:** Multiple methods in the same class with the **same name** but **different parameters** (number, type, or order).  
**<mark>Return type, parameter names, and access modifiers</mark> <mark>are *ignored* for overloading</mark>.**

```csharp
public class Calculator
{
    // Overloaded by number of parameters
    public int Add(int a, int b) => a + b;
    public int Add(int a, int b, int c) => a + b + c;

    // Overloaded by type
    public double Add(double a, double b) => a + b;

    // Overloaded by order (if types differ)
    public string Add(int a, string b) => a + b;
    public string Add(string a, int b) => a + b;

    // NOT ALLOWED – only return type differs
    // public double Add(int a, int b) => (double)a + b; // Compile error
}
```

**Key Point:** The compiler decides which method to call at **compile time** <mark>based on the arguments you pass</mark>.

---

## 2. Delegates – The Foundation

A **delegate** is a **type** that represents a **reference to a method** with a specific signature. It is:

- A **reference type** (derives from `System.Delegate`).
- A **type‑safe function pointer**.
- <mark>A **contract** for methods (similar to an interface but for a single method).</mark>

### Defining and Using a Delegate

```csharp
// 1. Define the delegate type
public delegate int MathOperation(int x, int y);

// 2. Create a method that matches the signature
public static int Add(int a, int b) => a + b;
public static int Multiply(int a, int b) => a * b;

// 3. Use the delegate
MathOperation op = Add;
int result = op(5, 3); // or op.Invoke(5, 3);
 // result = 8 
op = Multiply;
result = op(5, 3);              // result = 15
```

### Multicast Delegates

A delegate can reference **multiple methods** – invocation calls all of them in order.

```csharp
MathOperation op = Add;
op += Multiply;                  // now points to both Add and Multiply
int result = op(5, 3);           // returns 15 (last method's result)
```

---

## 3. Delegates in Java (Functional Interfaces)

C# delegates are **<mark>not a built‑in language feature in Java</mark>**. <mark>The closest equivalent</mark> is **functional interfaces** (interfaces with a single abstract method) combined with **lambda expressions**.

### Java Example

```java
// 1. Define a functional interface
@FunctionalInterface
interface MathOperation {
    int operate(int a, int b);
}

// 2. Use lambda to implement it
MathOperation add = (a, b) -> a + b;
int result = add.operate(5, 3);  // 8
```

**Key differences:**

| C# Delegate                            | Java Functional Interface                      |
| -------------------------------------- | ---------------------------------------------- |
| Built‑in type                          | Interface with `@FunctionalInterface`          |
| Can be multicast                       | No multicast (must combine manually)           |
| Supports `Action`, `Func`, `Predicate` | Uses `java.util.function` (e.g., `BiFunction`) |

---

## 4. Lambda Evolution – From Named Methods to Lambdas

C# delegates evolved to become more concise:

1. **Named Methods** (C# 1.0) – explicit method definition.  
2. **Anonymous Methods** (C# 2.0) – inline delegate with `delegate` keyword.  
3. **Lambda Expressions** (C# 3.0) – concise syntax using `=>`.

```csharp
// 1. Named method
bool IsEven(int n) { 
    return n % 2 == 0; 
}
Func<int, bool> pred1 = IsEven;

// 2. Anonymous method
Func<int, bool> pred2 = delegate(int n) { return n % 2 == 0; };

// 3. Lambda expression (most common)
Func<int, bool> pred3 = (n) => n % 2 == 0;
// Func<int, bool> pred3 = n => n % 2 == 0; // if you only have 1 param
```

**Lambdas are syntactic sugar** – the compiler generates a delegate instance behind the scenes.

---

## 5. Delegates vs Functions – <mark>Code as a Parameter</mark>

**<mark>Brilliant insight</mark>:**  

- **Functions** pass **values** as parameters.  
- **Delegates** pass **code (behavior)** as parameters.

```csharp
// Passing data (values)
int Add(int a, int b) => a + b;
int result = Add(5, 3);  // 5 and 3 are values

// Passing behavior (code)
void Execute(Func<int,int,int> operation, int a, int b)
{
    int result = operation(a, b);
    Console.WriteLine(result);
}

Execute((x,y) => x + y, 5, 3);  // passes addition logic
Execute((x,y) => x * y, 5, 3);  // passes multiplication logic
```

This enables **<mark>higher‑order functions</mark>** and <mark>is the foundation of LINQ</mark>.

---

## 8. Passing Data Between Forms (WinForms)

When building desktop applications, you often need to pass data between forms. Here are the **best practices** and options:

### 1. Constructor Parameters (<mark>one‑way, parent → child</mark>)

```csharp
// Form2 constructor
public Form2(string data)
{
    InitializeComponent();
    label1.Text = data;
}

// Form1
Form2 frm = new Form2(textBox1.Text);
frm.Show();
```

### 2. <mark>Public Properties</mark> (similar to constructor)

```csharp
Form2 frm = new Form2();
frm.UserData = textBox1.Text;
frm.Show();
```

### 3. Events/Delegates (<mark>two‑way</mark>, child → parent)

```csharp
// Form2
public event Action<object, int> DataBack;

private void SendButton_Click(object sender, EventArgs e)
{
    DataBack?.Invoke(this, int.Parse(txtID.Text));
    this.Close();
}

// Form1
Form2 frm = new Form2();
frm.DataBack += (sender, id) => 
    MessageBox.Show($"Received ID: {id}");
frm.ShowDialog();
```

### 4. Dependency Injection (DI) – for enterprise apps

Inject a service (e.g., `IDataService`) into both forms to share state.

### 5. Event Aggregator / Message Bus – for loose coupling

<mark>Centralised event system</mark> (e.g., Prism’s `EventAggregator`) – <mark>ideal for large MVVM applications</mark>.

**Recommendation:**  

- Small projects: **Constructor + Events**  
- Large projects: **DI + Event Aggregator**

---

## 9. Delegates vs Events – <mark>The Key Difference</mark>

| Aspect          | Delegate                                  | Event                                                   |
| --------------- | ----------------------------------------- | ------------------------------------------------------- |
| **Declaration** | `public Action<int> MyDelegate;`          | `public event Action<int> MyEvent;`                     |
| **Invocation**  | Can be invoked from anywhere              | Can only be invoked **inside** the declaring class      |
| **Assignment**  | Can be assigned (`=`) (`+=` / `-=` too ?) | Cannot be assigned outside the class (`+=` / `-=` only) |
| **Usage**       | General callbacks, functional programming | Publish/subscribe patterns, UI events                   |

```csharp
public class Publisher
{
    // Delegate – anyone can invoke it
    public Action<string> NotifyDelegate;

    // Event – only Publisher can invoke it
    public event Action<string> NotifyEvent;

    public void RaiseEvent(string msg)
    {
        NotifyEvent?.Invoke(msg);   // OK
        NotifyDelegate?.Invoke(msg); // Also OK, but external code could also invoke NotifyDelegate
    }
}
```

**Rule of thumb:** Use `event` when you want to enforce that **only the publisher** can trigger the notification.

---

## 10. <mark>What Exactly Is a Delegate</mark>?

A delegate is a **reference type** that:

- Defines a <mark>method signature</mark> (**<u>return type and parameter types</u>**).
- Can hold references to **one or more** methods (<mark>multicast</mark>).
- Is <mark>the base</mark> for **events** and **lambda expressions**.
- Is **<mark>type‑safe</mark>** (compile‑time checking of signatures).

> **Inheritance chain:**  
> <mark> `System.Object` → `System.Delegate` → `System.MulticastDelegate` → `YourDelegate` </mark>  

### <mark>**Under the Hood**</mark>

> When you define a delegate, the compiler generates a **sealed class** derived from `MulticastDelegate` with `Invoke`, `BeginInvoke`, and `EndInvoke` methods.

```csharp
// Compiler-generated (simplified)
public sealed class MathOperation : System.MulticastDelegate
{
    public int Invoke(int x, int y);
    // ... other members
}
```

> <mark>So a delegate is **not just a function pointer** – it is a full‑fledged object with metadata`.</mark>

---

## 11. Generics in C# (vs Java)

You already know generics from Java. Here’s how C# extends them:

| Feature                                    | C#                                                        | Java                                   |
| ------------------------------------------ | --------------------------------------------------------- | -------------------------------------- |
| **<mark>Runtime type preservation</mark>** | ✅ Yes (type is known at runtime)                          | ❌ Type erasure (type info lost)        |
| **Primitive type support**                 | ✅ `List<int>` (no boxing)                                 | ❌ Requires `Integer` (boxing)          |
| **<mark>Constraints</mark>**               | Very rich (`class`, `struct`, `new()`, `unmanaged`, etc.) | Limited to `extends` (class/interface) |
| **Default values**                         | `default(T)` works for any type                           | `null` only for reference types        |
| **<mark>Covariance/Contravariance</mark>** | Supported with `in`/`out` modifiers                       | Wildcards (`? extends`, `? super`)     |

### Generic Class Example

```csharp
public class Repository<T> where T : class, new()
{
    private List<T> _items = new List<T>();

    public T Create() => new T();
    public void Add(T item) => _items.Add(item);
    // ...
}
```

### Constraints in C#

```csharp
public class MyClass<T>
    where T : class        // reference type
    where T : struct       // value type
    where T : new()        // parameterless constructor
    where T : IComparable  // implements interface
    where T : unmanaged    // pointer-safe (C# 7.3)
    where T : notnull      // non-nullable (C# 8+)
{ }
```

---

## 12. Covariance and Contravariance

**Covariance** and **contravariance** are about **type relationships** in generic types and delegates. They are **not the same** as upcasting/downcasting.

| Concept                   | Direction                | Use case                                  |
| ------------------------- | ------------------------ | ----------------------------------------- |
| **Covariance** (`out`)    | More derived → More base | Return types (e.g., `IEnumerable<out T>`) |
| **Contravariance** (`in`) | More base → More derived | Parameter types (e.g., `Action<in T>`)    |
| **Invariance**            | Neither                  | Read+write (e.g., `List<T>`)              |

### Covariance Example (IEnumerable)

```csharp
IEnumerable<string> strings = new List<string> { "a", "b" };
IEnumerable<object> objects = strings;  // OK – covariance
// You can only read from IEnumerable, so it's safe.
```

### Contravariance Example (Action)

```csharp
Action<object> objectAction = (obj) => Console.WriteLine(obj);
Action<string> stringAction = objectAction;  // OK – contravariance
// stringAction can accept a string because it's an object.
```

### Delegate Covariance/Contravariance

```csharp
delegate object ObjectFactory();
delegate string StringFactory();

StringFactory sf = () => "Hello";
ObjectFactory of = sf;  // Covariance – return type can be more derived

Action<object> objAct = (o) => { };
Action<string> strAct = objAct;  // Contravariance – parameter can be more base
```

---

## 13. Advanced Core Concepts

Beyond delegates and async, C# offers several powerful features:

### Reflection & Attributes

- Inspect types at runtime (`Type`, `MethodInfo`, etc.).
- Read custom attributes for metadata.

```csharp
[MyCustom("Description")]
public class MyClass { }

var attr = typeof(MyClass).GetCustomAttribute<MyCustomAttribute>();
Console.WriteLine(attr?.Description);
```

### Expression Trees

- Represent code as **data** (abstract syntax tree).
- Used in LINQ to SQL/EF to translate C# to SQL.

```csharp
Expression<Func<int, int, int>> expr = (a, b) => a + b;
var compiled = expr.Compile();
int result = compiled(3, 5);  // 8
```

### Memory Management (IDisposable & GC)

- `IDisposable` for deterministic cleanup of unmanaged resources.
- `using` statement ensures disposal.
- `WeakReference` to allow GC collection while still holding a reference.

### Unsafe Code & Pointers

- Use `unsafe` blocks for pointer arithmetic (interop, performance).
- Requires `/unsafe` compiler option.

### Source Generators (C# 9+)

- Generate code at compile time (metaprogramming).
- Useful for serialization, DI, etc.

### Pattern Matching (C# 7–11)

- `is`, `switch` expressions, property patterns, tuple patterns.

```csharp
object obj = ...;
string result = obj switch
{
    int i when i > 0 => "positive",
    string s => s.Length.ToString(),
    _ => "unknown"
};
```

### Record Types (C# 9+)

- Immutable reference types with value equality.

```csharp
public record Person(string Name, int Age);
var p1 = new Person("John", 30);
var p2 = p1 with { Age = 31 }; // immutability
```

---

## 14. Event Publish/Subscribe – Deep Dive

The publish/subscribe pattern is central to decoupled communication.

### Standard Event Pattern (with `EventHandler<T>`)

```csharp
// Step 1: Define custom EventArgs
public class PersonEventArgs : EventArgs
{
    public int PersonID { get; set; }
}

// Step 2: Declare event using EventHandler<T>
public event EventHandler<PersonEventArgs> PersonSelected;

// Step 3: Protected virtual method to raise the event
protected virtual void OnPersonSelected(int personId)
{
    PersonSelected?.Invoke(this, new PersonEventArgs { PersonID = personId });
}

// Step 4: Raise when needed
private void SomeAction()
{
    OnPersonSelected(123);
}
```

### Subscription

```csharp
// Subscribe
publisher.PersonSelected += (sender, e) =>
    Console.WriteLine($"Selected ID: {e.PersonID}");

// Unsubscribe (avoid memory leaks)
publisher.PersonSelected -= handlerMethod;
```

### Why use `EventArgs`?

- Allows adding more data later without breaking the contract.
- Follows .NET Framework guidelines.

---

## 15. Thread Safety – Why Copy to a Local Variable?

Consider this common code:

```csharp
protected virtual void OnEvent()
{
    EventHandler handler = MyEvent;   // copy to local variable
    handler?.Invoke(this, EventArgs.Empty);
}
```

**Why the copy?** To avoid a **race condition** where another thread unsubscribes between the null check and the invocation.

```csharp
// Without copy – potential NullReferenceException
if (MyEvent != null)
{
    // Another thread could set MyEvent = null here!
    MyEvent(this, EventArgs.Empty);  // 💥 Boom!
}
```

**With the null-conditional operator (`?.`)**, the compiler generates the same safe pattern:

```csharp
MyEvent?.Invoke(this, EventArgs.Empty); // compiler does the copy under the hood
```

So you can safely use `?.Invoke()` in modern C# without an explicit local variable.

---

## 19. Custom Delegate vs Built-in (Action, Func, EventHandler)

| Need                    | Custom Delegate                                     | Built-in Replacement                        |
| ----------------------- | --------------------------------------------------- | ------------------------------------------- |
| Void, 1 param           | `delegate void MyDel(int x);`                       | `Action<int>`                               |
| Void, 2 params          | `delegate void MyDel(object s, int id);`            | `Action<object, int>`                       |
| Returns value, 2 params | `delegate int MyDel(int a, int b);`                 | `Func<int, int, int>`                       |
| Returns bool, 1 param   | `delegate bool MyDel(string s);`                    | `Predicate<string>` or `Func<string, bool>` |
| Event with custom args  | `delegate void MyEventHandler(object s, MyArgs e);` | `EventHandler<MyArgs>`                      |

**When to use custom delegates:**

- You want a **self‑documenting** name (e.g., `DataBackEventHandler`).
- You have a complex signature that would be confusing with generic names.

**When to use built‑in:**

- For quick, simple callbacks.
- When following standard .NET patterns (use `EventHandler<T>` for events).

---

## 20. Func Equivalents for Custom Delegates

**`Func`** is a generic delegate that **returns a value**. Its last type parameter is the return type.

| Custom Delegate                               | Func Equivalent             |
| --------------------------------------------- | --------------------------- |
| `delegate int Calculator(int x, int y);`      | `Func<int, int, int>`       |
| `delegate string Transformer(string s);`      | `Func<string, string>`      |
| `delegate bool Validator(int x);`             | `Func<int, bool>`           |
| `delegate double Converter(int x, string y);` | `Func<int, string, double>` |
| `delegate int NoParams();`                    | `Func<int>`                 |

For **void‑returning** delegates, use **`Action`**:

```csharp
delegate void Logger(string msg);   → Action<string>
delegate void NoParams();           → Action
```

**Visual pattern for Func:**

```csharp
Func<Param1, Param2, ..., ParamN, ReturnType>
```

---

## Final Summary

This reference covers all the essential concepts you asked about. Use it to refresh your memory on:

- Method overloading and delegate fundamentals.
- The evolution of delegates into lambdas.
- The powerful idea of passing **code** as a parameter.
- Safe event handling patterns (thread safety, standard `EventHandler<T>`).
- Generics and variance in C# compared to Java.
- Advanced features to explore when you need more power.

**When building your Markdet application (or any WinForms project), remember:**

- Use **constructors** and **events** for form communication.
- Keep validation logic in the **Business layer**.
- Use **delegates** to pass behavior (e.g., sorting, filtering).
- Use **events** for notification and decoupled communication.

Feel free to revisit any section – you now have a solid foundation to build upon. Good luck with your project! 🚀

---

*Document generated for personal reference – all examples are tested in .NET 6/8.*
