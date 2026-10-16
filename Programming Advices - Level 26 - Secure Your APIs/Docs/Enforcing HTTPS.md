# Enforcing HTTPS

---

## 1. Introduction

You already understand what HTTPS is and why it matters.

Now <mark>it's time to **enforce** it correctly</mark> in your existing Student API.

In this lesson,<mark> we will secure the transport layer **without changing endpoints, <u>without adding authentication</u>, and without breaking existing behavior**</mark>.

🎯 **Goal:** Ensure your API actually <mark>**runs on HTTPS** and **redirects HTTP traffic correctly**</mark>.

> <mark>This is **the first real**, applied **security layer**</mark>.

---

## 2. Why This Step Comes Before JWT (Short Reminder)

Later in the course, we will send:

- 🔹 Login credentials
- 🔹 JWT access tokens
- 🔹 Refresh tokens

Without enforced HTTPS:

- ❌ Tokens **<u>can be stolen</u>**
- ❌ Users **<u>can be impersonated</u>**

> 📌 **HTTPS <mark>must be enforced <u>before any identity logic exists</u></mark>.**

---

## 3. Step 1 — Verify HTTPS Redirection Middleware

📋 **Instructions**

Open `Program.cs` and ensure this line exists:

```csharp
app.UseHttpsRedirection();
```

📌 **Notes:**

- This **middleware <u>redirects</u>** HTTP → HTTPS
- It <mark>**must appear <u>before</u>** controller mapping</mark>
- <mark>It does **<u>not</u>** create HTTPS</mark>

---

## 4. Step 2 — Understand a Critical Truth (<mark>**Must Memorize**</mark>)

📌 **Key Rule**

> `UseHttpsRedirection()` <mark>does **NOT** enable HTTPS</mark>. It only redirects if HTTPS already exists.

For HTTPS to work:

- 🔹 HTTPS must **<u>be configured</u>**
- 🔹 HTTPS must **<u>be running</u>**
- 🔹 HTTPS must **be selected <u>as the startup profile</u>**

---

## 5. Step 3 — Check HTTPS Profile Configuration (<mark>**<u>CRITICAL</u>**</mark>)

📋 **Instructions**

- 🔹 Open: `Properties/launchSettings.json`

**How to find it in Visual Studio:**

1. Open your project in Visual Studio

2. Open Solution Explorer (right side)

3. Expand your API project (not the solution)

4. Expand the `Properties` folder

5. Click `launchSettings.json`
- 🔹 Locate the `profiles` section

You will usually see multiple profiles, such as:

- 🔹 `http`
- 🔹 `https`
- 🔹 `IIS Express`

### ✅ Correct HTTPS Profile

```json
"https": {
  "commandName": "Project",
  "launchBrowser": true,
  "launchUrl": "swagger",
  "applicationUrl": "https://localhost:7217;http://localhost:5215"
}
```

> 📌 <mark>This profile **<u>starts HTTPS</u>**</mark>.

### ❌ Common Beginner Mistake (Very Important)

<mark>Running the `http` profile</mark>:

```json
"http": {
  "applicationUrl": "http://localhost:5215"
}
```

- ❌ HTTPS **<u>is not started</u>**
- ❌ Redirection **<u>may not happen</u>**
- ❌ Swagger **opens <u>on HTTP</u>**

> 📌 <mark>This is **not a security bug**</mark> — <mark>it is a **<u>startup choice</u>**</mark>.

---

## 6. Step 4 — Run the Correct Profile (<mark>Most Missed Step</mark>)

📋 **Instructions (Visual Studio)**

- 🔹 At the top toolbar (next to ▶️ Run)
- 🔹 Select **https** (NOT `http`, NOT `IIS Express`)
- 🔹 Run the project

✅ Swagger should open as:

```js
https://localhost:7217/swagger
```

---

## 7. Step 5 — Trust the Development HTTPS Certificate

**In development**, HTTPS <mark>**requires <u>a trusted certificate</u>**</mark>.

❗ **<mark>Very Important Clarifications</mark>:**

- 🔹 Run these commands **<mark>only once per machine</mark>**
- 🔹 **<u>NOT once per project</u>**

> 📌 If HTTPS already works, <mark>you do not need to repeat them</mark>.

### Run it from Inside Visual Studio (Terminal)

📋 **Instructions**

1️⃣ Open your project
2️⃣ Go to: **View → Terminal**
3️⃣ Run:

```bash
dotnet dev-certs https --clean
dotnet dev-certs https
dotnet dev-certs https --trust
```

> 📌 This is just a terminal — <mark>same result as Command Prompt</mark>.

---

## 8. Step 6 — Understand Redirection Testing (Development vs Production)

📌 **<mark><u>Important clarification</u></mark>**

In real deployments (single hosting environment), <mark>HTTP → HTTPS **redirection works <u>automatically and always</u>**</mark>.

