# Using HashSet with LINQ

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        HashSet<int> numbers = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        
        
        Console.WriteLine("Even Numbers:");
        foreach (var number in evenNumbers)
            Console.WriteLine(number);

        var numbersGreaterThanFive = numbers.Where(n => n > 5);
        
        Console.WriteLine("\nNumbers Greater Than 5:");
        foreach (var number in numbersGreaterThanFive)
            Console.WriteLine(number);

        Console.ReadKey();
    }
}
```

**Output (order may vary)**

```
Even Numbers:
2
4
6
8
10

Numbers Greater Than 5:
6
7
8
9
10
```

---

## 2. The Result is No Longer a HashSet

A **<mark>common point of confusion</mark>**: after calling `.Where()`, the result is **not** a `HashSet<T>`. It is an `IEnumerable<T>`. 

```csharp
var evenNumbers = numbers.Where(n => n % 2 == 0);
// evenNumbers is IEnumerable<int>, NOT HashSet<int>
```

This happens because **<mark>LINQ methods are <u>extension methods</u> on</mark>** `IEnumerable<T>`. To allow **<mark>method chaining</mark>** (`.Where().OrderBy().Select()`), each method **<mark><u>must return an interface</u></mark>** that the next method can accept. All LINQ methods return `IEnumerable<T>` (or a derived interface like `IOrderedEnumerable<T>`), **<mark><u>not</u> the original collection type</mark>**.

### 2.1 Why `var` is used

Because the return type is `IEnumerable<int>` (a complex generic interface), we use `var` to **<mark>let the compiler <u>infer it</u></mark>**. If you wanted to be explicit:

```csharp
IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);
```

You **<mark>cannot declare</mark>** it as `HashSet<int> evenNumbers = ...` because `Where()` does not return a `HashSet`.

### 2.2 Getting a HashSet back

If you need a `HashSet<T>` for set operations (like `UnionWith` or `IntersectWith`) after filtering, **<mark>you <u>must materialize</u> it</mark>**:

```csharp
HashSet<int> evenNumbersSet = numbers.Where(n => n % 2 == 0).ToHashSet();
```

`ToHashSet()` is an extension method in `System.Collections.Generic` (available since .NET Core 2.0 / .NET Standard 2.1). It takes any `IEnumerable<T>` and builds a new `HashSet<T>` from it.

---

## 3. Ordering

The source text notes that `HashSet<T>` **<mark>does not maintain order</mark>**, but LINQ operations will process elements in the order they are stored internally. 

This is technically true but practically dangerous:

- A `HashSet<T>` stores elements in buckets based on their hash code. The enumeration order depends on the internal array size, hash distribution, and collision resolution.
- It can change between .NET versions, machine architectures, and runs.
- **<mark><u>Do not rely on the order</u></mark>.** If you need a specific order, **<mark>apply</mark>** `.OrderBy()` to the LINQ query.

```csharp
var sortedEvens = numbers.Where(n => n % 2 == 0).OrderBy(n => n);
```

---

## 4. Notes

- **LINQ methods return `IEnumerable<T>`.** Not `HashSet<T>`, not `List<T>`.
- **Method chaining works because of `IEnumerable<T>`.** The return type of one LINQ method is the parameter type of the next.
- **`var` is used to hide the complex type.** `IEnumerable<int>` is the actual type.
- **To get a `HashSet<T>`, use `.ToHashSet()`.** To get a `List<T>`, use `.ToList()`.
- **`HashSet<T>` order is not guaranteed.** If it matters, add `.OrderBy()`.
- **LINQ is deferred.** `numbers.Where(...)` does not run until the `foreach`.
- **`HashSet<T>` methods vs LINQ methods.** `RemoveWhere` is a `HashSet<T>` method. `Where` is a LINQ method. Both filter, but `Where` is lazy and returns `IEnumerable`, while `RemoveWhere` mutates the set immediately and returns an `int`.

---

📌 **Summary**

| Question                                 | Answer                                                                                         |
| ---------------------------------------- | ---------------------------------------------------------------------------------------------- |
| What does `Where` return on a `HashSet`? | `IEnumerable<T>`, not `HashSet<T>`                                                             |
| Why?                                     | LINQ methods return interfaces to allow chaining                                               |
| Why `var`?                               | To infer the complex `IEnumerable<T>` type                                                     |
| How to get a `HashSet` back?             | `.ToHashSet()`                                                                                 |
| Is the order preserved?                  | No — `HashSet` order is an implementation detail                                               |
| Is LINQ deferred?                        | Yes                                                                                            |
| `Where` vs `RemoveWhere`?                | `Where` is LINQ (lazy, new sequence); `RemoveWhere` is `HashSet` (immediate, mutates in place) |

---


