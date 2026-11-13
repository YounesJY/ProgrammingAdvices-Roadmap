# Looping Through a `List<int>` in C#

## 1. Full Code

```csharp
using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        Console.WriteLine("Number of items in the list: " + numbers.Count);

        Console.WriteLine("\nDisplaying list elements using a for loop:");
        for (int i = 0; i < numbers.Count; i++)
        {
            Console.WriteLine(numbers[i]);
        }

        Console.WriteLine("\nDisplaying list elements using a foreach loop:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nDisplaying list elements using List.ForEach:");
        numbers.ForEach(number => Console.WriteLine(number));

        Console.ReadKey();
    }
}
```

**Output**

```
Number of items in the list: 5

Displaying list elements using a for loop:
1
2
3
4
5

Displaying list elements using a foreach loop:
1
2
3
4
5

Displaying list elements using List.ForEach:
1
2
3
4
5
```

---

## 2. The Three Loop Forms

| Form              | Access style                               | Needs index? | Can modify during iteration?                             | Cost                                                       |
| ----------------- | ------------------------------------------ | ------------ | -------------------------------------------------------- | ---------------------------------------------------------- |
| `for`             | `list[i]` by index                         | Yes          | Yes (careful)                                            | **<mark>Fastest</mark>** — direct indexer access           |
| `foreach`         | **<mark>Element directly</mark>**          | No           | **<mark>No</mark>** — throws `InvalidOperationException` | Slightly slower — **<mark>enumerator overhead</mark>**     |
| `List<T>.ForEach` | **<mark>Delegate call</mark>** per element | No           | **<mark>No</mark>** — same enumeration guard             | Slowest — **<mark>delegate invocation per element</mark>** |

---

## 3. Speed Ordering

The common claim that `List<T>.ForEach` is a `for` loop in disguise **<mark>is wrong</mark>**. `List<T>.ForEach` invokes an `Action<T>` delegate per element, and `foreach` walks **an enumerator**. The honest ordering from fastest to slowest is:

1. **`for`** — direct indexer access, **no enumerator**, compiler **can optimize**
2. **`foreach`** — `List<T>`'s enumerator is a struct, so **<mark>no boxing</mark>**, but there is still enumerator overhead
3. **`List<T>.ForEach`** — delegate call per element; the overhead **grows** with list size

The gap is small for typical list sizes. It matters in hot paths and on large lists.

### Why `foreach` on `List<T>` is not slow by default

`List<T>.GetEnumerator()` returns a **<mark>struct enumerator</mark>** (`List<T>.Enumerator`). `foreach` uses that struct directly in the generated IL, so there is **<mark>no heap allocation and no boxing</mark>**. The "`foreach` boxes the enumerator" claim **<mark><u>applies</u></mark>** to `IEnumerable` (interface-typed variables), **<mark>not</mark>** to `foreach` over a `List<T>` variable directly.

```csharp
foreach (int n in numbers)             // struct enumerator — no boxing
foreach (int n in (IEnumerable)numbers) // boxed enumerator — allocation
```

The slow case is the second line, where the list is typed as `IEnumerable`.

---

## 4. The Modification-During-Iteration Exception

```csharp
foreach (int number in numbers)
{
    if (number == 20)
        numbers.Remove(number);   // throws InvalidOperationException
}
```

`List<T>` **<mark>maintains</mark>** a private `_version` field. **<mark><u>Every mutation</u></mark>** (`Add`, `Remove`, `Insert`, `Clear`, `RemoveAt`, `RemoveAll`, sorting, etc.) increments it. The enumerator captures `_version` when it is created and checks it on each `MoveNext()`. **<mark><u>If the version changed</u></mark>**, it throws:

```
InvalidOperationException: Collection was modified; enumeration operation may not execute.
```

This is **<mark>fail-fast</mark>**, not a bug. It catches the class of bugs where removing while iterating would skip or double-visit elements. The same check applies to `List<T>.ForEach`, `for` loops that call `Remove` (the check is in the collection, not the loop), and any `IEnumerable` consumer over a mutated list.

### The fix

```csharp
numbers.RemoveAll(x => x == 20);
```

`RemoveAll` is designed for this. It walks the internal array once, **<mark>marks survivors</mark>**, and compacts in one pass — **no enumerator**, **no `_version` check**, **no repeated shifting**.

### Other safe options

| Intent                          | Safe approach                                                        |
| ------------------------------- | -------------------------------------------------------------------- |
| Remove by predicate             | `RemoveAll(predicate)`                                               |
| Remove by value while iterating | Collect indices first, then `RemoveAt` in reverse                    |
| Remove by value                 | `Remove` outside the loop                                            |
| Build a filtered list           | LINQ `Where(...).ToList()`                                           |
| Iterate backwards with `for`    | `for (int i = list.Count - 1; i >= 0; i--)` — safe for `RemoveAt(i)` |

---

## 5. Notes

- **`for` gives the index.** Use it when you need both the element and its position, or when you must modify the collection during traversal (backwards `for` is **<mark>the idiomatic safe way</mark>**).
- **`foreach` <mark>is the default</mark>.** Cleaner, no index management, **<mark><u>works with any</u></mark>** `IEnumerable<T>`. Use it **<mark>whenever you only read</mark>**.
- **`List<T>.ForEach` exists but is <mark>rarely</mark> the right call.** It adds **delegate overhead** and an extra method call layer for no readability gain over `foreach`. Some teams ban it for that reason.
- **`foreach` over `IEnumerable<T>` <mark>boxes the enumerator</mark>.** Assigning a `List<T>` to an `IEnumerable<T>` variable and then iterating **<u>forces a boxed enumerator</u>**. Iterate the concrete type when performance matters.
- **The version check applies everywhere.** **<mark>Any mutation</mark>** during enumeration throws, whether via `foreach`, `List<T>.ForEach`, or an explicit `using` on the enumerator.

---

📌 **Summary**

| Question                                   | Answer                                                         |
| ------------------------------------------ | -------------------------------------------------------------- |
| Which is fastest?                          | `for` — direct indexer, no enumerator                          |
| Which is most readable?                    | `foreach` for reading, no index needed                         |
| Is `List<T>.ForEach` fast?                 | No — delegate call per element                                 |
| Does `foreach` box on `List<T>`?           | No — struct enumerator; boxes only when typed as `IEnumerable` |
| What happens if I mutate during `foreach`? | `InvalidOperationException` (`_version` mismatch)              |
| Fix for "remove while iterating"           | `RemoveAll(predicate)`                                         |
| When to use `for`?                         | Need the index, or must mutate backwards                       |
| When to use `foreach`?                     | Read-only traversal — the default                              |

---
