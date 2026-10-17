# ASP.NET Core CORS Configuration

**<mark>Configuring Browser Access Safely (and Avoiding Costly Mistakes)</mark>**

---

## 1. Introduction

You now understand what CORS is and why browsers enforce it.

In this lesson, we move from theory to real configuration.

🎯 **Goal of this lesson**

<mark>Configure CORS in ASP.NET Core **correctly, safely, and intentionally**</mark> — without:

- Breaking frontend apps
- Accidentally opening your API
- Creating false security

<mark>This lesson **is about precision**</mark>, not guessing.

🎯 **Outcome**

By the end of this lesson, you will:

- ✅ Configure CORS correctly in `Program.cs`
- ✅ Understand **<u>where and why</u>** CORS must be applied
- ✅ Avoid the **<u>most common</u>** real-world CORS mistakes
- ✅ Know what is safe for development vs production

---

## 2. Where CORS Is Configured in ASP.NET Core

CORS in ASP.NET Core <mark>has **two parts**</mark>:

1️⃣ Define CORS policies (<mark>**services**</mark>)
2️⃣ Apply CORS middleware (<mark>**pipeline**</mark>)

> 📌 If either part is missing → <mark>**CORS does not work correctly**</mark>.

---

## 3. Step 1 — Add CORS Services (Policy Definition)

📋 **Where**

In `Program.cs`, before:

```csharp
var app = builder.Build();
```

