# Configuration in ASP.NET Core: Priorities, Options Pattern, and Why Both `Get<T>` and `Configure<T>` Exist

---

## 1. Introduction

When you move from a tutorial (hardcoded secrets) to a production project (config files, environment variables, key vaults), the `Configuration` system **<mark>becomes the backbone</mark>** of every environment-aware decision.

This MD covers:

- 🔹 The priority order of configuration providers.
- 🔹 What `.env` files actually do (and do not do).
- 🔹 The `JwtSettings` pattern (strongly-typed config).
- 🔹 Why `Get<T>()` and `Configure<T>()` both exist.
- 🔹 The runtime lifetime of `IOptions<T>`.

---

## 2. The Priority Order (Highest Wins)

ASP.NET Core builds a **<mark>single configuration tree</mark>** from multiple providers. When the same key appears in two providers, the **later provider overrides the earlier one**.

**Default order (lowest to highest priority):**

| Priority    | Source                                                       | Notes                                        |
| ----------- | ------------------------------------------------------------ | -------------------------------------------- |
| 1 (lowest)  | `appsettings.json`                                           | Defaults, safe to commit                     |
| 2           | `appsettings.{Environment}.json` (e.g., `.Development.json`) | Per-environment overrides, often git-ignored |
| 3           | **User Secrets** (Development only)                          | `dotnet user-secrets` — never committed      |
| 4           | **Environment Variables**                                    | Production secrets, cloud provider settings  |
| 5           | **Command-line arguments**                                   | Highest — overrides everything               |
| 6 (highest) | (Optional) Custom providers                                  | Azure Key Vault, Consul, etc. — added last   |

📌 **<mark>The last provider</mark> to be added wins.** `WebApplication.CreateBuilder(args)` <mark>adds them</mark> in the order above.

### What this means in practice

- If `appsettings.json` has `JwtSettings:Issuer = "StudentApi"` and an environment variable `JwtSettings__Issuer` is set to `"ProductionApi"`, the environment variable **<mark>wins</mark>**.
- If a command-line argument `--JwtSettings:Issuer=Override` is passed, it **<mark>wins over everything</mark>**.

### The environment variable naming convention

| In JSON                     | In environment variable      |
| --------------------------- | ---------------------------- |
| `JwtSettings:SecretKey`     | `JwtSettings__SecretKey`     |
| `ConnectionStrings:Default` | `ConnectionStrings__Default` |

📌 **Double underscore `__` replaces the colon `:`.** The OS does not allow `:` in variable names.

---

## 3. What `.env` Files Actually Do

**<mark>They do nothing by default.</mark>** `.env` is not a .NET concept. It is **<u>a convention</u>** used by:

- Docker (`docker-compose` reads `.env` automatically).
- Node.js (`dotenv` library).
- Python (`python-dotenv`).

In .NET, `.env` has **<mark>no built-in reader</mark>**. If you want to use one, you must:

1. Install a library like `DotNetEnv`.
2. Call `DotNetEnv.Env.Load()` **<mark>before</mark>** `WebApplication.CreateBuilder(args)`.
3. The library reads `.env` and **injects** each entry into `Environment.SetEnvironmentVariable(...)`.
4. ASP.NET Core then **<mark>reads them as</mark>** environment variables (priority 4 above).

📌 `.env` **<mark><u>is not</u></mark>** a configuration provider. It **<mark>is a script</mark>** that populates environment variables.

**For your project:** you do not need `.env`. Use `appsettings.Development.json` for local, environment variables for production. `.env` is redundant unless you are using Docker.

---

## 4. The `JwtSettings` Pattern

### 4.1 Why not read raw strings?

Without a settings class, code looks like this:

```csharp
var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!));
var minutes = int.Parse(builder.Configuration["JwtSettings:AccessTokenMinutes"]!);
```

Problems:

- Typo in `"Secretkey"` compiles fine, fails at runtime.
- Type conversion **<mark>happens at runtime</mark>** (`int.Parse` throws).
- No IntelliSense.
- Every consumer **<mark>must know</mark>** the exact string paths.

### 4.2 The strongly-typed approach

```csharp
namespace StudentApi.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int AccessTokenMinutes { get; set; } = 30;
        public int RefreshTokenDays { get; set; } = 7;
    }
}
```

Now:

- Typo → **compile-time error**.
- Wrong type → **compile-time error**.
- IntelliSense works.
- One class documents the shape of the config.

📌 **`JwtSettings` is the "<mark>shape</mark>" of your config. <mark>It is not</mark> the value. The value comes from `appsettings.json`.**

---

## 5. Why Both `Get<T>()` and `Configure<T>()` Exist

This is the confusing part. Both bind the config section to `JwtSettings`, but for different reasons.

### 5.1 `Get<T>()` — Fail fast at startup

```csharp
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSection.Get<JwtSettings>()
    ?? throw new InvalidOperationException("JwtSettings section is missing.");
```

- **What it does:** **<mark><u>Binds the config</u></mark>** to a **new `JwtSettings` instance** immediately.
- **What it is for:** Validate at startup. If the secret is missing, **<mark><u>the app does not start</u></mark>**. You get an error <u>at boot</u>, not at the first login.
- **What it is NOT:** It does not register anything in DI. It is just a local variable.

📌 **Without this, the app starts even with an empty secret, and <mark><u>crashes later</u></mark> inside `GenerateAccessToken` with a cryptic `ArgumentNullException`.**

### 5.2 `Configure<T>()` — DI registration

```csharp
builder.Services.Configure<JwtSettings>(jwtSection);
```

