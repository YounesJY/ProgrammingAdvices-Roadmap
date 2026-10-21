# What is Middleware?

---

## 1. What is Middleware in simple terms?

Middleware is:

> A <mark>**piece of code**</mark> that sits in the middle of the road <mark>**<u>between</u>**</mark> the incoming Request from the client and the Controller / Endpoint in the API.

This means any request that enters the API:

- Passes through <mark>**<u>a set</u>**</mark> of Middleware <mark>**<u>in order</u>**</mark>
- Then reaches the Controller
- Then <mark>**<u>returns through</u>**</mark> the exact same path in reverse

---

## 2. The real path of any Request

A request does **not** go directly to the Controller.

The correct path is:

```
Request → Middleware 1 → Middleware 2 → Middleware 3 → Controller
       → Middleware 3 → Middleware 2 → Middleware 1 → Response
```

The Controller <mark>is **part of the path**</mark>, but Middleware <mark>is **the guard and organizer**</mark> of the entire road.

---

## 3. What does Middleware do?

Middleware <mark>is responsible for common, **<u>shared concerns</u>**</mark> such as:

- 🔹 Verifying JWT
- 🔹 Ensuring the user is authenticated (Authentication)
- 🔹 Checking permissions (Authorization)
- 🔹 HTTPS redirection
- 🔹 CORS
- 🔹 Logging
- 🔹 Rate Limiting
- 🔹 Exception handling

Note something very important:

> The Controller<mark> does **not**</mark> verify the token itself. The Middleware <mark>does that **before**</mark> the request reaches the Controller.

---

## 4. Example from the course (very important)

When we wrote:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

> This is **not** a Controller. <mark>This is **<u>Middleware</u>**</mark>.

It means:

- 🔹 Any incoming Request
- 🔹 Before it reaches any Endpoint
- 🔹 Gets checked:
  - Is there a Token?
  - Is it valid?
  - Has it expired?
  - Is the role allowed?

> If any check fails, the request <mark>is **rejected <u>before</u> the Controller <u>ever sees it</u>**</mark>.

---

## 📌 Summary

| Concept                | Meaning                                                                         |
| ---------------------- | ------------------------------------------------------------------------------- |
| **Middleware**         | Code that sits between the Request and the Controller                           |
| **Path**               | Request → Middleware chain → Controller → Middleware chain (reverse) → Response |
| **Controller's role**  | Business logic only                                                             |
| **Middleware's role**  | Cross-cutting concerns (auth, CORS, logging, etc.)                              |
| **When checks happen** | Before the Controller is reached                                                |
| **If a check fails**   | Request is rejected; Controller is never executed                               |

**Core principle:** the Controller <mark>**<u>only</u>**</mark> runs business logic. <mark>**Everything**</mark> that needs to happen <mark>***before* or *after***</mark> every request — authentication, authorization, logging, CORS — <mark>**<u>lives in Middleware</u>**</mark>, which wraps the Controller <mark>on **both sides** of **<u>the pipeline</u>**</mark>.

---


