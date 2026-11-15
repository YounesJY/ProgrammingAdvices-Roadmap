# Exploring Contains, Exists, Find, FindAll, and Any with List of Strings

## 1. Code Setup

```csharp
List<string> words = new List<string> { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };
```

---

## 2. Method Reference

| Method     | What it does with strings                                  |
| ---------- | ---------------------------------------------------------- |
| `Contains` | Checks if the list contains the exact string value         |
| `Exists`   | Checks if any string matches a predicate                   |
| `Find`     | Returns the first string matching a predicate, or `null`   |
| `FindAll`  | Returns all matching strings as a new `List<string>`       |
| `Any`      | LINQ equivalent of `Exists`; works on any `IEnumerable<T>` |

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
        List<string> words = new List<string> { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew" };

        Console.WriteLine("List contains 'apple': " + words.Contains("apple"));
        Console.WriteLine("List contains a word of length 5: " + words.Exists(word => word.Length == 5));
        Console.WriteLine("First word longer than 5 characters: " + words.Find(word => word.Length > 5));
        Console.WriteLine("Words longer than 5 characters: " + string.Join(", ", words.FindAll(word => word.Length > 5)));
        Console.WriteLine("Any words starting with 'a': " + words.Any(word => word.StartsWith("a")));

        Console.ReadKey();
    }
}
```

**Output**

```
List contains 'apple': True
List contains a word of length 5: True
First word longer than 5 characters: banana
Words longer than 5 characters: banana, cherry, elderberry, honeydew
Any words starting with 'a': True
```

---

## 4. Equality in C#: Reference vs Value

The behavior of `Contains` on a `List<string>` is not about strings specifically. It is about **<mark>how C# equality works</mark>** in general. The rule:

> Two objects can contain the exact same data **<mark><u>without being the same object</u></mark>**.

```csharp
class Person { public string Name { get; set; } }

Person p1 = new Person { Name = "Ahmed" };
Person p2 = new Person { Name = "Ahmed" };

Console.WriteLine(p1 == p2);   // False
```

`==` on `Person` checks **<mark><u>reference equality</u></mark>** **<u>by default</u>**. `p1` and `p2` point to two different objects on the heap. **<mark>Same data</mark>**, **<mark><u>different references</u></mark>**, so `False`.

### 4.1 The two kinds of equality

| Kind                   | What it compares                                                    | Default for                                                                     |
| ---------------------- | ------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| **Reference equality** | Do both variables **<mark><u>point to</u></mark>** the same object? | **<mark>Classes</mark>** (reference types)                                      |
| **Value equality**     | Do both objects **<mark><u>hold</u></mark>** the same data?         | **<mark>Structs</mark>**, **<mark>primitives</mark>**, **<mark>strings</mark>** |

```csharp
int a = 5, b = 5;
Console.WriteLine(a == b);        // True — value equality

string s1 = "hello", s2 = "hello";
Console.WriteLine(s1 == s2);      // True — value equality
```

### 4.2 The default `==` operator

For a class that does **<mark><u>not</u></mark>** overload `==`, the compiler uses `object.operator ==`, which **<mark>calls</mark>** `ReferenceEquals`. **<mark><u>No data comparison</u></mark>**. **<mark><u>No field check</u></mark>**. Just: same pointer?

```csharp
Person p3 = p1;
Console.WriteLine(p1 == p3);   // True — same reference
```

`p1` and `p3` point to the same object. `p1 == p3` is `True`. `p1 == p2` is `False`. Data is irrelevant.

### 4.3 `Equals` vs `==`

Two members exist for equality:

| Member                         | Default behavior                                            |
| ------------------------------ | ----------------------------------------------------------- |
| `==`                           | Reference equality for classes; overridable                 |
| `object.Equals(object)`        | Reference equality for classes; overridable                 |
| `object.ReferenceEquals(a, b)` | Always reference equality; **<mark>not overridable</mark>** |

`Equals` is **<mark><u>virtual</u> and can be overridden</mark>** to provide value equality. `==` is an operator and **<mark><u>must be overloaded explicitly</u></mark>**. Most well-designed domain types **override both**, and **<mark>they should agree</mark>**.

```csharp
class Person
{
    public string Name { get; set; }

    public override bool Equals(object? obj) =>
        obj is Person p && p.Name == Name;

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public static bool operator ==(Person? a, Person? b) =>
        ReferenceEquals(a, b) || (a is not null && a.Equals(b));

