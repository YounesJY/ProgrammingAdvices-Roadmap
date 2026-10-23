# Policy-Based Authorization: Deep Dive & Mind Map

---

## 1. Introduction

The policy-based authorization lesson introduces several concepts that are easy to confuse:

- 🔹 The `IAuthorizationService` and how it relates to the handler
- 🔹 The `[FromServices]` attribute
- 🔹 The `Singleton` vs `Scoped` lifetime question
- 🔹 What "resource" actually means in resource-based authorization
- 🔹 The full authorization ecosystem built into ASP.NET Core

This MD is a **mind map** to clarify each point.

---

## 2. The "Handler Tied to the User" Confusion

### 2.1 The Concern

> "If the handler is a `Singleton`, and the user changes per request, won't the handler hold the wrong user?"

### 2.2 Why This Concern Is Valid (In Theory)

Look at this hypothetical (bad) handler:

```csharp
public class BadHandler : IAuthorizationHandler
{
    private readonly User _currentUser; // ❌ Stored in the class
}
```

If this handler were registered as `Singleton`:

- It is created **once** at app startup.
- It lives for the entire app lifetime.
- If it stored the user in a field, that field would be **frozen to the first user who triggered it**.
- Every request from that point on would check the wrong user.

📌 **This is why `Singleton` + state = disaster.**

### 2.3 Why the Course's Handler Is Safe

Look at the actual handler signature:

```csharp
protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context,   // user is passed HERE
    StudentOwnerOrAdminRequirement requirement,
    int studentId)
```

- The handler has **no fields**.
- The user is passed as a **parameter** every time.
- The handler reads it, uses it, and forgets it.
- The next request gets a fresh `context` with a fresh user.

📌 **Stateless handler → `Singleton` is safe.**

---

## 3. What Is `[FromServices] IAuthorizationService`?

### 3.1 The Attribute

```csharp
public async Task<ActionResult<Student>> GetStudentById(
    int id,
    [FromServices] IAuthorizationService authorizationService)
```

`[FromServices]` tells ASP.NET Core:

> "Do not look for this in the URL or the JSON body. Look in the **DI Container** (`builder.Services`) and give me the instance."

📌 It is **Dependency Injection applied to a method parameter**, not the constructor.

### 3.2 Why Not the Constructor?

The course uses `[FromServices]` for **learning clarity** — you can see exactly where the service is used, and it doesn't clutter the controller with a field.

In production code, **constructor injection** is more common:

```csharp
public class StudentsController : ControllerBase
{
    private readonly IAuthorizationService _auth;

    public StudentsController(IAuthorizationService auth)
    {
        _auth = auth;
    }
}
```

Both work. `[FromServices]` is just a shortcut.

---

## 4. Is `[FromServices]` Related to `AddSingleton<IAuthorizationHandler, ...>`?

**They are related, but they are NOT the same thing.**

Here is the mind map:

### 4.1 The Two Separate Registrations

| What you register                                                   | What it does                                     |
| ------------------------------------------------------------------- | ------------------------------------------------ |
| `AddSingleton<IAuthorizationHandler, StudentOwnerOrAdminHandler>()` | Registers **your custom handler** (the logic)    |
| `IAuthorizationService` (built-in)                                  | The **manager** that finds and runs your handler |

### 4.2 The Flow (Step by Step)

1. You register your **handler** in `Program.cs`.
2. ASP.NET Core registers `IAuthorizationService` automatically (built-in).
3. In the controller, `[FromServices]` injects the **built-in service**.
4. You call `AuthorizeAsync(User, id, "PolicyName")`.
5. The service:
   - Looks up the **policy** by name.
   - Finds the **requirement** inside the policy.
   - Searches for a registered **handler** that can evaluate that requirement.
   - Finds **your handler** (because you registered it).
   - Passes the user and the resource (`id`) to it.
   - Returns the result.

### 4.3 The Mind Map