- **What it does:** **<mark>Registers</mark>** `IOptions<JwtSettings>` in the DI container, **<mark>pointing</mark>** at the `JwtSettings` section.
- **What it is for:** Making the settings injectable in any service (middleware, controllers, hosted services).
- **When it actually binds:** **<mark>Lazily</mark>** — the first time someone accesses `IOptions<JwtSettings>.Value`.

📌 **Without this, the app starts, but any service that injects `IOptions<JwtSettings>` <mark>fails</mark> with a DI resolution error.**

### 5.3 Summary table

| Method           | Purpose              | Lifetime                | When it runs                            |
| ---------------- | -------------------- | ----------------------- | --------------------------------------- |
| `Get<T>()`       | Fail-fast validation | Local variable          | Startup, once                           |
| `Configure<T>()` | DI registration      | `IOptions<T>` singleton | Registration at startup, binding lazily |

**<mark><u>Both are needed</u></mark>.** They serve two different concerns:

- `Get<T>()` guarantees that misconfiguration is caught **<mark><u>immediately</u></mark>**.
- `Configure<T>()` makes the settings **<mark>available to consumers via DI</mark>**.

---

## 6. `IOptions<T>` vs `IOptionsSnapshot<T>` vs `IOptionsMonitor<T>`

These three interfaces resolve the same `JwtSettings` but **<mark>differ in lifetime and <u>reload behavior</u></mark>**.

| Interface             | Lifetime                        | Reloads on change?        | When to use                        |
| --------------------- | ------------------------------- | ------------------------- | ---------------------------------- |
| `IOptions<T>`         | Singleton (created once)        | ❌ No                      | **Default. Use for JWT, DB, etc.** |
| `IOptionsSnapshot<T>` | Scoped (re-created per request) | ✅ Yes (per request)       | Config that can change at runtime  |
| `IOptionsMonitor<T>`  | Singleton, but reloads          | ✅ Yes (with notification) | When you need a callback on change |

### For JWT settings

**`IOptions<T>` is correct.**

- The secret key **<mark>never changes</mark>** at runtime.
- If it did change, you would restart the app.
- No need for per-request reload.

📌 **`IOptionsSnapshot<T>` for JWT settings is a performance mistake — it re-binds on every request.** Do not use it.

---

## 7. How the Pattern Flows in the Project

### Startup

```csharp
var jwtSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSection.Get<JwtSettings>()
    ?? throw new InvalidOperationException("JwtSettings section is missing.");

if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey))
    throw new InvalidOperationException("JwtSettings:SecretKey is missing.");

builder.Services.Configure<JwtSettings>(jwtSection);
```

### Middleware (Phase 4)

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,          // ← local variable
            ValidAudience = jwtSettings.Audience,      // ← local variable
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),  // ← local variable
            ClockSkew = TimeSpan.Zero
        };
    });
```

### `AuthController` (Phase 5)

```csharp
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;

    public AuthController(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    private string GenerateAccessToken(StudentCredentialsDTO student)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenMinutes),
            signingCredentials: creds);
        // ...
    }
}
```

📌 **Middleware uses the local variable. Controller uses `IOptions<T>`. Both come from the same config source. <mark>They cannot mismatch</mark>.**

---

## 8. The Failure Modes (What Goes Wrong Without This)

| Mistake                                               | Symptom                                                                        |
| ----------------------------------------------------- | ------------------------------------------------------------------------------ |
| `SecretKey` missing in `appsettings.Development.json` | App starts, crashes at first login                                             |
| `Issuer` typo in one place                            | Tokens issued with one issuer, validated against another → `401 invalid_token` |
| `Configure<T>()` omitted                              | DI exception when middleware tries to inject `IOptions<JwtSettings>`           |
| `Get<T>()` omitted                                    | App starts with empty secret, crashes at first login                           |
| `.env` used without a loader                          | The file is ignored; environment variables are empty                           |
| `appsettings.Development.json` committed              | Secret leaked to GitHub forever                                                |

---

## 📌 Summary

| Question                             | Answer                                                                                  |
| ------------------------------------ | --------------------------------------------------------------------------------------- |
| Configuration priority order         | `appsettings.json` → `.Development.json` → User Secrets → Env Vars → CLI Args (highest) |
| Nested key in env var                | `JwtSettings__SecretKey` (double underscore)                                            |
| What `.env` does                     | Nothing by default — needs a loader                                                     |
| What `JwtSettings` is                | Strongly-typed shape of the config                                                      |
| What `Get<T>()` does                 | Fail-fast validation at startup                                                         |
| What `Configure<T>()` does           | DI registration for `IOptions<T>`                                                       |
| Which `IOptions` for JWT             | `IOptions<T>` (singleton)                                                               |
| Why both `Get<T>` and `Configure<T>` | One validates, the other injects                                                        |
| What happens on mismatch             | Token issued by one secret, validated against another → `401`                           |

**Core principle:** configuration is a **<mark>tree</mark>** built from **<mark>multiple providers</mark>**, resolved by **<mark>priority</mark>**. `JwtSettings` is a **<mark>typed façade</mark>** over a subtree. `Get<T>()` makes startup **<mark>fail fast</mark>**. `Configure<T>()` makes settings **<mark>injectable</mark>**. `IOptions<T>` is a **<mark>singleton</mark>** — the settings are read once, and every consumer reads from **<mark>the same instance</mark>**. This is why the middleware and the issuer **<mark><u>cannot disagree</u></mark>** about the secret key: they are reading the exact same object.

---
