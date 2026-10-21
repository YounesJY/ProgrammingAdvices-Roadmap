---

# Modern C# Essentials: NuGet, Top-Level Statements, Records & Indexers

---

## 1. Introduction

When looking at modern C# code (like your ASP.NET Core `Program.cs` or DTOs), you encounter syntax that didn't exist in older C# versions:

- 🔹 `using Microsoft.IdentityModel.Tokens;` (NuGet)
- 🔹 Code at the top of `Program.cs` without a `Main` method
- 🔹 `public record LoginRequest(string Email, string Password);`
- 🔹 `myList[0]` (Indexers)

This MD covers these four concepts: <mark>**what they are, why they exist, and how they work.**</mark>

---

## 2. NuGet — The .NET Package Manager

### 2.1 What is it?

**NuGet** is <mark>**the official package manager** for .NET</mark>. It is to C# what **npm** is to JavaScript, **pip** is to Python, and **Maven/Gradle** is to Java.

It is <mark>**a central repository**</mark> (nuget.org) of <mark>**pre-built code libraries**</mark> (`.nupkg` files) that you can download and use in your project.

### 2.2 Why does it exist?

Before NuGet, if you wanted to use a library (like a JSON parser or a JWT library), you had to:

1. Find the DLL on a random website.
2. Download it.
3. Manually copy it to a `lib/` folder.
4. Add a manual reference in your `.csproj`.
5. Hope it didn't break when a new version came out.

NuGet <mark>**solved this by automating**</mark>:

- **<mark>Discovery</mark>:** Search for packages inside Visual Studio or the CLI.
- **<mark>Installation</mark>:** One click (or command) to add it.
- **<mark>Dependency Management</mark>:** If Package A needs Package B, NuGet downloads B automatically.
- **<mark>Versioning</mark>:** You can pin a specific version (`Version="8.0.0"`) or update easily.

### 2.3 How it works

When you install a package via Visual Studio or `dotnet add package`, NuGet updates your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="BCrypt.Net-Next" Version="4.2.0" />
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
</ItemGroup>
```

- 🔹 **`PackageReference`:** Tells .NET that this project <mark>**depends on this package**</mark>.
- 🔹 **Global Packages Folder:** The actual `.dll` files <mark>**<u>are cached</u>**</mark> on your machine (usually `~/.nuget/packages/`), so you don't re-download them for every project.
- 🔹 **Restore:** When you build, .NET "restores" the project by downloading any missing packages.

📌 **Without NuGet, your `BCrypt.Verify()` code would not compile.** The `using BCrypt.Net;` line only works because NuGet <mark>downloaded the package and **linked it**</mark> to your project.

---

## 3. Top-Level Statements (C# 9+)

### 3.1 The Old Way (Before C# 9)

Historically, <mark>**every** C# program **required**</mark> a `Main` method inside a `Program` class:

```csharp
using System;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

For a console app, <mark>that is **9 lines of boilerplate**</mark> just to print one thing.

### 3.2 The New Way (C# 9+)

Since C# 9, you can write code directly at the top of the file:

```csharp
Console.WriteLine("Hello, World!");
```

> This <mark>is called a **Top-Level Statement**</mark>. 

### 3.3 How does it work under the hood?

The C# compiler <mark>**secretly wraps** your top-level code **inside a hidden class**</mark> (`Program`) and a hidden `static void Main()` method. You don't see it, but <mark>**it exists in the compiled binary**</mark>.

You can still use `args`, `await`, and `return` at the top level, and the compiler will generate the correct `Main` signature.

### 3.4 Why use it?

- 🔹 **<mark>Less boilerplate</mark>:** Ideal for scripts, console apps, and `Program.cs` in ASP.NET Core.
- 🔹 **<mark>Cleaner ASP.NET Core setup</mark>:** Compare:

**Without top-level:**

```csharp
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // ... 50 lines ...
        app.Run();
    }
}
```

**With top-level:**

```csharp
var builder = WebApplication.CreateBuilder(args);
// ... 50 lines ...
app.Run();
```

