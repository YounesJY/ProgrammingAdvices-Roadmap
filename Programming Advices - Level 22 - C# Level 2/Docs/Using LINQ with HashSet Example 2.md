# Using LINQ with HashSet Example 2

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        HashSet<string> names = new HashSet<string> { "Alice", "Bob", "Charlie", "Daisy", "Ethan", "Fiona" };

        // Filter names starting with 'C'
        var namesStartingWithC = names.Where(name => name.StartsWith("C"));
        Console.WriteLine("Names Starting with C:");
        foreach (var name in namesStartingWithC)
        {
            Console.WriteLine(name);
        }

        // Filter names longer than 4 characters
        var namesLongerThanFour = names.Where(name => name.Length > 4);
        Console.WriteLine("\nNames Longer Than Four Characters:");
        foreach (var name in namesLongerThanFour)
        {
            Console.WriteLine(name);
        }

        Console.ReadKey();
    }
}
```

**Output (order may vary)**

```
Names Starting with C:
Charlie

Names Longer Than Four Characters:
Alice
Charlie
Daisy
Ethan
Fiona
```

---

## 2. How It Works

- **`HashSet<string>`** enforces uniqueness. If the initializer had `"Alice"` twice, only one would be stored.
- **`Where(...)`** returns a deferred `IEnumerable<string>`. It does not create a new `HashSet<string>`.
- **Predicates** are the same as with any other `IEnumerable<T>`: `StartsWith`, `Length`, and so on.
- **Output order** depends on the internal bucket layout of the `HashSet<string>`. It is not alphabetical, not insertion order. Do not rely on it.

---

## 3. Notes

- **The result is `IEnumerable<string>`, not `HashSet<string>`.** Same as Example 1. If you need a set, call `.ToHashSet()`.
- **String comparison in `StartsWith`** uses `CurrentCulture` by default. For ordinal matching, use `name.StartsWith("C", StringComparison.Ordinal)`. This matters for case and culture sensitivity, but not for `"Charlie"`.
- **No ordering guarantee.** Add `.OrderBy(n => n)` if alphabetical order matters.
- **Deferred execution.** Both `Where` queries run on the `foreach`, not on the assignment line.
- **LINQ over a `HashSet` is the same as LINQ over a `List`.** The source type does not change how `Where` works — it only accepts `IEnumerable<T>`, and `HashSet<T>` is one.

---

📌 **Summary**

| Question                      | Answer                                                                   |
| ----------------------------- | ------------------------------------------------------------------------ |
| What does `Where` do here?    | Filters names by a predicate                                             |
| Return type                   | `IEnumerable<string>`                                                    |
| Order guaranteed?             | No                                                                       |
| Deferred?                     | Yes                                                                      |
| To return a set               | `.ToHashSet()`                                                           |
| Ordinal vs culture comparison | `StartsWith("C")` uses culture; use `StringComparison.Ordinal` for exact |

---
