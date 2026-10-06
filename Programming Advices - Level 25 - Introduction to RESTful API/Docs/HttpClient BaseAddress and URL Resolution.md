# HttpClient BaseAddress and URL Resolution

## The setup

```csharp
static readonly HttpClient httpClient = new HttpClient();

static async Task Main()
{
    httpClient.BaseAddress = new Uri("http://localhost:5151/api/Students");
    await GetAllStudents();
}

static async Task GetAllStudents()
{
    var students = await httpClient.GetFromJsonAsync<List<Student>>("students");
}
```

<mark>Two confusing things happen with this code</mark>:

1. <mark>It works, **but shouldn't**</mark>.
2. <mark>**<u>Change one character and it breaks</u>**</mark>.

> Both are explained by **URI resolution rules** <mark>that aren't obvious</mark>.

---

## What `GetFromJsonAsync("path")` actually does

```csharp
var students = await httpClient.GetFromJsonAsync<List<Student>>("students");
```

<mark>One line, four steps</mark>:

1. **Builds a GET request** <mark>using `BaseAddress` + the relative path</mark>.
2. **Sends the HTTP request** to the resolved URL.
3. **Reads the response body** as JSON.
4. **Deserializes it** into `List<Student>` using `System.Text.Json`.

Returns a `Task<List<Student>?>` — nullable, because the response body might be empty.

**What it does NOT do:**

- Does not throw on 4xx/5xx status codes by itself. A 404 with an empty body returns `null`. To treat non-2xx as errors, use `GetAsync` + `EnsureSuccessStatusCode()` manually.
- Does not enforce a schema. If the JSON shape doesn't match `List<Student>`, deserialization fails silently or throws — depending on `JsonSerializerOptions`.

---

## The URL resolution rule

<mark>When you pass **a relative path**</mark> to `GetFromJsonAsync`, <mark>it's resolved against `BaseAddress`</mark> following RFC 3986. The rules most developers don't know:

| BaseAddress                 | Relative path    | Resolved URL                        |
| --------------------------- | ---------------- | ----------------------------------- |
| `http://host`               | `"api/Students"` | `http://host/api/Students`          |
| `http://host/`              | `"api/Students"` | `http://host/api/Students`          |
| `http://host/api/Students`  | `"students"`     | `http://host/api/students`          |
| `http://host/api/Students`  | `""`             | `http://host/api/Students`          |
| `http://host/api/Students/` | `"students"`     | `http://host/api/Students/students` |
| `http://host/api/Students/` | `""`             | `http://host/api/Students/`         |

**The rule in plain language:**

- <mark>If `BaseAddress` **ends with `/`**</mark>, the relative path <mark>is **appended**</mark>.
- If `BaseAddress` **doesn't end with `/`**, the **last segment of the base is replaced** by the relative path.

> <mark>**That last rule is <u>the source of every confusing behavior</u>**</mark>.

---

## What your code actually produced

```csharp
httpClient.BaseAddress = new Uri("http://localhost:5151/api/Students");
```

**No trailing slash.** Last segment is `Students`. So:

| Call                           | Resolved URL                         | Works?                                |
| ------------------------------ | ------------------------------------ | ------------------------------------- |
| `GetFromJsonAsync("")`         | `http://localhost:5151/api/Students` | exact match                           |
| `GetFromJsonAsync("students")` | `http://localhost:5151/api/students` | ⚠️ <mark>works **by accident**</mark> |

`"students"` resolves to `api/students` — which is different from `api/Students` in case. It only matches because **<mark>ASP.NET Core routing is <u>case-insensitive</u> for URLs by default</mark>**.

Now change to:

```csharp
httpClient.BaseAddress = new Uri("http://localhost:5151/api/Students/");
```

**Trailing slash.** Last segment is `Students/`. So:

| Call                           | Resolved URL                                  | Works? |
| ------------------------------ | --------------------------------------------- | ------ |
| `GetFromJsonAsync("students")` | `http://localhost:5151/api/Students/students` | ❌ 404  |
| `GetFromJsonAsync("")`         | `http://localhost:5151/api/Students/`         | ✅      |

The `404` you saw is exactly this: the trailing slash <mark>made it *append* instead of *replace*</mark>, producing `/api/Students/students` — which is not a valid route on your controller.

---

## The correct pattern