### 3.5 Rules

- <mark>Only **one file**</mark> in a project can have top-level statements.
- It <mark>**must come before**</mark> any namespace or class declaration in that file.
- You can define additional methods after your top-level code; the compiler <mark>**treats them as part**</mark> of the hidden `Program` class.

📌 **Your `Program.cs` in the Student API works this way. There is no visible `Main` method, but <mark>one exists in the compiled DLL</mark>.**

---

## 4. Records (C# 9+)

### 4.1 What is a Record?

A **record** is a reference type (like a `class`) <mark>designed for **immutable data**</mark>. <mark>**<u>It is a shorthand</u>**</mark> for writing a class with:

- Constructor with parameters
- `get`-only properties
- Value equality (two records are equal if their fields are equal)
- A readable `ToString()` output
- Built-in deconstruction

### 4.2 The Old Way (Classes)

If you wanted a DTO that just holds data, you wrote:

```csharp
public class LoginRequest
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public override bool Equals(object? obj) => /* ... */
    public override int GetHashCode() => /* ... */
    public override string ToString() => $"LoginRequest {{ Email = {Email}, Password = {Password} }}";
}
```

That's 15+ lines just to hold two strings.

### 4.3 The New Way (Records)

```csharp
public record LoginRequest(string Email, string Password);
```

<mark>That is **one line**</mark>. The compiler generates:

- The constructor
- `get; init;` properties
- Value equality
- A readable `ToString()`
- `Deconstruct` method

### 4.4 Key Differences from Classes

| Feature      | `class`                                                          | `record`                                                                 |
| ------------ | ---------------------------------------------------------------- | ------------------------------------------------------------------------ |
| Equality     | <mark>**Reference-based**</mark> (<mark>**same object**</mark>?) | <mark>**<u>Value-based</u>**</mark> (<mark>**<u>same data</u>**</mark>?) |
| Mutability   | Can be mutable                                                   | <mark>Designed **for immutability**</mark> (`init`)                      |
| `ToString()` | Prints the type name                                             | Prints all properties                                                    |
| Inheritance  | Normal                                                           | Records <mark>**can inherit**</mark> from records                        |
| Use case     | Entities, services, behavior                                     | DTOs, value objects, messages                                            |

### 4.5 When to use Records

- 🔹 **<mark>DTOs</mark>:** `LoginRequest`, `AuthResponse`, `StudentDto`
- 🔹 **<mark>Value Objects</mark>:** Money, Address, Coordinates
- 🔹 **Messages:** Commands, <mark>**Events in CQRS/MediatR**</mark>
- 🔹 **<mark>Configuration objects</mark>**

### 4.6 A subtle note on ASP.NET Core

ASP.NET Core's JSON deserializer (`System.Text.Json`) <mark>**can bind** JSON to a record's constructor</mark>. For example, the following works perfectly with a POST request:

```csharp
public record LoginRequest(string Email, string Password);
```

But you have less control over things like `[Required]` DataAnnotations, so <mark>**<u>many teams</u>**</mark> still prefer classes for **<mark>request DTOs</mark>** and reserve records for **<mark>responses</mark>** or **internal data**.

---

## 5. Indexers

### 5.1 What is an Indexer?

An **indexer** lets you access an object using `[]` syntax, like you would with an array or a `List<T>`:

```csharp
var list = new List<int> { 10, 20, 30 };
Console.WriteLine(list[0]); // 10
```

> You can <mark>**add this same syntax** to **your own classes**</mark>.

### 5.2 How to define an Indexer

An indexer <mark>**is a special property**</mark> named `this`:

```csharp
public class StudentCollection
{
    private Student[] _students = new Student[10];

    public Student this[int index]
    {
        get 
        { 
            if (index < 0 || index >= _students.Length) 
                throw new IndexOutOfRangeException();
            return _students[index]; 
        }
        set 
        { 
            _students[index] = value; 
        }
    }
}
```

Now <mark>you can use it **like an array**</mark>:

