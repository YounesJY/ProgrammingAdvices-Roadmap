# 📚 Programming Advices – My Learning Journey

![Roadmap](ProgrammingAdvices%20-%20RoadMap.png)

> This repository documents my progress through the **Programming Advices** curriculum designed by **Dr. Mohammed Abu-Hadhoud**.
> The roadmap builds strong foundations in programming, algorithms, data structures, databases, and C#/.NET development.

---

## 🗺️ Curriculum Structure

| #   | Course Name                                        | Status         |
| --- | -------------------------------------------------- | -------------- |
| 01  | Foundations Level 1                                | ✅ Completed    |
| 02  | Algorithms & Problem-Solving Level 1               | ✅ Completed    |
| 03  | Introduction to Programming with C++ – Level 1     | ✅ Completed    |
| 04  | Algorithms & Problem-Solving – Level 1 (Solutions) | ✅ Completed    |
| 05  | Algorithms & Problem-Solving – Level 2             | ✅ Completed    |
| 06  | Introduction to Programming Using C++ Level 2      | ✅ Completed    |
| 07  | Algorithms & Problem Solving Level 3               | ✅ Completed    |
| 08  | Algorithms & Problem Solving Level 4               | ✅ Completed    |
| 09  | Foundations Level 2                                | ✅ Completed    |
| 10  | OOP as it Should Be (Concepts)                     | ✅ Completed    |
| 11  | OOP as it Should Be (Applications)                 | ✅ Completed    |
| 12  | Data Structures – Level 1                          | ✅ Completed    |
| 13  | Algorithms & Problem Solving Level 5               | ✅ Completed    |
| 14  | C# – Level 1                                       | ✅ Completed    |
| 15  | Database Level 1 – SQL (Concepts and Practice)     | ✅ Completed    |
| 16  | OOP As It Should Be In C#                          | ✅ Completed    |
| 17  | Database – SQL (Projects & Practice)               | ✅ Completed    |
| 18  | C# & Database Connectivity (ADO.NET)               | ✅ Completed    |
| 19  | Full Real Project – DVLD                           | ✅ Completed    |
| 20  | C# Programming Level 2                             | ✅ Completed    |
| 21  | Database Level 2 – Concepts & T-SQL                | ✅ Completed    |
| 22  | Data Structures Level 2 in C#                      | 📅 Planned      |
| 23  | Algorithms Level 6                                 | 📅 Planned      |
| 24  | Windows Services                                   | 📅 Planned      |
| 25  | Introduction to RESTful APIs                       | ✅ Completed    |
| 26  | API Security (JWT, OAuth, Identity)                | 🔄 In Progress |

---

## 🏆 Course Highlights

### Course 19 — DVLD (Driving License Management System)

The most significant project so far.

| Metric                  | Value                                 |
| ----------------------- | ------------------------------------- |
| **Forms**               | 50+                                   |
| **User Controls**       | 25+                                   |
| **Business Classes**    | 15+                                   |
| **Data Access Classes** | 15+                                   |
| **SQL Tables**          | 30+                                   |
| **Architecture**        | 3-Tier (UI → Business → Data Access)  |

**Key learnings:**

- **Event Exposure Pattern** — works well for one level. Chained through multiple layers, it becomes an anti-pattern.
- **Lazy Loading** — load related objects only when needed, through property getters.
- **Transactions** — understood why they matter (data inconsistency hurts).
- **Repository Placement** — the reason a query exists determines its location, not the tables it touches.
- **Single Responsibility** — keeps repositories clean and focused.

### Course 20 — C# Programming Level 2

Deep dive into the features that separate daily coding from production engineering.

- Delegates, events, custom `EventArgs`, event exposure
- Multithreading, ThreadPool, Tasks, TPL, `async/await`
- Race conditions, locks, mutexes, semaphores
- `Parallel.For` / `ForEach` / `Invoke`
- Hashing (SHA-256, PBKDF2), symmetric/asymmetric encryption
- Reflection, custom attributes
- Serialization (JSON, XML, binary)
- Windows Registry, App.Config, Event Log
- Operator overloading, `StringBuilder`, immutability

