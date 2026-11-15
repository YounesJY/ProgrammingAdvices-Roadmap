# Dictionary vs Hashtable

## 1. Overview

Both store key-value pairs. They differ in **<mark>type safety</mark>**, **<mark>internal storage</mark>**, and **<mark>performance</mark>**. `Dictionary<TKey, TValue>` is **<mark><u>the modern default</u></mark>**; `Hashtable` is **<mark><u>the pre-generics legacy type</u></mark>**.

![](https://uploads.teachablecdn.com/attachments/b95ba8b1d0764154a48d0013a36e5f31.jpg)

| Feature                           | `Dictionary<TKey, TValue>`                     | `Hashtable`                                               |
| --------------------------------- | ---------------------------------------------- | --------------------------------------------------------- |
| Namespace                         | `System.Collections.Generic`                   | `System.Collections`                                      |
| Introduced                        | .NET 2.0                                       | .NET 1.0                                                  |
| Element types                     | Specified `TKey` / `TValue`                    | `object`                                                  |
| Type safety                       | **<mark>Compile-time</mark>**                  | **<mark><u>Runtime only</u></mark>** (by casts)           |
| Boxing (value types)              | No                                             | **<mark><u>Yes</u></mark>**                               |
| Missing key via indexer           | **<mark>Throws</mark>** `KeyNotFoundException` | Returns `null`                                            |
| **<mark>Iteration yields</mark>** | `KeyValuePair<TKey,TValue>`                    | `DictionaryEntry`                                         |
| Order preserved?                  | No                                             | No                                                        |
| Thread-safe?                      | No                                             | No (basic `Synchronized` wrapper exists)                  |
| Default choice?                   | **<mark>Yes</mark>**                           | **<mark><u>Legacy</u></mark>** / `object`-based APIs only |

---

## 2. The Boxing/Unboxing Claim

> "You don't care about boxing and unboxing when working with generic collections, but nor do I when working with non-generic collections. It is handled by the class, **<mark>isn't it</mark>**?"

> "No, you should care because when you retrieve data from non-generic collection, you retrieve it **<mark>as an object</mark>**, so **<mark>you should unbox (cast)</mark>** it to the correct datatype."

The second reply is correct. The "handled by the class" framing **<mark>is wrong in two places</mark>**.

### 2.1 "Handled by the class" — partly true, partly not

The **<mark>boxing</mark>** on insert **<mark>*is* automatic</mark>**. You write:

```csharp
Hashtable table = new Hashtable();
table["key"] = 42;   // int → object: boxes automatically
```

The class **<mark>does not</mark>** force you to write `(object)42`. The implicit conversion from `int` to `object` boxes, and it happens inside the assignment.

The **<mark>unboxing</mark>** on retrieval **<mark>is *not* handled silently</mark>**:

```csharp
int value = (int)table["key"];       // requires explicit cast
int value2 = table["key"];           // does not compile
```

`table["key"]` returns `object`. You **<mark>cannot assign</mark>** an `object` to an `int` without a cast. **<mark>The cast is unboxing</mark>**: a runtime type check plus a copy out of the boxed object. If the underlying value is not actually an `int`, it throws `InvalidCastException`.

So: **<mark>boxing is <u>silent</u>, unboxing is <u>explicit</u></mark>**. The class handles one direction but forces you to handle the other.

### 2.2 The real cost

**<mark>Every</mark>** value-type insert into a `Hashtable` **<mark>allocates a boxed copy on the heap</mark>**. Every value-type retrieval unboxes it (type check + copy). For a table with millions of entries, that is millions of **<mark>small allocations and <u>GC pressure</u></mark>**.

`Dictionary<TKey, TValue>` stores `TKey` and `TValue` directly in typed arrays (`int[]`, `Entry[]`), with no `object` wrapper. No allocation per entry beyond the internal array itself, no per-access unbox.

### 2.3 Why you should care

- **<mark>Performance</mark>.** Boxing in hot paths is measurable. `Hashtable` is a non-generic type, so it pays that tax for every value-type element.
- **<mark>Correctness</mark>.** The unbox cast can throw `InvalidCastException` at runtime if the stored object is not the exact expected type. `Dictionary<int, int>` **<mark>cannot have</mark>** that problem: the compiler **<mark>enforces it</mark>**.
- **<mark>Allocation</mark>.** Boxed objects **<mark>are GC-managed</mark>**. Each insert allocates. A tight loop of inserts creates garbage that the GC **<mark>has to collect</mark>**.

"Handled by the class" applies to boxing (the conversion is implicit), but not to unboxing (the cast is required). And handling it inside the class does not mean it is free — the allocation and the cast still happen, they are just invisible in the syntax.

---

## 3. Side-by-side

```csharp
// Dictionary — no boxing, type-safe
var dict = new Dictionary<string, int>();
dict["a"] = 42;
int n = dict["a"];              // direct read, no cast

// Hashtable — boxing on insert, explicit unboxing on retrieve
var table = new Hashtable();
table["a"] = 42;                // boxes int → object
int m = (int)table["a"];        // unbox, explicit cast required
```

Both store the same logical data. Only one of them allocates a box for `42` and requires a cast on read.

---

## 4. Thread Safety

**<mark><u>Neither is thread-safe</u></mark>** for concurrent read/write. Both will throw or corrupt if mutated from multiple threads without synchronization.

`Hashtable` has a `Synchronized(Hashtable)` static method that returns a thread-safe wrapper. **<mark>It is coarse-grained</mark>**: every operation **<mark>locks the whole table</mark>**. `Dictionary<TKey, TValue>` **<mark>has no such wrapper</mark>**; use `ConcurrentDictionary<TKey, TValue>` (from `System.Collections.Concurrent`) for concurrent access.

---

## 5. Order

**<mark><u>Neither preserves insertion order</u></mark>**. Do not rely on the enumeration order of either.

If order matters, use `SortedDictionary<TKey,TValue>` (sorted by key, tree-backed), `SortedList<TKey,TValue>` (sorted by key, array-backed), or `OrderedDictionary` (non-generic, preserves insertion order — rare).

---

## 6. When to Use Which

| Situation                                       | Use                                                                                |
| ----------------------------------------------- | ---------------------------------------------------------------------------------- |
| New code, generic types available               | `Dictionary<TKey, TValue>` — **<mark>always</mark>**                               |
| Value types (`int`, `struct`) as keys or values | `Dictionary<TKey, TValue>` — **<mark>avoids boxing</mark>**                        |
| Compile-time type safety required               | `Dictionary<TKey, TValue>`                                                         |
| Legacy code or API requires `Hashtable`         | `Hashtable`                                                                        |
| Truly heterogeneous storage                     | `Hashtable` — but **<mark>reconsider the design</mark>**                           |
| Concurrent access from multiple threads         | `ConcurrentDictionary<TKey, TValue>` — **<mark><u>not either of these</u></mark>** |

`Hashtable` **<mark>is not "wrong"</mark>**; it is a compatibility type. There is no new scenario where it is the better choice.

---

## 7. Notes

- **`Dictionary` and `Hashtable` are separate implementations.** They share the hashing concept, not inheritance.
- **`Dictionary` is faster on value types.** No boxing, no cast, no allocation per entry.
- **Missing key behavior differs.** `Dictionary` throws `KeyNotFoundException`; `Hashtable` returns `null`.
- **Iteration element type differs.** `KeyValuePair<TKey,TValue>` vs `DictionaryEntry`.
- **Neither preserves order.** Do not depend on it.
- **Neither is thread-safe.** `ConcurrentDictionary` is the modern answer.
- **Boxing on `Hashtable` is silent; unboxing is explicit.** The "class handles it" claim is half-true and the half that is missing is the more important one.

---

📌 **Summary**

| Question                                | Answer                                                          |
| --------------------------------------- | --------------------------------------------------------------- |
| Which is the modern default?            | `Dictionary<TKey, TValue>`                                      |
| Why?                                    | Type safety, no boxing, better performance                      |
| Is boxing "handled by the class"?       | Boxing is implicit; unboxing requires an explicit cast          |
| Why does that matter?                   | Allocations per entry, GC pressure, `InvalidCastException` risk |
| `Dictionary` vs `Hashtable` performance | `Dictionary` wins for value types                               |
| Missing key via indexer                 | `Dictionary` throws; `Hashtable` returns `null`                 |
| Order preserved?                        | No in either                                                    |
| Thread-safe?                            | No in either; use `ConcurrentDictionary`                        |
| `Hashtable`'s remaining use case        | Legacy APIs and .NET 1.0 compatibility                          |

---
