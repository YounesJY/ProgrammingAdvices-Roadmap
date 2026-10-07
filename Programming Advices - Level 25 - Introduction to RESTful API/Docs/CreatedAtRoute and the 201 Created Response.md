# CreatedAtRoute and the 201 Created Response

## The problem it solves

You just created a resource on the server. The client <mark>**needs to know three**</mark> things:

1. **Did it work?**
2. **What was created?** (the server <mark>may have added fields</mark> — ID, defaults, timestamps)
3. **Where does the new resource live?** (its URL, <mark>so the client can read/edit/delete it later</mark>)

A plain `Ok(newStudent)` <mark>gives you **only #2**</mark>. `CreatedAtRoute` <mark>gives you **all three**</mark>.

---

## What the HTTP spec says

RFC 7231 defines the convention:

> If a resource was created, the server <mark>SHOULD respond</mark> with `201 Created` and include a `Location` header <mark>with the URI of the new resource</mark>.

So the correct response to a successful POST-that-creates looks like:

```http
HTTP/1.1 201 Created
Location: http://localhost:5151/api/Students/5
Content-Type: application/json

{
  "id": 5,
  "name": "Ali",
  "age": 20,
  "grade": 85
}
```

| Piece             | Purpose                                          |
| ----------------- | ------------------------------------------------ |
| `201 Created`     | Confirms a new resource exists                   |
| `Location` header | Tells the client <mark>**where it lives**</mark> |
| Body              | Shows <mark>what was **actually**</mark> stored  |

---

## The concrete scenario

A mobile app lets users add students.

1. User fills in a form: name, age, grade.
2. App sends `POST /api/Students`.
3. Server creates the student, assigns ID 5.
4. Server returns `201 Created` with `Location: /api/Students/5` and the full student.
5. **Now the app knows:** the student exists, its ID is 5, its URL is `/api/Students/5`.
6. The app can immediately:
   - Show the detail page (`GET /api/Students/5`).
   - Navigate the user to edit it (`PUT /api/Students/5`).
   - Store the URL for later.

**Without the `Location` header**, the app has to guess:

- Assume the ID is in the body.
- Hard-code the URL pattern `/api/Students/{id}`.
- Or re-fetch the full list to find the new student.

<mark>**That's fragile**</mark>. If the API's URL structure ever changes, <mark>**the client breaks**</mark>.

**With `Location`**, the client <mark>is told **exactly where**</mark> the new resource is. No guessing. No hard-coded patterns.

---

## Why `CreatedAtRoute` and not `Created(uri, value)`

You could write:

```csharp
return Created($"api/Students/{newStudent.Id}", newStudent);
```

This works — but the URL template `"api/Students/{id}"` <mark>is **hard-coded in the controller**</mark>. Change the route later (`api/v2/Students/{id}`), and <mark>you have **to update** this line too</mark>.

`CreatedAtRoute("GetStudentById", new { id = ... }, newStudent)` avoids that. It says:

> "Look up the route named `GetStudentById`, fill in its parameters, and use that URL."

Now the URL comes from the actual route definition:

```csharp
[HttpGet("{id}", Name = "GetStudentById")]
```

Change the route in one place, and <mark>**every**</mark> `CreatedAtRoute` referencing that name <mark>**updates automatically**</mark>. **That's the entire point** — <mark>**<u>decoupling the response URL from the URL structure</u>**</mark>.

---

## Breaking down the call

```csharp
return CreatedAtRoute("GetStudentById", new { id = newStudent.Id }, newStudent);
```

| Argument | Value                        | Purpose                                                                |
| -------- | ---------------------------- | ---------------------------------------------------------------------- |
| 1        | `"GetStudentById"`           | Name of <mark>the target route</mark> **<u>to generate a URL for</u>** |
| 2        | `new { id = newStudent.Id }` | <mark>Route parameters</mark> to **<u>fill in</u>**                    |
| 3        | `newStudent`                 | <mark>The response body</mark>                                         |

### Argument 1 — the route name

`"GetStudentById"` matches the `Name = "GetStudentById"` set on:

```csharp
[HttpGet("{id}", Name = "GetStudentById")]
public ActionResult<Student> GetStudentById(int id) { ... }
```

The name <mark>**is a stable identifier**</mark>. <mark>**It doesn't change**</mark> if you change the route template or the action method's name. <mark>That's why **it's safe** to reference</mark>.

### Argument 2 — the anonymous object