📋 **Correct Configuration Example**

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("StudentApiCorsPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7217",
                "http://localhost:5215"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

📌 **Why This Is Correct**

- ✅ Uses **<u>a named policy</u>** (clear & reusable)
- ✅ Allows **only <u>specific origins</u>**
- ✅ Allows <u>**required headers & methods**</u>
- ❌ Does <mark>**<u>NOT allow everything</u>**</mark>

---

## 4. Step 2 — Apply CORS Middleware (Pipeline)

📋 **<mark>Correct Order Matters (Very Important)</mark>**

Your pipeline <mark>**<u>must</u>**</mark> look like this:

```csharp
app.UseHttpsRedirection();

app.UseCors("StudentApiCorsPolicy");

app.UseAuthorization();

app.MapControllers();
```

🚨 **Hard Rule (<mark><u>Must Memorize</u></mark>)**

> ❌ If `UseCors()` is placed after `MapControllers()` → <mark>CORS **<u>will NOT work</u>**</mark>.

> 📌 CORS <mark>must run **before**</mark> endpoints are executed.

---

## 5. Step 3 — Understanding Preflight Requests (OPTIONS)

Browsers send a **<mark>preflight request</mark>** when:

- Using `PUT`, `DELETE`, `PATCH`
- Sending custom headers (e.g. `Authorization`)
- Sending JSON

📌 The browser sends:

```js
OPTIONS /api/Students/5
```

ASP.NET Core:

- **Automatically handles this**
- <mark>**Only** if CORS **<u>is configured correctly</u>**</mark>

> ❌ If CORS is misconfigured → browser blocks the real request.

---

## 6. Browser Perspective (<mark><u>Very Important</u></mark>)

The browser's job is to <mark>protect the **user**</mark>, not the server.

The browser says:

> "I am about to send a request that:
> 
> - Can modify data
> - Can include credentials
> - Can expose private information
> 
> Let me <mark>**check first if this is allowed**</mark>."

So it sends a safe question: an `OPTIONS` request.

📌 This request:

- Has no body
- Changes nothing
- Is safe

---

## 7. Why Asking Once Is Not Enough

You might think:

> "Why not just send the real request and let the server decide?"

Because:

❌ **<mark>The server <u>may still process it</u>.</mark>**

Even if the browser blocks reading the response, the request may already:

- Delete data
- Update records
- Trigger actions

📌 **Blocking after sending <mark><u>is useless</u></mark>.**

---

## 8. Real Attack Scenario (Simple)

You are logged in to `bank.com`.

You visit `evil-site.com`.

**Without preflight:**

1. Browser sends `DELETE /api/account`
2. Cookies are attached automatically
3. Server processes it
4. Even if browser blocks the response: 💥 **<mark><u>Account already deleted</u></mark>**

> 📌 Preflight <mark>stops this **before it happens**</mark>.

---

## 9. Mental Model (Memorize This)

> 🧾 **Preflight = "Is this allowed?"**
> 🚫 If NO → real request is never sent.

Once a dangerous request is sent, <mark>**security <u>already failed</u>**</mark>.

---

## 10. Why Preflight Exists (In One List)

Preflight exists to:

- 🔹 <u>**Prevent silent data modification**</u>
- 🔹 Protect logged-in users
- 🔹 Stop malicious websites early
- 🔹 **<u>Avoid "too late" security decisions</u>**

🎯 **Final Answer**

> The browser asks first <mark>because security decisions must happen **<u>before</u>** damage</mark>, not after.

---

## 11. Step 4 — Development vs Production Configuration

### 🧪 Development (Local)

Typical trusted origins:

- `https://localhost:7217`
- `http://localhost:5215`

> 📌 <mark>**<u>Be explicit</u>**</mark>, even in development.

### 🏭 Production

Typical trusted origins:

- `https://studentportal.com`
- `https://admin.studentportal.com`

📌 **<mark><u>Never use</u> wildcards in production.</mark>**

---

## 12. Common CORS Configuration Mistakes (Critical Section)

🚨 Most real-world CORS vulnerabilities <mark>come from **misconfiguration**, not ignorance</mark>.

Developers often:

- "Just make it work"
- **<u>Copy-paste examples</u>**
- **<u>Disable restrictions during testing</u>**
- Forget to fix them

### ❌ Mistake 1 — `AllowAnyOrigin` + `AllowCredentials`

```csharp
policy.AllowAnyOrigin().AllowCredentials();
```

🚨 **<mark>This is INVALID and dangerous.</mark>**

**Why?**

- Browsers reject it
- Security risk
- ASP.NET Core blocks it

> 📌 Credentials <mark>require **explicit origins**</mark>.

### ❌ Mistake 2 — Using `AllowAnyOrigin()` in Production

```csharp
policy.AllowAnyOrigin();
```

**What happens?**

- **<u>Any</u>** website **<u>can read your API responses</u>**
- Users become vulnerable
- **<u>You lose control</u>**

> 📌 This is not security — <mark>**<u>it's exposure</u>**</mark>.

### ❌ Mistake 3 — Thinking CORS Secures the API

> "CORS blocks Postman, so we're safe."

❌ **False.**

CORS:

- ❌ Does not stop curl
- ❌ Does not stop mobile apps
- ❌ Does not stop attackers

> 📌 Authentication & authorization <mark>**<u>still required</u>**</mark>.

### ❌ Mistake 4 — Forgetting Ports

```csharp
.WithOrigins("https://localhost:3000")
```

But frontend runs on `https://localhost:5173`.

❌ **Result:**

- Browser blocks request
- "CORS error" confusion

> 📌 <mark>Port mismatch = **different origin**</mark>.

### ❌ Mistake 5 — Applying CORS Too Late

```csharp
app.MapControllers();
app.UseCors();
```

❌ CORS never runs.

> 📌 Middleware order <mark>**<u>is not optional</u>**</mark>.

---

## 13. Step 5 — Testing CORS Correctly (Very Important)

### ❌ Wrong Ways to Test

- Browser address bar
- Postman
- curl

### ✅ Correct Mental Test

- Browser-based frontend
- From allowed origin → ✅ works
- From untrusted origin → ❌ blocked by browser

📌 **Postman always works — and that is expected.**

---

## 14. Characteristics

- 🧬 No authentication yet
- 🧬 No JWT yet
- 🧬 No endpoint changes
- 🧬 Browser access is controlled
- 🧬 API surface is reduced

---

## 15. Interconnection

- 🔗 HTTPS → protects transport
- 🔗 CORS → controls browser access
- 🔗 Authentication → proves identity
- 🔗 Authorization → controls actions

📌 Each layer <mark>**solves a different problem**</mark>.

### 🛠️ Summary of Interconnections

- 🔹 HTTPS encrypts data
- 🔹 CORS protects users from malicious websites
- 🔹 Authentication protects identity
- 🔹 Authorization protects data ownership
- 🔹 Security <mark>**is layered** — not magical</mark>

---

## 16. Conclusion

You have now:

- ✅ Configured CORS correctly
- ✅ Avoided common real-world mistakes
- ✅ Controlled browser access safely

📌 But remember:

> 🚧 CORS does NOT secure your API.
> 🔐 <mark>It **<u>only controls who can</u>** read responses from browsers</mark>.

But the API still doesn't know:

❓ <mark>**Who is calling it**</mark>

📌 That changes next with authentication and JWT 🔐.

👉 **Next Lesson:** Adding JWT Authentication to the Student API

---

## 📌 Summary

| Step | Action                     | Where                                                                      |
| ---- | -------------------------- | -------------------------------------------------------------------------- |
| 1    | Define a named CORS policy | `builder.Services.AddCors(...)` before `Build()`                           |
| 2    | Apply it in the pipeline   | `app.UseCors("PolicyName")` before `UseAuthorization` and `MapControllers` |
| 3    | Preflight (`OPTIONS`)      | Handled automatically by ASP.NET Core when CORS is correct                 |
| 4    | Dev vs Prod origins        | Explicit in dev, **never** wildcard in prod                                |
| 5    | Test from a browser        | Not Postman, not curl                                                      |

| Mistake                               | Why it breaks                               |
| ------------------------------------- | ------------------------------------------- |
| `AllowAnyOrigin` + `AllowCredentials` | Invalid — browser & ASP.NET Core reject it  |
| `AllowAnyOrigin()` in prod            | Any site can read responses                 |
| Trusting CORS as API security         | Doesn't stop curl, Postman, mobile, backend |
| Wrong port in `WithOrigins`           | Different origin → blocked                  |
| `UseCors()` after `MapControllers()`  | CORS never runs                             |

**Core principle:** CORS is a browser-enforced rule <mark>**declared** by the server</mark> and <mark>**enforced** by the client</mark>. Configure it <mark>with **explicit origins**</mark>, place `UseCors()` <mark>**<u>before</u>**</mark> `MapControllers()`, and <mark>**<u>never</u>**</mark> treat it as API security. It protects **<mark>users from malicious sites</mark>**, <mark>not your API **from attackers**</mark>.

---


