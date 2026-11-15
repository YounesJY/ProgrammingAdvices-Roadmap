# Working with `Hashtable` in C#

## 1. Basic Operations

### 1.1 Setup

Add `using System.Collections;` at the top of the file.

```csharp
Hashtable myHashtable = new Hashtable();
```

### 1.2 Populating

```csharp
myHashtable.Add("key1", "value1");
myHashtable.Add("key2", 100);       // Mixed value types allowed
myHashtable.Add("key1", "value3");  // Throws ArgumentException: duplicate key
```

`Add` throws if the key already exists. Use the indexer (`myHashtable["key1"] = ...`) to overwrite.

### 1.3 Accessing

```csharp
string value = (string)myHashtable["key1"];
```

The indexer returns `object`. **<mark>Casting is required</mark>**. If the key is missing, the indexer returns `null`, and the cast throws `NullReferenceException` on the unbox (or a `NullReferenceException` on the string usage). **<mark>No</mark>** `KeyNotFoundException` here — `Hashtable` differs from `Dictionary` on this point.

### 1.4 Modifying and Removing

```csharp
myHashtable["key1"] = "newValue1";
myHashtable.Remove("key2");
```

**<mark>Both are O(1) average</mark>**.

### 1.5 Iterating

```csharp
foreach (DictionaryEntry entry in myHashtable)
{
    Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
}
```

`Hashtable`'s **<mark>enumerator yields</mark>** `DictionaryEntry`, a struct with `Key` and `Value` as `object`. The **<mark>same `_version` guard applies</mark>** — mutating during enumeration **<mark>throws</mark>** `InvalidOperationException`.

---

## 2. Full Example

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        Hashtable myHashtable = new Hashtable();
        myHashtable.Add("key1", "value1");
        myHashtable.Add("key2", "value2");
        myHashtable.Add("key3", "value3");

        Console.WriteLine($"Accessing key1: {myHashtable["key1"]}");

        myHashtable["key1"] = "newValue1";
        myHashtable.Remove("key2");

        Console.WriteLine("\nCurrent Hashtable contents:");
        foreach (DictionaryEntry entry in myHashtable)
        {
            Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        }

        Console.ReadKey();
    }
}
```

**Output (order may vary — `Hashtable` <mark>does not preserve insertion order</mark>)**

```
Accessing key1: value1

