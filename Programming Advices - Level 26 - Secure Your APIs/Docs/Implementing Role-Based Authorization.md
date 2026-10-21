# Implementing Role-Based Authorization in the Student API

---

## 1. Introduction

In the previous lesson, you clearly understood what authorization is and why authentication alone is dangerous.

Now we move from understanding to implementation.

Up to this point:

- ✅ The API knows who is calling it (JWT authentication)
- ❌ The API does not yet restrict actions based on role

In this lesson, we will teach the API <mark>what each role **is allowed to do**</mark>, using ASP.NET Core's built<mark>**-in role-based authorization**</mark>.

📌 We will **not** invent rules randomly.
We will strictly <mark>follow a **clear authorization boundary table**</mark> that will guide all future lessons.

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will:

- Apply <mark>**role-based authorization**</mark> correctly
- Enforce <mark>**Admin-only**</mark> endpoints
- Allow <mark>**safe public access**</mark> where appropriate
- Understand <mark>**why**</mark> some endpoints stay public
- Prepare the API for <mark>**ownership & policy-based rules**</mark>

---

## 3. 🔐 Authorization Boundary Table (Single Source of Truth)

This table defines who can access what in the Student API.

| Endpoint                         | Access Rule                    |
| -------------------------------- | ------------------------------ |
| `GET /api/Students/Passed`       | 🌐 Public                      |
| `GET /api/Students/AverageGrade` | 🌐 Public                      |
| `GET /api/Students/{id}`         | 🪪 Student (owner) or 👑 Admin |
| `GET /api/Students/All`          | 👑 Admin only                  |
| `POST /api/Students`             | 👑 Admin only                  |
| `PUT /api/Students/{id}`         | 👑 Admin only                  |
| `DELETE /api/Students/{id}`      | 👑 Admin only                  |

📌 This table is **not optional**.
It defines the security contract of the API.

---

## 4. 🔹 Step 1 — Identify Public Endpoints

Some endpoints <mark>**do not require**</mark> authentication.

**Why?**

- They expose aggregated or non-sensitive data
- They do not reveal individual student identities
- They are <mark>**safe** to consume **publicly**</mark>

**Public Endpoints:**

- `GET /api/Students/Passed`
- `GET /api/Students/AverageGrade`

Because you have `[Authorize]` at the controller level, these endpoints <mark>must **override** it</mark> with `[AllowAnonymous]` (boundary table says: Public).

### ✅ `GET /api/Students/Passed` → Public

```csharp
[AllowAnonymous]
[HttpGet("Passed", Name = "GetPassedStudents")]
public ActionResult<IEnumerable<Student>> GetPassedStudents()
{
    // ...
}
```

### ✅ `GET /api/Students/AverageGrade` → Public

```csharp
[AllowAnonymous]
[HttpGet("AverageGrade", Name = "GetAverageGrade")]
public ActionResult<double> GetAverageGrade()
{
    // ...
}
```

📌 Being explicit <mark>**prevents future mistakes**</mark> when global authorization rules are added.

---

## 5. 🔹 Step 2 — Secure the Controller by Default

All non-public endpoints <mark>**should require**</mark> authentication.

### Best Practice

Protect <mark>**<u>the controller itself</u>**</mark> (we already did this before):

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    // ...
}
```

📌 This ensures:

- Anonymous users <mark>are blocked **by default**</mark>
- Security <mark>**is centralized**</mark>
- <mark>**No endpoint**</mark> is accidentally left open

---

## 6. 🔹 Step 3 — Restrict Admin-Only Endpoints

Based on the boundary table, the following endpoints are **Admin-only**:

- `GET /api/Students/All`
- `POST /api/Students`
- `PUT /api/Students/{id}`
- `DELETE /api/Students/{id}`

### ✅ `GET /api/Students/All` → Admin

```csharp
[Authorize(Roles = "Admin")]
[HttpGet("All", Name = "GetAllStudents")]
public ActionResult<IEnumerable<Student>> GetAllStudents()
{
    // ...
}
```

### ✅ `POST /api/Students` → Admin

```csharp
[Authorize(Roles = "Admin")]
[HttpPost(Name = "AddStudent")]
public ActionResult<Student> AddStudent(Student newStudent)
{
    // ...
}
```

### ✅ `PUT /api/Students/{id}` → Admin

```csharp
[Authorize(Roles = "Admin")]
[HttpPut("{id}", Name = "UpdateStudent")]
public ActionResult<Student> UpdateStudent(int id, Student updatedStudent)
{
    // ...
}
```

### ✅ `DELETE /api/Students/{id}` → Admin

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("{id}", Name = "DeleteStudent")]
public ActionResult DeleteStudent(int id)
{
    // ...
}
```

