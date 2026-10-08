# DTOs — Data Transfer Objects

## What is a DTO?

A **DTO** (Data Transfer Object) is <mark>a **<u>plain class</u>** **whose only job** is to **carry data <u>across</u> a [boundary](#what-is-a-boundry)**</mark>. It has fields, **<u>maybe some validation attributes</u>**, and nothing else — <mark>no business logic, <u>no methods that change state</u>, no database queries</mark>.

```csharp
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

That's a DTO. <mark>A class with properties</mark>. <mark>**<u>No behavior</u>**</mark>.

The name says exactly what it is: <mark>an object **that transfers data**</mark>. **<u>From one layer to another</u>**, or **<u>from the server to a client</u>**. Nothing more.

---

### What is a Boundry

**Boundary** = <mark>**any place where data or control crosses <u>from</u>**</mark> one system, layer, or trust zone to another.

<mark>**Three common types**</mark>:

| Boundary             | Between                  | Example                                             |
| -------------------- | ------------------------ | --------------------------------------------------- |
| **Process boundary** | Two programs             | Client ↔ Server over HTTP                           |
| **Layer boundary**   | Two architectural layers | Controller ↔ Domain, Domain ↔ Database              |
| **Trust boundary**   | Two trust levels         | Your server ↔ the outside world (untrusted clients) |

---

**When data "crosses a boundary":**

- Client sends JSON → server receives it. **Process + trust boundary crossed.**
- Controller passes a `Student` object to the domain layer. **Layer boundary crossed.**
- Domain saves to the database. **Process boundary crossed** (network or disk).

---

**Why it matters for DTOs:**

<mark>A DTO exists **because of a boundary**</mark>. It's the <mark>object that carries data</mark> <mark>**across that boundary**</mark> <mark>in the shape **the receiving side <u>expects</u>**</mark>.

- `CreateStudentRequest` — crosses the boundary **<u>into</u>** the server (client → API).
- `StudentResponse` — crosses the boundary **<u>out of</u>** the server (API → client).
- `StudentDto` — crosses the boundary **<u>between</u>** the controller and the domain layer.

<mark>Without a boundary, <u>you wouldn't need a DTO</u></mark> — you'd just pass the domain object around. DTOs are a direct consequence of having boundaries.

---

> <mark>A **boundary** is any point <u>where data crosses</u></mark> from one system, layer, or trust zone to another — client to server, controller to domain, application to database. <mark>Every boundary is a translation point</mark>: the shape of the data on one side doesn't have to match the shape on the other. <mark>**<u>DTOs exist because of boundaries</u>**</mark> — they're the objects that carry data across in the shape the receiving side expects.

---

## <mark>**The core question**</mark> — why not just use the domain model?

You already have `Student`:

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

It has everything the client needs. <mark>Why introduce a separate `StudentDto` that **looks identical** ?</mark>

**Because <mark>the two have different jobs</mark>, <mark>different owners</mark>, and <mark>different lifecycles.</mark>**

|                  | Domain Model (`Student`)                          | DTO (`StudentDto`)                                 |
| ---------------- | ------------------------------------------------- | -------------------------------------------------- |
| **Purpose**      | <mark>Represent the business entity</mark>        | <mark>Carry data **across a boundary**</mark>      |
| **Lives in**     | Business layer / domain                           | <mark>**API contracts layer**</mark>               |
| **Owned by**     | <mark>**The domain team** / business rules</mark> | <mark>**The API team**</mark> / public contract    |
| **Changes when** | Business rules change                             | <mark>API contract changes</mark>                  |
| **Contains**     | **Business logic, invariants, behavior**          | <mark>**Only the fields**</mark> being transferred |
| **Who sees it**  | <mark>**<u>Internal code</u>**</mark>             | <mark>**<u>External clients</u>**</mark>           |

When you send `Student` directly to the client, you're saying: **"my internal model IS the public contract."** <mark>**That's a problem**</mark>, and it's worth understanding *why* before writing a single DTO.

---

## The 6 concrete reasons DTOs exist

### 1. You leak internals

Your `Student` <mark>**might have fields the client should never see**</mark>:

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
    public string PasswordHash { get; set; }        // ← must not go to the client
    public DateTime LastLoginAt { get; set; }        // ← internal
    public bool IsDeleted { get; set; }               // ← internal
    public string InternalNotes { get; set; }        // ← internal
}
```

Return this directly and every client gets the password hash and every internal field. **Security incident.**

<mark>A `StudentDto` **exposes only** what's allowed</mark>:

```csharp
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

<mark>**<u>The internal fields simply don't exist on the DTO</u>**</mark>. The client can't see what isn't there.

### 2. Your data model changes, your contract shouldn't

You rename `Grade` to `FinalGrade` in the database. You split `Name` into `FirstName` and `LastName`. You add a new required field. <mark>**Every client breaks** because the shape of the JSON changed</mark>.

<u>**If a DTO sits between the domain and the client**</u>:

- Rename the domain field.
- Update the mapping.
- <mark>**The DTO stays the same**</mark>.
- <mark><u>**Client never notices.**</u></mark>

<mark>The DTO is a **buffer** between the internal model and the public contract</mark>. You can refactor the domain freely; <mark>**<u>the contract is stable</u>**</mark>.

### 3. The domain and the API have different shapes

The client might need fields that don't exist in the domain model. Or the domain model has data that needs to be *reorganized* for the client.

Example — you want to send the student's full name as one field, plus a computed "pass/fail" status:

```csharp
public class StudentResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; }        // computed from FirstName + LastName
    public int Age { get; set; }
    public int Grade { get; set; }
    public string Status { get; set; }           // "Passed" or "Failed" — computed from Grade
}
```

`FullName` and `Status` don't exist in the domain — <mark>they're specific to what the client needs</mark>. A DTO lets you shape the response <mark>**without touching the domain**</mark>.

### 4. Input and output are different

The client sends one shape, receives another.

**Input DTO (what the client sends):**

```csharp
public class CreateStudentRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

