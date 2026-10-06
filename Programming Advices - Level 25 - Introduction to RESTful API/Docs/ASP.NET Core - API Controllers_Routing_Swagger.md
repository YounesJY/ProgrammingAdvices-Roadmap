# ASP.NET Core — API Controllers, Routing, and Swagger

---

## 0. What "Web Service" actually means

# Q

> Before we even start — when we say RESTful APIs are "web services," they're called *services* because they provide/expose internal services to the outside world via an API layer. That layer upgrades local functions — normal functions that serve the internal local system — into exposed functions/services through the ingredients of that API layer. So now, since they serve the world and they do it via the web, they're referred to as **web services**.

You got the intuition right. Let me sharpen it:

A **service** is any piece of software <mark>**that does work** for someone else</mark>. A **web service** is <mark>a service **you reach over the web**</mark>. A **RESTful API** <mark>is **a specific way of exposing** a web service</mark> — following REST constraints (resources, stateless, standard HTTP methods).

The "upgrade" you described is accurate:

- **Local function** — a caller <mark>**in the same process**</mark> calls it directly.
- **Web service** — a caller <mark>**anywhere on the network**</mark> calls it over HTTP.
- **API layer** — <mark>**<u>the code that translates HTTP requests into function calls</u>**</mark>, and function return values back into HTTP responses.

<mark>An ASP.NET Core **Controller** is exactly **that layer**</mark>. Each public method <mark>**becomes an endpoint**</mark> someone on the web can call.

---

## 1. What is Swagger?

<mark>**Swagger** is a specification + tooling for **describing and testing APIs**.</mark>

Originally, <u>"Swagger" was the name of the spec</u>. <mark>It's now called **OpenAPI Specification (OAS)**</mark>. The tooling kept the old name for a while, and ASP.NET Core's default library is still `Swashbuckle`, <mark>which generates a Swagger UI</mark>.

Swagger <mark>**<u>does three things</u>**</mark>:

| Purpose               | What it does                                                                                                                          |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| **Documentation**     | <mark>**Lists every endpoint**</mark>, its parameters, and its response shape — auto-generated from your code                         |
| **Testing**           | <mark>Provides a UI</mark> where **<u>you can send requests and see responses</u>**, <mark>**without writing any client code**</mark> |
| **Client generation** | <mark>The OpenAPI spec</mark> can feed <mark>tools that generate client SDKs</mark> in any language                                   |

**Why it matters:** without Swagger, <mark>**you'd need to write an API doc by hand**</mark> and <mark>**keep it in sync with the code**</mark>. **<u>Swagger generates the doc *from* the code</u>**, so it can't drift.

In your project, when you run the app and navigate to `/swagger`, you see a generated page listing `MyFirstAPI`, `YourName`, `sum`, `multi` — each with an input box for parameters and a "Try it out" button.

---

---

### **Minimum you need to know right now:**

**OpenAPI** is <mark>**<u>the *name of the standard* that describes what an API looks like</u>**</mark> — which endpoints exist, what parameters they take, what they return.

**Swagger** is <mark>**the *tool*** that **reads that description** and shows you the nice interactive page</mark>.

That's the only relationship you need:

> OpenAPI = the format (the spec). Swagger = the tool that reads it and shows the UI.

You write neither by hand. ASP.NET Core generates the OpenAPI description automatically from your controllers, and Swagger UI displays it. You just open `/swagger` and use it.

**One-line origin story, because it explains the naming confusion:**

- Originally, the spec was called **Swagger**.
- In 2015 it was donated to a standards body and renamed to **OpenAPI**.
- The tooling kept the name **Swagger** (Swagger UI, Swashbuckle).
- So today: the spec is OpenAPI, the tools are Swagger.

---

---

## 2. Swagger vs Postman / Bruno