📌 ASP.NET Core will automatically:

- Extract the role <mark>**from the JWT**</mark>
- Compare it to the required role
- Return `403 Forbidden` if the role does not match

---

## 7. 🔹 Step 4 — Understanding `401` vs `403` (Critical)

When authorization is active, two errors matter:

### ❌ 401 Unauthorized

- No token
- Invalid token
- Authentication failed

### ❌ 403 Forbidden

- Token is valid
- User is authenticated
- User does **not** have the required role

> 📌 `403` is a **security success**, <mark>**not a bug**</mark>.

---

## 8. 🔹 Step 5 — Endpoints That Need Ownership (Not Yet)

The endpoint below <mark>**cannot** be secured by role **<u>alone</u>**</mark>:

```http
GET /api/Students/{id}
```

**Why?**

- A student should see <mark>only **<u>their own</u>** record</mark>
- An admin should see <mark>**<u>any</u>** student</mark>

📌 Role-based authorization alone <mark>is **not enough** here</mark>.

> We **<mark>intentionally do not</mark> implement this now**.

This endpoint will be handled in the <mark>**Ownership & Policy-Based Authorization**</mark> lesson.

---

## 9. 🧠 Why We Do It in This Order

Security is built in layers:

1. Secure transport (HTTPS + CORS)
2. Authentication (JWT)
3. Authorization (Roles) ← **you are here**
4. Ownership rules
5. Policies & production hardening

Skipping steps leads to fragile systems.

---

## 10. 🧬 Characteristics of Role-Based Authorization

- Declarative (attributes, not `if` statements)
- Centralized
- Enforced automatically
- Based on identity claims
- Easy to audit and reason about

---

## 11. 🔗 Interconnection

- 🔗 Login → issues JWT
- 🔗 JWT → carries role claim
- 🔗 Middleware → validates identity
- 🔗 Authorization → checks role
- 🔗 Controller → allows or blocks action

### 🛠️ Final Summary of Interconnections

- 🔹 Authentication without authorization is dangerous
- 🔹 Roles convert identity into controlled access
- 🔹 Admin-only endpoints protect system integrity
- 🔹 Public endpoints must be **intentionally** public
- 🔹 Ownership rules require the next layer

---

## 12. 🏁 Conclusion

You have now:

- Implemented real role-based authorization
- Enforced Admin-only operations
- Protected sensitive endpoints
- Followed a clear security boundary
- Prepared the API for ownership rules

Your API has officially moved from:

**Authenticated API → Authorized API**

▶️ **Next Lesson:** We will move to **Module 6 — Ownership Rules (Policies)**

### 🧭 Ownership-Based Authorization — Is This Your Data?

In the next lesson, you will learn:

- Why roles are not enough
- How to restrict students to their own records
- How to prevent horizontal privilege escalation
- How to implement ownership checks correctly

This is where real-world security begins.

---

## 📌 Summary

| Step | Action                                                               | Result                                      |
| ---- | -------------------------------------------------------------------- | ------------------------------------------- |
| 1    | Add `[AllowAnonymous]` to `Passed` and `AverageGrade`                | Public access preserved                     |
| 2    | Keep `[Authorize]` at controller level                               | All other endpoints require auth by default |
| 3    | Add `[Authorize(Roles = "Admin")]` to `All`, `POST`, `PUT`, `DELETE` | Admin-only enforcement                      |
| 4    | Leave `GET /api/Students/{id}` unchanged                             | Ownership will be added next module         |

| Status Code        | Meaning                 | Message                                         |
| ------------------ | ----------------------- | ----------------------------------------------- |
| `401 Unauthorized` | No/invalid token        | "I don't know who you are."                     |
| `403 Forbidden`    | Valid token, wrong role | "I know who you are — but you are not allowed." |

**Core principle:** role-based authorization <mark>turns identity into **permission**</mark>. `[Authorize(Roles = "Admin")]` is <mark>the entire **enforcement mechanism**</mark> — no `if` statements, no manual claim parsing, no JWT reading. The framework does it all, as long as the role claim is present in the token (which you already emit in `AuthController`).

---
