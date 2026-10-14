# Security Audit — Attacking the Existing Student API

---

## 1. Introduction

In the previous lessons, you confirmed two critical facts:

- ✅ Your Student API works correctly
- ❌ Your Student API is completely open

In this lesson, we will intentionally attack our own API.

⚠️ <mark>This is **not hacking** — this is a **<u>security audit</u>**</mark>, <mark>something **<u>every professional</u> backend developer <u>must know how to do</u>**</mark>.

> <mark>**You cannot secure something**</mark> <mark>**<u>until you clearly see how it breaks</u>**</mark>.

---

## 2. Hacking vs Security Auditing

![](https://uploads.teachablecdn.com/attachments/87f9ee2c65d64139a6fd341cec63c492.png)

|                | 🔴 Hacking (<mark>Illegal / Malicious</mark>)        | 🟢 <mark>Security Auditing</mark> / <mark>Ethical Testing</mark> |
| -------------- | ---------------------------------------------------- | ---------------------------------------------------------------- |
| **Goal**       | <mark>**Cause harm or steal data**</mark>            | <mark>Find risks **before attackers do**</mark>                  |
| **Permission** | ❌ <mark>No permission</mark>                         | ✅ <mark>Explicit permission</mark>                               |
| **Intent**     | ❌ Malicious                                          | ✅ Defensive & protective                                         |
| **Outcome**    | Data theft, system damage, service disruption        | Risk reports, security improvements, stronger systems            |
| **Core idea**  | <mark>**Uses exploitation** for personal gain</mark> | <mark>**Uses attacker mindset** for protection</mark>            |

---

## 3. What Is a Security Audit?

A security audit means:

- 🔹 Testing the system <mark>as an attacker</mark>
- 🔹 <mark>Trying to misuse</mark> valid endpoints
- 🔹 Observing <mark>what the system allows</mark>
- 🔹 <mark>**Identifying security risks**</mark>, not bugs

> 📌 **Important:** <mark>The API **is doing exactly <u>what we told it to do</u>**</mark> — <mark>the problem is what we did <u>**NOT** tell it to do</u></mark>.

### 3.1 What "The API is doing exactly what we told it to do" Means

Imagine you wrote this API rule:

> "If someone calls `GET /students`, return all students."

<mark>**The API does that perfectly**</mark>.

<mark>It **<u>does not ask</u>**</mark>:

- Who is calling?
- Should they see all students?
- Are they allowed?

<mark>Because **<u>you never told it to ask those questions</u>**.</mark>

So from the API's point of view:

- ✅ <mark>It is correct</mark>
- ❌ <mark>**<u>It is not safe</u>**</mark>

### 3.2 What "the problem is what we did NOT tell it to do" Means

You did NOT tell the API things like:

- 🔹 "Only logged-in users can call this"
- 🔹 "Only admins can see all students"
- 🔹 "A student can see only their own record"
- 🔹 "Block too many requests"
- 🔹 "Reject unexpected input"

<mark>Since those rules **don't exist**, **the API <u>allows everything</u>**</mark>.

> 📌 That is not a bug — <mark>that is **missing security instructions**</mark>.

---

## 4. Think Like an Attacker (Security Audit Mindset)

An attacker does NOT think:

> "How do I crash the API?"

<mark>They think</mark>:

> "What is the API <mark>**allowed to do**</mark>?"

So they try things like:

- Call endpoints **<u>without logging in</u>**
- Access IDs **<u>that are not theirs</u>**
- Send requests **<u>very fast</u>**
- Use valid endpoints **<u>in unintended ways</u>**

And the API replies:

> "Sure, **<mark>you didn't tell me <u>not to</u></mark>**."

### 4.1 Very Important Difference

| ❌ Bug            | 🔐 Security Risk           |
| ---------------- | -------------------------- |
| Code crashes     | API works                  |
| Wrong output     | Returns data               |
| Exception thrown | Allows something dangerous |

> 📌 **<mark>Security risks are <u>successful behavior</u></mark>, not failures.**
> 
> 📌 successful behavior = <mark>**<u>The API is behaving as coded</u>**/ **The API follows defined rules**</mark>.

### 4.2 Real Example (Very Simple)

```js
GET /students/1
GET /students/2
GET /students/3
```

The API returns all of them.

Why?

- You told it: "Return student by ID"
- <mark>**You did NOT tell it**</mark>: "<mark>**<u>Check ownership</u>**</mark>"

So the API is:

- ✅ Correct
- ❌ Insecure

### 4.3 One Sentence Summary (<mark>**Memorize This**</mark>)

> **<mark>Security problems <u>are caused by missing rules</u>, <u>not broken code</u>.</mark>**

Or in other words:

> The API is doing exactly what we told it to do — <mark>**the real problem <u>is what we did NOT tell it to do</u>**</mark>.

---

## 5. Attack #1 — Unauthorized Delete (<mark>**Critical**</mark>)

📋 **Instructions**

Using Swagger or Postman:

- 🔹 Call `DELETE /api/Students/{id}`
- 🔹 Use any valid student ID
- 🔹 Do NOT login
- 🔹 Do NOT send any token

📌 **What Just Happened?**

- 🔹 The student was deleted
- 🔹 <mark>**No identity** was checked</mark>
- 🔹 <mark>**No permission** was required</mark>

❌ <mark>**Invalid for production**</mark>.

### 5.1 Why This Is Dangerous

<mark>Imagine this **in a real system**</mark>:

- 🔹 University student records
- 🔹 Grades
- 🔹 Personal data

Anyone could:

- ❌ Delete students
- ❌ Destroy academic history
- ❌ Corrupt reports

---

## 6. Attack #2 — Unauthorized Update

📋 **Instructions**

Call `PUT /api/Students/{id}`.

- 🔹 Modify:
  - Name
  - Age
  - Grade

Again:

- 🔹 No login
- 🔹 No token

📌 **Result**

- 🔹 Student data changes successfully

❌ <mark>**Invalid for production**</mark>.

---

## 7. Attack #3 — Mass Data Exposure (Privacy Risk)

📋 **Instructions**

Call `GET /api/Students/All`.

- 🔹 Anyone can retrieve:
  - All students
  - All grades
  - All data

📌 Even if deletion is blocked later, <mark>**data exposure is also a security risk**</mark>.

---

## 8. Characteristics (What We Discovered)

- 🧬 <mark>The API **trusts everyone**</mark>
- 🧬 <mark>**There is no identity**</mark>
- 🧬 <mark>**There is no permission system**</mark>
- 🧬 <mark>Write endpoints **<u>are extremely dangerous</u>**</mark>
- 🧬 <mark>Read endpoints **<u>may leak sensitive data</u>**</mark>

---

## 9. Interconnection (Audit → Security Design)

- 🔗 Attacks → <mark>**Security requirements**</mark>
- 🔗 Open endpoints → <mark>**Access rules**</mark>
- 🔗 CRUD logic → <mark>**Authorization logic**</mark>

### 🛠️ Summary of Interconnections

- 🔹 Seeing the attack → <mark>**understanding the risk**</mark>
- 🔹 Understanding the risk → <mark>**justifying security**</mark>
- 🔹 Security <mark>**<u>is a response to danger</u>**</mark>, not decoration

---

## 10. Conclusion

You have now proven that your API is unsafe.

Not by theory — but <mark>**<u>by real attacks on your own code</u>**</mark>.

This lesson gives us permission and motivation to add security.

📌 In the next lesson, <mark>**<u>we will define security boundaries</u>**</mark>:

- Which endpoints stay public
- Which require login
- Which require admin permissions

<mark>**<u>That is the foundation of all API security</u>**</mark>.

---

## 📌 Summary

| Attack              | Endpoint                    | Auth Required? | Result                              |
| ------------------- | --------------------------- | -------------- | ----------------------------------- |
| Unauthorized Delete | `DELETE /api/Students/{id}` | None           | <mark>**Student deleted**</mark>    |
| Unauthorized Update | `PUT /api/Students/{id}`    | None           | <mark>**Data modified**</mark>      |
| Mass Data Exposure  | `GET /api/Students/All`     | None           | **<mark>All records leaked</mark>** |

> **<mark>Core principle</mark>:** <mark>Security problems come from **missing rules**, <u>not broken code</u></mark>. The API is doing exactly what we told it to do — <mark>**the vulnerability <u>is what we never told it *not* to do</u>**</mark>.


