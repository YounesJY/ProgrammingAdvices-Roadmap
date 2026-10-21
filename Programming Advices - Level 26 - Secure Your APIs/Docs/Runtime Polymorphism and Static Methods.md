# Runtime Polymorphism and Static Methods

### 1. Dynamic Binding vs. Runtime Polymorphism vs. Dynamic Dispatch

These three terms describe the **exact same underlying mechanism**, but from slightly different angles:

- **Dynamic Method Dispatch:** <mark>**The *mechanism***</mark>. The runtime (CLR) <mark>looks at **the actual object** in memory</mark> and <mark>decides ***which*** method **implementation** to execute</mark>.
- **Dynamic Binding:** <mark>**The *timing***</mark>. <mark>Linking **the method call** to **the actual code** happens at **runtime**</mark>, not at compile time.
- **Runtime Polymorphism:** <mark>**The *result***</mark>. One interface, many forms, resolved while the app is running.

**The classic example:**

```csharp
Animal myPet = new Dog();
myPet.Speak(); 
```

1. At **compile time**, the compiler only knows `myPet` is an `Animal`. It checks if `Animal` has a `Speak()` method. (**<u>Static type check</u>**).
2. At **runtime**, the CLR sees that the actual object in memory is a `Dog`. Through the **<mark>vtable</mark>** (virtual method table) of the `Dog` object, it <mark>**dynamically <u>dispatches</u>** the call</mark> to `Dog.Speak()`. (Dynamic binding).

**Contrast with Static Binding (Overloading):**

```csharp
void Print(int x) { ... }
void Print(string x) { ... }

Print(5); 
```

The compiler <mark>knows ***exactly***</mark> which `Print` to call at compile time. <mark>This is **static binding**</mark> (compile-time polymorphism).

---

### 2. Static Methods: Functions Outside a Class?

**You heard a half-truth.** Here is the accurate nature of static methods:

1. **They live inside the class.** You cannot define a static method outside a class in C# (unlike C++). It is still a member of the class.
2. **They belong to the Type, not the Object.** A normal (instance) method belongs to an instance in memory (`var x = new MyClass(); x.DoWork();`). A static method belongs to the blueprint itself (`MyClass.DoWork();`). You don't need an object to call it.
3. **No `this` pointer.** Because they don't belong to an instance, they don't receive the hidden `this` reference. They cannot access non-static fields or methods.

<mark>**Why do people say they are "like <u>functions outside a class</u>"?**</mark>
Because they behave like procedural C functions. <mark>**They don't have access to instance state**</mark>; they just take parameters, do logic, and return a value.

**What about C# 9 Top-Level Statements?**
In modern C# (like your `Program.cs`), you can write code without explicitly wrapping it in a class:

```csharp
// Program.cs
Console.WriteLine("Hello");
```

The compiler <mark>**secretly wraps**</mark> this inside a hidden class (`Program`) and a hidden static method (`Main`). So even there, it *is* inside a class—you just don't see it.

**Static Method Under the Hood:**
When you call a static method, the CLR uses **static binding** (<mark>**no vtable lookup**</mark>). It jumps straight to the memory address of that method. It is slightly faster and doesn't allocate memory on the heap (no object creation required).

---
