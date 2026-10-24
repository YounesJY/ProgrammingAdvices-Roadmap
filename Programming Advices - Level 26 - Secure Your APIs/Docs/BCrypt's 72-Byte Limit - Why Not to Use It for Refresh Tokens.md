# BCrypt's 72-Byte Limit (Why Not to Use It for Refresh Tokens)

---

## 1. Introduction

During the discussion of the refresh token lesson, a student discovered a critical behavior:

> "I sent <mark>a **wrong** refresh token</mark> to the logout endpoint — I <mark>added a character **at the end**</mark> of the correct token. **<mark><u>The endpoint accepted it as valid.</u></mark>** But <mark>when I changed the **beginning**</mark> of the token, <mark>the endpoint **<u>rejected it</u>**</mark>."

This <mark>is **not a bug**</mark> in the code. It is <mark>a **<u>known limitation</u> of the BCrypt algorithm itself**</mark>.

This MD explains:

- 🔹 What the 72-byte limit is
- 🔹 Why BCrypt silently ignores the rest
- 🔹 Why this becomes a security hole for refresh tokens
- 🔹 The correct fix (SHA-256)

---

## 2. The 72-Byte Limit

The BCrypt algorithm <mark>**was designed** in 1999</mark>. It reads **only the first 72 bytes** of the input and **silently ignores everything after that**.

- ❌ No error
- ❌ No warning
- ❌ No exception

<mark>**It just**</mark> truncates and continues.

> 📌 **This <mark>is documented behavior of BCrypt</mark>, <mark>not a bug</mark> in any .NET library.**

---

## 3. Why This Becomes a Security Hole

### 3.1 The Refresh Token Size

In your API, the refresh token is generated as:

```csharp
var bytes = new byte[64];
rng.GetBytes(bytes);
return Convert.ToBase64String(bytes);
```

- **64 bytes** of random data
- Converted to **Base64** → **~88 characters**

### 3.2 What BCrypt Actually Sees

```
Full refresh token:    AAAA...AAAA...AAAA (88 chars)
BCrypt reads:          AAAA...AAAA      (first 72 chars only)
BCrypt ignores:                         ...AAAA (last 16 chars)
```

### 3.3 The Consequence

- ✅ Changing a character <mark>in the **first 72**</mark> → verification <mark>**fails**</mark> (correct)
- ❌ Changing a character <mark>**after the 72nd**</mark> → verification **<mark><u>succeeds</u></mark>** (<mark>**<u>security hole</u>**</mark>)

📌 **An attacker who steals the first 72 bytes of a refresh token <mark>can use <u>any variation</u> of the last 16 bytes</mark>.**

---

## 4. Why This Is Not a BCrypt "Bug"

BCrypt was designed to hash **passwords**, which are typically short.

The 72-byte limit <mark>is **<u>a deliberate design choice</u>**</mark>:

- 72 bytes = 576 bits of input
- Even 72 bytes <mark>is **<u>more than enough</u>**</mark> entropy for a password
- The truncation <mark>**<u>does not weaken</u>**</mark> password hashing in practice

> 📌 **<mark>BCrypt is correct</mark>. BCrypt <mark>is just used in <u>the wrong place</u></mark>.**

