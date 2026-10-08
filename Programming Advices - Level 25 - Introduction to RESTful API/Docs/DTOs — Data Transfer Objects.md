# DTOs — Data Transfer Objects

## What is a DTO?

A **DTO** (Data Transfer Object) is a plain class whose only job is to **carry data across a boundary**. It has fields, maybe some validation attributes, and nothing else — no business logic, no methods that change state, no database queries.

```csharp
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

That's a DTO. A class with properties. No behavior.

The name says exactly what it is: an object that transfers data. From one layer to another, or from the server to a client. Nothing more.

---

## The core question — why not just use the domain model?

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

It has everything the client needs. Why introduce a separate `StudentDto` that looks identical?

**Because the two have different jobs, different owners, and different lifecycles.**

|                  | Domain Model (`Student`)             | DTO (`StudentDto`)                |
| ---------------- | ------------------------------------ | --------------------------------- |
| **Purpose**      | Represent the business entity        | Carry data across a boundary      |
| **Lives in**     | Business layer / domain              | API contracts layer               |
| **Owned by**     | The domain team / business rules     | The API team / public contract    |
| **Changes when** | Business rules change                | API contract changes              |
| **Contains**     | Business logic, invariants, behavior | Only the fields being transferred |
| **Who sees it**  | Internal code                        | External clients                  |

When you send `Student` directly to the client, you're saying: **"my internal model IS the public contract."** That's a problem, and it's worth understanding *why* before writing a single DTO.

---

## The 6 concrete reasons DTOs exist

### 1. You leak internals

Your `Student` might have fields the client should never see:

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

A `StudentDto` exposes only what's allowed:

```csharp
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}
```

The internal fields simply don't exist on the DTO. The client can't see what isn't there.

### 2. Your data model changes, your contract shouldn't

You rename `Grade` to `FinalGrade` in the database. You split `Name` into `FirstName` and `LastName`. You add a new required field. **Every client breaks** because the shape of the JSON changed.

If a DTO sits between the domain and the client:

- Rename the domain field.
- Update the mapping.
- The DTO stays the same.
- **Client never notices.**

The DTO is a **buffer** between the internal model and the public contract. You can refactor the domain freely; the contract is stable.

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

`FullName` and `Status` don't exist in the domain — they're specific to what the client needs. A DTO lets you shape the response without touching the domain.

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

The client can't set `Id` or `CreatedAt` — the server assigns those. Using two different DTOs enforces this. If you reused a single `Student` for both input and output, a malicious client could send `"id": 999` and try to hijack another record.

**Rule:** input and output DTOs should be different classes when the fields differ. Often they do.

### 5. Validation lives on the DTO

The DTO is the right place for validation rules that apply to the API boundary:

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

ASP.NET Core validates automatically on bind. Bad requests get a `400 Bad Request` before your controller method runs. Your action method never sees invalid data.

**Where does this validation belong? On the DTO, not the domain model.** The domain model might have different constraints (a `Grade` of 150 might be valid for extra credit, but the API caps it at 100). Keeping validation on the DTO means the API contract has its own rules, independent of the domain.

### 6. The domain model might be expensive to serialize

Real domain models often carry heavy relationships:

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

Serialize this directly and every `GET /orders/5` returns a massive payload — customer details, payments, shipments, and the entire audit history. Slow, wasteful, and often unintended.

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

The client gets what they need. Nothing more.

---

## The relationship to statelessness

This is the part worth getting right. Statelessness and DTOs are connected, but not in a way that's obvious at first.

**Statelessness means:** each request must carry everything needed to process it. The server doesn't remember context between calls.

**What that implies:**

- Every request is self-contained — no "the client is on step 3 of a wizard, so the server knows what to do next."
- Every response is a snapshot of the current state — no incremental updates relying on the client's memory of previous responses.
- The server treats each call as if it's the first.

**Where DTOs come in:**

Because each request and response is a **complete, self-contained snapshot**, the shape of that snapshot matters. It's not "send whatever's in memory" — it's "send exactly what this operation needs, in the shape the client expects."

The DTO **is** the shape of that snapshot:

- Input DTO = what the client sends to describe its intent.
- Output DTO = what the server sends back to describe the resulting state.

In a stateful design (think server-side sessions), the server keeps a running context. It might send partial updates because it knows what the client already has. In a stateless design, there's no such continuity — each DTO is a full replacement for the client's understanding of the resource.

**Concrete example:**

Stateless API: `GET /students/5` returns the full `StudentResponseDto`. Client's local copy is now valid. Next call returns the full DTO again — the client replaces its local copy entirely. No partial diffs, no incremental sync.

Stateful design (hypothetical): the server could send only the changed fields, because it knows what the client's local state was. But that requires the server to track client state — which contradicts statelessness.

**So the connection is:**

> Stateless APIs need complete, well-defined snapshots of data per request/response. DTOs are the objects that define those snapshots. They're how the API communicates a full state, not a partial one, in a shape that's decoupled from the internal model.

The DTO is the unit of state transfer. Statelessness makes that unit complete per call. Those two ideas reinforce each other.

---

## The mental model

Think of DTOs as **envelopes** and the domain model as the **filing cabinet**.

- The **filing cabinet** (domain) has all the documents, in whatever order the business needs them.
- The **envelope** (DTO) contains only what's being sent to someone else.
- The envelope is separate from the cabinet. You don't mail the whole cabinet.

If you ever need to change how the cabinet is organized, you don't have to tell everyone who received an envelope last week. The envelope is its own thing.

---

## When to use DTOs (and when not to)

**Use DTOs when:**

- The API is public-facing or shared with other teams.
- The domain model contains internal or sensitive fields.
- The client needs a different shape than the domain (flattened, combined, computed).
- Input and output have different fields.
- You want stable API contracts while the domain evolves.
- The domain model has heavy relationships to trim.

**Skip DTOs when:**

- The API is a small internal tool and the domain model *is* the interface by design.
- The DTO would be identical to the domain model, forever. (Rare — usually one of the two starts diverging.)
- You're writing a throwaway prototype.

Most production APIs use DTOs. The overhead is small (a class + mapping) and the benefits compound as the API lives longer.

---

## DTOs and the layers of your app

A typical REST API with DTOs:

```
Client
   ↕ (JSON)
