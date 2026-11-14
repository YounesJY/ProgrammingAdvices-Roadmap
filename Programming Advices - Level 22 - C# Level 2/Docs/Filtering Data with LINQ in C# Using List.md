# Filtering Data with LINQ in C# Using List

## 1. Code Setup

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

---

## 2. Filtering Techniques

| Filter               | Predicate                      | Result on the sample list |
| -------------------- | ------------------------------ | ------------------------- |
| Even numbers         | `n => n % 2 == 0`              | 2, 4, 6, 8, 10            |
| Odd numbers          | `n => n % 2 != 0`              | 1, 3, 5, 7, 9             |
| Greater than 5       | `n => n > 5`                   | 6, 7, 8, 9, 10            |
| Every second element | `(n, index) => index % 2 == 1` | 2, 4, 6, 8, 10            |
| Between 3 and 8      | `n => n > 3 && n < 8`          | 4, 5, 6, 7                |

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

        Console.WriteLine("Even Numbers: " + string.Join(", ", numbers.Where(n => n % 2 == 0)));
        Console.WriteLine("Odd Numbers: " + string.Join(", ", numbers.Where(n => n % 2 != 0)));
        Console.WriteLine("Numbers Greater Than 5: " + string.Join(", ", numbers.Where(n => n > 5)));
        Console.WriteLine("Every Second Number: " + string.Join(", ", numbers.Where((n, index) => index % 2 == 1)));
        Console.WriteLine("Numbers Between 3 and 8: " + string.Join(", ", numbers.Where(n => n > 3 && n < 8)));

        Console.ReadKey();
    }
}
```

**Output**

```
Even Numbers: 2, 4, 6, 8, 10
Odd Numbers: 1, 3, 5, 7, 9
Numbers Greater Than 5: 6, 7, 8, 9, 10
Every Second Number: 2, 4, 6, 8, 10
Numbers Between 3 and 8: 4, 5, 6, 7
```

---

## 4. `Where` with Index

`Where` **<mark>has two overloads</mark>**:

```csharp
public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate);

public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, int, bool> predicate);
```

In the second overload, the predicate receives the element **<mark>and</mark>** its zero-based index in the source sequence.

```csharp
numbers.Where((n, index) => index % 2 == 1)  // 2nd, 4th, 6th, ...
```

The `n` parameter is not required by the predicate body here — the example filters purely by position. It is still in the signature, so **<mark>it must be declared</mark>**. You can name it `_` if you do not use it:

```csharp
numbers.Where((_, index) => index % 2 == 1)
```

### Why the second overload uses `Func` and not `Predicate`

`Predicate<T>` is a built-in delegate type declared as `delegate bool Predicate<in T>(T obj)`. It takes exactly **<mark>one</mark>** parameter. That is the whole reason it **<mark>cannot be used here</mark>**.

`Func<T, int, bool>` takes two parameters (`T` and `int`) and returns `bool`. **<mark>There is no built-in</mark>** `Predicate<T1, T2>` delegate in the BCL. `Func` is the general-purpose family (`Func<TResult>`, `Func<T, TResult>`, `Func<T1, T2, TResult>`, … up to 16 parameters) that covers every shape.

The first `Where` overload could have used `Predicate<T>`. It uses `Func<T, bool>` instead because:

- **<mark>Consistency</mark>.** LINQ uses `Func` **<mark>everywhere</mark>** (`Select`, `Where`, `Any`, `All`, `OrderBy`, …). One delegate family across the whole library.
- **`Predicate<T>` <mark>is legacy</mark>.** It exists mainly for `List<T>.Find`, `FindAll`, `RemoveAll`, `Exists`, etc. — **<mark>methods that predate LINQ</mark>**. New APIs use `Func`.
- **Composability.** `Func<T, bool>` is interchangeable with any other `Func`-typed API. `Predicate<T>` is not.

Same shape, different conventions. `Func<T, bool>` and `Predicate<T>` are structurally identical for one-parameter boolean predicates, but the CLR treats them as different delegate types. You cannot pass a `Predicate<int>` where a `Func<int, bool>` is expected without wrapping.

### The overload resolution rule

When you call `Where` with a single-parameter lambda, the compiler picks `Func<T, bool>`. When you call it with a two-parameter lambda, it picks `Func<T, int, bool>`. The number of lambda parameters selects the overload. There is no ambiguity.

```csharp
numbers.Where(n => n > 5)            // Func<T, bool>
numbers.Where((n, i) => i % 2 == 1)  // Func<T, int, bool>
```

---

## 5. Deferred Execution

### 5.1 What it means

`Where` does **<mark>not</mark>** run when you write it. It returns an `IEnumerable<T>` that holds a reference to the source and the predicate. **<mark><u>Nothing is filtered yet</u></mark>**. The filtering happens **<mark>when you enumerate the result</mark>** — `foreach`, `ToList()`, `Count()`, `string.Join`, `First()`, etc.

```csharp
var result = numbers.Where(n => n % 2 == 0);  // no filtering yet