Some libraries (like Go's `golang.org/x/crypto/bcrypt`) explicitly reject passwords longer than 72 bytes to prevent this exact confusion. The .NET library follows the original algorithm and **<mark>silently truncates</mark>** instead.

---

## 5. Why You Should Not Use BCrypt for Refresh Tokens

### 5.1 Refresh Tokens Are Not Passwords

| Characteristic         | Password                          | Refresh Token                    |
| ---------------------- | --------------------------------- | -------------------------------- |
| **Entropy source**     | Human memory                      | Cryptographic RNG                |
| **Length**             | Short (8–30 chars)                | Long (64+ bytes)                 |
| **Brute-force risk**   | High (humans pick weak passwords) | Negligible (512 bits of entropy) |
| **Needs slow hashing** | ✅ Yes                             | ❌ No                             |
| **Needs truncation**   | ❌ Should not be truncated         | ❌ Must not be truncated          |

> 📌 **Passwords <mark><u>need</u></mark> BCrypt because <mark>they are weak</mark>. Refresh tokens <u>do not need</u> BCrypt because they are <mark>already unguessable</mark>.**

### 5.2 The Damage of Truncation

- **Passwords:** truncation at 72 bytes is irrelevant (passwords are rarely longer than 20 chars)
- **Refresh tokens:** truncation at 72 bytes **removes part of the token's entropy from verification** — the last 16 characters can be modified freely without detection

> 📌 **BCrypt is <mark>safe for <u>passwords</u></mark>. It is <mark>unsafe for <u>refresh tokens</u></mark>.**

---

## 6. The Correct Fix — SHA-256

### 6.1 Why SHA-256

- ✅ Hashes the **entire input** — no truncation
- ✅ Fast (which is fine, because the token is already random)
- ✅ Deterministic — the same token always produces the same hash
- ✅ No salt needed (the token already contains 512 bits of entropy)
- ✅ No cost factor needed (brute-force is already infeasible)

### 6.2 The Helper

```csharp
using System.Security.Cryptography;
using System.Text;

private static string HashRefreshToken(string token)
{
    using var sha256 = SHA256.Create();
    var bytes = Encoding.UTF8.GetBytes(token);
    var hash = sha256.ComputeHash(bytes);
    return Convert.ToBase64String(hash);
}
```

### 6.3 Hashing on Login

**Before:**

```csharp
student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
```

**After:**

```csharp
student.RefreshTokenHash = HashRefreshToken(refreshToken);
```

### 6.4 Verifying on Refresh / Logout

**Before:**

```csharp
bool isValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, student.RefreshTokenHash);
```

**After:**

```csharp
string incomingHash = HashRefreshToken(request.RefreshToken);
bool isValid = incomingHash == student.RefreshTokenHash;
```

### 6.5 Why This Is Safe

- The refresh token is **random** (64 bytes = 512 bits of entropy).
- Brute-forcing 512 bits is **computationally impossible** — even with a million GPUs.
- A fast hash like SHA-256 is fine because the input is already unguessable.
- No truncation → the **entire** token is verified.

📌 **Slow hashing (BCrypt) is for weak inputs. Fast hashing (SHA-256) is for strong inputs.**

---

## 7. What This Means for Passwords vs. Tokens

| Asset                   | Hashing Algorithm        | Reason                                     |
| ----------------------- | ------------------------ | ------------------------------------------ |
| **Passwords**           | BCrypt / Argon2 / PBKDF2 | Weak, human-chosen → need slowness + salt  |
| **Refresh tokens**      | SHA-256                  | Strong, random → need full-input integrity |
| **Access tokens (JWT)** | Not hashed at all        | Signed, short-lived, stateless             |

📌 **Different inputs need different algorithms. Using BCrypt for a refresh token is like putting a bank vault on a plastic chair.**

---

## 8. Characteristics

- 🧬 BCrypt only reads the first 72 bytes
- 🧬 This is a limitation of the algorithm, not a bug
- 🧬 Passwords are < 72 bytes → safe
- 🧬 Refresh tokens are > 72 bytes → unsafe
- 🧬 SHA-256 hashes the entire input
- 🧬 Refresh tokens are already random → fast hashing is fine
- 🧬 Rule: **BCrypt for passwords, SHA-256 for refresh tokens**

---

## 9. Interconnection

- 🔗 BCrypt → designed for passwords → 72-byte limit acceptable
- 🔗 Refresh tokens → 88 bytes (Base64) → 16 bytes ignored by BCrypt
- 🔗 Truncation → attacker can modify the end → security hole
- 🔗 SHA-256 → full-input hash → no truncation
- 🔗 Randomness → makes fast hashing safe
- 🔗 Rule of thumb → the algorithm must match the input

### 🛠️ Summary of Interconnections

- 🔹 BCrypt's design is correct for its intended use
- 🔹 The 72-byte limit is invisible to developers
- 🔹 Using BCrypt on long tokens silently breaks part of the verification
- 🔹 SHA-256 is the right tool for random, long tokens
- 🔹 Different inputs → different algorithms

---

## 📌 Summary

| Question                                | Answer                                                               |
| --------------------------------------- | -------------------------------------------------------------------- |
| What is BCrypt's limit?                 | 72 bytes of input                                                    |
| What happens after 72 bytes?            | Silently ignored (no error)                                          |
| Is this a bug?                          | ❌ No — it is documented behavior                                     |
| Why is it dangerous for refresh tokens? | The last 16 bytes can be changed without detection                   |
| Why not use BCrypt for refresh tokens?  | Refresh tokens are long and random — BCrypt is not designed for that |
| What should be used instead?            | SHA-256                                                              |
| Why is SHA-256 safe?                    | The token already has 512 bits of entropy — fast hashing is fine     |
| What is the golden rule?                | **BCrypt for passwords. SHA-256 for refresh tokens.**                |

**Core principle:** BCrypt is <mark>**the right tool** for **the wrong input**</mark>. It <mark>**<u>was designed</u>**</mark> for short, weak, human-chosen passwords. A refresh token <mark>**is long, random, and unguessable**</mark> — it does not need slowness, and it must not be truncated. Use SHA-256 to hash the entire token, and keep BCrypt for the thing it was built for: passwords.

---
