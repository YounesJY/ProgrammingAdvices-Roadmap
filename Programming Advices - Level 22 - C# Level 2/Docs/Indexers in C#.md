# Indexers in C#

## 1. What an Indexer Is

An indexer lets an object be accessed with `[]` syntax, the way arrays and `List<T>` are. **<mark>It is a property</mark>** with a parameter list.

```csharp
var list = new List<int> { 10, 20, 30 };
int x = list[1];   // 20 — this calls List<int>'s indexer
```

`list[1]` **<mark>is not</mark>** array syntax. It is a call to `List<T>.this[int index]`, <mark>an **indexer**</mark>.

---

## 2. Why Indexers Exist

They give custom types **<mark>array-like access</mark>** when that is the natural API. Without them, `List<T>` **<mark>would force you</mark>** to write `list.Get(1)` instead of `list[1]`. The BCL uses indexers everywhere:

| Type                      | Indexer                          |
| ------------------------- | -------------------------------- |
| `List<T>`                 | `this[int index]`                |
| `T[]`                     | Built-in, not an indexer         |
| `Dictionary<TKey,TValue>` | `this[TKey key]`                 |
| `string`                  | `this[int index]` returns `char` |
| `DataTable`               | `this[int]`, `this[string]`      |
| `ArrayList`               | `this[int index]`                |

---

## 3. Syntax

An indexer looks like <mark>a property **whose name** is `this`</mark> and whose parameter list is in `[]`.

```csharp
public class FixedArray<T>
{
    private readonly T[] _items = new T[10];

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
}
```

Usage:

```csharp
var fa = new FixedArray<int>();
fa[0] = 42;
int x = fa[0];
```

- `this[int index]` is the indexer declaration.
- `get` and `set` are the accessors, same as a property.
- The return type (`T`) is the type of the element being read or written.

---

## 4. Get-Only vs Get-Set

```csharp
public T this[int index] => _items[index];        // read-only
public T this[int index] { get; set; }             // read + write (backed by a field)
```

A get-only indexer makes the type read-only through `[]` while keeping internal mutability. Useful for exposing collections safely.

---

## 5. Multiple Parameters

Indexers can take more than one parameter. The parameters are not required to be `int`.

```csharp
public class Matrix
{
    private readonly int[,] _cells = new int[3, 3];

    public int this[int row, int col]
    {
        get => _cells[row, col];
        set => _cells[row, col] = value;
    }
}
```

Usage:

```csharp
var m = new Matrix();
m[1, 2] = 7;
int v = m[1, 2];
```

The `Dictionary<TKey,TValue>` indexer is the same pattern with one parameter of type `TKey`:

```csharp
public TValue this[TKey key] { get; set; }
```

---

## 6. Overloading Indexers

A type can have multiple indexers as long as their parameter lists differ.

```csharp
public class Lookup
{
    public string this[int i] => $"int:{i}";
    public string this[string s] => $"string:{s}";
}
```

```csharp
var l = new Lookup();
string a = l[5];        // "int:5"
string b = l["x"];      // "string:x"
```

`DataTable` does this — `table[0]` returns the row at index 0, `table["Name"]` returns the column named "Name" of the current row. Same `[]`, different indexer per parameter type.

---

## 7. Indexers vs Properties vs Methods

| Feature                        | Property   | Indexer      | Method            |
| ------------------------------ | ---------- | ------------ | ----------------- |
| Access syntax                  | `obj.Name` | `obj[param]` | `obj.Name(param)` |
| Parameters                     | None       | One or more  | Any               |
| Overloadable by parameter list | No         | Yes          | Yes               |
| Same name for every overload   | Yes        | Yes (`this`) | Yes               |
| Can be static                  | Yes        | **No**       | Yes               |

**<mark>Indexers cannot be static</mark>**. They **<mark>are always</mark>** instance members, **<mark>because</mark>** they operate on a specific object's data.

---

## 8. Indexers vs the `Index` / `Range` Types

Since C# 8, `obj[^1]` (last element) and `obj[1..3]` (slice) are supported on types that expose them. They are not indexers themselves — they are compiler rewrites to `Index` and `Range` types, which the type handles via an indexer or a `Slice` method.

```csharp
int last = list[^1];       // list[list.Count - 1]
var slice = list[1..3];    // new List<int> { list[1], list[2] }
```

`List<T>` implements this via an indexer overload that accepts `Index` or `Range`. If you write a custom type with a `Range` indexer, the same syntax works.

This is a C# 8+ feature. C# 7 and earlier do not have it.

---

## 9. What Indexers Are Not

- **Not array syntax.** Even when the type has an indexer, `obj[i]` compiles to a method call, not a direct memory access.
- **Not virtual by default.** Like any member, mark it `virtual` if derived types should override it.
- **Not static.** Cannot be declared `static`.
- **Not a substitute for method semantics.** If access has side effects or meaning beyond "get/set this slot", a method is clearer.

---

## 10. Common Pitfalls

- **Out-of-range in a custom indexer.** You decide what happens. If you delegate to an array, it throws `IndexOutOfRangeException` (array) or `ArgumentOutOfRangeException` (list). Custom types should pick one and document it.
- **The `Dictionary` indexer throws on missing key.** `dict[key]` throws `KeyNotFoundException` if the key is absent. Use `TryGetValue` when the key may be missing.
- **Indexer on a null reference.** `obj[i]` on `null` throws `NullReferenceException`. The indexer does not change null semantics.
- **Confusing `this[...]` with a method named `this`.** There is no method named `this`; `this[...]` is the indexer syntax and nothing else.
- **Overloading by return type is not possible.** Overloads must differ by parameter list, not return type.

---

## 11. What the Interface Looks Like

`IList<T>` declares an indexer:

```csharp
T this[int index] { get; set; }
```

`IDictionary<TKey,TValue>` declares:

```csharp
TValue this[TKey key] { get; set; }
```

So when you implement `IList<T>` or `IDictionary<TKey,TValue>` in a custom collection in Level 22, you will implement their indexer. That is the only reason indexers will come up in the course even though the course does not teach them directly.

---

📌 **Summary**

| Question                 | Answer                                                                                           |
| ------------------------ | ------------------------------------------------------------------------------------------------ |
| What is an indexer?      | A property with parameters, accessed via `obj[...]`                                              |
| Syntax                   | `public T this[int i] { get; set; }`                                                             |
| Parameters               | One or more; any type, not just `int`                                                            |
| Overloadable?            | Yes, by parameter list                                                                           |
| Static?                  | No                                                                                               |
| Virtual?                 | Only if declared `virtual`                                                                       |
| Difference from a method | `[]` syntax instead of `()` and a name                                                           |
| When to use              | Natural element access on a custom type; implementing `IList<T>` or `IDictionary<TKey,TValue>`   |
| When to avoid            | When access has side effects or meaning beyond get/set                                           |
| Course relevance         | Implementing `IList<T>` / `IDictionary<TKey,TValue>` in Level 22 requires indexer implementation |
