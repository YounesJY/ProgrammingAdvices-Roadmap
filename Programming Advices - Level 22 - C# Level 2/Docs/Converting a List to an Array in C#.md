# Converting a List to an Array in C#

## 1. Code Setup

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

---

## 2. Converting List to Array

The `List<T>` class provides the `ToArray()` method. It allocates a new array and copies the list elements into it.

```csharp
int[] numbersArray = numbers.ToArray();
```

---

## 3. Full Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int[] numbersArray = numbers.ToArray();

        Console.WriteLine("Array elements: " + string.Join(", ", numbersArray));

        Console.ReadKey();
    }
}
```

**Output**

```
Array elements: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
```

---

## 4. Notes

- **`ToArray()` copies.** It does not return a view over the list. Mutating the array afterward **<mark>does not</mark>** affect the list, and adding/removing list elements afterward **<mark>does not</mark>** affect the array.
- **`ToArray()` allocates.** It creates a new array whose length equals `numbers.Count`. For large lists in hot paths, this allocation is real.
- **`ToArray()` is O(n).** It copies every element once.
- **`ToArray()` <mark>is immediate</mark>.** Unlike LINQ's deferred operators, `ToArray()` runs the copy right away.
- **If the list is `List<T>`, `ToArray()` is a method on `List<T>` itself.** For any `IEnumerable<T>` (array, LINQ query, `HashSet<T>`, etc.), `ToArray()` **<mark>is the LINQ extension method</mark>**. Same result, slightly different implementation.
- **Capacity <mark>is not preserved</mark>.** `ToArray()` returns an array of length `Count`, not `Capacity`. If the list's `Capacity` is 16 but `Count` is 10, the array has 10 elements.

---

## 5. When to use `ToArray()`

- **API requires an array.** Many BCL methods and third-party libraries **<mark>only</mark>** accept arrays.
- **You need fixed size.** Arrays **<mark><u>cannot</u></mark>** grow or shrink; if the size is fixed, an array may be more appropriate.
- **You need faster index access than `List<T>`.** Both are O(1), but arrays have **<mark>slightly less overhead</mark>** (no bounds check against `Count`, though the CLR still bounds-checks arrays).
- **You want to snapshot the list.** A copy **<mark><u>decouples</u></mark>** the array from later changes to the list.

## 6. When not to use `ToArray()`

- **You are iterating or filtering.** `IEnumerable<T>` from the list is already usable; `ToArray()` **<mark><u>forces a materialization</u></mark>** you may not need.
- **You need to pass it to a LINQ query.** LINQ works on any `IEnumerable<T>`. No conversion needed.
- **In a hot loop.** The allocation is measurable.

---

📌 **Summary**

| Question                                     | Answer                                                                       |
| -------------------------------------------- | ---------------------------------------------------------------------------- |
| How to convert?                              | `numbers.ToArray()`                                                          |
| Cost                                         | O(n) — allocates and copies                                                  |
| Does it affect the list?                     | No, it is a fresh array                                                      |
| Is the array's length `Count` or `Capacity`? | `Count`                                                                      |
| Does it run immediately?                     | Yes — unlike LINQ deferred operators                                         |
| Where does `ToArray()` come from?            | `List<T>` instance method for lists; LINQ extension for any `IEnumerable<T>` |
| When to use                                  | API requires an array, fixed size, snapshot                                  |
| When not to use                              | You just need to iterate or pass to LINQ                                     |

---
