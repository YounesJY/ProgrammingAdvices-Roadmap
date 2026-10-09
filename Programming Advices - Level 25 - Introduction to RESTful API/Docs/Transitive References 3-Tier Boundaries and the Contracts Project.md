# Transitive References, 3-Tier Boundaries, and the Contracts Project

## What this covers

<mark>A real problem we hit while building the StudentAPI</mark>: the DTO lived in the Data Access Layer, but the API layer <mark>**needed to use it**</mark>. Directly referencing the DAL from the API <mark>**would violate 3-tier architecture**</mark>. <mark><u>But the code compiled anyway</u></mark>. This MD explains why, and what the correct fix is.

---

## The setup

Standard 3-tier for a .NET Web API:

```
API Layer  →  Business Layer  →  Data Access Layer
```

The API references the BL. The BL references the DAL. <mark>**Nobody skips a tier**</mark>.

Each layer:

| Layer   | Job                                                    |
| ------- | ------------------------------------------------------ |
| **API** | Handle HTTP. **<u>Call the BL</u>**. Return responses. |
| **BL**  | Business rules. **<u>Call the DAL</u>**.               |
| **DAL** | <mark>**Talk to the database**</mark>. Return data.    |

<mark>The **DTO** (`StudentDTO`) **crosses every boundary**</mark>. It's what the API sends to the client, what the BL uses to hold state, and what the DAL returns from queries.

---

## The problem

In this project, <mark>`StudentDTO` was defined **inside the Data Access Layer**</mark>.

```csharp
namespace StudentDataAccessLayer
{
    public class StudentDTO { ... }
    public class StudentData { ... }
}
```

That means:

- The API needs `StudentDTO` to build responses → needs `StudentDTO` from `StudentDataAccessLayer`
- The BL needs `StudentDTO` for its method signatures → also from `StudentDataAccessLayer`
- <mark>Both layers **would have to reference the DAL directly**</mark>

**Referencing the DAL from the API <mark><u>is a 3-tier violation.</u></mark>**

Why? Because the API layer <mark>now **sees everything** the DAL exposes</mark>:

- `StudentData.GetAllStudents()`
- `StudentData.AddStudent(...)`
- All the internal ADO.NET types (`SqlConnection`, `SqlCommand`, etc.)

Once the API can call `StudentData.AddStudent()` directly, <mark>**the business layer becomes optional**</mark>. That's the whole point of 3-tier — <mark>the API **should not be able** to reach past the BL</mark>.

> <mark>**But we didn't add that reference.**</mark> And **<u>the code still compiled</u>**.

---

## Why the code compiled anyway

**In modern .NET (Core / .NET 5+), <mark>project references are transitive.</mark>**

If project A references project B, and project B references project C, then <mark>**A automatically gets access to C's types.**</mark> <mark>**<u>No explicit reference needed</u>**</mark>.

Applied to our case:

```
API  →  BL  →  DAL
```

The API references the BL. The BL references the DAL. So <mark>the API automatically sees `StudentDataAccessLayer` types</mark> — including `StudentDTO`.

<mark>**The compiler didn't complain**</mark>. `using StudentDataAccessLayer;` **<u>worked in the API</u>** even though the API's `.csproj` **<u>didn't list the DAL as a reference</u>**.

<mark>**This was the surprise.**</mark> A rule we thought existed (3-tier must be explicit) wasn't enforced by the compiler.

---

## Why .NET made references transitive

<mark>Old .NET Framework projects (pre-2019) had **non-transitive** references</mark>. If A referenced B, and B referenced C, then A did not see C's types. <mark>Every reference **had to be listed explicitly**</mark>.

SDK-style projects (`.NET Core` and `.NET 5+`) <mark>**<u>changed this</u>**</mark>. The reasoning:

- **Convenience.** You don't have to list a dozen package references per project.
- **Correctness.** If B's public API exposes C's types, A needs to see them to use B properly. Making this automatic removes a class of "why won't this compile" errors.
- **NuGet alignment.** <mark>**<u>NuGet packages have always been transitive</u>**</mark>. The project system now matches.

> <mark>**<u>The cost:</u>**</mark> <mark>**layer boundaries are no longer enforced by the compiler.**</mark> They're enforced by convention.

---

## Why this breaks 3-tier

The point of 3-tier is that <mark>**each layer only knows about the layer below it**</mark>. **<u>The compiler is supposed to enforce this</u>**:

- The API can call `StudentService.Find(id)`, but not `StudentData.GetStudentById(id)`.
- If the API accidentally reaches into the DAL, the code doesn't compile.

<mark>With transitive references, the compiler **no longer enforces this**</mark>. `StudentData` is visible from the API. Nothing stops a developer from writing:

```csharp
// Inside the API controller
var students = StudentData.GetAllStudents();   // compiles!
```

<mark>**This bypasses the business layer entirely**</mark>. Data access logic, transaction handling, business rules — all skipped. <mark>**<u>The 3-tier architecture becomes cosmetic</u>**</mark>.

For a solo learning project, discipline is enough. For a team of ten, someone will eventually do this. The compiler won't stop them.

---

## The controls .NET gives you

<mark>**<u>Two ways to block transitive propagation</u>**</mark>:

### 1. `PrivateAssets="all"` on a project reference

In the BL's `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\StudentDataAccessLayer\StudentDataAccessLayer.csproj">
    <PrivateAssets>all</PrivateAssets>
  </ProjectReference>