numbers.Add(12);                               // happens before enumeration

foreach (var n in result)                      // filtering happens here
    Console.WriteLine(n);
```

The `12` is included because `result` enumerates `numbers` at iteration time, not at creation time.

### 5.2 Lazy execution vs lazy loading — same idea, different context

- **Deferred execution** is the **<mark>LINQ term</mark>**. The query runs at enumeration time.
- **Lazy execution** is the **<mark>general programming term</mark>**. Same concept: compute only when needed.
- **Lazy loading** is the **<mark>ORM term</mark>** (EF, Hibernate). A navigation property is fetched from the database only when accessed.

All three describe **<mark><u>the same idea</u></mark>** — delay work until the result is required — applied in three contexts. For LINQ, "deferred execution" is the precise term; "lazy" is a correct informal synonym.

### 5.3 Why it exists

- **Composability.** You can chain `Where().Select().OrderBy()` and the whole pipeline **<mark>executes once</mark>**, in a single pass, when enumerated.
- **No intermediate allocations.** `Where` **does not** build a new list. It returns **<mark>a lazy wrapper</mark>**. Memory is allocated once, **<mark>at enumeration</mark>**.
- **Fresh data.** The query sees the current state of the source when enumerated, which is useful when the source changes between query definition and execution.

### 5.4 The trap

```csharp
var result = numbers.Where(n => n % 2 == 0);

