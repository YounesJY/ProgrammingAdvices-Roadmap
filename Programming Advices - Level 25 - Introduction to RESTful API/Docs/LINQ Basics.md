

# LINQ Basics

## The one line

```csharp
var passedStudents = StudentDataSimulation.StudentsList
    .Where(student => student.Grade >= 50)
    .ToList();
```

Two operations, one lambda. That's the whole pattern for now.

---

## What each piece does

**`student => student.Grade >= 50`**

A **lambda** — an inline, nameless function.

Read it as: "given a `student`, return `true` if `Grade >= 50`, else `false`."

The `=>` is the arrow. Left of it: the input. Right of it: what to return. No method name, no `return` keyword, no braces — all implicit.

**`.Where(condition)`**

<mark>Filters</mark>. Runs the condition <mark>for every element</mark> and keeps the ones that return `true`. Everything else is dropped.

**`.ToList()`**

<mark>Forces the filter to actually run *now*</mark> and puts the results in a new `List<T>`.

Without it, you'd have an `IEnumerable<Student>` that <mark>**re-runs the filter every time you iterate**</mark>.

---

## The mental model

```js
List<Student>          →  [Ali(85), Sara(40), Omar(72), Lina(35)]
        .Where(s => s.Grade >= 50)
                       →  [Ali(85), Omar(72)]           ← defined, not yet run
        .ToList()
                       →  List<Student> { Ali, Omar }   ← runs now, new list
```

---

## The comparison that makes it stick

<mark>**Every LINQ filte**r</mark> has a `foreach + if` <mark>equivalent</mark>:

```csharp
// Manual
var passed = new List<Student>();
foreach (var s in StudentDataSimulation.StudentsList)
{
    if (s.Grade >= 50)
        passed.Add(s);
}

// LINQ
var passed = StudentDataSimulation.StudentsList
    .Where(s => s.Grade >= 50)
    .ToList();
```

Same result. <mark><u>**LINQ is shorter and reads like the intent**</u></mark>.

---

## Why `.ToList()` matters in a Web API

<mark>LINQ is **lazy**</mark>. `Where` on its own <mark>doesn't run **until you iterate**</mark>. In a Web API, returning a lazy `IEnumerable<T>` can:

- <mark>Re-run the filter every time</mark> it's enumerated (serialization may enumerate more than once).
- <mark>Produce inconsistent results</mark> **if the underlying list changes between enumerations**.

`.ToList()` <mark>**materializes**</mark> the result once. Safe to return, safe to serialize.

**Rule:** <mark>**<u>always </u>**</mark>`.ToList()` at the end of a LINQ chain **<u>when returning from an API</u>**.

---

## The bigger picture: Collections, IEnumerable, LINQ

<mark>**Three layers of the same story**</mark>.

```js
List<Student>               ← concrete collection (holds data)
    implements
        ↓
IEnumerable<Student>        ← interface: "you can foreach me"
    extended by
        ↓
LINQ methods                ← Where, Select, OrderBy, ... work on anything iterable
```

**Why this matters:** LINQ operates on `IEnumerable<T>`. <mark>Every collection in C#</mark> implements `IEnumerable<T>`. So LINQ <mark>works on all of them</mark>, automatically, with the same syntax.

```csharp
List<Student> list = ...;
list.Where(s => s.Grade >= 50);         // works

Student[] array = ...;
array.Where(s => s.Grade >= 50);        // same LINQ, different collection

IEnumerable<Student> anything = ...;
anything.Where(s => s.Grade >= 50);     // same LINQ
```

<mark>Same methods everywhere</mark>. <mark>**<u>That's the power of the interface</u>**</mark>.

---

## Trade-offs — where LINQ actually costs something

### 1. Lazy evaluation

```csharp
var query = list.Where(s => s.Grade >= 50);   // nothing happens yet
foreach (var s in query) { }                   // filter runs NOW
foreach (var s in query) { }                   // filter runs AGAIN
```

<mark>Every enumeration **re-runs** the filter</mark>. Fine for small lists. <mark>**Wasteful**</mark> if you iterate the same query multiple times.

- **Fix:** `.ToList()` once, <mark>reuse **the materialized** list</mark>.

### 2. Chaining compounds work

```csharp
var result = list
    .Where(s => s.Grade >= 50)
    .OrderBy(s => s.Name)
    .Select(s => s.Name)
    .ToList();
```

<mark>**<u>One pass</u>**</mark> — <mark>LINQ pipelines the operations</mark>. Good.

But <mark>**if you break**</mark> the chain:

```csharp
list.Where(...).ToList()      // first pass
    .OrderBy(...).ToList()    // second pass
    .Select(...).ToList();    // third pass
```

Now <mark>you've **<u>tripled</u>** the iterations</mark>. Never `.ToList()` <mark>in the middle of a chain</mark> unless you need to.

**Rule:** one `.ToList()` at the end.

### 3. LINQ-to-Objects vs. LINQ-to-EF

Same syntax, completely different execution.

| Context                                         | What happens                                                                                                                  |
| ----------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------- |
| `list.Where(...)` on a `List<T>`                | **LINQ-to-Objects.** <mark>Runs **in memory**</mark>, in C#. Every element <mark>goes through the lambda</mark>.              |
| `dbContext.Students.Where(...)` on a `DbSet<T>` | **LINQ-to-EF.** <mark>Gets **translated to SQL**</mark>. <mark>Runs **in the database**</mark>. Only matching rows come back. |

Same code shape, radically different cost. Don't confuse them.

If you're filtering 10 million rows, <mark>you want the filter to run **in the database**</mark>, **<u>not in C#</u>**. <mark>That's **LINQ-to-EF's job**</mark>. In-memory LINQ is fine <mark>when the data is already **local and small**</mark>.

---

## When to use LINQ, when to think twice

| Scenario                                      | Verdict                                                      |
| --------------------------------------------- | ------------------------------------------------------------ |
| Small in-memory lists (hundreds to thousands) | LINQ is fine — <mark>cost is negligible</mark>               |
| Returning from a Web API                      | LINQ fine, `.ToList()` <mark>at the end</mark>               |
| Multiple enumerations of the same query       | `.ToList()` once, <mark>reuse</mark>                         |
| Large in-memory collections                   | Watch the chain, <mark>avoid intermediate</mark> `.ToList()` |
| Database queries                              | <mark>**Use LINQ-to-EF**</mark> — the SQL runs server-side   |
| Performance-critical inner loops              | Hand-written `for` loop may be faster                        |

---

## What to remember right now

- `.Where(lambda)` → filter
- `.ToList()` → run now, materialize
- `x => expression` → lambda
- One `.ToList()` at the end of a chain, never in the middle
- > <mark>LINQ works on anything that implements `IEnumerable<T>`</mark> — which is every collection
- > <mark>LINQ-to-Objects ≠ LINQ-to-EF (in-memory vs. database)</mark>

---


