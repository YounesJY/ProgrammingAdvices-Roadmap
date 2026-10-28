# Avoiding Sensitive Data Leaks in Logs

---

## 1. 🗝️ Introduction

Logging **<mark>gives you <u>visibility</u></mark>** — but **<mark><u>careless</u> logging</mark>** can create a **<mark><u>new</u> security <u>vulnerability</u></mark>**.

📌 **Logs themselves <mark><u>can become</u> a powerful <u>attack surface</u></mark>.**

This lesson explains:

- Why sensitive data must **<mark><u>never</u></mark>** appear in logs
- What types of data are **<mark><u>dangerous</u></mark>** to log
- How to think safely when designing security logs

---

## 2. 🔍 Why This Lesson Exists

Logs are often:

- 🔹 Stored for **<mark>long periods</mark>**
- 🔹 Accessible to **<mark>many people</mark>** (developers, ops, support)
- 🔹 Exported to **<mark>external systems</mark>** (SIEM, monitoring tools)

If logs contain secrets, then:

- ❌ A log leak becomes a **<mark>system <u>breach</u></mark>**
- ❌ An attacker does **not need** to hack your API
- ❌ **Reading logs <mark>is enough</mark>**

📌 **Many real-world breaches <mark>started from <u>exposed logs</u></mark>.**

---

## 3. 🔹 Why Logs Are a High-Value Target

Attackers love logs because they may contain:

- 🔹 **Credentials**
- 🔹 **Tokens**
- 🔹 Internal system details
- 🔹 **User data**

Logs are often **<mark><u>less protected</u></mark>** than databases — yet **<mark>just <u>as valuable</u></mark>**.

📌 **Logging mistakes <mark>can <u>undo</u></mark> strong authentication and encryption.**

---

## 4. 🔹 Sensitive Data That Must NEVER Be Logged

**<mark><u>Never</u> log <u>the following</u></mark> — <mark><u>under any circumstances</u></mark>:**

- ❌ Passwords (even hashed or partial)
- ❌ JWT access tokens
- ❌ Refresh tokens
- ❌ API keys or secrets
- ❌ Session identifiers
- ❌ Cryptographic keys

📌 **If <mark><u>someone</u></mark> can reuse it <mark>to gain <u>access</u></mark>, <mark>it <u>must not be</u></mark> logged.**

---

## 5. 🔹 Avoid Logging Personal Data by Default

Personal data should be logged **<mark><u>only if</u></mark> strictly necessary**.

**<mark>Avoid logging</mark>:**

- ❌ Full emails
- ❌ Phone numbers
- ❌ Addresses
- ❌ National IDs
- ❌ Financial information

**<mark>If needed</mark>:**

- 🔹 **Mask** values
- 🔹 **Truncate** data
- 🔹 Use **identifiers** instead

📌 **Logs should <mark><u>describe</u> behavior</mark> — <mark><u>not expose</u> identities</mark>.**

---

## 6. 🔹 Safe Logging Principles

To log securely:

- 🔹 Log **events**, not raw data
- 🔹 Log **outcomes**, not secrets
- 🔹 Prefer **IDs** over values
- 🔹 Assume logs **may be exposed**

📌 **Always log <mark><u>as if</u> an attacker <u>might read it</u></mark>.**

---

## 7. 🔹 Examples: Safe vs Unsafe Logging

### ❌ Unsafe

```text
Login failed for email=test@test.com password=123456
```

### ✅ Safe

```text
Login failed (invalid credentials) from IP=192.168.1.10
```

---

### ❌ Unsafe

```text
Issued refresh token: eyJhbGciOi...
```

### ✅ Safe

```text
Refresh token issued for userId=42
```

📌 **Context <mark><u>is enough</u></mark> — secrets <mark>are <u>never required</u></mark>.**

---

## 8. 🔹 Why Masking Is Better Than Removing Everything

Sometimes you need **<mark><u>partial</u> visibility</mark>**.

**<mark><u>Examples</u></mark>:**

- 🔹 Show **last 2–3 characters** of an ID
- 🔹 **Hash** identifiers before logging
- 🔹 Replace values with **placeholders**

📌 **Masking <mark><u>preserves usefulness</u></mark> without increasing risk.**

---

## 9. 🧬 Characteristics

- 🧬 Prevents secondary breaches
- 🧬 Protects credentials and tokens
- 🧬 Reduces blast radius
- 🧬 Makes logs safe by design
- 🧬 Preserves trust in logging systems

---

## 10. 🔗 Interconnection

- 🔗 Logging → provides **visibility**
- 🔗 Careless logging → creates **leaks**
- 🔗 Safe logging → preserves **security**

📌 **Visibility must never compromise protection.**

### 🛠️ Summary of Interconnections

- 🔹 Logs are **powerful** and **dangerous**
- 🔹 Sensitive data **amplifies risk**
- 🔹 Safe logging focuses on **behavior**
- 🔹 Masking **preserves usefulness**
- 🔹 Secure logs **protect the whole system**

📌 **Logs should explain events — not expose secrets.**

---

## 11. 🏁 Conclusion

You now understand:

- ✅ Why logs can become a security risk
- ✅ What data must **never** be logged
- ✅ How to log **safely and responsibly**
- ✅ How to balance **visibility with protection**

📌 **A secure system does not just log — it logs safely.**

---

## 📌 Summary

| Category           | Never Log                      | Safe Alternative             |
| ------------------ | ------------------------------ | ---------------------------- |
| **Passwords**      | Plaintext, hashed, partial     | "Invalid credentials"        |
| **Tokens**         | JWT, refresh, session IDs      | "Token issued for userId=42" |
| **Keys**           | API keys, cryptographic keys   | "Key rotated successfully"   |
| **Personal data**  | Full email, phone, national ID | Masked value or user ID      |
| **Financial data** | Card numbers, bank accounts    | Last 4 digits only           |

| Principle                     | Meaning                            |
| ----------------------------- | ---------------------------------- |
| **Log events, not data**      | "Login failed" not "password=123"  |
| **Log outcomes, not secrets** | "Token issued" not the token       |
| **Prefer IDs over values**    | `userId=42` not `email=...`        |
| **Assume logs will be read**  | Log as if the attacker is watching |

**<mark>Core principle</mark>:** logs are **<mark><u>evidence</u></mark>**, not a data store. They should describe **<mark>what happened</mark>**, <mark><u>not</u></mark> **what was sent**. A password in a log **<mark><u>is worse</u></mark>** than a password in a URL — because logs are copied, shared, exported, and forgotten. **A log leak <mark>is <u>a breach</u></mark>.** Always log as if an attacker is already reading the file — because eventually, they will.

---
