# Horizontal Privilege Escalation (Ownership Vulnerabilities)

---

## 1. Introduction

In the previous lesson, you added `[Authorize(Roles = "Admin")]` to protect <mark>**dangerous endpoints**</mark>. <mark>That is **<u>vertical</u>** authorization </mark>— it <mark>prevents a Student **from doing <u>Admin things</u>**</mark>.

But <mark>there is **a second type**</mark> of privilege escalation that <mark>roles **cannot** stop</mark>. <mark>It **<u>*is the most common vulnerability*</u>** in real-world APIs</mark> (OWASP API Security Top 1: **<mark>Broken Object Level Authorization</mark>**).

This MD answers:

- 🔹 What horizontal privilege escalation is
- 🔹 Why roles do not protect against it
- 🔹 What a real attack looks like
- 🔹 How to prevent it

---

## 2. What Is Horizontal Privilege Escalation?

> **Horizontal Privilege Escalation** happens when a user <mark>**accesses** data or **performs** actions</mark> that <mark>belong to **another user at <u>the same privilege level</u>**</mark>.

- You are a **Student**.
- You try to <mark>access the record of **another Student**</mark>.
- You <mark>**did not gain** a new role</mark> (that would <mark>**<u>be vertical</u>**</mark>). You just moved <mark>**<u>*sideways*</u>** across the system</mark> to <mark>**someone else**</mark>'s data.

> 📌 **<mark>Vertical</mark>** = Student <mark>acting like **Admin**</mark>.
> 📌 **<mark>Horizontal</mark>** = Student <mark>acting like ***<u>another</u>*** Student</mark>.

---

## 3. Vertical vs Horizontal (Quick Comparison)

| Type           | Meaning                                                                               | Example                                                | Defense                                               |
| -------------- | ------------------------------------------------------------------------------------- | ------------------------------------------------------ | ----------------------------------------------------- |
| **Vertical**   | Low-privilege user performs <mark>**high-privilege**</mark> action                    | Student calls `DELETE /api/Students/{id}`              | `[Authorize(Roles = "Admin")]`                        |
| **Horizontal** | User accesses <mark>**another user's**</mark> data at <mark>**the same level**</mark> | Student calls `GET /api/Students/2` when they are ID 1 | <mark>**Ownership check**</mark> (policies or manual) |

> 📌 <mark>**Roles**</mark> solve **vertical**. <mark>**Ownership**</mark> solves **horizontal**. They are <mark>**<u>different problems</u>**</mark>.

---

## 4. The Real Attack (Your API Right Now)

Your API has this endpoint:

```http
GET /api/Students/{id}
```

Right now, it is protected by `[Authorize]` <mark>**only**</mark>. Any logged-in user <mark>**can hit it**</mark>.

Imagine Student Ali (ID = 1) logs in. He has a valid JWT with:

- `NameIdentifier = 1`
- `Role = "Student"`

Then, using Postman or the Console Client, Ali simply changes the URL:

```http
GET /api/Students/2
```

**What happens?**

- ✅ Middleware checks the token → valid.
- ✅ `[Authorize]` passes → Ali is authenticated.
- ❌ <mark>**No ownership check**</mark> → Ali reads **Fadi's** data.

**Result:** `200 OK` with Fadi's record.

📌 Ali just "<mark>**hacked**</mark>" the API by changing the number in the URL. <mark>**No exploit code**</mark>. <mark>**No tools**</mark>. Just a valid login and a different ID.

---

## 5. Why Roles Cannot Stop This

Roles <mark>**<u>answer</u>**</mark>:

> "What type of user are you?"

They <mark>do **<u>not</u>** answer</mark>:

> "Do you own this specific record?"

If you add `[Authorize(Roles = "Student")]` to `GET /{id}`, Ali still passes — because Ali **is** a Student. He just reads Fadi's record because the API does not check **whose record it is**.

> 📌 **<mark>Roles are too blunt</mark> to handle individual data ownership.**

---

## 6. The Fix (The Logic)

You <mark>must check **ownership**</mark> inside the controller (or via a policy, which is the next lesson).

The logic:

