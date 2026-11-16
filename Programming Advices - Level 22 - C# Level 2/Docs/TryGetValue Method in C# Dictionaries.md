# TryGetValue Method in C# Dictionaries

## 1. What It Does

`TryGetValue` **<mark>safely retrieves a value</mark>** from a dictionary. It returns a `bool` indicating whether the key was found and assigns the value to an `out` parameter if it was.

```csharp
bool TryGetValue(TKey key, out TValue value)
```

- **Returns `true`:** key found, `value` holds the associated value.
- **Returns `false`:** key not found, `value` holds `default(TValue)`.

---

## 2. Why Use It

| Method                                | Lookups | Throws on missing key?                        |
| ------------------------------------- | ------- | --------------------------------------------- |
| `dict[key]`                           | 1       | **<mark>Yes</mark>** — `KeyNotFoundException` |
| `dict.ContainsKey(key)` + `dict[key]` | 2       | No (but two lookups)                          |
| `dict.TryGetValue(key, out var v)`    | 1       | No                                            |

**<mark>Safety</mark>:** No exception on missing key.
**<mark>Efficiency</mark>:** One hash lookup instead of two (`ContainsKey` followed by indexer does the **<mark>lookup twice</mark>**).
**<mark>Clarity</mark>:** The `if` / `else` structure makes the "found or not found" branching explicit.

---

## 3. Example

```csharp
Dictionary<string, int> fruitBasket = new Dictionary<string, int>
{
    { "Apple", 5 },
    { "Banana", 2 }
};

if (fruitBasket.TryGetValue("Apple", out int appleQuantity))
{
    Console.WriteLine($"Apple quantity: {appleQuantity}");
}
else
{
    Console.WriteLine("Apple not found in the basket.");
}
```

The indexer equivalent would throw:

```csharp
// Console.WriteLine($"Orange quantity: {fruitBasket["Orange"]}"); // KeyNotFoundException
```

`TryGetValue` handles that case:

```csharp
if (fruitBasket.TryGetValue("Orange", out int orangeQuantity))
{
    Console.WriteLine($"Orange quantity: {orangeQuantity}");
}
else
{
    Console.WriteLine("Orange not found in the basket.");
}
```

---

## 4. Full Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>
        {
            { "Apple", 5 },
            { "Banana", 2 }
        };

        if (fruitBasket.TryGetValue("Apple", out int appleQuantity))
        {
            Console.WriteLine($"Apple quantity: {appleQuantity}");
        }
        else
        {
            Console.WriteLine("Apple not found in the basket.");
        }

        if (fruitBasket.TryGetValue("Orange", out int orangeQuantity))
        {
            Console.WriteLine($"Orange quantity: {orangeQuantity}");
        }
        else
        {
            Console.WriteLine("Orange not found in the basket.");
        }

        Console.ReadKey();
    }
}
```

**Output**

```
Apple quantity: 5
Orange not found in the basket.
```

---

## 5. When to Use Which

| Goal                                                    | Use                                             |
| ------------------------------------------------------- | ----------------------------------------------- |
| **<mark>Both</mark>** the value and the existence check | `TryGetValue` — one lookup                      |
| **<mark>Only</mark>** the existence check               | `ContainsKey`                                   |
| **<mark>Only</mark>** the value, key guaranteed         | `dict[key]`                                     |
| Value or fallback                                       | `TryGetValue`, or `GetValueOrDefault` (C# 7.1+) |

### 5.1 The `TryGetValue` + `++` pattern

The classic insert-or-increment use case:

```csharp
if (fruitBasket.TryGetValue("Apple", out int current))
    fruitBasket["Apple"] = current + 1;
else
    fruitBasket["Apple"] = 1;
```

Or shorter (C# 7.1+):

```csharp
fruitBasket["Apple"] = fruitBasket.GetValueOrDefault("Apple") + 1;
```

`GetValueOrDefault` returns `default(TValue)` (`0` for `int`) when the key is missing, so the addition **<mark>always works</mark>**.

### 5.2 `ContainsKey` for guarded insert

When you only need to prevent an `Add` from throwing:

```csharp
if (!fruitBasket.ContainsKey("Apple"))
    fruitBasket.Add("Apple", 5);
```

Here you **<mark>do not need</mark>** the value, so `TryGetValue` is unnecessary. `ContainsKey` is the right tool. Two lookups only if you then read `dict[key]`; not the case here.

---

## 6. Notes

- **One lookup vs two.** `TryGetValue` is the efficient option when you need both the value and the existence check.
- **`out` variable is always assigned.** On `false`, it gets `default(TValue)` — `0` for `int`, `null` for reference types, `default` for structs. The variable is definitely assigned either way.
- **C# 7 `out var`.** `out int appleQuantity` declares the variable inline. Before C# 7, you had to declare it first.
- **Discard when the value is irrelevant.** `dict.TryGetValue(key, out _)` checks existence without holding the value.
- **`GetValueOrDefault` (C# 7.1+).** For the "value or fallback" case without branching. One lookup, no `out` parameter.
- **Not thread-safe.** Same as the rest of `Dictionary`. Use `ConcurrentDictionary` for concurrent access.

---

📌 **Summary**

| Question                                        | Answer                                                |
| ----------------------------------------------- | ----------------------------------------------------- |
| What does `TryGetValue` return?                 | `bool` — `true` if key found                          |
| What is the `out` parameter on failure?         | `default(TValue)` — `0`, `null`, or `default(struct)` |
| Why prefer it over `ContainsKey` + `dict[key]`? | One lookup instead of two                             |
| When to use `ContainsKey`?                      | When only existence matters (e.g., before `Add`)      |
| When to use `dict[key]`?                        | When the key is guaranteed to exist                   |
| C# 7 syntax                                     | `if (dict.TryGetValue(key, out var value))`           |
| C# 7.1 fallback syntax                          | `dict.GetValueOrDefault(key)`                         |
| Thread safety                                   | Not thread-safe; use `ConcurrentDictionary`           |

---
