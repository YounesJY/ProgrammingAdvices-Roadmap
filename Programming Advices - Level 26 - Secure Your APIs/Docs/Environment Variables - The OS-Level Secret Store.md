# Environment Variables (The OS-Level Secret Store)

---

## 1. Introduction

You know the concept of `.env` files, but it's time to unlearn the "tutorial" version and rebuild it correctly from the perspective of the **Operating System** and **ASP.NET Core**.

This MD answers:

- 🔹 What is an Environment Variable at the OS level?
- 🔹 Why is it better than `appsettings.json` or `.env`?
- 🔹 How does ASP.NET Core read them?
- 🔹 What is the order of priority between configuration sources?

---

## 2. What Is an Environment Variable? (The OS Level)

An **Environment Variable** is a **<mark><u>key-value</u> pair stored in the Operating System's <u>memory</u></mark>**, completely outside your C# code.

When a program (like your `StudentAPI`) starts, the OS hands it a dictionary of these variables.

**Example:**

```text
Key:   JWT_SECRET_KEY
Value: THIS_IS_A_VERY_SECRET_KEY_123456
```

The program reads them like a dictionary, but they live **<mark>outside the application</mark>**.

---

## 3. Why Are They Better Than `appsettings.json` or `.env`?

### 3.1 The Problem with Files

- 🔹 `appsettings.json` is committed to Git → **<mark><u>leaked forever</u></mark>**.
- 🔹 Even if you delete the secret later, it stays in the Git **<mark><u>history</u></mark>**.
- 🔹 `.env` files are commonly also **<mark>committed <u>by mistake</u></mark>**.

📌 **Any secret that lives in a file in the repository <mark>is <u>effectively public</u></mark>.**

### 3.2 The Environment Variable Solution

- 🔹 They live on the **<mark><u>server machine</u></mark>**, not in the codebase.
- 🔹 You **<mark>can have <u>different values</u></mark>** for Development, Staging, and Production **<mark><u>without changing</u></mark> a single line of code**.
- 🔹 If someone steals your GitHub repository, they get **nothing**.
- 🔹 They are managed by the **<mark><u>OS</u></mark>** or **<mark><u>cloud provider</u></mark>** (Azure, AWS, Docker).

📌 **A leaked repo <mark><u>does not</u></mark> leak the environment.**

### 3.3 What About `.env` Files?

- 🔹 `.env` is a **<mark><u>development convenience</u></mark>** — **<mark>a tool</mark>** used by local developers **<mark><u>to simulate</u> environment variables</mark>**.
- 🔹 In production, you rarely use `.env` files. You use **<mark><u>actual OS environment variables</u></mark>** (or a secrets manager like **<u>Azure Key Vault</u>**).
- 🔹 If `.env` is committed, it becomes just as dangerous as `appsettings.json`.

📌 **`.env` <mark>is a <u>development tool</u></mark>. Environment Variables <mark>are the <U>production mechanism</u></mark>.**

---

## 4. How ASP.NET Core Sees Them (The Order of Priority)

This is the **<mark><u>critical</u></mark>** part. When `WebApplication.CreateBuilder(args)` runs, ASP.NET Core builds a **<mark><u>configuration hierarchy</u></mark>**. It looks for values in **<mark><u>this order</u></mark>**:

1. **Command-Line Arguments** (**<u>highest priority</u>**)
2. **Environment Variables**
3. **User Secrets** (**<u>Development only</u>**)
4. **`appsettings.{Environment}.json`** (e.g., `appsettings.Development.json`)
5. **`appsettings.json`** (**<u>lowest priority</u>**)

📌 **<mark><u>The Golden Rule</u></mark>:** If a key exists in both `appsettings.json` and an Environment Variable, **the Environment Variable <mark><u>wins</u></mark>**. It **<mark><u>overrides</u></mark>** the file.

📌 **Why this matters:** it means you can deploy **<mark>the <u>same code</u> to <u>every environment</u></mark>**, and **<mark>the OS <u>decides</u> the actual value <u>at runtime</u></mark>**.

---

## 5. How to Use Them in the Student API

### 5.1 Current State (Hardcoded)

Your `Program.cs` has this hardcoded string:

```csharp
IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"))
```

And your `AuthController.cs` has the same:

```csharp
var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));
```

📌 **The secret <mark><u>is in source code</u></mark>. This <mark><u>is the vulnerability</u></mark>.**

### 5.2 Step 1 — Move It to `appsettings.json` (Local Dev)

**Add a section:**

```json
"JwtSettings": {
  "SecretKey": "THIS_IS_A_VERY_SECRET_KEY_123456"
}
```

📌 **This is still <mark><u>not safe for production</u></mark>**, but it is a step forward. Local development can read from this file.

### 5.3 Step 2 — Read It in Code

Instead of hardcoding, inject `IConfiguration` and read the value:

```csharp
public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
{
    _configuration = configuration;
    _logger = logger;
}

// Inside Login:
var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
```

📌 **The exact implementation will be in the next lesson.**

### 5.4 Step 3 — Override It in Production

When you deploy to Azure, AWS, or a Linux server, you **<mark><u>do not touch the code</u></mark>**.

