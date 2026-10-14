# 📖 Defining Security Boundaries for Student Endpoints

---

## 1. Introduction

In the previous lesson, you proved that your Student API is unsafe <mark>by performing real attacks</mark>.

Now comes a very important professional step:

🔐 <mark>**Deciding <u>who is allowed</u> <u>to access what</u>.**</mark>

This step is called <mark>**defining security boundaries**</mark>. <mark>**<u>Without clear boundaries</u>**</mark>, **<u>no</u>** authentication or JWT will help you.

<img title="" src="https://uploads.teachablecdn.com/attachments/9e7733930c6447d997a28d5b183066e0.png" alt="" data-align="center" width="401">

---

## 2. What Are Security Boundaries?

Security boundaries <mark>**answer three critical questions**</mark>:

- 🔹 **<u>Who can access</u>** this endpoint?
- 🔹 Should this endpoint **<u>be public or protected</u>**?
- 🔹 What happens if the **<u>wrong person accesses it</u>**?

> 📌 <mark>This is **design work, not coding yet**</mark>.

---

## 3. Step 1 — <mark>List All Existing Endpoints</mark> (From Your API)

Your current endpoints are:

| Method | Endpoint                     |
| ------ | ---------------------------- |
| GET    | `/api/Students/All`          |
| GET    | `/api/Students/Passed`       |
| GET    | `/api/Students/AverageGrade` |
| GET    | `/api/Students/{id}`         |
| POST   | `/api/Students`              |
| PUT    | `/api/Students/{id}`         |
| DELETE | `/api/Students/{id}`         |

---

## 4. Step 2 — <mark>Categorize Endpoints by Risk</mark>

### 4.1 🔴 High Risk (Must Be Protected)

- 🔹 `POST /api/Students`
- 🔹 `PUT /api/Students/{id}`
- 🔹 `DELETE /api/Students/{id}`

**Why?**

- 🔹 **<u>Modify data</u>**
- 🔹 **<u>Can destroy records</u>**
- 🔹 **<u>Can corrupt grades</u>**

### 4.2 🟡 Medium Risk (Needs Careful Rules)

- 🔹 `GET /api/Students/{id}`
- 🔹 `GET /api/Students/All`

**Why?**

- 🔹 **<u>Exposes personal data</u>**
- 🔹 **<u>Can leak grades and identities</u>**

### 4.3 🟢 Low Risk (Can Be Public)

- 🔹 `GET /api/Students/Passed`
- 🔹 `GET /api/Students/AverageGrade`

**Why?**

- 🔹 Aggregated data
- 🔹 **<u>No personal identity exposure</u>**

---

## 5. Step 3 — Define <mark>Who Should Access Each</mark> Group

Now we define clear access rules.

### 5.1 🔐 Admin Only

- 🔹 Create student
- 🔹 Update student
- 🔹 Delete student

**Endpoints:**

- 🔹 `POST /api/Students`
- 🔹 `PUT /api/Students/{id}`
- 🔹 `DELETE /api/Students/{id}`

### 5.2 🟡 Authenticated Student (With Rules)

- 🔹 View **<u>own student record</u>**

**Endpoint:**

- 🔹 `GET /api/Students/{id}`

📌 **Rule:** A student <mark>must only access his own ID</mark>.

### 5.3 🟢 Public (No Login Required)

- 🔹 View passed students
- 🔹 View average grade

**Endpoints:**

- 🔹 `GET /api/Students/Passed`
- 🔹 `GET /api/Students/AverageGrade`

---

## 6. Step 4 — Final Security Boundary Table

<img title="" src="https://uploads.teachablecdn.com/attachments/e15f4800917c489394985f34260df968.png" alt="" data-align="center" width="996">

| Endpoint                         | Risk      | Who Can Access                      | Auth Required |
| -------------------------------- | --------- | ----------------------------------- | ------------- |
| `POST /api/Students`             | 🔴 High   | Admin only                          | Yes           |
| `PUT /api/Students/{id}`         | 🔴 High   | Admin only                          | Yes           |
| `DELETE /api/Students/{id}`      | 🔴 High   | Admin only                          | Yes           |
| `GET /api/Students/{id}`         | 🟡 Medium | Authenticated student — own ID only | Yes           |
| `GET /api/Students/All`          | 🟡 Medium | (Not yet defined — revisit)         | TBD           |
| `GET /api/Students/Passed`       | 🟢 Low    | Public                              | No            |
| `GET /api/Students/AverageGrade` | 🟢 Low    | Public                              | No            |

---

## 7. Characteristics

- 🧬 <mark>Security **is planned before coding**</mark>
- 🧬 <mark>**Not all endpoints are equal**</mark>
- 🧬 <mark>**Read endpoints can still be dangerous**</mark>
- 🧬 <mark>**Clear rules simplify implementation**</mark>

---

## 8. Interconnection

- 🔗 Attacks → Risk analysis
- 🔗 Risk analysis → Access rules
- 🔗 Access rules → JWT, Roles, Policies

### 🛠️ Summary of Interconnections

- 🔹 Attacks expose weaknesses → boundaries define protection
- 🔹 Boundaries define rules → rules guide implementation
- 🔹 <mark>**Good security <u>starts with clear decisions</u>**</mark>

---

## 9. Conclusion

You have now completed the design phase of security.

You clearly know:

- ✅ Which endpoints are dangerous
- ✅ Which endpoints can remain public
- ✅ Who should access what

📌 From the next lesson onward, we will start implementing security.

---

## 📌 Summary

| Boundary                    | Endpoints                          | Rule                                     |
| --------------------------- | ---------------------------------- | ---------------------------------------- |
| 🔴 Admin only               | `POST`, `PUT`, `DELETE`            | Write operations require admin           |
| 🟡 Authenticated (own data) | `GET /{id}`                        | Student can only read own record         |
| 🟡 Medium (undefined)       | `GET /All`                         | Left open in the lesson — flag for later |
| 🟢 Public                   | `GET /Passed`, `GET /AverageGrade` | No auth needed                           |

**Note:** the lesson does not explicitly assign `GET /api/Students/All` a boundary. It is classified Medium Risk but never given an access rule. Worth deciding yourself before implementing — <mark>admin-only is the safe default</mark>, since it exposes every student record in one call.

**One-line takeaway:** <mark>**security boundaries <u>are a *design decision*</u> <u>made before any code</u>**</mark> — <mark>**they define <u>who can</u> <u>do what</u>**</mark>, and every later layer (JWT, roles, policies) <mark>**<u>is just enforcement of these decisions</u>**</mark>.