`new { id = newStudent.Id }` is an anonymous object. <mark>It exists **only to pass** route parameters</mark>.

**Rule:** the property names <mark>**must match**</mark> the `{placeholder}` <mark>names **in the target route**</mark>.

The target route is `{id}`, so you supply `id`. If the target route were `{studentId}`, you'd write `new { studentId = newStudent.Id }`.

**Mismatch = runtime error:**

```
InvalidOperationException: No route matches the supplied values.
```

### Argument 3 — the body

`newStudent` is serialized to JSON and sent as the response body. It includes the server-assigned `Id`.

---

## The mechanism step by step

```js
POST /api/Students
Body: { name: "Ali", age: 20, grade: 85 }
    ↓
Server:
  1. Binds body to newStudent
  2. Validates → passes
  3. Assigns Id = 5
  4. Adds to list
  5. return CreatedAtRoute("GetStudentById", new { id = 5 }, newStudent)
    ↓
ASP.NET Core runtime:
  6. Finds the route named "GetStudentById"
  7. Gets its URL template: "api/Students/{id}"
  8. Substitutes {id} = 5 → "http://localhost:5151/api/Students/5"
  9. Sets response status to 201
 10. Adds Location header with that URL
 11. Serializes newStudent as body
    ↓
Client receives:
  HTTP/1.1 201 Created
  Location: http://localhost:5151/api/Students/5
  Body: { "id": 5, "name": "Ali", "age": 20, "grade": 85 }
```

---

## Comparison: the alternatives

| Method                                                    | Status | Location header | Body | URL source        |
| --------------------------------------------------------- | ------ | --------------- | ---- | ----------------- |
| `Ok(value)`                                               | 200    | ❌               | ✅    | N/A               |
| `Created(uri, value)`                                     | 201    | ✅               | ✅    | Hard-coded string |
| `CreatedAtAction(action, controller, routeValues, value)` | 201    | ✅               | ✅    | Action name       |
| **`CreatedAtRoute(name, routeValues, value)`**            | 201    | ✅               | ✅    | Route name        |

`CreatedAtRoute` <mark>**wins when**</mark> the target endpoint has a `Name`. <mark>**<u>It's the most decoupled option</u>**</mark> — reference **by name**, <mark>**not by hard-coded** URL or **action method name**</mark>.

---

## Why this matters at scale

In a real API, <mark>the client **doesn't know** your URL patterns</mark>. <mark>**It shouldn't have to**</mark>.

`Location` lets the server say "here's where the new resource lives" without the client needing any knowledge of the URL scheme. <mark>This is the simplest form of **HATEOAS**</mark> (Hypermedia as the Engine of Application State) — <mark>a **<u>REST principle</u>** where the server **<u>guides</u>** the client **<u>through links</u>**</mark>.

You don't need full HATEOAS, <mark>but `201 Created` + `Location` **is the entry point**</mark>.

---

## Common mistakes

| Mistake                                                                              | What happens                                                                         |
| ------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------ |
| Returning `Ok(newStudent)` <mark>on a POST-create</mark>                             | Client <mark>doesn't know **where**</mark> the new resource is, has to guess the URL |
| <mark>Hard-coding the URL</mark> in `Created(uri, value)`                            | <mark>Breaks</mark> if the route changes                                             |
| Property name in the anonymous object <mark>doesn't match</mark> the `{placeholder}` | `InvalidOperationException: No route matches the supplied values`                    |
| Route name typo                                                                      | Same exception — verify the spelling matches `Name = "..."`                          |
| Using `CreatedAtAction` when the action name might change                            | Use `CreatedAtRoute` with a name instead; the name is stable                         |

---

## Summary

| Aspect                                  | Answer                                                                             |
| --------------------------------------- | ---------------------------------------------------------------------------------- |
| **What it does**                        | Returns `201 Created` with a `Location` header and the created resource as body    |
| **Why 201 and not 200**                 | HTTP convention: 200 means "here's the result," 201 means "something was created"  |
| **Why include Location**                | Tells the client where the new resource lives — no guessing, no hard-coded URLs    |
| **Why `CreatedAtRoute` over `Created`** | Uses the route name instead of a hard-coded URL — decoupled from the URL structure |
| **The anonymous object**                | Supplies route parameters; property names must match `{placeholder}` names         |
| **What the client gets**                | Status (success), resource body (what was stored), URL (where it lives)            |

---

Save as `CreatedAtRoute and 201 Created.md` under `Docs/` in Level 25.
