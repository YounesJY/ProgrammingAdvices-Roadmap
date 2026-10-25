# How Attackers Abuse Login Endpoints

---

## 1. 🗝️ Introduction

<img title="" src="https://uploads.teachablecdn.com/attachments/3cd9960abb1c4846af0f70e071b2d5bb.png" alt="" width="401">

Login endpoints are <mark>the **most abused endpoints**</mark> in any API.

Not because they are weak — but because <mark>they are **valuable and exposed**</mark>.

---

## 2. 🔹 Why Login Endpoints Are Targeted

Attackers focus on login endpoints because:

- 🔹 They <mark>accept **secrets**</mark> (passwords, tokens)
- 🔹 They <mark>are **publicly accessible**</mark>
- 🔹 They <mark>return **immediate success or failure**</mark>
- 🔹 They <mark>can be called **<u>without authentication</u>**</mark>

> 📌 **Breaking login <mark><u>breaks everything</u></mark>.**

---

## 3. 🔹 How Abuse Happens in Practice

<img src="https://uploads.teachablecdn.com/attachments/8a53ddc24c094ebbadd962f3a720e0fc.png" title="" alt="" width="430">

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

<img src="https://uploads.teachablecdn.com/attachments/1abd16cba9d24a50a068d5fd6bf5aed5.png" title="" alt="" width="370">

Credential stuffing works like this:

1. Attackers <mark>obtain **<u>leaked</u> email/password lists**</mark>
2. They <mark>test them **automatically**</mark> against your API
3. They <mark>rely on **password reuse**</mark>

Even if:

- 🔹 99.9% of attempts fail

> The remaining <mark>**0.1%** is enough at scale</mark>.

---

## 5. 🧬 Characteristics

- 🧬 Abuse <mark>is **automated**</mark>
- 🧬 Login endpoints <mark>are **cheap**</mark> to attack
- 🧬 Feedback <mark>is **<u>immediate</u>**</mark>
- 🧬 Scale compensates for low success rate

---

## 6. 🏁 Conclusion

Login endpoints are not abused **once**. <mark>They are abused **constantly**</mark>.

> 📌 **Any public login endpoint <mark><u>must assume hostile usage</u></mark>.**

---

## 📌 Summary

| Aspect                           | Detail                                                                                                          |
| -------------------------------- | --------------------------------------------------------------------------------------------------------------- |
| **Why login?**                   | It <mark>**accepts secrets**</mark>, is <mark>**public**</mark>, gives <mark>**<u>instant feedback</u>**</mark> |
| **How?**                         | Rapid requests, leaked credential lists, automated flooding                                                     |
| **What is credential stuffing?** | Testing leaked email/password pairs against your API                                                            |
| **Why does it work?**            | Password reuse + scale                                                                                          |
| **What is the attacker's goal?** | The 0.1% that succeeds, not the 99.9% that fail                                                                 |
| **What should you assume?**      | Every public login endpoint is under hostile usage by default                                                   |

**Core principle:** login <mark>is **<u>the highest-value</u>**</mark>, <mark>**<u>lowest-cost</u>**</mark> target in any API. Attackers do not need to be clever — they <mark>need to be **fast, automated, and patient**</mark>. <mark>**<u>The one</u>**</mark> successful guess pays for all the failures.

---
