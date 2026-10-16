# Middleware Redirection & the HTTP Pipeline

---

## 1. What "Middleware Redirects HTTP → HTTPS" Means

When a request arrives at your API over **HTTP** (port 80 or 5000, plaintext), `UseHttpsRedirection()` **does not serve it**. It sends back a tiny response:

```js
HTTP/1.1 307 Temporary Redirect
Location: https://localhost:7217/api/students
```

The client then re-sends the same request over HTTPS (port 443 or 7217, encrypted). <mark>**Only the second request reaches your controllers**</mark>.

**Key points:**

- It's a **redirect**, not a rewrite. Two round-trips happen, not one.
- Status is `307` by default (preserves method + body). Older docs mention `301`/`302`; ASP.NET Core uses `307` to avoid breaking POST/PUT.
- The redirect target is derived from:
  - `ASPNETCORE_HTTPS_PORT` env var, or
  - `https_port` in `launchSettings.json`, or
  - the first HTTPS URL in `applicationUrl`.

If none of these are configured, the middleware logs a warning and does nothing — which is why "it doesn't work" is usually a config issue, not a code issue.

---

## 2. What Is the HTTP Pipeline?

<mark>Every ASP.NET Core request **flows through an ordered** chain of **middleware** components</mark>. Each one:

- Receives the request
- Optionally does something (log, redirect, authenticate, etc.)
- Either passes to the next middleware (`next()`) or short-circuits (returns a response)

```js
Request
  │
  ▼
[Middleware A] ──► next()
  │
  ▼
[Middleware B] ──► next()
  │
  ▼
[Middleware C] ──► short-circuits, sends response
  │
  ▼
Response travels back up the chain
```

Typical pipeline order in `Program.cs`:

```csharp
app.UseHttpsRedirection();     // Layer 1
app.UseCors();                 // Layer 1
app.UseAuthentication();       // Layer 2
app.UseAuthorization();        // Layers 3–4
app.MapControllers();          // Endpoint
```

<mark><u>**Order matters.**</u></mark> Middleware **<u>registered earlier runs earlier on the way in and later on the way out</u>** (it wraps everything below it).

---

## 3. Where `UseHttpsRedirection` Sits

It sits **first** — before CORS, before auth, before everything that touches user data.

Why:

- If the request is plaintext, **<u>it shouldn't even reach authentication</u>**.
- Tokens, headers, credentials never travel over HTTP.
- The redirect happens before anything sensitive is read or logged.

```js
HTTP request  ──►  UseHttpsRedirection  ──►  307 Redirect to HTTPS
                                                  │
                                                  ▼
HTTPS request ──►  UseHttpsRedirection  ──►  next()  ──►  CORS  ──►  Auth  ──►  Controller
```

Only the second request continues down the pipeline. The first one is terminated at the first middleware.

---

## 4. Why It's Not "Enabling" HTTPS

The middleware is a **bouncer**, not a **builder**.

- It does not open a TLS listener.
- It does not load a certificate.
- It does not create an HTTPS endpoint.

Those are all done by **Kestrel** (the web server) when it starts — configured via `launchSettings.json` in dev, and via hosting config in production. If Kestrel isn't listening on HTTPS, there is nothing to redirect to, and the middleware either does nothing or redirects to a dead URL.

This is the exact trap the lesson warns about: `UseHttpsRedirection()` is present in the template by default, so developers assume HTTPS is working. It isn't — the **https** launch profile has to actually be running.

---

## 📌 Summary

| Concept                 | Meaning                                                                                                                                |
| ----------------------- | -------------------------------------------------------------------------------------------------------------------------------------- |
| Middleware              | <mark>**A component in the request pipeline**</mark> that inspects/transforms/short-circuits requests                                  |
| Pipeline                | <mark>**Ordered chain of middleware**</mark>; <mark>**runs top-down on request**</mark>, <mark>**<u>bottom-up on response</u>**</mark> |
| `UseHttpsRedirection()` | Returns `307` with `Location: https://...` when the request is HTTP                                                                    |
| What it does NOT do     | Does not enable, configure, or create HTTPS — Kestrel does that                                                                        |
| Why it runs first       | Plaintext requests must never reach auth, tokens, or logs                                                                              |
| Common pitfall          | Middleware present but HTTPS listener not running → redirect fails or loops                                                            |

**One-line takeaway:** `UseHttpsRedirection` <mark>**<u>is the first bouncer in the pipeline</u>**</mark> — it turns HTTP requests away at the door and tells the client to come back over HTTPS, but it has no power to build the HTTPS door itself.

---