However, <mark>**in local development** using Visual Studio, **redirection may appear <u>not to work due to launch profiles</u>**</mark>.

### 🔧 Don't Get Confused (<mark>**<u>Very Common</u>**</mark>)

You might think:

> "I typed `http://` and <mark>**it should redirect**</mark>."

But in Visual Studio:

**🚧 Development (Visual Studio)**

- <mark>**Each launch profile <u>is a separate</u> startup configuration**</mark>
- <mark>Redirection does **not** jump **<u>between profiles</u>**</mark>
- `http` profile <mark>**stays** HTTP</mark>
- `https` profile <mark>**runs** HTTPS</mark>

> 📌 <mark>This is **<u>a tooling behavior</u>**</mark>, <mark>**not a security failure**</mark>.

**🏭 Production (Real Hosting)**

- One hosting environment
- HTTP and HTTPS <mark>**handled together**</mark>
- HTTP → HTTPS <mark>**<u>redirection always works</u>**</mark>

> 📌 <mark>**No profiles**</mark>. <mark>**<u>No confusion</u>**</mark>.

### ✅ Final Rule to Remember

- 🔹 **Development:** Verify <mark>HTTPS **is active**</mark>
- 🔹 **Production:** Verify <mark>HTTP **redirects to** HTTPS</mark>

### 📋 Correct Way to Verify in Development

- 🔹 Run the `https` profile
- 🔹 Confirm Swagger opens at: `https://localhost:7217/swagger`

📌 If the API runs on HTTPS:

- ✅ Transport security <mark>**is enforced**</mark>
- ✅ HTTPS <mark>redirection middleware **is active**</mark>
- ✅ <mark>**<u>The foundation is correct</u>**</mark>

### 📌 How Redirection Works in Production

In production:

- 🔹 There <mark>is **<u>one</u> hosting environment**</mark>
- 🔹 HTTP and HTTPS **<u>are handled together</u>**
- 🔹 Requests to HTTP **are <u>always redirected</u> to HTTPS**

📌 No profiles, no confusion.

### ✅ Final Rule to Remember (<mark>**<u>Repeated</u>**</mark>)

- 🔹 Development → Verify HTTPS is active
- 🔹 Production → Verify HTTP redirects to HTTPS

---

## 9. Characteristics

- 🧬 **No authentication <u>yet</u>**
- 🧬 **No endpoint changes**
- 🧬 **No controller changes**
- 🧬 **Security added transparently**
- 🧬 <mark>**Foundation** for **<u>all future security</u>**</mark>

---

## 10. Interconnection

- 🔗 **Transport security** → <mark>**token security**</mark>
- 🔗 HTTPS → <mark>**trust**</mark>
- 🔗 **Encrypted traffic** → <mark>**safe authentication**</mark>

### 🛠️ Summary of Interconnections

- 🔹 HTTPS protects data → tokens **<u>remain confidential</u>**
- 🔹 Secure transport → **<u>meaningful authentication</u>**
- 🔹 Security layers **<u>build on each other</u>**

---

## 11. Conclusion

You have now added your first real security layer.

- ✅ Data is encrypted
- ✅ Traffic is protected
- ✅ Foundation is ready

📌 But… the API <mark>still doesn't know **who** is calling it</mark>. That changes in the next lesson.

📌 **Next Lesson:** 👉 CORS — <mark>Controlling **<u>Who Can</u> Call Your API**</mark>

---

## 📌 Summary

| Step | Action                                                   | Why                                             |
| ---- | -------------------------------------------------------- | ----------------------------------------------- |
| 1    | Verify `app.UseHttpsRedirection()` in `Program.cs`       | Redirects HTTP → HTTPS (does not create HTTPS)  |
| 2    | Understand the key rule                                  | Middleware redirects, but does not enable HTTPS |
| 3    | Check `launchSettings.json`                              | Correct HTTPS profile must exist                |
| 4    | Run the **https** profile in Visual Studio               | HTTP profile does not start HTTPS               |
| 5    | Trust dev certificate (`dotnet dev-certs https --trust`) | Once per machine, not per project               |
| 6    | Understand dev vs prod redirection                       | Dev uses profiles; prod redirects automatically |

| Environment | What to verify                                             |
| ----------- | ---------------------------------------------------------- |
| Development | HTTPS is actually active (`https://localhost:.../swagger`) |
| Production  | HTTP requests redirect to HTTPS                            |

**Core principle:** `UseHttpsRedirection()` <mark>**is a redirector**</mark>, not an enabler. HTTPS **must already be <u>configured and running</u>**. In development, this means <mark>**picking <u>the right launch profile</u> and <u>trusting the dev certificate</u>**</mark> once per machine. In production, <mark>**redirection is automatic**</mark> because <mark>there is **only one** hosting environment</mark>.

---


