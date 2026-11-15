# Converting an Array to a List in C#

## 1. Code Setup

```csharp
int[] numbersArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

---

## 2. Converting Array to List

The standard approach is the `List<T>` **<mark>constructor</mark>** that accepts an `IEnumerable<T>` (an array implements this interface).

```csharp
List<int> numbersList = new List<int>(numbersArray);
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
        int[] numbersArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        List<int> numbersList = new List<int>(numbersArray);

        Console.WriteLine("List elements: " + string.Join(", ", numbersList));

        Console.ReadKey();
    }
}
```

**Output**

```
List elements: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
```

---

## 4. Notes

- **This creates a copy.** The `List<T>` copies the array elements into its own internal array. Mutating the new list **<mark>does not</mark>** affect the original array, and **<mark>vice versa</mark>**.
- **The array <mark>must implement</mark> `IEnumerable<T>`.** Arrays do (`int[]` implements `IEnumerable<int>`). This is why the constructor accepts it directly.
- **Cost is O(n).** The constructor iterates the array once, copying each element into the list's internal buffer.
- **Capacity is optimized.** When passed an `ICollection<T>` (which arrays implement), the `List<T>` constructor knows the exact count upfront. It allocates the internal array to exactly that size, avoiding the usual 4 → 8 → 16 resizing steps.
- **Using `.ToList()`.** The LINQ extension method `numbersArray.ToList()` **<mark>does the exact same thing</mark>**. It internally calls `new List<T>(array)`. Use whichever reads better to you.

---

## 5. When to use `List<T>` constructor vs `.ToList()`

| Method                 | Namespace                    | What it does                   |
| ---------------------- | ---------------------------- | ------------------------------ |
| `new List<int>(array)` | `System.Collections.Generic` | Base class library constructor |
| `array.ToList()`       | `System.Linq`                | Extension method wrapper       |

There is **<mark><u>no performance difference</u></mark>**. `ToList()` is slightly **<mark>more idiomatic</mark>** in modern C# code that already uses LINQ. The constructor **<mark>is clearer</mark>** when you are already writing `new List<T>()` in the surrounding code.

---

📌 **Summary**

| Question                          | Answer                                                       |
| --------------------------------- | ------------------------------------------------------------ |
| How to convert?                   | `new List<int>(array)` or `array.ToList()`                   |
| Cost                              | O(n) — allocates and copies                                  |
| Is it a reference or a copy?      | A copy — the list has its own internal array                 |
| Does it avoid resizing?           | Yes — it sizes the internal array exactly to the input count |
| Does `ToList()` exist for arrays? | Yes, via the LINQ extension method                           |

---
