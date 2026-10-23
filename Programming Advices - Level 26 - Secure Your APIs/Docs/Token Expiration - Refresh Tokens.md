# Token Expiration & Refresh Tokens

---

## 1. 🗝️ Introduction

So far in this course, your Student API does something very important:

- It authenticates users using JWT
- It enforces identity and permissions
- It protects endpoints correctly

At this stage, many developers <mark>**think**</mark>:

> "Authentication is done."

But in real systems, **<mark>authentication <u>is not</u> a one-time event. It is <u>a lifecycle</u>.</mark>**

<img src="https://uploads.teachablecdn.com/attachments/5119f10c60bc4409bd971a2ab10b52aa.png" title="" alt="" width="310">

This lesson explains <mark>**why** tokens **<u>must expire</u>**</mark>, <mark>**what** problem refresh tokens **<u>solve</u>**</mark>, and <mark>**how** logout **works in <u>stateless APIs</u>**</mark> — **before writing any code**.

---

## 2. 🔍 Why This Lesson Exists

JWT authentication works — but without expiration and refresh strategy, <mark>it **<u>becomes dangerous</u>**</mark>.

**The core problems:**

- JWTs are **<mark>self-contained</mark>**
- Once issued, they are valid **<mark>until they expire</mark>**
- The server does **<mark>not track sessions</mark>**

This leads to <mark>**<u>two risks</u>**</mark>:

- 🔹 Long-lived tokens **are <u>dangerous</u>**
- 🔹 Short-lived tokens **hurt <u>usability</u>**

> This lesson <mark>**exists to solve that tension**</mark>.

---

## 3. ❓ Why JWTs Must Expire

<mark>**Every**</mark> JWT has an expiration time (`exp` claim).

**This <mark><u>is not</u> optional</mark>.**

**Why expiration is mandatory:**

- Tokens **<mark>can be stolen</mark>** (browser, logs, memory, proxy)
- Tokens **<mark>can be leaked</mark>** accidentally
- Tokens <mark>**<u>cannot</u> be "revoked"**</mark> easily in stateless systems

If a token <mark>**<u>never expires</u>**</mark>:

- Anyone holding it <mark>has **<u>permanent access</u>**</mark>
- Logout <mark>becomes **<u>meaningless</u>**</mark>
- Stolen tokens <mark>can be **<u>reused forever</u>**</mark>

> 📌 **Expiration <mark>limits damage</mark>.**
> It <mark>**does not**</mark> prevent theft — it <mark>limits **how long**</mark> theft is useful.

---

## 4. ⚠️ The Problem with Long-Lived Tokens

Imagine issuing a JWT that lasts:

- 30 days
- 90 days
- Or "never expires"

**What happens if:**

- A student logs in on a shared computer?
- A token is copied from browser storage?
- A token appears in logs or error messages?

The attacker <mark>does **not** need</mark>:

- Password
- Login access
- Brute-force

They <mark>**<u>already have access</u>**</mark>.

> 📌 **Long-lived tokens <mark>turn one mistake into <u>long-term</u> compromise</mark>.**

---

## 5. 😣 The Problem with Very Short-Lived Tokens

Now imagine the opposite:

- Token expires every 5 minutes

Security is improved — <mark>**<u>but</u>**</mark>:

- Users are logged out **<u>frequently</u>**
- Applications break when tokens expire
- Clients **<u>must re-login</u>** constantly

This creates a **usability problem**.

> 📌 **<mark>This is <u>the core tradeoff</u></mark>: Security vs usability.**

---

## 6. 🧩 The Concept of Refresh Tokens

Refresh tokens exist to solve this exact problem.

<img title="" src="https://uploads.teachablecdn.com/attachments/1563a2a7f44f4b56859a9814a085eb30.png" alt="" width="368">

**<mark>The idea <u>is simple</u></mark>:**

- Use **short-lived access tokens**
- Use **longer-lived refresh tokens**
- **<mark><u>Never</u></mark>** use refresh tokens to access APIs directly

### 6.1 Roles of Each Token

**Access token:**

- Short lifespan
- Sent with every API request
- <mark>**If stolen**</mark> → limited damage

**Refresh token:**

- Long lifespan
- Used only to request a new access token
- Stored more securely
- <mark>**Rotated and controlled**</mark>

> 📌 **Access token = <mark><u>permission</u> to act</mark>.**
> 📌 **Refresh token = <mark><u>ability</u> to renew permission</mark>.**

**Think of it like this:**

