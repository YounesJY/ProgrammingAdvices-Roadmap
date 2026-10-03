# Main Elements of a RESTful API

<mark>A REST API fundamentally relies on **three major elements**</mark>.

---

## 1. Client

The **client** is <mark><u>**the software code or application that requests a resource from a server**</u></mark>.

<mark>**It initiates every interaction**</mark>. <mark>It **never holds the data itself**</mark> — <mark>it asks the server for it</mark>. Examples: a web browser, a mobile app, a desktop application, <mark><u>**another backend service**</u></mark>.

---

## 2. Server

The **server** is <mark><u>**the software code or application that controls the resource and responds to client requests**</u></mark>.

<mark>It **<u>owns the data</u>**</mark>, <mark>enforces the rules</mark>, and <mark>decides what to send back</mark>. <u>**The client doesn't dictate what data exists — the server does**</u>.

---

## 3. Resource

<mark>The **resource** is any data or content the server controls and makes available in response to client requests</mark> — text, video, images, structured records, anything.

In REST, <mark><u>**everything of value is treated as a resource**</u></mark>, identified by a URL (e.g., `/students/42`), and acted on using standard HTTP methods (`GET`, `POST`, `PUT`, `DELETE`).

---

## 📌 Summary

| Element      | Role                                |
| ------------ | ----------------------------------- |
| **Client**   | Requests resources                  |
| **Server**   | Controls and serves resources       |
| **Resource** | The data or content being exchanged |

These three form the foundation of every REST interaction. Everything else — methods, status codes, headers, auth — exists to support the conversation between client and server about resources.

---
