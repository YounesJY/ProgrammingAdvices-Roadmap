# 📖 Implementing Secure Logging & Auditing in the Student API

**(Practical Security Visibility)**

---

## 1. 🗝️ Introduction

You now understand:

- ✅ Why logs are a security feature
- ✅ Why failed logins must be logged
- ✅ Why admin actions must be audited
- ✅ Why sensitive data must never leak into logs

Now it's time to implement this visibility inside your Student API.

📌 **This lesson adds security visibility without changing authentication or authorization logic.**

<img title="" src="https://uploads.teachablecdn.com/attachments/377c0e924319434eae14bc8bd9b8f1e3.png" alt="" width="321" />

---

## 2. 🔍 What We Will Implement

By the end of this lesson, your API will:

- 🔹 Log failed login attempts
- 🔹 Log forbidden access (`403`)
- 🔹 Audit admin actions
- 🔹 Avoid logging sensitive data

📌 **We will log events, not secrets.**

---

## 3. 🔹 Step 1 — Use ASP.NET Core Built-in Logging

ASP.NET Core already provides a secure logging abstraction:

```csharp
ILogger<T>
```

📌 **Never use `Console.WriteLine` for security logs.**

---

## 4. 🔹 Step 2 — Inject Logger into `AuthController`

In `AuthController`, inject an `ILogger<AuthController>`.

**Update constructor:**

```csharp
private readonly ILogger<AuthController> _logger;

public AuthController(ILogger<AuthController> logger)
{
    _logger = logger;
}
```

📌 **This allows structured, centralized logging.**

---

## 5. 🔹 Step 3 — Log Failed Login Attempts (Safely)

Add **safe** logging to Login failure paths.

📋 **Inside `Login()`:**

### 5.1 Capture IP Once

```csharp
var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
```

### 5.2 Update the `student == null` Block

**Replace:**

```csharp
if (student == null)
    return Unauthorized("Invalid credentials");
```

**With:**

```csharp
if (student == null)
{
    _logger.LogWarning(
        "Failed login attempt (email not found). Email={Email}, IP={IP}",
        request.Email,
        ip
    );

    return Unauthorized("Invalid credentials");
}
```

### 5.3 Update the `!isValidPassword` Block

**Replace:**

```csharp
if (!isValidPassword)
    return Unauthorized("Invalid credentials");
```

**With:**

```csharp
if (!isValidPassword)
{
    _logger.LogWarning(
        "Failed login attempt (bad password). Email={Email}, IP={IP}",
        request.Email,
        ip
    );

    return Unauthorized("Invalid credentials");
}
```

✅ **This stays safe:** no password, no tokens.

---

## 6. 🔹 Step 4 — Log Successful Login (Optional, Low Noise)

Right before `return Ok(new TokenResponse { ... })`, add:

```csharp
_logger.LogInformation(
    "Successful login. UserId={UserId}, Email={Email}, IP={IP}",
    student.Id,
    student.Email,
    ip
);
```

📌 **Useful for tracing suspicious behavior later.**

---

## 7. 🔹 Step 5 — Log Refresh Token Abuse (Important)

In the `refresh` endpoint, log invalid or revoked attempts:

```csharp
_logger.LogWarning(
    "Invalid refresh token attempt. Email={Email}, IP={IP}",
    request.Email,
    ip
);
```

📌 **Refresh abuse is often silent — logs expose it.**

---

## 8. 🔹 Step 6 — Log Forbidden Access (403) Globally

**Add a middleware to capture authorization failures.**

### 8.1 Add the `using`

```csharp
using System.Security.Claims;
```

### 8.2 Add the Middleware (After `UseAuthorization`)

**Your pipeline should become:**

```csharp
app.UseAuthentication();
app.UseAuthorization();

// ✅ Step 6: Global 403 logging middleware (place it HERE)
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var path = context.Request.Path.ToString();

        // ✅ Centralized security log for authorization abuse
        app.Logger.LogWarning(
            "Forbidden access. UserId={UserId}, Path={Path}, IP={IP}",
            userId,
            path,
            ip
        );
    }
});

app.MapControllers();
```

📌 **This logs authorization abuse centrally.**

### 8.3 Why This Works

- ✅ It logs any `403` from anywhere in your API
- ✅ You don't need to add logging to every controller
- ✅ It captures:
  - **Who** (UserId or anonymous)
  - **What** (Path)
  - **From where** (IP)

---

## 9. 🔹 Step 7 — Audit Admin Actions (Manual but Explicit)

For admin-only endpoints, log **before** or **after** the action.

**Example in `StudentsController`:**

```csharp
_logger.LogInformation(
    "Admin action executed. AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}",
    User.FindFirstValue(ClaimTypes.NameIdentifier),
    studentId
);
```

📌 **Audit logs must include:**

- 🔹 **Who**
- 🔹 **What**
- 🔹 **Target**

