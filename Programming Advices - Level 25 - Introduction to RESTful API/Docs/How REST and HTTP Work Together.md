# How REST and HTTP Work Together

**HTTP** (Hypertext Transfer Protocol) <mark>is the foundational protocol used by **REST** APIs</mark>. <mark><u>**REST is an architectural style**</u></mark> for networked applications that relies on a stateless, client–server protocol — typically HTTP.

---

## 1. Basic Concepts

### Client–Server Architecture

In REST, <mark>the client sends requests and the server responds</mark>.

- **Client** — <u>responsible for the user interface and user experience</u>
- **Server** — handles data storage and processing

<mark><u>**Each side has its role**</u></mark>. <mark>They communicate over HTTP <u>**but evolve independently**</u></mark>.

### Statelessness

<mark>Every request</mark> from the client <mark><u>must contain **all the information needed to process it**</u></mark>. The server <u>**doesn't store any state about the client session**</u>.

Consequence: <mark>each request is **independent**</mark>. <mark><u>**The server can't "remember"**</u></mark> that a previous request came from the same client — <mark><u>**that context must be re-sent every time**</u></mark>.

### Resources and URIs

In REST, <mark>resources (data or services) are identified by **URIs**</mark> (Uniform Resource Identifiers). Each resource can be retrieved or manipulated using its URI.

Example: `/students/42` is the URI for the student with ID 42. The URI *is* the address of the resource.

### HTTP Methods

REST APIs use standard HTTP methods to act on resources:

| Method   | Action                      |
| -------- | --------------------------- |
| `GET`    | Retrieve a resource         |
| `POST`   | Create a new resource       |
| `PUT`    | Update an existing resource |
| `DELETE` | Delete a resource           |

### HTTP Status Codes

The response carries <mark>a status code indicating the result</mark>:

| Code                        | Meaning                                 |
| --------------------------- | --------------------------------------- |
| `200 OK`                    | Request succeeded                       |
| `201 Created`               | Resource successfully created           |
| `204 No Content`            | Request succeeded, no content to return |
| `400 Bad Request`           | Request was invalid or cannot be served |
| `404 Not Found`             | Requested resource doesn't exist        |
| `500 Internal Server Error` | Server-side error occurred              |

---

## 2. How It Works

The interaction follows three steps.

### Step 1 — Client Request

The client sends an HTTP request to the server. <mark><u>**The request contains**</u></mark>:

- HTTP method (`GET`, `POST`, `PUT`, `DELETE`)
- Endpoint (URI)
- Headers (auth, content type, etc.)
- Body (for `POST` / `PUT`)

### Step 2 — Server Processing

The server receives the request, processes it, and <mark>performs the necessary operations on the resource</mark>. It may:

- Query a database
- Run business logic
- Trigger other actions required to fulfill the request

### Step 3 — Server Response

After processing, <mark><u>**the server sends an HTTP response back to the client**</u></mark>:

- **<u>Status code (`200`, `404`, `500`, …)</u>**
- Headers (content type, cache directives, etc.)
- Body (the requested data, if any)

The client then interprets the response and acts accordingly.

---

## 📌 Summary

| Layer    | What it provides                                               |
| -------- | -------------------------------------------------------------- |
| **HTTP** | The protocol — the wire format for requests and responses      |
| **REST** | The design style — how to structure resources and interactions |

REST gives the API its **shape** (resources, statelessness, uniform methods). HTTP gives it the **transport** (how messages actually travel between client and server). They work together: REST defines the rules, HTTP executes them.

---