DTO                    ← the API boundary
   ↕ (mapping)
Domain Model           ← the business layer
   ↕ (mapping)
Entity (DB model)      ← the persistence layer
```

Each layer has its own shape:

- **Entity** matches the database schema (column names, FK relationships).
- **Domain Model** matches business rules and invariants.
- **DTO** matches what the API exposes to clients.

The maps between them are where translation happens:

- Entity → Domain: "load from DB into business object."
- Domain → DTO: "package a subset of the business object for the client."
- DTO → Domain: "unpack the client's request into a business object."

**Common mistake:** skipping a layer. Using entities as domain models, or domain models as DTOs. Each shortcut works until it doesn't — and then you're stuck with a schema-locked API or a leaky security boundary.

**Common overkill:** having all three layers for a simple CRUD app. Sometimes one class *is* all three (the API is trivial, the domain has no logic). This is fine for small projects. Just be aware you've made the choice deliberately.

---

## Summary

| Question                               | Answer                                                                                                       |
| -------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| What is a DTO?                         | A plain class for carrying data across a boundary — no logic, just fields                                    |
| Why not use the domain model directly? | You'd leak internal fields, couple the API to the domain, and lose control of the contract                   |
| Key benefits                           | Encapsulation, security, flexibility, stable contracts, validation, trimmed payloads                         |
| Input vs output DTOs                   | Different classes when fields differ — the client shouldn't be able to set server-assigned fields            |
| Relation to statelessness              | Stateless requests/responses are self-contained snapshots — DTOs are the objects that define those snapshots |
| When to skip DTOs                      | Small internal tools where the domain model *is* the interface by design                                     |
| Common mistake                         | Skipping a layer — using entities as DTOs, or domain models as entities                                      |

---

## One-line summary

> A DTO is the shape of the data as it crosses the API boundary. It exists because the API contract and the internal model have different owners, different lifecycles, and different jobs. Using the domain model directly couples them together — and in a stateless API, every request and response is a fresh, complete snapshot, so getting that snapshot's shape right is the whole point.

---

Save as `DTOs - Data Transfer Objects.md` under `Docs/` in Level 25. This is a concept you'll come back to every time you design an API boundary.
