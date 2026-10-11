# DTO Construction Rules in ASP.NET Core

> **TL;DR** — DTOs you *receive* use a parameterless ctor + public setters (model binder fills them). DTOs you *send back* use a ctor with all params (you build them). That's why `StudentResponseDTO` has a constructor and the request DTOs don't.

---

## The two directions of data flow

```
   Client  ──────►  API  ──────►  Client
            request       response

   [Request DTO]     [Response DTO]
   created by:       created by:
   model binder      your code
```

| DTO                       | Direction               | Created by                | Needs ctor? |
| ------------------------- | ----------------------- | ------------------------- | ----------- |
| `CreateStudentRequestDTO` | Incoming (client → API) | ASP.NET Core model binder | ❌ No        |
| `UpdateStudentRequestDTO` | Incoming (client → API) | ASP.NET Core model binder | ❌ No        |
| `StudentResponseDTO`      | Outgoing (API → client) | **Your code** (DAL / BL)  | ✅ Yes       |

---

## Why request DTOs don't need a constructor

Client sends:

```json
{ "name": "Ali", "age": 20, "grade": 90 }
```

The model binder does:

1. `new CreateStudentRequestDTO()` — parameterless ctor
2. Sets each property via its **public setter**

So it's just:

```csharp
public class CreateStudentRequestDTO
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 150)]
    public int Age { get; set; }

    [Range(0, 100)]
    public int Grade { get; set; }
}
```

No ctor needed — the framework fills it in.

---

## Why `StudentResponseDTO` **does** have a constructor

You build it yourself in two places:

**In the DAL** (mapping from a DB row):

```csharp
private static StudentResponseDTO MapToResponseDTO(SqlDataReader reader)
{
    return new StudentResponseDTO(
        reader.GetInt32(reader.GetOrdinal("Id")),
        reader.GetString(reader.GetOrdinal("Name")),
        reader.GetInt32(reader.GetOrdinal("Age")),
        reader.GetInt32(reader.GetOrdinal("Grade"))
    );
}
```

**In the BL** (`ToDTO()`):

```csharp
public StudentResponseDTO ToDTO()
    => new StudentResponseDTO(ID, Name, Age, Grade);
```

The ctor makes it one-shot and safe:

```csharp
public class StudentResponseDTO
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public int Grade { get; }

    public StudentResponseDTO(int id, string name, int age, int grade)
    {
        Id = id;
        Name = name;
        Age = age;
        Grade = grade;
    }
}
```

### Without the ctor, you'd write:

```csharp
var dto = new StudentResponseDTO();
dto.Id = id;
dto.Name = name;
dto.Age = age;
dto.Grade = grade;
```

<mark>More verbose, and you can accidentally produce a **half-filled** DTO</mark>.

---

## The general rule

> **<mark>DTOs you _receive_</mark>** → parameterless ctor + settable properties.
> **<mark>DTOs you _send back</mark>_ → constructor with all fields, no setters (read-only).**

```js
   Input DTO                         Output DTO
   ─────────                         ──────────
   mutable                           immutable
   empty ctor                        all-args ctor
   { get; set; }                     { get; }
   model binder builds it            your code builds it
   validation attributes on props    values already validated
```

---

## Bonus: how the old `StudentDTO` broke the rule

The original single `StudentDTO` had **both** a ctor *and* settable properties:

```csharp
public class StudentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }

    public StudentDTO(int id, string name, int age, int grade) { ... }
}
```

<mark>It tried to serve **both roles at once** (input **and** output)</mark>. That's why splitting it into three DTOs made the design cleaner:

```
StudentDTO  (old, one class, two jobs)
    │
    ├──► CreateStudentRequestDTO   (input,  no ctor)
    ├──► UpdateStudentRequestDTO   (input,  no ctor)
    └──► StudentResponseDTO        (output, all-args ctor)
```

<mark>Each DTO now has exactly **one job** and **one construction style**.</mark>

---

## Optional: making request DTOs immutable too

If you want the input side to also be immutable, use `init` setters (C# 9+) or `record`:

```csharp
// Option A — init setters
public class CreateStudentRequestDTO
{
    [Required] public string Name { get; init; } = string.Empty;
    [Range(1, 150)] public int Age { get; init; }
    [Range(0, 100)] public int Grade { get; init; }
}
```

```csharp
// Option B — positional record
public record CreateStudentRequestDTO(
    [Required, StringLength(50, MinimumLength = 1)] string Name,
    [Range(1, 150)] int Age,
    [Range(0, 100)] int Grade
);
```

<mark>Both work fine with the model binder — it can set `init` properties during deserialization.</mark>

---

## <mark>**Cheat sheet**</mark>

| Question                                   | Answer                                                                                                                               |
| ------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------ |
| Why does `StudentResponseDTO` have a ctor? | **Your code** creates it from known values (DB row, BL entity).                                                                      |
| Why don't the request DTOs have one?       | The **model binder** creates them by `new()` + property setters.                                                                     |
| What if I add a ctor to a request DTO?     | The binder will try to use it only if the params match JSON names; otherwise it fails or picks the wrong one. Keep it parameterless. |
| Can I make request DTOs read-only?         | Yes — use `init` setters or a `record`.                                                                                              |

---
