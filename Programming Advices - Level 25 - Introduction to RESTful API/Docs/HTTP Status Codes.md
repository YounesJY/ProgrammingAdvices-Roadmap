# HTTP Status Codes

HTTP status codes in RESTful APIs indicate the **result of a client's request to a server**. They tell the client whether the request was successful, encountered an error, or requires additional action.

---

## Quick Reference

| Code                        | Meaning                                   |
| --------------------------- | ----------------------------------------- |
| `200 OK`                    | Request succeeded                         |
| `201 Created`               | Resource created (typically after `POST`) |
| `204 No Content`            | Request succeeded, nothing to return      |
| `400 Bad Request`           | Request was malformed or invalid          |
| `401 Unauthorized`          | Authentication required                   |
| `404 Not Found`             | Resource doesn't exist                    |
| `500 Internal Server Error` | Server hit an unexpected condition        |

---

## 1. 2xx — Success

<mark><u>**The client's request was successfully received, understood, and accepted.**</u></mark>

### `200 OK`

The request succeeded. The response body depends on the method used.

**Example:** Successfully retrieved a resource or list of resources via `GET`.

### `201 Created`

The request was fulfilled and a new resource was created. <mark>Typically returned after a `POST`</mark>.

**Example:** Successfully created a new user in the database.

### `204 No Content`

The server processed the request but isn't returning any content. <mark>Common for `DELETE`</mark>.

**Example:** Successfully deleted a resource.

---

## 2. 3xx — Redirection

<mark><u>**Further action is needed to complete the request.**</u></mark>

### `301 Moved Permanently`

The resource has been permanently moved to a new URI.

**Example:** The endpoint for a resource has changed permanently.

### `302 Found`

The resource is temporarily under a different URI. The client should keep using the original URI for future requests.

**Example:** Temporary redirection to another endpoint.

---

## 3. 4xx — Client Errors

The request contains bad syntax or cannot be fulfilled by the server.

### `400 Bad Request`

The server can't process the request due to a client error — malformed syntax, invalid framing, deceptive routing.

**Example:** Missing required parameters in the request body.

### `401 Unauthorized`

Authentication is required and has either failed or not been provided.

**Example:** <mark>Accessing a protected resource without credentials</mark>.

### `403 Forbidden`

The server understood the request but refuses to authorize it.

**Example:** <mark>Trying to access a resource **<u>the user doesn't have permission for</u>**</mark>.

### `404 Not Found`

The requested resource doesn't exist on the server.

**Example:** <mark>Trying to retrieve a resource that isn't there.</mark>

### `405 Method Not Allowed`

The HTTP method in the request isn't allowed for that URI.

**Example:** <mark>Using `POST` on an endpoint that **<u>only accepts</u>** `GET`.</mark>

### `409 Conflict`

The request conflicts with the current state of the resource — e.g., an edit conflict between simultaneous updates.

**Example:** Trying to create a resource that already exists.

---

## 4. 5xx — Server Errors

The server failed to fulfill a valid request.

### `500 Internal Server Error`

The server hit an unexpected condition that prevented it from fulfilling the request.

**Example:** <mark><u>**An unhandled exception on the server side**</u></mark>.

### `501 Not Implemented`

The server doesn't support the functionality required.

**Example:** <mark>The requested HTTP method isn't supported</mark>.

### `502 Bad Gateway`

The server, acting as a gateway or proxy, <mark><u>**received an invalid response from an upstream server**</u></mark>.

**Example:** A proxy server received garbage from a backend.

### `503 Service Unavailable`

The server is temporarily unavailable — overloaded or down for maintenance.

**Example:** <mark><u>**The server can't handle the request right now**</u></mark>.

---

## 📌 Why This Matters

Understanding and using the right status codes is what makes an API **robust** and **user-friendly**. The client shouldn't have to read error messages to know what happened — the status code alone should tell it:

- Did the request succeed? (2xx)
- Do I need to go somewhere else? (3xx)
- Did I do something wrong? (4xx)
- Is the server having problems? (5xx)

> <mark>That's the whole point of **<u>a well-designed status code system</u>**: **clear communication between client and server**, without ambiguity.</mark>

---
