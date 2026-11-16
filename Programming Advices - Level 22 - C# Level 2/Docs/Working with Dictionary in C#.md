# Working with Dictionary in C#

## 1. Core Operations

### 1.1 Creating

```csharp
Dictionary<string, int> fruitBasket = new Dictionary<string, int>();
```

Keys are `string` (fruit name), values are `int` (quantity).

### 1.2 Adding

```csharp
fruitBasket.Add("Apple", 5);
fruitBasket.Add("Banana", 2);
fruitBasket.Add("Orange", 12);
```

`Add` **<mark>throws</mark>** `ArgumentException` if the key already exists. To insert-or-update, use the indexer:

```csharp
fruitBasket["Apple"] = 10;  // adds if missing, updates if present
```

### 1.3 Accessing

```csharp
int appleQuantity = fruitBasket["Apple"];
```

**<mark>Throws</mark>** `KeyNotFoundException` if the key is missing. To avoid the exception, **<mark>use</mark>** `TryGetValue` or `ContainsKey`.

### 1.4 Checking existence

```csharp
if (fruitBasket.ContainsKey("Apple"))
{
    // ...
}
```

`ContainsKey` is O(1). For get-or-default, **<mark>prefer</mark>** `TryGetValue` — one lookup instead of two:

```csharp
if (fruitBasket.TryGetValue("Apple", out int qty))
{
    Console.WriteLine(qty);
}
```

### 1.5 Removing

```csharp
fruitBasket.Remove("Banana");  // returns bool: true if found and removed
```

### 1.6 Iterating

```csharp
foreach (KeyValuePair<string, int> item in fruitBasket)
{
    Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");
}
```

The `var` shorthand works and is equivalent:

```csharp
foreach (var item in fruitBasket)
{
    Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");
}
```

`var` does not remove the `KeyValuePair<TKey, TValue>` type — **<mark>it infers it</mark>**. The element is still a `KeyValuePair`; you just do not spell it out. If you **<mark>only need</mark>** keys or values, use the `Keys` or `Values` collections directly:

```csharp
foreach (var key in fruitBasket.Keys) { ... }
foreach (var value in fruitBasket.Values) { ... }
```

### 1.7 Keys and Values via deconstruction (C# 7+)

```csharp
foreach (var (fruit, qty) in fruitBasket)
{
    Console.WriteLine($"{fruit}: {qty}");
}
```

`KeyValuePair<TKey, TValue>` has a `Deconstruct` method, so this works directly. **<mark>Cleanest form</mark>** for iteration.

---

## 2. Full Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>();

        fruitBasket.Add("Apple", 5);
        fruitBasket.Add("Banana", 2);
        // fruitBasket.Add("Banana", 5);  // throws ArgumentException
        fruitBasket.Add("Orange", 12);

        fruitBasket["Apple"] = 10;

        Console.WriteLine("\nDictionary Content:");
        foreach (KeyValuePair<string, int> item in fruitBasket)
        {
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");
        }

        fruitBasket.Remove("Banana");

        Console.WriteLine("\nDictionary Content after removing Banana:");
        foreach (KeyValuePair<string, int> item in fruitBasket)
        {
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");
        }

        Console.ReadKey();
    }
}
```

**Output (order may vary)**

```
Dictionary Content:
Fruit: Apple, Quantity: 10
Fruit: Banana, Quantity: 2
Fruit: Orange, Quantity: 12

Dictionary Content after removing Banana:
Fruit: Apple, Quantity: 10
Fruit: Orange, Quantity: 12
```

`Dictionary<TKey, TValue>` preserves insertion order in practice (since .NET Core 3.0), but the API contract does not guarantee it. **<mark>Do not depend on it</mark>**.

---

## 3. Word Frequency: Dictionary in Practice

The classic use case: count how many times each word appears in a string. Split the text, then count with a dictionary where the word is the key and the count is the value.

### 3.1 With a loop

```csharp
string text = "the quick brown fox jumps over the lazy dog the fox";
string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

Dictionary<string, int> counts = new Dictionary<string, int>();

foreach (string word in words)
{
    if (counts.ContainsKey(word))
    {
        counts[word]++;
    }
    else
    {
        counts[word] = 1;
    }
}

foreach (var (word, count) in counts)
{
    Console.WriteLine($"{word}: {count}");
}
```

`TryGetValue` **<mark>removes the double lookup</mark>**:

```csharp
foreach (string word in words)
{
    counts.TryGetValue(word, out int current);
    counts[word] = current + 1;
}
```

### 3.2 With LINQ

```csharp
var counts = words
    .GroupBy(w => w)
    .ToDictionary(g => g.Key, g => g.Count());
```

`GroupBy` produces **<mark>one group per distinct word</mark>**; `ToDictionary` turns each group into a key-value pair. **<mark><u>Same result</u>, <u>one expression</u></mark>**.

### 3.3 Which is better

| Approach                        | Readability          | Performance                                                           |
| ------------------------------- | -------------------- | --------------------------------------------------------------------- |
| `foreach` + `TryGetValue`       | Verbose, explicit    | **<mark>Fastest</mark>** — single pass, no intermediate allocations   |
| LINQ `GroupBy().ToDictionary()` | Concise, declarative | **<mark>Slightly slower</mark>** — builds groups first, then converts |

Both are correct. The loop is **<mark>faster and easier</mark>** to step through. LINQ is **<mark>shorter and composes better</mark>** with other queries. In hot paths, use the loop. In one-off code, LINQ is fine.

---

## 4. Notes

- **`Add` throws on duplicate key.** Use the indexer for insert-or-update.
- **Indexer throws `KeyNotFoundException` on missing key.** `Hashtable` returns `null` instead; `Dictionary` is the stricter type.
- **`TryGetValue` is the safe accessor.** One lookup, no exception. Preferred over `ContainsKey` + indexer (which is two lookups).
- **`var` in `foreach` does not change the type.** It infers `KeyValuePair<TKey, TValue>`. It only saves typing.
- **Deconstruction works.** `foreach (var (key, value) in dict)` is the cleanest iteration form in C# 7+.
- **`Keys` and `Values` are views.** `dict.Keys` and `dict.Values` are live collections over the dictionary, not copies. Mutating the dictionary during iteration over either throws `InvalidOperationException`.
- **Order is not part of the contract.** In practice, `Dictionary` preserves insertion order in .NET Core 3.0+, but treat that as an implementation detail.
- **Not thread-safe.** For concurrent access, use `ConcurrentDictionary<TKey, TValue>`.

---

📌 **Summary**

| Operation             | Method                                     | Notes                                    |
| --------------------- | ------------------------------------------ | ---------------------------------------- |
| Add new               | `dict.Add(key, value)`                     | Throws on duplicate key                  |
| Add or update         | `dict[key] = value`                        | Safe for both cases                      |
| Read                  | `dict[key]`                                | Throws `KeyNotFoundException` if missing |
| Safe read             | `dict.TryGetValue(key, out var v)`         | One lookup, no exception                 |
| Check existence       | `dict.ContainsKey(key)`                    | O(1)                                     |
| Remove                | `dict.Remove(key)`                         | Returns `bool`                           |
| Iterate (typed)       | `foreach (KeyValuePair<K,V> item in dict)` |                                          |
| Iterate (`var`)       | `foreach (var item in dict)`               | Same element type, inferred              |
| Iterate (keys only)   | `foreach (var k in dict.Keys)`             | View, not a copy                         |
| Iterate (deconstruct) | `foreach (var (k, v) in dict)`             | Cleanest — C# 7+                         |

---
