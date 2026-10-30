# Real-World Authorization Mistakes

---

## 1. 🗝️ Introduction

**Authentication** answers an important question:

> **<mark>Who</mark> are you?**

**Authorization** answers an even more critical question:

> **<mark>What</mark> are you allowed to do?**

Many real-world security breaches occur **after authentication succeeds**, because authorization rules are **<mark>implemented incorrectly</mark>**.

In many systems:

- ✅ The user is authenticated correctly
- ❌ The user is allowed to perform actions they should **<mark>not</mark>** be allowed to perform

These mistakes are **<mark><u>extremely common</u></mark>** in APIs and web applications.

This lesson explores real-world authorization mistakes developers frequently make.

---

## 2. 🔍 Why This Lesson Exists

Developers **<mark>often <u>assume</u></mark>** that once a user is **authenticated**, the system is secure.

**But attackers do not attack authentication.**
**They attack <mark><u>authorization logic</u></mark>.**

They ask questions like:

- 🔹 Can I access another user's data?
- 🔹 Can I modify something I don't own?
- 🔹 Can I call an endpoint meant only for admins?
- 🔹 Can I manipulate request data to bypass restrictions?

📌 **Authorization mistakes allow attackers <mark>to <u>abuse</u> legitimate access</mark>.**

---

## 3. 🔓 Mistake 1 — Missing Ownership Checks

One of the most common authorization mistakes is **failing to verify <mark><u>resource ownership</u></mark>**.

Imagine this endpoint:

```http
GET /api/students/{id}
```

**The endpoint requires authentication:**

```csharp
[Authorize]
```

**But the API <mark><u>does not</u></mark> verify that the student <mark><u>belongs</u></mark> to the authenticated user.**

A user can simply change the ID:

```http
/api/students/1
/api/students/2
/api/students/3
```

If the API returns those records:

- ❌ **The system exposes other users' data.**

📌 **Authentication <mark>succeeded</mark>.**
📌 **But authorization <mark><u>failed</u></mark>.**

---

## 4. 🔓 Mistake 2 — Trusting Client-Supplied Identity

Another dangerous mistake is **<mark><u>trusting</u> values <u>sent by the client</u></mark>**.

**Example request:**

```json
POST /api/orders
{
  "userId": 5,
  "productId": 10
}
```

If the API **trusts** the `UserId` field:

- An attacker can send:

```json
"userId": 1
```

- And create orders for **another user**.

📌 **Secure systems <mark><u>never trust</u></mark> identity values from the client.**

**Instead:**

📌 **The server must <mark><u>derive</u> the user identity</mark> from the authenticated token.**

---

## 5. 🔓 Mistake 3 — UI-Only Authorization

Sometimes developers rely on the **<mark><u>UI</u></mark>** to enforce restrictions.

**Example:**

- 🔹 The admin page is hidden from normal users
- 🔹 Only admins see the button

But the API endpoint still exists:

```http
DELETE /api/users/{id}
```

If the endpoint does **<mark><u>not</u></mark>** verify admin privileges:

**Attackers can <mark>call the API <u>directly</u></mark>.**

📌 **The UI <mark><u>is not</u></mark> a security boundary.**
📌 **The server <mark><u>must enforce</u> authorization</mark>.**

---

## 6. 🔓 Mistake 4 — Missing Role Restrictions

An endpoint may allow dangerous operations but **<mark><u>forget to restrict roles</u></mark>**.

**Example:**

```http
POST /api/users/promote
```

**If the endpoint lacks role checks:**

```csharp
[Authorize(Roles="Admin")]
```

**<mark><u>Any</u> authenticated user</mark>** might be able to change roles.

This can lead to **<mark><u>privilege escalation</u></mark>** (*Vertical*).

---

## 7. 🔓 Mistake 5 — ID Enumeration

Attackers often try **<mark><u>sequential identifiers</u></mark>**.

**Example:**

