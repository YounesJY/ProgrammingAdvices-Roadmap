# Why Authentication Is Needed Now?

**<mark><u>Authentication Fundamentals</u> — <u>Who Is</u> Calling Your Student API?</mark>**

<img title="" src="https://uploads.teachablecdn.com/attachments/4df8bd4cea184d458946dd61c44a94ac.png" alt="" width="426">

---

## 1. Introduction

So far, we have achieved two important things:

- ✅ <mark>Data **travels securely**</mark> (HTTPS)
- ✅ <mark>Browser access **is controlled**</mark> (CORS)

But the API <mark>**still has <u>one huge blind spot</u>**</mark>:

❓ **<mark>It does not know <u>who is</u> making the request</mark>.**

In this lesson, we <u>**will not write JWT code yet**</u>.

Instead, <mark>we will build the **mental model** of authentication</mark>.

This lesson answers one question only:

> **<mark>How does</mark> an API identify the caller?**

---

## 2. What Is Authentication?

<img title="" src="https://uploads.teachablecdn.com/attachments/2ebd6f04547e4610a03dae0a70d08bba.png" alt="" width="492">

<mark>Authentication **answers**</mark> this question:

🔑 <mark><u>**Who are you**</u></mark>?

It does **not** answer:

- What can you do?
- Are you allowed to do this?

> <mark>Those are **authorization**</mark>, which comes later.

---

## 3. Authentication in Real Life (Analogy)

Think about entering a university:

- 🔹 Gate security checks your ID
- 🔹 They do not decide which office you can enter
- 🔹 They only confirm your identity

> 📌 **<mark>Authentication = <u>identity verificatio</u>n.</mark>**

---

## 4. Why Your Student API Needs Authentication

Look at your existing endpoints:

- 🔹 `POST /api/Students`
- 🔹 `PUT /api/Students/{id}`
- 🔹 `DELETE /api/Students/{id}`

Right now:

- ❌ <mark>**Anyone**</mark> can call them
- ❌ API <mark>trusts **every** request</mark>

Authentication will allow the API to say:

> "<mark>I know **who you are**</mark> — now <mark>**I can decide**</mark> what you're allowed to do."

---

## 5. Common Authentication Approaches (High-Level)

<img src="https://uploads.teachablecdn.com/attachments/962a60254cad4d5192c6993420c874ba.png" title="" alt="" width="510">

APIs usually use one of these:

- 🔹 Username & Password → <mark>**Token**</mark>
- 🔹 <mark>**API Keys**</mark>
- 🔹 <mark>**OAuth**</mark> (Google, Microsoft, etc.)

📌 **In this course, we will use:**
🔐 Username + Password → **<mark>JWT Token</mark>**

Because it is:

- Beginner-friendly
- <mark>**Stateless**</mark>
- <mark>**Widely used**</mark>
- Perfect for REST APIs

---

## 6. Stateless Authentication (<mark>**<u>Important Concept</u>**</mark>)

<img src="https://uploads.teachablecdn.com/attachments/313292c7d947433eb8ec0fc8ac8492a8.png" title="" alt="" width="533">

Your Student API <mark>is **stateless**</mark>, which means:

- 🔹 The server <mark>**does not remember** users between requests</mark>
- 🔹 <mark>**Every** request **must prove identity <u>again</u>**</mark>

📌 JWT solves this by:

- Carrying identity info <mark>**inside** the token</mark>
- Being sent <mark>with **every** request</mark>

---

## 7. What Authentication Will Look Like in Our API

<img src="https://uploads.teachablecdn.com/attachments/a33721ab4ad9414a846592f47d705978.png" title="" alt="" width="569">

We will add new endpoints (without changing existing Student endpoints):

- 🔹 `POST /api/Auth/Register`
- 🔹 `POST /api/Auth/Login`

**<mark><u>Flow</u></mark>:**

1️⃣ Student logs in
2️⃣ API **<u>verifies credentials</u>**
3️⃣ API **returns a <u>token</u>**
4️⃣ Client **<mark>sends token with requests</mark>**

---

## 8. What Authentication Does NOT Do

Authentication does **NOT**:

- ❌ Decide if you are admin
- ❌ Decide which student you can access
- ❌ Protect business rules

> 📌 <mark>That comes in **authorization** </mark>lessons.

---

## 9. Characteristics

- 🧬 Authentication identifies users
- 🧬 It does not grant permissions
- 🧬 <mark>**<u>Stateless by design</u>**</mark>
- 🧬 <mark>**Required <u>before</u>** authorization</mark>
- 🧬 <mark>**Foundation** **<u>for all</u>** advanced security</mark>

---

## 10. Interconnection

- 🔗 HTTPS → safe **<u>credential transfer</u>**
- 🔗 CORS → safe **<u>browser access</u>**
- 🔗 Authentication → <mark>**identity**</mark>
- 🔗 Authorization → permissions (next lessons)

### 🛠️ Summary of Interconnections

- 🔹 Secure transport → <mark>**safe login**</mark>
- 🔹 Identity → <mark>**permission decisions**</mark>
- 🔹 Authentication <mark>**always** comes **<u>before</u>**</mark> authorization

---

## 11. Conclusion

You now clearly understand:

- ✅ What authentication is
- ✅ Why your Student API needs it
- ✅ Why we don't jump directly into JWT code

📌 In the next lesson, we will finally open the JWT box and answer:

🔐 **What is a JWT and how does it represent a student identity?**

---

## 📌 Summary

| Concept            | Meaning                                              |
| ------------------ | ---------------------------------------------------- |
| **Authentication** | Proves *who* the caller is                           |
| **Authorization**  | Decides *what* the caller can do                     |
| **Stateless**      | The server remembers nothing between requests        |
| **Token**          | Carries identity in every request                    |
| **JWT**            | The token format this course uses                    |
| **New Endpoints**  | `POST /api/Auth/Register`, `POST /api/Auth/Login`    |
| **Rule**           | Authentication always comes **before** authorization |

**Core principle:** HTTPS <mark>protects the ***channel***</mark>; CORS <mark>restricts ***browser origins***</mark>; authentication <mark>**<u>identifies the *user*</u>**</mark>. Without identity, permissions <mark>**<u>cannot exist</u>**</mark> because the API <mark>**has no one** to grant them to</mark>.

---
