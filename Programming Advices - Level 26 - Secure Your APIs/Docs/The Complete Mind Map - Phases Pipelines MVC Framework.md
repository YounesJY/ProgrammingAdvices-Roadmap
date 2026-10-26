# 📖 The Complete Mind Map: Phases, Pipelines & MVC Framework

---

## 1. Introduction

When learning ASP.NET Core, **<mark>four concepts</mark>** are constantly mixed up:

- 🔹 The **Build Phase**
- 🔹 The **HTTP Pipeline**
- 🔹 The **MVC Framework**
- 🔹 The **MVC Pipeline**

They sound similar. They **<mark>are not</mark>**. This MD separates **<mark>when things happen</mark>** (phases) from **<mark>what happens</mark>** (pipelines and frameworks), and lists every middleware and filter in the correct order.

---

## 2. The Phases vs. The Pipelines

First, separate **<mark>time</mark>** (Phases) from **<mark>execution flow</mark>** (Pipelines).

### 2.1 The Phases (Time)

| Phase              | What Happens                                  | Where                                     |
| ------------------ | --------------------------------------------- | ----------------------------------------- |
| **Build Phase**    | You fill the DI container with tools          | `Program.cs` **before** `builder.Build()` |
| **Pipeline Phase** | You define the road that HTTP requests travel | `Program.cs` **after** `builder.Build()`  |

- **Build Phase:** The app is **<mark>being assembled</mark>**. It is **<mark>not running <u>yet</u></mark>**.
- **Pipeline Phase:** The app **<mark><u>is running</u></mark>**. It is now waiting for traffic.

### 2.2 The Pipelines (Execution Flow)

| Pipeline          | Owner                                     | Scope                                                                                      |
| ----------------- | ----------------------------------------- | ------------------------------------------------------------------------------------------ |
| **HTTP Pipeline** | **<mark>Kestrel</mark>** (the web server) | All traffic (even 404s, static files)                                                      |
| **MVC Pipeline**  | **<mark>MVC Framework</mark>**            | **<mark><u>Only</u></mark>** requests that reach `MapControllers()` and match a Controller |

📌 **HTTP Pipeline <mark>is <u>the outer road</u></mark>. MVC Pipeline <mark>is <u>the inner road</u></mark>.**

---

## 3. Why Is It Called "MVC Framework" and Not "MVC Pipeline"?

- **MVC Framework** = a **<mark>large collection</mark>** of tools (Routing, Model Binding, Validation, Serialization, Filters).
- **MVC Pipeline** = the *<mark>execution flow</mark>* **<mark><u>inside</u></mark>** the framework (the Filter Pipeline).

ASP.NET Core does not force you to use the MVC framework. You could use **Minimal APIs** (`app.MapGet(...)`), which skip the MVC framework entirely.

📌 **"MVC" <mark>is <u>the framework</u></mark>. The "pipeline" <mark>is its <u>internal filter flow</u></mark>.**

---

## 4. The HTTP Pipeline Order (Your Current Setup)

Here is the exact order in your `Program.cs`. The return journey happens in reverse.

1. **Kestrel** — Receives the raw TCP request.
2. **Swagger UI** (`if (app.Environment.IsDevelopment())`) — Bypasses the rest if you are only viewing docs.
3. **`app.UseHttpsRedirection()`** — Redirects HTTP to HTTPS if needed.
4. **`app.UseCors()`** — Checks the browser's `Origin`. Blocks disallowed origins.
5. **`app.UseRateLimiter()`** — Checks the IP bucket. Returns `429` if exceeded.
6. **Custom message middleware** — Runs on **<mark>the return journey</mark>** to add the friendly `429` message.
7. **`app.UseAuthentication()`** — Reads the JWT and sets `HttpContext.User`.
8. **`app.UseAuthorization()`** — Enforces `[Authorize]`, roles, and policies.
9. **`app.MapControllers()`** — **<mark>The handoff</mark>** to the MVC Framework.

📌 **Order matters because <mark>each middleware <u>wraps</u></mark> the next one.**

---

## 5. The MVC Pipeline Order (Inside the Framework)

Once the request **<mark>reaches</mark>** `app.MapControllers()`, it enters the **<mark>MVC Filter Pipeline</mark>**.

1. **Routing & Endpoint Selection** — Finds the matching Controller and Action.
2. **Model Binding** — Reads the JSON body and converts it to `LoginRequest`.
3. **Authorization Filters** — **<mark>Secondary</mark>** `[Authorize]` **<mark>check</mark>** at the MVC level.
4. **Resource Filters** — Runs before the action (caching, resource checks).
5. **Action Filters (`OnActionExecuting`)** — Runs before the method body.
6. **The Action Method** — Your actual code (`return Ok(...)`).
7. **Action Filters (`OnActionExecuted`)** — Runs after the method body.
8. **Exception Filters** — Catches exceptions from the action.
9. **Result Filters** — Runs before the response is converted to JSON.
10. **Result Execution** — Serializes the `IActionResult` into a JSON HTTP response.

