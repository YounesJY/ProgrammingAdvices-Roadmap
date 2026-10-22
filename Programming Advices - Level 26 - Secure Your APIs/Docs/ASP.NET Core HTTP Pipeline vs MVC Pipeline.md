---

# ASP.NET Core HTTP Pipeline vs. MVC Pipeline

---

## 1. Introduction

When learning ASP.NET Core, you hear the word "pipeline" <mark>used in **two <u>different </u>contexts**</mark>:

1. The **HTTP Pipeline** (<mark>**<u>Middleware</u>**</mark>)
2. The **MVC Pipeline** (<mark>**<u>Filters</u>**</mark>)

Are they the same thing? No. Do they overlap? <mark>**Yes**</mark>. 

This MD explains the difference, how they connect, and where MVC fits into your current roadmap.

---

## 2. The Big Picture (The Factory)

Imagine your ASP.NET Core application as a factory with **two distinct security checkpoints**:

1. **<mark>The Outer Gate</mark>:** The HTTP Pipeline (Middleware). <mark>**Every single** truck (**request**)</mark> must pass through this gate. Even if the truck is just asking for a static image, it goes through here.
2. **<mark>The Inner Factory Floor</mark>:** The MVC Pipeline (Filters). <mark>**<u>Only</u>**</mark> trucks that are delivering to specific machines (Controllers) enter this area. 

---

## 3. The HTTP Pipeline (Middleware)

This is <mark>what **you have been building** in `Program.cs`</mark> since Level 25.

- **What it is:** A sequence of `app.Use...` <mark>**components**</mark> that run <mark>on ***every*** HTTP request</mark>.
- **Scope:** <mark>**Global**</mark>. It wraps <mark>**<u>the entire application</u>**</mark>.
- **Who owns it:** <mark>**Kestrel**</mark> (the web server) and the <mark>ASP.NET Core **framework**</mark>.
- **Examples:** `UseHttpsRedirection`, `UseCors`, `UseAuthentication`, `UseAuthorization`.

**When it runs:**

1. Request enters Kestrel.
2. It goes through Middleware 1, 2, 3...
3. It hits `app.MapControllers()`.
4. **This is <mark><u>where</u> the HTTP Pipeline <u>ends</u></mark> and hands over control.**

---

## 4. The MVC Pipeline (Filters)

Once a request <mark>**<u>survives</u>** the HTTP Pipeline</mark> and enters `app.MapControllers()`, ASP.NET Core <mark>**has to find the right**</mark> Controller and Action. To do this, <mark>it uses the **MVC Pipeline**</mark>.

- **What it is:** A sequence of components called **Filters** (Authorization Filters, Resource Filters, Action Filters, Exception Filters, Result Filters).
- **Scope:** <mark>**Local**</mark>. It <mark>**only**</mark> applies to requests that <mark>**<u>hit</u>** your Controllers</mark>.
- **Who owns it:** <mark>**<u>The MVC framework</u>**</mark> (the `Microsoft.AspNetCore.Mvc` assembly).
- **Examples:** `[Authorize]` (Authorization Filter), `[ResponseCache]` (Action Filter), `[ApiController]` (Convention), Model Validation.

**When it runs:**

1. `app.MapControllers()` routes the request to `StudentsController`.
2. The MVC pipeline <mark>**creates the Controller instance**</mark> (**<u>Dependency Injection</u>**).
3. It runs MVC Filters (e.g., validates the model, checks `[Authorize]`).
4. It executes your Action Method (`GetStudentById`).
5. It runs Result Filters (e.g., formats the JSON output).

---

## 5. How They Connect (The Handoff)

<mark>**The <u>handoff </u>happens**</mark> at `app.MapControllers()`.

```text
[ Client Request ]
       │
       ▼
===========================================
  HTTP PIPELINE (Middleware)
===========================================
  app.UseHttpsRedirection()
  app.UseCors()
  app.UseAuthentication()
  app.UseAuthorization()
       │
       ▼
  app.MapControllers()  <--- THE HANDOFF
       │
===========================================
  MVC PIPELINE (Filters)
===========================================
  Model Binding
  [Authorize] check (again, but at MVC level)
  Action Filter (OnActionExecuting)
       │
       ▼
  [ Your Controller Action ]
  public IActionResult Get() { ... }
```

📌 **Note:** You will see `[Authorize]` <mark>in **both pipelines**</mark>. The Middleware <mark>checks it **first**</mark> (to set `HttpContext.User`), and the MVC Filter <mark>checks it **again**</mark> just before the action runs. 

---

## 6. Why Does MVC Have Its Own Pipeline?

Because Middleware <mark>is **<u>too blunt</u>** for **fine-grained tasks**</mark>.

For example, if you want to measure exactly how long `GetStudentById()` takes to execute (excluding the time it took to verify the JWT), <mark>**you cannot**</mark> do this with Middleware. <mark>**You need**</mark> an **Action Filter** that <mark>runs **exactly before and after** the method</mark>. 

Middleware <mark>knows about ***HTTP***</mark>. <mark>Filters know about ***MVC***</mark> (the Controller, the parameters, the action method).

---

## 7. Where This Fits in Your Roadmap

Right now (Level 26), you are <mark>**heavily focused**</mark> on the **HTTP Pipeline** <mark>**<u>because security</u>**</mark> (Auth, CORS, JWT) <mark>**belongs there**</mark>. 

However, the **MVC Pipeline** <mark>is **already running**</mark> under the hood:

- Every time you use `[Authorize]`, <mark>**that's**</mark> an MVC Filter.
- Every time you return `[ProducesResponseType]`, Swagger <mark>**reads it via**</mark> the MVC pipeline.
- Every time you use `[FromBody]` to parse `LoginRequest`, <mark>that's **Model Binding**</mark> in the MVC pipeline.

**<mark>When</mark> will you use the MVC Pipeline <mark>directly</mark>?**

- When you <mark>learn about **Action Filters**</mark> (e.g., custom logging or validation).
- When you need to modify the data <mark>***after*** model binding but ***before*** the controller runs</mark>.
- When you <mark>build **a traditional MVC web app**</mark> (with HTML views).

---

## 📌 Summary

| Feature        | HTTP Pipeline (Middleware)     | MVC Pipeline (Filters)                         |
| -------------- | ------------------------------ | ---------------------------------------------- |
| **Scope**      | Global (every request)         | Local (only Controller requests)               |
| **Defined in** | `Program.cs`                   | Attributes or `AddControllers` options         |
| **Examples**   | `UseCors`, `UseAuthentication` | `[Authorize]`, `[FromBody]`, `[ResponseCache]` |
| **Runtime**    | Runs before routing            | Runs after routing (inside the Controller)     |
| **Access**     | `HttpContext`                  | `HttpContext` + Action Arguments + Model State |

**Core principle:** The HTTP Pipeline <mark>is the **outer wall**</mark> that protects and routes the request. The MVC Pipeline <mark>is the **inner machinery**</mark> that executes your Controller. They are <mark>**<u>two different layers</u>**</mark> that hand off to each other at `app.MapControllers()`.

---
