# 📖 Encoding vs Encryption vs Hashing

**Understanding What Protects Data — and What Does NOT**

> "If you confuse these three, <mark>your API **will look secure**… **<u>and still get hacked</u>**.</mark>"

<img title="" src="https://uploads.teachablecdn.com/attachments/dea21b085bf0483387d6e3333a3f6e03.png" alt="" width="448" data-align="center">  

---

## 1. Introduction

Many developers confuse encoding, encryption, and hashing.

They sound similar. They transform data. <mark>**They all** "change" the original value</mark>. But <mark>they serve **completely different purposes**</mark>.

❌ Mixing them leads to:

- Storing passwords **<u>incorrectly</u>**
- **<u>False sense of security</u>**
- **<u>Easily compromised systems</u>**

In this lesson, you will clearly understand:

- 🔹 What **<u>each one does</u>**
- 🔹 **<u>When to use each one</u>**
- 🔹 **<u>Why confusing them breaks real systems</u>**

> 🎯 **Outcome:** <mark>**<u>You will never misuse</u>**</mark> encoding, encryption, or hashing again.

---

## 2. Side-by-Side Comparison (Very Important)

| Aspect                          | Encoding                                          | Encryption                                                   | Hashing                                                                     |
| ------------------------------- | ------------------------------------------------- | ------------------------------------------------------------ | --------------------------------------------------------------------------- |
| **Purpose**                     | <mark>**Change format**</mark>                    | <mark>**Protect <u>confidentiality</u>**</mark>              | <mark>**Protect integrity**</mark> / <mark>**<u>verify secrets</u>**</mark> |
| **Reversible?**                 | ✅ Yes (trivially)                                 | ✅ Yes (with key)                                             | ❌ No (<mark>**<u>by design</u>**</mark>)                                    |
| **Requires a key?**             | ❌ No                                              | ✅ Yes                                                        | ❌ No (<mark>**<u>salt is not a key</u>**</mark>)                            |
| **Protects against attackers?** | ❌ No                                              | ✅ Yes                                                        | ✅ Yes (for verification only)                                               |
| **Typical use**                 | **<u>Base64</u>**, URL encoding, **<u>UTF-8</u>** | **<u>TLS</u>**, **<u>encrypted fields</u>**, secrets at rest | **<u>Password storage</u>**, **<u>checksums</u>**, **<u>signatures</u>**    |
| **If exposed**                  | <mark>**<u>Fully readable</u>**</mark>            | <mark>**<u>Useless</u>** **without the key**</mark>          | <mark>**<u>Cannot be reversed</u>**</mark>                                  |

---

## 3. Characteristics (Reality Check)

- 🧬 Encoding ≠ security
- 🧬 Encryption protects **<u>data you must retrieve</u>**
- 🧬 Hashing protects **<u>secrets permanently</u>**
- 🧬 **<u>Choosing the wrong one breaks security</u>**

---

## 4. Interconnection

- 🔗 Encoding → **<u>data handling</u>**
- 🔗 Encryption → **<u>confidentiality</u>**
- 🔗 Hashing → **<u>identity protection</u>**
- 🔗 Wrong choice → <mark>**<u>real-world breaches</u>**</mark>

### 🛠️ Summary of Interconnections

- 🔹 Encoding formats data → **<u>does not protect</u>**
- 🔹 Encryption protects data → **<u>requires a secret key</u>**
- 🔹 Hashing protects secrets → **<u>irreversible *by design*</u>**
- 🔹 Secure systems exist <mark>because **the right tool** is **<u>used for the right job</u>**</mark>

---

## 5. Conclusion

If you remember one thing from this lesson, remember this:

> 🔐 Encoding <mark>**changes format**</mark>.
> 🔐 Encryption <mark>**protects data**</mark>.
> 🔐 Hashing <mark>**protects secrets**</mark>.

🎯 **Outcome achieved:** You now initially understand:

- ✅ When encoding is safe
- ✅ When encryption is required
- ✅ Why hashing is mandatory for passwords

In the next lessons, we will detail each one.

---

## 📌 Summary

|                | Encoding                         | Encryption              | Hashing                     |
| -------------- | -------------------------------- | ----------------------- | --------------------------- |
| Reversible     | ✅ Anyone                         | ✅ With key              | ❌ Never                     |
| Key needed     | ❌                                | ✅                       | ❌                           |
| Security value | ❌ None                           | ✅ High                  | ✅ High (verification only)  |
| Use it for     | Transport format, storage format | Data you must read back | Passwords, integrity checks |

> **<mark><u>One-line rule</u></mark>:** if you <mark>**can reverse it <u>without a key</u>**</mark>, it's encoding. If you <mark>**can reverse it <u>with a key</u>**</mark>, it's encryption. If you <mark>**<u>can't reverse it at all</u>**</mark>, it's hashing.

---
