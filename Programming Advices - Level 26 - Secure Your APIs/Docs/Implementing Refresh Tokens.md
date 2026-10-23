# Implementing Refresh Tokens

**<mark>Access Token + Refresh Token + Rotation + Logout (Student API, <u>In-Memory</u>)</mark>**

---

## 1. 🗝️ Introduction

Your current JWT login is correct, <mark>but it creates **a real problem**</mark>:

- Access tokens expire (**<mark>good for security</mark>**)
- Users would need to re-login often (**<mark>bad for usability</mark>**)

Refresh tokens <mark>**<u>solve this</u>**</mark> by adding <mark>a **renewal mechanism**</mark>:

- **Access Token:** short-lived, used to call Student endpoints
- **Refresh Token:** long-lived, used <mark>**only**</mark> to get a new access token

In this lesson you will implement:

- Login returns <mark>`AccessToken` + `RefreshToken`</mark>
- Refresh endpoint <mark>issues **a new** `AccessToken` and rotates `RefreshToken`</mark>
- Logout <mark>**revokes the <u>refresh token</u>**</mark> (<mark>**cuts**</mark> the renewal path)

---

## 2. 🎯 Outcome

By the end of this lesson, your API will support:

- ✅ Short-lived access tokens (safer)
- ✅ Long sessions without re-login (usable)
- ✅ Refresh token rotation (professional-grade)
- ✅ Logout support for stateless auth (correct model)

---

## 3. 🧱 Step 1 — Add Auth DTOs (Correct Placement)

### ✅ Create folders

```text
StudentApi
├─ DTOs
│  └─ Auth
│     ├─ TokenResponse.cs
│     ├─ RefreshRequest.cs
│     └─ LogoutRequest.cs
```

### ✅ DTO: `TokenResponse.cs`

```csharp
namespace StudentApi.DTOs.Auth
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
```

### ✅ DTO: `RefreshRequest.cs`

```csharp
namespace StudentApi.DTOs.Auth
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
```

### ✅ DTO: `LogoutRequest.cs`

```csharp
namespace StudentApi.DTOs.Auth
{
    public class LogoutRequest
    {
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
```

📌 **Note:** Move your existing `LoginRequest` to `DTOs/Auth/LoginRequest.cs` for consistency.

---

## 4. 🧱 Step 2 — Update Student Model (Store Refresh Token State)

In `Models/Student.cs`, add:

```csharp
public string RefreshTokenHash { get; set; }
public DateTime? RefreshTokenExpiresAt { get; set; }
public DateTime? RefreshTokenRevokedAt { get; set; }
```

> 📌 <mark>**We store the hash**</mark>, <mark>**<u>not</u>**</mark> the refresh token itself.
> This is the same security logic as password hashing.

---

## 5. 🧱 Step 3 — Add Refresh Token Generator (Secure Random)

In `AuthController.cs`, add:

```csharp
using System.Security.Cryptography;

private static string GenerateRefreshToken()
{
    var bytes = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(bytes);
    return Convert.ToBase64String(bytes);
}
```

📌 **<mark>Why 64 bytes</mark>?**

- 64 bytes = 512 bits of cryptographic entropy.
- Practically <mark>**<u>impossible</u>** to brute-force</mark>.
- Base64 encoded for safe HTTP transport.

---

## 6. 🧱 Step 4 — Update Login Endpoint (Return Both Tokens)

### ✅ Add DTO namespace

At the top of `AuthController.cs`:

```csharp
using StudentApi.DTOs.Auth;
```

### ✅ Update the login action logic

After verifying credentials and generating the JWT access token, add refresh token creation + storage:

```csharp
var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

// Create refresh token (random)
var refreshToken = GenerateRefreshToken();

// Store refresh token securely (hash + expiry + not revoked)
student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
student.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
student.RefreshTokenRevokedAt = null;

return Ok(new TokenResponse
{
    AccessToken = accessToken,
    RefreshToken = refreshToken
});
```

📌 **Important:** Use `DateTime.UtcNow` consistently (avoid timezone surprises).

---

## 7. 🧱 Step 5 — Add Refresh Endpoint (Rotation)

Add to `AuthController`:

