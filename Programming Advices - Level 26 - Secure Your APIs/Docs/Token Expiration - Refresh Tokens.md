# Token Expiration & Refresh Tokens

---

## 1. 🗝️ Introduction

So far in this course, your Student API does something very important:

- It authenticates users using JWT
- It enforces identity and permissions
- It protects endpoints correctly

At this stage, many developers think:

> "Authentication is done."

But in real systems, **authentication is not a one-time event. It is a lifecycle.**

This lesson explains why tokens must expire, what problem refresh tokens solve, and how logout works in stateless APIs — **before writing any code**.

---

## 2. 🔍 Why This Lesson Exists

JWT authentication works — but without expiration and refresh strategy, it becomes dangerous.

**The core problems:**

- JWTs are **self-contained**
- Once issued, they are valid **until they expire**
- The server does **not track sessions**

This leads to two risks:

- 🔹 Long-lived tokens are dangerous
- 🔹 Short-lived tokens hurt usability

This lesson exists to solve that tension.

---

## 3. ❓ Why JWTs Must Expire

Every JWT has an expiration time (`exp` claim).

**This is not optional.**

**Why expiration is mandatory:**

- Tokens can be stolen (browser, logs, memory, proxy)
- Tokens can be leaked accidentally
- Tokens cannot be "revoked" easily in stateless systems

**If a token never expires:**

- Anyone holding it has permanent access
- Logout becomes meaningless
- Stolen tokens can be reused forever

📌 **Expiration limits damage.**
It does not prevent theft — it limits how long theft is useful.

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

The attacker does **not** need:

- Password
- Login access
- Brute-force

They already have access.

📌 **Long-lived tokens turn one mistake into long-term compromise.**

---

## 5. 😣 The Problem with Very Short-Lived Tokens

Now imagine the opposite:

- Token expires every 5 minutes

**Security is improved — but:**

- Users are logged out frequently
- Applications break when tokens expire
- Clients must re-login constantly

This creates a **usability problem**.

📌 **This is the core tradeoff: Security vs usability.**

---

## 6. 🧩 The Concept of Refresh Tokens

Refresh tokens exist to solve this exact problem.

**The idea is simple:**

- Use **short-lived access tokens**
- Use **longer-lived refresh tokens**
- **Never** use refresh tokens to access APIs directly

### 6.1 Roles of Each Token

**Access token:**

- Short lifespan
- Sent with every API request
- If stolen → limited damage

**Refresh token:**

- Long lifespan
- Used only to request a new access token
- Stored more securely
- Rotated and controlled

📌 **Access token = permission to act.**
📌 **Refresh token = ability to renew permission.**

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

These endpoints do real work and return real data.

**Normally, you access them like this:**

```http
Authorization: Bearer <ACCESS_TOKEN>
```

### 7.1 ❌ What You Must NEVER Do

You must never send a refresh token like this:

```http
Authorization: Bearer <REFRESH_TOKEN>   ❌
```

To:

- 🔹 `/students`
- 🔹 `/orders`
- 🔹 `/payments`
- 🔹 `/anything-business-related`

That's what "use refresh token to access APIs directly" means — and it is **wrong by design**.

### 7.2 ✅ What Refresh Tokens Are Allowed to Do

A refresh token has **ONE job only**:

> 🔁 Request a new access token

**Example:**

