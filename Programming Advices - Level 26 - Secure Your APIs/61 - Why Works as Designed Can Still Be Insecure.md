# Why "Works as Designed" Can Still Be Insecure

---

## 1. 🗝️ Introduction

One of the **<mark><u>most dangerous</u></mark>** phrases in security is:

> **"The system works as designed."**

- 🔹 Developers often say this when security issues are discovered.
- 🔹 From a developer's perspective, the system behaves **<mark>exactly</mark>** according to the code.
- 🔹 But security does **<mark><u>not</u></mark>** evaluate what the system was **<mark>designed to do</mark>**.
- 🔹 Security evaluates what **attackers <mark>are <u>able to do</u></mark>**.

📌 **A system <mark>can function exactly as intended</mark> and <mark><u>still</u> be dangerously insecure</mark>.**

This lesson explains why **correct functionality <mark><u>does not guarantee</u></mark> security**.

---

## 2. 🔍 Why This Lesson Exists

<img src="https://uploads.teachablecdn.com/attachments/3795fea7f1eb436cb4261e250d90116c.png" title="" alt="" width="462" />

Many vulnerabilities **<mark>are <u>dismissed</u></mark>** during development because:

- 🔹 The feature **behaves correctly**
- 🔹 **No error** occurs
- 🔹 The code **executes successfully**

But attackers are **not** concerned with whether the system works.

**<mark>They <u>care about how</u> the system <u>can be abused</mark></u>.**

In many real-world breaches:

📌 **The application <mark>behaved exactly as programmed</mark>.**
**The problem was that <mark>the design itself <u>allowed dangerous behavior</u></mark>.**

---

## 3. 🧠 Security Evaluates Abuse, Not Just Functionality

**Developers <mark><u>usually</u></mark> test features by asking:**

- 🔹 Does the request succeed?
- 🔹 Does the feature produce the correct result?
- 🔹 Does the UI behave correctly?

**If the answer is yes, the feature is <mark>considered finished</mark>.**

**But attackers <mark>test something <u>completely different</u></mark>:**

- 🔹 Can I change the request?
- 🔹 Can I repeat this operation many times?
- 🔹 Can I access something I should not see?
- 🔹 Can I manipulate inputs to bypass restrictions?

📌 **Security testing <mark>focuses <u>on abuse cases</u></mark>, not just normal usage.**

---

## 4. 🔓 Example 1 — Accessing Other Users' Data

Imagine the Student API contains this endpoint:

```http
GET /api/students/{id}
```

**The developer designed the endpoint to return student information.**

**When tested normally:**

```http
GET /api/students/5
```

The system correctly returns student #5.

**Everything works <mark>as intended</mark>.**

**But an attacker tries:**

```http
GET /api/students/1
GET /api/students/2
GET /api/students/3
```

If the API returns those records:

- ❌ The attacker can read **other students' data**.

**The endpoint <mark>works perfectly</mark>.**
**But <mark>the design <u>failed to enforce ownership</u></mark>.**

📌 **The system worked exactly as designed — and the design <mark><u>was insecure</u></mark>.**

---

## 5. 🔓 Example 2 — Unlimited Requests

Consider an API endpoint for login:

```http
POST /api/auth/login
```

**The system correctly verifies passwords.**

**If the password is wrong:**

- ❌ Login fails.

From a functionality perspective, **everything works**.

**But if the endpoint <mark>allows <u>unlimited attempts</u></mark>, attackers can try thousands of passwords.**

**This leads to brute-force attacks.**

Again:

📌 **The system works exactly as designed — but the design <mark><u>allows abuse</u></mark>.**

---

## 6. 🔓 Example 3 — Hidden Admin Features

A developer **<mark><u>hides</u></mark>** admin features in the UI.

Normal users do not see the admin buttons.

**But the API endpoint still exists:**

```http
DELETE /api/users/{id}
```

**If the API does not verify admin privileges:**

**Attackers can call the endpoint directly.**

**The system behaves exactly as coded.**

**But the design assumed the UI would protect the feature.**

📌 **The UI <mark>is not a <u>security boundary</u></mark>.**

---

## 7. 🧬 Characteristics of "Works as Designed" Vulnerabilities

These vulnerabilities share common traits:

- 🧬 The system **behaves correctly**
- 🧬 **No technical error** occurs
- 🧬 Authentication **may succeed**
- 🧬 Framework security **may work**
- 🧬 The **design** allows abuse

📌 **Because of this, these vulnerabilities are often missed during normal testing.**

---

## 8. 🔗 Interconnection with Previous Lessons

This lesson connects directly with the security layers you implemented:

- 🔗 **HTTPS** → protects communication
- 🔗 **CORS** → restricts origins
- 🔗 **Authentication** → verifies identity
- 🔗 **Authorization** → enforces permissions
- 🔗 **Rate limiting** → prevents abuse
- 🔗 **Logging** → detects suspicious activity
- 🔗 **Auditing** → traces privileged actions

**Even when these protections exist:**

📌 **Poor design decisions can still create vulnerabilities.**

### 🛠️ Final Summary of Interconnections

- 🔹 Systems can behave correctly and still be **insecure**
- 🔹 Security must consider **abuse scenarios**
- 🔹 Authorization mistakes **expose data**
- 🔹 Lack of limits **enables attacks**
- 🔹 Design assumptions often **create vulnerabilities**

📌 **Security is not just about writing correct code.**
📌 **It is about designing systems that cannot be abused.**

---

## 9. 🏁 Conclusion

You now understand:

- ✅ Why correct functionality **does not guarantee** security
- ✅ Why attackers exploit **design weaknesses**
- ✅ Why developers must think **beyond normal use cases**
- ✅ Why **abuse scenarios** must be considered during design

📌 **Secure systems are not only designed to work correctly.**

**They are designed to fail safely when abused.**

---

## 📌 Summary

| "Works as Designed"       | "Secure by Design"     |
| ------------------------- | ---------------------- |
| System behaves correctly  | System resists abuse   |
| No error occurs           | No exposure occurs     |
| Happy path tested         | Abuse cases tested     |
| Functionality is verified | Security is verified   |
| Developer is satisfied    | Attacker is frustrated |

| Example             | What Works              | What Fails                     |
| ------------------- | ----------------------- | ------------------------------ |
| **Ownership check** | Returns student #5      | Returns *any* student          |
| **Rate limiting**   | Login verifies password | Unlimited attempts allowed     |
| **Hidden admin**    | UI hides the button     | API endpoint is still callable |

| Question                              | Answer                                                         |
| ------------------------------------- | -------------------------------------------------------------- |
| Does "works as designed" mean secure? | ❌ No                                                           |
| What does security evaluate?          | What attackers can do, not what the system is *supposed* to do |
| What is the key difference?           | Functionality vs. abuse resistance                             |
| What is the fix?                      | Design with abuse in mind                                      |

**<mark>Core principle</mark>:** *"Works as designed"* is a statement about **<mark><u>functionality</u></mark>**, not about **security**. A system that returns the correct data for the correct user **<mark><u>can still be</u> catastrophically insecure</mark>** if it also returns the correct data for the **wrong** user. A login that verifies passwords correctly can still be compromised if it allows **<mark><u>unlimited</u></mark>** attempts. A hidden admin button is not protection if the **endpoint behind it is unprotected**. **Security <mark><u>is not</u></mark> about what the system is designed to do — it is about what the system is *<mark>forced</mark>* to do by an attacker.** Design for the attacker, not just for the user.

---