| Tool        | Role                                                                                                                                                                                                                                      |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Swagger** | Lives **inside your app**. Auto-generates docs from code. Has a testing UI. <mark>Meant for consumers of the API</mark> to discover and try endpoints.                                                                                    |
| **Postman** | <mark>Standalone client</mark>. <mark>You build requests **manually**</mark>. <mark>**No integration**</mark> with your app's code. <mark>Used by developers</mark> to **<u>test APIs during development</u>**, or by external consumers. |
| **Bruno**   | Same idea as Postman, but <mark>**<u>offline-first and file-based</u>**</mark> (stores collections as files in your repo instead of in a cloud account).                                                                                  |

**Key differences:**

- **Swagger <mark>requires no setup</mark> to test endpoints** — it reads your code and generates everything. Postman/Bruno <mark>**require** you to configure **each request by hand**</mark>.
- **Postman/Bruno can test anything** — including third-party APIs, external services, WebSockets, GraphQL, etc. <mark>Swagger is **bound to your own API**</mark>.
- **Swagger <mark>is documentation-first</mark>.** Postman/Bruno <mark>**<u>are testing-first</u>**</mark>.
- **Postman/Bruno can save and share collections.** Swagger generates a fresh UI each time you run the app.

<mark>In practice, **you use both**</mark>: Swagger during early development (to see what you built), Postman/Bruno for saved test suites you run repeatedly.

---

## 3. Is the API name really the controller name?

Mostly yes, but not exactly. The name is **whatever the route attribute says** — <mark>**it just *defaults* to the controller name <u>minus the "Controller" suffix</u>**</mark>.

Your code:

```csharp
[Route("api/MyFirstAPI")]
public class MyFirtstAPIController : ControllerBase
```

The route says `api/MyFirstAPI`. <mark>So the endpoint base is `/api/MyFirstAPI`</mark>. It doesn't matter that the class is named `MyFirtstAPIController`.

If you had written:

```csharp
[Route("api/[controller]")]
```

<mark>Then `[controller]` is a **placeholder** replaced by <u>the class name minus "Controller"</u></mark>. So `MyFirtstAPIController` becomes `MyFirstAPI`.

**Rule:**

- `[Route("api/MyFirstAPI")]` — <mark>hard-coded</mark>, <mark>**<u>always </u>**</mark>`/api/MyFirstAPI`.
- `[Route("api/[controller]")]` — <mark>dynamic</mark>, <u>**replaced**</u> by class name minus "Controller".

<mark>**`[controller]` is the recommended default <u>when you have one controller per resource</u>**</mark>. It keeps the route in sync with the class name — rename the class, **<u>the route updates automatically</u>**. Use a hard-coded string **<u>when you want the URL to be independent of the class name</u>**.

---

## 4. Client & server on same domain + port, different routes?

Yes, during development. When you run the app:

- **Server** runs on `https://localhost:7007` (or whatever port VS assigns).
- **Client** — during development, the same ASP.NET Core process can also serve static files, or you run a separate dev server for a frontend on a different port.

For a pure API project (like yours), there's no client. <mark>The "client" is whatever calls the API</mark> — Swagger UI, Postman, a browser, another service.

<mark>**In production**, both can live on the same domain + port</mark>:

```js
https://example.com/            → frontend (static files or SPA)
https://example.com/api/...      → API endpoints
```

<mark>**Same origin, same port, <u>different routes</u>**</mark>. <mark>This is the cleanest deployment because there's no CORS complexity</mark>.

**If they're on different domains:**

```js
https://example.com/            → frontend
https://api.example.com/...      → API
```

<mark>Different origins → CORS must be configured on the API</mark> to allow the frontend to call it.

---

---

### **Minimum you need to know right now:**

<mark>**CORS** is a **browser security rule**</mark> that stops a web page from one domain from calling an API on a different domain — unless the API explicitly says "yes, I allow it."

That's it.

**Why it exists:** without it, any website you visit could quietly make requests to your bank's API using your logged-in cookies. The browser blocks cross-origin calls by default to prevent that.

**Origin** = the combination of **scheme + domain + port**:

