# Why the Anonymous Middleware Didn't Work

---

## 1. Introduction

During the rate limiting lesson, you implemented the course's approach: an **<mark>anonymous middleware</mark>** that checks the response on **<mark>the return journey</mark>** and writes a custom `429` message.

When you tested it, the `429` status code worked — but <mark>the custom message <u>never appeared</u></mark>.

You switched to `options.OnRejected` inside `AddRateLimiter`, and it worked.

This MD answers:

- 🔹 Why the anonymous middleware failed
- 🔹 Why `OnRejected` succeeded
- 🔹 The difference between the two approaches
- 🔹 Which one you should use in production

---

## 2. The Course's Approach (Anonymous Middleware)

The lesson <mark>used <u>this pattern</u></mark>:

```csharp
app.Use(async (context, next) =>
{
    await next();

    if (!context.Response.HasStarted && 
        context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
    {
        await context.Response.WriteAsync("Too many login attempts...");
    }
});
```

### 2.1 How It Was Supposed to Work

1. The request reaches `UseRateLimiter()`.
2. The limiter rejects the request and sets the status code to `429`.
3. The response begins its **return journey** upward.
4. The custom middleware catches the `429` and writes a message into the body.
5. The client receives `429` with the message.

📌 **<mark>In <u>theory</u></mark>, this works.**

---

## 3. Why the Anonymous Middleware Failed

### 3.1 The Rate Limiter Starts the Response Immediately

When `UseRateLimiter()` rejects a request, it <mark>does <u>not</u></mark> just set `StatusCode = 429` and wait. It **completes the response**:

- Sets the status code
- Sends the headers
- Sets `Content-Length: 0`
- Flushes the response to the client

By the time the response reaches your custom middleware on the return journey, **`context.Response.HasStarted` <mark>is already `true`</mark>**.

### 3.2 The `HasStarted` Check Fails

Your middleware runs this check:

```csharp
if (!context.Response.HasStarted && context.Response.StatusCode == 429)
```

Since `HasStarted` is `true`, `!HasStarted` is `false`.

The entire condition is `false`.

**The message <mark>is skipped</mark>.**

### 3.3 Why This Is Not a Bug in Your Code

Your code is correct. It is **<mark>defensive</mark>**. It correctly refused to write to a response that had already started, because writing to a started response throws:

> `System.InvalidOperationException: The response has already started`

If you had removed the `HasStarted` check, your API would have thrown a `500 Internal Server Error` instead of returning `429`.

📌 **The check <mark>saved your API <u>from crashing</u></mark>. But it <mark>also prevented</mark> the message <mark><u>from appearing</u></mark>.**

### 3.4 The Root Cause

The failure is not in your middleware. It is in the **<mark>order of operations</mark>**:

- The rate limiter **<mark>completes</mark>** the response.
- Your middleware runs **<mark>after</mark>** the response **<mark>is <u>already sent</u></mark>**.

There is no room for your middleware to inject a body, because the body **<mark>has <u>already been closed</u></mark>**.

---

## 4. Why `OnRejected` Worked

### 4.1 How `OnRejected` Works

Instead of intercepting the response on the way back, `OnRejected` runs **<mark>inside</mark>** the rate limiter, **<mark>before</mark>** the response is sent:

```csharp
options.OnRejected = async (context, token) =>
{
    context.HttpContext.Response.ContentType = "application/json";
    await context.HttpContext.Response.WriteAsync("{\"error\":\"Too many login attempts...\"}", token);
};
```

### 4.2 The Flow

1. The request hits `UseRateLimiter()`.
2. The limiter detects the limit is exceeded.
3. **It fires `OnRejected` <mark><u>before</u></mark> finishing the response.**
4. `OnRejected` writes the message.
5. The limiter sets the status code (`429`) and sends the response — **with the body already in place**.
6. The client receives `429` with the message.

📌 **`OnRejected` runs <mark>inside the response lifecycle</mark>, <mark><u>not after it</u></mark>.**

### 4.3 Why It Succeeds

- It has access to the response **<mark>before</mark>** it is flushed.
- It can set the `Content-Type`.
- It can write the body.
- It can still set the status code.

**<mark>No conflict</mark>. No `HasStarted` problem. <mark><u>No crash</u></mark>.**

---

## 5. The Key Difference

