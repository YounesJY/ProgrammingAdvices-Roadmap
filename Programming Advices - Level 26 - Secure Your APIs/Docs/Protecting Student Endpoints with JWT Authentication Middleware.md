# Protecting Student Endpoints with JWT Authentication Middleware

---

## 1. Introduction

So far, you have achieved something critical:

- ✅ Students can log in
- ✅ Passwords are verified securely
- ✅ The API issues a JWT
- ✅ Identity is now represented as a token

<mark>But **there is a big problem**</mark>:

❓ **<mark>Anyone can still</mark> access Student endpoints.**

Right now:

- Login works
- JWT exists
- But <mark>the API **does not enforce authentication yet**</mark>

🎯 **Goal of this lesson**

Make the API say:

> "If you don't have a valid JWT — <mark>**you are not allowed in**</mark>."

---

## 2. What JWT Authentication Middleware Does

JWT authentication middleware <mark>is the **security guard**</mark> of your API.

For every incoming request, it:

1️⃣ Looks for a JWT
2️⃣ Validates the token
3️⃣ Extracts identity claims
4️⃣ Rejects invalid or missing tokens

> 📌 **This happens <mark>before your controller code runs</mark>.**

---

## 3. Important Rule (<mark>**Mental Model**</mark>)

- 🔐 **Login** <mark>**<u>issues</u> the token**</mark>
- 🔒 **Middleware** <mark>**<u>enforces</u> the token**</mark>

> They are <mark>two different **responsibilities**</mark>.

---

## 4. Step 1 — Install Required JWT Packages

Locate and install these packages:

- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `System.IdentityModel.Tokens.Jwt`

📌 Make sure to select the right version (e.g., `8.0.0`, or whichever matches your .NET target).

📌 **These packages allow ASP.NET Core to:**

- Read JWTs
- Validate signatures
- Extract claims

---

## 5. Step 2 — Configure JWT Authentication in Program.cs

This is where the middleware is registered.

📁 `Program.cs`

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Create the application builder.
// This object is responsible for configuring services and middleware.
var builder = WebApplication.CreateBuilder(args);

// ===============================
// JWT Authentication Configuration
// ===============================

// Register authentication services in the dependency injection container.
// JwtBearerDefaults.AuthenticationScheme tells ASP.NET Core that
// JWT Bearer authentication will be the default authentication method.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // TokenValidationParameters define how incoming JWTs will be validated.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Ensures the token was issued by a trusted issuer.
            ValidateIssuer = true,

            // Ensures the token is intended for this API (audience check).
            ValidateAudience = true,

            // Ensures the token has not expired.
            ValidateLifetime = true,

            // Ensures the token signature is valid and was signed by the API.
            ValidateIssuerSigningKey = true,

            // The expected issuer value (must match the issuer used when creating the JWT).
            ValidIssuer = "StudentApi",

            // The expected audience value (must match the audience used when creating the JWT).
            ValidAudience = "StudentApiUsers",

            // The secret key used to validate the JWT signature.
            // This must be the same key used when generating the token.
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"))
        };
    });

// ===============================
// Authorization Configuration
// ===============================

// Register authorization services.
// This enables attributes like [Authorize] and role-based authorization.
builder.Services.AddAuthorization();

// Register controller support.
builder.Services.AddControllers();

// ===============================
// Swagger Configuration
// ===============================

// Enables Swagger endpoint discovery.
builder.Services.AddEndpointsApiExplorer();

// Enables Swagger UI for testing and documentation.
builder.Services.AddSwaggerGen();

// Build the application.
// After this point, services are frozen and middleware is configured.
var app = builder.Build();

// ===============================
// HTTP Request Pipeline
// ===============================

// Enable Swagger only in development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// IMPORTANT:
// Authentication middleware must run BEFORE authorization middleware.
// Authentication identifies the user.
// Authorization decides what the user is allowed to do.
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes (e.g., /api/Students, /api/Auth).
app.MapControllers();

// Start the application.
app.Run();
```

> 📌 **<mark>Order matters</mark>** — authentication must come before authorization.

---

## 6. Step 3 — Protect Student Endpoints Using `[Authorize]`

Now we tell the API which endpoints require authentication.

📁 `Controllers/StudentsController.cs`

```csharp
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    // Protected endpoints
}
```

📌 **This means:**

- Every endpoint inside this controller
- <mark>**<u>Requires a valid JWT</u>**</mark>

---

## 7. What Happens Now (Critical Understanding)

**❌ Without JWT:**

```js
GET /api/Students
```

→ `401 Unauthorized`

**✅ With Valid JWT:**

```js
GET /api/Students
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

