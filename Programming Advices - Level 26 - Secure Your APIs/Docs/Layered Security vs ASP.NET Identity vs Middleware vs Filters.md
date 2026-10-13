# Layered Security vs ASP.NET Identity vs Middleware vs Filters

---

## 1. The 7 Layers (Course Roadmap)

**What they are:** <mark>a *conceptual progression*</mark> — <mark>a teaching order **for adding security concerns to an API**</mark>. <mark>**Not a single framework feature**</mark>.

**Where they live:** spread across your codebase — <mark>**some are middleware, some are attributes, some are services, some are config**</mark>.

> **<mark><u>Purpose</u></mark>:** answer "<mark>**in what order should I think about API security?**</mark>"

---

## 2. ASP.NET Core Identity

**What it is:** <mark>**a full membership system shipped by Microsoft**</mark>. User store, password hashing (PBKDF2), roles, claims, lockout, 2FA, email confirmation, external logins (Google, Facebook), token providers.

**Where it lives:** a set of NuGet packages + EF Core tables (`AspNetUsers`, `AspNetRoles`, `AspNetUserClaims`, …) wired into DI via `AddIdentity<TUser, TRole>()`.

> **<mark>Purpose:</mark>** answer "<mark>**who are my users, and how do I store and manage them?**</mark>"

**Why this course skips it:** <mark>**<u>Identity hides the mechanics</u>**</mark> (hashing, token issuance, claim building) **<u>behind a framework</u>**. This course implements JWT, roles, and ownership by hand first — <mark>**<u>so you understand what Identity is doing later</u>**</mark>.

|                  | Course's 7 layers                | ASP.NET Identity               |
| ---------------- | -------------------------------- | ------------------------------ |
| Scope            | Concepts + manual implementation | Full user-management framework |
| User storage     | In-memory / your own tables      | EF Core tables out of the box  |
| Password hashing | You implement                    | Built-in (PBKDF2)              |
| JWT              | You issue it manually            | Optional, via separate package |
| Roles / claims   | You define                       | Built-in + EF-backed           |

**Identity overlaps with Layers 2, 3, and partially 4 — but <mark>you can implement all three without ever touching Identity.</mark>**

---

## 3. Middleware

**What it is:** <mark>**a component in the ASP.NET Core request pipeline**</mark>. **<u>Runs on *every* request</u>**, in registration order, and decides whether to pass the request to the next component or short-circuit it.

**Where it lives:** `Program.cs` — `app.UseAuthentication()`, `app.UseAuthorization()`, `app.UseHttpsRedirection()`, `app.UseCors()`, custom `app.Use(...)`.

> **<mark>Purpose:</mark>** <mark>**cross-cutting concerns at the *pipeline* level**</mark>.

**Examples relevant to this course:**

- HTTPS redirection → Layer 1
- CORS → Layer 1
- Authentication (validates JWT, sets `HttpContext.User`) → Layer 2
- Authorization (enforces policies) → Layers 3–4
- Rate limiting → Layer 6
- Logging → Layer 7

**So: <mark>most of the 7 layers are *implemented as* middleware. Middleware <u>is the mechanism</u>; the layers <u>are the concerns</u></mark>.**

---

## 4. Filters

**What they are:** <mark>**a finer-grained extension point**</mark> that <mark>**runs *inside* MVC / the endpoint, after routing and after the authorization middleware**</mark>. They have access to MVC context (action arguments, model state, the action result).

**Where they live:** <mark>as attributes on controllers/actions</mark> (`[Authorize]`, `[ServiceFilter]`, custom `IActionFilter`) or registered globally via `options.Filters.Add(...)`.

**Types (in order of execution):**

1. Authorization filters — run first (`[Authorize]` is one)
2. Resource filters
3. Action filters — before/after the action method
4. Exception filters
5. Result filters

> **<mark>Purpose:</mark>** <mark>**concerns <u>scoped to *a specific action or controller*</u>, not the whole pipeline**</mark>.

**Examples relevant to this course:**

- `[Authorize]`, `[Authorize(Roles="Admin")]`, `[Authorize(Policy="OwnerOnly")]` — Layers 3–4
- `[AllowAnonymous]` — Layer 2
- Custom filters for auditing a single action — Layer 7

---

## 5. How the Four Relate

