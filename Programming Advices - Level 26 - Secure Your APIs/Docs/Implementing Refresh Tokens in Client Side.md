# Implementing Refresh Tokens in Client Side

---

## 1. Introduction

Your old console client only stores one token (the access token) and never refreshes it.

Now that your API <mark>returns **AccessToken + RefreshToken**</mark>, the console client <mark>**must implement**</mark> the full <mark>**<u>client-side refresh flow</u>**</mark>.

This lesson <mark>is about the **client's responsibilities**</mark> in the refresh token system.

---

## 2. The 6 Key Steps

### 2.1 Store Both Tokens

After a successful login, save both:

- ✅ `AccessToken`
- ✅ `RefreshToken`

The client now <mark>has **<u>two</u> credentials**</mark>, not one.

### 2.2 Use `AccessToken` for API Calls

Every protected request <mark>**must include**</mark> the access token in the header:

```http
Authorization: Bearer <AccessToken>
```

The refresh token <mark>is **never** used</mark> for normal API calls.

### 2.3 Handle `401 Unauthorized` Responses

If any API call returns `401 Unauthorized`, this signals:

- 🔹 The access token <mark>is **expired**</mark>, or
- 🔹 The access token <mark>is **invalid**</mark>

The client <mark>must **not** **<u>immediately</u>** log the user out</mark>. Instead, it <mark>**must attempt a refresh**</mark>.

### 2.4 Call the Refresh Endpoint

When `401` is detected, the client <mark>**<u>automatically</u>**</mark> calls:

```http
POST /api/Auth/refresh

Body:
{
  "email": "<user's email>",
  "refreshToken": "<current RefreshToken>"
}
```

This is <mark>the **only** endpoint</mark> that accepts a refresh token.

### 2.5 Rotate Tokens

If the refresh succeeds, the API returns:

```json
{
  "accessToken": "new.jwt.access",
  "refreshToken": "new.refresh.token"
}
```

The client <mark>**replaces** both</mark> tokens in its storage:

- 🔹 The old access token is discarded
- 🔹 The old refresh token is discarded (rotated)
- 🔹 The new ones are stored for future calls

📌 This <mark>is **refresh token rotation**</mark> in action — <mark>on **<u>the client side</u>**</mark>.

### 2.6 Retry the Failed API Call

After successful renewal, the client retries the **original** request once, using the new access token.

If the retry **also** returns `401`, the refresh flow has failed — the client <mark>should **log out**</mark> and require the user to re-authenticate.

---

## 3. The Full Flow (Visual)

```text
1. Login → receive AccessToken + RefreshToken → store both
                  │
                  ▼
2. Make API call with AccessToken
                  │
                  ▼
             ┌─────────┐
             │ 200 OK? │
             └────┬────┘
                  │ Yes → done ✅
                  │
                  │ No (401)
                  ▼
3. Call /refresh with RefreshToken
                  │
                  ▼
        ┌─────────────────────┐
        │ Refresh succeeded?  │
        └──────────┬──────────┘
                   │ Yes                     No
                   ▼                          ▼
4. Replace both tokens            Log out / require re-login
                   │
                   ▼
5. Retry the original request with new AccessToken
                   │
                   ▼
        ┌─────────────────────┐
        │ Still 401?          │
        └──────────┬──────────┘
                   │ Yes                     No
                   ▼                          ▼
             Log out ✅                     Done ✅
```

---

## 4. What the Client <mark>Must Never Do</mark>

- ❌ **Never use the refresh token <mark>for normal API calls</mark>.** It belongs only in `/refresh`.
- ❌ **Never <mark>retry more than once</mark>.** A retry loop that keeps refreshing is a bug (or a sign of a broken token).
- ❌ **Never store the refresh token <mark>in an insecure place</mark>** (in a browser, use `HttpOnly` cookies; in a console app, in memory is acceptable).
- ❌ **Never <mark>silently ignore a `401`</mark> after refresh.** <mark>**Force**</mark> the user to log in again.
- ❌ **Never <mark>reuse an old</mark> refresh token.** It <mark>was **<u>rotated</u>**</mark> — using it <mark>would trigger **theft detection**</mark> on the server.

---

## 5. Why This Matters (<mark>**<u>The Value of the Lesson</u>**</mark>)

Without this client-side flow:

- The user <mark>would be **logged out <u>every</u>**</mark> 30 minutes.
- The refresh token would exist on the server but <mark>**never be used**</mark>.
- The entire "silent renewal" design <mark>would be **dead code**</mark>.

> 📌 **The refresh token system <mark>only works</mark> if the client implements <mark>the renewal loop</mark>.** The server <mark>**provides** the tokens</mark>; the client <mark>**is responsible** for **using them <u>correctly</u>**</mark>.

---

## 6. Characteristics

- 🧬 Client stores both tokens
- 🧬 Client sends the access token on every request
- 🧬 Client intercepts `401` and calls `/refresh`
- 🧬 Client rotates both tokens on success
- 🧬 Client retries the failed request exactly once
- 🧬 Client forces re-login if refresh fails

---

## 7. Interconnection

- 🔗 Login → returns both tokens → client stores them
- 🔗 Access token → sent on every API call
- 🔗 `401` → triggers refresh
- 🔗 Refresh endpoint → issues new tokens
- 🔗 Rotation → old tokens become useless
- 🔗 Retry → uses the new access token
- 🔗 Failed refresh → forces logout

### 🛠️ Summary of Interconnections

- 🔹 Server issues tokens → client must manage them
- 🔹 Server enforces expiration → client must renew
- 🔹 Server rotates refresh tokens → client must replace them
- 🔹 Server revokes on logout → client must discard them
- 🔹 Both sides must cooperate for the flow to work

---

## 📌 Summary

| Step | Action                                     |
| ---- | ------------------------------------------ |
| 1    | Store both tokens after login              |
| 2    | Use AccessToken for every API call         |
| 3    | On `401`, do not log out — try refresh     |
| 4    | Call `/api/Auth/refresh` with RefreshToken |
| 5    | Replace both tokens with the new ones      |
| 6    | Retry the original request once            |

**Core principle:** the server <mark>**is only <u>half</u>**</mark> of the refresh token system. The client must implement <mark>the **renewal loop**</mark> — detect `401`, refresh, rotate, retry. Without it, the server's refresh tokens are just unused data. With it, users <mark>stay **logged in <u>silently</u>**</mark> while the server retains full control over revocation and rotation.

---












































































