### Course 21 — Database Level 2 (Concepts & T-SQL)

Moved from querying databases to programming inside them.

- T-SQL control flow (variables, `IF/ELSE`, `WHILE`, `CASE`)
- Error handling with `TRY/CATCH`, `THROW`, error functions
- Transactions, isolation levels, locking
- Cursors (and when *not* to use them)
- CTEs, including recursive CTEs for hierarchical data
- Triggers (`AFTER`, `INSTEAD OF`, `inserted` / `deleted` pseudo-tables)
- Stored procedures, scalar UDFs, inline TVFs, multi-statement TVFs
- Window functions (`ROW_NUMBER`, `RANK`, `DENSE_RANK`, `LAG`, `LEAD`)
- Temp tables vs table variables
- Query optimization, paging with `OFFSET` / `FETCH`

### Course 25 — Introduction to RESTful APIs

The bridge from desktop to web.

- HTTP fundamentals: methods, status codes, request/response objects
- REST principles and constraints
- ASP.NET Core controllers, attribute routing, model binding
- DTOs (request vs response), DataAnnotations validation
- 3-tier architecture with the Contracts pattern
- File uploads (`IFormFile`, `multipart/form-data`, MIME types)
- Client-server communication, `HttpClient` URL resolution
- `CreatedAtRoute`, `201 Created`, `204 No Content`
- TOCTOU race conditions and act-and-check fixes

---

## 🧠 Technologies & Concepts Covered

| Area                      | Topics                                                                                     |
| ------------------------- | ------------------------------------------------------------------------------------------ |
| **Programming Languages** | C++, C#, SQL, T-SQL                                                                        |
| **Core CS**               | Algorithms (Levels 1–5), Data Structures (Level 1), OOP (Concepts & Applications)         |
| **Database**              | SQL Server, T-SQL programming, stored procedures, transactions, triggers, CTEs, window functions |
| **C# & .NET**             | Delegates, events, multithreading, `async/await`, reflection, serialization, cryptography  |
| **Data Access**           | ADO.NET (DataTable, DataSet, DataView, DataAdapter, Transactions)                          |
| **Backend & APIs**        | ASP.NET Core, REST, HTTP, model binding, DTOs, 3-tier architecture, Contracts pattern      |
| **Desktop**               | WinForms, User Controls, custom controls, event-driven programming                         |
| **Tools & OS**            | Git, GitHub, Visual Studio, SSMS, Postman, Swagger, Linux                                  |
| **Projects**              | Bank & ATM, Contacts Management, DVLD (License Management), StudentAPI (REST), Win32 demos |

---

## 📈 Progress

- **Started:** August 2024
- **Last updated:** October 2026
- **Courses completed:** 21 / 26 (plus side projects)
- **Current course:** Level 26 – API Security (JWT, OAuth, ASP.NET Core Identity)

---

## 📂 Repository Structure

Each course has its own folder containing:

- Code examples
- Exercises and solutions
- Final projects (where applicable)
- Markdown documentation in `Docs/`

Example:


---

## 🔗 Related Repositories

- **[StudentAPIProject](https://github.com/YounesJY/StudentAPIProject)** — 3-tier ASP.NET Core Web API, extracted from Level 25
- **[Win32-Api-Demos](https://github.com/YounesJY/Win32-Api-Demos)** — 12 Win32 API demos via P/Invoke

---

## 🔗 Connect with me

- **GitHub:** [YounesJY](https://github.com/YounesJY)
- **LinkedIn:** [jayid-younes](https://www.linkedin.com/in/jayid-younes/)
- **Email:** [younes.jayid.23@gmail.com](mailto:younes.jayid.23@gmail.com)

---

## 🙏 Acknowledgements

Special thanks to **Dr. Mohammed Abu-Hadhoud** for creating this structured, practical roadmap that bridges the gap between academic knowledge and real-world development.

---

> *"Understand the why, then write the how."*
