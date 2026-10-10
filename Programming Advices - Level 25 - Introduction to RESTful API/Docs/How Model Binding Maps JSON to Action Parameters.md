# How Model Binding Maps JSON to Action Parameters

## The question

When a POST request arrives with a JSON body:

```json
{
  "id": 0,
  "name": "JY",
  "age": 67,
  "grade": 78
}
```

How does it end up in the action method's parameter?

```csharp
public ActionResult<StudentDTO> AddStudent(StudentDTO newStudent)
```

> <mark>Where does `newStudent` get its values?</mark>

---

## The answer: Model Binding

<mark>**ASP.NET Core has a pipeline step** that runs **before** your action method</mark>. <mark>It's called **model binding**</mark>.

**<u>It reads the HTTP request</u>** (body, route, query string, headers) and <mark>**constructs the parameter objects** your action method **expects**</mark>.

> <mark>"Auto-mapped" isn't the exact term. The exact term is <u>**model binding** + **JSON deserialization**</u>.</mark>

---

## The full flow

```js
1. Client sends POST /api/Students with a JSON body.
2. ASP.NET Core routing matches the request to AddStudent.
3. Model binding runs before the method:
   a. It sees the parameter: StudentDTO newStudent.
   b. It determines the source (body, route, query, etc.).
   c. It deserializes the body into a StudentDTO instance.
   d. If deserialization fails, it returns 400 Bad Request
      and the action method is never invoked.
4. Validation runs (DataAnnotations, if any).
5. The action method executes with the populated StudentDTO.
```

<mark>**All of this happens before your first line of code runs.**</mark>

---

## How the framework picks the source

ASP.NET Core <mark>decides **where to read each parameter** based on its **type**</mark>:

| Parameter type                                             | Default source                  |
| ---------------------------------------------------------- | ------------------------------- |
| Simple types (`int`, `string`, `bool`, `Guid`, `DateTime`) | Route, query string, or headers |
| Complex types (classes, records)                           | Request body (JSON)             |

So:

```csharp
public ActionResult<StudentDTO> GetStudentById(int id)              // id from route or query
public ActionResult<StudentDTO> AddStudent(StudentDTO newStudent)   // newStudent from body
```

`int id` is a simple type → route/query.
`StudentDTO newStudent` is a complex type → JSON body.

<mark>**<u>You can override the default</u>**</mark> with attributes:

```csharp
public ActionResult<StudentDTO> AddStudent([FromBody] StudentDTO newStudent)
public ActionResult<StudentDTO> GetById([FromRoute] int id)
public ActionResult<StudentDTO> Search([FromQuery] string name)
public ActionResult<StudentDTO> Upload([FromForm] IFormFile file)
```

<mark>For most cases, **the defaults work**</mark>. No attribute needed.

---

## What actually deserializes the JSON

The default JSON serializer in ASP.NET Core is **`System.Text.Json`**.

When the body arrives, the framework runs the equivalent of:

```csharp
var student = JsonSerializer.Deserialize<StudentDTO>(requestBody);
```

That produces a `StudentDTO` object with properties <mark>populated from the JSON</mark>.

---

## The matching rules

`System.Text.Json` <mark>matches JSON properties to C# properties **by name, <u>case-insensitive</u>**</mark>:

| JSON key      | C# property | Match? |
| ------------- | ----------- | ------ |
| `"name"`      | `Name`      | ✅      |
| `"Name"`      | `Name`      | ✅      |
| `"NAME"`      | `Name`      | ✅      |
| `"full_name"` | `Name`      | ❌      |
| `"grade"`     | `Grade`     | ✅      |

**Rules:**

- **Case-insensitive.** `"name"`, `"Name"`, `"NAME"` <mark>all map to `Name`</mark>.
- **Missing JSON fields** → C# properties <mark>left at their default values</mark> (`0` for `int`, `null` for `string`).
- **Extra JSON fields** → <mark>**ignored silently. No error**</mark>.
- **Wrong type** (e.g., a string where an int is expected) →<mark> `400 Bad Request`</mark>.

