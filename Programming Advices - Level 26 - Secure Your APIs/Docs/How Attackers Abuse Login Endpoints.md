# How Attackers Abuse Login Endpoints

---

## 1. 🗝️ Introduction

Login endpoints are <mark>the **most abused endpoints**</mark> in any API.

Not because they are weak — but because <mark>they are **valuable and exposed**</mark>.

---

## 2. 🔹 Why Login Endpoints Are Targeted

Attackers focus on login endpoints because:

- 🔹 They <mark>accept **secrets**</mark> (passwords, tokens)
- 🔹 They <mark>are **publicly accessible**</mark>
- 🔹 They <mark>return **immediate success or failure**</mark>
- 🔹 They <mark>can be called **without authentication**</mark>

> 📌 **Breaking login <mark><u>breaks everything</u></mark>.**

---

## 3. 🔹 How Abuse Happens in Practice

Attackers abuse login endpoints by:

- 🔹 Sending **rapid repeated requests**
- 🔹 Testing **leaked credentials** automatically
- 🔹 **Flooding** the endpoint continuously

They <mark>do **not** care</mark> if:

- ❌ Responses are `401`
- ❌ Errors are returned

They <mark>care **<u>only</u>**</mark> about:

- ✅ **<mark><u>The one request</u></mark> that succeeds.**

---

## 4. 🔹 Credential Stuffing (<mark>**Most Common Abuse**</mark>)

Credential stuffing works like this:

1. Attackers obtain **leaked email/password lists**
2. They test them **automatically** against your API
3. They rely on **password reuse**

Even if:

- 🔹 99.9% of attempts fail

The remaining **0.1%** is enough at scale.

---

## 5. 🧬 Characteristics

- 🧬 Abuse is automated
- 🧬 Login endpoints are cheap to attack
- 🧬 Feedback is immediate
- 🧬 Scale compensates for low success rate

---

## 6. 🏁 Conclusion

Login endpoints are not abused **once**. They are abused **constantly**.

📌 **Any public login endpoint must assume hostile usage.**

---

## 📌 Summary

| Aspect                           | Detail                                                        |
| -------------------------------- | ------------------------------------------------------------- |
| **Why login?**                   | It accepts secrets, is public, gives instant feedback         |
| **How?**                         | Rapid requests, leaked credential lists, automated flooding   |
| **What is credential stuffing?** | Testing leaked email/password pairs against your API          |
| **Why does it work?**            | Password reuse + scale                                        |
| **What is the attacker's goal?** | The 0.1% that succeeds, not the 99.9% that fail               |
| **What should you assume?**      | Every public login endpoint is under hostile usage by default |

**Core principle:** login is the highest-value, lowest-cost target in any API. Attackers do not need to be clever — they need to be **fast, automated, and patient**. The one successful guess pays for all the failures.

---
