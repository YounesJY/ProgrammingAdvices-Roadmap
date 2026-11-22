# Intersection Operation with HashSet

## 1. Basic Example

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
        HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };

        // Intersection of set1 and set2
        set1.IntersectWith(set2);

        Console.WriteLine("Intersection of sets:");
        foreach (int item in set1)
        {
            Console.WriteLine(item);
        }
        
        Console.ReadKey();
    }
}
```

**Output**

```
Intersection of sets:
3
```

`IntersectWith` retains only the elements that are present in **both** sets. 

---

## 2. `IntersectWith` vs `Intersect`

Just like `Union`, there are **<mark>two ways to do this</mark>**: the `HashSet` instance method or the LINQ extension method.

| Feature                          | `.Intersect()` (LINQ)                        | `.IntersectWith()` (HashSet)                             |
| -------------------------------- | -------------------------------------------- | -------------------------------------------------------- |
| **Modifies original?**           | ❌ No                                         | ✅ **<mark><u>Yes</u></mark>** (modifies `set1` in place) |
| **Return type**                  | `IEnumerable<T>`                             | `void`                                                   |
| **Works on**                     | **<mark><u>Any</u></mark>** `IEnumerable<T>` | Only `HashSet<T>`                                        |
| **What happens to non-matches?** | Ignored                                      | **<mark>Deleted from `set1`</mark>**                     |

### 2.1 The Danger of `IntersectWith`

`set1.IntersectWith(set2)` **<mark><u>mutates</u></mark> `set1`**. Any element in `set1` that is *not* in `set2` **<mark><u>is deleted</u></mark>**. 

If you want to keep `set1` intact and store the intersection in a new set, **<mark>you <u>must clone it</u> first</mark>**:

```csharp
HashSet<int> result = new HashSet<int>(set1); // Copy
result.IntersectWith(set2);                   // Mutate the copy
// set1 is still { 1, 2, 3 }
```

**<mark>Or</mark>** use the LINQ method, which does not mutate the original:

```csharp
var result = set1.Intersect(set2).ToHashSet();
// set1 is still { 1, 2, 3 }
```

---

## 3. Real-World Use Case: Mutual Friends

The classic production use case for set intersection is **<mark>finding mutual friends</mark>** (like on Facebook). 

In a social graph, a user's friends are a collection of unique User objects. To find mutual friends between Alice and Bob, **<mark>you intersect their friend lists</mark>**. 

```csharp
public class SimpleFBUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
    // Use HashSet for O(1) lookups and built-in set operations
    public HashSet<SimpleFBUser> Friends { get; set; }

    public SimpleFBUser()
    {
        Friends = new HashSet<SimpleFBUser>();
    }

    public HashSet<SimpleFBUser> GetMutualFriends(SimpleFBUser otherUser)
    {
        // 1. Create a copy of my friend list
        HashSet<SimpleFBUser> potentialMutuals = new HashSet<SimpleFBUser>(this.Friends);
        
        // 2. Keep only those who are ALSO in the other user's friend list
        potentialMutuals.IntersectWith(otherUser.Friends);
        
        return potentialMutuals;
    }
}
```

*Note: For `HashSet<SimpleFBUser>` to work correctly and recognize that Alice's "Charlie" object is the same person as Bob's "Charlie" object, the `SimpleFBUser` class **<mark>must override</mark>** `Equals` and `GetHashCode` based on `Id`.*

---

## 4. Building Intersect from Scratch

If you had to build intersection manually without `HashSet<T>`, it would look like this:

```csharp
public static HashSet<T> ManualIntersect<T>(HashSet<T> set1, HashSet<T> set2)
{
    HashSet<T> result = new HashSet<T>();
    
    foreach (T item in set1)
    {
        if (set2.Contains(item)) // O(1) check thanks to hashing
        {
            result.Add(item);
        }
    }
    
    return result;
}
```

While you *could* use a nested loop to compare every item in `set1` against every item in `set2` (O(n²)), using the `Contains` method on a `HashSet` **<mark>makes it O(n)</mark>** because the lookup is constant time.

---

📌 **Summary**

| Question                            | Answer                                                               |
| ----------------------------------- | -------------------------------------------------------------------- |
| What does `IntersectWith` do?       | Removes elements from the current set that are not in the target set |
| Does it mutate the original?        | Yes, it modifies the `HashSet` it is called on                       |
| Does it return anything?            | No (`void`)                                                          |
| How to avoid mutating the original? | Clone it first (`new HashSet<T>(set1)`) or use LINQ `.Intersect()`   |
| How does it work internally?        | Iterates the smaller set and checks `Contains` on the larger set     |
| Real-world use case                 | Facebook/LinkedIn mutual friends                                     |

---


