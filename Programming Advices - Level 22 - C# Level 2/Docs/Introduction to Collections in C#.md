# Introduction to Collections in C#

## 1. What Collections Are

Collections are data structures used to store and organize groups of related objects in memory. They are **<mark>more flexible</mark>** than basic arrays: the size can grow or shrink **<mark>as needed</mark>**, and they ship with built-in methods for common operations.

---

## 2. Why Use Collections

- Store, retrieve, manipulate, and communicate aggregate data
- Handle large amounts of data efficiently
- Provide built-in methods for common tasks
- Support dynamic sizing (grow or shrink at runtime)

**Common operations:** adding, removing, modifying, and accessing elements.

---

## 3. Namespaces

| Namespace                    | Contents                                                                       |
| ---------------------------- | ------------------------------------------------------------------------------ |
| `System.Collections`         | Non-generic collections (`ArrayList`, `Hashtable`, `Stack`, `Queue`, etc.)     |
| `System.Collections.Generic` | Generic collections (`List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>`, etc.) |

The non-generic namespace **<mark>stores everything</mark>** as `object`, which **<mark>boxes</mark>** value types. The generic namespace stores strongly typed elements **<mark>with no boxing</mark>** — the reason generics are preferred in modern C#.

---

## 4. Common Collection Types

| Type                                | What it is                                     | When to use                                       |
| ----------------------------------- | ---------------------------------------------- | ------------------------------------------------- |
| `List<T>`                           | Dynamic array, index-accessible, auto-resizing | General-purpose list of a specific type           |
| `SortedList<TKey,TValue>`           | Array + hash table combo; keeps keys sorted    | Sorted key-value pairs with fast lookups          |
| `Dictionary<TKey,TValue>`           | Hash table of key-value pairs                  | Fast lookup by unique key                         |
| `HashSet<T>`                        | Set of unique elements                         | Uniqueness, set operations (union, intersection)  |
| `Stack<T>`                          | LIFO structure                                 | Reverse-order processing, undo mechanisms         |
| `Queue<T>`                          | FIFO structure                                 | Task scheduling, ordered processing               |
| `LinkedList<T>`                     | Doubly linked list                             | Frequent insertions and deletions at any position |
| `ObservableCollection<T>`           | Notifies listeners of changes                  | Data binding, UI updates                          |
| `ConcurrentDictionary<TKey,TValue>` | Thread-safe dictionary                         | Multi-threaded access                             |
| `BitArray`                          | Compact array of bits (Booleans)               | Bit storage without the overhead of a `bool[]`    |

Each type **<mark>serves a specific purpose</mark>**. Choosing the right one depends on the requirements of the application or the problem being solved.

---

## 5. Practical Examples

- Creating and using a `List<T>` to store and manipulate a collection of integers
- Implementing a generic method to find the maximum value in a collection
- Using a `Dictionary<TKey,TValue>` to store and retrieve data using keys
- Working with `Stack<T>` and `Queue<T>` to demonstrate LIFO and FIFO behavior
- Implementing a custom generic data structure, such as a linked list

---

📌 **Summary**

| Question                   | Answer                                                                                                                                 |
| -------------------------- | -------------------------------------------------------------------------------------------------------------------------------------- |
| What is a collection?      | A data structure for storing and managing groups of related objects                                                                    |
| Main advantage over arrays | Dynamic sizing + built-in operations                                                                                                   |
| Generic vs non-generic     | Generic avoids boxing and gives type safety; non-generic stores `object`                                                               |
| Key namespaces             | `System.Collections` (non-generic), `System.Collections.Generic` (generic)                                                             |
| How to choose              | Match the type's structure and complexity to the problem: ordering, uniqueness, key lookup, LIFO/FIFO, threading, or bit-level storage |

---

# 