    public static bool operator !=(Person? a, Person? b) => !(a == b);
}
```

Now `p1 == p2` is `True`. And `p1.Equals(p2)` is `True`. **<mark><u>Both agree</u></mark>**.

The general contract:

- If you override `Equals`, override `GetHashCode` too.
- If you overload `==`, also overload `!=` and **<mark>make them consistent</mark>** with `Equals`.
- Do not mix: `a == b` and `a.Equals(b)` **<mark><u>should</u></mark>** return the same answer.

### 4.4 `string` is the exception, not the rule

`string` is a reference type, but `==` on strings is **<mark><u>value equality</u></mark>**. The `string` class **<mark>overloads</mark>** `==` to compare character content, not references.

```csharp
string s1 = "hello";
string s2 = "hello";
Console.WriteLine(s1 == s2);           // True — value equality
Console.WriteLine((object)s1 == (object)s2);  // True or False depending on interning, not the point
```

That is why `Contains("apple")` on a `List<string>` works as you expect. `List<string>.Contains` uses `EqualityComparer<string>.Default`, which delegates to `string.Equals`, which **<mark>compares content</mark>**.

`string` is the standard example, **<mark><u>not</u></mark>** the default behavior of reference types. **<mark><u>Every other class</u></mark>** follows the reference-equality default **<mark>until you override it</mark>**.

### 4.5 String Interning (The String Pool)

Because strings are used everywhere, the CLR **<mark>has an optimization</mark>** called **<mark><u>string interning</u></mark>**. When you declare a string literal, the CLR checks an internal table called the **<mark><u>intern pool</u></mark>**. If the string already exists, it returns a reference **to the existing instance**; if not, it adds it. 

```csharp
string s1 = "hello";
string s2 = "hello";

Console.WriteLine(ReferenceEquals(s1, s2)); // True
```

Both `s1` and `s2` point to the exact same object in memory.

**<mark><u>But this only applies to literals</u></mark>.** If you use `new string(...)`, or concatenate strings dynamically, or read from a file, the CLR **<mark>bypasses the pool</mark>** and allocates a brand-new object on the heap, even if an identical string already exists.

```csharp
string s1 = "hello";
string s2 = new string("hello".ToCharArray());

Console.WriteLine(ReferenceEquals(s1, s2)); // False
Console.WriteLine(s1 == s2);                // True
```

**Why this matters for your code:**

Because `==` on strings checks value, `s1 == s2` is `True` **<mark>regardless of the pool</mark>**. But if you rely on `ReferenceEquals()` or object identity, **<mark><u>the pool will trick you</u></mark>**. 

In Java, this is the exact reason `==` fails for strings. Java interns literals, so `"hello" == "hello"` is `true`, but `new String("hello") == new String("hello")` is `false`. C# **<mark>interns the same way</mark>**, but C# saves you from the bug by making `==` check the characters, not the memory address.

**<mark><u>You can</u></mark>** force a string into the pool using `string.Intern()`:

```csharp
string s1 = "hello";
string s2 = new string("hello".ToCharArray());
string s3 = string.Intern(s2);

Console.WriteLine(ReferenceEquals(s1, s3)); // True
```

### Summary Addition

| Scenario                    | `ReferenceEquals` | `==` (C#) | `==` (Java) |
|:--------------------------- |:----------------- |:--------- |:----------- |
| Two literals (`"a"`, `"a"`) | `True` (interned) | `True`    | `True`      |
| Literal vs `new`            | `False`           | `True`    | `False`     |
| `new` vs `new`              | `False`           | `True`    | `False`     |

## 4.6 The Mechanics: Operators vs. Methods

### 4.6.1 Overloading `==` is Independent of `Equals()`

Overriding `Equals()` changes how the `.Equals()` method behaves. It does **<mark><u>not</u></mark>** change the `==` operator. If you want `==` to perform value equality for your custom class, **<mark>you must explicitly overload it</mark>**:

```csharp
public static bool operator ==(Person left, Person right)
{
    if (ReferenceEquals(left, right)) return true;
    if (left is null || right is null) return false;
    return left.Equals(right);
}
```

### 4.6.2 Compile-Time Types Resolve `==` Overloads

Operator overloading is resolved at **<mark>compile time</mark>** based on the static types of the variables. Virtual methods (like `Equals()`) are resolved at **<mark>runtime</mark>** based on the actual object.

```csharp
Person p1 = new Person { Name = "Ahmed" };
Person p2 = new Person { Name = "Ahmed" };
object o1 = p1;
object o2 = p2;

