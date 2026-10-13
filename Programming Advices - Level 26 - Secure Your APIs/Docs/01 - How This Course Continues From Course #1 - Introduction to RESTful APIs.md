# How This Course Continues From Course #1 — Introduction to RESTful APIs

---

## 1. Introduction

In the previous course, *Introduction to RESTful API*, <mark>you built a fully functional Student RESTful API with real endpoints, database integration, and CRUD operations</mark>.

In this course, <mark>we are **not** building a new API</mark>.

Instead, <mark>we are upgrading the **same** project to become</mark>:

- 🔐 <mark>**Secure**</mark>
- 🧠 <mark>**Professional**</mark>
- 🏭 <mark>**Production-ready**</mark>

This lesson establishes where we start, what already exists, and what will be added — without repeating anything from Course #1.

---

## 2. What You Already Have (From Course #1)

The existing Student API already exposes these endpoints:

| Method | Endpoint                     | Purpose              |
| ------ | ---------------------------- | -------------------- |
| GET    | `/api/Students/All`          | List all students    |
| GET    | `/api/Students/Passed`       | List passed students |
| GET    | `/api/Students/AverageGrade` | Average grade        |
| GET    | `/api/Students/{id}`         | Get student by ID    |
| POST   | `/api/Students`              | Create student       |
| PUT    | `/api/Students/{id}`         | Update student       |
| DELETE | `/api/Students/{id}`         | Delete student       |

These endpoints are:

- ✅ Valid — they work
- ✅ They return correct data
- ✅ They modify the database

<mark><u>**But…**</u></mark>

---

## 3. The Missing Part (The Problem)

Although the API works, it is currently:

- ❌ <mark>Open to anyone</mark>
- ❌ <mark>No authentication</mark>
- ❌ <mark>No authorization</mark>
- ❌ <mark>No ownership rules</mark>
- ❌ <mark>No protection against abuse</mark>

📌 **Example:** Anyone can call:

```js
DELETE /api/Students/{id}
PUT    /api/Students/{id}
```

<mark>**<u>Without logging in.</u>**</mark>

---

## 4. What This Course Will Do (Very Important)

**We will NOT:**

- ❌ Recreate controllers
- ❌ Rebuild CRUD logic
- ❌ Rename endpoints
- ❌ Change routing

**We will:**

- ✅ <mark>**Secure the same endpoints**</mark>
- ✅ <mark>**Add security <u>step by step</u>**</mark>
- ✅ <mark>**Keep the project always running**</mark>

---

## 5. Characteristics (How This Course Is Different)

- 🧬 <mark>**Gradual security — <u>no big jumps</u>**</mark>
- 🧬 Same project from start to end
- 🧬 <mark>**Real attack → real fix**</mark>
- 🧬 <mark>**<u>Beginner-friendly explanations</u>**</mark>

---

## 6. Interconnection (Course 1 → Course 2)

| Course #1         | Course #2                                     |
| ----------------- | --------------------------------------------- |
| REST fundamentals | <mark>**<u>Security fundamentals</u>**</mark> |
| CRUD logic        | <mark><u>**Access control**</u></mark>        |
| DTOs              | <mark>**<u>Claims & policies</u>**</mark>     |
| Working API       | <mark>**<u>Secure API</u>**</mark>            |

### 🛠️ Summary of Interconnections

- 🔹 Existing endpoints → <mark>**upgraded, <u>not replaced</u>**</mark>
- 🔹 Functionality → <mark>**secured functionality**</mark>
- 🔹 Learning REST → <mark>**learning <u>real-world backend security</u>**</mark>

---

## 7. Conclusion

This lesson sets the starting line.

You now clearly know:

- ✅ What exists
- ✅ What is missing
- ✅ Why security is required

In the next lesson, we will run the project and verify everything works before adding security layers step by step.

---

## 📌 Summary

| Aspect                       | Detail                                    |
| ---------------------------- | ----------------------------------------- |
| Goal                         | Upgrade the Level 25 Student API in place |
| New code                     | None — orientation only                   |
| Endpoints                    | Unchanged (7 total)                       |
| Controllers / CRUD / routing | Untouched                                 |
| Security state               | Fully open, no auth, no authorization     |
| Course approach              | Gradual, same project, attack → fix       |
| Next lesson                  | Run & verify the existing API             |
