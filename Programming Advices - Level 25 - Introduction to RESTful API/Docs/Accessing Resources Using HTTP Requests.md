# Accessing Resources Using HTTP Requests

To access a resource, the client sends an HTTP request to the server. A request has **four principal parts**.

---

## 1. HTTP Method

<mark>The method describes **what should happen** to the specified resource</mark>. The four fundamental HTTP methods are known as **verbs**:

| Method   | Purpose                               | CRUD         |
| -------- | ------------------------------------- | ------------ |
| `POST`   | Create a new resource                 | **C**reate   |
| `GET`    | Retrieve an existing resource         | **R**etrieve |
| `PUT`    | Update or change an existing resource | **U**pdate   |
| `DELETE` | Delete a resource                     | **D**elete   |

**<u>These four verbs map directly to the classic CRUD operations.</u>**

---

## 2. Endpoint

The endpoint shows **where the resource is located**. It typically includes a <mark>**Uniform Resource Identifier (URI)**</mark>. <mark>If the resource is accessed over the internet</mark>, <mark><u>**the URI is a URL**</u></mark> that gives the resource a web address.

Example: `https://api.example.com/students/42`

---

## 3. Header

A header <mark>carries the **details needed to execute the call and handle the response**</mark>. It can include:

- Authentication data (API keys, tokens)
- Encryption keys
- Server location or access information
- The desired data format for the response (e.g., `Accept: application/json`)

<mark>Headers are metadata</mark> — they <mark><u>**describe *how* the request should be handled**</u></mark>, <mark><u>**not *what* the request contains**</u></mark>.

---

## 4. Body

The body <mark>carries the **data** between client and server</mark>. Its presence depends on the method.

### Sending data to the server

**`POST` (create):** When creating a new resource, the client includes the necessary data in the request body. Adding a new user? The user's name, email, and password go in the body.

**`PUT` (update):** When updating an existing resource, the client sends the updated data in the body. Changing a user's email? The new email goes in the body.

### Receiving data from the server

**Response body:** When the server responds, it often includes the requested data in the body. After a successful `GET` to fetch user details, the server returns the user's information in the response body.

**Note:** `GET` and `DELETE` requests typically have no body — there's no data to send. `POST` and `PUT` almost always do.

---

## How the server responds

The server hosting the API processes the call and forms a response:

- Sends a **machine-readable representation** of the requested data (JSON, XML, or plain text).
- Includes **metadata** needed to interpret the response — content type, timestamps, error codes.
- Adds **instructions** for the client when needed.

In short, <mark>**calls and responses are self-descriptive**</mark>. Each one includes the information needed to process and interpret it — **<u>you don't need external context to understand what came back</u>**.

---

## 📌 Summary

| Part            | Purpose                                                        |
| --------------- | -------------------------------------------------------------- |
| **HTTP method** | What to do (`GET`, `POST`, `PUT`, `DELETE`)                    |
| **Endpoint**    | Where the resource lives (URL)                                 |
| **Header**      | How to handle the call (auth, format, metadata)                |
| **Body**        | The data itself (for `POST` and `PUT`; sometimes in responses) |

Every HTTP interaction is made of these four parts. The client sends them; the server reads them, processes the request, and returns a self-descriptive response.

---