```
You register the Handler  ─────►  DI Container
                                      │
                                      │ (ASP.NET Core looks here)
                                      ▼
IAuthorizationService  ─────►  Finds policy → finds requirement → finds handler → runs it
       ▲
       │
[FromServices] injects this into your controller
```

📌 **You never call the handler directly. The `IAuthorizationService` does it for you.**

---

## 5. What Is the "Resource" in Resource-Based Authorization?

### 5.1 The Short Answer

The "resource" is **<mark><u>the specific data</u> the user is trying to access or modify</mark>**.

### 5.2 In the Course

```csharp
AuthorizeAsync(User, id, "StudentOwnerOrAdmin")
//                ▲
//                └── This is the resource
```

The resource is the **`id`** from the route (`/api/students/5`). It is the **identifier** of the actual data in the database.

### 5.3 Why It Is Not the Data Itself

The authorization system does not need the full `Student` object. It only needs **<mark>enough information to make a decision</mark>**.

- The handler needs to know **which student** is being requested → it needs the `id`.
- The handler does **not** need the student's name, grade, or age.

📌 **Pass the minimum identifier, not the full object.**

### 5.4 Examples of "Resource" in Different Systems

| Endpoint                    | Resource                         |
| --------------------------- | -------------------------------- |
| `GET /api/students/5`       | `int id = 5`                     |
| `DELETE /api/comments/42`   | `int commentId = 42`             |
| `GET /api/files/report.pdf` | `string filename = "report.pdf"` |
| `PUT /api/orders/1001`      | `int orderId = 1001`             |

### 5.5 The Data vs Identifier Distinction

- **Resource identifier** = the `id` you pass to `AuthorizeAsync`.
- **Resource data** = the actual object in the database.

📌 The authorization system only needs the **identifier**. The controller loads the data after authorization passes (or before, depending on the design — this is a separate decision).

---

## 6. The Built-In Authorization Systems in ASP.NET Core

You asked: *"Do we have more systems built in?"*

**Yes. <mark><u>Five layers</u></mark> of authorization are built into ASP.NET Core.**

### 6.1 Role-Based (Coarse-Grained)

```csharp
[Authorize(Roles = "Admin")]
```

- Checks <mark>**the `role` claim**</mark> in the JWT.
- <mark>Runs **in the middleware**</mark> (before the controller).
- **Use for:** <mark>**broad access** decisions</mark> ("only admins").

### 6.2 Policy-Based (Fine-Grained)

```csharp
[Authorize(Policy = "MinimumAge18")]
```

- Checks <mark>**a named policy**</mark>.
- <mark>**Runs in the middleware**</mark> (before the controller).
- **Use for:** rules that <mark>**require logic**</mark> ("must be 18+").

### 6.3 Resource-Based (Contextual)

```csharp
await authorizationService.AuthorizeAsync(User, id, "OwnerOrAdmin");
```

- Checks <mark>the policy against a **<u>specific resource</u>**</mark>.
- Runs <mark>**<u>inside</u>** the controller</mark>.
- **Use for:** <mark>**ownership checks**</mark> ("is this your record?").

### 6.4 Claims-Based

```csharp
context.User.HasClaim("Department", "IT")
```

- Checks any claim in the JWT.
- Used <mark>**inside** a policy handler</mark>.
- **Use for:** <mark>**attribute-level** decisions</mark> ("only IT department").

### 6.5 Custom Middleware

```csharp
app.Use(async (context, next) => {
    // custom check here
});
```

- <mark>**<u>Full control</u>**</mark> over the pipeline.
- Runs <mark>**<u>before</u>** authentication and authorization</mark>.
- **Use for:** non-standard rules (IP allowlists, custom headers).

### 6.6 Summary Table

