# RESTful APIs vs Other API Types (SOAP, GraphQL, RPC)

Four main approaches to building APIs, each with a different philosophy. **Choosing between them <mark>depends on what the application needs</mark>.**

---

## 1. RESTful APIs (Representational State Transfer)

**Overview:**

- <mark>An **architectural style**</mark> for networked applications
- Uses standard HTTP methods (`GET`, `POST`, `PUT`, `DELETE`)
- Resources identified by **URIs**

**Advantages:**

- <mark>**Scalability**</mark> — stateless nature allows better scaling
- <mark>**Flexibility**</mark> — multiple data formats (JSON, XML)
- <mark>**Caching**</mark> — HTTP caching mechanisms improve performance
- <mark>**Easy to use**</mark> — standard HTTP methods, testable with Postman
- <mark>**Stateless**</mark> — server handles more requests because it doesn't track sessions

**Disadvantages:**

- <mark>**Stateless**</mark> — **<u>every request must carry all context</u>**
- <mark>**Overhead**</mark> — **<u>may require multiple requests for related resources</u>** (e.g., user details and user posts fetched separately)

---

## 2. SOAP (Simple Object Access Protocol)

**Overview:**

- <mark>A **protocol** for exchanging structured information in web services</mark>
- <mark>Uses **XML**</mark> as the message format
- **<u>Runs over HTTP, SMTP, or other application-layer protocols</u>**

**Advantages:**

- **Standardized** — <mark>strict standards ensure reliability and security</mark>
- **Extensibility** — <mark>WS-Security and similar specs **<u>provide enterprise-level features</u>**</mark>
- **Stateful operations** — <mark>**<u>can maintain context across multiple calls</u>**</mark>

**Disadvantages:**

- <mark>**Complexity**</mark> — harder to set up and understand than REST
- <mark>**Overhead**</mark> — XML-based, larger messages, slower processing

---

## 3. GraphQL

**Overview:**

- <mark>A **query language** for APIs</mark>, plus a runtime for executing queries
- <mark><u>**Clients request exactly the data they need**</u></mark> — <mark>**<u>nothing more, nothing less</u>**</mark>

**Advantages:**

- <mark>**Efficiency**</mark> — **<u>one request can fetch multiple related resources</u>**
- <mark>**Flexibility**</mark> — clients specify exactly what data they want
- **Strongly typed** — schema and types are defined, providing clear documentation and validation

**Disadvantages:**

- <mark>**Complexity**</mark> — requires understanding its syntax and structure
- <mark>**Caching challenges**</mark> — **<u>harder to cache at the HTTP layer because queries vary</u>**
  - > **The result:** <mark>you lose most of the free caching HTTP gives you with REST</mark>. To cache GraphQL, you typically need **application-level caching** — a resolver-level cache, a persisted-query cache, or a CDN that understands the GraphQL schema. None of that is HTTP-native. That's the "challenge."
- <mark>**Over- or under-fetching**</mark> — still possible **<u>if queries aren't carefully constructed</u>**

---

## 4. RPC (Remote Procedure Call)

**Overview:**

- <mark>A **protocol** where one program <u>requests a service</u> from another program on the network</mark>
- <mark><u>**Designed to look like a local function call, even though it crosses address spaces**</u></mark>

**Advantages:**

- <mark>**Simplicity**</mark> — **<u>straightforward method invocation across the network</u>**
- <mark>**Performance**</mark> — **<u>often faster than REST for specific tasks due to lower protocol overhead</u>**

**Disadvantages:**

- <mark><u>**Tight coupling**</u></mark> — the client is **<u>tightly bound to the server's procedure names and signatures</u>**
- <mark><u>**Scalability issues**</u></mark> — **<u>stateful operations and tight coupling</u>** make scaling harder
- <mark>**Limited flexibility**</mark> — less adaptable to different data formats and request styles

---

## Summary Table

| Aspect          | REST                                             | SOAP                                                           | GraphQL                                       | RPC                                          |
| --------------- | ------------------------------------------------ | -------------------------------------------------------------- | --------------------------------------------- | -------------------------------------------- |
| **Style**       | <mark>**Architectural style**</mark>             | <mark>Protocol</mark>                                          | <mark>**Query language**</mark>               | <mark>Protocol</mark>                        |
| **Data format** | JSON, XML, plain text                            | <mark>**XML only**</mark>                                      | JSON (usually)                                | Varies                                       |
| **Transport**   | HTTP                                             | HTTP, SMTP, others                                             | HTTP                                          | TCP, HTTP                                    |
| **Caching**     | <mark><u>**Built-in via HTTP**</u></mark>        | <u>**Limited**</u>                                             | <mark><u>**Difficult**</u></mark>             | **<u>Limited</u>**                           |
| **Coupling**    | Loose                                            | Loose                                                          | Loose                                         | <mark><u>**Tight**</u></mark>                |
| **Best for**    | <mark><u>**General-purpose web APIs**</u></mark> | <mark><u>**Enterprise**</u></mark>, <mark>high-security</mark> | <mark><u>**Complex data fetching**</u></mark> | <mark><u>**Performance-critical**</u></mark> |

---

## 📌 Conclusion

Choosing the right API type depends on the use case:

| API type        | Best suited for                                                         |
| --------------- | ----------------------------------------------------------------------- |
| **RESTful API** | Web services needing flexibility, scalability, and simplicity           |
| **SOAP**        | Enterprise applications requiring high security and transaction support |
| **GraphQL**     | Applications where clients fetch complex data structures efficiently    |
| **RPC**         | Performance-critical applications that benefit from direct method calls |

<mark>**<u>None is universally better</u>**</mark> — <mark><u>**each solves a different problem**</u></mark>. REST is the default for general web APIs because it hits the sweet spot between simplicity, flexibility, and scale. <mark>**<u>The others exist because not every problem is that sweet spot</u>**</mark>.

---
