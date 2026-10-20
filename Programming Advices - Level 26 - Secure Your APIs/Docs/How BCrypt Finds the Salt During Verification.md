# How BCrypt Finds the Salt During Verification

---

## 1. Introduction

When you store a password with BCrypt, <mark>you only save **one value**</mark> in the database:

```csharp
student.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1");
```

<mark>You do **not** store a separate `Salt` column</mark>.

This raises an obvious question:

❓ **When a user logs in, <mark>how does BCrypt know <u>which salt was used</u></mark> to create the stored hash?**

The answer is simple once you see it: **the salt <mark>is stored <u>inside the hash string itself</u></mark>.**

---

## 2. What a BCrypt Hash Actually Looks Like

Calling `HashPassword("password1")` returns a **60-character string** like this:

```haskell
$2a$11$R9h/cIPz0gi.URNNX3kh2OPST9/PgBkqquzi.Ss7KIUgO2t0jWMUW
```

That string <mark>**is not just a hash**</mark>. <mark>It is a **self-contained record** with **<u>four parts</u>**</mark>:

| Part                              | Meaning                                         | Example |
| --------------------------------- | ----------------------------------------------- | ------- |
| `$2a$`                            | BCrypt version                                  | `$2a$`  |
| `$11$`                            | Cost factor (2^11 = 2048 rounds)                | `$11$`  |
| `R9h/cIPz0gi.URNNX3kh2O`          | **The salt** (22 Base64 chars = 16 bytes)       |         |
| `PST9/PgBkqquzi.Ss7KIUgO2t0jWMUW` | The resulting hash (31 Base64 chars = 23 bytes) |         |

📌 **The salt <mark>is not hidden</mark>. <mark>It is <u>embedded</u>, prefixed, and readable</mark>** — right after the cost factor.

---

## 3. How Verification Works Step by Step

When the login endpoint calls:

```csharp
BCrypt.Verify("password1", storedHash)
```

BCrypt does **not** remember the salt from registration. It **<mark>reads it from the stored hash</mark>** you just passed in.

### The flow:

1. **Parse the stored hash.**
   
   - Read the version: `2a`
   - Read the cost factor: `11`
   - Extract the salt: `R9h/cIPz0gi.URNNX3kh2O`
   - Extract the original hash: `PST9/PgBkqquzi.Ss7KIUgO2t0jWMUW`

2. **Recompute the hash** using:
   
   - The plaintext password the user just sent
   - The salt extracted from the stored hash
   - The cost factor extracted from the stored hash

3. **Compare.**
   
   - If the newly computed hash equals the stored hash → ✅ password matches
   - If they differ → ❌ wrong password

📌 <mark>The salt was never "forgotten"</mark> — it was stored next to the hash, in the same string, the whole time.

---

## 4. The Mental Model

Think of a BCrypt hash as a **sealed envelope**:

- The **salt** is written on the outside.
- The **hash result** is written on the inside.
- During verification, BCrypt opens the envelope, reads the salt, recomputes the hash, and checks if the inside matches.

You never need a separate `Salt` column in your database because <mark>**the salt is part of the hash string itself.**</mark>

---

## 5. What the Stored Column Looks Like

```haskell
| Id | Email                     | PasswordHash                                                  |
|----|---------------------------|---------------------------------------------------------------|
| 1  | ali.ahmed@student.com     | $2a$11$R9h/cIPz0gi.URNNX3kh2OPST9/PgBkqquzi.Ss7KIUgO2t0jWMUW |
| 2  | fadi.khail@student.com    | $2a$11$Xr3k.9LqY7sW2nP0mB4/Tc9JfHuZ8KxVnR2cQ3pM5N0bY7eLsWj1 |
```

Notice:

- Each row has <mark>a **different salt**</mark> (visible in the middle section)
- The **cost factor is the same** (`$11$`)
- The **final hash is different** — because the salt is different

Even if Ali and Fadi use the **same password**, their stored hashes <mark>**are completely different**</mark>.

---

## 6. Why This Design Is Brilliant

| Traditional approach                           | BCrypt approach                                                 |
| ---------------------------------------------- | --------------------------------------------------------------- |
| Store `PasswordHash` and `Salt` as two columns | Store one `PasswordHash` column                                 |
| Developer must remember to use the salt        | <mark>**<u>Salt is automatic</u>**</mark>                       |
| Salt loss = verification broken                | <mark>**Salt loss is impossible**</mark> — it's inside the hash |
| Easy to accidentally reuse salt                | <mark>**Impossible**</mark> — each hash carries its own         |
| Schema change needed to migrate                | <mark>**No schema change**</mark> — one string column           |

📌 This is why the course says: **"<mark>You never need to 'get the salt' manually</mark> — BCrypt already did that for you."**

---

## 7. Is the Salt Secret?

❌ **No.** The salt is visible in the hash string. Anyone with database access can read it.

**So what does the salt actually protect against?**

- 🌈 **Rainbow tables** — precomputed `password → hash` lookups no longer work, because the same password produces a different hash per user.
- 🧮 **Mass cracking** — an attacker cannot compute one hash per guess and compare against the whole table. They must crack each user **individually**, using that user's specific salt. That turns a batch attack into N separate attacks.

📌 The salt does **not** need to be secret. It needs to be **unique and random per password**. That is enough to destroy precomputed attacks.

The **slow hashing** (cost factor `$11$`) is what protects against brute force.

The **salt** protects against precomputation.

The **hash** protects against recovery.

---

## 📌 Summary

| Question                                    | Answer                                  |
| ------------------------------------------- | --------------------------------------- |
| Where is the salt stored?                   | **Inside the hash string**              |
| Does BCrypt need a separate `Salt` column?  | ❌ No                                    |
| How does BCrypt find the salt during login? | It **parses it out** of the stored hash |
| Is the salt secret?                         | ❌ No — it is visible in the hash        |
| What does the salt protect against?         | Rainbow tables, mass cracking           |
| What does the cost factor protect against?  | Brute force, GPU attacks                |
| What does the hash protect against?         | Password recovery                       |

**Core principle:** a BCrypt hash is <mark>**<u>not just a hash</u>**</mark> — it <mark>is a **self-contained record**</mark>: `version + cost + salt + hash`. BCrypt <mark>never "remembers" the salt</mark>; it **<mark>re-reads it from the hash every time</mark>**. That is why <mark>one database column **is enough**</mark>, and why <mark>the salt can **<u>never go missing</u>**</mark>.

---


