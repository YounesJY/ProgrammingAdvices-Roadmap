# Applying Policies Across the Student API

**<mark>Ownership + Admin Rules in Practice</mark>**

---

## 1. Introduction

In the previous lesson, you learned how to:

- Create an ownership-based policy
- Centralize authorization logic
- Separate security rules from controller code

Now it's time to apply that policy **consistently** across the Student API.

This lesson is about structure, discipline, and correctness.

> **Security is not just about writing rules — <mark>it's about applying them everywhere they belong</mark>.**

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will be able to:

- Apply ownership policies to multiple endpoints
- Combine ownership rules with admin privileges
- <mark>**Decide when to use**</mark>:
  - Role-based authorization
  - Ownership-based policies
  - Both together
- Test authorization behavior using:
  - Swagger
  - The C# console client
- Build an authorization structure that scales cleanly

---

## 3. 🔹 Step 1 — Revisit the Authorization Boundary Table

<mark>**Before applying policies**</mark>, we must be clear about access boundaries.

Your Student API rules are:

| Endpoint                         | Access Rule                 |
| -------------------------------- | --------------------------- |
| `GET /api/Students/Passed`       | 🌐 Public                   |
| `GET /api/Students/AverageGrade` | 🌐 Public                   |
| `GET /api/Students/{id}`         | Owner (Student) or 👑 Admin |
| `GET /api/Students/All`          | 👑 Admin only               |
| `POST /api/Students`             | 👑 Admin only               |
| `PUT /api/Students/{id}`         | 👑 Admin only               |
| `DELETE /api/Students/{id}`      | 👑 Admin only               |

---

## 4. 🔹 Step 2 — Identify Where Ownership Policies Are Required

Ownership policies are required when:

- The endpoint <mark>targets a **specific resource**</mark>
- The resource <mark>**belongs to a specific user**</mark>
- The decision <mark>depends on **runtime data**</mark>

In your API, that is:

```js
GET /api/Students/{id}
```

This endpoint <mark>**must use**</mark>:

**`StudentOwnerOrAdmin` policy**

---

## 5. 🔹 Step 3 — Apply the Ownership Policy Correctly

The correct pattern is:

1. Authentication <mark>happens **automatically**</mark>
2. Controller <mark>asks the **authorization system**</mark>
3. Policy <mark>evaluates **ownership + admin override**</mark>
4. Controller <mark>does **not** re-implement security logic</mark>

**Result:**

- **<u>No</u>** duplicated logic
- **<u>No</u>** forgotten checks
- **<u>Clean</u>** controller methods

> 📌 This is <mark>**professional-grade** authorization</mark>.

---

## 6. 🔹 Step 4 — Keep Admin-Only Endpoints Role-Based

<mark>**Not every**</mark> endpoint <mark>needs **a policy**</mark>.

Endpoints that are:

- **<u>Administrative</u>**
- **<u>Global</u>**
- <mark>**Not tied** to **<u>a specific</u>**</mark> resource owner

...<mark>should remain **role-based**</mark>.

**Examples:**

```csharp
[Authorize(Roles = "Admin")]
GET /api/Students/All

[Authorize(Roles = "Admin")]
POST /api/Students

[Authorize(Roles = "Admin")]
PUT /api/Students/{id}

[Authorize(Roles = "Admin")]
DELETE /api/Students/{id}
```

> 📌 **Do not overuse policies <mark>when roles <u>are sufficient</u></mark>.**

---

## 7. 🔹 Step 5 — Combine Ownership + Admin Rules (Conceptual Model)

Your authorization logic now follows this model:

- **Authentication** answers: *<mark>**Who is**</mark> calling?*
- **Roles** answer: *<mark>**What actions**</mark> are allowed globally?*
- **Ownership policies** answer: *Is this specific data <mark>**yours**</mark>?*

**Admins:**

- <mark>**Bypass**</mark> ownership checks
- <mark>**Retain <u>full access</u>**</mark>

**Students:**

- <mark>**Restricted**</mark> by ownership
- <mark>**<u>Cannot escalate</u>**</mark> privileges horizontally

---

## 8. 🔹 Step 6 — Test Policy Behavior Using Swagger

In Swagger, you should test:

