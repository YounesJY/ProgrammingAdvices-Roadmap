# GetFromJsonAsync vs GetAsync — Choosing the Right Tool

## The problem

`GetFromJsonAsync<T>` <mark>is convenient</mark> but <mark>breaks on some responses</mark>. `GetAsync` <mark>**is verbose** but flexible</mark>. Knowing **<u>when</u>** to use which saves you from mysterious errors.

---

## What `GetFromJsonAsync<T>` does

```csharp
var student = await httpClient.GetFromJsonAsync<Student>($"api/Students/{id}");
```

One line. But under the hood it does three things:

```csharp
var response = await client.GetAsync(url);
response.EnsureSuccessStatusCode();       // throws on 4xx/5xx
var stream = await response.Content.ReadAsStreamAsync();
return await JsonSerializer.DeserializeAsync<T>(stream);
```

| Step                        | Behavior                                      |
| --------------------------- | --------------------------------------------- |
| Sends the request           | Standard GET                                  |
| `EnsureSuccessStatusCode()` | **Throws** if status is not 2xx               |
| Deserializes the body       | **Throws** if body is empty or not valid JSON |

<mark><u>**Two implicit assumptions:**</u></mark>

1. Success means `200 OK` with a body.
2. Failure means throw an exception.

<mark>**Both are usually true**</mark>. When they're not, <mark>**<u>this method breaks</u>**</mark>.

---

## What `GetAsync` does

```csharp
var response = await httpClient.GetAsync($"api/Students/{id}");
```

Sends the GET request and returns the raw `HttpResponseMessage`. <mark>**No assumptions.**</mark> You get:

- `response.StatusCode` — the HTTP status.
- `response.Headers` — response headers.
- `response.Content` — the body (as a stream, string, or JSON if you ask).

<mark>Nothing is thrown for 4xx/5xx</mark>. Nothing is deserialized automatically. <mark>**<u>You decide what to do</u>**</mark>.

---

## The break case: `204 No Content`

The classic scenario:

```js
Server returns:  HTTP/1.1 204 No Content
                 (empty body)
```

The status is 2xx, so `EnsureSuccessStatusCode()` passes. Then `JsonSerializer.DeserializeAsync<T>` tries to parse an empty body:

```js
System.Text.Json.JsonException: The input does not contain any JSON tokens.
```

> <mark>The call **fails** even though the request succeeded.</mark>

`GetFromJsonAsync` cannot handle `204 No Content`. <mark>**It assumes** the body exists</mark>.

---

## The decision rule

```js
Does the endpoint ever return:
  - 204 No Content, OR
  - a non-2xx status I want to handle differently, OR
  - a header I need to read before deserializing?

├── No  → GetFromJsonAsync<T>
│         (endpoint always returns 200 OK with a JSON body)
│
└── Yes → GetAsync + manual checks + ReadFromJsonAsync
          (you control the flow)
```

> <mark>**One question:**</mark> can the success path return <mark>anything other than `200 OK` with a body</mark>?

- **No** → `GetFromJsonAsync`.
- **Yes** → `GetAsync`.

---

## Applied to the StudentAPI

| Endpoint                         | Success returns  | `204` possible?               | Recommended                    |
| -------------------------------- | ---------------- | ----------------------------- | ------------------------------ |
| `GET /api/Students/All`          | List of students | Yes (empty list)              | `GetAsync`                     |
| `GET /api/Students/Passed`       | List of students | Yes (no passing students)     | `GetAsync`                     |
| `GET /api/Students/AverageGrade` | A number         | Yes (no students)             | `GetAsync`                     |
| `GET /api/Students/{id}`         | One student      | No (returns `404` if missing) | Either — pick one consistently |

> <mark>**Rule of thumb:**</mark> if the server can return `204`, use `GetAsync`.

---

## The manual pattern with `GetAsync`

```csharp
var response = await httpClient.GetAsync("api/Students/All");

// 1. Handle the "no content" case first
if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
{
    Console.WriteLine("No students.");
    return;
}

// 2. Handle errors — read the server's message
if (!response.IsSuccessStatusCode)
{
    var error = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Error {(int)response.StatusCode}: {error}");
    return;
}

// 3. Success — deserialize the body
var students = await response.Content.ReadFromJsonAsync<List<Student>>();
```

<mark>Three branches, explicit, **no assumptions**.</mark>

---

## The shortcut with `GetFromJsonAsync`

```csharp
try
{
    var student = await httpClient.GetFromJsonAsync<Student>($"api/Students/{id}");
    if (student != null)
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
}
catch (HttpRequestException ex)
{
    // Thrown by EnsureSuccessStatusCode on 4xx/5xx
    Console.WriteLine($"Request failed: {ex.Message}");
}
catch (JsonException ex)
{
    // Thrown if the body is empty or invalid
    Console.WriteLine($"Invalid JSON: {ex.Message}");
}
```

