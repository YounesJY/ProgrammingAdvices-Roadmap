# Checking for Existence in HashSet

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<string> fruits = new HashSet<string> { "Apple", "Banana", "Cherry" };

        if (fruits.Contains("Apple"))
        {
            Console.WriteLine("Apple is in the HashSet");
        }
        else
        {
            Console.WriteLine("Apple is not in the HashSet");
        }
    }
}
```

**Output**

```
Apple is in the HashSet
```

`Contains` returns `true` if the element is present, `false` otherwise. It is an **O(1) average** operation.

---

## 2. How `Contains` Works Under the Hood

The pasted comment notes that `Contains()` **<mark>internally uses</mark>** `Equals()` and `GetHashCode()`. That is correct, and it is worth understanding the exact steps:

1. Call `element.GetHashCode()` to get the hash code.
2. Compute the bucket index from the hash (`hash % bucketCount`).
3. Walk the linked list of elements in that bucket.
4. For each element in the bucket, call `element.Equals(existingElement)`.
5. If a match is found, return `true`; otherwise, return `false`.

This is the same path `Add` **<mark><u>uses to detect duplicates</u></mark>**. Both rely on `GetHashCode` to locate the bucket and `Equals` to confirm the match.

### 2.1 Custom objects: `Equals` and `GetHashCode` are mandatory

For `HashSet<string>` or `HashSet<int>`, the BCL types **<mark>already implement value equality</mark>**, so `Contains` works out of the box.

For custom objects, the default is **<mark>reference equality</mark>**. If you have not overridden `Equals` and `GetHashCode`, `Contains` only finds the exact same reference, **<mark>not an <u>equal-by-value</u> object</mark>**.

```csharp
class Person { public string Name { get; set; } }

var people = new HashSet<Person> { new Person { Name = "Ahmed" } };

// Not found — different reference, no Equals override
bool found = people.Contains(new Person { Name = "Ahmed" }); // false
```

Override both to make `Contains` work by value:

```csharp
public override bool Equals(object? obj) => obj is Person p && p.Name == Name;
public override int GetHashCode() => Name?.GetHashCode() ?? 0;
```

Now the same lookup returns `true`. The override is what tells `HashSet` which objects are considered equal.

---

## 3. Reimplementing `Contains`

If you were to build `Contains` yourself, the **<mark>naive approach is a linear scan</mark>**:

```csharp
public static bool ContainsLinear<T>(HashSet<T> set, T item)
{
    foreach (T element in set)
    {
        if (EqualityComparer<T>.Default.Equals(element, item))
            return true;
    }
    return false;
}
```

This works, but it is **<mark>O(n)</mark>**, not O(1). The entire point of `HashSet<T>` is to avoid this scan by computing the bucket index from `GetHashCode` and checking only the elements in that bucket.

A correct reimplementation would have to replicate the hashing logic:

```csharp
public static bool ContainsHashed<T>(HashSet<T> set, T item)
{
    int hash = item?.GetHashCode() ?? 0;
    int bucketIndex = hash % set.Capacity;   // conceptual — not public API

    // Walk only the chain at bucketIndex, not the whole set.
    foreach (T element in set.BucketChain(bucketIndex))  // also conceptual
    {
        if (EqualityComparer<T>.Default.Equals(element, item))
            return true;
    }
    return false;
}
```

The bucket-level API is private, so external code cannot do this — which is exactly why you use `Contains` **<mark>instead of rewriting it</mark>**. The BCL `Contains` is the O(1) version; the linear scan is the O(n) fallback you would use if hashing were unavailable.

### 3.1 Why the naive version is not "the same thing"

A linear `foreach` scan works because `HashSet<T>` is enumerable. It produces the correct answer. **<mark>It is just <u>slower</u></mark>**. For small sets (< 100 elements), the difference is unmeasurable. For large sets, the O(n) scan is the entire cost the hash table was designed to avoid.

The point of `HashSet<T>` **<mark>is <u>the hashing</u></mark>**. If you reimplement `Contains` as a linear scan, you have built a `List<T>` with extra steps.

---

## 4. Notes

- **`Contains` is O(1) average.** The whole point of using a hash set.
- **`Contains` uses `EqualityComparer<T>.Default`.** That is `Equals` + `GetHashCode` for custom types.
- **Custom objects need `Equals` and `GetHashCode` overrides.** Without them, `Contains` only matches the exact same reference.
- **Override both, not one.** `Equals` without `GetHashCode` breaks the bucket lookup; `GetHashCode` without `Equals` breaks the match confirmation.
- **Do not reimplement `Contains` as a linear scan in production code.** It works, but it defeats the purpose of the hash set.
- **`Contains` does not allocate.** No garbage, no copying.
- **Not thread-safe.** Use `ConcurrentDictionary<T, byte>` if you need concurrent membership, since there is no `ConcurrentHashSet`.

---

📌 **Summary**

| Question                           | Answer                                                            |
| ---------------------------------- | ----------------------------------------------------------------- |
| What does `Contains` return?       | `bool` — `true` if the element is present                         |
| Complexity                         | O(1) average                                                      |
| Under the hood                     | `GetHashCode` → bucket → `Equals` against elements in that bucket |
| Custom objects — default behavior  | Reference equality                                                |
| Custom objects — to match by value | Override `Equals` and `GetHashCode`                               |
| Naive reimplementation             | Linear scan — correct but O(n), defeats the purpose               |
| Thread-safe?                       | No                                                                |

---
