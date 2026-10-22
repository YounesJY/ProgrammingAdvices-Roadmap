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

The "resource" is **the specific data the user is trying to access or modify**.

### 5.2 In the Course

```csharp
AuthorizeAsync(User, id, "StudentOwnerOrAdmin")
//                ▲
//                └── This is the resource
```

The resource is the **`id`** from the route (`/api/students/5`). It is the **identifier** of the actual data in the database.

### 5.3 Why It Is Not the Data Itself

The authorization system does not need the full `Student` object. It only needs **enough information to make a decision**.

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

**Yes. Five layers of authorization are built into ASP.NET Core.**

### 6.1 Role-Based (Coarse-Grained)

```csharp
[Authorize(Roles = "Admin")]
```

- Checks the `role` claim in the JWT.
- Runs in the middleware (before the controller).
- **Use for:** broad access decisions ("only admins").

### 6.2 Policy-Based (Fine-Grained)

```csharp
[Authorize(Policy = "MinimumAge18")]
```

- Checks a named policy.
- Runs in the middleware (before the controller).
- **Use for:** rules that require logic ("must be 18+").

### 6.3 Resource-Based (Contextual)

```csharp
await authorizationService.AuthorizeAsync(User, id, "OwnerOrAdmin");
```

- Checks the policy against a **specific resource**.
- Runs **inside** the controller.
- **Use for:** ownership checks ("is this your record?").

### 6.4 Claims-Based

```csharp
context.User.HasClaim("Department", "IT")
```

- Checks any claim in the JWT.
- Used **inside** a policy handler.
- **Use for:** attribute-level decisions ("only IT department").

### 6.5 Custom Middleware

```csharp
app.Use(async (context, next) => {
    // custom check here
});
```

- Full control over the pipeline.
- Runs **before** authentication and authorization.
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
│                     ASP.NET Core Request                     │
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│  Middleware Pipeline (HTTPS, CORS, Auth, Authorization)      │
│                                                              │
│  [Authorize(Roles = "Admin")]         ← Role-based           │
│  [Authorize(Policy = "MinimumAge18")] ← Policy-based (static)│
└─────────────────────────────────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
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
│  │  IAuthorizationService (built-in manager)     │            │
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
└─────────────────────────────────────────────────────────────┘
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

**Core principle:** the authorization system is a **chain**: `[FromServices]` injects the manager → the manager finds the policy → the policy finds the requirement → the requirement finds your handler → your handler runs. The "resource" is the identifier that flows through this chain, giving the handler the context it needs to make a decision.If this class is a `Singleton`, it is created **once** when the app starts. It sits in memory forever. If you stored the current user inside it, the very first user who logs in would be stuck there forever. Every subsequent request would be checked against *that* first user. 

**Why is `Singleton` safe for the course's handler?**
Because the handler has **no fields**. It is "stateless." The user is passed into the `HandleRequirementAsync(context, ...)` method as a parameter *every single time*. So, it doesn't matter if it's a Singleton; it just reads the parameter and forgets it. 

---

### 2. What is `[FromServices] IAuthorizationService`?

Yes! This is **Dependency Injection (DI)** in action, but on a **method parameter** instead of the constructor.

Normally, you inject services into the Controller's constructor:

```csharp
public class StudentsController : ControllerBase
{
    private readonly IAuthorizationService _auth;
    public StudentsController(IAuthorizationService auth) { _auth = auth; }
}
```

But `[FromServices]` tells ASP.NET Core: *"Don't look for this in the URL or the JSON body. Look in the DI Container (`builder.Services`) and give me the instance."*

**Is it related to `AddSingleton<IAuthorizationHandler>`?**
They are related, but they do **different** things:

- `AddSingleton<IAuthorizationHandler>` registers **your custom logic** (the handler). <mark>**You never call this directly**</mark>.
- `IAuthorizationService` is the **built-in manager** provided by ASP.NET Core. You call it (`AuthorizeAsync`), and *it* looks up your registered handler behind the scenes.

**Mind Map:**

1. You register your Handler in `Program.cs`.
2. You ask the DI container for `IAuthorizationService` using `[FromServices]`.
3. You call `AuthorizeAsync(..., "PolicyName")`.
4. `IAuthorizationService` <mark>**looks up the policy**</mark>, finds <mark>the handler **you registered**</mark>, runs it, and gives you the result.

---

### 3. Dump Q: What is the "Resource" in Resource-based authorization?

The "resource" is **the specific data or object the user is trying to access or modify**. 

In your lesson, the resource is the **`id` passed as a parameter** (e.g., `int id = 5`). 

But conceptually, it can be anything:

- A specific DB row (Student with ID 5).
- A specific file (Document_123.pdf).
- A specific comment in a blog post.

**Why the path parameter?**
Because the path parameter (`/api/students/5`) is the **identifier** that points to the actual resource. The authorization system needs that ID to ask: *"Does this user own the data behind this ID?"*

If the course were checking a blog comment, you would pass the `commentId` to the authorization service. It is not the *data itself*, it is the *identity of the data*.

---

### 4. Do we have more built-in authorization systems in ASP.NET?

Yes, ASP.NET Core has several layers of authorization built-in. You've already learned the first two, and you're touching the third:

1. **Role-Based (Coarse-grained):** `[Authorize(Roles = "Admin")]`. Checked in the middleware. Great for broad access.
2. **Policy-Based (Fine-grained):** `[Authorize(Policy = "MinimumAge18")]`. Checked in the middleware or manually. Great for rules that require logic.
3. **Resource-Based (Contextual):** `AuthorizeAsync(User, resource, "Policy")`. Checked manually inside the controller (what you are doing now). Great for ownership and specific data access.
4. **Claims-Based:** Checking the contents of the JWT manually inside a policy handler (e.g., `context.User.HasClaim("Department", "IT")`).
5. **Custom Middleware:** Writing your own `app.Use(...)` to check headers or IP addresses for extremely custom rules.
