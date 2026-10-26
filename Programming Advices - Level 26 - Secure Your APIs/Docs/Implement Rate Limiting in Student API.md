# Implement Rate Limiting in Student API

**(Fixed Window + Per IP + 429) — Practical**

---

## 1. 🗝️ Introduction

You already understand **<mark>why</mark>** rate limiting is needed.

Now we implement it step by step inside your Student API using:

- ✅ **Fixed Window** rate limiting
- ✅ **Per IP** strategy
- ✅ **429 Too Many Requests** response
- ✅ Applied only to:
  - 🔹 `POST /api/Auth/Login`
  - 🔹 `POST /api/Auth/Refresh`

> 📌 **We <mark>will not change</mark> your JWT logic at all.**
> <mark>We only</mark> add <mark>a **protective layer** before it</mark>.

---

## 2. 🔹 Step 1 — Add Required `using` Statements in `Program.cs`

At the top of your `Program.cs`, add:

```csharp
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
```

📌 These give you access to <mark>**built-in ASP.NET Core rate limiting**</mark>.

---

## 3. 🔹 Step 2 — Register Rate Limiting Service (DI)

In `Program.cs`, add this **before** `builder.Services.AddControllers();`:

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("AuthLimiter", httpContext =>
    {
        var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
});
```

### ✅ Meaning

- 🔹 **<mark>5 requests per minute</mark>** per IP
- 🔹 **<mark><u>No queueing</u></mark>** for login attempts
- 🔹 Exceeding limit → `429` automatically

---

## 4. 🔹 Step 3 — Add Rate Limiting Middleware

In `Program.cs`, add this middleware:

✅ **<mark>Place it <u>before</u></mark> authentication and authorization.**

```csharp
app.UseRateLimiter();
```

So your pipeline becomes:

```csharp
app.UseHttpsRedirection();

app.UseRateLimiter();      // ✅ before auth

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
```

📌 **This ensures abusive requests <mark>are blocked <u>early</u></mark>, <mark><u>before</u> expensive work</mark>.**

---

## 5. 🔹 Step 4 — Add Required `using` in `AuthController`

At the top of your `AuthController.cs`, add:

```csharp
using Microsoft.AspNetCore.RateLimiting;
```

---

## 6. 🔹 Step 5 — Apply Rate Limit Policy to Login Endpoint

Add the attribute above `Login`:

```csharp
[HttpPost("login")]
[EnableRateLimiting("AuthLimiter")]
public IActionResult Login([FromBody] LoginRequest request)
{
    // ...
}
```

✅ **Now login <mark>is protected</mark>.**

---

## 7. 🔹 Step 6 — Apply Rate Limit Policy to Refresh Endpoint

Add the attribute above `Refresh`:

```csharp
[HttpPost("refresh")]
[EnableRateLimiting("AuthLimiter")]
public IActionResult Refresh([FromBody] RefreshRequest request)
{
    // ...
}
```

✅ **Now refresh <mark>is protected</mark>.**

---

## 8. 🔹 Step 7 — What Happens When Limit Is Exceeded?

When a client exceeds the limit:

- ❌ Request blocked
- 🔹 Status: `429 Too Many Requests`
- 🔹 Controller code **does not execute**

📌 **Your login/refresh code <mark>remains <u>untouched</u></mark>.**

---

## 9. 🔹 Step 8 — Optional Safe Message (Recommended)

If you want a friendly message **without exposing internal limits**, add this after `app.UseRateLimiter();`:

```csharp
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
    {
        await context.Response.WriteAsync("Too many login attempts. Please try again later.");
    }
});
```

📌 **<mark><u>Do not</u></mark> include <mark>the <u>exact numbers</u></mark> in the message.**

*(Revealing "5 per minute" helps attackers calibrate their bots. A generic message is safer.)*

---

## 10. 🧬 Characteristics

- 🧬 Protects Login and Refresh endpoints
- 🧬 Stops abuse before controller logic
- 🧬 Returns proper HTTP `429`
- 🧬 Keeps JWT logic unchanged
- 🧬 Improves stability under attack

---

## 11. 🔗 Interconnection

- 🔗 Login verifies identity
- 🔗 Refresh maintains session
- 🔗 Rate limiting prevents abuse before both

📌 **Authentication remains correct — and now becomes resilient.**

### 🛠️ Summary of Interconnections

- 🔹 JWT proves identity → rate limiting protects entry points
- 🔹 Refresh extends session → rate limiting protects renewal path
- 🔹 Security layers must survive hostile traffic

---

## 12. 🏁 Conclusion

You have now implemented:

- ✅ Rate limiting middleware
- ✅ Fixed window rule (5/min per IP)
- ✅ `429` response handling
- ✅ Protection on login + refresh endpoints

📌 **Your Student API is now protected from brute-force and abuse — using clean middleware, not controller hacks.**

---

## 📌 Summary

| Step | Action                                      | Where                             |
| ---- | ------------------------------------------- | --------------------------------- |
| 1    | Add `using` statements                      | Top of `Program.cs`               |
| 2    | Register `AddRateLimiter(...)`              | Services (before `Build()`)       |
| 3    | Add `app.UseRateLimiter()`                  | Pipeline (before auth)            |
| 4    | Add `using` in controller                   | `AuthController.cs`               |
| 5    | Apply `[EnableRateLimiting("AuthLimiter")]` | Login endpoint                    |
| 6    | Apply `[EnableRateLimiting("AuthLimiter")]` | Refresh endpoint                  |
| 7    | Optional: friendly `429` message            | Middleware after `UseRateLimiter` |

| Concept         | Value (Course)          |
| --------------- | ----------------------- |
| **Strategy**    | Fixed Window            |
| **Scope**       | Per IP                  |
| **Limit**       | 5 requests / minute     |
| **Queue**       | 0 (reject immediately)  |
| **Status code** | `429 Too Many Requests` |
| **Applied to**  | Login + Refresh only    |

**Core principle:** rate limiting <mark>**<u>is not</u>** about **catching attackers**</mark> — it's about <mark>making their attempt **mathematically <u>unprofitable</u>**</mark>. 5 requests per minute means a 1-million-password wordlist takes **139 days** to try, per IP. <mark>That **<u>alone</u>** **stops** 99% of automated abuse</mark>. The middleware <mark>runs **before**</mark> your controller, so the attack <mark>**<u>never even</u>**</mark> reaches your BCrypt hashing, your database, or your logging. **<mark>The cheapest way</mark> to handle an attack <mark>is to reject it <u>at the door</u></mark>.**

---
