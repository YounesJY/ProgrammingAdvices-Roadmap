# Working with HashSet

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<string> fruits = new HashSet<string>();

        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Cherry");

        fruits.Add("Apple"); // ignored — already in the set
        fruits.Add("Apple"); // ignored
        fruits.Add("Apple"); // ignored

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }

        Console.ReadKey();
    }
}
```

**Output (order may vary)**

```
Apple
Banana
Cherry
```

The duplicate `Apple` additions **<mark>are silently ignored</mark>**. `Add` returns `bool`: `true` if inserted, `false` if the element was already present.

---

## 2. Custom Objects in `HashSet`

When the elements are custom objects, `HashSet<T>` **<mark>uses</mark>** `EqualityComparer<T>.Default`, which **<mark>depends on two things</mark>**:

1. **`Equals(object)`** — determines whether two objects are equal.
2. **`GetHashCode()`** — determines which bucket the element goes into.

By default, both come from `object`, which uses **<mark>reference equality</mark>**. Two objects with identical data but different references are treated as different elements.

```csharp
class Person
{
    public string Name { get; set; }
}

var people = new HashSet<Person>();
people.Add(new Person { Name = "Ahmed" });
people.Add(new Person { Name = "Ahmed" });  // added! different reference

Console.WriteLine(people.Count); // 2
```

Both objects end up in the set, because **<mark>reference equality says <u>they are different</u></mark>**.

### 2.1 Fix: override `Equals` and `GetHashCode`

```csharp
public class Person
{
    public string Name { get; set; }

    public Person(string name) => Name = name;

    public override bool Equals(object? obj)
        => obj is Person other && Name == other.Name;

    public override int GetHashCode()
        => Name?.GetHashCode() ?? 0;
}
```

Now:

```csharp
var people = new HashSet<Person>();
people.Add(new Person("Ahmed"));
people.Add(new Person("Mohamed"));
people.Add(new Person("Ali"));
people.Add(new Person("Ahmed"));   // ignored — equal to the first

foreach (Person p in people)
    Console.WriteLine(p.Name);
```

**Output (order may vary)**

```
Ahmed
Mohamed
Ali
```

The second `"Ahmed"` **<mark>is rejected</mark>** because `Equals` returns `true` and `GetHashCode` returns the same value.

### 2.2 The contract

- **Equal objects <mark>must have</mark> equal hash codes.** If `a.Equals(b)` is `true`, `a.GetHashCode() == b.GetHashCode()` must be `true`. Violating this breaks `HashSet`, `Dictionary`, and every hash-based collection.
- **Non-equal objects <mark>may share</mark> a hash code.** **<mark>That is a <u>collision</u></mark>**; handled internally by the set. **<mark>Not a correctness problem</mark>**, only a performance one.
- **`GetHashCode` <mark>must be stable</mark>.** It should not change for an object while the object is in a hash-based collection. If it depends on a mutable field, mutating that field after insertion corrupts the set.

### 2.3 When to override, when not to

The decision depends on the domain:

| Question                                                              | If yes                            | If no                                  |
| --------------------------------------------------------------------- | --------------------------------- | -------------------------------------- |
| Should two objects with the same data be considered the same element? | Override `Equals` + `GetHashCode` | Leave the default (reference equality) |
| Are duplicate-by-value objects legitimate?                            | Leave the default                 | Override                               |

For a `Person` with an `Id`, two instances with the same `Id` represent the same entity — override. For two `Transaction` records that happen to have the same amount and date, they are still distinct records — do not override.

The `HashSet` behavior is not wrong in either case. The default (reference equality) is the safe choice; overriding is an explicit statement about identity.

---

## 3. Why the Duplicate Is Ignored

`HashSet<T>.Add` follows the same hashing path as `Dictionary`:

1. Compute `GetHashCode()` for the new element.
2. Compute the bucket index from that hash.
3. Walk the bucket chain, calling `Equals` against each element already there.
4. If a match is found, return `false` and do not insert.
5. Otherwise, insert and return `true`.

**<mark>With reference equality</mark>** (no override), step 3 never finds a match for a distinct object, so insertion always succeeds. **<mark>With value equality</mark>** (override), step 3 finds the existing element and insertion is rejected.

---

## 4. Notes

- **`Add` returns `bool`.** `true` = inserted, `false` = already present.
- **Duplicate insertion is silent.** No exception. If you need to detect duplicates, check the return value of `Add`.
- **`HashSet<T>` uses `EqualityComparer<T>.Default`.** For custom classes, that means reference equality unless you override `Equals` and `GetHashCode`.
- **Override both or neither.** Overriding `Equals` without `GetHashCode` produces inconsistent behavior: two objects that `Equals` considers equal may land in different buckets, and `HashSet` will treat them as distinct.
- **Mutable keys corrupt the set.** If `GetHashCode` depends on a field that changes after insertion, the element stays in the wrong bucket and can no longer be found or removed. Same rule as `Dictionary`.
- **Order is not guaranteed.** The `foreach` output order is an implementation detail.
- **`HashSet<T>` is not thread-safe.** Concurrent writes need external synchronization (or use `ConcurrentDictionary<T, byte>` as an alternative — there is no `ConcurrentHashSet`).
- **C# `HashSet<T>` vs C++ `std::set`.** `HashSet` is hash-based (O(1) average). `std::set` is tree-based (O(log n)). The equivalent of C++'s `std::set` is `SortedSet<T>`.

---

📌 **Summary**

| Question                                    | Answer                                                                      |
| ------------------------------------------- | --------------------------------------------------------------------------- |
| What does `Add` return?                     | `bool` — `true` if inserted, `false` if duplicate                           |
| Duplicate insertion behavior                | Silently ignored, no exception                                              |
| Custom object duplicates — default behavior | Reference equality — two equal-data objects are distinct elements           |
| To deduplicate by value                     | Override `Equals` and `GetHashCode`                                         |
| Contract                                    | Equal objects must have equal hash codes                                    |
| `GetHashCode` on mutable fields             | Breaks the set — the element becomes unfindable                             |
| When to override                            | When identity should be based on data, not reference                        |
| When not to override                        | When each instance is a distinct entity (e.g., transaction records)         |
| Thread-safe?                                | No                                                                          |
| C++ equivalent                              | `std::unordered_set` (hash-based); `SortedSet<T>` = `std::set` (tree-based) |

---