### 9.1 Full Example (Delete Endpoint)

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("{id}", Name = "DeleteStudent")]
public ActionResult DeleteStudent(int id)
{
    var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown";

    if (id < 1)
    {
        _logger.LogWarning(
            "Admin action blocked (invalid id). AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}, IP={IP}",
            adminId,
            id,
            ip
        );
        return BadRequest($"Not accepted ID {id}");
    }

    var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

    if (student == null)
    {
        _logger.LogWarning(
            "Admin action failed (target not found). AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}, IP={IP}",
            adminId,
            id,
            ip
        );
        return NotFound($"Student with ID {id} not found.");
    }

    // ✅ Audit BEFORE deleting (recommended)
    _logger.LogInformation(
        "Admin action started. AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}, TargetEmail={TargetEmail}, IP={IP}",
        adminId,
        student.Id,
        student.Email,
        ip
    );

    StudentDataSimulation.StudentsList.Remove(student);

    // ✅ Audit AFTER deleting (optional, confirms success)
    _logger.LogInformation(
        "Admin action succeeded. AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}, IP={IP}",
        adminId,
        id,
        ip
    );

    return Ok($"Student with ID {id} has been deleted.");
}
```

### 9.2 Why "Before" and "After"

- **Before:** If the delete throws, you still have the record of the *attempt*.
- **After:** Confirms the *success*.

📌 **Both together give you the full story.**

---

## 10. 🔹 Step 8 — NEVER Log Sensitive Data

Before logging, always ask:

> ❓ **Can this value be reused to gain access?**

If yes → ❌ **DO NOT LOG IT.**

**Never log:**

- ❌ Passwords
- ❌ JWTs
- ❌ Refresh tokens
- ❌ API keys

📌 **Log events, not payloads.**

---

## 11. 🔹 Step 9 — Log Levels (Very Important)

Use correct severity levels:

| Level            | When to Use                                       |
| ---------------- | ------------------------------------------------- |
| `LogInformation` | Normal security events (successes)                |
| `LogWarning`     | Suspicious behavior (failures, abuse attempts)    |
| `LogError`       | Failures or security issues (exceptions, crashes) |

📌 **Severity helps filtering and alerting.**

---

## 12. 🧬 Characteristics

- 🧬 Centralized security visibility
- 🧬 Logs failures and abuse
- 🧬 Audits privileged actions
- 🧬 No sensitive data exposure
- 🧬 Supports incident investigation

---

## 13. 🔗 Interconnection

- 🔗 Authentication → identity
- 🔗 Authorization → permissions
- 🔗 Rate limiting → prevention
- 🔗 Logging → detection
- 🔗 Auditing → accountability

📌 **Security is a system, not a feature.**

### 🛠️ Summary of Interconnections

- 🔹 Logs **reveal attacks**
- 🔹 Audits **prove responsibility**
- 🔹 Safe logging **prevents secondary breaches**
- 🔹 Visibility **enables response**

📌 **You cannot secure what you cannot observe.**

---

## 14. 🏁 Conclusion

You have now:

- ✅ Added security visibility
- ✅ Logged failed logins and refresh abuse
- ✅ Audited admin actions
- ✅ Protected logs from leaking secrets

📌 **Your Student API is no longer just secure — it is observable, traceable, and defensible.**

---

## 📌 Summary

| Step | What                   | Where                                          |
| ---- | ---------------------- | ---------------------------------------------- |
| 1    | Use `ILogger<T>`       | Everywhere                                     |
| 2    | Inject logger          | `AuthController` (and `StudentsController`)    |
| 3    | Log failed logins      | `Login` failure paths                          |
| 4    | Log successful login   | End of `Login`                                 |
| 5    | Log refresh abuse      | `Refresh` failure paths                        |
| 6    | Log `403` globally     | Pipeline middleware (after `UseAuthorization`) |
| 7    | Audit admin actions    | `DeleteStudent`, other admin endpoints         |
| 8    | Never log secrets      | Everywhere                                     |
| 9    | Use correct log levels | Everywhere                                     |

| Log Level        | Meaning                                       |
| ---------------- | --------------------------------------------- |
| `LogInformation` | Success events                                |
| `LogWarning`     | Failures, abuse attempts, suspicious activity |
| `LogError`       | Exceptions, crashes                           |

| What to Log               | What NOT to Log |
| ------------------------- | --------------- |
| UserId                    | Passwords       |
| Email                     | JWT tokens      |
| IP address                | Refresh tokens  |
| Endpoint path             | API keys        |
| Action name               | Session IDs     |
| Outcome (success/failure) | Full PII        |

**Core principle:** logging and auditing are **not optional extras** — they are the difference between *"we think nothing happened"* and *"we can prove what happened, when, and by whom."* The middleware and the controller are the **protection**. The logs and audits are the **proof**. And the proof is what lets you investigate, respond, and hold people accountable — including the people you trust with admin power. **You cannot secure what you cannot observe.**
