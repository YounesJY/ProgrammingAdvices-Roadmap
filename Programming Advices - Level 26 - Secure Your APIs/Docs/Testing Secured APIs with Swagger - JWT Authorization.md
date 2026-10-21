# Testing Secured APIs with Swagger (JWT Authorization)

---

## 1. Introduction

At this stage, your API <mark>**is officially secured**</mark>:

- ✅ Login works
- ✅ JWT is issued
- ✅ Authentication middleware is enabled
- ✅ Student endpoints are protected with `[Authorize]`

But now students face a very common confusion:

❓ "JWT works… but <mark>**how do I send the token**</mark> when testing?"
❓ "Where is the Authorize button in Swagger?"

🎯 **Goal of this lesson**

Teach students how to:

- 🔹 Enable JWT support in Swagger
- 🔹 Locate the Authorize (🔒) button
- 🔹 Send JWT correctly
- 🔹 Test protected endpoints with confidence

This lesson turns security into something testable, not theoretical.

> 📌 We <mark>**need to enable the Authorize**</mark> button.

---

## 2. Important Reality Check (Very Important)

JWT authentication can work perfectly even if:

❌ Swagger does **NOT** show an Authorize button.

**Why?**

Because:

- Swagger is only a documentation & testing tool
- It does not automatically understand JWT

> 📌 We <mark>**must explicitly**</mark> teach Swagger <mark>**how authentication works**</mark>.

---

## 3. What the Authorize Button Really Does

The Authorize (🔒) button in Swagger:

- 🔹 Adds the JWT to request headers
- 🔹 <mark>**Automatically**</mark> sends it with every protected request
- 🔹 Saves you from <mark>**manually typing**</mark> headers each time

> 📌 **It does NOT change API security — <mark>it only helps testing</mark>.**

---

## 4. Step 1 — Why You Don't See the Authorize Button

If you don't see the 🔒 button:

- ❌ Swagger is not configured for JWT
- ❌ Your API is still secure
- ❌ Swagger just doesn't know how to send tokens

This is normal and expected.

---

## 5. Step 2 — Enable JWT Support in Swagger

📁 **File:** `Program.cs`

### ✅ Step 2.1 — Add Required Namespace

At the top of `Program.cs`:

```csharp
using Microsoft.OpenApi.Models;
```

### ✅ Step 2.2 — Configure Swagger for JWT

Replace your existing `AddSwaggerGen()` in `Program.cs` with:

```csharp
// Register Swagger generator and customize its behavior.
builder.Services.AddSwaggerGen(options =>
{
    // ===============================
    // 1) Define the JWT Bearer security scheme
    // ===============================
    //
    // This tells Swagger that our API uses JWT Bearer authentication
    // through the HTTP Authorization header.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // The name of the HTTP header where the token will be sent.
        Name = "Authorization",

        // Indicates this is an HTTP authentication scheme.
        Type = SecuritySchemeType.Http,

        // Specifies the authentication scheme name.
        // Must be exactly "Bearer" for JWT Bearer tokens.
        Scheme = "Bearer",

        // Optional metadata to describe the token format.
        BearerFormat = "JWT",

        // Specifies that the token is sent in the request header.
        In = ParameterLocation.Header,

        // Text shown in Swagger UI to guide the user.
        Description = "Enter: Bearer {your JWT token}"
    });

    // ===============================
    // 2) Require the Bearer scheme for secured endpoints
    // ===============================
    //
    // This tells Swagger that endpoints protected by [Authorize]
    // require the Bearer token defined above.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                // Reference the previously defined "Bearer" security scheme.
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            // No scopes are required for JWT Bearer authentication.
            // This array is empty because JWT does not use OAuth scopes here.
            new string[] {}
        }
    });
});
```

📌 **This tells Swagger:**

- JWT is used
- Token goes in the `Authorization` header
- Format is `Bearer <token>`

---

## 6. Step 3 — Restart the API (Critical)

⚠️ Swagger changes <mark>**require a full restart**</mark>.

