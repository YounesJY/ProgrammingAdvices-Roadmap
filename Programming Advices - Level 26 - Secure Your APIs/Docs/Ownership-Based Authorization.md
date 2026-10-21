# Ownership-Based Authorization — Is This Your Data?

---

## 1. Introduction

So far, your API has reached an important milestone:

- ✅ It knows **who** is calling it (Authentication)
- ✅ It knows **what role** the user has (Authorization)

But there is still a critical real-world security problem that roles alone cannot solve.

Right now, a logged-in student can do this:

```js
GET /api/Students/1
GET /api/Students/2
GET /api/Students/999
```

And the API <mark>**has no idea**</mark> whether <mark>this data **belongs to them or not**</mark>.

This lesson fixes that problem.

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will understand:

- Why roles are not enough
- What ownership-based authorization really means
- What horizontal privilege escalation is
- Why this is one of the most common real-world vulnerabilities
- How ASP.NET Core is designed to support ownership checks
- Where ownership fits in the security roadmap

> 📌 This lesson <mark>focuses on **<u>thinking correctly</u>** **before** coding</mark>.

---

## 3. 🔹 Why Roles Alone Are Not Enough

Roles answer this question:

> 🛂 What can this user do **in general**?

**Example:**

- Admin → can manage students
- Student → limited access

But roles do **not** answer this question:

> 🧭 Is **this specific data** owned by this user?

A student with role `"Student"` should **NOT** automatically be allowed to:

- Read other students' profiles
- View other students' grades
- Modify records that are not theirs

Yet many systems stop at roles — and that is dangerous.

---

## 4. 🔹 The Real-World Problem: Horizontal Privilege Escalation

This type of vulnerability is called:

**<mark>Horizontal Privilege Escalation</mark>**

It means:

- The attacker does **not** become admin
- The attacker **stays** in the same role
- But accesses data belonging to other users

**Example:**

- Student with `Id = 3` logs in
- Changes the URL from `/api/Students/3` to `/api/Students/4`
- API returns another student's data

> 📌 This <mark>**<u>is one of the most</u>**</mark> common API security failures.

---

## 5. 🔹 Real-Life Analogy

Think of a university portal:

- All students are "Students"
- But each student can only see **their own** grades
- Being a student does **not** mean: *"You can view everyone's transcript"*

The rule <mark>**is not just**</mark>:

> "You are a student."

The rule is:

> "You are **this** student."

> <mark>**<u>That difference</u>** is **ownership**</mark>.

---

## 6. 🔹 What Is Ownership-Based Authorization?

Ownership-based authorization means:

- The API checks **who the data belongs to**
- The API compares:
  - The authenticated user identity
  - With the resource owner
- Access is granted only if:
  - The user <mark>**owns**</mark> the data
  - <mark>**OR**</mark> the user has a higher privilege (Admin)

> 📌 <mark>Ownership is **contextual**</mark>, not global.

---

## 7. 🔹 Why This Is Harder Than Roles

**Roles are static:**

- Stored in JWT
- Checked with attributes

**Ownership is dynamic:**

- Depends on request data
- Depends on database values
- Must be evaluated at **runtime**

> 📌 This is why ownership checks <mark>**are often skipped**</mark> — and why <mark>systems **<u>get breached</u>**</mark>.

---

## 8. 🔹 How Ownership Will Work in Your Student API

In your API:

- Each student record has an `Id`
- Each JWT contains the authenticated student's `Id`
- The API must enforce this rule:

> A student can only access a record if
> `JWT.StudentId == Route.StudentId`

📌 **Admins are exempt from this rule.**

This logic **cannot** be expressed with roles alone.

---

## 9. 🔹 Ownership vs Role — Clear Comparison

| Aspect                 | Role-Based                     | Ownership-Based            |
| ---------------------- | ------------------------------ | -------------------------- |
| Question asked         | "What type of user are you?"   | "Do you own this record?"  |
| Source of truth        | JWT claims (`role`)            | JWT claims + resource data |
| Evaluated              | At authorization time (static) | At runtime (contextual)    |
| Prevents               | Vertical escalation            | Horizontal escalation      |
| Example endpoint       | `DELETE /api/Students/{id}`    | `GET /api/Students/{id}`   |
| ASP.NET Core mechanism | `[Authorize(Roles = "...")]`   | Policies / manual checks   |

---

## 10. 🔹 Where Ownership Fits in the Security Roadmap

Your security layers now look like this:

| Layer                                               | Status             |
| --------------------------------------------------- | ------------------ |
| 1️⃣ HTTPS + CORS — secure communication             | ✅ Done             |
| 2️⃣ Authentication — identity                       | ✅ Done             |
| 3️⃣ Authorization (Roles) — permissions             | ✅ Done             |
| 4️⃣ Ownership-Based Authorization — data boundaries | ← **You are here** |
| 5️⃣ Policies & advanced rules                       | Pending            |
| 6️⃣ Production hardening                            | Pending            |

> 📌 **Skipping this step <mark>leaves a huge security hole</mark>.**

---

## 11. 🧬 Characteristics of Ownership-Based Authorization

- Requires authentication
- Depends on identity claims
- Depends on resource data
- Prevents horizontal privilege escalation
- Essential for real-world APIs

---

## 12. 🔗 Interconnection

- 🔗 JWT → provides user identity
- 🔗 Route data → identifies requested resource
- 🔗 Ownership logic → compares both
- 🔗 Authorization → allows or blocks access

📌 **Ownership connects identity to data.**

### 🛠️ Final Summary of Interconnections

- 🔹 Roles protect **actions**
- 🔹 Ownership protects **data**
- 🔹 Authentication without ownership is incomplete
- 🔹 Most real-world breaches happen here
- 🔹 This is where APIs become **truly** secure

---

## 13. 🏁 Conclusion

You now understand:

- ✅ Why roles are not enough
- ✅ What ownership-based authorization means
- ✅ Why this problem is so dangerous
- ✅ Where real-world API security actually begins

Your API is about to move from:

> **"Authorized"**

to

> **"<mark>Correctly authorized.</mark>"**

▶️ **Next Lesson:** 🛠️ Implementing Ownership Checks in the Student API

In the next lesson, you will:

- Extract user ID from JWT
- Compare it with route parameters
- Allow student access only to their own data
- Allow admin override
- See real examples of blocked attacks

This is where professional-grade API security starts.

---

## 📌 Summary

| Question          | Role-Based                   | Ownership-Based                        |
| ----------------- | ---------------------------- | -------------------------------------- |
| Answers           | "What type of user?"         | "Is this your data?"                   |
| Prevents          | Vertical escalation          | Horizontal escalation                  |
| Source            | JWT claim (`role`)           | JWT claim + route/resource ID          |
| Example violation | Student deletes a student    | Student reads another student's record |
| ASP.NET mechanism | `[Authorize(Roles = "...")]` | Policies / manual checks               |
| Static or dynamic | Static                       | Dynamic (runtime)                      |

**Core principle:** roles are about **<mark>what kind of user you are</mark>**. Ownership is about **<mark>which record belongs to you</mark>**. Without ownership, any authenticated Student can read any other Student's data by changing one number in the URL — and the API will happily return `200 OK`. Ownership <mark>is the layer that **<u>closes that hole</u>**</mark>.

---


