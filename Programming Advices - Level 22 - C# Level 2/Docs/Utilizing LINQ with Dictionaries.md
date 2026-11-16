# Utilizing LINQ with Dictionaries

## 1. Core Concept

`Dictionary<TKey, TValue>` **<mark>implements</mark>** `IEnumerable<KeyValuePair<TKey, TValue>>`. That is why **<mark>every LINQ operator</mark>** works on it. The elements flowing through the query pipeline are `KeyValuePair<TKey, TValue>` instances.

---

## 2. Basic Operations

### 2.1 Select (Projection)

```csharp
var fruitInfo = fruitBasket.Select(kvp => new { kvp.Key, kvp.Value });
```

Projects each `KeyValuePair` into **<mark>a new shape</mark>** — here, an **<mark><u>anonymous type</u></mark>** with `Key` and `Value` properties.

`Select` is a **<mark>projection</mark>**. It **<mark>does not modify</mark>** `fruitBasket`. It returns a new `IEnumerable<TResult>` that, when enumerated, yields the projected items. This is **<mark>a map function</mark>**, same as `map` in other languages.

`Select` is deferred. Nothing executes until the result is enumerated.

### 2.2 Where (Filtering)

```csharp
var filteredFruit = fruitBasket.Where(kvp => kvp.Value > 3);
```

Filters `KeyValuePair` elements by value. **<mark>Deferred</mark>**. **<mark><u>No mutation</u></mark>**.

### 2.3 OrderBy (Sorting)

```csharp
var sortedByQuantity = fruitBasket.OrderBy(kvp => kvp.Value);
var sortedByQuantityDesc = fruitBasket.OrderByDescending(kvp => kvp.Value);
```

Sort by key or value. **<mark><u>Deferred and stable</u></mark>**. The dictionary itself **<mark>is not</mark>** reordered.

### 2.4 Aggregate (Sum, Max, Min, Average)

```csharp
int totalQuantity = fruitBasket.Sum(kvp => kvp.Value);
```

Aggregates the values. **<mark><u>Immediate</u></mark>** — it runs when called.

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
        Dictionary<string, int> fruitBasket = new Dictionary<string, int>
        {
            { "Apple", 5 },
            { "Banana", 2 },
            { "Orange", 7 }
        };

        var fruitInfo = fruitBasket.Select(kvp => new { kvp.Key, kvp.Value });
        Console.WriteLine("Transformed Items:");
        foreach (var item in fruitInfo)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var filteredFruit = fruitBasket.Where(kvp => kvp.Value > 3);
        Console.WriteLine("\nFiltered Items >3:");
        foreach (var item in filteredFruit)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var sortedByQuantity = fruitBasket.OrderBy(kvp => kvp.Value);
        Console.WriteLine("\nSorted Items:");
        foreach (var item in sortedByQuantity)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        var sortedByQuantityDesc = fruitBasket.OrderByDescending(kvp => kvp.Value);
        Console.WriteLine("\nSorted Items Desc:");
        foreach (var item in sortedByQuantityDesc)
            Console.WriteLine($"Fruit: {item.Key}, Quantity: {item.Value}");

        int totalQuantity = fruitBasket.Sum(kvp => kvp.Value);
        Console.WriteLine($"\nTotal Quantity: {totalQuantity}");

        Console.ReadKey();
    }
}
```

**Output (order may vary for the non-sorted lines)**

```
Transformed Items:
Fruit: Apple, Quantity: 5
Fruit: Banana, Quantity: 2
Fruit: Orange, Quantity: 7

Filtered Items >3:
Fruit: Apple, Quantity: 5
Fruit: Orange, Quantity: 7

Sorted Items:
Fruit: Banana, Quantity: 2
Fruit: Apple, Quantity: 5
Fruit: Orange, Quantity: 7

Sorted Items Desc:
Fruit: Orange, Quantity: 7
Fruit: Apple, Quantity: 5
Fruit: Banana, Quantity: 2

Total Quantity: 14
```

---

## 4. Select is Projection, Not Copy

The pasted notes ask about this:

> `var fruitInfo = fruitBasket.Select(kvp => new KeyValuePair(kvp.Key, kvp.Value));` vs `var fruitInfo = fruitBasket;`

These **<mark>do different things</mark>**, and **<mark><u>neither is a deep copy</u></mark>** of the dictionary.

### 4.1 `var fruitInfo = fruitBasket;`

This copies the **<mark>reference</mark>**. `fruitInfo` and `fruitBasket` **<mark>point to the <u>same dictionary object</u></mark>**. Any mutation via `fruitInfo` **<mark><u>changes</u></mark>** `fruitBasket`.

```csharp
var dict1 = new Dictionary<string, string>();
dict1["apple"] = "red";

var dict2 = dict1;
dict2["apple"] = "green";