1. **Who is asking?** → Extract the user ID from the JWT claims (`ClaimTypes.NameIdentifier`).
2. **What are they asking for?** → The `{id}` from the URL.
3. **Do they match?** → Or is the caller an Admin?

```csharp
var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
var currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;

if (currentUserRole != "Admin" && currentUserId != id)
{
    return Forbid(); // 403 Forbidden — you're a Student, but this isn't your record.
}
```

📌 **Admin** → can see any record.
📌 **Student** → can only see records where `currentUserId == id`.

---

## 7. What `return Forbid()` Actually Does

- Returns `403 Forbidden`.
- Means: **"I know who you are — but this is not yours."**
- Different from `401 Unauthorized` (which means "I don't know who you are").

> 📌 `403` <mark>is **the correct response**</mark> to a horizontal escalation attempt.

---

## 8. Why This Comes After Roles

You **cannot** implement ownership checks <mark>**before**</mark> you have <mark>**identity** and **roles**</mark>:

- **Identity** → tells you <mark>**who**</mark> the caller is.
- **Roles** → tells you <mark>**what**</mark> type of user they are.
- **Ownership** → tells you if they <mark>**have permission to access**</mark> the specific record.

<mark>**Skipping**</mark> a layer <mark>**<u>breaks the next one</u>**</mark>. That is why the course order is:

1. HTTPS + CORS
2. JWT (identity)
3. Roles (vertical authorization)
4. Ownership (horizontal authorization) ← **next**
5. Production hardening

---

## 9. Real-World Examples

This is not a theoretical problem. <mark>It is the **#1 API vulnerability**</mark> in the OWASP list, and it has been <mark>the root cause</mark> of many high-profile breaches:

- **T-Mobile (2023):** A leaked API endpoint allowed attackers to query any customer's data by iterating IDs.
- **Parler (2021):** Sequential URLs allowed scraping the entire site by incrementing post IDs.
- **Facebook (2018):** Access token bug allowed users to access other users' accounts.

> 📌 In every case: **<mark>no ownership check</mark>**. Valid identity + predictable IDs = <mark>**<u>mass data exposure</u>**</mark>.

---

## 10. Characteristics

- 🧬 Roles protect vertical access, not horizontal
- 🧬 Ownership requires comparing `currentUserId` to the requested `id`
- 🧬 The vulnerability is silent — the API returns `200 OK`
- 🧬 It is the most common real-world API vulnerability
- 🧬 Prevention requires either manual checks or policies
- 🧬 Correct response: `403 Forbidden`, not `401`

---

## 11. Interconnection

- 🔗 Authentication → knows **who** you are
- 🔗 Roles → control **what type** you are
- 🔗 Ownership → controls **which record** you can touch
- 🔗 Policies → the clean ASP.NET Core way to implement ownership
- 🔗 Next lesson → implements ownership via `IAuthorizationHandler` and policies

### 🛠️ Summary of Interconnections

- 🔹 Identity without roles → everyone is equal
- 🔹 Roles without ownership → Students can read other Students' data
- 🔹 Ownership without policies → manual `if` blocks in every controller
- 🔹 Policies → the scalable, framework-native way to enforce ownership

---

## 📌 Summary

| Concept                             | Meaning                                     | Defense                                |
| ----------------------------------- | ------------------------------------------- | -------------------------------------- |
| **Vertical Privilege Escalation**   | Student acts like Admin                     | `[Authorize(Roles = "Admin")]`         |
| **Horizontal Privilege Escalation** | Student accesses another Student's data     | Ownership check                        |
| **Where to check ownership**        | Controller (manual) or policy (automated)   | Next lesson                            |
| **Response on violation**           | `403 Forbidden`                             | Not `401`                              |
| **OWASP ranking**                   | Broken Object Level Authorization is **#1** | Real, common, silent                   |
| **Real-world examples**             | T-Mobile, Parler, Facebook                  | All caused by missing ownership checks |

**Core principle:** authentication knows <mark>***who*** you are</mark>. Roles know <mark>***what type*** you are</mark>. But **ownership knows <mark><u>which record is yours</u></mark>**. Without ownership, <mark>**every** authenticated user can read **every** other user's data</mark> — and the API will happily return `200 OK`.

---


