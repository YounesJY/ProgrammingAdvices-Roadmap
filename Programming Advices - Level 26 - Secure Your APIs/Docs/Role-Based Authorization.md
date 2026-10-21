# Role-Based Authorization — What Can Students and Admins Do?

---

## 1. Introduction

Up to this point, your API <mark>has achieved **something critical**</mark>:

- ✅ Secure communication (HTTPS + CORS)
- ✅ Authentication using JWT
- ✅ The API knows who is calling it

> But knowing who is calling <mark>**<u>is not enough</u>**</mark>.

If every authenticated user <mark>can access **every endpoint**</mark>, then your system <mark>**is still <u>unsafe</u>**</mark>.

This lesson introduces <mark>clear **security boundaries** using **roles**</mark>.

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will understand:

- 🔹 Why authentication alone <mark>**is dangerous**</mark>
- 🔹 What role-based authorization really means
- 🔹 How to define clear access boundaries
- 🔹 How students and admins are treated differently
- 🔹 Why `403 Forbidden` <mark>is **a security feature**</mark>
- 🔹 How this <mark>**prepares you** for <u>ownership & policies</u></mark>

---

## 3. 🔐 Authentication vs 🛂 Authorization (Quick Reminder)

**Authentication answers:**

> <mark>**Who are you**</mark>?

Example:

- "I am Ali"
- "Here is my valid JWT"

**Authorization answers:**

> What are you <mark>**allowed to do**</mark>?

Example:

- "Can you delete students?"
- "Can you see all records?"
- "Can you access someone else's data?"

> 📌 Authentication always <mark>comes **<u>first</u>**</mark>.
> 📌 Authorization always <mark>comes **second**</mark>.

---

## 4. 🔹 The Security Boundary Table (<mark>**Very Important**</mark>)

From now on, this table defines the rules of your API. Every lesson that follows will respect this boundary.

| Endpoint                         | Access Rule                 |
| -------------------------------- | --------------------------- |
| `GET /api/Students/Passed`       | 🌐 Public                   |
| `GET /api/Students/AverageGrade` | 🌐 Public                   |
| `GET /api/Students/{id}`         | Student (owner) or 👑 Admin |
| `GET /api/Students/All`          | 👑 Admin only               |
| `POST /api/Students`             | 👑 Admin only               |
| `PUT /api/Students/{id}`         | 👑 Admin only               |
| `DELETE /api/Students/{id}`      | 👑 Admin only               |

> 📌 This table <mark>is **<u>not</u> optional**</mark>.
> 📌 This is your **<mark><u>security contract</u></mark>**.

---

## 5. 🔹 What This Table Really Means

### 5.1 🔓 Public Endpoints

These endpoints:

- Do **NOT** require authentication
- Do **NOT** require a JWT
- Are <mark>**safe to expose**</mark> publicly

Examples:

- Viewing passed students
- Viewing average grades

These endpoints <mark>do not reveal **sensitive data**</mark>.

### 5.2 Student (Owner) or Admin

> This is <mark>**the first advanced security rule**</mark>.

Example:

```js
GET /api/Students/{id}
```

Access is allowed if:

- The user is an **Admin**, OR
- The user is a **Student** requesting <mark>their **own** record</mark>

> 📌 A student <mark>must **NOT** access</mark> another student's data.

> This introduces <mark>the concept of **ownership**</mark>, which will be handled later <mark>using **policies**</mark>.

### 5.3 👑 Admin-Only Endpoints

These are <mark>**dangerous operations**</mark>:

- Viewing all students
- Creating students
- Updating students
- Deleting students

Only **admins** are allowed.

> Students are <mark>**explicitly blocked**</mark> — <mark>**even** if authenticated</mark>.

---

## 6. 🔹 Why This Boundary Is Critical

Without this table:

- ❌ <mark>**<u>Any</u>**</mark> logged-in student could **delete** data
- ❌ <mark>**<u>Any</u>**</mark> logged-in student could **modify** records
- ❌ <mark>**<u>Any</u>**</mark> logged-in student could **view all students**

> This is how real systems <mark>**get breached**</mark>, even with JWT.

---

## 7. 🔹 How ASP.NET Core Enforces These Rules

ASP.NET Core <mark>uses **declarative authorization**</mark>.

**Examples:**

```csharp
[Authorize]
```

Requires authentication only.