| Layer                 | Syntax                                   | Where           | Use For              |
| --------------------- | ---------------------------------------- | --------------- | -------------------- |
| **Role-Based**        | `[Authorize(Roles = "...")]`             | Middleware      | Admin/Student        |
| **Policy-Based**      | `[Authorize(Policy = "...")]`            | Middleware      | Complex static rules |
| **Resource-Based**    | `AuthorizeAsync(User, resource, policy)` | Controller      | Ownership            |
| **Claims-Based**      | `User.HasClaim(...)`                     | Inside handlers | Attribute checks     |
| **Custom Middleware** | `app.Use(...)`                           | Pipeline        | Non-standard rules   |

📌 **All five exist. They are not alternatives — they are layers.** A real system often uses all of them.

---

## 7. The Complete Mind Map (One Diagram)

```text
┌─────────────────────────────────────────────────────────────┐
│                     ASP.NET Core Request                    │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌──────────────────────────────────────────────────────────────┐
│  Middleware Pipeline (HTTPS, CORS, Auth, Authorization)      │
│                                                              │
│  [Authorize(Roles = "Admin")]         ← Role-based           │
│  [Authorize(Policy = "MinimumAge18")] ← Policy-based (static)│
└──────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌──────────────────────────────────────────────────────────────┐
│                    Controller Action                         │
│                                                              │
│  [FromServices] IAuthorizationService _auth;                 │
│                                                              │
│  await _auth.AuthorizeAsync(User, id, "OwnerOrAdmin");       │
│       │                       ▲                              │
│       │                       └── The resource (id)          │
│       │                                                      │
│       ▼                                                      │
│  ┌──────────────────────────────────────────────┐            │
│  │  IAuthorizationService (built-in manager)    │            │
│  │                                              │            │
│  │  Finds policy → finds requirement →          │            │
│  │  finds your registered handler → runs it     │            │
│  └──────────────────────────────────────────────┘            │
│       │                                                      │
│       ▼                                                      │
│  ┌──────────────────────────────────────────────┐            │
│  │  Your Handler (registered in Program.cs)     │            │
│  │  StudentOwnerOrAdminHandler                  │            │
│  └──────────────────────────────────────────────┘            │
│       │                                                      │
│       ▼                                                      │
│  Result: Succeeded / Failed → controller decides             │
└──────────────────────────────────────────────────────────────┘
```

---

## 📌 Summary

| Question                                                              | Answer                                                                                  |
| --------------------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| Why is `Singleton` safe for the handler?                              | It is stateless — the user is passed as a parameter each time                           |
| When to switch to `Scoped`?                                           | When the handler injects `DbContext` or any `Scoped` service                            |
| What is `[FromServices]`?                                             | DI on a method parameter (not the constructor)                                          |
| Is `[FromServices]` related to `AddSingleton<IAuthorizationHandler>`? | They are separate: one is your handler, the other is the built-in manager that finds it |
| What is "the resource"?                                               | The identifier (usually `{id}`) of the data the user is trying to access                |
| Is it the path parameter or the DB object?                            | The path parameter — the authorization system does not need the full object             |
| How many authorization layers does ASP.NET Core have?                 | **Five:** Role-based, Policy-based, Resource-based, Claims-based, Custom Middleware     |
| Do they replace each other?                                           | ❌ No — they are layers used together                                                    |

**Core principle:** the authorization system is a **chain**: `[FromServices]` <mark>**injects**</mark> the manager → the manager <mark>**finds**</mark> the policy → the policy <mark>**finds**</mark> the requirement → the requirement <mark>**finds**</mark> your handler → your handler <mark>**runs**</mark>. The "resource" is the identifier that <mark>**flows through this chain**</mark>, giving the handler <mark>**the context**</mark> it needs to <mark>**make a decision**</mark>.If this class is a `Singleton`, it is created **once** when the app starts. It sits in memory forever. If you stored the current user inside it, the very first user who logs in would be stuck there forever. Every subsequent request would be checked against *that* first user. 

**Why is `Singleton` safe for the course's handler?**
Because the handler has **no fields**. It is "stateless." The user is passed into the `HandleRequirementAsync(context, ...)` method as a parameter *every single time*. So, it doesn't matter if it's a Singleton; it just reads the parameter and forgets it. 

---