→ `200 OK`

**<mark>The middleware</mark>:**

- Validates the token
- Extracts claims
- Allows the request to proceed

---

## 8. How Identity Reaches the Controller

Once authenticated, the controller <mark>**can access identity**</mark>:

```csharp
User.Identity.IsAuthenticated
User.FindFirst(ClaimTypes.NameIdentifier)
User.FindFirst(ClaimTypes.Role)
```

📌 **<mark>The controller <u>does not parse</u> JWT manually</mark>.**

---

## 9. What This Protection Does NOT Do (Important)

JWT authentication middleware:

- ❌ Does NOT <mark>**check roles**</mark>
- ❌ Does NOT <mark>**check ownership**</mark>
- ❌ Does NOT <mark>**enforce business rules**</mark>

<mark>**<u>It only answers</u>**</mark>:

> "Is this request coming from a valid, authenticated user?"

---

## 10. Common Beginner Mistakes (Warn Students)

- ❌ Forgetting `app.UseAuthentication()`
- ❌ <mark>**Wrong middleware order**</mark> (`UseAuthorization` before `UseAuthentication`)
- ❌ Hardcoding token validation values <mark>**that don't match**</mark> the token issuer
- ❌ Thinking `[Authorize]` means role checking

---

## 11. Characteristics

- 🧬 <mark>**Stateless authentication**</mark>
- 🧬 Token validated on every request
- 🧬 <mark>**Identity injected automatically**</mark>
- 🧬 No session storage

---

## 12. Interconnection

- 🔗 Login → <mark>**issues**</mark> JWT
- 🔗 Middleware → <mark>**validates**</mark> JWT
- 🔗 Controller → <mark>**<u>trusts identity</u>**</mark>
- 🔗 Authorization → comes next

### 🛠️ Final Summary of Interconnections

- 🔹 Token issued → token enforced
- 🔹 Identity verified → request allowed
- 🔹 No token → request rejected

---

## 13. Conclusion

You have now:

- ✅ Enabled JWT authentication middleware
- ✅ Protected Student endpoints
- ✅ Blocked anonymous access
- ✅ Turned JWT into real security

📌 **Your API is now authenticated, not just "token-aware".**

▶️ **Next Lesson:** Testing Secured APIs with Swagger (JWT Authorization)

You will learn how to test your secured APIs using Swagger and how to enable the authorization button :-)

---

## 📌 Summary

| Step | Action                                                                                      | Where                   |
| ---- | ------------------------------------------------------------------------------------------- | ----------------------- |
| 1    | Install `Microsoft.AspNetCore.Authentication.JwtBearer` + `System.IdentityModel.Tokens.Jwt` | NuGet                   |
| 2    | Register `AddAuthentication(...).AddJwtBearer(...)` with `TokenValidationParameters`        | `Program.cs` (services) |
| 3    | Register `AddAuthorization()`                                                               | `Program.cs` (services) |
| 4    | Add `app.UseAuthentication()` **before** `app.UseAuthorization()`                           | `Program.cs` (pipeline) |
| 5    | Add `[Authorize]` to the controller                                                         | `StudentsController.cs` |

| Concept           | Value                                                             |
| ----------------- | ----------------------------------------------------------------- |
| Token issuer      | `StudentApi`                                                      |
| Audience          | `StudentApiUsers`                                                 |
| Signing algorithm | HMAC-SHA256                                                       |
| Key source        | Hardcoded for learning; must be env var / Key Vault in production |
| Middleware order  | HTTPS → CORS → Authentication → Authorization → Controllers       |

**Core principle:** login *issues* the token; middleware *enforces* the token. <mark>**They are two separate responsibilities**</mark> — <mark>one **builds identity**</mark>, <mark>the other **<u>trusts it</u>**</mark>. `[Authorize]` is just the switch <mark>**that tells the pipeline**</mark>: "reject any request that arrives without a validated identity."

**One thing to flag:** the secret key `"THIS_IS_A_VERY_SECRET_KEY_123456"` is now duplicated in two files (issuing in `AuthController`, validating in `Program.cs`). If one changes and the other doesn't, <mark>**every token becomes invalid**</mark>. The course <mark>**<u>*will fix this*</u>**</mark> in Module 7.5 (<mark>**<u>secrets management</u>**</mark>), but it's worth noting now — <mark>**<u>it is a classic production bug</u>**</mark>.


