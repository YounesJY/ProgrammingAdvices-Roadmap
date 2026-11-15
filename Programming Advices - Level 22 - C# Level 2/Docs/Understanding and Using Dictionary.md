# Understanding and Using Dictionary

## 1. What is `Dictionary<TKey, TValue>`?

`Dictionary<TKey, TValue>` is **<mark><u>a generic collection</u></mark>** that **<mark>stores data as key-value pairs</mark>**. It lives in the `System.Collections.Generic` namespace and is **<mark><u>designed for fast lookups by key</u></mark>**.

- **Key-Value Pairs:** Each key is unique and maps to exactly one value.
- **Fast Lookups:** Uses hashing for O(1) average retrieval.
- **Generic:** You specify the types **<mark>for both</mark>** the key and the value.
- **Analogy:** It is **<mark>the C# equivalent of</mark>** `std::map` in C++ or `HashMap` in Java.

---

## 2. Is `Dictionary` built on `Hashtable`?

**<mark>No</mark>.** This is **<mark><u>a common misconception</u></mark>**. They share the **<mark>same underlying concept</mark>** (hashing) but are **<mark><u>entirely separate implementations</u></mark>**.

| Feature              | `Hashtable`                           | `Dictionary<TKey, TValue>`                         |
| -------------------- | ------------------------------------- | -------------------------------------------------- |
| Namespace            | `System.Collections`                  | `System.Collections.Generic`                       |
| Type safety          | None (stores `object`)                | **<mark>Strongly typed</mark>** (`TKey`, `TValue`) |
| Internal storage     | `struct bucket` array, `object[]`     | `struct Entry` array (typed)                       |
| Collision resolution | **<mark>Chaining</mark>**             | **<mark>Chaining</mark>**                          |
| Boxing               | **<mark>Yes</mark>**, for value types | **<mark>No</mark>**                                |

### Why is `Dictionary` faster?

1. **<mark><u>No boxing</u></mark>**. `Hashtable` stores keys and values as `object`, meaning value types (like `int`) must be boxed on insert and unboxed on retrieval. `Dictionary<TKey, TValue>` stores them directly in a typed internal array.
2. **<mark><u>No casting overhead</u></mark>.** Retrieving from a `Dictionary` doesn't require casting from `object`.
3. **<mark><u>Better internal design</u></mark>.** The generic version was written later (in .NET 2.0) and optimized from scratch, avoiding the overhead of the 1.0-era `Hashtable`.

They both solve the exact same problem (fast key-based access), but `Dictionary` **<mark>does it</mark>** without the object-wrapping tax.

---

## 3. Core Operations

```csharp
// Initialization
var ages = new Dictionary<string, int>();

// Add (throws if key exists)
ages.Add("Younes", 22);

// Add or update via indexer
ages["Ahmed"] = 25;

// Access (throws KeyNotFoundException if missing)
int age = ages["Younes"];

// Try access (safe)
if (ages.TryGetValue("Ali", out int aliAge))
{
    Console.WriteLine(aliAge);
}

// Remove
ages.Remove("Ahmed");

// Iterate
foreach (KeyValuePair<string, int> kvp in ages)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
```

---

## 4. Key Differences from `Hashtable`

| Behavior                | `Hashtable`       | `Dictionary<TKey, TValue>`                            |
| ----------------------- | ----------------- | ----------------------------------------------------- |
| Missing key via indexer | Returns `null`    | **<mark><u>Throws</u></mark>** `KeyNotFoundException` |
| Iteration element       | `DictionaryEntry` | `KeyValuePair<TKey, TValue>`                          |
| Value types             | Boxes             | **<mark><u>No boxing</u></mark>**                     |
| Compile-time safety     | None              | **<mark><u>Enforced</u></mark>**                      |

---

📌 **Summary**

| Question                    | Answer                                                                                                                  |
| --------------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| What is `Dictionary`?       | A generic, strongly typed key-value collection                                                                          |
| Is it built on `Hashtable`? | No, they are separate implementations that share the same conceptual design (hashing)                                   |
| Why is `Dictionary` faster? | No boxing, no casting, better optimized internal storage                                                                |
| C++ equivalent              | `std::map` (though C++ `map` is **<mark>usually tree-based</mark>**, `std::unordered_map` is the hash-based equivalent) |
| Missing key via indexer     | Throws `KeyNotFoundException` (unlike `Hashtable` which returns `null`)                                                 |
| Iteration yields            | `KeyValuePair<TKey, TValue>`                                                                                            |

---