| Aspect                  | Anonymous Middleware            | `OnRejected`                                         |
| ----------------------- | ------------------------------- | ---------------------------------------------------- |
| **Where it runs**       | After the response is generated | Inside the rate limiter, before the response is sent |
| **Access to response**  | Only on the return journey      | Direct, before headers are sent                      |
| **`HasStarted` state**  | Already `true`                  | Still `false`                                        |
| **Can write the body?** | ❌ No (would throw)              | ✅ Yes                                                |
| **Result**              | Message missing                 | Message works                                        |
| **Production-ready?**   | ⚠️ Fragile (relies on check)     | ✅ Recommended                                        |

📌 **Middleware runs *around* the pipeline. `OnRejected` runs *inside* the middleware that is generating the response.**

---

## 6. A Useful Mental Model

### 6.1 The Middleware Approach (Course)

> "Let the rate limiter slam the door. Then try to slip a note under the door before the client walks away."

If the client has already walked away with an empty envelope, your note never arrives.

### 6.2 The `OnRejected` Approach

> "Before the door is slammed, the rate limiter asks you to write the note. You write it, and it is placed inside the envelope before it is sealed."

📌 **You have to write the message *before* the envelope is sealed.**

---

## 7. Why the Course Used the Middleware Approach

The teacher used the anonymous middleware for a **<mark>specific reason</mark>**:

- It teaches the **Return Journey** concept.
- It shows how middleware **<mark>can <u>inspect</u> responses</mark>** after the pipeline.
- It demonstrates a real-world pattern that *would* work for other status codes (like `404` or `500` from controllers, which do not flush the response early).

📌 **The middleware approach works for endpoints that <mark>do not pre-flush</mark>. It fails for rate limiting specifically <mark>because the limiter <u>flushes early</u></mark>.**

The teacher likely knew this but used it **<mark><u>to reinforce</u> the pipeline concept</mark>**. In production, `OnRejected` is the correct tool.

---

## 8. When to Use Which

| Use Case                                  | Recommended Approach                     |
| ----------------------------------------- | ---------------------------------------- |
| **Customizing 429 from the rate limiter** | `OnRejected`                             |
| **Logging all responses**                 | Middleware (return journey works)        |
| **Modifying 404 from MVC**                | Middleware (response is not yet flushed) |
| **Customizing errors from a controller**  | Middleware or `IExceptionFilter`         |
| **Adding headers to every response**      | Middleware (works on the return journey) |

📌 **The middleware approach <mark><u>is not wrong</u></mark> — it is just wrong *for this <u>specific case</u>*.**

---

## 9. Characteristics

- 🧬 `UseRateLimiter()` flushes the response immediately
- 🧬 Middleware runs after the flush → `HasStarted` is true
- 🧬 `HasStarted` check prevents crashes but also prevents writing
- 🧬 `OnRejected` runs *inside* the limiter before flushing
- 🧬 `OnRejected` is the correct place to customize `429`
- 🧬 Middleware remains useful for other status codes

---

## 10. Interconnection

- 🔗 Rate limiter → generates `429` → flushes response
- 🔗 Middleware → runs after → sees `HasStarted = true`
- 🔗 `HasStarted` check → prevents crash → skips body write
- 🔗 `OnRejected` → runs before flush → writes body
- 🔗 Client receives → `429` with message

### 🛠️ Summary of Interconnections

- 🔹 The middleware and `OnRejected` are both correct code
- 🔹 The difference is **when** they run relative to the response flush
- 🔹 The rate limiter is the only place that knows the rejection is about to happen
- 🔹 Intercepting after the fact is too late
- 🔹 Intercepting inside the limiter works

---

## 📌 Summary

| Question                                | Answer                                                     |
| --------------------------------------- | ---------------------------------------------------------- |
| Why did the middleware fail?            | The rate limiter had already started the response          |
| Why was the message missing?            | `HasStarted` was true → the write was skipped              |
| Why did the API not crash?              | The `!HasStarted` check prevented a crash                  |
| Why did `OnRejected` work?              | It runs inside the limiter, before the response is flushed |
| Is the middleware approach useless?     | ❌ No — it works for other status codes                     |
| Is `OnRejected` the production pattern? | ✅ Yes — it is the official way                             |
| What is the rule?                       | Write the message **before** the envelope is sealed        |

**Core principle:** you cannot write to a response that **has *already been sent***. The rate limiter sends the `429` response **immediately *for performance*** — so any middleware sitting above it on the return journey **is *too late***. `OnRejected` **<mark>is not a workaround</mark>**; it is the **<mark>correct extension point</mark>** — it runs inside the limiter, **at *the exact moment*** the rejection happens, before the response leaves the server. The course used the middleware to teach the pipeline concept. In production, use `OnRejected`.

---


