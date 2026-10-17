# Runtime Polymorphism and Static Methods

### 1. Dynamic Binding vs. Runtime Polymorphism vs. Dynamic Dispatch

These three terms describe the **exact same underlying mechanism**, but from slightly different angles:

- **Dynamic Method Dispatch:** The *mechanism*. The runtime (CLR) looks at the actual object in memory and decides *which* method implementation to execute.
- **Dynamic Binding:** The *timing*. Linking the method call to the actual code happens at **runtime**, not at compile time.
- **Runtime Polymorphism:** The *result*. One interface, many forms, resolved while the app is running.

**The classic example:**

```csharp
Animal myPet = new Dog();
myPet.Speak(); 
```

1. At **compile time**, the compiler only knows `myPet` is an `Animal`. It checks if `Animal` has a `Speak()` method. (Static type check).
2. At **runtime**, the CLR sees that the actual object in memory is a `Dog`. Through the **vtable** (virtual method table) of the `Dog` object, it dynamically dispatches the call to `Dog.Speak()`. (Dynamic binding).

**Contrast with Static Binding (Overloading):**

```csharp
void Print(int x) { ... }
void Print(string x) { ... }

Print(5); 
```

The compiler knows *exactly* which `Print` to call at compile time. This is **static binding** (compile-time polymorphism).

---

### 2. Static Methods: Functions Outside a Class?

**You heard a half-truth.** Here is the accurate nature of static methods:

1. **They live inside the class.** You cannot define a static method outside a class in C# (unlike C++). It is still a member of the class.
2. **They belong to the Type, not the Object.** A normal (instance) method belongs to an instance in memory (`var x = new MyClass(); x.DoWork();`). A static method belongs to the blueprint itself (`MyClass.DoWork();`). You don't need an object to call it.
3. **No `this` pointer.** Because they don't belong to an instance, they don't receive the hidden `this` reference. They cannot access non-static fields or methods.

**Why do people say they are "like functions outside a class"?**
Because they behave like procedural C functions. They don't have access to instance state; they just take parameters, do logic, and return a value.

**What about C# 9 Top-Level Statements?**
In modern C# (like your `Program.cs`), you can write code without explicitly wrapping it in a class:

```csharp
// Program.cs
Console.WriteLine("Hello");
```

The compiler secretly wraps this inside a hidden class (`Program`) and a hidden static method (`Main`). So even there, it *is* inside a class—you just don't see it.

**Static Method Under the Hood:**
When you call a static method, the CLR uses **static binding** (no vtable lookup). It jumps straight to the memory address of that method. It is slightly faster and doesn't allocate memory on the heap (no object creation required).
