# Salting & Slow Hashing (Defending Against Real Attacks)

**<mark>Protecting Passwords <u>With Time</u>, Not Secrecy</mark>**

---

## 1. Introduction

In the previous lesson, you learned why passwords must be hashed and how systems compare hashes without knowing the password.

But here is the hard truth:

📌 **<mark>Hashing alone <u>is NOT enough</u> in real systems.</mark>**

<mark>Attackers **don't try to reverse** hashes</mark>. <mark>**<u>They guess passwords</u>**</mark>, hash them, and compare results.

This lesson explains how real systems defend against that.

🎯 **Outcome**

You will understand:

- ✅ Why simple hashing fails
- ✅ What salting really does
- ✅ Why slow hashing protects users
- ✅ How modern systems store passwords safely

---

## 2. The Real Attack — Offline Password Cracking

Let's think like an attacker.

If attackers steal your database, they get:

```js
user@email.com → a94a8fe5ccb19ba...z
```

They can now:

- 🔹 Guess passwords
- 🔹 Hash guesses
- 🔹 Compare hashes
- 🔹 **Do this <u>millions of times</u> <u>per second</u>**

> 📌 <mark>This is called an **offline attack**</mark>.

- **<u>No rate limits</u>**
- **<u>No login endpoint</u>**
- **<u>No alerts</u>**

### ❌ Why Fast Hashing Is Dangerous

Algorithms like:

- ❌ SHA-256
- ❌ MD5

Are:

- 🔹 **<u>Very fast</u>**
- 🔹 <mark>**Designed for integrity**, not passwords</mark>

> 📌 **<mark>Fast hashing</mark> = <mark><u>fast cracking</u></mark>.**

---

## 3. What Is Salting?

<mark>Salting means adding **random data** to a password **before hashing it**</mark>.

<img src="https://uploads.teachablecdn.com/attachments/c80a1bb21b9c4cdb83a7afbb3761a116.png" title="" alt="" width="388">

### 📘 Definition

> A **salt** is <mark>a unique, random value</mark> <mark>added **to each** password **before hashing**</mark>.

### 🧬 Why Salting Exists

**Without salting:**

- **<u>Same passwords</u>** → **<u>same hashes</u>**
- Attackers **<u>use rainbow tables</u>**

> 🌈 **Rainbow Tables** are <mark>**precomputed lists of hashes** created **from common passwords**</mark>, used by attackers <mark>to crack hashed passwords **quickly**</mark>.

**With salting:**

- Same passwords → different hashes
- Precomputed attacks fail

### 🧠 Example (Simple)

**Without salt:**

```csharp
Hash("Password123") → ABC123
Hash("Password123") → ABC123
```

**<mark>With salt</mark>:**

```csharp
Hash("Password123" + "X9$A") → H1
Hash("Password123" + "Q2!M") → H2
```

📌 Same password. <mark>**<u>Completely different hashes</u>**</mark>.

### 🔹 Important Clarification

📌 **<mark>Salts <u>are NOT secret</u></mark>.**

They are:

- 🔹 Stored in the database
- 🔹 Unique per user
- 🔹 **<u>Used only</u>** to defeat precomputed attacks

Security comes from:

- 👉 <mark>**randomness**</mark>
- 👉 <mark>**uniqueness**</mark>

---

## 4. Mental Model (<mark>**Memorize This**</mark>)

### 🧂 Salt = Unique Recipe per User

Even if the attacker knows the recipe:

- They must cook each dish separately
- They cannot reuse results
- It becomes slow and expensive

📌 Even if the attacker knows the salt, they must:

- Hash each guess
- For each user
- Every time

> <mark>That's where **slow hashing** finishes the job</mark>.

---

## 5. What Is Slow Hashing?

<mark>Slow hashing **intentionally** makes hashing **<u>expensive and slow</u>**</mark>.

### 📘 Definition

Slow hashing algorithms are designed to:

- 🔹 **<u>Take measurable time</u>**
- 🔹 **<u>Consume CPU / memory</u>**
- 🔹 **<u>Be expensive to brute-force</u>**

### 🧬 Why Slow Hashing Matters

Attackers rely on speed.

**If:**

- One hash = 1 ms
- 1,000 guesses = 1 second

**But if:**

- One hash = 500 ms
- 1,000 guesses = 500 seconds

> 📌 **<mark>Slow hashing <u>turns brute-force into a nightmare</u>.</mark>**

---

