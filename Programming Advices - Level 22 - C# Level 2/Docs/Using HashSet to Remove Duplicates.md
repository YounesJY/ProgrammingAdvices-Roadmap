# Using HashSet to Remove Duplicates

## 1. The Basic Approach

The `HashSet<T>` constructor **<mark>can take any</mark>** `IEnumerable<T>` (including arrays and lists) and **<mark><u>automatically</u> filter out duplicates</mark>**.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] array = new int[] { 1, 2, 2, 3, 4, 4, 5 };

        // Initialize the HashSet with the array
        HashSet<int> uniqueNumbers = new HashSet<int>(array);
        foreach (int number in uniqueNumbers)
            Console.WriteLine(number);

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

The set iterates through the array, attempts to `Add` each element, and **<mark><u>silently</u> ignores the duplicates</mark>**. 

---

## 2. Comparing Approaches: Loop vs LINQ vs HashSet

There are **<mark>three common ways</mark>** to remove duplicates from a collection. 

### 2.1 The Manual Way (O(n²))

Without hash sets, you have to **<mark>keep a separate array of unique items</mark>** and check every new item against it. This **<mark>requires a nested loop</mark>** and is **<mark><u>slow for large collections</u></mark>**.

```csharp
int[] uniqueArray = new int[array.Length];
int uniqueIndex = 0;

for (int i = 0; i < array.Length; i++)
{
    bool isDuplicate = false;
    for (int j = 0; j < uniqueIndex; j++)
    {
        if (array[i] == uniqueArray[j])
        {
            isDuplicate = true;
            break;
        }
    }
    if (!isDuplicate)
    {
        uniqueArray[uniqueIndex] = array[i];
        uniqueIndex++;
    }
}
```

### 2.2 LINQ (`Distinct()`)

```csharp
int[] uniqueArray2 = array.Distinct().ToArray();
```

`Distinct()` is a LINQ extension method. **<mark>Under the hood</mark>, <mark><u>it uses a</u></mark> `HashSet<T>`** to track what it has already seen. It is **<mark><u>functionally identical</u></mark>** to manually creating a HashSet, but **<mark>reads much cleaner</mark>**.

### 2.3 HashSet (Direct)

```csharp
HashSet<int> uniqueNumbers = new HashSet<int>(array);
```

This creates a set directly. If you need it back as a list or array, you can convert it:

```csharp
List<int> uniqueList = uniqueNumbers.ToList();
int[] uniqueArray3 = uniqueNumbers.ToArray();
```

---

## 3. Converting Back to List (`ToHashSet()` and `Distinct()`)

If you start with a `List<T>` and want to end up with a `List<T>` that has no duplicates, there are **<mark><u>two modern</u>, <u>one-line approaches</u></mark>**:

**Option A: LINQ `Distinct()`**

```csharp
list = list.Distinct().ToList();
```

**Option B: `ToHashSet()` extension**

```csharp
list = list.ToHashSet().ToList();
```

### What is the difference?

- **`Distinct()`** **<mark>is a LINQ method</mark>** (`System.Linq`). It returns an `IEnumerable<T>` **<mark><u>lazily</u></mark>**. Calling `.ToList()` **<mark><u>materializes it</u></mark>**.
- **`ToHashSet()`** is an extension method (added in .NET Core 2.0 / .NET Standard 2.1) located in `System.Collections.Generic`. **<mark>It <u>immediately returns</u></mark>** a new `HashSet<T>`. Calling `.ToList()` then converts that set into a list.

Both methods use a hash-based approach internally, so both run in O(n) time. `list.Distinct().ToList()` is the **<mark><u>most common</u> and <u>readable idiom</u></mark>**.

### The "Same List" Illusion

If you write `list = list.Distinct().ToList();`, you are **not** modifying the original list in place. You are **<mark>creating a new list in memory</mark>** and reassigning the `list` variable to point to it. 

```csharp
List<int> listOfNumbers = new List<int> { 1, 2, 2, 3 };
var originalReference = listOfNumbers; // Points to the first object

listOfNumbers = listOfNumbers.Distinct().ToList(); // Points to a NEW object

Console.WriteLine(originalReference.Count); // 4 (The old list still exists)
Console.WriteLine(listOfNumbers.Count);    // 3 (The new list)
```

If you want to modify the list in place, **<mark>you have to</mark>** clear it and re-add the unique items:

```csharp
var unique = listOfNumbers.Distinct().ToList();
listOfNumbers.Clear();
listOfNumbers.AddRange(unique);
```

---

## 4. The Database Trick (Random Rows)

A pasted comment mentions an "out of the box" trick related to SQL `Guid`. While not directly a C# `HashSet` feature, the underlying concept is the same: **using a hash to assign a random ordering.**

In SQL Server, if you want to get random rows, you can order by a newly generated GUID:

```sql
SELECT TOP 10 * FROM Users ORDER BY NEWID();
```

In C#, you can mimic this on a collection by sorting by a randomly generated Guid:

```csharp
var randomItems = list.OrderBy(x => Guid.NewGuid()).Take(10);
```

This works because `Guid.NewGuid()` **<mark><u>generates a unique hash for every element</u></mark>**, and sorting by it scrambles the order.

---

## 5. Notes

- **Constructor deduplication:** Passing an `IEnumerable<T>` to the `HashSet<T>` constructor is the fastest way to deduplicate.
- **Order is not guaranteed:** A `HashSet<T>` does not preserve the original order of the array or list.
- **LINQ `Distinct()`:** Uses a `HashSet<T>` internally. It is deferred, so you must call `.ToList()` or `.ToArray()` to get a concrete collection.
- **Reassignment vs In-place:** `list = list.Distinct().ToList();` reassigns the reference to a new list. It does not mutate the original list object.
- **`ToHashSet()`:** An extension method in `System.Collections.Generic` (not LINQ) that converts an `IEnumerable<T>` to a `HashSet<T>`.

---

📌 **Summary**

| Method                           | Namespace                    | Returns          | Mutates original?     |
| -------------------------------- | ---------------------------- | ---------------- | --------------------- |
| `new HashSet<T>(collection)`     | `System.Collections.Generic` | `HashSet<T>`     | No                    |
| `collection.Distinct()`          | `System.Linq`                | `IEnumerable<T>` | No                    |
| `collection.Distinct().ToList()` | `System.Linq`                | `List<T>`        | No (returns new list) |
| `collection.ToHashSet()`         | `System.Collections.Generic` | `HashSet<T>`     | No                    |

---


