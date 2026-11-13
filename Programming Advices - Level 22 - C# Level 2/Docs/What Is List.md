# What Is `List<T>`?

## 1. Definition

`List<T>` is a generic collection class in the .NET framework that stores elements of the same type (`T` is the type parameter). Unlike arrays, it resizes automatically as elements are added.

---

## 2. Key Concepts

### 2.1 Generic

`T` is a type parameter, so you can create a list of any type: `List<int>`, `List<string>`, `List<YourCustomType>`. The list is compiled once and specialized per `T` at runtime.

### 2.2 Dynamic sizing

The list grows automatically. When the internal array is full, it allocates a larger one (roughly double) and copies the elements over.

### 2.3 Zero-based index

Like arrays, `List<T>` uses zero-based indexing. `list[0]` is the first element.

### 2.4 Strongly typed

The compiler enforces type safety. Adding an `int` to a `List<string>` is a compile error, not a runtime one.

---

## 3. Capacity vs Count

| Property   | Meaning                                                     |
| ---------- | ----------------------------------------------------------- |
| `Count`    | Number of elements actually in the list                     |
| `Capacity` | Number of elements the list can hold before the next resize |

`Count` is what you care about logically. `Capacity` is what the internal array has reserved. They diverge: a freshly created list has `Count = 0` and `Capacity = 0`, then both grow as elements are added.

---

## 4. Thread Safety

`List<T>` is **not thread-safe**. Concurrent reads are fine, but concurrent writes (or read + write) need external synchronization. For concurrent scenarios, use a type from `System.Collections.Concurrent` (`ConcurrentBag<T>`, `ConcurrentQueue<T>`, `ConcurrentDictionary<TKey,TValue>`, etc.), or lock around access to the list.

---

## 5. Why `List<T>` Is the Default Choice

- Dynamic sizing without the overhead of manual array management
- Type safety and no boxing for value types
- Works directly with LINQ and generic APIs
- Backed by a contiguous array, so index access is O(1) and iteration is cache-friendly

---

📌 **Summary**

| Aspect                | Detail                                                     |
| --------------------- | ---------------------------------------------------------- |
| Namespace             | `System.Collections.Generic`                               |
| Kind                  | Generic, dynamic array                                     |
| Sizing                | Automatic (grows, does not shrink automatically)           |
| Indexing              | Zero-based                                                 |
| Type safety           | Compile-time                                               |
| Boxing                | None for value types                                       |
| Thread-safe           | No — use `System.Collections.Concurrent` or external locks |
| `Count` vs `Capacity` | `Count` = elements; `Capacity` = reserved slots            |
| When to use           | Default list type for a known element type                 |

---
