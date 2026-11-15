# Sorting a List in C# Using Various Methods

## 1. Code Setup

```csharp
List<int> numbers = new List<int> { 44, 22, 55, 666, 9, -6, 345, 11, 3, 3 };
```

---

## 2. Sorting Techniques

| Technique                         | Method                                                       | Order          | Cost                            |
| --------------------------------- | ------------------------------------------------------------ | -------------- | ------------------------------- |
| Default `Sort`                    | `numbers.Sort()`                                             | Ascending      | O(n log n) time, O(log n) space |
| Descending via `Sort` + `Reverse` | `numbers.Sort(); numbers.Reverse();`                         | Descending     | O(n log n) + O(n)               |
| LINQ ascending                    | `numbers.OrderBy(n => n)`                                    | Ascending      | O(n log n) time, O(n) space     |
| LINQ descending                   | `numbers.OrderByDescending(n => n)`                          | Descending     | O(n log n) time, O(n) space     |
| Custom comparison                 | `numbers.Sort((a, b) => Math.Abs(a).CompareTo(Math.Abs(b)))` | By custom rule | O(n log n)                      |

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
        List<int> numbers = new List<int> { 44, 22, 55, 666, 9, -6, 345, 11, 3, 3 };

        numbers.Sort();
        Console.WriteLine("Sorted in Ascending Order: " + string.Join(", ", numbers));

        numbers.Reverse();
        Console.WriteLine("Sorted in Descending Order: " + string.Join(", ", numbers));

        Console.WriteLine("Sorted Ascending with LINQ: " + string.Join(", ", numbers.OrderBy(n => n)));
        Console.WriteLine("Sorted Descending with LINQ: " + string.Join(", ", numbers.OrderByDescending(n => n)));

        Console.ReadKey();
    }
}
```

**Output**

```
Sorted in Ascending Order: -6, 3, 3, 9, 11, 22, 44, 55, 345, 666
Sorted in Descending Order: 666, 345, 55, 44, 22, 11, 9, 3, 3, -6
Sorted Ascending with LINQ: -6, 3, 3, 9, 11, 22, 44, 55, 345, 666
Sorted Descending with LINQ: 666, 345, 55, 44, 22, 11, 9, 3, 3, -6
```

The two LINQ lines operate on the already-reversed list, so "ascending with LINQ" re-sorts the list ascending and "descending with LINQ" sorts it descending. The original `numbers` **<mark><u>is not modified</u></mark>** by `OrderBy` / `OrderByDescending`.

---

## 4. `Sort` vs `OrderBy`

| Aspect                 | `List<T>.Sort()`                                    | `Enumerable.OrderBy()`                                 |
| ---------------------- | --------------------------------------------------- | ------------------------------------------------------ |
| Execution              | **<mark>Immediate</mark>** — runs when called       | **<mark>Deferred</mark>** — runs when enumerated       |
| Mutation               | **<mark>In-place</mark>** — modifies the list       | **<mark>Out-of-place</mark>** — returns a new sequence |
| Return                 | `void`                                              | `IOrderedEnumerable<T>`                                |
| Works on               | `List<T>` only                                      | **<mark><u>Any</u></mark>** `IEnumerable<T>`           |
| Comparison             | `IComparable<T>` on `T`, or a passed `IComparer<T>` | `Func<TSource, TKey>` key selector                     |
| Key type               | Must be `T` itself                                  | Any type that implements `IComparable<TKey>`           |
| Time complexity        | O(n log n)                                          | O(n log n)                                             |
| Space complexity       | O(log n) — in-place, **<mark>small stack</mark>**   | O(n) — **<mark><u>buffers the sequence</u></mark>**    |
| Stable sort            | No                                                  | Yes                                                    |
| Side effects on source | Reorders the source list                            | None                                                   |

### 4.1 Immediate vs deferred

```csharp
numbers.Sort();                  // sort runs now
var query = numbers.OrderBy(n => n); // nothing runs yet
```

`Sort` returns `void` and sorts the list **<mark>immediately</mark>**. `OrderBy` returns an `IOrderedEnumerable<T>` wrapper that **<mark>holds the source and the key selector</mark>**. The sort runs when you enumerate the query (`foreach`, `string.Join`, `ToList`, etc.). That is the same deferred execution model as `Where` from the previous lesson.

### 4.2 Why `Sort` is O(log n) space and `OrderBy` is O(n)

`Sort` **<mark>uses an introsort</mark>** (quicksort + heapsort + insertion sort hybrid) that sorts the list's internal array in place. Recursion depth is O(log n). No buffer proportional to `n`.

`OrderBy` **<mark>is deferred and stable</mark>**, so it **<mark><u>cannot</u></mark>** sort in place. It buffers the source into an array, sorts a key array alongside it, then yields the elements in order. That buffer is O(n).

### 4.3 Stability

`Sort` **<mark><u>is not stable</u></mark>**. Equal keys may come out in any order.

`OrderBy` **<mark>is stable</mark>**. Equal keys keep their original relative order. That matters when you chain sorts:

```csharp
var query = people
    .OrderBy(p => p.LastName)
    .ThenBy(p => p.FirstName);  // stable, so first-name order preserved within same last name
