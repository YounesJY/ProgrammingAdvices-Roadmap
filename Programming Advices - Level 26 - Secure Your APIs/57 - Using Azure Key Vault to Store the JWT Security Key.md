# Using Azure Key Vault to Store the JWT Security Key (Step-by-Step)

---

## 1. 🗝️ Introduction

In this lesson, you will implement **Azure Key Vault** integration step by step.

This lesson is **<mark>purely practical</mark>**:

- No theory repetition
- No security philosophy
- **Only <mark>what to do</mark> and <mark>where</mark>**

By the end, your Student API will read the JWT signing key **directly from Azure Key Vault**.

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will:

- Create an **Azure Key Vault**
- Store a JWT signing key as a **secret**
- Grant access to your **application**
- Configure ASP.NET Core to read secrets from **Key Vault**
- **Remove** the JWT key from source code

---

## 3. 🔹 Step 1 — Create an Azure Key Vault

1. Open **Azure Portal**
2. Click **Create a resource**
3. Search for **Key Vault**
4. Click **Create**
5. Fill in:
   - **Resource Group** → select or create
   - **Key Vault Name** → must be **globally unique**
   - **Region** → same region as your API
   - **Pricing Tier** → **Standard**
6. Click **Review + Create**, then **Create**

---

## 4. 🔹 Step 2 — Add the JWT Signing Key as a Secret

1. Open your **Key Vault**
2. Go to **Secrets**
3. Click **Generate / Import**
4. Enter:
   - **Name:** `JwtSigningKey`
   - **Value:** a long random string (**32+ characters**)
5. Click **Create**

📌 **This value replaces the hardcoded JWT key.**

---

## 5. 🔹 Step 3 — Grant Access to the Key Vault (Development Mode)

For learning and local development:

1. Go to **Key Vault → Access policies**
2. Click **Create**
3. Select **your Azure user account**
4. Under **Secret permissions**, enable:
   - `Get`
   - `List`
5. **Save**

📌 **This allows your application (running under your user) to read secrets.**

---

## 6. 🔹 Step 4 — Install Required NuGet Packages

In your ASP.NET Core project, install:

```bash
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
dotnet add package Azure.Identity
```

📌 **<mark>These packages</mark> allow ASP.NET Core to authenticate with Azure and read Key Vault secrets.**

---

## 7. 🔹 Step 5 — Configure Key Vault in `appsettings.json`

Add the Key Vault URL:

```json
{
  "KeyVault": {
    "Url": "https://your-key-vault-name.vault.azure.net/"
  }
}
```

📌 **Replace `your-key-vault-name` with your actual vault name.**

---

## 8. 🔹 Step 6 — Configure `Program.cs` to Load Secrets from Key Vault

**At the top of `Program.cs`:**

```csharp
using Azure.Identity;
```

**Before calling `builder.Build()`:**

```csharp
var keyVaultUrl = builder.Configuration["KeyVault:Url"];

if (!string.IsNullOrWhiteSpace(keyVaultUrl))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUrl),
        new DefaultAzureCredential());
}
```

📌 **ASP.NET Core will now load secrets from Key Vault automatically.**

---

## 9. 🔹 Step 7 — Read the JWT Key from Configuration

**Replace any hardcoded JWT key with:**

```csharp
var secretKey = builder.Configuration["JwtSigningKey"];
```

**Use it normally:**

```csharp
IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(secretKey));
```

📌 **The secret is now coming from Azure Key Vault.**

---

## 10. 🔹 Step 8 — Run and Verify

1. **Restart Visual Studio**
2. **Run the API**
3. **Call the login endpoint**
4. **Verify:**
   - JWT is issued correctly
   - Secured endpoints still work
   - **No JWT secret exists in code**

**If the app fails to start:**

- Check **Key Vault URL**
- Check **access policy**
- Check **Azure login status**

---

## 11. 🔹 Common Issues and Fixes

| Issue                              | Cause                           | Fix                                         |
| ---------------------------------- | ------------------------------- | ------------------------------------------- |
| `403 Forbidden` from Key Vault     | Access policy missing           | Add your user/identity with `Get` + `List`  |
| `401 Unauthorized` from Azure      | Not logged in                   | Run `az login` or sign in via Visual Studio |
| `Configuration` key is `null`      | Secret name mismatch            | Check the secret name in Key Vault          |
| App crashes on startup             | Key Vault URL invalid           | Verify the URL in `appsettings.json`        |
| Works locally, fails in production | Managed Identity not configured | Enable Managed Identity on the App Service  |

---

## 12. 🏁 Conclusion

You have successfully:

- ✅ **Removed** the JWT key from source code
- ✅ **Stored** it securely in Azure Key Vault
- ✅ **Configured** ASP.NET Core to load secrets at runtime
- ✅ **Applied** a real production-grade practice

📌 **This lesson completes secure secret storage implementation.**

---

## 📌 Summary

| Step | Action                              | Where                       |
| ---- | ----------------------------------- | --------------------------- |
| 1    | Create Key Vault                    | Azure Portal                |
| 2    | Add secret `JwtSigningKey`          | Key Vault → Secrets         |
| 3    | Grant access policy                 | Key Vault → Access Policies |
| 4    | Install NuGet packages              | `.csproj`                   |
| 5    | Add Key Vault URL                   | `appsettings.json`          |
| 6    | Add `AddAzureKeyVault(...)`         | `Program.cs`                |
| 7    | Read the secret via `Configuration` | `Program.cs`                |
| 8    | Verify login still works            | Swagger                     |

| NuGet Package                                       | Purpose                                          |
| --------------------------------------------------- | ------------------------------------------------ |
| `Azure.Extensions.AspNetCore.Configuration.Secrets` | Load secrets into `IConfiguration`               |
| `Azure.Identity`                                    | Authenticate to Azure (`DefaultAzureCredential`) |

| Concept                      | Meaning                                                            |
| ---------------------------- | ------------------------------------------------------------------ |
| **Secret in Key Vault**      | Not in code, not in repo, not in `appsettings.json`                |
| **`DefaultAzureCredential`** | Tries multiple auth methods (Visual Studio, CLI, Managed Identity) |
| **Access Policy**            | Grants read access to a specific identity                          |
| **Secret name**              | Matches the config key (`JwtSigningKey`)                           |
| **Production**               | Use **Managed Identity** instead of user account                   |

**<mark>Core principle</mark>:** the JWT signing key now lives **<mark>outside</mark> your code, <mark>outside</mark> your repo, and <mark><u>outside</u></mark> your server**. It is fetched at **<mark><u>runtime</u></mark>** from a vault that is encrypted, audited, and protected by identity — not passwords. The code change **<mark>is <u>minimal</u></mark>** (`AddAzureKeyVault` + one line to read the secret), but the security shift is massive: **your app <mark><u>no longer</u></mark> knows the secret — it <mark><u>asks</u> for permission to use it</mark>.** This is the final step in moving from *"it works"* to *"it is secure at scale."*

---