**Output DTO (what the client receives):**

```csharp
public class StudentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

The client can't set `Id` or `CreatedAt` — the server assigns those. <mark>Using two different DTOs enforces this</mark>. If you reused a single `Student` for both input and output, a malicious client could send `"id": 999` and try to hijack another record.

> <mark>**Rule:**</mark> **input and output DTOs should be different classes when the fields differ**. <mark>**<u>Often they do</u>**</mark>.

### 5. Validation lives on the DTO

The DTO <mark>**is the right place for validation rules**</mark> that **<u>apply to the API boundary</u>**:

```csharp
public class CreateStudentRequest
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; }

    [Range(0, 150)]
    public int Age { get; set; }

    [Range(0, 100)]
    public int Grade { get; set; }
}
```

<mark>ASP.NET Core validates automatically **on bind**</mark>. Bad requests get a `400 Bad Request` before your controller method runs. <mark><u>Your action method never sees invalid data</u></mark>.

**Where does this validation belong? On the DTO, not the domain model.** The domain model might have different constraints (a `Grade` of 150 might be valid for extra credit, but the API caps it at 100). Keeping validation on the DTO means <mark>**the API contract has its own rules**</mark>, <mark>independent of the domain</mark>.

### 6. The domain model might be expensive to serialize

**Real domain** models often <mark>**<u>carry heavy relationships</u>**</mark>:

```csharp
public class Order
{
    public int Id { get; set; }
    public List<OrderItem> Items { get; set; }       // nested
    public Customer Customer { get; set; }            // nested
    public List<Payment> Payments { get; set; }       // nested
    public List<Shipment> Shipments { get; set; }     // nested
    public List<AuditLog> AuditTrail { get; set; }    // huge
}
```

Serialize this directly and every `GET /orders/5` <mark>**returns a massive payload**</mark> — customer details, payments, shipments, and the entire audit history. <mark>**<u>Slow, wasteful, and often unintended</u>**</mark>.

A DTO trims it:

```csharp
public class OrderSummaryDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}
```

<mark>**The client gets <u>what they need</u>**</mark>. Nothing more.

---

## The relationship to statelessness

This is the part worth getting right. <mark>**Statelessness and DTOs are connected**</mark>, but not in a way that's obvious at first.

**Statelessness means:** each request must carry everything needed to process it. The server doesn't remember context between calls.

**What that implies:**

- <mark>Every request is self-contained</mark> — no "the client is on step 3 of a wizard, so the server knows what to do next."
- <mark>Every response is a snapshot of the current state</mark> — no incremental updates relying on the client's memory of previous responses.
- <mark>The server treats each call as if it's the first</mark>.

**Where DTOs come in:**

Because each request and response is a **complete, self-contained snapshot**, the shape of that snapshot matters. It's not "send whatever's in memory" — it's "send exactly what this operation needs, in the shape the client expects."

The DTO **is** the shape of that snapshot:

- <mark>**Input DTO**</mark> = what the client sends <mark>**<u>to describe its intent</u>**</mark>.
- <mark>**Output DTO**</mark> = what the server sends back <mark>**to describe the resulting state**</mark>.

In a stateful design (think server-side sessions), the server keeps a running context. It might send partial updates because it knows what the client already has. <mark>**In a stateless design**, there's no such continuity</mark> — <mark>**each DTO is a full replacement**</mark> for the client's understanding of the resource.

**Concrete example:**

Stateless API: `GET /students/5` returns the full `StudentResponseDto`. Client's local copy is now valid. Next call returns the full DTO again — <mark>the client **replaces** its local copy entirely</mark>. <mark>**No partial diffs, no incremental sync**</mark>.

Stateful design (hypothetical): the server could send only the changed fields, because <mark>it knows what **the client's local state** was</mark>. But that requires the server to track client state — <mark>**<u>which contradicts statelessness</u>**</mark>.

**So the connection is:**

> <mark>Stateless APIs need **complete, well-defined snapshots of data** per request/response</mark>. DTOs are the objects that define those snapshots. <mark>**They're how the API communicates a full state**</mark>, not a partial one, <mark>in a shape that's **decoupled from the internal model**</mark>.

> <mark>**<u>The DTO is the unit of state transfer</u>**</mark>. Statelessness <mark>**makes that unit complete**</mark> per call. Those two ideas reinforce each other.

---

## The mental model

Think of DTOs as **envelopes** and the domain model as the **filing cabinet**.

- The **filing cabinet** (domain) has all the documents, in whatever order the business needs them.
- The **envelope** (DTO) contains only what's being sent to someone else.
- <mark>The envelope is separate from the cabinet</mark>. You don't mail the whole cabinet.

If you ever need to change how the cabinet is organized, you don't have to tell everyone who received an envelope last week. <mark>**The envelope is its own thing**</mark>.

---

## When to use DTOs (and when not to)

**Use DTOs when:**

- The API is public-facing or shared with other teams.
- The domain model <mark>**contains internal or sensitive fields**</mark>.
- The client <mark>**needs a different shape than the domain**</mark> (flattened, combined, computed).
- Input and output <mark>**have different fields**</mark>.
- You want <mark>**stable API contracts <u>while the domain evolves</u>**</mark>.
- The domain model <mark>**has heavy relationships to trim**</mark>.

**Skip DTOs when:**

- The API is a small internal tool and the domain model *is* the interface by design.
- The DTO would be identical to the domain model, forever. (Rare — usually one of the two starts diverging.)
- You're writing a throwaway prototype.

<mark>**Most production APIs use DTOs**</mark>. The overhead is small (a class + mapping) and the benefits compound as the API lives longer.

---

## DTOs and the layers of your app

A typical REST API with DTOs:

```js
Client
   ↕ (JSON)
