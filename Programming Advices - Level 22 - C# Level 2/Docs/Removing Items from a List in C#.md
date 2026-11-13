# Removing Items from a `List<int>` in C#

## 1. Setup

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
```

---

## 2. Removal Methods

| Method                 | Purpose                                     | Cost                                           |
| ---------------------- | ------------------------------------------- | ---------------------------------------------- |
| `Remove(item)`         | Remove first occurrence of `item`           | O(n) — scans, then shifts                      |
| `RemoveAt(index)`      | Remove element at `index`                   | O(n) — shifts elements left of the gap         |
| `RemoveAll(predicate)` | Remove every element matching the predicate | O(n) — single pass, then one compaction        |
| `Clear()`              | Remove all elements                         | O(n) for reference types, O(1) for value types |

---

## 3. Examples

### 3.1 Remove by value

```csharp
numbers.Remove(5);
```

Finds the first element equal to `5`, shifts everything after it one slot left, and decrements `Count`. If `5` does not exist, nothing happens and the method returns `false`.

### 3.2 Remove by index

```csharp
numbers.RemoveAt(0);
```

Removes the element at index 0 and shifts everything left. Throws `ArgumentOutOfRangeException` if `index < 0` or `index >= Count`.

### 3.3 Remove by predicate

```csharp
numbers.RemoveAll(n => n % 2 == 0);
```

Removes every even number in a single pass. `RemoveAll` **<mark>does not</mark>** delete element-by-element; it walks the list once, **<mark>marks which elements to keep</mark>**, then compacts the array. The order of survivors is preserved.

### 3.4 Clear

```csharp
numbers.Clear();
```

Sets `Count = 0`. The internal array is **<mark>not</mark>** released — `Capacity` **<mark>stays</mark>** at its last value.

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

        numbers.Remove(5);
        Console.WriteLine("After removing 5: " + string.Join(", ", numbers));

        numbers.RemoveAt(0);
        Console.WriteLine("After removing first element: " + string.Join(", ", numbers));

        numbers.RemoveAll(n => n % 2 == 0);
        Console.WriteLine("After removing all even numbers: " + string.Join(", ", numbers));

        numbers.Clear();
        Console.WriteLine("After clearing the list, count: " + numbers.Count);

        Console.ReadKey();
    }
}
```

**Output**

```
After removing 5: 1, 2, 3, 4, 6, 7, 8, 9, 10
After removing first element: 2, 3, 4, 6, 7, 8, 9, 10
After removing all even numbers: 3, 7, 9
After clearing the list, count: 0
```

---

## 5. Notes

### 5.1 Removing the last element is O(1)

```csharp
list.RemoveAt(list.Count - 1);
```

Because `List<T>` is array-backed and tracks `Count`, removing the last element **<mark>needs no shifting</mark>**. Just decrement `Count`. That is O(1). Any other index triggers a leftward shift of every element after it, which is O(n).

### 5.2 `Remove(item)` is O(n), not O(1)

It has to scan for the item first. Even when the item is at index 0, the shift alone is O(n). `Remove` returns `true` if removed, `false` otherwise. There is no `Remove` overload that takes an `IEqualityComparer<T>`, so equality uses `EqualityComparer<T>.Default`.

### 5.3 `RemoveAll` does not delete one at a time

`RemoveAll` walks the list once, marks which elements to keep, then compacts in one pass. The cost is a single O(n) traversal with no repeated shifting. It is not `n × Remove()`.

### 5.4 `Clear()` complexity depends on the element type

```csharp
public void Clear()
{
    if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        Array.Clear(_items, 0, _size);   // reference types: null out slots so GC can collect
    _size = 0;
}
```

- **Value types** (`int`, `struct` without references): `Clear` only resets `Count = 0`. The array contents are left as-is. O(1).
- **Reference types** (or structs containing references): `Clear` **<mark>nulls each slot</mark>** so the GC can collect the objects. O(n).

### 5.5 `Clear()` does not shrink `Capacity`

`Capacity` stays at its last value. The internal array **<mark>is kept</mark>** so the list can be refilled without reallocating. To release the memory, call `list.TrimExcess()`.

### 5.6 `Capacity` growth

An empty `List<T>` has `Capacity = 0`. Adding the first element allocates 4. At 5 elements it grows to 8, at 9 to 16, and so on — doubling each time. `Clear` does not reset this; `TrimExcess` does.

### 5.7 Lists hold references, not copies

For reference-type elements, `List<T>` stores the reference, not the object. Removing an element removes the reference; the object itself lives until the GC collects it (if nothing else holds a reference). Modifying an object through one list is visible through any other list holding the same reference.

---

📌 **Summary**

| Operation               | Syntax                 | Cost | Notes                                 |
| ----------------------- | ---------------------- | ---- | ------------------------------------- |
| Remove by value         | `list.Remove(x)`       | O(n) | First occurrence only; returns `bool` |
| Remove by index         | `list.RemoveAt(i)`     | O(n) | Shifts left; O(1) if `i == Count - 1` |
| Remove by predicate     | `list.RemoveAll(pred)` | O(n) | Single pass + compaction              |
| Clear (value types)     | `list.Clear()`         | O(1) | `Count = 0`; array untouched          |
| Clear (reference types) | `list.Clear()`         | O(n) | Slots nulled for GC; `Count = 0`      |
| Shrink capacity         | `list.TrimExcess()`    | O(n) | Reallocates to `Count`                |
| Capacity growth         | —                      | —    | 0 → 4 → 8 → 16 → … doubling           |