| Concept          | Level                  | Answers                                        |
| ---------------- | ---------------------- | ---------------------------------------------- |
| 7 Layers         | Conceptual roadmap     | <mark>In what order do I add security</mark>?  |
| ASP.NET Identity | Framework / library    | <mark>How do I store and manage users</mark>?  |
| Middleware       | Pipeline component     | <mark>How do I intercept every request</mark>? |
| Filters          | MVC/endpoint component | <mark>How do I intercept this action</mark>?   |

**Execution order (simplified):**

```js
Request
  ↓
Middleware pipeline (HTTPS, CORS, Auth, Rate limit, Logging)
  ↓
Routing
  ↓
Authorization middleware / filters ([Authorize], policies)
  ↓
MVC filters (Resource, Action, Exception, Result)
  ↓
Controller action
  ↓
Response
```

- **Middleware** wraps everything — first in, last out.
- **Authorization filters** run early, but *inside* MVC, after the authorization middleware.
- **Layers 1, 6, 7** live mostly in middleware.
- **Layers 2** spans both — the JWT validation is middleware; the `[Authorize]` attribute is a filter.
- **Layers 3–4** are enforced mostly through filters (attributes) backed by services (`IAuthorizationHandler`).
- **Layer 5** (refresh) is mostly your own endpoints + services, not middleware or filters.

---

## 6. Where Identity Sits

If you *were* using Identity, it would slot in as:

- **Middleware:** `app.UseAuthentication()` uses Identity's cookie or JWT handler.
- **Services:** `UserManager<T>`, `SignInManager<T>` used inside your login/register controllers.
- **Filters:** `[Authorize]` still enforces access — Identity just provides the identity behind it.

<mark>Identity is **not** an alternative to middleware or filters — **it is a *service layer* that those mechanisms consume**</mark>.

---

## 📌 Summary

| Question                                  | Answer                                                                       |
| ----------------------------------------- | ---------------------------------------------------------------------------- |
| What are the 7 layers?                    | <mark>A teaching roadmap</mark> of security concerns                         |
| What is Identity?                         | <mark>Microsoft's user-management framework</mark> (storage + auth services) |
| What is middleware?                       | <mark>Pipeline component</mark> that runs on every request                   |
| What are filters?                         | <mark>MVC-level component</mark> scoped to actions/controllers               |
| Which implements the 7 layers?            | Middleware + filters + custom services + config                              |
| Does this course use Identity?            | No — <mark>**it reimplements the core pieces by hand**</mark>                |
| Does Identity replace middleware/filters? | No — <mark>**it feeds them**</mark>                                          |
| Where do `[Authorize]` and policies run?  | As filters, inside MVC, after auth middleware                                |

📌 **One-line version:** <mark>the 7 layers **are the *what and why***; middleware and filters **are the *where***</mark>; Identity is an optional *framework* that sits underneath — <mark>**<u>skipped here on purpose</u>** **so the mechanics stay visible**</mark>.

---

---

# Right Order to Learn These

---

## 1. The Order

| #   | Topic                                | Why here                                                                                                                            |
| --- | ------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------- |
| 1   | **The 7 Layers (concepts)**          | <mark>**Frames the problem**</mark>. <mark>**WHAT/WHY before HOW**</mark>.                                                          |
| 2   | **Middleware**                       | <mark>**The pipeline is where most security runs**</mark>. Learn it before anything that plugs into it.                             |
| 3   | **Filters**                          | <mark>**Runs inside MVC**</mark>. Learn after middleware <mark>**so you know the boundary** between the two</mark>.                 |
| 4   | **The 7 layers implemented by hand** | JWT, roles, ownership, refresh, rate limiting, logging, HTTPS/CORS — using middleware, filters, and services you now understand.    |
| 5   | **ASP.NET Identity**                 | <mark>**<u>Only after you've built auth by hand</u>**</mark>. Then you'll see it as a framework that packages what you already did. |

---

## 2. Why This Order

### 2.1 Concepts before mechanisms

Layers 1–7 answer "<mark>**what problem am I solving?**</mark>" Middleware and filters answer "<mark>**where does the solution run?**</mark>" Learning middleware first without a threat model <mark>**gives you a pipeline with no purpose**</mark>. Learning the layers first <mark>**gives middleware a job to do**</mark>.

### 2.2 Middleware before filters

Most security in this course is middleware:

- HTTPS redirect — middleware
- CORS — middleware
- JWT validation (`UseAuthentication`) — middleware
- Rate limiting — middleware
- Logging — middleware

