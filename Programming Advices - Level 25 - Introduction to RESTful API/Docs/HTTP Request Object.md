# HTTP Request Object

The **request object** in HTTP <mark>represents the data sent by the client to the server</mark>. It <mark><u>**carries everything the server needs to understand what the client is asking for and how to process it**</u></mark>.

---

## 1. Request Line

<mark><u>**The first line of every HTTP request**</u></mark>. It contains three parts:

| Part             | Meaning                                     |
| ---------------- | ------------------------------------------- |
| **Method**       | The action (`GET`, `POST`, `PUT`, `DELETE`) |
| **URL**          | The resource being fetched or manipulated   |
| **HTTP Version** | The protocol version (`HTTP/1.1`, `HTTP/2`) |

**Example:**

```http
GET /index.html HTTP/1.1
```

---

## 2. Request Headers

Headers are **key-value pairs** carrying metadata about the request — how to authenticate, what format to respond in, what the client can handle.

### Common Headers

| Header            | Purpose                                                |
| ----------------- | ------------------------------------------------------ |
| `Host`            | <mark><u>**The server's domain and port**</u></mark>   |
| `User-Agent`      | Info about the client software                         |
| `Accept`          | Media types <mark>the client can process</mark>        |
| `Content-Type`    | Media type <mark>of the request body</mark>            |
| `Authorization`   | <mark>Credentials for authenticating the clien</mark>t |
| `Content-Length`  | Length of the body in bytes                            |
| `Cookie`          | <mark>Cookies stored by the client</mark>              |
| `Accept-Encoding` | Encodings the client can handle (`gzip`, `deflate`)    |
| `Cache-Control`   | <mark>Caching directives</mark>                        |

> `Content-Length` <mark>declares how many bytes to expect</mark>. It's a size/truncation check — <mark>not an integrity check</mark>. Integrity comes from TLS, hashes, or signatures, not from the length header.

### Examples

```http
Content-Type: application/json
Accept: application/json
Authorization: Bearer <token>
Content-Length: 123
User-Agent: Mozilla/5.0
Host: example.com
Accept-Encoding: gzip, deflate
Cache-Control: no-cache
Cookie: sessionId=abc123
```

### A Realistic Header Block

```http
Host: www.example.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
```

---

## 3. Body (Optional)

<mark>The body carries the **data being sent to the server**</mark>. <mark><u>**Used with methods that submit data**</u></mark> — `POST`, `PUT`, `PATCH`. Not used with `GET` or `DELETE`.

The format varies: JSON, XML, form data. JSON dominates modern APIs.

**Example (`POST` request body):**

```json
{
  "username": "exampleUser",
  "password": "examplePass"
}
```

---

## Complete Request Examples

### `GET` Request

```http
GET /index.html HTTP/1.1
Host: www.example.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
```

No body — `GET` retrieves, doesn't send.

### `POST` Request

```http
POST /login HTTP/1.1
Host: www.example.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)
Content-Type: application/json
Content-Length: 48

{
  "username": "exampleUser",
  "password": "examplePass"
}
```

<mark>Body present, `Content-Type` **<u>declares its format</u>**, `Content-Length` **<u>declares its size</u>**.</mark>

---

## How the Request Object Is Used

### Step 1 — Client Constructs the Request

    When a user interacts with a web app (clicks a link, submits a form), <mark>the client builds an HTTP request</mark> **<u>based on that action and sends it to the server</u>**.

### Step 2 — Server Receives and Processes

    The server <mark>reads the request line, headers, and body (if present)</mark>, **<u>then determines how to respond</u>**. It may query a database, run business logic, or retrieve a file.

### Step 3 — Server Sends a Response

    <mark><u>The server constructs an HTTP response and sends it back</u></mark> — status code, headers, and optionally a body with the requested data or an error message.

---

## 📌 Summary

| Component        | Role                                                     |
| ---------------- | -------------------------------------------------------- |
| **Request line** | What (`method`), where (`URL`), and how (`HTTP version`) |
| **Headers**      | Metadata — auth, format, client info, caching            |
| **Body**         | The data itself (only for `POST`, `PUT`, `PATCH`)        |

<mark>The HTTP request object is the foundation of client–server communication</mark>. **<u>It gives the client a standardized way to ask for resources and actions</u>**, and gives the server everything it needs to fulfill the request.

---