```csharp
[HttpPost("refresh")]
public IActionResult Refresh([FromBody] RefreshRequest request)
{
    var student = StudentDataSimulation.StudentsList
        .FirstOrDefault(s => s.Email == request.Email);

    if (student == null)
        return Unauthorized("Invalid refresh request");

    if (student.RefreshTokenRevokedAt != null)
        return Unauthorized("Refresh token is revoked");

    if (student.RefreshTokenExpiresAt == null || student.RefreshTokenExpiresAt <= DateTime.UtcNow)
        return Unauthorized("Refresh token expired");

    bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, student.RefreshTokenHash);
    if (!refreshValid)
        return Unauthorized("Invalid refresh token");

    // Issue NEW access token (same claims & signing settings as login)
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
        new Claim(ClaimTypes.Email, student.Email),
        new Claim(ClaimTypes.Role, student.Role)
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var jwt = new JwtSecurityToken(
        issuer: "StudentApi",
        audience: "StudentApiUsers",
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: creds
    );

    var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

    // Rotation: replace refresh token
    var newRefreshToken = GenerateRefreshToken();
    student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken);
    student.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
    student.RefreshTokenRevokedAt = null;

    return Ok(new TokenResponse
    {
        AccessToken = newAccessToken,
        RefreshToken = newRefreshToken
    });
}
```

📌 **Rotation rule:** The old refresh token becomes useless after one use.

---

## 8. 🧱 Step 6 — Add Logout Endpoint (Revoke Refresh Token)

Add to `AuthController`:

```csharp
[HttpPost("logout")]
public IActionResult Logout([FromBody] LogoutRequest request)
{
    var student = StudentDataSimulation.StudentsList
        .FirstOrDefault(s => s.Email == request.Email);

    if (student == null)
        return Ok(); // Do not reveal if user exists

    bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, student.RefreshTokenHash);
    if (!refreshValid)
        return Ok();

    student.RefreshTokenRevokedAt = DateTime.UtcNow;
    return Ok("Logged out successfully");
}
```

📌 **Stateless logout** = *"cut the renewal path"* (revoke the refresh token), not *"delete the access token"*.

---

## 9. ✅ Testing (Swagger + Console Client)

### 9.1 Swagger

1️⃣ Login → copy `AccessToken` and `RefreshToken`
2️⃣ Authorize Swagger with: `Bearer <AccessToken>`
3️⃣ Call secured endpoint: `GET /api/Students/All`
4️⃣ Call `POST /api/Auth/refresh` (Email + RefreshToken)
5️⃣ Replace the Bearer token with the new `AccessToken`
6️⃣ Try refresh again using the **old** RefreshToken → should fail
7️⃣ Logout using the current RefreshToken → refresh should fail after logout

### 9.2 Next Lesson — Console Client

Update your console client to:

- Store **both** tokens
- Use the `/refresh` endpoint when the access token expires

---

## 📌 Summary

| Step | What                                                          | Why                                                    |
| ---- | ------------------------------------------------------------- | ------------------------------------------------------ |
| 1    | Add DTOs (`TokenResponse`, `RefreshRequest`, `LogoutRequest`) | Clean separation of request/response shapes            |
| 2    | Add `RefreshTokenHash`, `ExpiresAt`, `RevokedAt` to `Student` | Track refresh token state without storing it plaintext |
| 3    | `GenerateRefreshToken()`                                      | Cryptographically secure random token                  |
| 4    | Update `Login` to return both tokens                          | Client needs both to operate                           |
| 5    | Add `/refresh` with rotation                                  | Silent renewal + theft detection                       |
| 6    | Add `/logout`                                                 | Cut the renewal path                                   |
| 7    | Test in Swagger                                               | Verify rotation and revocation work                    |

| Concept                    | Rule                                                 |
| -------------------------- | ---------------------------------------------------- |
| **Refresh token storage**  | Hash only — same as passwords                        |
| **Refresh token lifespan** | 7 days (course)                                      |
| **Access token lifespan**  | 30 minutes (course)                                  |
| **Rotation**               | Old token dies after one use                         |
| **Logout**                 | Revoke refresh token; access token expires naturally |
| **Timestamps**             | Always `DateTime.UtcNow`                             |

**Core principle:** the access token is a temporary permission slip; the refresh token is the long-term session. The server **never trusts a refresh token just because it came in the request** — it always checks the hash, the expiration, and the revocation state. Rotation + logout complete the lifecycle: the client can renew silently, and the server can cut the renewal path at any time.