<mark>If you need to **map a JSON key with a different name**</mark>, use `[JsonPropertyName]`:

```csharp
[JsonPropertyName("full_name")]
public string Name { get; set; }
```

Now the client can send `"full_name": "JY"` and it maps to `Name`.

---

## What happens on errors

ASP.NET Core handles three failure modes:

| Scenario                             | What happens                                                                  |
| ------------------------------------ | ----------------------------------------------------------------------------- |
| **Malformed JSON** (syntax error)    | `400 Bad Request` — <mark>the action method **never runs**</mark>             |
| **Type mismatch** (`"age": "sixty"`) | `400 Bad Request` — <mark>the action method **never runs**</mark>             |
| **Missing fields**                   | The method runs — <mark>missing fields are **at their default values**</mark> |
| **Extra fields**                     | The method runs — <mark>**<u>extra fields are ignored</u>**</mark>            |

So if you send:

```json
{ "name": "JY", "age": "not-a-number", "grade": 80 }
```

The server returns `400` with a `ValidationProblemDetails` body, and `AddStudent` is not invoked. <mark>**<u>The framework did its job before your code</u>**</mark>.

---

## DataAnnotations for required fields

The default behavior tolerates missing fields. To force them to be present:

```csharp
using System.ComponentModel.DataAnnotations;

public class StudentDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(0, 150)]
    public int Age { get; set; }

    [Range(0, 100)]
    public int Grade { get; set; }
}
```

Now if `"name"` is missing from the JSON, ASP.NET Core returns `400 Bad Request` **automatically** — <mark>before your method runs</mark>.

That means your manual checks:

```csharp
if (newStudent == null
    || string.IsNullOrEmpty(newStudent.Name)
    || newStudent.Age < 0
    || newStudent.Grade < 0)
```

...<mark>**become redundant** for the fields the framework validates</mark>. You can remove them, and <mark>**the framework enforces the rules for you**</mark>.

**The overlap:** <mark>manual checks and DataAnnotations **do the same thing**</mark>. DataAnnotations **<u>is declarative</u>** — you write the rules once, and **<u>the framework applies them at every endpoint where the DTO is bound</u>**. Manual checks **<u>are per-method</u>**.

<mark>For production APIs, **<u>DataAnnotations (or FluentValidation) wins</u>** because it **doesn't duplicate across endpoints**</mark>.

---

## The mental model

> <mark>**Model binding is all-or-nothing.**</mark> Either the body deserializes into a valid object (and your method runs), or it fails (and the framework returns `400` before your method). There's no "partially bound, some fields null" middle state for a body-bound complex type — **<u>except for the value-type defaults</u>**, which DataAnnotations catch.

> That's why `newStudent == null` <mark>**is dead code** in your case</mark>.
> 
> <mark>The DTO's DataAnnotations **are the contract**. The framework **<u>enforces it</u>**.</mark>

---

## The complete pipeline

```js
POST /api/Students
Body: { "id": 0, "name": "JY", "age": 67, "grade": 78 }
                ↓
    ASP.NET Core routing
    matches URL to AddStudent
                ↓
    Model binding
    StudentDTO newStudent ← deserialize body
    System.Text.Json reads JSON, builds the object
                ↓
    Model validation
    [Required], [Range], etc. — if any fails, return 400
                ↓
    Action method
    AddStudent(newStudent) runs with populated data
```

By the time `AddStudent` executes, <mark>`newStudent` is fully populated. **Your code just uses it**</mark>.

---

## How to inspect what's happening

**1. Send bad JSON and see the response.**

```bash
curl -X POST http://localhost:5151/api/Students \
     -H "Content-Type: application/json" \
     -d '{ "name": "JY", "age": "not-a-number", "grade": 80 }'
```

You'll get a `400` with a validation error message. <mark>**The action method never runs**</mark>.

**2. <mark>Log the model binding process.</mark>**

In `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore.Mvc.ModelBinding": "Debug"
    }
  }
}
```

