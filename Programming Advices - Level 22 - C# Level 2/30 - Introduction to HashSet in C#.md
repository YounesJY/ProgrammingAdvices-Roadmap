# Introduction to HashSet in C#

## 1. What is HashSet?

`HashSet<T>` is a collection class in the `System.Collections.Generic` namespace **<mark>designed to store a <u>set of unique elements</u></mark>**. 

**<mark><u>Under the hood</u></mark>**, it is essentially a `Dictionary<TKey, TValue>` **<mark><u>without</u> the values</mark>**. The element itself **<mark><u>acts as both</u></mark>** the key and the value. This is why the Microsoft documentation states: *"The HashSet<T> class can be thought of as a Dictionary<TKey,TValue> collection without values."*

Because it uses hashing, it provides O(1) average time complexity for lookups, additions, and removals, making it **<mark><u>much faster</u></mark>** than a `List<T>` for checking existence.

---

## 2. Key Characteristics

- **Uniqueness:** The primary feature of `HashSet<T>` is that it automatically ensures all **<mark><u>elements are unique</u></mark>**. Adding a duplicate **<mark>is silently ignored</mark>** (or returns `false`).
- **No Indexing:** Unlike lists, `HashSet<T>` **<mark>does not</mark>** support indexing (no `set[0]`). It **<mark>does not</mark>** maintain the order of its elements.
- **Generic:** It can **<mark><u>store any type</u></mark>** of object, just like `List<T>` or `Dictionary<TKey, TValue>`.
- **C++ Equivalent:** It is the C# equivalent of `std::unordered_set` in C++ (or `std::set` if you consider the generic concept of a unique collection, though C++'s `std::set` is **<u>tree-based and sorted</u>**, while C#'s `HashSet` is **<mark><u>hash-based</u> and <u>unsorted</u></mark>**).

---

## 3. Why Use It?

The best way to understand `HashSet<T>` is through **<mark>a real-world analogy</mark>**. Imagine you are managing the guest list for a large event. Your goal is to ensure **<mark>no one is invited twice</mark>**, so you enforce a strict rule: every guest must be unique.

- **No Duplicates:** If someone tries to add a guest who is already on the list, you ignore it. `HashSet` does this automatically.
- **No Order:** It doesn't matter what order the names are on the list, only that each name is unique. `HashSet` does not care about element order.
- **Set Operations:** Later, you might want to compare this guest list with another list to see who is attending both events (intersection) or who is only attending one (difference). `HashSet` is optimized for these operations.

---

## 4. Summary

📌 **Summary**

| Feature            | `HashSet<T>`                                                                  |
| ------------------ | ----------------------------------------------------------------------------- |
| Namespace          | `System.Collections.Generic`                                                  |
| Uniqueness         | Enforced automatically                                                        |
| Indexing           | Not supported                                                                 |
| Order              | Not guaranteed                                                                |
| Lookup complexity  | O(1) average                                                                  |
| Internal structure | Hash table (similar to `Dictionary` without values)                           |
| C++ equivalent     | `std::unordered_set`                                                          |
| Primary use case   | Removing duplicates, fast existence checks, set math (Union/Intersect/Except) |

---