Console.WriteLine(dict1["apple"]); // green
```

Same object, two references. Mutation through either is **<mark>visible through both</mark>**.

### 4.2 `var fruitInfo = fruitBasket.Select(kvp => new KeyValuePair(...));`

This creates a **<mark>deferred query</mark>**. Nothing is copied at the moment of the `Select`. The returned `IEnumerable<KeyValuePair<TKey, TValue>>` still reads from `fruitBasket` **<mark>when enumerated</mark>**.

> "Actually, I tried using `select`, but still it is referencing to the main dictionary."

Correct. `Select` **<mark>does not</mark>** copy the data. It projects the *elements* as they are enumerated. If `fruitBasket` **<mark><u>changes between</u></mark>** the `Select` and the `foreach`, the `foreach` sees the new state.

To force a real, independent snapshot, **<mark><u>materialize it</u></mark>**:

```csharp
var fruitInfo = fruitBasket
    .Select(kvp => new KeyValuePair<string, int>(kvp.Key, kvp.Value))
    .ToList();   // now it is an independent list
```

Now `fruitInfo` is a `List<KeyValuePair<string, int>>` holding copies of each pair. Mutating `fruitBasket` afterward **<mark>does not affect it</mark>**.

### 4.3 To copy a dictionary, use the constructor

```csharp
var newDict = new Dictionary<string, int>(fruitBasket);
```

This creates a new `Dictionary` with the same key-value pairs. It is **<mark><u>a shallow copy</u></mark>** — new dictionary object, same key and value references (matters only if `TKey` or `TValue` are reference types). For `Dictionary<string, int>` it is effectively a full copy.

This is the right answer when the goal is "I want a separate dictionary."

### 4.4 When each is appropriate

| Intent                                  | Expression                                             |
| --------------------------------------- | ------------------------------------------------------ |
| Share the dictionary                    | `var b = a;`                                           |
| Deferred query that projects elements   | `a.Select(...)`                                        |
| Materialized list of projected elements | `a.Select(...).ToList()`                               |
| New dictionary with same pairs          | `new Dictionary<K,V>(a)`                               |
| New dictionary with transformed values  | `a.ToDictionary(kvp => kvp.Key, kvp => kvp.Value * 2)` |

---

## 5. Select Returns `IEnumerable<T>` — Not `IQueryable<T>`

One of the pasted comments claims `squares` is `IQueryable<int>`. **<mark>It is not</mark>**. The correction in the last comment is right.

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4 };
var squares = numbers.Select(x => x * x);
```

`numbers` is a `List<int>`. The `Select` overload that applies is `Enumerable.Select`, which returns `IEnumerable<int>`, **<mark>not</mark>** `IQueryable<int>`.

`IQueryable<T>` comes from `Queryable.Select` and requires the source to implement `IQueryable<T>` — which `List<T>` does not. EF sources (`DbSet<T>`) do, and there `Select` returns `IQueryable<T>`.

The deferred-execution behavior is shared by both interfaces, but **<mark>they are not the same</mark>**:

| Interface        | Source                           | Translates to                       |
| ---------------- | -------------------------------- | ----------------------------------- |
| `IEnumerable<T>` | In-memory collections            | Runs in-process                     |
| `IQueryable<T>`  | EF, LINQ-to-SQL, other providers | Translates to SQL (or other target) |

**<mark>Both are deferred</mark>**. **<mark>Both run when enumerated</mark>**. Only `IQueryable<T>` translates to a remote query language.

---

## 6. Notes

- **`Dictionary<TKey, TValue>` is `IEnumerable<KeyValuePair<TKey, TValue>>`.** That is why every LINQ operator works on it.
- **`Select` is projection, not copy.** It projects elements as they are enumerated; it does not snapshot the source.
- **`var b = a;` is a reference copy, not a value copy.** Both variables point to the same dictionary.
- **To copy a dictionary:** `new Dictionary<K, V>(a)`.
- **To snapshot a query:** `.ToList()` (or `.ToArray()`).
- **`Select` on `List<T>` returns `IEnumerable<T>`.** `Select` returns `IQueryable<T>` only when the source is `IQueryable<T>` (EF, LINQ-to-SQL).
- **`Select`, `Where`, `OrderBy` are deferred.** `Sum`, `Max`, `Count` are immediate.
- **Order not guaranteed on the source.** `Dictionary` order is an implementation detail; `OrderBy` produces a new ordered sequence.
- **LINQ with `Dictionary` is one of the few places where `Select(kvp => ...)` is idiomatic.** The `KeyValuePair` projection is common enough that C# 7 deconstruction is often cleaner: `foreach (var (key, value) in query)`.

---

📌 **Summary**

| Question                                       | Answer                                                             |
| ---------------------------------------------- | ------------------------------------------------------------------ |
| What does LINQ iterate over in a `Dictionary`? | `KeyValuePair<TKey, TValue>` elements                              |
| Does `Select` copy the dictionary?             | No — it projects elements lazily                                   |
| Does `Select` reference the source?            | Yes — deferred, reads the source on enumeration                    |
| How to copy a dictionary?                      | `new Dictionary<K,V>(source)`                                      |
| How to snapshot a query?                       | `.ToList()`                                                        |
| Does `Select` return `IQueryable<T>`?          | No — `IEnumerable<T>` for in-memory sources                        |
| When does `IQueryable<T>` appear?              | When the source is EF or another `IQueryable<T>` provider          |
| Deferred vs immediate                          | `Select`/`Where`/`OrderBy` deferred; `Sum`/`Count`/`Max` immediate |

---
