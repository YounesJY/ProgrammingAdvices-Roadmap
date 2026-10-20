# Why We Use POST for the Login Endpoint

---

## 1. Introduction

When designing the authentication endpoint, we chose:

```js
POST /api/Auth/login
```

But why not `GET`? The credentials have to go somewhere, and the choice of HTTP method is not cosmetic — <mark>it **directly affects** security, caching, and semantics</mark>.

This MD answers one question: **<mark>why POST and nothing else</mark>.**

---

## 2. The Three Core Reasons

### 2.1 Passwords Must Not Travel in the URL

- **GET** <mark>sends data in the **URL**</mark> (query string), e.g.:
  
  ```js
  GET /api/Auth/login?email=ali@student.com&password=password1
  ```
- <mark>URLs **<u>are logged in</u>**</mark>:
  - Browser history
  - Server access logs
  - Proxies and load balancers
  - Referrer headers sent to third parties
- **POST** sends data in the **HTTP body**, <mark>which **is not logged** by default and **is encrypted**</mark> end-to-end by HTTPS.

📌 **<mark>A password in a URL <u>is a leaked password</u> — even over HTTPS</mark>**, because the URL itself <mark>**is often stored, forwarded, and shared**</mark>.

### 2.2 Login Changes State (It Is Not a Read)

- **GET** is defined by RFC 9110 as **safe** and **idempotent** — calling it repeatedly <mark>**must not change**</mark> server state.
- **POST** is for requests <mark>that **<u>create or modify state</u>**</mark>.

When a user logs in, the server:

- Verifies credentials
- Generates a signed JWT
- (Eventually) may issue a refresh token

That <mark>**is an active state change**</mark> — a **POST** operation, not a read.

### 2.3 Browsers and CDNs Cache GET Requests

- Browsers, proxies, and CDNs <mark>**cache**</mark> `GET` responses by default.
- If login were a `GET`, a shared computer or a caching proxy could serve a **cached token** to the wrong user.
- `POST` requests <mark>are **never cached** by default</mark>.

> 📌 <mark>Every login must be **a fresh, unique request**</mark>.

---

## 3. Comparison Table

| Aspect                      | GET                | POST                               |
| --------------------------- | ------------------ | ---------------------------------- |
| Data location               | URL (query string) | <mark>**HTTP body**</mark>         |
| Visible in logs             | ✅ Yes              | ❌ <mark>**No**</mark> (by default) |
| Cached by browser/proxy     | ✅ Yes              | ❌ <mark>**No**</mark>              |
| Safe (read-only)            | ✅ Yes              | ❌ No                               |
| Intended for authentication | ❌ Never            | ✅ <mark>**Yes**</mark>             |
| Passwords safe              | ❌ No               | ✅ Yes (with HTTPS)                 |

---

## 4. What the Wrong Choice Looks Like

A `GET` login would look like this:

```http
GET /api/Auth/login?email=ali@student.com&password=password1 HTTP/1.1
Host: studentapi.com
```

<mark>**Risks introduced**</mark>:

- The password is now in **<mark>browser history</mark>**.
- It is in the **<mark>server's access log</mark>** (`/var/log/nginx/access.log`).
- It appears in **<mark>any proxy</mark>** between the client and the server.
- It may leak via the **`Referer` header** if the login page links anywhere.
- The response (a JWT) <mark>may be **cached**</mark> by a shared proxy.

A `POST` login:

```http
POST /api/Auth/login HTTP/1.1
Host: studentapi.com
Content-Type: application/json

{
  "email": "ali@student.com",
  "password": "password1"
}
```

Risks introduced:

- The password <mark>is in the **body**</mark>, not the URL.
- The body <mark>is encrypted</mark> by HTTPS and <mark>**not logged** by default</mark>.
- The response <mark>is **not cached**</mark>.

---

## 5. <mark>Rule of Thumb</mark>

- **GET** → retrieve data, no side effects, safe to cache.
- **POST** → create, submit, authenticate, change state.
- **If you would not paste it into your browser address bar, it should not be in a URL.**

Passwords, tokens, API keys, credit card numbers — <mark>all of these belong in a **POST body**</mark>, <mark>**<u>never</u>**</mark> in a query string.

---

## 6. Characteristics

- 🧬 Sensitive data never travels in the URL.
- 🧬 Login changes state (token issuance).
- 🧬 POST bypasses browser/proxy caching.
- 🧬 Matches REST semantics and HTTP standards.
- 🧬 The wrong method silently leaks credentials.

---

## 7. Interconnection

- 🔗 HTTPS → encrypts the POST body.
- 🔗 POST → hides credentials from logs and URLs.
- 🔗 JWT → is generated as a result of the POST.
- 🔗 GET → retrieves data later, using the JWT from the `Authorization` header.

### 🛠️ Summary of Interconnections

- 🔹 HTTPS makes POST body unreadable in transit.
- 🔹 POST keeps credentials out of URLs and caches.
- 🔹 Authentication is a state change → therefore POST.
- 🔹 Later resource calls use GET/PUT/DELETE with the JWT attached.

---

## 8. Conclusion

We use **POST** for `/api/Auth/login` because:

- ✅ It keeps passwords out of the URL.
- ✅ It matches the semantic meaning of login (state change).
- ✅ It bypasses browser and proxy caching.
- ✅ It aligns with standard HTTP and REST conventions.

📌 **A login is <mark>a request <u>to create a session</u></mark> — not a request to read a page. <mark>POST is not an option here</mark>; <mark>it is <u>the only correct choice</u></mark>.**

---

## 📌 Summary

| Question                | Answer                                                               |
| ----------------------- | -------------------------------------------------------------------- |
| Which method for login? | `POST`                                                               |
| Why not `GET`?          | Passwords would leak in URLs and logs                                |
| Does POST change state? | Yes — issues a JWT                                                   |
| Does POST cache?        | No                                                                   |
| Is POST safe over HTTP? | No — always pair with HTTPS                                          |
| Rule of thumb           | If it would not go in a browser address bar, it does not go in a URL |

**Core principle:** the HTTP method is a contract. `GET` <mark>promises **safe reads**</mark>. `POST` <mark>promises **state changes**</mark>. Sending a password through `GET` <mark>**<u>breaks the contract</u>**</mark> — and once broken, no amount of HTTPS fixes <mark>**<u>the leak</u>** in your logs</mark>.

---