You just open the server's environment variable settings and add:

- **Key:** `JwtSettings__SecretKey`
- **Value:** `A_NEW_SECRET_KEY_GENERATED_ON_THE_SERVER`

📌 **The double underscore `__` <mark><u>maps</u></mark> to nested JSON sections.**

- `JwtSettings__SecretKey` → `JwtSettings:SecretKey` → the nested key inside `appsettings.json`.
- ASP.NET Core reads the environment variable and **<mark><u>automatically replaces</u></mark>** the value from the file.

---

## 6. The Naming Convention (The Double Underscore)

| Format                   | Where It Lives      | Example                                 |
| ------------------------ | ------------------- | --------------------------------------- |
| **JSON**                 | `appsettings.json`  | `"JwtSettings": { "SecretKey": "..." }` |
| **Flat config string**   | `IConfiguration`    | `"JwtSettings:SecretKey"`               |
| **Environment Variable** | OS / Docker / Cloud | `JwtSettings__SecretKey`                |

📌 **Why double underscore?** Because `:` is not allowed in environment variable names on most OSes. The `__` **<mark>is <u>the standard</u></mark>** replacement.

---

## 7. Where to Set Environment Variables (**<mark>By Environment</mark>**)

| Environment                 | Where to Set                                            | How                                          |
| --------------------------- | ------------------------------------------------------- | -------------------------------------------- |
| **Local (Visual Studio)**   | `launchSettings.json` or `appsettings.Development.json` | Add to `environmentVariables` section        |
| **Local (CLI)**             | OS environment                                          | `$env:JWT_SECRET="..."` (PowerShell)         |
| **Local (Docker)**          | `docker-compose.yml` or `docker run -e`                 | `environment:` section                       |
| **Production (Azure)**      | App Service Configuration → Application Settings        | Add key + value                              |
| **Production (AWS)**        | EC2 user data / ECS task definition                     | Environment section                          |
| **Production (Kubernetes)** | `Secret` + `envFrom`                                    | **<mark>Secret manifest</mark>**             |
| **Production (Any)**        | Azure Key Vault / AWS Secrets Manager                   | **<mark><u>Managed secret store</u></mark>** |

📌 **<mark><u>Every</u></mark> environment can <mark><u>have a different secret</u></mark>. The code <mark><u>never changes</u></mark>.**

---

## 8. Why This Is the Foundation of Production Security

- 🔹 **The code is <mark><u>safe to share</u></mark>.** No secrets in Git.
- 🔹 **The code is <mark><u>portable</u></mark>.** Same DLL runs in dev, staging, and production.
- 🔹 **The secrets <mark>are <u>OS-managed</u></mark>.** Protected by permissions, not by `.gitignore`.
- 🔹 **<mark><u>Rotation is possible</u></mark>.** Change the environment variable, restart the app, and the key is updated without a redeploy.

📌 **This is why the course moves the JWT secret out of source code in Module 7.5.**

---

## 9. Characteristics

- 🧬 Environment Variables live outside the code
- 🧬 They **override** `appsettings.json`
- 🧬 Nested keys use `__` (double underscore)
- 🧬 Every environment can have its own secret
- 🧬 `.env` is a dev convenience, not production
- 🧬 Committed files leak secrets forever
- 🧬 Cloud providers manage them natively

---

## 10. Interconnection

- 🔗 Source code → never contains secrets
- 🔗 `appsettings.json` → local default
- 🔗 `appsettings.{Environment}.json` → per-environment default
- 🔗 Environment Variable → overrides both
- 🔗 Cloud Key Vault → overrides everything (when used)
- 🔗 OS → manages the source of truth

### 🛠️ Summary of Interconnections

- 🔹 The code reads the secret from **configuration**
- 🔹 The configuration reads from **environment variables first**
- 🔹 The OS is the **true keeper of the secret**
- 🔹 The repo remains **safe to share**
- 🔹 The environment decides **what the actual value is**

---

## 📌 Summary

| Concept                  | Meaning                                                 |
| ------------------------ | ------------------------------------------------------- |
| **Environment Variable** | An OS-level key-value pair outside the code             |
| **Why use it?**          | Keeps secrets out of Git and source control             |
| **Priority**             | Overrides `appsettings.json`                            |
| **Nested key syntax**    | `Section__SubKey` (double underscore)                   |
| **Local dev**            | `launchSettings.json` or `appsettings.Development.json` |
| **Production**           | OS, Docker, cloud provider, or Key Vault                |
| **`.env` file**          | Dev convenience, not production                         |
| **Rule of thumb**        | If it's committed, it's leaked                          |

**Core principle:** your code should never **<mark><u>know</u></mark>** the actual secret. Your code should only know **<mark><u>where</u></mark> <mark><u>to look for it</u></mark>** (the configuration). The OS tells the app the value at runtime. This is what allows the same code to run in every environment while keeping the secret locked to the server. **The repo <mark><u>stays public</u></mark>; the secret <mark><u>stays private</u></mark>.** That is **<mark><u>the entire purpose</u></mark>** of environment variables.

---