</ItemGroup>
```

**Meaning:** "I use the DAL, but my consumers don't get it."

Result: the API, referencing the BL, no longer sees `StudentDataAccessLayer` types. It has to reference the DAL <mark>**explicitly if it wants to reach it**</mark>. Now the compiler <mark>**can enforce the boundary**</mark>.

**Downside:** the API also loses visibility of `StudentDTO` (because it lived in the DAL). The API can't compile until it either references the DAL directly (which is the problem we're trying to avoid) or <mark>**<u>the DTO moves somewhere shared</u>**</mark>.

### 2. `DisableTransitiveProjectReferences`

In a `.csproj` or `Directory.Build.props`:

```xml
<PropertyGroup>
  <DisableTransitiveProjectReferences>true</DisableTransitiveProjectReferences>
</PropertyGroup>
```

**Meaning:** "Every project must declare its dependencies explicitly. Nothing is inherited."

Result: every project's references become visible in its `.csproj`. <mark>**No hidden transitive chains**</mark>. Strict, explicit, verbose.

**Downside:** you write a lot of `<ProjectReference>` lines. If the DAL references five packages, every project that needs those five must list them. That's how old .NET Framework projects worked.

---

## The correct architectural fix: a shared Contracts project

Neither of the controls above fixes the root problem. <mark>The root problem is that **`StudentDTO` lives in the DAL.**</mark> <mark>**<u>It shouldn't</u>**</mark>.

<mark>**<u>The DTO crosses every boundary</u>**</mark>. Putting it inside any one layer makes the other layers depend on that layer just to get the DTO shape.

**The fix:** extract `StudentDTO` into a **shared library** <mark>**that every layer can reference directly**</mark>.

```js
Contracts  ←  shared project (StudentDTO lives here)
    ↑
    ├── API
    ├── BL
    └── DAL
```

New dependency graph:

```js
API  →  BL  →  DAL
 │      │      │
 └──────┴──────┴──→  Contracts
```

Nobody skips a tier to get the DTO. <mark>`Contracts` is a legitimate shared dependency</mark>, referenced by everyone. The 3-tier rule is preserved because the DTO isn't owned by any single tier.

### Why this works

- **The API references `Contracts` directly.** It gets `StudentDTO` <mark>without touching the DAL</mark>.
- **The BL references `Contracts` directly.** Same.
- **The DAL references `Contracts` directly.** Same.
- **Nobody references the DAL except the BL.** <mark>**<u>3-tier preserved</u>**</mark>.

### What Contracts contains

<mark>Only **shared types that cross boundaries**</mark>. For a Web API, that's typically:

- DTOs (request and response shapes)
- Shared enums
- Shared constants
- API-specific interfaces (if any)

**What does NOT go in Contracts:**

- Business logic (that's the BL)
- Data access code (that's the DAL)
- Controllers (that's the API)
- Entities (that's the DAL)

<mark>**Rule:**</mark> if a type is used by more than one layer, and it's not the responsibility of any single layer to own it, <mark>it belongs in Contracts</mark>.

---

## Naming

Common names for the shared project:

| Name            | Fits when                                                  |
| --------------- | ---------------------------------------------------------- |
| **`Contracts`** | Web API projects. The DTOs are literally the API contract. |
| `Common`        | General shared types — vague but common                    |
| `Shared`        | Same as Common                                             |
| `DTOs`          | Only DTOs in the project. Direct, but limiting.            |
| `Core`          | Ambiguous — often means the domain layer, not shared       |
| `Models`        | Generic — "models" could be anything                       |

For a Web API, **`Contracts`** is the most accurate.

---

## What we did in the project

1. **Created `Contracts.csproj`** as a class library in the solution.
2. **Moved `StudentDTO` into it**, changing its namespace from `StudentDataAccessLayer` to `Contracts`.
3. **Added a reference to `Contracts`** from the API, the BL, and the DAL.
4. **Removed any reference to the DAL from the API.**
5. **Updated `using` statements** in all consuming files.

After this:

- The API sees `StudentDTO` via `Contracts`.
- The API does not see `StudentData` because it no longer references the DAL.
- The compiler enforces the 3-tier boundary.

---

## What to take away

**1. Modern .NET references are transitive.** A project sees everything its dependencies see. That's a change from .NET Framework.

**2. This makes layer boundaries convention-based, not compiler-enforced.** If you want the compiler to enforce them, you have to opt in with `PrivateAssets` or `DisableTransitiveProjectReferences`.

**3. Shared types belong in a shared project.** If a DTO is used by multiple layers, no layer should own it. Put it in `Contracts` (or `Common`, `Shared`, etc.), and let every layer reference it explicitly.

**4. Direct references should match `using` statements.** If you `using Contracts;` in a file, the project should reference `Contracts` explicitly — even if transitive resolution makes it compile without the reference. The `.csproj` should document the actual dependencies.

**5. The compiler only enforces what you tell it to.** 3-tier is not a language feature. It's an architectural convention. If you want it enforced at compile time, you have to configure the project to enforce it.

---

## Summary table

| Question                                                                               | Answer                                                                                                    |
| -------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------- |
| Are .NET project references transitive?                                                | Yes, in SDK-style projects (.NET Core / .NET 5+)                                                          |
| Are NuGet package references transitive?                                               | Yes, always                                                                                               |
| Why does `using StudentDataAccessLayer` compile in the API without a direct reference? | Because the API → BL → DAL chain is transitive                                                            |
| Does this violate 3-tier?                                                              | The reference chain is legal, but the API gains access to DAL types it shouldn't see                      |
| How do you block transitive propagation?                                               | `<PrivateAssets>all</PrivateAssets>` on a reference, or `DisableTransitiveProjectReferences` project-wide |
| What's the correct fix for a shared DTO?                                               | Extract it into a shared `Contracts` project                                                              |
| What goes into Contracts?                                                              | Types used by multiple layers that no single layer should own                                             |
| How do you ensure the compiler enforces 3-tier?                                        | Configure the project correctly AND structure types so boundaries don't need to be broken                 |

---
