# What is Hashtable?

## 1. The Concept (General)

A hash table (**<mark>also called</mark>** a hash map) is a data structure that stores data as **<mark>key-value pairs</mark>** with **<mark>direct access in constant time</mark>**. It uses a hash function to compute an index into an array of buckets, where the desired value is stored. 

The relationship is **<mark>one-to-one</mark>** (1:1). Each key maps to exactly one value. The key must be unique; the value **<mark><u>may repeat</u></mark>** across different keys. If you try to add a second value to an existing key, **<mark>it overwrites</mark>** (or throws, depending on the API), because a key **<mark>cannot map</mark>** to multiple values.

**How it works internally:**

1. You provide a key.
2. A hash function converts the key into a number (the hash code).
3. That hash code is divided by the size of the internal array (usually a prime number), and the remainder is used as the index.
4. The table jumps directly to that index to find the value.

There is **<mark>no middle mapping</mark>** table. The index is calculated on the fly. This is why the average lookup is O(1) — the number of steps to find an element **<mark>is constant</mark>**, regardless of the collection's size.

## 2. Hash Tables in C#

C# **<mark>has both</mark>** a non-generic and a generic implementation:

| Feature     | `Hashtable`                           | `Dictionary<TKey, TValue>`                                                |
| ----------- | ------------------------------------- | ------------------------------------------------------------------------- |
| Namespace   | `System.Collections`                  | `System.Collections.Generic`                                              |
| Type safety | None (stores `object`)                | **<mark>Strong</mark>** (stores `TKey`, `TValue`)                         |
| Boxing      | **<mark>Yes</mark>**, for value types | No                                                                        |
| Performance | Slower (due to boxing)                | **<mark>Faster</mark>**                                                   |
| Order       | **<mark>No predictable order</mark>** | **<mark>No predictable order</mark>** (insertion order is not guaranteed) |
| Introduced  | .NET 1.0 (legacy)                     | .NET 2.0 (modern standard)                                                |

`Hashtable` allows keys and values of any type, but retrieving elements **<mark>requires casting</mark>**, and passing value types (like `int`) **<mark><u>requires boxing</u></mark>**.

---

## 3. The Collision Problem (Average vs. Worst Case)

The O(1) lookup is the **average case**. The **worst case** is O(n).

This happens due to **<mark><u>hash collisions</u></mark>**. A collision occurs when two different keys produce the same hash code or map to the same index.

```text
Hash("Omar") = 12345 → Index: 12345 % 10 = 5
Hash("Ali")  = 67895 → Index: 67895 % 10 = 5
```

If both keys map to index 5, the hash table must handle it. The standard solution is **<mark><u>separate chaining</u></mark>**: instead of storing a single element at index 5, the table stores **<mark>a linked list (or a bucket)</mark>** of elements. 

If too many keys collide and end up in the same bucket, looking up an element requires walking that linked list. In the absolute worst case (e.g., a bad hash function or a deliberately malicious input), all elements might end up in one bucket, degrading lookup to O(n).

---

## 4. Lookup (O(1)) vs. Search (O(n))

A **<mark>common point of confusion</mark>** is the difference between a "lookup" and a "search".

**<mark>Lookup (<u>using the key</u>)</mark>:**

```csharp
var value = hashtable["Ahmed"];
```

You have the key, and you ask the table for the value. The hash function calculates the index immediately. This is an O(1) operation.

**<mark>Search (<u>using a condition</u>)</mark>:**

```csharp
var item = list.Find(x => x == 5);
```

You do not have the key (or you are searching by a non-key property). The collection **<mark>must iterate</mark>** through elements one by one until it finds a match. This is an O(n) operation.

---

## 5. Key Takeaways from the Discussion

- **Keys are mapped to indices using the hash function only.** There is no intermediate mapping table. The math **<mark>is done on the fly</mark>**.
- **The relationship is 1:1.** One key points to exactly one value. A value can be shared by multiple keys, but the key itself must be unique.
- **Worst-case O(n) <mark>is inherent</mark>.** **<mark>Collisions are inevitable</mark>**, especially when the number of keys exceeds the number of array buckets. Chaining is the standard resolution, adding O(n) traversal to the collision bucket.

---

📌 **Summary**

| Question                  | Answer                                                                    |
| ------------------------- | ------------------------------------------------------------------------- |
| What is a hash table?     | A key-value structure that maps keys to array indices via a hash function |
| What is the general term? | Hash map / dictionary / associative array                                 |
| C# non-generic            | `Hashtable` (`System.Collections`)                                        |
| C# generic                | `Dictionary<TKey, TValue>` (`System.Collections.Generic`)                 |
| Lookup vs. Search         | Lookup by key = O(1); search by condition = O(n)                          |
| Relationship              | 1:1 (unique key → one value)                                              |
| Average complexity        | O(1) for insert, delete, lookup                                           |
| Worst-case complexity     | O(n) due to hash collisions                                               |
| Collision resolution      | Separate chaining (linked list at the index)                              |

---
