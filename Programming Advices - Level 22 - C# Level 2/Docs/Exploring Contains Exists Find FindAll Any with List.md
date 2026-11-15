# Exploring Contains, Exists, Find, FindAll, and Any with List

## 1. Code Setup

```csharp
List<int> numbers = new List<int> { 44, 22, 55, 666, 9, -6, 345, 11, 3, 3 };
```

---

## 2. Method Reference

| Method     | Signature (simplified)                | Namespace                    | What it does                                                      |
| ---------- | ------------------------------------- | ---------------------------- | ----------------------------------------------------------------- |
| `Contains` | `bool Contains(T item)`               | `System.Collections.Generic` | Checks for exact element presence                                 |
| `Exists`   | `bool Exists(Predicate<T> match)`     | `System.Collections.Generic` | Checks if any element matches the predicate                       |
| `Find`     | `T Find(Predicate<T> match)`          | `System.Collections.Generic` | Returns the first element matching the predicate, or `default(T)` |
| `FindAll`  | `List<T> FindAll(Predicate<T> match)` | `System.Collections.Generic` | Returns all matching elements as a new `List<T>`                  |
| `Any`      | `bool Any(Func<T, bool> predicate)`   | `System.Linq`                | Checks if any element matches the predicate                       |

### 2.1 Examples

```csharp
bool containsNine = numbers.Contains(9);
bool existsNegative = numbers.Exists(n => n < 0);
int firstNegative = numbers.Find(n => n < 0);
List<int> allNegatives = numbers.FindAll(n => n < 0);
bool anyGreaterThan100 = numbers.Any(n => n > 100);
```

---