<mark>**<u>Two catch blocks needed.</u>**</mark> Because failures come as exceptions, not as return values.

---

## Why the shortcut exists

Because 80% of endpoints on the success path return `200 OK` with JSON. For those, one line beats five.

The problem is the other 20%:

- `204 No Content` responses
- Errors whose body you want to read (but `EnsureSuccessStatusCode` throws before you get the chance)
- Success statuses other than `200` (`201`, `202`, `206`)
- Headers you need to inspect

For those, drop to `GetAsync`.

---

## The same split for POST, PUT, PATCH

| Verb   | Shortcut                       | Assumption                     | Manual                     |
| ------ | ------------------------------ | ------------------------------ | -------------------------- |
| POST   | `PostAsJsonAsync(url, value)`  | Success = 2xx with a JSON body | `PostAsync(url, content)`  |
| PUT    | `PutAsJsonAsync(url, value)`   | Same                           | `PutAsync(url, content)`   |
| PATCH  | `PatchAsJsonAsync(url, value)` | Same                           | `PatchAsync(url, content)` |
| DELETE | —                              | (no shortcut)                  | `DeleteAsync(url)`         |

<mark>**DELETE has no shortcut.**</mark> Because a successful DELETE typically returns `204 No Content` — no body to deserialize. So there's no `DeleteAsJsonAsync`. You always use `DeleteAsync` and check the status manually.

That's a strong hint about which verb responses tend to have bodies:

| Verb          | Success body?                      |
| ------------- | ---------------------------------- |
| GET           | Usually yes                        |
| POST (create) | Usually yes (the created resource) |
| PUT           | Sometimes                          |
| DELETE        | No                                 |
| PATCH         | Sometimes                          |

<mark>Shortcuts exist **where bodies are common**.</mark> Where they aren't, you go manual.

---

## When `GetFromJsonAsync` still throws

Even for endpoints that always return `200 OK` with a body, `GetFromJsonAsync` throws on:

- **Network failure** (DNS, timeout, connection reset) — `HttpRequestException`.
- **4xx/5xx status** — `HttpRequestException` from `EnsureSuccessStatusCode`.
- **Invalid JSON in the body** — `JsonException`.
- **Empty body** — `JsonException`.

So even when you use it, you usually want a `try/catch`. The convenience is about the happy path, not the failure path.

---

## The mental template

**For endpoints that always return `200 OK` with a body:**

```csharp
try
{
    var data = await httpClient.GetFromJsonAsync<T>(url);
    if (data != null)
    {
        // use data
    }
}
catch (HttpRequestException ex) { /* network or HTTP error */ }
catch (JsonException ex) { /* deserialization failed */ }
```

**For endpoints that might return `204` or non-2xx:**

```csharp
var response = await httpClient.GetAsync(url);

if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
{
    // handle "nothing to return"
    return;
}

if (!response.IsSuccessStatusCode)
{
    var error = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Error {(int)response.StatusCode}: {error}");
    return;
}

var data = await response.Content.ReadFromJsonAsync<T>();
// use data
```

**Rule:** when in doubt, use `GetAsync`. The extra lines give you full control and prevent surprises from hidden assumptions.

---

## Why this matters in real applications

The `GetFromJsonAsync` shortcut hides two decisions:

1. **How to handle non-2xx responses.** It throws, which forces exception-based control flow. Some teams prefer explicit status checks over exceptions.

2. **Whether the body is expected.** It assumes yes. When that assumption is wrong, you get a JSON parse error that looks unrelated to the real cause.

For a small client app, both are fine to overlook. For a production client hitting dozens of endpoints with mixed behaviors, the extra control of `GetAsync` pays for itself — you don't have to keep track of which endpoints can return `204` and which can't.

**Practical rule:** if you can't remember whether an endpoint returns `204`, use `GetAsync`. You'll handle it correctly whether it does or doesn't.

---

## Summary

| Aspect             | `GetFromJsonAsync<T>`                             | `GetAsync`                                                    |
| ------------------ | ------------------------------------------------- | ------------------------------------------------------------- |
| Lines of code      | 1                                                 | 5–10                                                          |
| Assumes 200 + body | Yes                                               | No                                                            |
| Handles 204        | ❌                                                 | ✅                                                             |
| Throws on 4xx/5xx  | Yes                                               | No (returns the response)                                     |
| Reads error body   | Not directly                                      | Yes                                                           |
| Good for           | Simple endpoints that always return 200 with JSON | Endpoints with `204`, or where you need status/header control |

<mark>**One-line rule:**</mark> `GetFromJsonAsync<T>` for endpoints that always return `200 OK` with a body. `GetAsync` for everything else. When in doubt, `GetAsync` — <mark>**the extra lines buy you control and prevent surprises**</mark>.

---


