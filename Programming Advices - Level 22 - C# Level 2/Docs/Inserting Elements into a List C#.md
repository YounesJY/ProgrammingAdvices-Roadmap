# Inserting Elements into a `List<int>` in C#

## 1. Setup

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

Collection initializer syntax: the compiler calls `Add` once per element.

---

## 2. Insertion Methods

| Method                           | Purpose                               | Cost                                    |
| -------------------------------- | ------------------------------------- | --------------------------------------- |
| `Add(item)`                      | Append to end                         | O(1) amortized                          |
| `Insert(index, item)`            | Insert at a specific index            | O(n) — shifts elements right of `index` |
| `InsertRange(index, collection)` | Insert a sequence at a specific index | O(n + k) — shifts + copies `k` items    |

---

## 3. Examples

### 3.1 Add at the end

```csharp
numbers.Add(11);
```

Appends `11`. `Count` grows by one. When the internal array is full, it grows (roughly doubles) and copies.

### 3.2 Insert at a specific index

```csharp
numbers.Insert(0, 0);
```

Inserts `0` at index 0. Every element from index 0 onward shifts one slot right. `Insert` with index equal to `Count` behaves like `Add`.

### 3.3 Insert multiple elements

```csharp
numbers.InsertRange(5, new List<int> { 55, 56 });
```

Inserts `55` and `56` starting at index 5. The first inserted element lands at index 5, the second at index 6, and everything originally at index 5 and beyond shifts right by two.

---

## 4. Full Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        numbers.Add(11);
        Console.WriteLine("After adding 11: " + string.Join(", ", numbers));

        numbers.Insert(0, 0);
        Console.WriteLine("After inserting 0 at the beginning: " + string.Join(", ", numbers));

        numbers.InsertRange(5, new List<int> { 55, 56 });
        Console.WriteLine("After inserting 55 and 56 at index 5: " + string.Join(", ", numbers));

        Console.ReadKey();
    }
}
```

**Output**

```
After adding 11: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
After inserting 0 at the beginning: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
After inserting 55 and 56 at index 5: 0, 1, 2, 3, 4, 55, 56, 5, 6, 7, 8, 9, 10, 11
```

---

## 5. Notes

- **`Add` <mark>is cheap</mark>; `Insert` and `InsertRange` <mark>are not</mark>.** Both shift every element after the insertion point. For bulk prepends or middle inserts, consider a different structure (`LinkedList<T>`, or build then reverse).
- **`Insert(index, item)` accepts `index == Count`.** It behaves like `Add`.
- **`Insert` with `index > Count` or `< 0` throws `ArgumentOutOfRangeException`.**
- **`InsertRange` copies from the source collection first**, so passing the same list to itself (`numbers.InsertRange(2, numbers)`) is safe — it does not iterate while mutating.
- **`string.Join(", ", numbers)`** calls `ToString` on each element and joins them. This is why the output looks like a comma-separated list.

---

📌 **Summary**

| Method             | Signature                                | Cost           | Behavior                                    |
| ------------------ | ---------------------------------------- | -------------- | ------------------------------------------- |
| `Add`              | `Add(T item)`                            | O(1) amortized | Append to end                               |
| `Insert`           | `Insert(int index, T item)`              | O(n)           | Shift right from `index` and place item     |
| `InsertRange`      | `InsertRange(int index, IEnumerable<T>)` | O(n + k)       | Shift right from `index` and copy `k` items |
| Out-of-range index | `Insert`, `InsertRange`                  | —              | `ArgumentOutOfRangeException`               |
| `index == Count`   | `Insert`                                 | O(1)           | Same effect as `Add`                        |

---