```csharp
[Authorize(Roles = "Admin")]
```

Requires:

- A valid JWT
- `Role = Admin`

📌 Controllers do **NOT** read JWT manually.
📌 Controllers do **NOT** parse claims manually.
📌 The framework <mark>**enforces rules consistently**</mark>.

---

## 8. 🔹 Understanding `401` vs `403` (Critical)

### ❌ 401 Unauthorized

- No token
- Invalid token
- Authentication failed

**Meaning:**

> "I don't know who you are."

### ❌ 403 Forbidden

- Token is valid
- User is authenticated
- User is **NOT** allowed

**Meaning:**

> "I know who you are — but you are not allowed."

> 📌 `403` <mark>**is not** an error</mark>.
> 📌 `403` <mark>is a **security success**</mark>.

---

## 9. 🧠 Your Current Security Level

You are now moving from:

**Authenticated API (JWT)**

⬇️

<mark>**Authorized API (Roles)**</mark>

This means:

- <mark>Identity **exists**</mark>
- <mark>**<u>Permissions now matter</u>**</mark>
- <mark>**Not all** users **are equal <u>anymore</u>**</mark>

---

## 10. 🔗 How This Fits Into the Security Roadmap

You are building security in layers:

1️⃣ Secure transport (HTTPS + CORS)
2️⃣ Authentication (JWT — identity)
3️⃣ Authorization (Roles — permissions) ← **you are here**
4️⃣ Ownership & policy-based rules
5️⃣ Production hardening

> 📌 <mark>**Skipping**</mark> a layer <mark>**<u>breaks security</u>**</mark>.
> 📌 You are building it in the correct order.

---

## 11. 🛠️ Final Summary of Interconnections

- 🔹 Authentication tells <mark>**who**</mark> you are
- 🔹 Roles tell <mark>**what**</mark> you can do
- 🔹 The boundary table defines <mark>**what must be protected**</mark>
- 🔹 Authorization enforces those rules automatically
- 🔹 `403 Forbidden` <mark>**protects** your system</mark>

---

## 12. 🏁 Conclusion

You now clearly understand:

- ✅ Why authentication alone is dangerous
- ✅ How roles protect sensitive operations
- ✅ How to read and enforce access boundaries
- ✅ Why admins and students must be separated
- ✅ How this prepares you for ownership rules

> 📌 Your API <mark>**is no longer**</mark> just **authenticated**.
> 📌 It is becoming **<mark>professionally authorized</mark>**.

▶️ **Next Lesson:** 🛠️ Implementing Role-Based Authorization in the Student API

You will:

- Apply `[Authorize(Roles = "...")]`
- Enforce admin-only endpoints
- Prepare for ownership rules
- Test `403 Forbidden` in action
- Respect the boundary table fully

> This is where permissions <mark>**become**</mark> real security 🔐.

---

## 📌 Summary

| Endpoint                         | Who Can Access           | Auth Required | Role Required                                 |
| -------------------------------- | ------------------------ | ------------- | --------------------------------------------- |
| `GET /api/Students/Passed`       | Anyone                   | ❌             | —                                             |
| `GET /api/Students/AverageGrade` | Anyone                   | ❌             | —                                             |
| `GET /api/Students/{id}`         | Owner (Student) or Admin | ✅             | Student or Admin (with ownership check later) |
| `GET /api/Students/All`          | Admin                    | ✅             | Admin                                         |
| `POST /api/Students`             | Admin                    | ✅             | Admin                                         |
| `PUT /api/Students/{id}`         | Admin                    | ✅             | Admin                                         |
| `DELETE /api/Students/{id}`      | Admin                    | ✅             | Admin                                         |

| Status Code        | Meaning                       | Message                                         |
| ------------------ | ----------------------------- | ----------------------------------------------- |
| `401 Unauthorized` | Not authenticated             | "I don't know who you are."                     |
| `403 Forbidden`    | Authenticated but not allowed | "I know who you are — but you are not allowed." |

**Core principle:** <mark>authentication is ***identity***</mark>. <mark>Authorization is ***permission***</mark>. Identity without permission <mark>is a door **<u>with no lock</u>**</mark> — anyone with a key <mark>**<u>gets everywhere</u>**</mark>. <mark>**Roles**</mark> are the first lock. <mark>**Ownership policies**</mark> will be the second.

---


