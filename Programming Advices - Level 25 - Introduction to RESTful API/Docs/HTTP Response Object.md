# HTTP Response Object

The **response object** in HTTP <mark>is what the server sends back to the client after processing a request</mark>. It <mark><u>**tells the client what happened**</u></mark>, **<u>carries any requested data, and includes metadata about how to interpret it</u>**.

---

## 1. Status Line

<mark><u>**The first line of every HTTP response**</u></mark>. Three parts:

| Part              | Meaning                                                                 |
| ----------------- | ----------------------------------------------------------------------- |
| **HTTP Version**  | <mark>Protocol</mark> version (`HTTP/1.1`, `HTTP/2`)                    |
| **Status Code**   | Three-digit <mark>result code</mark> (`200`, `404`, `500`)              |
| **Reason Phrase** | <mark>Human-readable</mark> <mark><u>description</u></mark> of the code |

**Example:**

```http
HTTP/1.1 200 OK
```

---

## 2. Response Headers

<mark>Headers are **key-value pairs** carrying metadata about the response</mark> — content type, caching rules, cookies, server info.

### Common Headers

| Header           | Purpose                                                  |
| ---------------- | -------------------------------------------------------- |
| `Content-Type`   | Media type of the body (`application/json`, `text/html`) |
| `Content-Length` | Body size in bytes                                       |
| `Set-Cookie`     | <mark><u>**Sets a cookie in the client**</u></mark>      |
| `Cache-Control`  | <mark><u>**Caching directives**</u></mark>               |
| `Date`           | When the message was sent                                |
| `Server`         | <mark><u>**Info about the server software**</u></mark>   |

### Detailed Examples

```http
Content-Type: application/json
Content-Length: 456
Date: Tue, 15 Nov 2023 08:12:31 GMT
Server: Apache/2.4.41 (Ubuntu)
Set-Cookie: sessionId=xyz789; Path=/; HttpOnly
Cache-Control: no-store
ETag: "abcd1234"
Location: https://example.com/new-resource
Retry-After: 120
Allow: GET, POST, PUT, DELETE
```

| Header        | Purpose                                                                             |
| ------------- | ----------------------------------------------------------------------------------- |
| `ETag`        | Unique identifier for a resource version — <mark><u>**used for caching**</u></mark> |
| `Location`    | <mark><u>**Redirection target**</u></mark>, or the URL of a newly created resource  |
| `Retry-After` | **<u>How long the client should wait before retrying</u>**                          |
| `Allow`       | **<u>HTTP methods supported by the resource</u>**                                   |

### A Realistic Header Block

```http
Content-Type: text/html; charset=UTF-8
Content-Length: 1234
Set-Cookie: sessionId=abc123; Path=/; HttpOnly
Cache-Control: no-cache
Date: Tue, 15 Nov 2024 08:12:31 GMT
```

---

## 3. Body (Optional)

<mark>The body carries the **requested data** or an **error message**</mark>. **<u>Its presence depends on the request type and the status code</u>**.

**Example (HTML body):**

```html
<html>
<body>
<h1>Hello, World!</h1>
</body>
</html>
```

---

## Complete Response Examples

### Success — `200 OK` with JSON

```http
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 123
Date: Tue, 15 Nov 2024 08:12:31 GMT

{
  "status": "success",
  "data": {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "roles": ["user", "admin"]
  },
  "message": "User data retrieved successfully"
}
```

**Breakdown:**

- **Status line:** `HTTP/1.1 200 OK` — protocol, code, reason
- **Headers:** `Content-Type` declares JSON; `Content-Length` gives size; `Date` gives timestamp
- **Body:** a JSON object with:
  - `status` — the result of the request (`"success"`)
  - `data` — the actual payload (`id`, `name`, `email`, `roles`)
  - `message` — a human-readable description

### Success — `200 OK` with HTML

```http
HTTP/1.1 200 OK
Content-Type: text/html; charset=UTF-8
Content-Length: 1234
Set-Cookie: sessionId=abc123; Path=/; HttpOnly
Cache-Control: no-cache
Date: Tue, 15 Nov 2024 08:12:31 GMT

<html>
<body>
<h1>Hello, World!</h1>
</body>
</html>
```

### Error — `404 Not Found`

```http
HTTP/1.1 404 Not Found
Content-Type: text/html; charset=UTF-8
Content-Length: 87
Date: Tue, 15 Nov 2024 08:12:31 GMT

<html>
<body>
<h1>404 Not Found</h1>
<p>The requested resource could not be found on this server.</p>
</body>
</html>
```

> <mark>Note: **even error responses** have a body. **<u>It's the server's chance to explain what went wrong</u>**.</mark>

---

## How the Response Object Is Used

### Step 1 — Server Constructs the Response

After processing the request — querying a database, running logic, reading a file — <mark>the server builds an HTTP response</mark>: status line, headers, and optionally a body.

### Step 2 — Client Receives and Interprets

The client (browser, mobile app, another service) reads:

- The **status line** <mark>to know if the request succeeded</mark>
- The **headers** for content type, caching, cookies
- The **body** if present

### Step 3 — Client Renders the Content

Based on the response, the client might:

- Render HTML
- Update the UI with JSON data
- Display an error message
- Redirect the user

---

## 📌 Summary

| Component       | Role                                                   |
| --------------- | ------------------------------------------------------ |
| **Status line** | What happened (`200 OK`, `404 Not Found`)              |
| **Headers**     | Metadata — content type, caching, cookies, server info |
| **Body**        | The requested data or an error message                 |

<mark><u>**The HTTP response object is the server's standardized way of telling the client what happened and delivering the result**</u></mark>. <mark><u>**Every HTTP interaction ends with one**</u></mark> — **<u>*it's the other half of the request/response cycle*</u>**.

---
