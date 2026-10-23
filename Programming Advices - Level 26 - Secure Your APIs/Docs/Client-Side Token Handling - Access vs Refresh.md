# Client-Side Token Handling (Access vs. Refresh)

---

## 1. Introduction

<mark>**A common question**</mark> when learning JWT + Refresh Tokens is:

> "Do we <mark>receive **both** tokens</mark>? Do we <mark>**keep both**</mark>? Where do we keep them?"

Yes and yes. But **each token has a different role, <mark>a different place</mark>, and a different lifecycle <mark>on the client side</mark>.**

This MD explains the client's responsibilities in the refresh token flow.

---

## 2. What the Client Receives

When the client calls:

```http
POST /api/Auth/login
```

The API returns **both tokens**:

```json
{
  "accessToken": "eyJhbGciOi...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2g..."
}
```

📌 Both are received in the same response. <mark>Both **must be stored**</mark>.

---

## 3. Do We Keep Both? Yes — But Differently

The two tokens <mark>**are not**</mark> stored the same way, and <mark>for **good reasons**</mark>.

| Token             | Where It Lives                                                | Why                                                                    |
| ----------------- | ------------------------------------------------------------- | ---------------------------------------------------------------------- |
| **Access Token**  | Memory, or `localStorage`                                     | Short-lived; if stolen, damage is limited                              |
| **Refresh Token** | `HttpOnly` cookie (web) or Secure Enclave / Keychain (mobile) | Long-lived; <mark>**must be protected**</mark> from JavaScript and XSS |

### 3.1 Why the Access Token Is Less Protected

- It expires quickly (5–30 minutes).
- It is sent on every API call, so it must be easy to access.
- If it leaks, the attacker has a **very short window** to use it.

> 📌 **Short lifespan <mark>is itself</mark> a security measure.**

### 3.2 Why the Refresh Token Must Be Protected

- It lasts days or weeks.
- It is rarely used (only to renew).
- If it leaks, the attacker can keep getting new access tokens — **<mark><u>permanent compromise</u></mark>**.

> 📌 **The refresh token is the crown jewel. It <mark><u>must never</u> be readable by JavaScript</mark>.**

---

## 4. The Client's Lifecycle (Step by Step)

### 4.1 Login

1. Client calls `POST /api/Auth/login` with email + password.
2. Server returns **access token** + **refresh token**.
3. Client stores both:
   - <mark>**Access token**</mark> → memory or `localStorage`
   - <mark>**Refresh token**</mark> → `HttpOnly` cookie (web) or Keychain (mobile)

### 4.2 Normal API Calls

4. Client sends `GET /api/Students` with:

```http
Authorization: Bearer <access_token>
```

5. Server validates the token → `200 OK`.

### 4.3 Access Token Expires

6. Client calls `GET /api/Students` with an expired token.
7. Server returns `401 Unauthorized`.
8. Client **automatically** (interceptor / middleware):
- Grabs the refresh token
- Calls `POST /api/Auth/refresh`
- Receives a **new access token** (and a new refresh token if rotation is used)
9. Client replaces the old tokens in storage.
10. Client retries the original request. **The user sees nothing.**

> 📌 **This is <mark>silent re-authentication</mark>.**

### 4.4 Logout

11. Client calls `POST /api/Auth/logout`.
12. Server invalidates the **refresh token** (in the database).
13. Client <mark>**deletes** both</mark> tokens from storage.
14. The access token will expire naturally within 5–30 minutes.

> 📌 **<mark>Logout kills <u>the renewal path</u></mark>, <mark><u>not the current</u> access token</mark>.**

---

## 5. What the Client Must NEVER Do

- ❌ **Never store the refresh token in `localStorage` or a regular JS variable.** Any XSS can read it.
- ❌ **Never send the refresh token to normal API endpoints.** It goes only to `/auth/refresh`.
- ❌ **Never send the refresh token in the `Authorization` header.** Only the access token belongs there.
- ❌ **Never reuse a rotated refresh token** (if the server rotates). The old one is dead; using it triggers theft detection.
- ❌ **Never log the tokens.** Logs are a common leak source.

---

## 6. The Storage Rule of Thumb

| Storage              | Access Token                          | Refresh Token                        |
| -------------------- | ------------------------------------- | ------------------------------------ |
| **Browser**          | `localStorage` (acceptable) or memory | ✅ `HttpOnly` cookie only             |
| **Mobile app**       | Keychain / Keystore (shared)          | ✅ Keychain / Keystore (shared)       |
| **Server-to-server** | In-memory variable                    | In-memory variable (no user session) |
| **Never**            | ❌ In a URL                            | ❌ In `localStorage`                  |

📌 **`HttpOnly` means JavaScript cannot read the cookie. XSS has no access. This is the point.**

---

## 7. Characteristics

- 🧬 Both tokens are received from `/login`
- 🧬 Both are stored, but in different places
- 🧬 Access token = accessible, short-lived
- 🧬 Refresh token = protected, long-lived
- 🧬 Silent renewal happens on `401`
- 🧬 Logout = delete refresh token + let access token expire

---

## 8. Interconnection

- 🔗 `/login` → returns both tokens
- 🔗 Access token → sent on every API call
- 🔗 Refresh token → sent only to `/refresh`
- 🔗 `401` → triggers silent renewal
- 🔗 Logout → invalidates refresh token, deletes both
- 🔗 XSS → can read access token, **cannot** read `HttpOnly` refresh token

### 🛠️ Summary of Interconnections

- 🔹 The client's job is not just to hold tokens — it is to **protect them differently**
- 🔹 The access token is fast and disposable
- 🔹 The refresh token is slow, powerful, and guarded
- 🔹 The API decides **what the token grants**
- 🔹 The client decides **how the token is stored**

---

## 📌 Summary

| Question                                    | Answer                                                                  |
| ------------------------------------------- | ----------------------------------------------------------------------- |
| Does the client receive both tokens?        | ✅ Yes — in the login response                                           |
| Does the client keep both?                  | ✅ Yes — but in different storage                                        |
| Where is the access token stored?           | Memory or `localStorage`                                                |
| Where is the refresh token stored?          | `HttpOnly` cookie (web) or Keychain (mobile)                            |
| Can JavaScript read the refresh token?      | ❌ Never — it is in an `HttpOnly` cookie                                 |
| What happens when the access token expires? | Client silently calls `/refresh` and retries                            |
| What happens on logout?                     | Refresh token is invalidated; access token expires naturally            |
| What must never be done?                    | Store refresh token in `localStorage`, send it to API endpoints, log it |

**Core principle:** the client <mark>receives **two tokens**</mark>, but treats them as two completely different objects. The **access token** is <mark>**disposable and accessible**</mark>; the **refresh token** is <mark>**powerful and locked away**</mark>. Storing them the same way — in `localStorage`, in JavaScript variables, or in logs — <mark>defeats **the entire** security model</mark> the refresh flow <mark>**<u>was designed</u>** to provide</mark>.

---


