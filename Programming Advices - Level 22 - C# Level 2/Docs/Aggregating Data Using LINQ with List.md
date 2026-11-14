# Aggregating Data Using LINQ with List

## 1. Code Setup

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

---

## 2. Aggregation Operations

| Operation | Method      | Returns  | Example                     |
| --------- | ----------- | -------- | --------------------------- |
| Sum       | `Sum()`     | `int`    | `numbers.Sum()` → `55`      |
| Average   | `Average()` | `double` | `numbers.Average()` → `5.5` |
| Min       | `Min()`     | `int`    | `numbers.Min()` → `1`       |
| Max       | `Max()`     | `int`    | `numbers.Max()` → `10`      |
| Count     | `Count()`   | `int`    | `numbers.Count()` → `10`    |

`Count()` is technically **<mark>an aggregation in LINQ</mark>**. `List<T>` also has a `Count` **property** (no parentheses) — **<mark><u>that is not LINQ</u></mark>**.

---

## 3. Full Example

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Sum: " + numbers.Sum());
        Console.WriteLine("Average: " + numbers.Average());
        Console.WriteLine("Minimum: " + numbers.Min());
        Console.WriteLine("Maximum: " + numbers.Max());
        Console.WriteLine("Count: " + numbers.Count());

        Console.ReadKey();
    }
}
```

**Output**

```
Sum: 55
Average: 5.5
Minimum: 1
Maximum: 10
Count: 10
```

---

## 4. Minimum Background — What You Need to Know Right Now

### 4.1 LINQ is a set of extension methods on `IEnumerable<T>`

**<mark>Every</mark>** LINQ operator (`Sum`, `Where`, `Select`, `OrderBy`, `Count`, …) is a **<mark><u>static method</u></mark>** in `System.Linq.Enumerable`, defined to take an `IEnumerable<T>` as its **<mark>first parameter</mark>** and **<mark>marked with `this`</mark>**. **<mark><u>That is the entire mechanism</u></mark>**. Nothing more.

```csharp
public static int Sum(this IEnumerable<int> source) { ... }
```

When you call `numbers.Sum()`, the compiler rewrites it to `Enumerable.Sum(numbers)`. The `using System.Linq;` is what makes these static methods visible.

### 4.2 Extension methods in one paragraph

An extension method **<mark>is a static method in a static class</mark>** where **<mark>the first parameter is prefixed with `this`</mark>**. It lets you write `obj.Method()` where `Method` is really `SomeStaticClass.Method(obj)`. The type you extend **<mark>does not need to be yours</mark>**, and **<mark>you <u>do not touch</u> its source</mark>**.

```csharp
public static class IntExtensions
{
    public static bool IsEven(this int x) => x % 2 == 0;
}

int n = 4;
bool b = n.IsEven();   // same as IntExtensions.IsEven(n)
```

**<mark><u>That is the whole feature</u></mark>**. LINQ is one large library of extension methods built on this mechanism.

### 4.3 `IEnumerable<T>` is the contract LINQ works against

`IEnumerable<T>` has one member:

```csharp
public interface IEnumerable<out T> : IEnumerable
{
    IEnumerator<T> GetEnumerator();
}
```

**<mark>Anything</mark>** that implements `IEnumerable<T>` **<mark>can be passed to a LINQ method</mark>**. `List<T>`, arrays, `HashSet<T>`, `Dictionary<TKey,TValue>`, `Queue<T>`, `Stack<T>`, and every custom collection in the course — **<mark><u>all implement it</u></mark>**. That is why **<mark><u>LINQ works uniformly</u></mark>** across all of them.

`foreach` also depends on it (structurally, via the pattern — not necessarily the interface). The same contract powers both.

### 4.4 `IEnumerator<T>` is what actually walks the data

```csharp
public interface IEnumerator<out T> : IDisposable
{
    T Current { get; }
    bool MoveNext();
    void Reset();
}
```

`GetEnumerator()` returns an `IEnumerator<T>`. **<mark><u>Each LINQ call</u></mark>**, each `foreach`, each `ToList()` gets an enumerator and calls `MoveNext()` until it returns `false`. This is **<mark><u>the whole iteration model</u></mark>**.

### 4.5 The minimal mental model for now

| Piece                    | What it is                                   | Why it matters                               |
| ------------------------ | -------------------------------------------- | -------------------------------------------- |
| `IEnumerable<T>`         | Interface with `GetEnumerator()`             | Every LINQ method accepts one                |
| `IEnumerator<T>`         | Interface with `MoveNext()` / `Current`      | What actually iterates the data              |
| Extension method         | Static method with `this` on first parameter | The syntax `numbers.Sum()`                   |
| `System.Linq.Enumerable` | The static class holding every LINQ operator | The implementation of LINQ                   |
| `using System.Linq;`     | Brings those static methods into scope       | Without it, `numbers.Sum()` does not compile |

That is all you need to understand *what* LINQ is at the language level. Deferred execution (`yield return`, lazy evaluation) **<mark>is the next layer</mark>**, and the interface-implementation arc in Level 22 will make it concrete.

### 4.6 What not to worry about yet

- **`IQueryable<T>` / expression trees** — that is LINQ-to-Entities territory, belongs to EF.
- **`yield return` / iterator methods** — you will meet it during the interface lessons. It is how LINQ produces results lazily.
- **Deferred vs immediate execution** — same lesson, same moment.
- **Custom LINQ operators** — you can write them, but nothing in this course requires it.

---

## 5. Notes

- **`Count()` vs `Count`.** `numbers.Count` is a property on `List<T>` — O(1). `numbers.Count()` is a LINQ extension — O(1) on `List<T>` because LINQ detects the `ICollection<T>` interface and uses its `Count`, but O(n) on a plain `IEnumerable<T>`.
- **`Average()` returns `double`.** Integer input, floating-point output. `Sum` returns `int` for `IEnumerable<int>`; `Min`/`Max` return the element type.
- **`Sum` throws `OverflowException` for `int` overflow.** The overload for `int` uses a checked sum. Use `Sum(x => (long)x)` if you expect large values.
- **Empty sequence behavior.** `Sum` and `Count` return `0`; `Average`, `Min`, `Max` throw `InvalidOperationException`. That is **<mark>a common production bug</mark>** — guard with `Any()` first if the sequence can be empty.
- **LINQ <mark>works on any</mark> `IEnumerable<T>`.** `List<T>` is just the example here. The same aggregation methods work on arrays, `HashSet<T>`, query results, and any custom collection.

---

📌 **Summary**

| Question                                 | Answer                                                          |
| ---------------------------------------- | --------------------------------------------------------------- |
| What is LINQ?                            | A library of extension methods on `IEnumerable<T>`              |
| Where does it live?                      | `System.Linq.Enumerable`                                        |
| How does `numbers.Sum()` compile?        | `Enumerable.Sum(numbers)`                                       |
| What makes `numbers.Sum()` legal syntax? | `Sum` is an extension method (`this IEnumerable<int>`)          |
| What interface does LINQ require?        | `IEnumerable<T>` (and `IEnumerator<T>` underneath)              |
| Which types work with LINQ?              | Any type implementing `IEnumerable<T>`                          |
| `Count` vs `Count()`                     | Property on `List<T>` vs LINQ extension; both O(1) on `List<T>` |
| Empty list: `Sum`/`Count`                | Return `0`                                                      |
| Empty list: `Average`/`Min`/`Max`        | Throw `InvalidOperationException`                               |
| What to defer                            | `yield return`, deferred execution, `IQueryable<T>`             |

---