Current Hashtable contents:
Key: key3, Value: value3
Key: key1, Value: newValue1
```

The exact order **<mark>depends on the hash codes</mark>** and the internal bucket layout. It **<mark>can change</mark>** between runs, between .NET versions, and between machines. Do not rely on it.

---

## 3. On the Pasted Note: Boxing in This Example

> "But string literals are of type String, which is a reference type, therefore, this example **<mark>doesn't involve boxing</mark>**, it's just up-casting. If the values were of type int, then yes, this is boxing."

The note is correct, and the distinction is **<mark>worth being precise about</mark>**.

### 3.1 `Hashtable` stores everything as `object`

`Hashtable` is **<mark>a non-generic collection</mark>**. Its `Add` method signature is:

```csharp
public virtual void Add(object key, object value);
```

**<mark>Both parameters are</mark>** `object`. Every insertion goes through an implicit conversion to `object`.

### 3.2 Reference-type elements: upcast, no boxing

```csharp
myHashtable.Add("key1", "value1");    // string → object
myHashtable.Add("key2", somePerson);  // Person → object
```

`string` and `Person` are reference types. Converting a reference type to `object` **<mark><u>reuses the same reference</u></mark>**. No allocation, no copy. **<mark><u>That is an upcast</u></mark>**.

### 3.3 Value-type elements: boxing

```csharp
myHashtable.Add("key2", 100);         // int → object
```

`int` is a value type. Converting a value type to `object` allocates a boxed `int` on the heap and copies the value into it. **<mark>That is boxing</mark>**.

### 3.4 Both at once in the same example

```csharp
myHashtable.Add("key1", "value1");    // string → upcast
myHashtable.Add("key2", 100);         // int → boxing
myHashtable.Add("key3", "value3");    // string → upcast
```

The original example in the source has `Add("key2", 100)`. That line **does** box. If you removed it and kept only strings, the example would involve no boxing at all.

### 3.5 Retrieval is the reverse

```csharp
string s = (string)myHashtable["key1"];  // downcast, no unboxing
int n    = (int)myHashtable["key2"];     // unboxing
```

`"key1"` was stored as a boxed... no, as an upcast reference. The cast back to `string` **<mark>is a downcast</mark>** — **<mark><u>no unboxing needed</u></mark>** because the underlying object is a `string` reference.

`"key2"` was stored as a boxed `int`. The cast back to `int` **<mark>is an unboxing</mark>** — type check + copy out of the boxed object.

### 3.6 The precise rule for `Hashtable`

`Hashtable` **<mark>boxes</mark>** **value-type elements** (keys and values). It **<mark>does not box</mark>** reference-type elements. Both go through the same **<mark>implicit conversion</mark>** to `object`, but only one pays the allocation cost.

The note in the pasted comment says "just up-casting" for the string case. **<mark>That is accurate</mark>**. And "if the values were of type `int`, then yes, this is boxing" is also accurate. The clarification: **the example as written does both, because `key2` has the `int` value `100` in one of the snippets.**

---

## 4. `Hashtable` vs `Dictionary<TKey,TValue>`

| Aspect                         | `Hashtable`                | `Dictionary<TKey,TValue>`                |
| ------------------------------ | -------------------------- | ---------------------------------------- |
| Namespace                      | `System.Collections`       | `System.Collections.Generic`             |
| Type safety                    | None (`object`)            | Strong (`TKey`, `TValue`)                |
| Value-type boxing              | Yes                        | No                                       |
| Missing key access via indexer | Returns `null`             | Throws `KeyNotFoundException`            |
| Duplicate key via `Add`        | Throws `ArgumentException` | Throws `ArgumentException`               |
| Iteration element              | `DictionaryEntry`          | `KeyValuePair<TKey,TValue>`              |
| Order                          | No guarantee               | No guarantee                             |
| Thread-safe                    | No                         | No (use `ConcurrentDictionary` for that) |

`Dictionary<TKey,TValue>` is the modern default. `Hashtable` exists for .NET 1.0 **<mark>compatibility</mark>** and for APIs that require it.

---

## 5. Notes

- **`Add` vs indexer.** `Add` throws on duplicate key; the indexer overwrites.
- **Missing key via indexer returns `null`.** This differs from `Dictionary`, which throws. It also means you cannot distinguish "key missing" from "key present with `null` value".
- **Casting is required on retrieval.** The result is `object`; cast to the expected type.
- **Boxing depends on the element type.** Value types box on insert and unbox on retrieval; reference types do neither.
- **Order is unspecified.** The output in the source shows `key3` before `key1`; that is incidental. The order is not guaranteed and should not be relied on.
- **Iteration yields `DictionaryEntry`.** That is a struct wrapping `Key` and `Value` as `object`.
- **Mutation during iteration throws.** Same `_version` guard as `List<T>`.

---

📌 **Summary**

| Question                  | Answer                                             |
| ------------------------- | -------------------------------------------------- |
| Namespace                 | `System.Collections`                               |
| Stores elements as        | `object`                                           |
| `Add` on duplicate key    | Throws `ArgumentException`                         |
| Indexer on missing key    | Returns `null` (unlike `Dictionary`, which throws) |
| Iteration element type    | `DictionaryEntry`                                  |
| Order preserved?          | No                                                 |
| Boxing on value types     | Yes (both keys and values)                         |
| Boxing on reference types | No — upcast only                                   |
| Thread-safe               | No                                                 |
| Modern replacement        | `Dictionary<TKey,TValue>`                          |

---