```

`ThenBy` depends on `OrderBy` being stable. `Sort` with a custom comparer cannot replicate this cleanly.

---

## 5. Ordering as SQL `ORDER BY`

`OrderBy(n => n)` is **<mark>the LINQ equivalent</mark>** of SQL `ORDER BY`. The lambda is the **<mark><u>key selector</u></mark>** — it produces the value to sort by. **<mark><u>Read it as</u></mark>** "add a computed column, then sort by that column".

```csharp
numbers.OrderBy(n => n)              // ORDER BY n
numbers.OrderBy(n => Math.Abs(n))    // ORDER BY ABS(n)
numbers.OrderBy(n => n % 10)         // ORDER BY n % 10
```

The key column does not have to be the same type as the elements. It **<mark>only has</mark>** to implement `IComparable<TKey>`.

```csharp
var people = new List<Person> { ... };
people.OrderBy(p => p.Name)          // key is string
people.OrderBy(p => p.Age)           // key is int
people.OrderBy(p => p.Name.Length)   // key is int (length of string)
```

Unlike SQL `ORDER BY`, the key can depend on the current element (which it always does, since it is a lambda over the element). That is the flexibility `Func<TSource, TKey>` provides.

### 5.1 Random ordering

There is no `OrderBy(random)`. **<mark>The trick</mark>** is to sort by a random key:

```csharp
numbers.OrderBy(n => Guid.NewGuid())
```

Each element gets a fresh GUID as its sort key, so the resulting order is random. Equivalent to SQL `ORDER BY NEWID()`.

`Guid.NewGuid()` is enough. `Guid.NewGuid().ToString()` also works but allocates a string per element **<mark>for no benefit</mark>**. Use the `Guid` directly.

---

## 6. Custom Sorting

### 6.1 `Sort` with a comparison delegate

```csharp
numbers.Sort((a, b) => Math.Abs(a).CompareTo(Math.Abs(b)));
```

The delegate returns a negative, zero, or positive `int`, same convention as `IComparable.CompareTo`.

### 6.2 `OrderBy` with a key selector

```csharp
numbers.OrderBy(n => Math.Abs(n))
```

Same result, expressed as "the key is the absolute value". No comparison logic; you describe the key, LINQ handles the comparison.

### 6.3 Descending with a key selector

```csharp
numbers.OrderByDescending(n => Math.Abs(n))
```

`Sort` has no descending mode. You sort ascending then call `Reverse`. `OrderByDescending` does it in one operator.

---

## 7. Notes

- **`Sort` mutates; `OrderBy` does not.** `numbers.Sort()` reorders `numbers`. `numbers.OrderBy(...)` leaves `numbers` untouched and returns a new sequence.
- **`Sort` returns `void`.** Nothing is assigned back. The list itself is reordered.
- **`Sort` is not stable; `OrderBy` is.** This is why `ThenBy` chains work.
- **`OrderBy` is deferred.** Writing `var q = numbers.OrderBy(...)` does nothing until you enumerate `q`.
- **`Sort` is faster for simple types.** No delegate call per comparison, no buffer allocation.
- **`Sort` requires `T` to be `IComparable<T>`** if no custom comparer is passed. `int`, `string`, `DateTime` all are. Custom classes need to implement it, or you must pass a comparer.
- **`OrderBy` requires `TKey` to be `IComparable<TKey>`.** The key selector produces `TKey`; LINQ uses `Comparer<TKey>.Default` to sort by it.
- **`OrderBy` can be used on any `IEnumerable<T>`.** `Sort` is only on `List<T>`.
- **`numbers.Sort(); numbers.Reverse();`** is ascending + reverse, which gives descending. Two passes over the list.
- **Random order:** `OrderBy(_ => Guid.NewGuid())`. No dedicated `Random` operator in LINQ.

---

📌 **Summary**

| Question                      | Answer                                                |
| ----------------------------- | ----------------------------------------------------- |
| `Sort` vs `OrderBy` execution | `Sort` immediate; `OrderBy` deferred                  |
| Mutates source?               | `Sort` yes; `OrderBy` no                              |
| Return type                   | `Sort` → `void`; `OrderBy` → `IOrderedEnumerable<T>`  |
| Time complexity               | Both O(n log n)                                       |
| Space complexity              | `Sort` O(log n); `OrderBy` O(n)                       |
| Stable?                       | `Sort` no; `OrderBy` yes                              |
| Works on                      | `Sort` only `List<T>`; `OrderBy` any `IEnumerable<T>` |
| Descending                    | `Sort` + `Reverse`; or `OrderByDescending`            |
| Custom rule                   | `Sort` comparer delegate; `OrderBy` key selector      |
| Random order                  | `OrderBy(_ => Guid.NewGuid())`                        |
| SQL analogy                   | `OrderBy(n => key)` = `ORDER BY key`                  |

---