foreach (var n in result) Console.WriteLine(n);  // first enumeration
foreach (var n in result) Console.WriteLine(n);  // second enumeration — runs again
```

`result` is **<mark><u>not cached</u></mark>**. Each enumeration **<mark>re-runs</mark>** the predicate over the source. If the source is a database (in LINQ-to-Entities), each enumeration **<mark>issues a new SQL query</mark>**. If the source is a list, each enumeration re-scans it.

To force a single execution:

```csharp
var result = numbers.Where(n => n % 2 == 0).ToList();  // executes once, stores results
```

`ToList`, `ToArray`, `ToDictionary`, `ToHashSet`, `Count`, `Sum`, `First`, `Single`, `Any`, `All` — these are **<mark><u>immediate</u></mark>**. They enumerate and produce a result.

### 5.5 Why `string.Join` triggers execution

```csharp
Console.WriteLine("Even Numbers: " + string.Join(", ", numbers.Where(n => n % 2 == 0)));
```

`string.Join(string, IEnumerable<T>)` **<mark>iterates the sequence</mark>** to build the string. That is what triggers the filter.

### 5.6 Why `Console.WriteLine(list.Where(...))` does not

```csharp
Console.WriteLine(numbers.Where(n => n % 2 == 0));
```

`Console.WriteLine(object)` calls `ToString()` on the argument. The LINQ object's `ToString()` returns its type name, **<mark>not its elements</mark>**. The filter is **<mark>never triggered</mark>** because nothing enumerates it.

To see the elements, force enumeration:

```csharp
Console.WriteLine(string.Join(", ", numbers.Where(n => n % 2 == 0)));
// or
foreach (var n in numbers.Where(n => n % 2 == 0)) Console.WriteLine(n);
```

### 5.7 Which operators are deferred and which are immediate

| Deferred (returns `IEnumerable<T>`)          | Immediate (returns a value or collection)              |
| -------------------------------------------- | ------------------------------------------------------ |
| `Where`, `Select`, `OrderBy`, `ThenBy`       | `ToList`, `ToArray`, `ToDictionary`, `ToHashSet`       |
| `Distinct`, `Skip`, `Take`, `GroupBy`        | `Count`, `Sum`, `Average`, `Min`, `Max`                |
| `SelectMany`, `Concat`, `Union`, `Intersect` | `First`, `FirstOrDefault`, `Single`, `SingleOrDefault` |
| `Cast`, `OfType`, `Reverse`, `Zip`           | `Any`, `All`, `Contains`, `ElementAt`                  |

The left column **<mark>builds a pipeline</mark>**. The right column **<mark>drains it</mark>**. Deferred operators **<mark><u>compose into one pass</u></mark>**; immediate operators **<mark><u>force execution</u></mark>**.

## 5.8 The debugger's "Results View" is an enumeration

Visual Studio's "**Results View**" is not a passive property read. To display the elements, the debugger **<mark>calls</mark>** `GetEnumerator()` and walks the sequence in **<mark>a hidden evaluation</mark>**, separate from your code's instruction pointer. That is a real enumeration with real side effects — the predicate runs, and on EF sources a query fires.

The runtime type shown for `result` confirms it is a wrapper, not materialized data:

| Debugger display                                | Meaning                                                               |
| ----------------------------------------------- | --------------------------------------------------------------------- |
| `System.Linq.Enumerable.WhereListIterator<int>` | Runtime type — **<mark>a lazy iterator</mark>** for `List<T>` sources |
| `IEnumerable<int>`                              | Declared type of `result` (via `var`)                                 |
| `Current 0 int`                                 | Debugger's enumerator state — default, not an element of the result   |
| `Results View — Expanding ... will enumerate`   | The debugger warns: expanding runs a full, new enumeration            |
| `[0] 2, [1] 4, ...` after expanding             | The hidden enumeration's output, not cached into `result`             |

`WhereListIterator<T>` is one of three `Where` iterators inside `System.Linq` (`WhereArrayIterator<T>` for arrays, `WhereListIterator<T>` for `List<T>`, `WhereEnumerableIterator<T>` for other `IEnumerable<T>` sources). The class name is an implementation detail and can change between .NET versions; what matters is that it is an iterator, not a collection.

Expanding the view runs one enumeration. `foreach` runs another. Two passes, two predicate evaluations, no caching. The cursor appearing to **<mark>jump back</mark>** to the `Where` line during `foreach` is the predicate **<mark>re-executing</mark>** for the current element, not a re-assignment of `result`.

To inspect the result in the debugger without triggering a second evaluation, materialize it first:

```csharp
var result = numbers.Where(n => n % 2 == 0).ToList();
```

Now the runtime type is `List<int>`, the debugger shows the elements directly without the warning, and the predicate ran exactly once — at the `.ToList()` line.

---

📌 **Summary**

| Question                                                      | Answer                                                                                     |
| ------------------------------------------------------------- | ------------------------------------------------------------------------------------------ |
| What does `Where` return?                                     | An `IEnumerable<T>` **<mark>wrapper</mark>**, unfiltered **<mark>until enumerated</mark>** |
| When does the filter run?                                     | At enumeration: `foreach`, `ToList`, `Count`, `string.Join`, etc.                          |
| Is "deferred execution" the same as "lazy execution"?         | Yes, same idea. "Deferred" is the LINQ term.                                               |
| Is it the same as "lazy loading"?                             | Conceptually yes, but "lazy loading" is the ORM term (EF, Hibernate)                       |
| Why does `Console.WriteLine(query)` print type info?          | `WriteLine(object)` calls `ToString`, which does not enumerate                             |
| Why two `Where` overloads?                                    | One filters by element, one by element + index                                             |
| Why `Func<T,int,bool>` and not `Predicate`?                   | `Predicate<T>` takes one parameter; `Func` supports two                                    |
| Why not `Predicate<T>` for the one-parameter overload either? | LINQ standardizes on `Func`; `Predicate<T>` is a legacy `List<T>` convention               |
| How to force one execution?                                   | `.ToList()` / `.ToArray()` / `.Count()` / any immediate operator                           |
| Enumerating twice                                             | Runs the query twice; use `.ToList()` if that is not intended                              |

---