```http
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

🔥 **Refresh tokens are powerful.**
They live much longer than access tokens.

**If you allow them to access APIs:**

- ❌ An attacker gets long-term access
- ❌ Token expiration becomes meaningless
- ❌ Your system becomes impossible to contain

That would destroy the whole security model.

---

## 8. 🛡️ The Security Separation (Very Important)

**Think of it like this:**

- 🔑 **Access token** → "I may enter this room"
- 🧾 **Refresh token** → "I may request a new key"

**You never open doors with the receipt.**

### 8.1 Why Access Tokens CAN Be Used Everywhere

- 🔹 Short lifespan
- 🔹 Limited damage if stolen
- 🔹 Designed for frequent exposure
- 🔹 Safe to send on every request

That's why middleware accepts **only** access tokens.

### 8.2 Why Refresh Tokens Must Be Protected

- 🔹 Long lifespan
- 🔹 Rarely used
- 🔹 Stored more securely
- 🔹 Rotated and revoked

They are not meant to travel around your API.

---

## 9. 🧠 One-Sentence Mental Model (Remember This)

📌 **Access token = permission to act**
📌 **Refresh token = permission to renew permission**

Or even simpler:

> **Access tokens do work. Refresh tokens only ask for new tools.**

---

## 10. 🔁 What Is Refresh Token Rotation?

Refresh token rotation means:

- Every time a refresh token is used
- It is replaced with a new one
- The old one becomes invalid

**Why this matters:**

- If a refresh token is stolen and reused
- The system can detect reuse
- The session can be invalidated

📌 **Rotation turns silent theft into a detectable event.**

---

## 11. 🚪 How Logout Works in Stateless Systems

**In traditional session-based systems:**

- Logout = delete session from server

**In JWT-based systems:**

- Server does not store sessions
- Tokens are already issued

**So logout means:**

- Invalidate refresh token
- Let access token expire naturally

📌 **Logout is not about deleting tokens.**
📌 **It is about cutting the renewal path.**

---

## 12. 🔁 How a Refresh Token Renews an Access Token (Clear, Practical Flow)

Here's the exact mechanics of how refresh tokens work in real systems — no magic, just a controlled exchange.

### 🧠 The One-Sentence Idea

When the access token expires, the client presents a refresh token to a special endpoint, and the server issues a brand-new access token (and usually a new refresh token).

### 12.1 1️⃣ Access Token Expires

The client makes an API call with an expired access token.

- 🔹 Server checks `exp`
- 🔹 Token is expired
- 🔹 Server responds: `401 Unauthorized`

This is expected and healthy.

### 12.2 2️⃣ Client Calls the Refresh Endpoint

Instead of asking the user to log in again, the client sends the refresh token to a **single, dedicated endpoint**:

```http
POST /auth/refresh

Payload (or cookie, depending on your design):

refreshToken = "abc123..."
```

🚫 This endpoint does **not** access business data.

### 12.3 3️⃣ Server Validates the Refresh Token

The server performs strict checks:

- 🔹 Is the refresh token known (exists in storage)?
- 🔹 Is it not expired?
- 🔹 Is it not revoked?
- 🔹 Has it not been used before (rotation check)?

If any check fails → ❌ deny and force re-login.

### 12.4 4️⃣ Server Issues New Tokens

**If validation succeeds:**

- 🔹 ✅ Create a new **access token** (short-lived)
- 🔁 Create a new **refresh token** (rotation)
- ❌ Invalidate the **old** refresh token

**Response:**

```json
{
  "accessToken": "new.jwt.access",
  "refreshToken": "new.refresh.token"
}
```

📌 **This is silent re-authentication.**

### 12.5 5️⃣ Client Stores New Tokens

The client replaces the old tokens:

- 🔹 **Access token** → used for API calls
- 🔹 **Refresh token** → stored securely for the next renewal

The user notices **nothing** — no login screen.

### 12.6 🔐 Why This Is Secure

- 🔹 Access tokens expire quickly → limited damage
- 🔹 Refresh tokens are rarely sent → reduced exposure
- 🔹 Rotation detects token theft
- 🔹 Logout = revoke refresh token → renewal stops

---

## 13. 📌 Two Rules to Remember (Golden Rules)

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

**Core principle:** the access token is short-lived on purpose — it limits the damage if it leaks. The refresh token is long-lived on purpose — it enables silent renewal. The two must **never** be confused. Access tokens do work; refresh tokens only ask for new tools.