## 6. Password Hashing Algorithms (Correct Ones)

- ✅ bcrypt
- ✅ Argon2 (**modern & recommended**)
- ✅ PBKDF2

These provide:

- 🔹 **<u>Built-in salting</u>**
- 🔹 **<u>Adjustable slowness</u>**
- 🔹 <mark>**Defense against GPU attacks**</mark>

### ❌ What NOT to Use for Passwords

- ❌ MD5
- ❌ SHA-1
- ❌ SHA-256 (alone)
- ❌ Base64 (not hashing at all)

> 📌 **<mark>If it's fast — it's dangerous.</mark>**

---

## 7. Mental Model (Very Important)

### 🛡️ Password Defense = Lock + Noise + Time

- 🔹 Hashing → Lock
- 🔹 Salting → <mark>Unique lock</mark> per user
- 🔹 Slow hashing → Alarm that wastes attacker time

> 📌 **Attackers don't break in — <mark><u>they give up</u></mark>.**

---

## 8. Complete Secure Password Flow

1️⃣ User sends password
2️⃣ API generates a random salt
3️⃣ Password + salt → slow hash
4️⃣ Hash + salt stored in database
5️⃣ Original password discarded

> 📌 Password **<mark>never exists</mark>** after hashing.

---

## 9. Common Questions (Very Important)

### ❓ If salt is stored, doesn't that weaken security?

❌ **No.**

Salts:

- 🔹 Prevent rainbow tables
- 🔹 Prevent identical hashes
- 🔹 Do not need secrecy

### ❓ Why not encrypt passwords instead?

❌ **<mark>Because encryption <u>is reversible</u></mark>.**

If encryption key leaks: 🔥 <mark>**all passwords are exposed**</mark>.

📌 Hashing + salt + slowness <mark>avoids this **entirely**</mark>.

---

## 10. Characteristics (Reality Check)

- 🧬 **<u>Attackers steal databases</u>**, not passwords
- 🧬 Hashing alone <u>**is insufficient**</u>
- 🧬 **<u>Speed helps attackers</u>**, not defenders
- 🧬 **<u>Time is your strongest weapon</u>**

---

## 11. Interconnection

- 🔗 Hashing → **<u>one-way protection</u>**
- 🔗 Salting → **<u>uniqueness</u>**
- 🔗 Slow hashing → **brute-force <u>resistance</u>**
- 🔗 Together → <mark>**<u>real password security</u>**</mark>

### 🛠️ Summary of Interconnections

- 🔹 Hashing **prevents password <u>recovery</u>**
- 🔹 Salting **prevents <u>precomputed attacks</u>**
- 🔹 Slow hashing **prevents <u>large-scale cracking</u>**
- 🔹 Secure systems exist **<u>because attackers run out of time</u>**

---

## 12. Conclusion

If you remember one thing from this lesson, remember this:

> 🔐 **Passwords are not protected by secrecy — <mark>they are protected <u>by time</u></mark>.**

- ✅ Hashing alone **<u>is not enough</u>**
- ✅ Salting **<u>defeats rainbow tables</u>**
- ✅ Slow hashing **<u>defeats brute-force</u>**

🎯 **Outcome achieved:** You now understand how real systems defend passwords.

---

## 📌 Summary

| Concept      | What it does                          | Defeats                          | Secret?         |
| ------------ | ------------------------------------- | -------------------------------- | --------------- |
| Hashing      | One-way transform                     | Recovery of password from hash   | ❌               |
| Salting      | Adds unique random value per password | Rainbow tables, identical hashes | ❌ Stored openly |
| Slow hashing | Makes each hash expensive             | Brute-force, GPU cracking        | ❌               |

| Algorithm       | Verdict | Reason                       |
| --------------- | ------- | ---------------------------- |
| MD5             | ❌ Never | Too fast, broken             |
| SHA-1           | ❌ Never | Too fast, broken             |
| SHA-256 (plain) | ❌ No    | Fast, designed for integrity |
| PBKDF2          | ✅ Yes   | Tunable iterations           |
| bcrypt          | ✅ Yes   | Built-in salt, tunable cost  |
| Argon2          | ✅ Best  | Memory-hard, modern          |

**Core principle:** passwords are not protected by secrecy — <mark>they are protected by **time**</mark>. <mark>Salting removes **precomputation**</mark>; <mark>slow hashing **removes speed**</mark>. Together, <mark>they make offline cracking **<u>economically pointless</u>**</mark>.

---