```csharp
var collection = new StudentCollection();
collection[0] = new Student { Name = "Ali" };
Console.WriteLine(collection[0].Name); // Ali
```

### 5.3 Where it is used in real code

- 🔹 `List<T>`, `Dictionary<TKey, TValue>`, `Array` — <mark>**all use indexers**</mark>.
- 🔹 `HttpContext.Request.Headers["Authorization"]` — the `Headers` collection <mark>**uses an indexer** with **a string key**</mark>.
- 🔹 `Configuration["ConnectionStrings:Default"]` — `IConfiguration` uses an indexer with `string`.
- 🔹 `DataRow["ColumnName"]` — in ADO.NET.

### 5.4 The `Dictionary` example

```csharp
var headers = new Dictionary<string, string>();
headers["Content-Type"] = "application/json";
var ct = headers["Content-Type"];
```

That `[]` is calling the **indexer** of `Dictionary`.

### 5.5 Overloading Indexers

<mark>**You can have multiple indexers**</mark>, as long as the parameter types differ:

```csharp
public class StudentCollection
{
    public Student this[int index] { get => ...; set => ...; }
    public Student this[string name] { get => ...; set => ...; }
}
```

📌 This is how `HttpContext.Request.Headers["Authorization"]` and `Dictionary<int, Student>` <mark>**work under the hood**</mark>.

---

## 6. Characteristics

- 🧬 **NuGet** is the package manager — no more manual DLLs.
- 🧬 **Top-level statements** remove `Main()` boilerplate.
- 🧬 **Records** are immutable data containers with value equality.
- 🧬 **Indexers** let your classes use `[]` syntax like arrays.

---

## 7. Interconnection

- 🔗 **NuGet** → installs packages like BCrypt and JwtBearer.
- 🔗 **Top-level statements** → explains why `Program.cs` has no `Main`.
- 🔗 **Records** → the modern way to write DTOs and value objects.
- 🔗 **Indexers** → why `myDict["key"]` and `myList[0]` work.
- 🔗 **All four** → part of modern C# idioms used every day in ASP.NET Core.

### 🛠️ Summary of Interconnections

- 🔹 Without NuGet → `BCrypt.Verify()` and `JwtSecurityTokenHandler` would not compile.
- 🔹 Without Top-Level Statements → `Program.cs` would need 10 extra lines of boilerplate.
- 🔹 Without Records → DTOs would be 10x longer.
- 🔹 Without Indexers → every collection would need `.Get(0)` instead of `[0]`.

---

## 8. Conclusion

You now understand four pieces of modern C# that were never in the classic roadmap but appear in every ASP.NET Core project:

- ✅ **NuGet** — the package manager that powers your dependencies.
- ✅ **Top-level statements** — the hidden `Main` in `Program.cs`.
- ✅ **Records** — the cleanest way to write DTOs.
- ✅ **Indexers** — the `[]` mechanism behind `List`, `Dictionary`, and `Headers`.

📌 **<mark>None of these are advanced</mark>. They are the current standard of the language — and <mark>you now read them fluently</mark>.**

---

## 📌 Summary

| Concept                  | What it is                              | Why it exists                         | Where used in your project                  |
| ------------------------ | --------------------------------------- | ------------------------------------- | ------------------------------------------- |
| **NuGet**                | Package manager for .NET                | Automate library install + versioning | BCrypt, JwtBearer, Swagger                  |
| **Top-Level Statements** | Code without a `Main` method            | Remove boilerplate                    | `Program.cs`                                |
| **Records**              | Immutable data type with value equality | 1-line DTOs                           | `AuthResponse`, `LoginRequest` (optionally) |
| **Indexers**             | `[]` access on custom classes           | Array-like syntax                     | `Dictionary`, `Headers`, `Configuration`    |

**Core principle:** modern C# is not harder — it is *shorter*. NuGet replaces manual DLLs, top-level statements replace boilerplate, records replace 15-line DTOs, and indexers replace `.Get(x)` with `[x]`. Once you recognize them, the language reads faster.

---
