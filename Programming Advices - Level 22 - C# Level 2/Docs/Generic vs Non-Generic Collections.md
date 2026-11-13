# Generic vs Non-Generic Collections

## 1. The Split

C# collections fall into two namespaces:

| Namespace                    | Stores elements as    | Type safety                   | Boxing                                |
| ---------------------------- | --------------------- | ----------------------------- | ------------------------------------- |
| `System.Collections.Generic` | The declared type `T` | **<mark>Compile-time</mark>** | **<mark>None</mark>** for value types |
| `System.Collections`         | `object`              | Runtime only                  | Required for value types              |

The generic namespace was introduced in C# 2.0 / .NET 2.0 to remove boxing and restore compile-time type checking. Everything in `System.Collections` predates generics.

---

## 2. Generic Collections

Generics introduce **<mark>type parameters</mark>** so a collection can work with a specific type without duplicating code per type. `List<int>` and `List<string>` are separate, fully typed collections **<mark>backed</mark>** by `int[]` and `string[]` respectively.

### Advantages

- **Type safety.** Wrong-type assignment fails at compile time, not runtime.
- **Performance.** Value types are stored directly, no boxing or unboxing.
- **Memory.** No `object` wrapper per element.
- **Reusability.** One generic algorithm works for any type.

### Key types

| Type                                | Role                                 |
| ----------------------------------- | ------------------------------------ |
| `List<T>`                           | Resizable indexed list               |
| `Dictionary<TKey,TValue>`           | Key-value lookup by unique key       |
| `Queue<T>`                          | FIFO                                 |
| `Stack<T>`                          | LIFO                                 |
| `HashSet<T>`                        | Unique, unordered set                |
| `LinkedList<T>`                     | Doubly linked list                   |
| `SortedSet<T>`                      | Ordered unique set                   |
| `SortedDictionary<TKey,TValue>`     | Sorted keys, tree-backed             |
| `SortedList<TKey,TValue>`           | Sorted keys, array-backed            |
| `ConcurrentDictionary<TKey,TValue>` | Thread-safe dictionary               |
| `BlockingCollection<T>`             | Blocking / bounded producer-consumer |
| `ConcurrentBag<T>`                  | Unordered thread-safe bag            |
| `ConcurrentQueue<T>`                | Thread-safe FIFO                     |
| `ConcurrentStack<T>`                | Thread-safe LIFO                     |

---

## 3. Non-Generic Collections

Non-generic collections store every element as `object`. **<mark>They accept any type</mark>**, but **<mark>you pay for that</mark>** flexibility with runtime casts, boxing, and weaker compile-time guarantees.

### Disadvantages

- **Type unsafe.** The compiler cannot prevent mixing types; failures surface at runtime.
- **Boxing / unboxing.** Every value type is wrapped on insert and cast on retrieval.
- **Memory overhead.** Each element carries an `object` reference plus the boxed payload.

### Key types

| Type                  | Role                                                              |
| --------------------- | ----------------------------------------------------------------- |
| `ArrayList`           | Resizable array of `object`                                       |
| `Hashtable`           | Key-value pairs hashed by key                                     |
| `Queue`               | FIFO                                                              |
| `Stack`               | LIFO                                                              |
| `SortedList`          | Key-value pairs sorted by key, accessible by key or index         |
| `BitArray`            | Compact array of bits                                             |
| `HybridDictionary`    | `ListDictionary` while small, switches to `Hashtable` as it grows |
| `ListDictionary`      | Small dictionary backed by a singly linked list                   |
| `NameValueCollection` | String keys and values, accessible by key or index                |
| `OrderedDictionary`   | Key-value pairs accessible by key or index                        |
| `StringCollection`    | Collection of strings                                             |
| `StringDictionary`    | String keys and values backed by a hash table                     |

---

## 4. When Non-Generic Still Appears

- Legacy APIs and older framework code
- Heterogeneous storage where the element type is genuinely mixed
- Interop with APIs that predate generics

For new code, the generic namespace **<mark>covers everything</mark>** these types do, without boxing. The non-generic types are **<mark>kept for compatibility</mark>**, **<mark>not recommended as a default</mark>**.

---

## 5. Choosing Between Them

- **Default:** generic collections. Type safety and no boxing.
- **Exception:** a real need for heterogeneous storage, or a legacy API that requires a non-generic type.
- **Never:** reach for `ArrayList` or `Hashtable` in new code when `List<T>` or `Dictionary<TKey,TValue>` fits.

---

📌 **Summary**

| Aspect         | Generic                      | Non-Generic                 |
| -------------- | ---------------------------- | --------------------------- |
| Namespace      | `System.Collections.Generic` | `System.Collections`        |
| Element type   | Declared `T`                 | `object`                    |
| Type safety    | Compile-time                 | Runtime                     |
| Value types    | No boxing                    | Boxed                       |
| Memory         | Direct storage               | Object wrapper per element  |
| Introduced     | C# 2.0 / .NET 2.0            | .NET 1.0                    |
| Default choice | Yes                          | Legacy / heterogeneous only |

---
