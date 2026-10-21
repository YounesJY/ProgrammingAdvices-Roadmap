# Why Token Revocation Makes JWT Stateful

---

## 1. Introduction

JWT is famous because it is **stateless**. This is one of its biggest selling points. But <mark>the moment you need to **revoke** a token</mark>, that statelessness <mark>**breaks**</mark> — and the system <mark>becomes **stateful** again</mark>.

This MD answers one question:

> **How does JWT become stateful when we add revocation?**

---

## 2. The Default JWT Behavior (Stateless)

In pure JWT authentication, the server <mark>remembers **nothing**</mark> about the tokens it issues.

### The flow:

1. User logs in with email + password.
2. The server verifies credentials and <mark>**signs** a token</mark>.
3. The server <mark>**forgets**</mark> the token was ever created.
4. On every request, the server:
   - Reads the token
   - Verifies <mark>the **mathematical signature**</mark>
   - <mark>Checks the `exp`</mark> (expiration) claim
   - Trusts the claims inside

> 📌 **There is <mark>no database lookup</mark>. There is <mark>no session store</mark>. There <mark><u>is no list</u></mark> of issued tokens.**

The server <mark>**<u>*does not ask*</u>**</mark>:

- ❓ "Did I issue this token?"
- ❓ "Is this token still valid?"
- ❓ "Did I revoke this token?"

It only asks:

- ✅ "Is the signature <mark>**mathematically**</mark> valid?"
- ✅ "Is the token still within its expiration window?"

If both are true → access granted.

> **That is <mark>what "stateless" means</mark>:** the server <mark>holds **zero state**</mark> about who is logged in.

---

## 3. The Revocation Problem

Now consider this scenario:

> An employee is fired. They still hold a valid token that expires in 45 minutes. They want to cause damage.

You want to <mark>revoke their access **immediately**</mark>. But you **cannot**:

- 🔹 The token is already signed.
- 🔹 The signature is still valid.
- 🔹 The `exp` claim is still in the future.
- 🔹 The server <mark>has **no record**</mark> of this token.

<mark>There is **no mechanism**</mark> to say *"This token is no longer trusted."*

> 📌 <mark><u>**By design**</u></mark>, JWT <mark>**has no revocation mechanism**</mark>.

---

## 4. The Workaround: Revocation via Blacklist

To revoke a token, <mark>you introduce a **blacklist**</mark>:

1. Assign each token <mark>**a unique ID**</mark> (the `jti` claim).
2. When a token is revoked, store its `jti` <mark>in a **fast store**</mark> (Redis, database, memory).
3. On **every single request**, <mark><u>**before trusting**</u> the token</mark>, the server must check:
   - *"Is this `jti` in the blacklist?"*

**If yes** → reject the request.
**If no** → continue with normal validation.

---

## 5. Why This Makes JWT Stateful

Here is the key insight:

> **The token itself <mark>did not change</mark>. But <mark>the server now <u>has to remember</u> state</mark>.**

Before revocation:

```js
Request → Verify signature → Trust claims → Allow
```

The server holds **nothing**.

After revocation:

```
Request → Verify signature → Check blacklist (Redis) → Trust claims → Allow
```

The server <mark>now **must** consult</mark> a stateful store on every request.

📌 **The state is no longer inside the token — it is inside the server.**

That is what "stateful" means: the server holds **shared memory** about who is banned.

---

## 6. The Trade-Off

You cannot have both at the same time:

| Feature                            | Stateless JWT      | With Revocation                  |
| ---------------------------------- | ------------------ | -------------------------------- |
| Server keeps state                 | ❌ No               | ✅ Yes (blacklist)                |
| Revocation possible                | ❌ No               | ✅ Yes (instant)                  |
| Requires Redis/DB on every request | ❌ No               | ✅ Yes                            |
| Horizontal scaling easy            | ✅ Yes              | ⚠️ Needs shared store            |
| Latency                            | ✅ Fast (math only) | ⚠️ Slightly slower (network hop) |

> 📌 **Every revocation mechanism <mark><u>adds state</u></mark>. <mark>There is no free lunch</mark>.**

---

## 7. The Alternative: Short Expiry + Refresh Tokens

Instead of a blacklist, production systems <mark>**<u>often use</u>**</mark>:

- 🔹 **Access token** with **short expiry** (5–15 minutes)
- 🔹 **Refresh token** stored in the **database**

When a user must be revoked:

- 🔹 Delete the refresh token from the database.
- 🔹 The access token expires within 5–15 minutes.
- 🔹 The maximum damage window is 15 minutes.

**Why this works:**

- ✅ Access tokens <mark>**stay stateless**</mark>.
- ✅ Revocation <mark>**is stateful**</mark>, but <mark>**<u>only</u>**</mark> for the **refresh token**.
- ✅ The refresh token is the "stateful part" — and it already lives in the database.

> 📌 This is the modern standard. <mark>It combines **the best of both** worlds</mark>.

---

## 8. Characteristics

- 🧬 Default JWT is stateless — the server remembers nothing
- 🧬 Revocation requires a stateful store (Redis / DB)
- 🧬 Adding revocation breaks statelessness
- 🧬 Short expiry + refresh tokens is the common compromise
- 🧬 Every security upgrade has a cost in complexity

---

## 9. Interconnection

- 🔗 JWT → issued and forgotten (stateless)
- 🔗 `jti` claim → unique identifier for each token
- 🔗 Redis blacklist → state that lets you revoke
- 🔗 Refresh tokens → state that lets you revoke sessions without breaking access tokens
- 🔗 Every "revocation" solution adds back the state JWT tried to remove

### 🛠️ Summary of Interconnections

- 🔹 Statelessness → the reason JWT scales well
- 🔹 Revocation → the reason JWT has to scale *differently*
- 🔹 You cannot revoke what you never stored
- 🔹 Therefore, revocation requires storage
- 🔹 Therefore, revocation = state

---

## 📌 Summary

| Question                                    | Answer                                          |
| ------------------------------------------- | ----------------------------------------------- |
| Is JWT stateless by default?                | ✅ Yes — the server remembers nothing            |
| Can you revoke a JWT without extra storage? | ❌ No — there is nothing to mark as revoked      |
| What does revocation require?               | A blacklist (Redis, DB, memory)                 |
| Does that make JWT stateful?                | ✅ Yes — the server now holds state              |
| Is that a problem?                          | ⚠️ It is a trade-off, not a bug                 |
| What do real systems do?                    | Short expiry + refresh tokens, or `jti` + Redis |

**Core principle:** statelessness means *"<mark>the server remembers **nothing**</mark>."* Revocation means *"the server **<mark>must remember something</mark>**."* You <mark>**<u>cannot have both</u>**</mark>. Every revocation mechanism is a decision to reintroduce the state that JWT <mark>**<u>was designed to remove</u>**</mark>.

---