Filters come *after* authorization middleware in the pipeline and <mark>**cover a narrower scope**</mark> (a specific action). If you learn filters first, <mark>**you'll misplace logic that belongs in the pipeline**</mark>.

### 2.3 Hand-rolled before Identity

ASP.NET Identity is a black box that hides:

- Password hashing (PBKDF2, iterations, salt)
- User store (EF Core tables)
- Claims building
- Token providers
- Lockout, 2FA, external logins

If you learn Identity first, you can ship a login but cannot explain why the password is safe, what the token contains, or how to debug a 401. If you build auth by hand first, <mark>Identity becomes readable</mark> — <mark>**you'll see exactly which parts it automates**</mark>.

### 2.4 Identity is opt-in, not foundational

You can build a production API without Identity (this course proves it). You cannot build one without middleware. Learn the mandatory layer first; the optional framework last.

---

## 3. What Each Step Unlocks

| After learning…      | You can…                                                                                             |
| -------------------- | ---------------------------------------------------------------------------------------------------- |
| 7 layers             | <mark>Explain ***what* security** this API **needs and *why***</mark>                                |
| Middleware           | <mark>**Trace any request** through the pipeline</mark>; add or reorder security globally            |
| Filters              | <mark>**Scope access rules**</mark> to specific controllers/actions                                  |
| Hand-rolled 7 layers | Build, debug, and defend JWT + roles + ownership + refresh + rate limiting + logging end to end      |
| ASP.NET Identity     | <mark>**Recognize what Identity automates**</mark>, and choose it deliberately instead of by default |

---

## 4. What This Course Covers vs Skips

| Topic                                                  | Covered in Level 26?      | Position in learning order |
| ------------------------------------------------------ | ------------------------- | -------------------------- |
| 7 layers                                               | ✅                         | Step 1 (conceptual)        |
| Middleware                                             | ✅ Implicitly              | Step 2                     |
| Filters (`[Authorize]`, policies)                      | ✅                         | Step 3                     |
| JWT, roles, ownership, refresh, rate limiting, logging | ✅                         | Step 4                     |
| ASP.NET Identity                                       | ❌ Explicitly out of scope | Step 5 (self-study after)  |
| OAuth / OpenID Connect                                 | ❌ Out of scope            | Step 6 (later)             |

The course stops before Identity <mark>**<u>on purpose</u>**</mark>. <mark>**<u>Everything before it is the prerequisite</u>**</mark>.

---

## 📌 Summary

> **Layers (concepts) → Middleware → Filters → Hand-rolled security → Identity.**

| Order | Learn                                                               | Then you know                           |
| ----- | ------------------------------------------------------------------- | --------------------------------------- |
| 1     | The 7 layers                                                        | WHAT and WHY                            |
| 2     | Middleware                                                          | WHERE (global)                          |
| 3     | Filters                                                             | WHERE (scoped)                          |
| 4     | Hand-rolled JWT / roles / policies / refresh / rate limit / logging | HOW — and why                           |
| 5     | ASP.NET Identity                                                    | What the framework was hiding all along |

📌 **Rule of thumb:** <mark>**<u>never learn a framework before you've solved the problem it solves</u>**</mark>. Otherwise <mark>**you inherit its decisions <u>without understanding them</u>**</mark>.

---

# ASP.NET as an SDK ?

ASP.NET Core is a **framework**, delivered as a set of NuGet packages + a runtime + tooling (dotnet CLI, templates). You could loosely call it an SDK in the everyday sense — it ships under the .NET SDK — but the precise term is **web framework**.

Quick breakdown:

| Term                  | What it means                                                                            |
| --------------------- | ---------------------------------------------------------------------------------------- |
| .NET SDK              | <mark>**The toolchain**</mark>: compiler, CLI, templates, runtime. You install this.     |
| ASP.NET Core          | <mark>**The web framework inside that SDK**</mark> — middleware, routing, MVC, auth, DI. |
| ASP.NET Core Identity | <mark>**A library**</mark> on top of ASP.NET Core for user management.                   |
| NuGet package         | <mark>**<u>How the pieces ship</u>**</mark> (`Microsoft.AspNetCore.*`).                  |

> So: <mark>**SDK = <u>toolchain</u>. ASP.NET Core = <u>framework</u>. Identity = <u>library</u>.**</mark>

---
