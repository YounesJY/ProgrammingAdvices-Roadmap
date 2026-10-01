# HERE's why

**"Architectural style"** means: a **set of rules and constraints** for how to structure a system — not a specific technology, framework, or library. It's a pattern for organizing communication, not a tool you install.

<mark>REST <u>isn't a protocol</u> (like HTTP) and it<u> isn't a standard</u> (like SQL)</mark>. <mark><u>It's a **design approach**</u></mark>. You can build a RESTful API in any language, on any framework. <mark>The "style" is the set of constraints <u>**you agree to follow**</u>.</mark>

---

## REST's constraints — what makes an API "RESTful"

<mark>REST is defined by **six constraints**</mark>. **<u>If you follow all six, your API is RESTful</u>**. <mark><u>If you skip some, it isn't (or it's "REST-like")</u></mark>.

| #   | Constraint                      | What it means                                                                                                                                                 |
| --- | ------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1   | **Client–Server**               | <mark>Separate the UI (client) from the data/logic (server)</mark>. <u>**They evolve independently**</u>.                                                     |
| 2   | **Stateless**                   | <mark><u>**Each request contains everything needed**</u></mark>. The server <mark><u>**doesn't remember**</u></mark> previous requests.                       |
| 3   | **Cacheable**                   | Responses must declare whether they can be cached. Clients can reuse responses.                                                                               |
| 4   | **Uniform Interface**           | A consistent way to interact — <mark>**URLs identify resources**</mark>, <mark>**HTTP methods define actions**</mark>, <mark>**responses carry data**</mark>. |
| 5   | **Layered System**              | A client <mark><u>**can't tell if it's talking to the real server or an intermediary**</u></mark> (proxy, load balancer, gateway).                            |
| 6   | **Code on Demand** *(optional)* | Servers can send executable code to clients. <mark>**Rarely used**</mark>.                                                                                    |

Those are the rules. Following them produces a system that behaves in a predictable way. That's what "architectural style" means — a named, agreed-upon set of rules.

---

## <mark><u>**Why it's called a "style" and not a "standard"**</u></mark>

| Term                    | Meaning                                                                                                                     | Example                                           |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------- |
| **Protocol**            | A concrete, wire-level spec. Implementations must match byte-for-byte.                                                      | HTTP, TCP                                         |
| **Standard**            | **<u>A published, formal specification</u>**. <mark><u>**Usually versioned**</u></mark>.                                    | SQL (**<u>ANSI</u>**), JSON (<u>**RFC 8259**</u>) |
| **Architectural style** | <mark><u>**A set of design constraints**</u></mark>. Not enforced by tools. <mark><u>**Followed by convention**</u></mark>. | REST, MVC, microservices                          |
| **Framework**           | Code you build on top of.                                                                                                   | ASP.NET Core, Express, Django                     |

REST is **not enforced by the compiler, the runtime, or the network**. If you write an API that violates one of REST's constraints, nothing breaks automatically. It just stops being "RESTful" in the strict sense.

> <mark>**That's the point of calling it a style: it's a **design philosophy**, not a technical requirem**ent.</mark>

---

## An analogy

Think of building a house.

- **Protocol** = the building code (mandatory — inspectors check it).
- **Standard** = the dimensions of a brick (fixed — everyone uses the same size).
- **Architectural style** = "modern" or "traditional" or "industrial." The house still stands if you mix elements. It's a set of conventions about how it's laid out, not a law.

REST is "modern minimalist" for APIs. You can build a valid API that ignores REST. It just won't be *RESTful* — it'll be something else (RPC, SOAP, GraphQL, custom).

---

## Concrete example

Two APIs that do the same thing:

**RESTful:**

```
GET    /students/42
POST   /students
PUT    /students/42
DELETE /students/42
```

- URLs identify **resources** (`/students/42`).
- HTTP methods define **actions**.
- Stateless — each request is self-contained.
- Uses standard status codes (`200`, `201`, `404`, etc.).

**Not RESTful (RPC-style):**

```
GET /getStudentById?id=42
GET /createNewStudent?name=Ali&age=20
GET /updateStudent?id=42&name=Ali
GET /deleteStudent?id=42
```

- URLs describe **actions**, not resources.
- Everything is a `GET` — HTTP verbs are ignored.
- Not uniform. Each endpoint has its own query string shape.

Both work. The second one is a **RPC-style API**. It's not RESTful because it violates the uniform interface constraint — the API doesn't treat everything as a resource you act on with standard verbs.

---

## So, in one sentence

> **"RESTful API is an architectural style"** means: <mark>it's a design approach — a set of six constraints for how a networked system should be structured</mark> — not a technology, protocol, or standard. <mark>You follow the constraints by choice</mark>, and if you follow all of them, your API is RESTful.

The word "style" is doing a lot of work in that sentence. It signals: *this is not a spec you compile against; it's a set of principles you design within.*