📌 After step 10, the response travels **<mark>back up</mark>** through the HTTP Pipeline in reverse.

---

## 6. The Full Journey (One Diagram)

```text
CLIENT
   │
   ▼
┌────────────────────────────────────────────┐
│           Kestrel (Web Server)             │
└────────────────────────────────────────────┘
   │
   ▼
┌────────────────────────────────────────────┐
│            HTTP PIPELINE                   │
│  (app.Use... — defined after Build())      │
│                                            │
│  1. UseHttpsRedirection                    │
│  2. UseCors                                │
│  3. UseRateLimiter                         │
│  4. Custom 429 message (return journey)    │
│  5. UseAuthentication                      │
│  6. UseAuthorization                       │
└────────────────────────────────────────────┘
   │
   ▼  app.MapControllers()  ──── THE HANDOFF
   │
┌────────────────────────────────────────────┐
│            MVC PIPELINE                    │
│  (inside the MVC Framework)                │
│                                            │
│  1. Routing & Endpoint Selection           │
│  2. Model Binding                          │
│  3. Authorization Filters                  │
│  4. Resource Filters                       │
│  5. Action Filters (OnExecuting)           │
│  6. ACTION METHOD (your C# code)           │
│  7. Action Filters (OnExecuted)            │
│  8. Exception Filters                      │
│  9. Result Filters                         │
│ 10. Result Execution (JSON output)         │
└────────────────────────────────────────────┘
   │
   ▼
CLIENT (response arrives back up)
```

---

## 7. The Fundamental Difference: Middleware vs. Filters

| Aspect                 | Middleware                                       | Filters                                                     |
| ---------------------- | ------------------------------------------------ | ----------------------------------------------------------- |
| **<mark>Owner</mark>** | Kestrel / ASP.NET Core                           | MVC Framework                                               |
| **Scope**              | Global (all requests)                            | Only MVC requests                                           |
| **Knows about**        | <mark>HTTP</mark> (headers, IP, path)            | <mark>MVC</mark> (parameters, method, result)               |
| **Can access**         | `HttpContext`                                    | <mark>`HttpContext` + Action arguments + Model State</mark> |
| **Examples**           | `UseCors`, `UseAuthentication`, `UseRateLimiter` | `[Authorize]`, `[ResponseCache]`, custom action filters     |

📌 **Middleware <mark>is <u>coarse</u></mark>. Filters <mark>are <u>fine</u></mark>.**

---

## 8. Characteristics

- 🧬 **Build Phase** = setup (before `Build()`)
- 🧬 **Pipeline Phase** = running (after `Build()`)
- 🧬 **HTTP Pipeline** = all requests
- 🧬 **MVC Pipeline** = controller requests only
- 🧬 **MVC Framework** = the toolset; **MVC Pipeline** = its internal flow
- 🧬 Middleware is coarse; filters are fine
- 🧬 Both pipelines follow a strict order

---

## 9. Interconnection

- 🔗 Build Phase → prepares the DI container
- 🔗 Pipeline Phase → defines the request journey
- 🔗 HTTP Pipeline → decides **if** a request reaches the MVC layer
- 🔗 MVC Pipeline → decides **how** the request is executed
- 🔗 `MapControllers()` → the exact handoff point
- 🔗 Return journey → the response travels back up the same paths

### 🛠️ Summary of Interconnections

- 🔹 Build Phase is the **what**
- 🔹 Pipeline Phase is the **how**
- 🔹 HTTP Pipeline is the **outer wall**
- 🔹 MVC Pipeline is the **inner machinery**
- 🔹 They hand off at `MapControllers()`
- 🔹 Both wrap the request on both sides

---

## 📌 Summary

| Concept            | What It Is                   | Where It Lives                  | When It Runs                 |
| ------------------ | ---------------------------- | ------------------------------- | ---------------------------- |
| **Build Phase**    | Assembling the app           | `Program.cs` (before `Build()`) | Once at startup              |
| **Pipeline Phase** | Defining the request journey | `Program.cs` (after `Build()`)  | Once at startup (definition) |
| **HTTP Pipeline**  | The outer road               | `app.Use...`                    | Every request                |
| **MVC Framework**  | The toolset                  | Inside `MapControllers()`       | Every controller request     |
| **MVC Pipeline**   | The filter flow              | Inside the MVC Framework        | Every controller request     |

---

**Core principle:** the **Build Phase** **<mark>assembles</mark>** the app. The **Pipeline Phase** **<mark>defines the road</mark>**. The **HTTP Pipeline** **<mark>decides</mark>** *if* a request should reach the MVC layer. The **MVC Framework** **<mark>provides the tools</mark>** to execute the request. The **MVC Pipeline** **<mark>is the internal flow</mark>** that runs your controller. **<mark><u>The handoff</u></mark>** between the two pipelines is `app.MapControllers()`. Everything before it is *global*. Everything after it is *scoped to the controller*.

---
