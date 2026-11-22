# Difference Operation with HashSet

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

        // Removing elements of set2 from set1
        set1.ExceptWith(set2);

        Console.WriteLine("Difference of sets (set1 - set2):");
        foreach (int item in set1)
        {
            Console.WriteLine(item);
        }
        
        Console.ReadKey();
    }
}
```

**Output**

```
Difference of sets (set1 - set2):
1
2
```

The output includes `1` and `2` — the elements present in `set1` but **<mark>not</mark>** in `set2`. `ExceptWith` removes any element from `set1` that **<mark>also exists</mark>** in `set2`.

---

## 2. The Four Mutating Set Operations

`ExceptWith` is one of four instance methods on `HashSet<T>` that modify the set **<mark>in place</mark>**. They all return `void`.

| Method                           | Operation                                                       | `set1 = {1, 2, 3, 4}`, `set2 = {3, 4, 5, 6}` → result |
| -------------------------------- | --------------------------------------------------------------- | ----------------------------------------------------- |
| `set1.UnionWith(set2)`           | **<mark>All</mark>** unique elements **<mark>from both</mark>** | `{1, 2, 3, 4, 5, 6}`                                  |
| `set1.IntersectWith(set2)`       | **<mark>Only</mark>** elements in **<mark>both</mark>**         | `{3, 4}`                                              |
| `set1.ExceptWith(set2)`          | Elements in `set1` **<mark>not in</mark>** `set2`               | `{1, 2}`                                              |
| `set1.SymmetricExceptWith(set2)` | Elements in **<mark>exactly one of the sets</mark>**            | `{1, 2, 5, 6}`                                        |

### 2.1 `ExceptWith` vs `IntersectWith`

**<mark>They are <u>opposites</u></mark>**:

- **`IntersectWith`** **<mark><u>keeps</u> only what is <u>shared</u></mark>**.
- **`ExceptWith`** **<mark><u>deletes</u> what is <u>shared</u></mark>**.

Both mutate the set they are called on (`set1`). `set2` **is <mark>never modified</mark>**.

---

## 3. Preserving the Original Set

If you want the result **<mark>without destroying the original</mark>** `set1`, you must **<mark>create a copy</mark>** first:

```csharp
HashSet<int> result = new HashSet<int>(set1);
result.ExceptWith(set2);

// set1 remains {1, 2, 3, 4}
// result is {1, 2}
```

Alternatively, use the **<mark>LINQ extension methods</mark>**, which **<mark>do not mutate</mark>** the original set and return an `IEnumerable<T>`:

```csharp
var result = set1.Except(set2); // Returns IEnumerable<int>
```

**<mark><u>Notice</u></mark>** the naming pattern: methods ending in `With` **<mark>mutate</mark>** the `HashSet`; methods without `With` are LINQ extensions.

---

## 4. Building Except from Scratch

To build the difference operation manually, you iterate the first set and check if the item is **<mark>absent</mark>** from the second set:

```csharp
public static HashSet<T> ManualExcept<T>(HashSet<T> set1, HashSet<T> set2)
{
    HashSet<T> result = new HashSet<T>();
    
    foreach (T item in set1)
    {
        if (!set2.Contains(item)) // O(1) check
        {
            result.Add(item);
        }
    }
    
    return result;
}
```

This runs in O(n) time because `HashSet<T>.Contains` is O(1).

---

📌 **Summary**

| Question                      | Answer                                                             |
| ----------------------------- | ------------------------------------------------------------------ |
| What does `ExceptWith` do?    | Removes elements from the current set that exist in the target set |
| Does it mutate the original?  | Yes (`set1` is modified, `set2` is not)                            |
| Does it return anything?      | `void`                                                             |
| How to preserve the original? | Copy it first: `new HashSet<T>(set1)`                              |
| LINQ alternative?             | `set1.Except(set2)` (returns `IEnumerable<T>`, does not mutate)    |
| Complexity                    | O(n) — uses hash lookups                                           |
| Naming rule                   | `With` = mutates `HashSet`; no `With` = LINQ (read-only)           |

---