## 3. Full Example

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 44, 22, 55, 666, 9, -6, 345, 11, 3, 3 };

        Console.WriteLine("List contains 9: " + numbers.Contains(9));
        Console.WriteLine("List contains negative numbers: " + numbers.Exists(n => n < 0));
        Console.WriteLine("First negative number: " + numbers.Find(n => n < 0));
        Console.WriteLine("All negative numbers: " + string.Join(", ", numbers.FindAll(n => n < 0)));
        Console.WriteLine("Any numbers greater than 100: " + numbers.Any(n => n > 100));

        Console.ReadKey();
    }
}
```

**Output**

```
List contains 9: True
List contains negative numbers: True
First negative number: -6
All negative numbers: -6
Any numbers greater than 100: True
```

---

## 4. `Predicate<T>` vs `Func<T, bool>`

Two delegate types cover "takes an element, returns a bool":

```csharp
public delegate bool Predicate<in T>(T obj);
public delegate TResult Func<in T, out TResult>(T arg);  // TResult = bool for predicates
```

| Delegate        | Where used                                                     | Parameters |
| --------------- | -------------------------------------------------------------- | ---------- |
| `Predicate<T>`  | `List<T>.Exists`, `Find`, `FindAll`, `RemoveAll`, `TrueForAll` | 1          |
| `Func<T, bool>` | LINQ: `Where`, `Any`, `All`, `Count`, `First`                  | 1          |

They are structurally the same for the single-parameter case, but they are **<mark>different delegate types</mark>** to the CLR. A `Predicate<int>` **<mark><u>cannot be passed</u></mark>** where a `Func<int, bool>` is expected **<mark><u>without wrapping</u></mark>**:

```csharp
Func<int, bool> f = isEven;          // does not compile
Func<int, bool> f = x => isEven(x);  // wrap
```

Both accept lambdas and method groups directly:

```csharp
Predicate<int> isEven = n => n % 2 == 0;
Predicate<int> isOdd  = IsOdd;       // method group
List<int> even = numbers.FindAll(isEven);
List<int> odd  = numbers.FindAll(IsOdd);
```

The lambda and the method reference **<mark>are interchangeable</mark>** at the call site. That is a property of the compiler's method-group conversion, not of `Predicate<T>` specifically. The same works for `Func<T, bool>`.

---

## 5. `Exists` vs `Any`

| Aspect                | `Exists`                                                                        | `Any`                                                                             |
| --------------------- | ------------------------------------------------------------------------------- | --------------------------------------------------------------------------------- |
| Namespace             | `System.Collections.Generic`                                                    | `System.Linq`                                                                     |
| Declared on           | `List<T>` (**<mark>instance method</mark>**)                                    | `IEnumerable<T>` (**<mark>extension method</mark>**)                              |
| Works on              | `List<T>` only                                                                  | **<mark>Any</mark>** `IEnumerable<T>` (arrays, lists, query results, EF, …)       |
| Predicate type        | `Predicate<T>`                                                                  | `Func<T, bool>`                                                                   |
| No-predicate overload | No                                                                              | Yes — `Any()` checks for non-empty                                                |
| Execution             | **<mark>Immediate</mark>**                                                      | **<mark>Immediate</mark>**                                                        |
| Performance           | Slightly **<mark>faster</mark>** on `List<T>` (direct loop over internal array) | Slight **<mark>extra overhead</mark>** from LINQ's wrapper (iterator abstraction) |
| Practical difference  | Negligible in normal use                                                        | Negligible in normal use                                                          |

### 5.1 The performance claim

`Exists` is marginally faster on `List<T>` because it is **<mark>a direct instance method</mark>** that walks the internal array. `Any` goes through LINQ's `IEnumerable<T>` path, which adds a small abstraction cost. In practice, **<mark><u>the difference is negligible</u></mark>** — measurable only in tight loops over very large lists run millions of times.

The "`Any` is always better" claim from one of the comments is wrong. `Exists` **<mark><u>is not deprecated</u></mark>**, not a code smell, and is not slower. It is the natural method when the source is `List<T>` and the only need is "does any element match". `Any` is the **<mark>natural choice</mark>** when the source is any `IEnumerable<T>` (arrays, query results, EF, custom collections) or when consistency with the rest of a LINQ codebase matters.

### 5.2 What each cannot do

- `Exists` cannot work on anything other than `List<T>`. If the source is an `IEnumerable<T>`, `IQueryable<T>`, array, `HashSet<T>`, or a LINQ query, **<mark>only</mark>** `Any` compiles.
- `Any` **<mark>with no predicate</mark>** (`numbers.Any()`) checks whether the sequence contains at least one element. `Exists` **<mark><u>has no no-predicate overload</u></mark>**; on a `List<T>` you would write `numbers.Count > 0`, which is O(1) for `List<T>` — faster than `Any()` in that specific case.

### 5.3 When to prefer which

| Scenario                                                | Prefer                          |
| ------------------------------------------------------- | ------------------------------- |
| Source is `List<T>`, predicate in hand                  | `Exists` — direct and idiomatic |
| Source is `List<T>`, no predicate, just non-empty check | `numbers.Count > 0` — O(1)      |
| Source is any `IEnumerable<T>` / `IQueryable<T>`        | `Any` — only option             |
| Consistency with LINQ code around it                    | `Any`                           |
| Mixed collection types in a codebase                    | `Any` — works everywhere        |

Neither is "wrong". The method should match the source type, not a style preference.

---

## 6. `Contains` vs `Exists` / `Any`

`Contains(9)` checks **exact equality** against a value. It does not take a predicate.

`Exists(predicate)` / `Any(predicate)` check **any match against a condition**.

```csharp
numbers.Contains(9)             // is the literal value 9 present?
numbers.Exists(n => n == 9)     // same idea, expressed as a predicate
numbers.Any(n => n == 9)        // same idea, LINQ
```

`Contains` is O(n) on `List<T>`. For a single exact-value check, it is **<mark>clearer than a predicate</mark>** and does the same work.

`Contains` uses `EqualityComparer<T>.Default`. For custom classes, either override `Equals` + `GetHashCode`, or use `Exists` / `Any` with an explicit comparison.

---

## 7. `Find` vs `FirstOrDefault`

`Find` returns the first match, or `default(T)` if there is none. For `int`, `default(int)` is `0`, which is **<mark>indistinguishable</mark>** from a legitimate `0` in the list. That is a known gotcha of `Find` on value types — a "no match" result looks like a real match.

```csharp
int firstNegative = numbers.Find(n => n < 0);  // -6
int firstHuge = numbers.Find(n => n > 10_000); // 0 — no match, but looks valid
```

`FirstOrDefault` (LINQ) has the same issue. Both share the "no match = default value" convention. For reference types, `null` is unambiguous; for value types, the caller needs context to distinguish.

If the distinction matters, use `First` (throws on no match) or check with `Exists` / `Any` first.

---

## 8. Notes

- **`Contains` checks a value; `Exists` / `Any` check a predicate.** Different questions.
- **`Exists` is a `List<T>` instance method.** It does not exist on arrays, `HashSet<T>`, or `IEnumerable<T>`.
- **`Any` is a LINQ extension on `IEnumerable<T>`.** It works everywhere LINQ works.
- **Performance difference is negligible.** Not a deciding factor.
- **`Any()` with no predicate is the LINQ "is non-empty" check.** For `List<T>`, `Count > 0` is O(1) and faster. For `IEnumerable<T>`, `Any()` is the right tool.
- **`Predicate<T>` and `Func<T, bool>` are not interchangeable.** Different delegate types. Both accept lambdas and method groups.
- **`Find` returns `default(T)` on no match.** For `int`, that is `0`, which can be confused with a real `0`.
- **`FindAll` returns a new `List<T>`.** It allocates. `Where(...).ToList()` does the same; `Where(...)` alone does not (deferred).

---

📌 **Summary**

| Question                   | Answer                                                           |
| -------------------------- | ---------------------------------------------------------------- |
| `Contains` vs `Exists`     | `Contains` = exact value; `Exists` = predicate on `List<T>`      |
| `Exists` vs `Any`          | `Exists` on `List<T>` only; `Any` on any `IEnumerable<T>`        |
| Which is faster?           | `Exists` marginally, by a negligible amount                      |
| Which is "better"?         | Depends on source type — neither is deprecated                   |
| `Any()` with no predicate  | Is the sequence non-empty                                        |
| `Exists` with no predicate | Does not exist (only on `List<T>` with a `Count` check)          |
| `Find` on no match         | Returns `default(T)` — `0` for `int`, `null` for reference types |
| `FindAll` return type      | New `List<T>` (allocates)                                        |
| Delegate types             | `Predicate<T>` for `List<T>` methods, `Func<T, bool>` for LINQ   |
| Are they interchangeable?  | No — different types, but both accept lambdas and method groups  |

---