```http
/orders/1001
/orders/1002
/orders/1003
```

If authorization checks are weak, attackers can **<mark><u>enumerate</u></mark> and <mark>access large amounts</mark> of data**.

📌 **Always verify the user has permission to access the requested resource.**

---

## 8. 🧠 Why These Mistakes Happen

These mistakes usually occur because developers **<mark><u>assume</u></mark>**:

- 🔹 Users will behave **honestly**
- 🔹 The **UI** will prevent misuse
- 🔹 IDs will **not be guessed**
- 🔹 Authentication is **sufficient**

**Attackers <mark><u>rely on these assumptions</u> being wrong</mark>.**

---

## 9. 🧬 Characteristics of Authorization Mistakes

- 🧬 Occur **after** authentication succeeds
- 🧬 Often caused by **missing checks**
- 🧬 Usually exploit **business logic**
- 🧬 Can expose **large amounts of data**
- 🧬 Frequently **overlooked** during testing

---

## 10. 🔗 Interconnection with Previous Lessons

The Student API security stack now includes:

- 🔗 **HTTPS** → secure communication
- 🔗 **CORS** → restrict origins
- 🔗 **Authentication** → verify identity
- 🔗 **Authorization** → enforce permissions
- 🔗 **Rate limiting** → prevent abuse
- 🔗 **Logging** → observe activity
- 🔗 **Auditing** → trace privileged actions

**Authorization mistakes break the permission layer of this stack.**

📌 **If authorization fails, the rest of the security layers cannot prevent data exposure.**

### 🛠️ Final Summary of Interconnections

- 🔹 Authentication **identifies the user**
- 🔹 Authorization **defines what the user can do**
- 🔹 Missing checks **create vulnerabilities**
- 🔹 Attackers **exploit authorization gaps**

📌 **The most dangerous attacks use valid accounts with invalid permissions.**

---

## 11. 🏁 Conclusion

You now understand:

- ✅ The most common **real-world authorization mistakes**
- ✅ Why **authentication alone is not enough**
- ✅ How attackers exploit **missing ownership checks**
- ✅ Why the **server must enforce all permissions**

📌 **Security is not just verifying identity.**

**It is correctly restricting power.**

---

## 📌 Summary

| Mistake                      | What Happens                 | Fix                                         |
| ---------------------------- | ---------------------------- | ------------------------------------------- |
| **Missing Ownership Check**  | User reads others' records   | Verify `resource.OwnerId == currentUser.Id` |
| **Trusting Client Identity** | User impersonates others     | Derive identity from JWT, never from body   |
| **UI-Only Authorization**    | User calls API directly      | Enforce on the server, not the UI           |
| **Missing Role Restriction** | Any user promotes themselves | `[Authorize(Roles = "Admin")]`              |
| **ID Enumeration**           | User reads all data via IDs  | Ownership checks on every resource          |

| Principle                         | Meaning                                    |
| --------------------------------- | ------------------------------------------ |
| **Authenticated ≠ Authorized**    | Identity is not permission                 |
| **Trust the token, not the body** | The JWT is signed; the request body is not |
| **Server enforces, UI displays**  | UI can be bypassed                         |
| **Verify ownership per request**  | Every request, every resource              |
| **Assume hostile input**          | Attackers will try IDs, fields, endpoints  |

**Core principle:** authentication proves *who* the user is. Authorization decides *what* they can do. **The <mark><u>most dangerous breaches</u></mark> happen when the first one <mark>is solid</mark> and the second one <mark>is <u>missing</u></mark>.** A user with a valid token can still **<mark>be an attacker</mark>** — because the token only proves they logged in, not that the specific record they are requesting **<mark>belongs to them</mark>**. **Every endpoint <mark>must answer <u>two questions</u></mark>: "Are you authenticated?" AND "Is this specific action yours to take?"** Answering only the first is the most common mistake in real-world APIs.

---
