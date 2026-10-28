# Logs as an Information Disclosure Attack Vector

---

## 1. Introduction

Logging is **<mark><u>meant</u> to give <u>visibility</u></mark>**. But if it is done carelessly, it becomes a **<mark>new attack surface</mark>**.

This MD covers two related risks:

- 🔹 Sensitive data in logs = **<mark>Information Disclosure</mark>**
- 🔹 Committing logs to GitHub = **<mark><u>Leaking secrets</u> through version control</mark>**

Both fall under the same category: **Information Disclosure Attack**.

---

## 2. Logs and Information Disclosure

### 2.1 The Attack Category

Logging mistakes are classified as:

> **<mark>Information Disclosure Attack</mark>**

**Meaning:** The system **<mark><u>reveals</u> sensitive information</mark>** to an attacker **<mark>who <u>should not</u> have</mark>** access to it.

### 2.2 Why Logs Are a Target

Logs are often:

- 🔹 Stored **long-term**
- 🔹 Shared with **support teams**
- 🔹 Exported to **monitoring systems**
- 🔹 Backed up to **external storage**
- 🔹 Sometimes **committed to Git**

📌 **<mark>A <u>leaked log</u> = a <u>leaked credential</u></mark>.**

---

## 3. The Git Commit Attack (Very Important)

### 3.1 The Scenario

A developer logs sensitive data (passwords, tokens, API keys) during development. Later, they commit the log file to the repository on GitHub.

The attacker does **not** need to hack the server. They just:

1. Find the repository on GitHub (public or leaked).
2. Browse the **commit history**.
3. Search for keywords: `password`, `token`, `Bearer`, `apiKey`, `secret`.
4. Find the sensitive data in a **past commit**.

📌 **Even if the current file is clean, <mark>the history <u>is forever</u></mark>.**

### 3.2 Why It Works

- 🔹 GitHub keeps **<mark>every commit</mark>**.
- 🔹 Deleting the file in a new commit does **not** remove it from history.
- 🔹 Public repos are crawled by **automated bots** looking for secrets.
- 🔹 Attackers can search commits the same way they search code.

### 3.3 Real-World Example

```text
Commit 1: "Add login logic"
+ logger.LogInformation($"User {email} logged in with password {password}");
```

The developer later removes the logging line:

```text
Commit 2: "Remove sensitive logging"
- logger.LogInformation($"User {email} logged in with password {password}");
```

But in Commit 1, the password is **still visible** forever.

📌 **Git <mark>remembers</mark> what developers forget.**

---

## 4. What This Means for You

### 4.1 Never Log Sensitive Data

- ❌ Passwords
- ❌ JWT tokens
- ❌ Refresh tokens
- ❌ API keys
- ❌ Session IDs
- ❌ Cryptographic keys
- ❌ Full credit card numbers
- ❌ Full PII (national ID, phone, address) unless masked

### 4.2 Never Commit Log Files

- ❌ `*.log`
- ❌ `logs/` folder
- ❌ `app.log`
- ❌ `error.log`

Add them to `.gitignore` **before** the first commit.

### 4.3 Never Commit Configuration Files with Secrets

- ❌ `appsettings.json` containing secrets
- ❌ `.env` files
- ❌ `secrets.json`

Use **<mark>environment variables</mark>** or **<mark>Key Vault</mark>** instead (the next lesson covers this).

---

## 5. If You Already Committed a Secret

**Removing the file in a new commit <mark><u>is not enough</u></mark>.** The secret is still in history.

**<mark><u>You must</u></mark>** either:

1. **Rotate the secret immediately** — assume it is compromised.
2. **Rewrite history** using `git filter-repo` or BFG Repo-Cleaner (advanced and dangerous).
3. **Delete the repository** and recreate it (last resort).

📌 **The safest assumption: if it was pushed to a public repo, <mark>it is <u>already compromised</u></mark>.**

---

## 6. Characteristics

- 🧬 Logging sensitive data = Information Disclosure
- 🧬 Git keeps full history forever
- 🧬 Deleting the file does not delete the secret
- 🧬 Public repos are scraped by bots
- 🧬 Logs must never be committed
- 🧬 Secrets must be rotated if leaked

---

## 7. Interconnection

- 🔗 Logging → gives visibility
- 🔗 Careless logging → creates a breach
- 🔗 Git commit → makes the breach permanent
- 🔗 Public repo → makes the breach visible to attackers
- 🔗 `.gitignore` → prevents the mistake
- 🔗 Rotation → limits the damage after the mistake

### 🛠️ Summary of Interconnections

- 🔹 Logging is a security feature **when done safely**
- 🔹 Logging is an attack surface **when done carelessly**
- 🔹 Git is a **permanent** record
- 🔹 The two combined create an **Information Disclosure Attack**

---

## 📌 Summary

| Risk                         | Cause                          | Fix                           |
| ---------------------------- | ------------------------------ | ----------------------------- |
| **Sensitive data in logs**   | Logging passwords, tokens, PII | Log events, not raw data      |
| **Logs committed to Git**    | No `.gitignore` rule           | Ignore `*.log`, `logs/`       |
| **Secrets in config files**  | Hardcoded values               | Use env vars / Key Vault      |
| **Secret leaked in history** | Commit pushed to GitHub        | Rotate the secret immediately |
| **Full attack chain**        | Log + commit + public repo     | Prevent both at the source    |

**Core principle:** an attacker does not need to breach your server if you **publish your secrets yourself**. Logging sensitive data is dangerous. Committing logs to GitHub makes it permanent. Together, they form an **Information Disclosure Attack** — and it is one of the most common ways real systems get compromised. **Treat every log line and every commit <mark>as if an attacker will read it</mark>.**

---