- 🔑 **Access token** → "I may enter this room"
- 🧾 **Refresh token** → "I may request a new key"

---

## 7. 🧠 What "Accessing APIs Directly" Means

Accessing an API directly means calling business endpoints like:

- 🔹 `GET /students`
- 🔹 `POST /orders`
- 🔹 `PUT /profile`
- 🔹 `DELETE /courses/5`

These endpoints <mark>**do real work**</mark> and return real data.

**Normally, you access them like this:**

```http
Authorization: Bearer <ACCESS_TOKEN>
```

### 7.1 ❌ What You Must NEVER Do

You <mark>**<u>must never</u>**</mark> send a refresh token like this:

```http
Authorization: Bearer <REFRESH_TOKEN>   ❌
```

To:

- 🔹 `/students`
- 🔹 `/orders`
- 🔹 `/payments`
- 🔹 `/anything-business-related`

That's what "use refresh token to access APIs directly" means — and <mark>it is **wrong by design**</mark>.

### 7.2 ✅ What Refresh Tokens Are Allowed to Do

A refresh token <mark>has **<u>ONE</u> job <u>only</u>**</mark>:

> 🔁 <mark>**Request**</mark> a new access token

**Example:**

```js
POST /auth/refresh

Body:
{
  "refreshToken": "xyz..."
}
```

That's it.

- No data access
- No business logic
- No permissions

### 7.3 Why This Rule Exists (The Core Reason)

🔥 **Refresh tokens <mark>are powerful</mark>.**
They live <mark>**<u>much longer</u>**</mark> than access tokens.

**If you allow them to access APIs:**

- ❌ An attacker **gets** long-term access
- ❌ Token expiration becomes **meaningless**
- ❌ Your system becomes **impossible to contain**

> That **<mark>would destroy</mark>** the whole security model.

---

## 8. 🛡️ The Security Separation (Very Important)

**Think of it like this:**

- 🔑 **Access token** → "I may enter this room"
- 🧾 **Refresh token** → "I may request a new key"

**You never open doors with the receipt.**

### 8.1 Why Access Tokens CAN Be Used Everywhere

- 🔹 Short lifespan
- 🔹 Limited damage if stolen
- 🔹 <mark>**Designed**</mark> for frequent exposure
- 🔹 Safe to send on every request

<mark>That's **why**</mark> middleware accepts **<mark>only</mark>** access tokens.

### 8.2 Why Refresh Tokens Must Be Protected

- 🔹 Long lifespan
- 🔹 Rarely used
- 🔹 Stored more securely
- 🔹 <mark>**Rotated and revoked**</mark>

They are <mark>not meant **to travel**</mark> around your API.

---

## 9. 🧠 One-Sentence Mental Model (Remember This)

> 📌 **Access token = <mark>permission <u>to act</u></mark>**
> 📌 **Refresh token = <mark>permission <u>to renew permission</u></mark>**

Or even simpler:

> **Access tokens <mark>do work</mark>. Refresh tokens only <mark>ask for new tools</mark>.**

---

## 10. 🔁 What Is Refresh Token Rotation?

<img src="https://uploads.teachablecdn.com/attachments/ff89603a6810441395e810cbc6be7a34.png" title="" alt="" width="434">

Refresh token rotation means:

- Every time a refresh token is used
- <mark>It **is replaced**</mark> with a new one
- The old one <mark>**becomes invalid**</mark>

**Why this matters:**

- If a refresh token is stolen and reused
- The system <mark>**can detect reuse**</mark>
- The session <mark>**can be invalidated**</mark>

> 📌 **Rotation <mark>turns silent theft into <u>a detectable event</u></mark>.**

---

## 11. 🚪 How Logout Works in Stateless Systems

**In traditional session-based systems:**

<img src="https://uploads.teachablecdn.com/attachments/d31d4ab0f8084d2da44b04e764f5eb78.png" title="" alt="" width="470">

- Logout = delete session from server

**In JWT-based systems:**

- Server does not store sessions
- Tokens are already issued

**So <mark>logout means</mark>:**

- <mark>**<u>Invalidate</u>**</mark> refresh token
- Let access token <mark>**<u>expire naturally</u>**</mark>

> 📌 **Logout <mark>is not about <u>deleting</u></mark> tokens.**
> 📌 **It <mark>is about <u>cutting</u></mark> the renewal path.**

---

## 12. 🔁 How a Refresh Token Renews an Access Token (Clear, Practical Flow)

