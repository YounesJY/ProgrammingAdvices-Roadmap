# CORS for Beginners

**<mark><u>Who Is Allowed</u> to Call Your API (and Why Browsers Care)</mark>**

---

## 1. Introduction

Now that your API uses HTTPS, data is encrypted while traveling.

But here is a surprising truth:

- 🔐 HTTPS protects data
- 🚧 CORS <mark>**controls access <u>from browsers</u>**</mark>

These are **<mark>different problems</mark>**.

<img title="" src="https://uploads.teachablecdn.com/attachments/c92d87628fc148c8a078631dd488dcd4.png" alt="" width="450">

An API can be:

- **<u>Perfectly encrypted</u>**
- <mark>**<u>Completely open to abuse</u>**</mark>

🎯 **Outcome**

By the end of this lesson, you will understand:

- ✅ What CORS really is
- ✅ Why it exists
- ✅ When it applies (and when it doesn't)
- ✅ How browsers enforce it
- ✅ Why backend developers must care

---

## 2. What Is CORS? (Simple Definition)

**CORS** stands for **<mark>Cross-Origin Resource Sharing</mark>**.

In simple words:

> CORS <mark>**is <u>a browser</u> security rule**</mark> that <mark>**decides** which websites **are allowed**</mark> to call your API.

> 📌 **CORS is <mark><u>enforced by browsers</u></mark>, not by servers.**

---

## 3. What Is an "Origin"?

<img src="https://uploads.teachablecdn.com/attachments/4e7c05e924c142858619821ffdc4df80.png" title="" alt="" width="526">

An origin is defined by three things together:

- 🔹 Protocol (`http` / `https`)
- 🔹 Domain (`example.com`)
- 🔹 Port (if any)

**Example origins:**

```js
https://localhost:3000
https://studentportal.com
https://studentportal.com:8080
```

> 📌 <mark>If **any part** changes</mark>, <mark>it is a **different origin**</mark>.

---

## 4. The Problem CORS Solves

Imagine this situation:

- You are logged in to `bank.com`
- You <mark>**visit a malicious website**</mark>: `evil-site.com`
- That website <mark>**secretly calls**</mark>:

```js
GET https://bank.com/api/account
```

**Without protection:**

- ❌ Your browser **<u>would send the request</u>**
- ❌ Cookies/tokens **<u>might be included</u>**
- ❌ Private data **<u>could be read</u>**

> 📌 This attack is called **<mark><u>Cross-Site Request Abuse</u></mark>**.

---

## 5. Why Browsers Enforce CORS

<mark>Browsers protect <u>**users**</u>, not servers</mark>.

So browsers ask:

> "Is this website <mark>**allowed to**</mark> read data from that API?"

If not:

- ❌ Browser **<u>blocks the response</u>**
- ❌ <mark>**<u>JavaScript</u>**</mark> cannot access the data

> 📌 The API <mark>**<u>may still respond</u>**</mark> — <mark>but **<u>the browser</u> hides it**</mark>.

---

## 6. Very Important Clarification (<mark>**<u>Critical</u>**</mark>)

🚨 **<mark>CORS <u>is NOT an API security feature</u> by itself.</mark>**

CORS:

- ✅ Restricts <mark>**<u>browser-based JavaScript</u>**</mark>
- ❌ Does **<u>NOT stop Postman</u>**
- ❌ Does **<u>NOT stop curl</u>**
- ❌ Does **<u>NOT stop backend-to-backend</u>** calls

> 📌 **CORS <mark>is about <u>browser access</u></mark>, not total security.**

---

## 7. What Does "CORS Does NOT Stop curl" Mean?

### 🔹 What is curl?

`curl` is **<u>a command-line tool</u>** used to send HTTP requests **<u>directly to an API</u>**.

📌 Think of it as:

> "<mark>A browser **<u>without browser rules</u>**</mark>."

**Example:**

```bash
curl https://api.example.com/students
```

This request:

- ❌ <mark>**Is NOT coming from a browser**</mark>
- ❌ Has NO JavaScript environment
- ❌ Has NO CORS enforcement

### 🔍 Why CORS Does NOT Stop curl

🚧 **<mark>CORS is enforced <u>by browsers only</u>.</mark>**

CORS is a browser safety rule, <mark>**<u>not a server firewall</u>**</mark>.

<img title="" src="https://uploads.teachablecdn.com/attachments/7dc7372198204f9f9f354bd867b07b51.png" alt="" width="562">

<img title="" src="https://uploads.teachablecdn.com/attachments/fe47ff73a4e04bca8b33824267558e8c.png" alt="" width="566">

---

## 8. When Does CORS Apply?

CORS applies <mark>when **ALL** of these are true</mark>:

- ✅ Request comes **<u>from a browser</u>**
- ✅ **<u>JavaScript is making the request</u>**
- ✅ Origin **<u>is different</u>**

**Examples where CORS applies:**

- React app → API
- Angular app → API
- Vue app → API

**Examples where CORS does NOT apply:**

- Postman
- curl
- Mobile apps
- Backend services

---

## 9. Same-Origin vs Cross-Origin

### 🟢 Same-Origin

```js
Frontend: https://app.com
API:      https://app.com
```

✅ Allowed automatically.

### 🔴 Cross-Origin

```
Frontend: https://app.com
API:      https://api.com
```

🚧 Browser asks for permission → CORS.

---

## 10. How CORS Works (High-Level Flow)

1️⃣ Browser sends request
2️⃣ API <mark>**responds with CORS headers**</mark>
3️⃣ Browser <mark>**checks origin**</mark>
4️⃣ Browser decides:

- ✅ Allow JavaScript access
- ❌ Block JavaScript access

> 📌 <mark>The **server** **<u>declares</u>** rules</mark>.
> 📌 <mark>The **browser** **<u>enforces</u>** them</mark>.

---

## 11. Common CORS Headers (Conceptual)

- `Access-Control-Allow-Origin`
- `Access-Control-Allow-Methods`
- `Access-Control-Allow-Headers`

> 📌 These headers <mark>tell the browser **<u>what is allowed</u>**</mark>.

---

## 12. Mental Model (Very Important)

### 🏢 CORS = Building Security Desk

- Anyone can walk near the building (API is public)
- But <mark>**only approved visitors**</mark> can enter certain rooms
- The security guard <mark>**checks where you came from**</mark>

> 📌 The guard does NOT stop thieves outside — <mark>**it controls who is allowed**</mark> inside from the lobby (browser).

---

## 13. Common Beginner Myths

- ❌ "CORS secures my API"
- ❌ "If CORS blocks it, it's safe"
- ❌ "Postman not blocked = CORS broken"

📌 **All false.**

---

## 14. What CORS Is NOT

- ❌ Authentication
- ❌ Authorization
- ❌ Rate limiting
- ❌ Attack prevention

📌 CORS is **access control for browsers only**.

---

## 15. Why Backend Developers Must Understand CORS

Because:

- 🔹 Frontend apps **<u>depend on it</u>**
- 🔹 Wrong configuration **<u>breaks apps</u>**
- 🔹 Overly open CORS **<u>exposes users</u>**
- 🔹 <mark>**Misunderstanding leads <u>to false security</u>**</mark>

> 📌 CORS mistakes <mark>**<u>are extremely common</u>**</mark>.

---

## 16. Interconnection

- 🔗 HTTPS → <mark>**encrypts transport**</mark>
- 🔗 CORS → <mark>**controls browser access**</mark>
- 🔗 Authentication → **<u>identifies users</u>**
- 🔗 Authorization → **<u>controls actions</u>**

> 📌 Each layer <mark>**solves <u>a different problem</u>**</mark>.

### 🛠️ Summary of Interconnections

- 🔹 HTTPS protects data in transit
- 🔹 CORS protects users from malicious websites
- 🔹 Authentication proves identity
- 🔹 Authorization enforces rules
- 🔹 <mark>**Security <u>is layered</u>**</mark>, not single-feature

---

## 17. Conclusion

CORS is not magic. It is not optional. <mark>**It <u>is not security</u> by itself**</mark>.

🚧 <mark>CORS is a **browser safety mechanism**</mark>.

- ✅ It protects users
- ✅ It controls who can read API responses
- ❌ It does not secure your API alone

📌 From now on, remember this rule:

> **If the request <mark><u>does not come</u> from a browser</mark>, <mark><u>CORS does not apply</u></mark>.**

📌 **Next Lesson:** 👉 Configuring CORS in ASP.NET Core (Safely)

---

## 📌 Summary

| Aspect                 | CORS                                                                      |
| ---------------------- | ------------------------------------------------------------------------- |
| Stands for             | Cross-Origin Resource Sharing                                             |
| Enforced by            | <mark>**Browsers**</mark>                                                 |
| Protects               | <mark>Users</mark> of other sites from having their browser read your API |
| Applies when           | Browser + JavaScript + different origin                                   |
| Does NOT apply to      | Postman, curl, mobile apps, backend-to-backend                            |
| Configuration lives in | Response headers (`Access-Control-Allow-*`)                               |
| Guarantees             | Nothing about server-side security                                        |
| Common myth            | "CORS secures my API" — false                                             |
| Mental model           | 🏢 Building security desk — controls entry from the lobby, not the street |
| Golden rule            | <mark>No browser</mark> → <mark>**no CORS**</mark>                        |

**Core principle:** CORS <mark>is a **browser-enforced** policy</mark> that <mark>**decides which <u>origins</u>** may read responses</mark>. It is not authentication, not authorization, not rate limiting, and <mark>**not a server-side firewall**</mark>. Configure it to allow your legitimate frontends — nothing more. <mark>**Do not rely on it** **<u>to protect the API</u>** itself</mark>.

---



---

# Brief Explanation

**The core idea:**

1. **The browser stores cookies.**
   When you log into your bank, the browser saves the bank's cookies.

2. **Cookies are auto-attached to every request.**
   That's how cookies work — the browser sends them automatically to the domain that set them.

3. **The danger:**
   If a malicious site (`evil-site.com`) triggers a request to `bank.com`, your browser will **attach the bank's cookies automatically** — because it doesn't know the request was triggered by a different site.

4. **What CORS does:**
   CORS doesn't stop the request from being *sent*. It stops the malicious site from **reading the response**. The bank's server may still process it, but the browser hides the data from the attacker's JavaScript.

5. **<mark>Why Postman isn't affected</mark>:**
   Postman has no browser, no cookie jar tied to your logged-in session, and no origin. An attacker using Postman can't obtain your cookies in the first place. So CORS is irrelevant there — and that's fine, because there's nothing to protect in that scenario.

6. **Conclusion:**
   <mark>CORS exists to **protect the user's browser session**</mark>, <mark>not to protect the API from non-browser clients</mark>.

---

**One-line summary:**

> Cookies are auto-sent by the browser → a malicious site can trigger authenticated requests → CORS blocks the *response* from being read → Postman is unaffected because it never had your cookies.

**Correction worth noting:** the Arabic text says CORS prevents the malicious site "from reading the data *after sending* the request." That's accurate. CORS does **not** prevent the request from being sent. That distinction is exactly why CSRF (state-changing abuse) is a separate problem requiring separate defenses (CSRF tokens, `SameSite` cookies) — CORS alone cannot stop a write.

---