- `https://example.com` ≠ `http://example.com` (different scheme)
- `https://example.com` ≠ `https://api.example.com` (different subdomain)
- `https://localhost:7007` ≠ `https://localhost:5000` (different port)

<mark>If any of those three differ, it's a **cross-origin** request.</mark>

**What happens in practice:**

- **Frontend and API on same origin** (same scheme + domain + port, different routes) → no CORS, requests just work.
- **Frontend and API on different origins** → the browser blocks the call unless the API sends special headers saying "I allow calls from this origin."

**How the API allows it:** it sends back a header like:

```
Access-Control-Allow-Origin: https://myfrontend.com
```

The browser sees that header and lets the response through. Without it, the browser throws a CORS error and the frontend gets nothing.

**What you'll do when you get there:** in ASP.NET Core, you configure CORS in `Program.cs` with `builder.Services.AddCors(...)` and add a policy listing which origins are allowed. Three lines, not complicated. You'll learn it properly when the course reaches that section.

**For now:** just remember the definition — **the browser's rule against cross-domain API calls**. Same origin, no problem. Different origin, the API must opt in.

---

---

## 5. `/swagger` vs `/api/MyFirstAPI`

They're completely different things.

| URL                                            | What it does                                                                                                                                                                                                                            |
| ---------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `https://localhost:7007/swagger`               | The Swagger UI page. <mark>It's a **development tool** </mark>— the ASP.NET Core middleware generates an HTML page **<u>you open in a browser <mark>to explore the API</mark></u>**. <mark>**It's not an endpoint of your API**</mark>. |
| `https://localhost:7007/api/MyFirstAPI/MyName` | <mark>**An actual API endpoint**</mark>. It runs the `GetMyName` method and returns the string.                                                                                                                                         |

Swagger UI is a separate route registered by middleware — <mark>usually only enabled in development, disabled in production</mark> so consumers don't see your internal docs.

**TL;DR:**

- `/swagger` → documentation tool
- `/api/MyFirstAPI/...` → your actual API
- They <mark>**live on the same server, but serve different purposes**</mark>.

---

## 6. Inspecting `[HttpGet]`

```csharp
[HttpGet("MyName", Name = "MyName")]
public string GetMyName()
{
    return "My Name is Mohammed Abu-Hadhoud";
}
```

<mark>`HttpGet` is an **attribute**</mark> — a class that <mark>derives from `HttpMethodAttribute`</mark> (which derives from `Attribute`).

**Constructor:**

```csharp
public HttpGetAttribute(string template)
```

The `template` is <mark>the **route segment** <u>**appended**</u> to **the controller's route**</mark>. So the full route is:

```js
[Route("api/MyFirstAPI")]  →  controller base
[HttpGet("MyName")]        →  method segment
                              = /api/MyFirstAPI/MyName
```

**Properties:**

| Property   | Purpose                                                                                                                         |
| ---------- | ------------------------------------------------------------------------------------------------------------------------------- |
| `Template` | The route template (from constructor)                                                                                           |
| `Name`     | **<u>A unique name for the endpoint</u>**. <mark>**Used by `Url.Link()` and API versioning**</mark>. <mark>Not required</mark>. |
| `Order`    | Priority when multiple routes could match                                                                                       |

The `Name = "MyName"` part **<u>is optional</u>**. It <mark>**lets you generate a URL by name**</mark> elsewhere in the code:

```csharp
string url = Url.Link("MyName", null);   // returns "https://localhost:7007/api/MyFirstAPI/MyName"
```

> Useful <mark>**<u>when you don't want to hard-code URLs</u>**</mark>.

**Other HTTP method attributes:** `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`, `[HttpPatch]`. Same shape, different verb.

**Attribute without a template:**

```csharp
[HttpGet]
public string Something() { ... }
```

No explicit route segment. <mark>ASP.NET Core uses the **method name** as the default segment</mark>. So `Something` becomes `/api/MyFirstAPI/Something`.

