# Symmetric Difference Operation with HashSet

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
        HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };

        // Symmetric difference between set1 and set2
        set1.SymmetricExceptWith(set2);

        Console.WriteLine("Symmetric difference of sets:");
        foreach (int item in set1)
        {
            Console.WriteLine(item);
        }
        
        Console.ReadKey();
    }
}
```

**Output (order may vary)**

```
Symmetric difference of sets:
1
2
4
5
```

The output includes `1, 2, 4, 5` — the **<mark>elements unique to each set</mark>**, present **<mark>in either</mark>** `set1` or `set2` but **<mark><u>not both</u></mark>**. The shared element `3` is removed.

---

## 2. Visualizing Set Operations

Here is how all four `HashSet` operations map to mathematics and Venn diagrams:

<img src="file:///C:/Users/poste/AppData/Roaming/marktext/images/fedf885ff34a68f5e8bfcdd1f8bdf027c247ca34.png" title="" alt="" width="306" />

| Method                | Math Notation   | Venn Diagram (Shaded Area)                         | Description                         |
| --------------------- | --------------- | --------------------------------------------------:|:----------------------------------- |
| `UnionWith`           | $A \cup B$      | Both circles fully shaded                          | All elements in A, B, or both       |
| `IntersectWith`       | $A \cap B$      | Only the **<mark>overlapping</mark>** middle       | Elements in both A and B            |
| `ExceptWith`          | $A \setminus B$ | Left circle only (minus overlap)                   | Elements in A but not in B          |
| `SymmetricExceptWith` | $A \triangle B$ | Both circles, but **<mark>middle is empty</mark>** | Elements in exactly one of the sets |

---

## 3. Implementation via LINQ

LINQ does **<mark>not</mark>** have a built-in `SymmetricExcept()` method. To achieve the same result without mutating the original sets, **<mark>you <u>combine</u></mark>** `Except` and `Union`.

Mathematically, the symmetric difference is: $(A \cup B) \setminus (A \cap B)$

```csharp
HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4 };
HashSet<int> set2 = new HashSet<int> { 3, 4, 5, 6 };

// Symmetric difference: (set1 - set2) U (set2 - set1)
var symmetricDiff = set1.Except(set2)
                        .Union(set2.Except(set1));

foreach (var item in symmetricDiff)
{
    Console.WriteLine(item);
}
```

Because LINQ's `Except` and `Union` return `IEnumerable<T>`, neither `set1` nor `set2` is modified. The result **<mark>is a deferred query</mark>**.

---

## 4. Building It From Scratch

To manually build the symmetric difference, you use two nested logic blocks: 

1. Add every item from `set1` that **<mark>does not</mark>** exist in `set2`.
2. Add every item from `set2` that **<mark>does not</mark>** exist in `set1`.

```csharp
public static HashSet<T> ManualSymmetricExcept<T>(HashSet<T> set1, HashSet<T> set2)
{
    HashSet<T> result = new HashSet<T>();

    foreach (T item in set1)
    {
        if (!set2.Contains(item)) result.Add(item);
    }

    foreach (T item in set2)
    {
        if (!set1.Contains(item)) result.Add(item);
    }

    return result;
}
```

---

## 5. Real-World Use Case

The classic real-world example of these set operations is a **<mark>social network friend list</mark>**:

- `UnionWith` → See all unique people you and a friend follow combined.
- `IntersectWith` → See **<mark>Mutual Friends</mark>** (you both follow them).
- `ExceptWith` → See **<mark>Suggested Friends</mark>** (people your friend follows, but **<u>you don't</u>**).
- `SymmetricExceptWith` → See people you don't have in common (all friends minus mutuals).

---

📌 **Summary**

| Question                            | Answer                                                       |
| ----------------------------------- | ------------------------------------------------------------ |
| What does `SymmetricExceptWith` do? | Keeps only elements unique to each set, removing shared ones |
| Does it mutate the original?        | Yes (`set1` is modified, `set2` is not)                      |
| Is there a LINQ equivalent?         | No built-in method; use `.Except().Union()`                  |
| How to avoid mutating the original? | Clone first, or use the LINQ combination                     |
| Math equivalent                     | $(A \cup B) \setminus (A \cap B)$                            |

---


