# 📖 JWT Structure — Header, Payload & Signature Explained

---

## 1. Introduction

In the previous lesson, you learned what a JWT is and why it exists.

Now we answer the next important question:

🔍 **<mark>What is inside a JWT, and <u>why does each part matter</u>?</mark>**

By the end of this lesson, you will:

- ✅ Understand <mark>**every part**</mark> of a JWT
- ✅ Know <mark>**what information**</mark> belongs where
- ✅ Stop seeing JWT as a "black box"

Still:

- ❌ No middleware
- ❌ No configuration
- ❌ No coding yet

> <mark>**Understanding** comes **<u>before</u>**</mark> implementation.

---

## 2. The JWT Format (One Line)

<img src="https://uploads.teachablecdn.com/attachments/87a32295d6b140aaac67bc289346744e.png" title="" alt="" width="285">

A JWT always looks like this:

```
Header.Payload.Signature
```

Three parts, separated by dots.

Each part has a specific job.

<img src="https://uploads.teachablecdn.com/attachments/7bdfda4f9b394187a1d06233c010de6a.png" title="" alt="" width="377">

---

## 3. Part 1 — Header (How the Token Is Built)

📌 **<mark>What Is the Header?</mark>**

The Header describes<mark>**how** the token **<u>is created</u>**</mark>.

It usually contains:

- 🔹 The token type (`JWT`)
- 🔹 The signing algorithm (for example: `HS256`)

**Example (conceptual):**

```json
{
  "typ": "JWT",
  "alg": "HS256"
}
```

📌 **Meaning:**

- "This is a JWT"
- "This is how it was signed"

> ❗ The header <mark>does **not** contain</mark> user data.

---

## 4. Part 2 — Payload (Who the Student Is)

📌 **<mark>What Is the Payload?</mark>**

The Payload <mark>contains the <u>**claims**</u></mark>.

Claims are <mark>**pieces of information**</mark> about the user.

In your Student API, typical claims will be:

- 🔹 Student ID
- 🔹 Role (Student / Admin)
- 🔹 Token expiration time

**Example (conceptual):**

```json
{
  "studentId": 5,
  "role": "Admin",
  "exp": 1700000000
}
```

📌 This is the **identity card** part of the JWT.

### 4.1 Important Rule About Payload

The payload is:

- ❌ <mark>**Not encrypted**</mark>
- ✅ <mark>**<u>Only encoded</u>**</mark>

That means:

- Anyone can read it
- But <mark>**no one**</mark> can safely change it

📌 **Therefore:**

- ❌ <mark>**<u>Never</u>**</mark> store passwords
- ❌ <mark>**<u>Never</u>**</mark> store sensitive personal data

---

## 5. Part 3 — Signature (Why the Token Can Be Trusted)

📌 **<mark>What Is the Signature?</mark>**

The Signature proves that:

- 🔹 The token was created by your API
- 🔹 The payload was not modified

It is created using:

- 🔹 Header
- 🔹 Payload
- 🔹 A secret key (known only to the server)

📌 **<mark>If anything changes:</mark>**

- ❌ Signature validation fails
- ❌ API rejects the request

### 5.1 Why the Signature Is <mark>the Most Important Part</mark>

Without the signature:

- ❌ Anyone could change role from "Student" to "Admin"
- ❌ Anyone could impersonate another student

> 📌 **<mark><u>The signature prevents forgery</u></mark>.**

---

## 6. JWT Trust Flow (Very Important)

<img title="" src="https://uploads.teachablecdn.com/attachments/ced8284c127a450ab98b3adc0c9f907f.png" alt="" width="319">

When a request arrives with a JWT:

1️⃣ API reads the token
2️⃣ API checks the signature
3️⃣ API checks expiration
4️⃣ API extracts claims
5️⃣ API trusts the identity

> 📌 **<mark>If any step fails</mark> → request is rejected.**

---

## 7. Characteristics

- 🧬 Header = metadata
- 🧬 Payload = identity (claims)
- 🧬 Signature = trust
- 🧬 Payload is readable, not editable
- 🧬 Signature protects everything

---

## 8. Interconnection

- 🔗 Header defines how to verify
- 🔗 Payload carries identity
- 🔗 Signature ensures integrity

### 🛠️ Summary of Interconnections

- 🔹 Identity without signature → not trusted
- 🔹 Signature without payload → meaningless
- 🔹 All three parts work together

---

## 9. Conclusion

You now fully understand:

- ✅ The structure of a JWT
- ✅ The role of each part
- ✅ Why JWT is trusted

📌 You are now ready for real implementation.

In the next lesson, we will finally move from theory to practice and answer:

🛠️ **How do we generate a JWT for a student after login?**

---

## 📌 Summary

| Part          | Contains                                 | Job                                                        | Encrypted?                                                                   |
| ------------- | ---------------------------------------- | ---------------------------------------------------------- | ---------------------------------------------------------------------------- |
| **Header**    | `typ`, `alg`                             | Tells the API how the token was built and how to verify it | ❌ (<mark>**Base64 encoded only**</mark>)                                     |
| **Payload**   | Claims (student ID, role, `exp`)         | Carries identity                                           | ❌ (<mark>**Base64 encoded only**</mark> — <mark>**<u>readable</u>**</mark>!) |
| **Signature** | Hash of Header + Payload + server secret | Proves the token is authentic and untampered               | ✅ (Cryptographic)                                                            |

**Core principle:** the payload <mark><u>is **readable by anyone**</u></mark> — <mark>**never**</mark> store secrets in it. The signature is what makes the token <mark>**trustworthy**</mark>; without it, any claim (like `"role": "Admin"`) could be forged. All three parts <mark>**<u>must be present and valid</u>**</mark> for the API to accept the token.

---



---

> <mark>**Check**</mark> "https://share.google/WYRLNmg7JN9A8zJJz"

---