<mark>`BaseAddress` should be **the host**, <u>not a specific endpoint</u></mark>. Each call provides the full path to the resource.

```csharp
httpClient.BaseAddress = new Uri("http://localhost:5151");

var students = await httpClient.GetFromJsonAsync<List<Student>>("api/Students");
```

Resolved URL: `http://localhost:5151/api/Students` ✅

Now check a second endpoint:

```csharp
var student = await httpClient.GetFromJsonAsync<Student>("api/Students/GetStudentByID/5");
```

Resolved URL: `http://localhost:5151/api/Students/GetStudentByID/5` ✅

Both work with the same `BaseAddress`. No slash gymnastics.

**If you prefer trailing slash:**

```csharp
httpClient.BaseAddress = new Uri("http://localhost:5151/");

var students = await httpClient.GetFromJsonAsync<List<Student>>("api/Students");
```

Same result. `Uri` normalizes `http://localhost:5151/` so its path is `/` — the append behavior kicks in.

---

## The mental model

> **`BaseAddress` is a folder. Resources are files inside it. <mark>Folders end with `/`</mark>.**

Treating `BaseAddress` **<u>as a specific endpoint</u>** is what **<u>produces the surprising behavior</u>**. It's technically <mark>**not a specific endpoint**</mark> — <mark>**it's a base for resolving relative paths**</mark>.

---

## Why the browser "fixes" your URL

You observed:

> writing `http://localhost:5151/api/students` <mark>in the browser auto turns into</mark> `http://localhost:5151/api/Students`

<mark>That's **not** the browser fixing the case</mark>. That's ASP.NET Core responding to `/api/students`, matching it against the route `api/Students` <mark>(case-insensitive)</mark>, and returning the resource. The browser's address bar still shows what you typed — but if you look carefully, some browsers auto-complete or preserve what you typed.

Two things are happening:

1. <mark>**ASP.NET Core routing is case-insensitive by default.**</mark> `api/students`, `api/STUDENTS`, and `api/Students` all match the same endpoint.
2. **Some browsers display the "canonical" form** if the site redirects or does any URL normalization. But in a typical Web API, no redirect happens — the request just succeeds.

So it's not the browser correcting you. <mark>It's the API **accepting** **<u>the casing difference</u>**</mark>.

**Why this matters:** if you ever deploy to a system with case-sensitive routing (Linux with certain configs, or a reverse proxy that normalizes strictly), <mark>**the "works in dev" behavior <u>breaks</u> in production**</mark>. <mark>**<u>Don't rely on case-insensitive routing</u>**</mark>.

---

## Summary table

| BaseAddress                 | Path passed      | Resolved                            | Result               |
| --------------------------- | ---------------- | ----------------------------------- | -------------------- |
| `http://host/api/Students`  | `""`             | `http://host/api/Students`          | ✅                    |
| `http://host/api/Students`  | `"students"`     | `http://host/api/students`          | ✅ (case-insensitive) |
| `http://host/api/Students/` | `""`             | `http://host/api/Students/`         | ✅                    |
| `http://host/api/Students/` | `"students"`     | `http://host/api/Students/students` | ❌ 404                |
| `http://host`               | `"api/Students"` | `http://host/api/Students`          | ✅                    |

**Rule:** `BaseAddress` = host (<mark>with or without</mark> trailing `/`), relative path = full resource path.

## Which to use

**Use `http://host/` (with the slash).** Two reasons:

1. <mark>**It's clearer.**</mark> It signals "this is a base, and I intend to append paths to it."

2. <mark>**It's consistent.**</mark> If you ever move to `http://host/api/`, the trailing slash is what keeps appending behavior sane. **<u>Using it from the start avoids surprises later</u>**.

> <mark>Microsoft's own documentation</mark> uses `http://host/` in every example. <mark>**<u>Follow the convention</u>**</mark>.

---

## Key takeaways

- `GetFromJsonAsync<T>(path)` sends GET, deserializes JSON to `T`, returns `T?`.
- It does **not** throw on HTTP error status codes. Only on network errors or JSON parse failures.
- `BaseAddress` should be the **host**, not a specific endpoint.
- Relative paths are **appended** if base ends with `/`; the last segment is **replaced** otherwise.
- ASP.NET Core routing is **case-insensitive** by default, which hides URL-casing bugs that would break in other environments.
- Never rely on case-insensitive routing for production URLs.

---
