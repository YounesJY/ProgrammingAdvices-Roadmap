# Testing Rate Limiting in Student API

**(Fixed Window + Per IP + 429)**

---

## 1. 🗝️ Introduction

After implementing rate limiting, you must verify:

- ✅ The limit actually blocks requests
- ✅ The API returns `429 Too Many Requests`
- ✅ The controller is **not** executed when blocked
- ✅ The limit resets after the time window

This section shows you exactly how to test your implementation.

---

## 2. 🔹 Step 1 — Confirm You're Testing the Right Endpoints

Rate limiting is applied only to:

- 🔹 `POST /api/Auth/Login`
- 🔹 `POST /api/Auth/Refresh`

📌 **Testing `/api/Students/...` will not show limits (<mark>by design</mark>).**

---

## 3. 🔹 Step 2 — Test Using Swagger (Quick Manual Test)

1️⃣ Open Swagger
2️⃣ Go to: `POST /api/Auth/Login`
3️⃣ Use any credentials (even wrong ones is fine)
4️⃣ Click Execute **6 times quickly**

**Expected behavior:**

- ✅ **First 5 requests:** normal response (`200` or `401` depending on credentials)
- ❌ **6th request:** `429 Too Many Requests`

📌 If you added the optional message middleware, you will also see:

> "Too many login attempts. Please try again later."

---

## 4. 🔹 Step 3 — Test Using Postman (More Reliable)

1️⃣ Create a request:

- 🔹 Method: `POST`
- 🔹 URL: `https://localhost:xxxx/api/Auth/login`

2️⃣ Body (JSON):

```json
{
  "email": "ali.ahmed@student.com",
  "password": "wrongPassword"
}
```

3️⃣ Send the request **<mark>6 times <u>within</u> 1 minute</mark>**

**Expected:**

- ✅ Requests 1–5 → `200` / `401`
- ❌ Request 6 → `429`

📌 **Postman <mark>makes it easy</mark> to repeat quickly.**

---

## 5. 🔹 Step 4 — Test Using PowerShell (Fast Burst Test)

Run this in PowerShell (Windows):

```powershell
for ($i=1; $i -le 6; $i++) {
  Write-Host "Attempt $i"
  Invoke-RestMethod -Method Post `
    -Uri "https://localhost:7217/api/Auth/login" `
    -ContentType "application/json" `
    -Body '{"email":"ali.ahmed@student.com","password":"wrongPassword"}'
}
```

**Expected:**

- ✅ Attempts 1–5 succeed (`200` / `401`)
- ❌ Attempt 6 fails with `429`

📌 Replace port `7217` to match your actual port if needed.

### ✅ Where Do You Run This PowerShell Script?

#### Option 1 (Best) — Use Visual Studio Terminal

Visual Studio has a built-in terminal.

📋 **Steps:**

1️⃣ Open your API project in Visual Studio
2️⃣ From the top menu click: **View → Terminal**
3️⃣ A terminal panel opens at the bottom
4️⃣ Click the dropdown and choose **PowerShell**
5️⃣ Paste your script and run it

#### Option 2 — Use Windows PowerShell Outside Visual Studio

This is the most common way.

📋 **Steps:**

1️⃣ Run your API normally in Visual Studio (`F5`)
2️⃣ Open PowerShell:

- Press `Win + R`
- Type: `powershell`

3️⃣ Paste the script and press Enter

That's it.

---

## 6. 🔹 Step 5 — Verify Limit Reset (Window Behavior)

Your rule is:

- 🔹 **5 requests per 1 minute**

So:

1️⃣ Trigger the `429` (6th request)
2️⃣ Wait **60 seconds**
3️⃣ Try again

**Expected:**

- ✅ Requests work again (counter reset)

---

## 7. 🔹 Step 6 — Confirm It's Per IP (Important)

To prove it's per IP:

- ✅ Test from your machine → you hit `429`
- ✅ Test from a different network/device (phone hotspot) → you should **NOT** be blocked immediately

**Why?**

- 🔹 Different IP → different rate limit bucket

📌 **This confirms the limiter is partitioned by IP.**

---

## 8. 🔹 Step 7 — Confirm Controller Code Is Not Executed (Proof Test)

To verify requests are blocked **before** controller logic:

✅ Add a simple log line at the start of `Login`:

```csharp
Console.WriteLine("Login endpoint executed");
```

Then:

1️⃣ Send 6 requests
2️⃣ On the 6th request (`429`), check the console

**Expected:**

- ✅ You will **NOT** see the log line for the blocked request

📌 **This proves middleware blocked it before reaching the controller.**

---

## 9. 🔹 Step 8 — Test Refresh Endpoint (Same Method)

Repeat the same tests with:

- 🔹 `POST /api/Auth/refresh`

Send 6 requests quickly.

**Expected:**

- ✅ 1–5 normal
- ❌ 6th returns `429`

📌 **Refresh endpoints are also high-risk and must behave identically.**

---

## 10. 🧬 Characteristics of a Correct Test Result

- 🧬 `429` happens on the 6th request
- 🧬 Counter resets after 1 minute
- 🧬 Different IP has a separate counter
- 🧬 Blocked calls do not execute controller code

---

## 11. 🏁 Conclusion

You have now verified:

- ✅ Rate limiting is active
- ✅ Login + Refresh are protected
- ✅ `429` is returned correctly
- ✅ Middleware blocks requests early
- ✅ Limits reset properly

📌 **Your implementation is not only written — it is proven working.**

---

## 📌 Summary

| Step | What to Test            | Expected                         |
| ---- | ----------------------- | -------------------------------- |
| 1    | Confirm endpoints       | Only Login + Refresh are limited |
| 2    | Swagger test            | 6th request → `429`              |
| 3    | Postman test            | 6th request → `429`              |
| 4    | PowerShell test         | Fast burst → `429` on 6th        |
| 5    | Window reset            | After 60s → works again          |
| 6    | Per-IP confirmation     | Different IP → separate counter  |
| 7    | Controller not executed | No log for blocked request       |
| 8    | Refresh endpoint        | Same behavior as login           |

**Core principle:** a rate limiter is not "working" because the code compiles — it is working because you **<mark>proved</mark>** four things: the `429` **<mark>fires</mark>**, the window **<mark>resets</mark>**, the IP **<mark>partition works</mark>**, and the controller **<mark>is never <u>reached</u></mark>**. Without the proof, you have *configured* rate limiting. With the proof, you have *implemented* it.

---