Here's <mark>**the exact mechanics**</mark> of how refresh tokens work in real systems — no magic, <mark>just **a controlled exchange**</mark>.

<img title="" src="https://uploads.teachablecdn.com/attachments/b6064d8a7cc3489ba13299b55637153f.png" alt="" width="403">

### 🧠 The One-Sentence Idea

When the access token expires, the client presents a refresh token to a special endpoint, and the server issues a brand-new access token (<mark>and **usually**</mark> a new refresh token).

### 12.1 1️⃣ Access Token Expires

The client makes an API call <mark>with **an <u>expired</u> access token**</mark>.

- 🔹 Server checks `exp`
- 🔹 Token is expired
- 🔹 Server responds: `401 Unauthorized`

> This <mark>is **expected and healthy**</mark>.

### 12.2 2️⃣ Client Calls the Refresh Endpoint

Instead of asking the user to log in again, the client <mark>sends **<u>the refresh token</u>**</mark> to a **single, dedicated endpoint**:

```js
POST /auth/refresh

Payload (or cookie, depending on your design):

refreshToken = "abc123..."
```

> 🚫 This endpoint <mark>does **not**</mark> access business data.

### 12.3 3️⃣ Server Validates the Refresh Token

The server performs strict checks:

- 🔹 Is the refresh token known (exists in storage)?
- 🔹 Is it not expired?
- 🔹 Is it not revoked?
- 🔹 Has it not been used before (<mark>**rotation check**</mark>)?

> If any check fails → ❌ <mark>**<u>deny and force re-login</u>**</mark>.

### 12.4 4️⃣ Server Issues New Tokens

**If validation succeeds:**

- 🔹 ✅ Create <mark>**a new**</mark> **access token** (<mark>**short-lived**</mark>)
- 🔁 Create <mark>**a new**</mark> **refresh token** (<mark>**rotation**</mark>)
- ❌ <mark>**Invalidate** the **old**</mark> refresh token

**Response:**

```json
{
  "accessToken": "new.jwt.access",
  "refreshToken": "new.refresh.token"
}
```

> 📌 **This <mark>is <u>silent re-authentication</u></mark>.**

### 12.5 5️⃣ Client Stores New Tokens

The client <mark>**replaces** the old tokens</mark>:

- 🔹 **Access token** → used for API calls
- 🔹 **Refresh token** → stored securely for the next renewal

> <mark>The user notices **<u>nothing</u>**</mark> — no login screen.

### 12.6 🔐 Why This Is Secure

- 🔹 Access tokens expire quickly → <mark>**limited** damage</mark>
- 🔹 Refresh tokens are rarely sent → <mark>reduced **exposure**</mark>
- 🔹 Rotation <mark>**detects**</mark> token theft
- 🔹 Logout = <mark>**revoke**</mark> refresh token → <mark>**renewal**</mark> stops

---

## 13. 📌 Two Rules to Remember (<mark>**Golden Rules**</mark>)

📌 **Access tokens do work (API calls)**
📌 **Refresh tokens renew work permission**

Or simply:

> **Access token opens doors. Refresh token asks for a new key.**

---

## 14. 🏁 Conclusion

Your API already knows:

- Who the user is
- What they can do

Now it starts learning:

- How long trust should last
- How to recover safely
- How to limit damage when things go wrong

This is the first step into **real production security thinking**.

---

## 📌 Summary

| Token             | Lifespan          | Used For                  | Sent With               |
| ----------------- | ----------------- | ------------------------- | ----------------------- |
| **Access token**  | Short (5–15 min)  | API requests              | Every request           |
| **Refresh token** | Long (days/weeks) | Getting new access tokens | Only to `/auth/refresh` |

| Concept           | Meaning                                                |
| ----------------- | ------------------------------------------------------ |
| **Expiration**    | Limits damage if token is stolen                       |
| **Refresh token** | Enables renewal without re-login                       |
| **Rotation**      | Detects stolen refresh tokens                          |
| **Logout**        | Kills the refresh token, not the access token          |
| **Renewal flow**  | Expired access → refresh → new tokens → silent re-auth |

**Core principle:** the access token <mark>is **short-lived <u>on purpose</u>**</mark> — it <mark>**limits**</mark> the damage if it leaks. The refresh token <mark>is **long-lived** on **<u>purpose</u>**</mark> — it <mark>enables **silent renewal**</mark>. The two <mark>must **never**</mark> be confused. Access tokens do work; refresh tokens only ask for new tools.

---
