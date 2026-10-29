# The 403 Logging Placement Trap (Middleware vs. Return Journey)

---

## 1. Introduction

When implementing a global `403 Forbidden` logger, **<mark>a <u>very common</u> mistake</mark>** is **<mark>placing the middleware <u>in the wrong order</u></mark>**.

The confusion arises because we hear "Middleware **<mark>runs <u>top-down</u></mark>**, and responses **<mark>run <u>bottom-up</u></mark>**." This is true, but it leads to a false assumption: *"If I put the logger after the Authorization middleware, the return journey will bubble the 403 back through it."*

**This is false.** The return journey **<mark><u>only unwinds</u></mark>** through middleware that the request **<mark>actually entered</mark>**. If a middleware **<mark>short-circuits</mark>** the request, **<mark><u>nothing below</u> it executes</mark>** — on the way down *or* on the way up.

---

## 2. The Two Sources of a 403

To understand where to place the logger, you must know **<mark>who</mark>** is generating the `403`.

### Source A: The Authorization Middleware

- **What:** The global `app.UseAuthorization()` or `[Authorize(Roles="Admin")]` attribute.

- **When:** The request is stopped **before** the controller.

- **Where:** The HTTP Pipeline.

### Source B: The Controller (Resource-Based Policies)

- **What:** A policy handler (like `StudentOwnerOrAdminHandler`) that fails inside the controller.

- **When:** The request has already entered the controller.

- **Where:** Inside the MVC Pipeline.

---

## 3. Scenario A: 403 from the Authorization Middleware

If the request is blocked by `UseAuthorization`, it **<mark>never reaches</mark>** any middleware placed after it.

### ❌ Wrong Placement (What the course code does)

```csharp
app.UseAuthorization(); // <-- Blocks the request here
app.Use(LoggerMiddleware); // <-- Never reached!
app.MapControllers();
```

The request stops at `UseAuthorization`. The response bubbles back up to `UseAuthentication` and exits. `LoggerMiddleware` is **<mark>never executed</mark>**. The 403 **<mark><u>is not logged</u></mark>**.

### ✅ Correct Placement

```csharp
// Logger is placed BEFORE Authorization
app.Use(LoggerMiddleware);
app.UseAuthorization(); // <-- Blocks the request here
app.MapControllers();
```

**Flow:**

1. Request enters `LoggerMiddleware`. Pauses at `await next()`.

2. Request goes to `UseAuthorization`.

3. `UseAuthorization` blocks it, creates a 403.

4. The 403 travels **<mark>back up</mark>** to `LoggerMiddleware`.

5. `LoggerMiddleware` wakes up, sees the 403, and logs it.

---

## 4. Scenario B: 403 from the Controller (Resource-Based Policy)

If the request passes `UseAuthorization` but fails a policy check inside the controller (e.g., a student tries to read another student's record), the 403 is generated **<mark>inside</mark>** the MVC pipeline.

### ✅ Placement (The Course's Code)

```csharp
app.UseAuthorization();
app.Use(LoggerMiddleware); // <-- Catches controller-generated 403s
app.MapControllers();
```

**Flow:**

1. Request passes `UseAuthorization` (it has a valid token and role).

2. Request enters `LoggerMiddleware`. Pauses at `await next()`.

3. Request enters the Controller.

4. The controller's `AuthorizeAsync` fails, generates a 403.

5. The 403 travels back up to `LoggerMiddleware`.

6. `LoggerMiddleware` logs it.

---

## 5. The Golden Rule

To catch **<mark><u>all</u></mark>** 403s (both middleware and controller generated), you must place the logger **before** `UseAuthorization` in the pipeline.

To catch **<mark><u>only</u></mark>** controller-generated 403s, placing it after `UseAuthorization` is sufficient.

**<mark>For <u>production-grade</u> visibilit</mark>y, place it before `UseAuthorization`.**

---

## 📌 Summary

| Source of 403                    | Where it is generated | Logger Placement                |
| -------------------------------- | --------------------- | ------------------------------- |
| **Authorization Middleware**     | HTTP Pipeline         | ✅ **Before** `UseAuthorization` |
| **Controller (Resource Policy)** | MVC Pipeline          | ✅ After `UseAuthorization`      |
| **Both (Production-ready)**      | Both                  | ✅ **Before** `UseAuthorization` |

| Concept               | Meaning                                                                                    |
| --------------------- | ------------------------------------------------------------------------------------------ |
| **Short-circuit**     | A middleware stops the request; nothing below it runs                                      |
| **Return journey**    | Response bubbles back up only through middleware the request actually entered              |
| **Paused middleware** | Middleware above the short-circuit pauses at `await next()` and resumes on the way back up |

**Core principle:** **<mark>a <u>short-circuit</u></mark>** means the request **<mark>never enters</mark>** the middleware **<mark><u>below it</u></mark>**. The return journey does not "skip ahead" — it unwinds through the same stack the request traveled. Place the logger **<mark><u>before</u></mark>** the short-circuit point to observe what the short-circuit produced.

---