1️⃣ Login as **Student**
2️⃣ Call `GET /api/Students/{ownId}` → `200 OK`
3️⃣ Call `GET /api/Students/{otherId}` → `403 Forbidden`

Then:

4️⃣ Login as **Admin**
5️⃣ Call any `GET /api/Students/{id}` → `200 OK`

**This confirms:**

- Ownership <mark>is **enforced**</mark>
- Admin <mark>**override** works</mark>
- Authorization rules <mark>**<u>are correct</u>**</mark>

---

## 9. 🔹 Why This Structure Scales

Your API now has:

- <mark>**Centralized**</mark> authorization rules
- <mark>**Clear**</mark> responsibility boundaries
- <mark>**Minimal logic**</mark> inside controllers
- A model that <mark>**supports growth**</mark>

If tomorrow you add:

- Teachers
- Supervisors
- Multi-tenant rules
- Subscription tiers

> ...<mark>you extend **policies**</mark> — not controllers.

---

## 10. 🧬 Characteristics of a Clean Authorization Structure

- Explicit boundaries
- Centralized rules
- Minimal duplication
- Easy auditing
- Safe by default

---

## 11. 🔗 Interconnection

- 🔗 JWT → <mark>**identity**</mark>
- 🔗 Roles → <mark>**global permissions**</mark>
- 🔗 Policies → <mark>**resource ownership**</mark>
- 🔗 Authorization system → <mark>**single decision engine**</mark>
- 🔗 Controllers → <mark>**<u>execution only</u>**</mark>

### 🛠️ Final Summary of Interconnections

- 🔹 Roles protect **<mark>actions</mark>**
- 🔹 Ownership protects **<mark>data</mark>**
- 🔹 Policies <mark>**<u>unify both</u>**</mark>
- 🔹 Controllers <mark>**<u>remain clean</u>**</mark>
- 🔹 Security <mark>**<u>becomes scalable</u>**</mark>

> 📌 **<mark>Do not</mark> use policies when roles already solve the problem.**

---

## 12. 🔹 Why We Built a Policy Even for One Endpoint

This is **intentional**.

Policies are created <mark>based on **correctness and future growth**</mark>, not quantity.

**Today**, the policy protects:

- One endpoint

**Tomorrow**, it may protect:

- Multiple endpoints
- New features
- Additional resources

Because the rule <mark>is **<u>centralized</u>**</mark>, you can reuse it safely without rewriting logic.

---

## 13. 🧠 How to Think Going Forward

Whenever <mark>you design **a new endpoint**</mark>, ask:

1. Does this access depend on **who owns the data**?
2. Can **roles alone** express the rule?
3. Does this require comparing **user identity** with **resource data**?

If the answer is **yes**, use an **ownership policy**.

If **not**, use **roles** or allow **anonymous access**.

---

## 14. 🏁 Conclusion

You have now:

- Applied ownership policies correctly
- Combined roles and policies safely
- Tested authorization in real scenarios
- Built a structure used in real production APIs

**Your Student API now enforces identity, permissions, and data boundaries correctly.**

---

## 📌 <mark>**Summary**</mark>

| Question                                            | When to Use                                 |
| --------------------------------------------------- | ------------------------------------------- |
| Is this endpoint tied to a specific resource owner? | ✅ Ownership policy (`AuthorizeAsync`)       |
| Is this endpoint administrative/global?             | ✅ Role-based (`[Authorize(Roles = "...")]`) |
| Can anyone access it safely?                        | ✅ `[AllowAnonymous]`                        |
| Does the rule depend on runtime data?               | ✅ Policy (cannot be done with attributes)   |
| Is the rule static and role-only?                   | ✅ Attribute                                 |

| Concept                | Defends Against                 |
| ---------------------- | ------------------------------- |
| **Roles**              | Vertical privilege escalation   |
| **Ownership Policies** | Horizontal privilege escalation |
| **Both Together**      | Complete authorization boundary |

**Core principle:** roles and policies <mark>**are not** competing tools</mark> — <mark>they are **complementary layers**</mark>. Roles handle *"what type of user are you?"*. Ownership policies handle *"is this data yours?"*. Use each <mark>**where it fits**</mark>, and the API <mark>stays both **secure** and **maintainable**</mark>.

---