Console.WriteLine(p1 == p2); // True (Compiler sees Person == Person)
Console.WriteLine(o1 == o2); // False (Compiler sees object == object)
```

Even though the runtime objects are `Person`, the compiler only knows `o1` and `o2` are `object`. It therefore selects the base `object == object` operator, which checks references.

### 4.6.3 `ReferenceEquals()` Internals

`ReferenceEquals` is defined in `System.Object` and takes two `object` parameters:

```csharp
public static bool ReferenceEquals(object objA, object objB)
{
    return objA == objB; // Resolves to object == object (reference check)
}
```

Because the parameters are typed as `object`, the compiler **<mark><u>ignores</u></mark>** any custom `==` overloads on derived types like `string` or `Person`. **<mark>It always checks identity</mark>**.

### 4.7 Java vs C# on `==` for strings

This is where the two languages diverge, and it is a common source of bugs when people move between them.

| Aspect                        | C#                                          | Java                                                       |
| ----------------------------- | ------------------------------------------- | ---------------------------------------------------------- |
| `==` on strings               | **Value equality** (overloaded on `string`) | **Reference equality** (always)                            |
| Value equality on strings     | `s1 == s2`                                  | `s1.equals(s2)`                                            |
| Reference equality on strings | `ReferenceEquals(s1, s2)`                   | `s1 == s2`                                                 |
| `==` on user classes          | Reference by default, overloadable          | Reference always, **<mark><u>not overloadable</u></mark>** |
| `Equals` on strings           | Value equality                              | Value equality                                             |

**C#:**

```csharp
string s1 = new string("hello".ToCharArray());
string s2 = new string("hello".ToCharArray());
Console.WriteLine(s1 == s2);      // True — value
Console.WriteLine(s1.Equals(s2)); // True — value
```

**Java:**

```java
String s1 = new String("hello");
String s2 = new String("hello");
System.out.println(s1 == s2);       // false — reference
System.out.println(s1.equals(s2));  // true — value
```

In Java, `==` on strings **<mark>compares references</mark>** and gives `false` for two distinct objects with the same content. **<mark>You must use</mark>** `.equals()`. In C#, `==` on strings **<mark><u>compares content</u></mark>** and gives `true`. This is why "**<u>C# is Java done right</u>**" arguments often start with this exact example.

The Java string interning pool makes `s1 == s2` sometimes return `true` (when both string literals are interned), which **<mark>makes the bug intermittent and worse</mark>**. C#'s `string ==` bypasses that entirely by always comparing content.

### 4.8 How this connects to the collection methods

`List<string>.Contains` uses `EqualityComparer<string>.Default`, which uses `string.Equals`. Because `string` has value equality, `Contains` works as you would expect.

If `words` were a `List<Person>` and `Person` **<mark>did not override</mark>** `Equals`, `Contains(person1)` would only find `person1` if the exact same reference is in the list. An equal-but-distinct `Person` would not be found:

```csharp
List<Person> people = new List<Person> { new Person { Name = "Ahmed" } };
Person query = new Person { Name = "Ahmed" };

Console.WriteLine(people.Contains(query));   // False — different reference, no Equals override
```

To fix this, **<mark>either</mark>** override `Equals` + `GetHashCode` on `Person`, or use `Exists` / `Any` with an explicit comparison:

```csharp
people.Exists(p => p.Name == query.Name);    // True
people.Any(p => p.Name == query.Name);       // True
```

`Find` and `FindAll` use a predicate, so equality is whatever the predicate says. `Contains` uses the default equality comparer, so it depends on whether the element type has value equality.

### 4.9 Why `string` in `Find` returns `null` on no match

`Find` on a `List<string>` returns `default(string)`, which is `null`. For `int` it returns `0`. The "no match" and "the result happens to be that value" cases are indistinguishable for value types, distinguishable for reference types.

```csharp
string? firstShort = words.Find(w => w.Length < 2);    // null — no match
int firstNegative = numbers.Find(n => n < 0);          // 0 — no match, but 0 could be real
```

If you need to distinguish "not found" from "found with default value", use `FindIndex` (returns `-1` on no match), or check with `Exists` / `Any` first.

---

## 5. Notes

- **`Contains` on `List<string>` uses `string.Equals`.** Because `string` overloads `==` as value equality, `Contains` compares content.
- **`Contains` on `List<Person>` without `Equals` overridden uses reference equality.** Same data, different reference, not found.
- **`==` on `string` is value equality; `==` on every other class is reference equality by default.** That is the C# rule.
- **`==` on `string` in Java is reference equality.** Value equality requires `.equals()`. This is the sharpest C# vs Java difference on equality.
- **`Equals` is the canonical value-equality method.** `==` is a syntax convenience that should agree with it.
- **`Find` returns `default(T)` on no match.** `null` for `string`, `0` for `int`, `null` for any reference type.
- **`Exists` and `Any` let you specify the comparison.** Use them when you need custom equality without overriding `Equals`.
- **`Predicate<T>` for `List<T>` methods; `Func<T, bool>` for LINQ.** Different delegate types, same call-site shape.

---

📌 **Summary**

| Question                                                               | Answer                                                           |
| ---------------------------------------------------------------------- | ---------------------------------------------------------------- |
| Is `==` on `string` value or reference?                                | Value equality — `string` overloads `==`                         |
| Is `==` on `Person` value or reference?                                | Reference equality by default                                    |
| Is Java the same?                                                      | No — Java `==` on strings is reference equality; use `.equals()` |
| Why does `List<string>.Contains` work as expected?                     | `string` has value equality                                      |
| Why would `List<Person>.Contains` fail for equal-but-distinct objects? | No `Equals` override means reference equality                    |
| How to compare strings in Java?                                        | `.equals()`, not `==`                                            |
| How to compare strings in C#?                                          | `==` or `.Equals()` — both value equality                        |
| What does `Find` return on no match?                                   | `default(T)` — `null` for `string`, `0` for `int`                |
| How to provide custom equality without overriding `Equals`?            | Use `Exists` / `Any` / `Find` with a predicate                   |

---
