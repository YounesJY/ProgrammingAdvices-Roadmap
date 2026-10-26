# C# Console Rate Limit Tester

**(Login Flood Simulator)**

---

## 1. 🔹 What This Tester Does

- 🔹 Sends **N login requests** in rapid succession
- 🔹 Uses the same IP (your machine)
- 🔹 Stops naturally when the rate limit is hit
- 🔹 Shows exactly which request returned `429`

📌 **<mark>This simulates a brute-force or abuse attempt</mark>.**

---

## 2. 🔹 Step 1 — Create a New Console App

```bash
dotnet new console -n RateLimitTester
cd RateLimitTester
```

Or create it using Visual Studio.

---

## 3. 🔹 Step 2 — Replace `Program.cs` with This Code

```csharp
using System.Net.Http;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        var client = new HttpClient();

        // 🔴 CHANGE THIS PORT to match your API
        var url = "https://localhost:7217/api/Auth/login";

        var payload = new
        {
            email = "test@test.com",
            password = "wrongpassword"
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        Console.WriteLine("Starting login flood test...\n");

        for (int i = 1; i <= 10; i++)
        {
            try
            {
                var response = await client.PostAsync(url, content);

                Console.WriteLine(
                    $"Attempt {i} → Status: {(int)response.StatusCode} ({response.StatusCode})");

                // Recreate content each time (important)
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {i} → ERROR: {ex.Message}");
            }
        }

        Console.WriteLine("\nTest completed :-).");
        Console.ReadLine();
    }
}
```

📌 **<mark>Important detail</mark>:** `StringContent` must be **<mark>recreated each loop</mark>** — otherwise requests fail. The `HttpContent` stream <mark>is consumed <u>after the first send</u></mark>.

---

## 4. 🔹 Step 3 — Run the Tester

Before you run the tester, you should **run the server first**.

```bash
dotnet run
```

Or using Visual Studio run.

---

## 5. 🔹 Expected Output (Correct Behavior)

```text
🔁 Starting login flood test...

Attempt 1 → Status: 401 (Unauthorized)
Attempt 2 → Status: 401 (Unauthorized)
Attempt 3 → Status: 401 (Unauthorized)
Attempt 4 → Status: 401 (Unauthorized)
Attempt 5 → Status: 401 (Unauthorized)
Attempt 6 → Status: 429 (TooManyRequests)
Attempt 7 → Status: 429 (TooManyRequests)
Attempt 8 → Status: 429 (TooManyRequests)

✅ Test completed.
```

📌 **The exact status before `429` may vary (`200` or `401`), but `429` <mark>must appear <u>after the limit</u></mark>.**

---

## 6. 🔹 Step 4 — Verify Reset Behavior

1️⃣ Run the tester → hit `429`
2️⃣ Wait **60 seconds**
3️⃣ Run it again

**Expected:**

- ✅ Attempts 1–5 work again
- ❌ 6th hits `429` again

---

## 7. 🔹 Step 5 — What This Proves

- ✅ Rate limiting is enforced
- ✅ Per-IP partition works
- ✅ Login endpoint is protected
- ✅ Controller code is bypassed after the limit
- ✅ Middleware blocks abuse correctly

---

## 8. 🧠 Final Mental Model (Lock This In)

- 🔹 **JWT** secures **<mark>identity</mark>**
- 🔹 **Refresh tokens** secure **<mark>sessions</mark>**
- 🔹 **Rate limiting** secures **<mark>entry points</mark>**
- 🔹 **Testing** <mark>proves reality, <u>not theory</u></mark>

---

## 📌 Summary

| Concept                     | Detail                                       |
| --------------------------- | -------------------------------------------- |
| **What it simulates**       | A brute-force / credential-stuffing attempt  |
| **How many requests**       | 10 (but stops effectively at 6 due to `429`) |
| **What to look for**        | The **6th** request returns `429`            |
| **Common bug**              | Reusing `StringContent` (must recreate)      |
| **Verification after test** | Wait 60s → limit resets                      |
| **What it proves**          | Rate limiting runs before the controller     |
| **What it does NOT prove**  | Business logic correctness, JWT validity     |

**Core principle:** the rate limiter **is not proven <mark>by reading the code</mark>** — it is proven by **<mark>attacking it</mark>**. Flood the login endpoint with 10 wrong-password attempts, watch the first 5 return `401`, and the 6th return `429`. Then wait a minute and watch it reset. ***<mark>That is the difference</mark>*** between *"I think rate limiting works"* and *"I watched it work."* The console client turns the theory **<mark>into evidence</mark>**.

---


