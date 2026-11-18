# Union Operation with HashSet

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

        // Union of set1 and set2
        set1.UnionWith(set2);

        Console.WriteLine("Union of sets:");
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
1
2
3
4
5
```

The duplicate `3` **<mark>appears only once</mark>**, because a `HashSet<T>` guarantees uniqueness.

---

## 2. `UnionWith` vs `Union`

When combining two sets, you have **<mark><u>two choices</u></mark>**: the `HashSet<T>` instance method or the LINQ extension method.

| Feature                | `.Union()` (LINQ)                                                         | `.UnionWith()` (HashSet)                                          |
| ---------------------- | ------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| **Modifies original?** | ❌ No (creates new collection)                                             | ✅ **<mark>Yes</mark>** (modifies in place)                        |
| **Return type**        | `IEnumerable<T>`                                                          | `void`                                                            |
| **Performance**        | Slower (allocates new memory)                                             | **<mark><u>Faster</u></mark>** (in-place modification)            |
| **Works on**           | **<mark><u>Any</u></mark>** `IEnumerable<T>` (List, Array, HashSet, etc.) | Only `HashSet<T>`                                                 |
| **Use case**           | When you **<mark>need a new collection</mark>**                           | When you **<mark>want to modify an existing `HashSet<T>`</mark>** |

### 2.1 `set1.UnionWith(set2)` (HashSet method)

- **<mark>Mutates</mark>** `set1` directly.
- Returns `void` (nothing to assign).
- `set2` is **<mark>left unchanged</mark>**.

### 2.2 `set1.Union(set2)` (LINQ method)

- **<mark>Does not modify</mark>** `set1` or `set2`.
- Returns a new `IEnumerable<T>` sequence that you must enumerate.
- Works **<mark>on any collection</mark>**, not just `HashSet<T>`.

```csharp
var union = set1.Union(set2); // Returns IEnumerable<int>
// set1 is still { 1, 2, 3 }
```

---

## 3. What Collections Support Union?

Because `Union` is a LINQ extension method, it works with **any** type that implements `IEnumerable<T>`. The pasted comment asks if it works everywhere — **<mark><u>the answer is yes</u></mark>**, as long as the collection can be iterated. 

This includes:

- Arrays (`int[]`)
- Lists (`List<T>`)
- Sets (`HashSet<T>`, `SortedSet<T>`)
- Dictionaries (`Dictionary<TKey, TValue>`, though it unions the KeyValuePairs)
- Custom collections implementing `IEnumerable<T>`

---

## 4. How Union Works Internally

The LINQ `Union()` method **<mark>uses</mark>** a `HashSet<T>` internally. 

1. It creates an empty `HashSet<T>`.
2. It loops through the first sequence, adding each item (the set silently ignores duplicates).
3. It loops through the second sequence, adding each item (again, duplicates ignored).
4. It yields the items in the set.

This ensures the result **<mark>contains only unique elements</mark>** and runs in **<mark>O(n + m)</mark>** time (where n and m are the sizes of the two collections).

---

📌 **Summary**

| Question                                         | Answer                                                    |
| ------------------------------------------------ | --------------------------------------------------------- |
| What does `UnionWith` do?                        | Combines another collection into the current `HashSet<T>` |
| Does `UnionWith` return anything?                | No (`void`)                                               |
| Does `UnionWith` modify the original?            | Yes, the `HashSet<T>` is mutated in place                 |
| What is the LINQ alternative?                    | `.Union()`                                                |
| Does `.Union()` modify the original?             | No, it returns a new `IEnumerable<T>`                     |
| Does `.Union()` work on non-HashSet collections? | Yes, any `IEnumerable<T>`                                 |
| Under the hood?                                  | Both use hash-based lookups to ensure uniqueness in O(n)  |

---