Now the console shows every binding attempt.

**3. Inspect the JSON serializer options.**

To see the exact options `System.Text.Json` uses in your app, add a temporary endpoint that prints them, or check `Program.cs` where `AddControllers()` is configured. Default options include case-insensitive matching and camelCase output (JSON is written as `"name"`, not `"Name"`).

---

## Why the framework does this

In older frameworks, you'd write:

```csharp
var body = new StreamReader(Request.InputStream).ReadToEnd();
var student = JsonSerializer.Deserialize<StudentDTO>(body);
if (student == null) return BadRequest();
```

> <mark>**<u>Every endpoint. Every time.</u>**</mark>

Model binding moves that to the framework:

- **Source selection** — body, route, query, headers, <mark>**automatically chosen <u>by parameter type</u>**</mark>.
- **Deserialization** — JSON to object, <mark>**handled**</mark>.
- **Error handling** — bad JSON → `400`, <mark>**no code needed**</mark>.
- **Validation** — <mark>DataAnnotations enforced **on every bound object**</mark>.

> <mark>**Controller methods <u>stay focused on business logic</u>. The framework <u>handles the HTTP plumbing</u>.**</mark>

---

## The mental model

> <mark>The action method's **signature is a contract**</mark>. You declare what the method needs. <mark>Model binding **figures out** how to get it from the request</mark>.

<mark>Same idea as dependency injection</mark>, but for HTTP inputs. You describe the shape; the framework provides the value.

---

## Practical consequences

**1. JSON keys and C# property names are linked.**

If you rename `Name` to `FullName` in `StudentDTO`, the client's JSON must also change from `"name"` to `"fullName"` (unless you add `[JsonPropertyName("name")]` to preserve the wire format).

**2. Missing fields default, not error — unless you use `[Required]`.**

The server accepts incomplete objects by default. If that's not what you want, use DataAnnotations.

**3. Extra fields are ignored.**

If the client sends a field the DTO doesn't have, it's silently dropped. No error. This is often a security feature — you control exactly what fields the client can set.

**4. Validation order: deserialization → validation → action method.**

The framework handles both before your code runs. This is why `[ApiController]` is important — it enables automatic validation responses. Without it, you'd have to check `ModelState.IsValid` manually.

**5. `[ApiController]` changes the behavior.**

With `[ApiController]` on the class, model binding errors trigger automatic `400` responses with details. Without it, you'd get a `null` parameter and no error. Always use `[ApiController]` on API controllers.

---

## Summary

| Question                                       | Answer                                                               |
| ---------------------------------------------- | -------------------------------------------------------------------- |
| What's the mechanism called?                   | Model binding + JSON deserialization                                 |
| Which component does it?                       | ASP.NET Core's `ModelBinder` + `System.Text.Json`                    |
| When does it run?                              | Before the action method is invoked                                  |
| Where does a complex-type parameter come from? | The request body (JSON by default)                                   |
| Where does `int id` come from?                 | Route, query string, or headers                                      |
| Is property matching case-sensitive?           | No — `System.Text.Json` matches case-insensitively                   |
| What happens on invalid JSON?                  | `400 Bad Request`, method never runs                                 |
| What happens on missing fields?                | Method runs, fields at their defaults — unless `[Required]`          |
| How do you override the source?                | `[FromBody]`, `[FromRoute]`, `[FromQuery]`, `[FromForm]`             |
| Can you log what's happening?                  | Yes — set `Microsoft.AspNetCore.Mvc.ModelBinding` to `Debug`         |
| Why does it exist?                             | So you don't parse HTTP yourself; the framework handles the plumbing |

---

## One-line summary

> Model binding is the ASP.NET Core mechanism that reads the HTTP request and populates your action method's parameters. For a complex type like `StudentDTO`, the default source is the JSON body, deserialized by `System.Text.Json` with case-insensitive property matching. It runs before your code, returns `400` on deserialization errors, and (with `[ApiController]` + DataAnnotations) enforces validation rules automatically.

---
