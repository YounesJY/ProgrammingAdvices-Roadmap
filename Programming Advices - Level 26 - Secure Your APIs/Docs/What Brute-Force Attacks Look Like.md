# What Brute-Force Attacks Look Like

---

## 1. 🗝️ Introduction

<img src="https://uploads.teachablecdn.com/attachments/92231502220343c6b4b84b108d4a181b.png" title="" alt="" width="405">

When developers hear "brute-force attack", many imagine:

- ❌ A human guessing passwords
- ❌ A few wrong attempts
- ❌ An impatient user

This mental image <mark>**is wrong**</mark>.

> Modern brute-force attacks <mark>are **not human actions**</mark>.
> They are <mark>**automated processes**</mark> designed <mark>to exploit **repetition**</mark>.

---

## 2. 🔹 What a Real Brute-Force Attack Is

<img title="" src="https://uploads.teachablecdn.com/attachments/d14d09881f3649d68dc07bc80d4596e9.png" alt="" width="420">

A real brute-force attack looks like:

- 🔹 Hundreds or thousands of login requests
- 🔹 Requests sent by scripts or bots
- 🔹 No pauses, no thinking
- 🔹 Continuous retries

> 📌 **The attacker <mark>does not "try harder"</mark>. <mark>They simply <u>try more</u></mark>.**

---

## 3. 🔹 Common Brute-Force Patterns

<img src="https://uploads.teachablecdn.com/attachments/bb94a06e9b0643199dce9c9795ae526a.png" title="" alt="" width="351">

Attackers typically use one of these patterns:

- 🔹 **Same email** → many passwords (password guessing against one account)
- 🔹 **Same password** → many emails (credential stuffing across accounts)
- 🔹 **Sequential guessing** from a wordlist (<mark>**dictionary attack**</mark>)
- 🔹 **High-frequency requests** (every second or faster)

> 📌 **One request <mark>is harmless</mark>.**
> 📌 **Repetition <mark><u>is the weapon</u></mark>.**

---

## 4. 🔹 Why Brute-Force Attacks Are Effective

<img src="https://uploads.teachablecdn.com/attachments/318120727bdb4252b32e3ee59478f47a.png" title="" alt="" width="395">

Brute-force attacks succeed because:

- 🔹 Computers are fast
- 🔹 APIs respond quickly
- 🔹 Requests are cheap to send
- 🔹 Time favors the attacker

> 📌 **If attempts <mark>are unlimited</mark>, the attacker <mark>eventually <u>wins</u></mark>.**

---

## 5. 🧬 Characteristics

- 🧬 Fully automated
- 🧬 High request volume
- 🧬 Repetitive behavior
- 🧬 No concern for errors
- 🧬 Exploits unlimited retries

---

## 6. 🏁 Conclusion

Brute-force attacks are **not about guessing well**.
They are about **<mark>guessing <u>endlessly</u></mark>**.

> 📌 **Any authentication system that allows unlimited attempts <mark>is vulnerable <u>by design</u></mark>.**

---

## 📌 Summary

| Aspect                       | Reality                                              |
| ---------------------------- | ---------------------------------------------------- |
| **Who attacks?**             | Scripts and bots, not humans                         |
| **How many attempts?**       | Hundreds or thousands per minute                     |
| **What patterns?**           | Same email, same password, wordlists, high frequency |
| **Why does it work?**        | Unlimited retries + fast APIs                        |
| **What makes it dangerous?** | Repetition, not intelligence                         |
| **What is the fix?**         | Limits on attempts (rate limiting / blocking)        |

**Core principle:** brute-force <mark>**is not** a **thinking attack**</mark> — <mark>it is a **numbers attack**</mark>. The attacker <mark>**does not outsmart**</mark> your system; they <mark>simply **outlast it**</mark>. <mark>**<u>The only defense</u>**</mark> is to make the numbers unaffordable: limit attempts, slow down responses, and block abuse before it becomes profitable.

---