DTO                    ← the API boundary
   ↕ (mapping)
Domain Model           ← the business layer
   ↕ (mapping)
Entity (DB model)      ← the persistence layer
```

<mark>**Each layer has its own shape**</mark>:

- **Entity** matches <mark>the database schema</mark> (column names, FK relationships).
- **Domain Model** matches <mark>business rules and invariants</mark>.
- **DTO** matches <mark>what the API exposes to clients</mark>.

The maps between them are **<u>where translation happens</u>**:

- Entity → Domain: "load from DB into business object."
- Domain → DTO: "package a subset of the business object for the client."
- DTO → Domain: "unpack the client's request into a business object."

**Common mistake:** <mark>**skipping a layer**</mark>. Using entities as domain models, or domain models as DTOs. Each shortcut works until it doesn't — and then you're stuck with a schema-locked API or a leaky security boundary.

**Common overkill:** <mark>**having all three layers for a simple CRUD app**</mark>. Sometimes one class *is* all three (the API is trivial, the domain has no logic). This is fine for small projects. Just be aware you've made the choice deliberately.

---

## Summary

| Question                               | Answer                                                                                                                        |
| -------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------- |
| What is a DTO?                         | A plain class <mark>**for carrying data across a boundary**</mark> — <mark>**no logic**</mark>, just fields                   |
| Why not use the domain model directly? | <mark>**You'd leak internal fields**</mark>, <mark>**couple the API to the domain**</mark>, and lose control of the contract  |
| Key benefits                           | Encapsulation, security, flexibility, stable contracts, validation, trimmed payloads                                          |
| Input vs output DTOs                   | <mark>**Different classes when fields differ**</mark> — the client shouldn't be able to set server-assigned fields            |
| Relation to statelessness              | <mark>**Stateless requests/responses are self-contained snapshots**</mark> — DTOs are the objects that define those snapshots |
| When to skip DTOs                      | Small internal tools where the domain model *is* the interface by design                                                      |
| Common mistake                         | Skipping a layer — using entities as DTOs, or domain models as entities                                                       |

---

## One-line summary

> A DTO is the shape of the data as it crosses the API boundary. It exists because the API contract and the internal model have different owners, different lifecycles, and different jobs. Using the domain model directly couples them together — and in a stateless API, every request and response is a fresh, complete snapshot, so getting that snapshot's shape right is the whole point.

---