**Why it defaults to the method name:** <mark>ASP.NET Core's convention</mark> is "if you don't specify a route, use the action name." It's convenient for simple cases but leads to URLs tied to method names — <mark>**which is fragile**</mark>. If you rename `GetMyName` to `FetchMyName`, <mark>**<u>the URL changes and breaks any client</u>**</mark>. **<u>That's why most APIs specify explicit routes</u>**.

---

## 7. Ambiguity when two actions have the same route

If two methods map to the same URL and verb:

```csharp
[HttpGet("Get")]
public string A() { return "A"; }

[HttpGet("Get")]
public string B() { return "B"; }
```

At startup (or first request), ASP.NET Core throws:

```
AmbiguousMatchException: The request matched multiple endpoints.
```

The router can't decide which action to invoke. Fix: <mark>give them distinct routes, or distinguish by HTTP verb</mark> (`[HttpGet]` vs `[HttpPost]` **<u>on the same route is fine</u>** — <mark>**different verbs don't conflict**</mark>).

---

## 8. `num1, num2` — route params or placeholders?

Both. They're **route parameters**.

```csharp
[HttpGet("sum/{Num1}/{Num2}")]
public int Sum2Numbers(int Num1, int Num2)
```

<mark>The `{Num1}` and `{Num2}` in the template are **placeholders**</mark>. **<u>At runtime</u>**, <mark>ASP.NET Core **<u>extracts</u>** the values from the URL and **<u>binds them</u>** to the C# method parameters</mark> of the same name.

**Request:** `GET /api/MyFirstAPI/sum/5/3`

**Binding:**

- `{Num1}` matches `5` → `Num1` parameter = `5`
- `{Num2}` matches `3` → `Num2` parameter = `3`

Method runs: `5 + 3 = 8`. Response: `8`.

<mark>**Not query parameters.**</mark> Query parameters would look like:

```
/api/MyFirstAPI/sum?num1=5&num2=3
```

> <mark>Route parameters **are part of the URL path**. Query parameters **come after** `?`.</mark>

**Rules for route parameters:**

- The name in the template <mark>**<u>must match</u>**</mark> the method parameter name (<mark>**case-insensitive**</mark>).
- The <mark>type is inferred</mark> from the method parameter's C# type (`int`, `string`, `Guid`, etc.). ASP.NET Core parses it automatically.
- If parsing fails (e.g., `/sum/abc/3`), the route doesn't match and returns `404` — unless you specify a route constraint like `{Num1:int}`.

<mark>**Route constraints** — <u>optional but useful</u></mark>:

```csharp
[HttpGet("sum/{Num1:int}/{Num2:int}")]
```

Now `{Num1}` only matches if the segment is an integer. `/sum/abc/3` won't match at all; it goes to the next route or returns `404`. Cleaner than relying on automatic parsing failures.

---

## 📌 Summary

| Concept                          | Answer                                                                                                            |
| -------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| **Web service**                  | Any service reachable over the web. RESTful API is one style of web service.                                      |
| **Swagger**                      | Auto-generated API documentation + testing UI, built from your code.                                              |
| **Swagger vs Postman**           | Swagger lives inside your app, generates itself from code. Postman is a standalone client you configure manually. |
| **API name**                     | Whatever `[Route(...)]` says. `[controller]` is a placeholder replaced by the class name minus "Controller".      |
| **Client & server same origin?** | Yes, in production, using different routes. Common setup.                                                         |
| **`/swagger` vs `/api/...`**     | `/swagger` = docs UI. `/api/...` = your actual API endpoints.                                                     |
| **`[HttpGet]`**                  | Attribute that maps a method to a route + verb. Template is the route segment; Name is optional.                  |
| **Default route**                | If no template is given, the method name is used. Convenient but fragile.                                         |
| **Ambiguity**                    | Two actions with the same verb + route → `AmbiguousMatchException` at runtime.                                    |
| **`{Num1}`**                     | Route parameter — a placeholder in the URL path, bound to a C# method parameter by name. Not a query param.       |

---
