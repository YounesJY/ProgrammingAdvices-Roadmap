# Login Endpoint — Verifying Passwords & Issuing JWT

---

## 1. Introduction

So far, you have already built the correct foundation:

- ✅ Prepared the Student model for authentication
- ✅ Stored hashed passwords securely (BCrypt + salt)
- ✅ Understood what JWT is and how it works

Now we finally implement real authentication behavior.

🎯 **<mark>Goal of this lesson</mark>**

Create a login endpoint that:

- 🔹 **Verifies** student **<u>credentials securely</u>**
- 🔹 **<u>Issues</u>** a JWT after successful login
- 🔹 Does NOT store sessions
- 🔹 Does NOT mix authentication with student CRUD logic

> 📌 <mark>This lesson introduces **authentication behavior**</mark>, not authorization yet.

---

## 2. Very Important Design Rule (<mark>**Read Carefully**</mark>)

👉 **Authentication <mark>must NOT be</mark> inside `StudentsController`.**

**Why?**

Because:

- `StudentsController` = student data (CRUD)
- Authentication = identity & login logic

> 📌 Mixing them <mark>is a **design mistake**</mark>, even if the code "works".

---

## 3. Where the Login Endpoint Belongs

### ✅ Correct Location

📁 `Controllers/AuthController.cs`

This controller is responsible for:

- Login
- Authentication
- Token issuance

Later it can also handle:

- <mark>**<u>Refresh tokens</u>**</mark>
- <mark>**Logout**</mark>
- <mark>**<u>External providers</u>**</mark>

### 🧱 Recommended Project Structure

```js
StudentApi
│
├── Controllers
│   ├── StudentsController.cs   (student CRUD)
│   └── AuthController.cs       (authentication)
│
├── Models
│   ├── Student.cs
│   └── LoginRequest.cs
│
├── DataSimulation
│   └── StudentDataSimulation.cs
│
└── Program.cs
```

> 📌 This structure <mark>mirrors **real-world production** APIs</mark>.

---

## 4. Step 1 — Login Request Model

We define a request model for login.

📁 `Models/LoginRequest.cs`

```csharp
namespace StudentApi.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
```

📌 **Password is:**

- Sent <mark>**only during login**</mark>
- <mark>**Never** stored</mark>
- <mark>**Never** returned</mark>

---

## 5. Step 2 — Create AuthController

📁 `Controllers/AuthController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.DataSimulation;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentApi.Controllers
{
    // This controller is responsible for authentication-related actions,
    // such as logging in and issuing JWT tokens.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // This endpoint handles user login.
        // It verifies credentials and returns a JWT token if login succeeds.
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Step 1: Find the student by email from the in-memory data store.
            // Email acts as the unique login identifier.
            var student = StudentDataSimulation.StudentsList
                .FirstOrDefault(s => s.Email == request.Email);

            // If no student is found with the given email,
            // return 401 Unauthorized without revealing which field was wrong.
            if (student == null)
                return Unauthorized("Invalid credentials");

            // Step 2: Verify the provided password against the stored hash.
            // BCrypt handles hashing and salt internally.
            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(request.Password, student.PasswordHash);

            // If the password does not match the stored hash,
            // return 401 Unauthorized.
            if (!isValidPassword)
                return Unauthorized("Invalid credentials");

            // Step 3: Create claims that represent the authenticated user's identity.
            // These claims will be embedded inside the JWT.
            var claims = new[]
            {
                // Unique identifier for the student
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),

                // Student email address
                new Claim(ClaimTypes.Email, student.Email),

                // Role (Student or Admin) used later for authorization
                new Claim(ClaimTypes.Role, student.Role)
            };

            // Step 4: Create the symmetric security key used to sign the JWT.
            // This key must match the key used in JWT validation middleware.
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));

            // Step 5: Define the signing credentials.
            // This specifies the algorithm used to sign the token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Step 6: Create the JWT token.
            // The token includes issuer, audience, claims, expiration, and signature.
            var token = new JwtSecurityToken(
                issuer: "StudentApi",
                audience: "StudentApiUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            // Step 7: Return the serialized JWT token to the client.
            // The client will send this token with future requests.
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
```

---

## 6. Step 3 — Install Required Packages

1️⃣ Right-click your project
2️⃣ Manage NuGet Packages
3️⃣ Go to **Browse**
4️⃣ Search and install:

- `System.IdentityModel.Tokens.Jwt`
- `Microsoft.IdentityModel.Tokens`

> 📌 Make sure <mark>both are installed</mark> successfully.

---

## 7. Step 4 — Login Flow (What Actually Happens)

1️⃣ Student sends email + password
2️⃣ API finds the student by email
3️⃣ API verifies password using BCrypt
4️⃣ API builds identity claims
5️⃣ API issues a signed JWT

> 📌 No session is stored.
> 📌 Identity <mark>**travels inside the token**</mark>.

---

## 8. Final Login Endpoint

**Endpoint:** `POST /api/Auth/login`

**Example Request:**

```json
{
  "email": "ali.ahmed@student.com",
  "password": "password1"
}
```

**Example Response:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

---

## 9. What This Endpoint Does NOT Do

- ❌ Does not authorize actions
- ❌ Does not protect endpoints yet
- ❌ Does not check ownership

> 📌 Those responsibilities <mark>**come in later**</mark> lessons.

---

## 10. Characteristics

- 🧬 Stateless authentication
- 🧬 Secure password verification
- 🧬 JWT-based identity
- 🧬 Clean separation of concerns

---

## 11. Interconnection

- 🔗 Prepared data → possible login
- 🔗 BCrypt → verifies credentials
- 🔗 JWT → carries identity
- 🔗 Claims → used by authorization later

### 🛠️ Final Summary of Interconnections

- 🔹 Identity verified → JWT issued
- 🔹 JWT issued → future access control
- 🔹 Clean architecture → scalable security

---

## 12. Conclusion

You have now:

- ✅ Implemented a clean login endpoint
- ✅ Placed it in the correct controller
- ✅ Verified passwords securely
- ✅ Issued JWT correctly

📌 This is the backbone of modern API authentication.

▶️ **Next Lesson:** 🔒 Protecting Student Endpoints with JWT Authentication Middleware

This is where your API starts rejecting anonymous requests 🚀.

---

## 📌 Summary

| Step | What Happens                | Why                                   |
| ---- | --------------------------- | ------------------------------------- |
| 1    | Find student by email       | Email is the unique login identifier  |
| 2    | Verify password with BCrypt | Never compare plaintext               |
| 3    | Build claims                | Identity to embed in the token        |
| 4    | Create symmetric key        | Server signs and later verifies       |
| 5    | Sign with HMAC-SHA256       | Standard symmetric algorithm          |
| 6    | Create `JwtSecurityToken`   | Package claims + metadata + signature |
| 7    | Return token to client      | Client carries it on every request    |

**Note for your code:** the secret key `"THIS_IS_A_VERY_SECRET_KEY_123456"` <mark>**is hardcoded for learning**</mark>. In production, it must come from <mark>**environment variables or a secrets manager**</mark> (which the course covers in Module 7.5). Do not commit it to a public repo.

**Architecture rule:** `AuthController` handles identity. `StudentsController` handles data. <mark>**Never mix them**</mark>. This is what makes the API maintainable <mark>**when**</mark> you add <mark>**refresh tokens, logout, and role management**</mark> later.

---