1️⃣ Stop the API
2️⃣ Run again
3️⃣ Open Swagger

---

## 7. Step 4 — Where to Find the Authorize Button

After restart:

🔒 **Authorize button appears at the top-right corner of Swagger UI.**

📌 If you don't see it:

- Refresh the page
- Ensure Swagger configuration was added correctly

---

## 8. Step 5 — How to Use the Authorize Button (Exact Steps)

### 8.1 Get a JWT from Login

Call: `POST /api/Auth/login`

**Example body:**

```json
{
  "email": "ali.ahmed@student.com",
  "password": "password1"
}
```

Copy the returned token.

### 8.2 Click Authorize (🔒)

1️⃣ Click **Authorize**

2️⃣ In the input box, type exactly:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

⚠️ `Bearer` + space is mandatory.

3️⃣ Click **Authorize**

4️⃣ Click **Close**

### 8.3 Test a Protected Endpoint

Call: `GET /api/Students`

---

## 9. Expected Results

**❌ Without JWT:**

`401 Unauthorized`

**✅ With Valid JWT:**

`200 OK`

Student data is returned successfully.

---

## 10. What Swagger Is Doing Behind the Scenes

Swagger automatically adds this header:

```js
Authorization: Bearer <your-token>
```

<mark>To **every** protected request</mark>.

> 📌 <mark>**You don't manually**</mark> write headers anymore.

---

## 11. Common Beginner Mistakes (Warn Students)

- ❌ Forgetting the `Bearer` keyword
- ❌ Using an expired token
- ❌ Not restarting the API after Swagger changes
- ❌ Thinking the Swagger button affects security

---

## 12. Characteristics

- 🧬 <mark>**Swagger-only** configuration</mark>
- 🧬 <mark>**No impact** on production security</mark>
- 🧬 Token <mark>**reused across** requests</mark>
- 🧬 Beginner-friendly testing

---

## 13. Interconnection

- 🔗 Login → <mark>**issues**</mark> JWT
- 🔗 Swagger → <mark>**sends**</mark> JWT
- 🔗 Middleware → <mark>**validates**</mark> JWT
- 🔗 Controller → <mark>**trusts**</mark> identity

### 🛠️ Final Summary of Interconnections

- 🔹 Token issued → token tested
- 🔹 Swagger configured → easy validation
- 🔹 JWT enforced → real security

---

## 14. Conclusion

You have now:

- ✅ Enabled JWT testing in Swagger
- ✅ Found the Authorize button
- ✅ Sent JWT correctly
- ✅ Tested protected endpoints successfully

> 📌 **Your API is now <mark>secure and testable</mark> with Swagger :-)**

▶️ **Next Lesson:** 🧪 📖 Testing Secured Student API Using a C# Console Client

You will learn how to test your secured APIs using a real client in C# :-)

---

## 📌 Summary

| Step | Action                                      | Where                  |
| ---- | ------------------------------------------- | ---------------------- |
| 1    | Add `using Microsoft.OpenApi.Models;`       | Top of `Program.cs`    |
| 2    | Call `AddSecurityDefinition("Bearer", ...)` | Inside `AddSwaggerGen` |
| 3    | Call `AddSecurityRequirement(...)`          | Inside `AddSwaggerGen` |
| 4    | Restart the API                             | Anywhere               |
| 5    | Click 🔒 Authorize, enter `Bearer <token>`  | Swagger UI             |
| 6    | Test protected endpoint                     | Swagger UI             |

| Symptom                                 | Cause                                      |
| --------------------------------------- | ------------------------------------------ |
| No 🔒 button                            | Swagger not configured for JWT             |
| `401 Unauthorized` after entering token | Missing `Bearer ` prefix, or expired token |
| Button missing after config             | API not restarted                          |

**Core principle:** Swagger <mark>**is not**</mark> part of your API's security — <mark>it is a **testing convenience**</mark>. The API is secured by the middleware and `[Authorize]` attributes. Swagger just learns how to *send* the token so you can verify the security you already built.

---


