# Programming Advices: Roadmap Progress

![Roadmap](ProgrammingAdvices%20-%20RoadMap.png)

This repository is a personal archive of the Programming Advices curriculum by Dr. Mohammed Abu-Hadhoud. It has 22 level folders (Levels 1, 3–21, 25, and 26) covering C++ exercises, algorithms and data structures, OOP projects, C#/.NET desktop applications, SQL Server and T-SQL, ADO.NET, the DVLD desktop system, and ASP.NET Core REST APIs with JWT security. The folders mix Visual Studio solutions, standalone source files, SQL scripts, Markdown notes, and downloaded course videos. The repository has 13,459 files, 4,429 of them outside Visual Studio build folders. The git history covers 320 commits from 2025-11-06 to 2026-10-30.

---

## Structure

- One numbered folder per level: `Programming Advices - Level N - <course name>`.
- Level folders may contain:
  - `courseLessons/`: per-lesson or per-problem Visual Studio projects and source files.
  - `CodingC++/`: standalone `.cpp` files (Levels 3–5).
  - `Docs/`: Markdown notes (Levels 20, 21, 25, 26).
  - `Assets/`, `Ressources/`, `Resources/`: backups, snapshots, extra videos, and reference files.
- **Level folders present:** 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 25, 26.
- **Level folders absent:** 2, 22, 23, 24.
- Root files: `README.md`, `ProgrammingAdvices - RoadMap.png`, `.gitignore`, `.gitattributes`.

### Status key

| Status | Meaning in this README |
| --- | --- |
| **Complete** | The folder has the final project, the last numbered lesson or topic, or both. |
| **Partial** | Only some lessons or problems are present. |
| **Reference only** | Videos or notes are present, but no source code or SQL scripts. |
| **Duplicate** | The source files replicate another level's files. |
| **Absent** | No folder exists for that level. |

---

## Progress table

| Level | Course | Status | Topics | Projects |
| --- | --- | --- | --- | --- |
| 01  | Programming Foundations Level 1 | Reference only | Computer basics, number systems, networks, programming languages, variables | None |
| 02  | Algorithms Level 1 | Absent | 50 problems solved as flowcharts instead of code, to force thinking before writing code | 50 flowcharts for 50 different problems, drawn on paper; none attached to the repository |
| 03  | Introduction to Programming Using C++ Level 1 | Partial | C++ syntax, data types, control flow, loops, functions, structs, plus 50 Level 2 problems solved in code | No Visual Studio projects; 20 standalone `.cpp` files |
| 04  | Algorithms & Problem-Solving Level 1 Solutions | Duplicate | Same files as Level 03, refactored around DRY, divide and conquer, small functions, and enums | 20 standalone `.cpp` files; refactors of Level 03 |
| 05  | Algorithms & Problem-Solving Level 2 | Partial | Functions, arrays, strings, enums, inheritance, OOP calculator | TicTacToe, Math Game. |
| 06  | Introduction to Programming Using C++ Level 2 | Partial | Recursion, static variables, vectors, pointers, dynamic memory, file I/O, datetime | 26 lesson projects |
| 07  | Algorithms & Problem Solving Level 3 | Complete | Problems 1–51 (49 folders present), file-based records | Bank1 console project |
| 08  | Algorithms & Problem Solving Level 4 | Complete | Problems 1–65 in grouped folders, header-based libraries | Extended Bank project, ATM |
| 09  | Programming Foundations Level 2 | Reference only | IP, TCP, DNS, HTTP/HTTPS, APIs, XML, JSON, GUID, 3-tier architecture | None |
| 10  | OOP as it Should Be (Concepts) | Partial | Classes, constructors, inheritance, virtual functions, friends, `this`, object arrays | Calculator, Date, String, Period class projects |
| 11  | OOP as it Should Be (Applications) | Complete | Console banking app: clients, users, login, permissions, transactions, extensions | Incremental C++ banking system (39 lessons) |
| 12  | Data Structures Level 1 | Reference only | Big O, arrays, matrices, stack, queue, linked lists, maps, unions | None |
| 13  | Algorithms & Problem Solving Level 5 | Complete | Custom stacks, queues, dynamic arrays, linked lists, date and string headers | 8 console projects |
| 14  | C# - Level 1 | Complete | WinForms controls, dialogs, MDI, timers, image lists, custom control library | Pizza project iterations, TicTacToe, image viewer |
| 15  | Database Level 1 - SQL (Concepts and Practice) | Reference only | Database concepts, ERD, relational schema, DDL/DML, joins, subqueries, views, indexes | None; videos and one `.bak` file |
| 16  | OOP As It Should Be In C# | Partial | C# OOP, class libraries, virtual functions, WinForms | Calculator, class library, Person, Pizza, Virtual Functions |
| 17  | Database - SQL (Projects & Practice) | Complete | ERD design, SQL query problems, joins, aggregates, views, data import | 3 ERD requirements, 5 ERD solutions, 54 query scripts |
| 18  | C# & Database Connectivity | Complete | ADO.NET, parameterized queries, CRUD, DataTable, DataView, DataSet, DataAdapter | Contacts solution (console and WinForms) |
| 19  | Full Real Project - DVLD | Complete | 3-tier desktop architecture, user controls, events between forms, licensing domain | DVLD solution: UI, Business, DataAccess, Common |
| 20  | C# - Level 2 | Partial | Delegates, events, lambdas, serialization, reflection, attributes, threading, tasks, crypto, registry | Traffic Light, Pool Club, custom controls |
| 21  | Database Level 2 - T-SQL | Complete | Control flow, errors, transactions, cursors, CTEs, triggers, functions, window functions | 110 T-SQL scripts |
| 22  | n/a | Absent | n/a | n/a |
| 23  | n/a | Absent | n/a | n/a |
| 24  | n/a | Absent | n/a | n/a |
| 25  | Introduction to RESTful API | Complete | Windows interop demos, HTTP, REST, controllers, DTOs, 3-tier API, file uploads | 12 Win32 demos, StudentAPI client/server, image upload/download |
| 26  | Secure Your APIs | Complete | Encoding, hashing, encryption, JWT, refresh tokens, authorization, rate limiting, logging, secrets | StudentAPI secured server/client, crypto demos, rate-limit tester |

---

## Notable projects

| Project | Location | What is in the folder |
| --- | --- | --- |
| **DVLD** (Driving License Management) | `Level 19/DVLD_Project` | Four-project solution: WinForms UI with 30 `frm*` files and 12 user controls, 16 Business classes, 16 DataAccess classes, 2 Common classes, and `SQL/DummyData.sql`. |
| **StudentAPI** | `Level 25/courseLessons/14–16`, `Level 26/courseLessons/StudentAPI Project` | ASP.NET Core Web API with console client, database-backed 3-tier version, image upload/download, JWT authentication, refresh tokens, role/policy authorization, rate limiting, and `.http` request files. |
| **Bank / ATM** | `Level 07`, `Level 08`, `Level 11` | C++ console banking systems: Bank1 and Extended Bank (Levels 7 and 8), ATM (Level 8), and a 39-lesson incremental bank application with users, permissions, and currency exchange (Level 11). |
| **Contacts** | `Level 18/courseLessons/Lesson _19–25` | ADO.NET Contacts solution in console and WinForms versions, split into Presentation, Business, and Data Access projects. |
| **Win32 demos** | `Level 25/courseLessons/01–12` | 12 console demos using P/Invoke and Windows APIs: wallpaper, resolution, battery, shutdown, MessageBox, processes, audio, Wi-Fi, Outlook, Word, Excel, PowerPoint. |

---

## Level details

### Level 01: Programming Foundations Level 1

- **Topics:** data, information, knowledge, and wisdom; computer components and functions; binary, octal, and hexadecimal systems; byte units; networks; programming languages; compiler vs interpreter; arithmetic operations and precedence; variables and constants; learning path.
- **Projects:** None.

### Level 03: Introduction to Programming Using C++ Level 1

- **Topics:** C++ program structure, variables and data types, arithmetic, conditions, loops, nested loops, functions, and structs.
- **Projects:** None. The files come from the CodingC++ mobile app and sit outside Visual Studio projects.

### Level 04: Algorithms & Problem-Solving Level 1 Solutions

- **Topics:** Same file set as Level 03.
- **Projects:** None.
- **File types:** 20 `.cpp`.
- **Notable artifacts:** All 20 `.cpp` files in `CodingC++/` are byte-identical (MD5) to the same-named files in Level 03. The two folders contain the same refactored code.

### Level 05: Algorithms & Problem-Solving Level 2

- **Topics:** functions and procedures, arrays, strings, enums, random values, loops, inheritance, and an OOP calculator.
- **Projects:** TicTacToe and Math Game exercises; C++ calculator and multi-level inheritance examples.
- **File types:** 29 `.cpp`, 1 `.hpp`, 1 PDF summary.

### Level 06: Introduction to Programming Using C++ Level 2

- **Topics:** ternary operator, input validation, recursion, static variables, two-dimensional arrays, vectors of structs, pointers vs references, pointers and arrays, pointers to structs, void pointers, `new`/`delete`, dynamic arrays, vector iterators, file write/append/read, loading and saving vectors, deleting and updating records, local/UTC datetime.
- **Projects:** 26 Visual Studio projects, one per lesson folder.
- **Notable artifacts:** `courseLessons/` holds lessons 9–60 with gaps; `Pointers/` is a separate folder.

### Level 07: Algorithms & Problem Solving Level 3

- **Topics:** algorithm problem solving in C++, file-based record management, input and validation libraries.
- **Projects:** Bank1 console project with client records and file persistence.
- **File types:** 99 `.cpp`, 8 `.h`, 50 `.vcxproj`/`.sln`, 55 `.mp4`.
- **Notable artifacts:** 49 `Problem #NN` folders covering problems 1–51 with problems 05 and 43 missing; `Project 1 - Bank1`.

### Level 08: Algorithms & Problem Solving Level 4

- **Topics:** algorithm problem solving, reusable header libraries, grouped problem sets, bank and ATM workflows.
- **Projects:** Extended Bank project (`Project 1 - Bank1`) and ATM.
- **Notable artifacts:** 30 problem folders, some grouping several problems: `Problem #20 - 32`, `Problem #33 - 46`, `Problem #47 - 53`, `Problem #63 - 64`; `CxxDroid/` and `libs/` folders.

### Level 09: Programming Foundations Level 2

- **Topics:** IPv4 and IPv6, IP types, DHCP, NAT, ISP, ports and sockets, subnet masks, MAC addresses, VPN, internet vs WWW, browsers, HTTP vs HTTPS, domain names, DNS, subdomains, URLs, FTP, APIs, XML, JSON, GUIDs, 3-tier architecture, business intelligence and cubes.
- **Projects:** None.

### Level 10: OOP as it Should Be (Concepts)

- **Topics:** classes and objects, constructors, up/down casting, virtual functions, friend classes and functions, inner structs and classes, separate classes in libraries, `this` pointer, passing objects by value/reference, vectors of objects, dynamic object arrays, arrays and constructors.
- **Projects:** Calculator, Date, String, and Period class projects.

### Level 11: OOP as it Should Be (Applications)

- **Topics:** utility and input-validation libraries, client CRUD, transactions, deposits, withdrawals, total balances, user management, login/logout, permissions, nine bank extensions, currency exchange.
- **Projects:** incremental C++ console banking application built across 39 lessons.

### Level 12: Data Structures Level 1

- **Topics:** data structures vs databases, classification of data structures, Big O notation (O(1), O(n), O(n²), O(log n)), arrays, matrices, stacks, queues, vectors, singly/doubly/circular linked lists, ADTs, maps, and unions in C++.
- **Projects:** None.
- **Notable artifacts:** `Data Structures Visualization ｜ Log2Base2` holds 12 visualization videos.

### Level 13: Algorithms & Problem Solving Level 5

- **Topics:** custom implementations of stacks, queues, dynamic arrays, doubly linked lists, dates, and strings.
- **Projects:** eight console projects, `Project #01` through `Project #08`.
- **Notable artifacts:** `headerFiles/` contains `Date.h`, `DoubleLinkedList.h`, `DynamicArray.h`, `Queue.h`, `QueueArray.h`, `QueueLine.h`, `Stack.h`, `StackArray.h`, and `String.h`.

### Level 14: C# - Level 1

- **Topics:** WinForms controls, Pizza project iterations, image handling, `ImageList`, `TreeView`, `NotifyIcon`, `ListView`, timers, progress bars, track bars, `NumericUpDown`, `TabControl`, panels, file dialogs, MDI, `MenuStrip`, context menus, and a custom control library.
- **Projects:** Pizza project across multiple stages, TicTacToe, image viewer, and `WindowsFormsControlLibrary1`.
- **Notable artifacts:** 16 numbered lesson groups in `courseLessons/`.

### Level 15: Database Level 1 - SQL (Concepts and Practice)

- **Topics:** data vs information, databases vs data structures, NULL, redundancy, data integrity, constraints, SQL, DBMS vs RDBMS, ERD symbols and components, entities, attributes, relationships, cardinality, participation, associative entities, generalization/specialization, relational schema conversion, normalization, DDL and DML, keys, joins including self-join, subqueries, views, indexes, `GROUP BY`, `HAVING`, `LIKE`, and wildcards.
- **Projects:** None. The folder contains videos and one `.bak` file; no `.sql` scripts.
- **Notable artifacts:** `Assets/`, `SQL DBMS/`, `SQL DBMS NF/`, `SQL INDEX/`, `SQL Joins/Self-Join/`, `SQL SubQueries/`, `SQL Views/`, and `SQL With Baraa/`.

### Level 16: OOP As It Should Be In C#

- **Topics:** C# OOP, class libraries, virtual functions, and a WinForms Pizza project.
- **Projects:** `CalcuatorProject`, `DotNET_ClassLibrary` (`MyFirstClassLibrary` and `TestMyFirstClassLibraryProject`), `personExercice`, `PizzaProjectUpdated`, and `Virtual Functions`.
- **Notable artifacts:** five project folders in `courseLessons/`.

### Level 17: Database - SQL (Projects & Practice)

- **Topics:** ERD design for clinic, library, karate, car rental, and online store scenarios; SQL query practice on an Employees/Vehicles database, including joins, aggregates, `GROUP BY`, `HAVING`, subqueries, `CASE`, and views.
- **Projects:** three StarUML ERD requirement files, five ERD solution videos, and 54 query scripts.
- **Notable artifacts:** `Section 01 - DataBase Design Projects/ERD Projects Requirements/` (`Project 1 - Clinic.mdj`, `Project 2 - Library.mdj`, `Project 3 - Karate.mdj`), `ERD Projects Solutions/`, `Section 02 - Queries/`, and `Assets/EmployeesDB.bak` / `Assets/VehicleMakesDB.bak`.

### Level 18: C# & Database Connectivity

- **Topics:** ADO.NET connections and commands, parameterized queries, `LIKE`, single-value retrieval, insert/update/delete, identity retrieval, `IN` statements, Contacts solution in console and WinForms, DataTable examples, DataView creation/filtering/sorting, DataSet, and DataAdapter.
- **Projects:** Contacts management solution split into Presentation, Business, and Data Access layers; DataTable/DataSet/DataView demos.
- **Notable artifacts:** 30 lesson folders numbered 07–44 with gaps, including three Contacts versions (`Find Contact`, `Extended`, `FinalVersion`) and `Lesson _25 - Contacts Solution (WinForm)`.

### Level 19: Full Real Project - DVLD

- **Topics:** 3-tier desktop architecture, user controls, passing data between forms, delegates and events, a driving-license management domain, and SQL Server data access.
- **Projects:** `DVLD_Project.sln` with four projects:
  - `DVLD_Project`: WinForms UI, 30 `frm*` files, 12 user controls, 85 non-designer `.cs` files.
  - `DVLD_Business`: 16 `.cs` files.
  - `DVLD_DataAccess`: 16 `.cs` files.
  - `DVLD_Common`: 2 `.cs` files.
  - Plus four introductory `courseLessons` projects on user controls and form communication.
- **File types:** 163 `.cs`, 139 `.png`, 57 `.resx`, 36 `.mp4`, 8 `.csproj`, 5 `.sln`, 4 `.pdf`.
- **Notable artifacts:** `SQL/DummyData.sql` is the only SQL script; the full schema is not included. `Assets/` holds the requirements in `.md`, `.pdf`, `.drawio`, and image form. `Videos/` is split into `Requirements`, `Database Schema`, and `Sections`.

### Level 20: C# - Level 2

- **Topics:** delegates (`Func`, `Action`, `Predicate`, multicast), events and `EventArgs`, lambda expressions, `using` with files and database connections, binary/JSON/XML serialization, conditional/obsolete/custom attributes, reflection, operator overloading, `StringBuilder` and immutability, Windows Registry, `App.Config`, hashing, symmetric/asymmetric encryption, image encryption, threads, synchronization, race conditions, `Task`, `TaskFactory`, `Parallel.For`/`ForEach`/`Invoke`, and custom controls.
- **Projects:** Traffic Light project (original and refactor), Pool Club project, and custom controls.
- **Notable artifacts:** `Docs/` has 23 Markdown reference notes; 14 more `.md` files sit at the level root. `Resources/Delegates & Events/` holds extra videos. Lesson coverage runs from 01 to 82 with large gaps (14–19, 21–23, 25–28, 37–59, 61–64, 83–86).

### Level 21: Database Level 2 - T-SQL

- **Topics:** variables and parameters, `IF`/`ELSE`, `CASE`, `WHILE`, `BREAK`/`CONTINUE`, error handling with `TRY`/`CATCH`, `THROW`, `@@ERROR`, `@@ROWCOUNT`, temporary tables vs table variables, stored procedures, scalar functions, inline and multi-statement table-valued functions, transactions, cursors (static, dynamic, forward-only, scroll), CTEs including recursive CTEs, date-series generation, triggers (`AFTER`, `INSTEAD OF`), window functions (`ROW_NUMBER`, `RANK`, `DENSE_RANK`, `LAG`, `LEAD`), string/date/aggregate functions, paging, and a SQL-injection example.
- **Projects:** 110 T-SQL scripts numbered 01–68, plus 41 Markdown notes.
- **Notable artifacts:** `Docs/` contains 32 topic notes; 8 more `.md` files sit at the level root, and `courseLessons/15 - T-SQL Query VS C# Code - Execution Time.md` is the 41st. `Assets/` and `Ressources/` hold supporting material.

### Level 25: Introduction to RESTful API

- **Topics:** Windows interop through P/Invoke (wallpaper, screen resolution, battery, shutdown, MessageBox, processes, audio, Wi-Fi, Outlook, Word, Excel, PowerPoint); HTTP request/response objects and status codes; REST principles; ASP.NET Core controllers, routing, and Swagger; model binding; DTOs; the 3-tier Contracts pattern; file uploads; `HttpClient` URL resolution; `CreatedAtRoute` and `201 Created`; TOCTOU race conditions; LINQ basics; and `SET NOCOUNT ON`.
- **Projects:** 12 Windows interop demos, FirstRESTProject, StudentAPI client/server, StudentAPI with database (Contracts, Business, DataAccess, API, client), and image upload/download projects.
- **Notable artifacts:** 16 lesson sections, `Docs/` with 25 notes, `Assets/` with 17 `.rar` and 12 `.zip` snapshots, and `Ressources/` with API reference videos.

### Level 26: Secure Your APIs

- **Topics:** encoding vs encryption vs hashing, BCrypt and salting, JWT structure and signing, token expiration, refresh tokens and rotation, token revocation, authentication and authorization, role-based and policy-based authorization, ownership/horizontal privilege escalation, middleware and the ASP.NET Core pipeline, CORS, HTTPS, rate limiting, logging/auditing/monitoring, sensitive data in logs, secrets in environment variables and Azure Key Vault, security auditing, and production hardening.
- **Projects:** `Encoding-Demo`, `Encryption-Demo`, `Hashing-Demo`, secured StudentAPI server, StudentAPI client, client refresh-token demo, testing console app, and rate-limit tester. Two additional refresh-token snapshots are in `Assets/`.
- **Notable artifacts:** `Docs/` has 69 notes; 24 more `.md` files sit at the level root, including `62 - Final Review`, `63 - What Comes Next`, and `99 - Critical Opinion`. NuGet references include `Microsoft.AspNetCore.Authentication.JwtBearer`, `System.IdentityModel.Tokens.Jwt`, `Microsoft.IdentityModel.Tokens`, and `BCrypt.Net-Next`.

### Levels 02, 22, 23, and 24: not present

No folders exist for these levels. Level 02 work was done on paper; Levels 22–24 have no files in the repository.

---

## Tech stack

| Area | What the repository contains | Evidence |
| --- | --- | --- |
| **C++** | C++ with STL, standalone exercises and Visual Studio projects | 295 `.cpp`, 104 `.h`, 1 `.hpp`, 177 `.vcxproj` |
| **C# / .NET** | WinForms, console apps, class libraries, ASP.NET Core Web API | 841 `.cs`, 151 `.csproj`; targets .NET Framework 4.8 (118 projects), 4.7.2 (12), .NET 8 (20), .NET 9 Windows (1) |
| **SQL** | SQL Server and T-SQL scripts, backups, ERD models | 165 `.sql`, 6 `.bak`, 3 `.mdj` (StarUML) |
| **Data access** | ADO.NET (`SqlConnection`, `SqlCommand`, `DataTable`, `DataSet`, `DataView`, `DataAdapter`) | Level 18 projects; `Microsoft.Data.SqlClient` package |
| **Web / API** | ASP.NET Core controllers, attribute routing, Swagger, DTOs, `HttpClient`, `.http` request files | Levels 25–26; `Swashbuckle.AspNetCore`, `Microsoft.AspNetCore.Authentication.JwtBearer` |
| **Security** | JWT, refresh tokens, BCrypt, role/policy authorization, rate limiting, HTTPS, CORS | Level 26 projects and 93 notes; `BCrypt.Net-Next`, `System.IdentityModel.Tokens.Jwt` |
| **Desktop** | WinForms forms and user controls, custom controls, MDI | Levels 14, 16, 18, 19, 20 |
| **Native interop** | P/Invoke Win32 APIs, NAudio | Level 25 demos; `NAudio.Wasapi` |
| **Office automation** | Outlook, Word, Excel, and PowerPoint creation from C# | Level 25 lessons 09–12 |
| **Serialization** | JSON, XML, binary | Level 20 lessons and notes; `Newtonsoft.Json` |
| **Tools** | Visual Studio (`.sln`, `.vs`), VS Code (`.vscode`), JetBrains Rider (`.idea`), Git, StarUML, draw.io | Project and IDE metadata files |

---

## Side projects and extras

- **Roadmap image:** `ProgrammingAdvices - RoadMap.png`.
- **External repositories, not part of this repository:**
  - [StudentAPIProject](https://github.com/YounesJY/StudentAPIProject): a 3-tier ASP.NET Core Web API extracted from Level 25.
  - [Win32-Api-Demos](https://github.com/YounesJY/Win32-Api-Demos): Win32 API demos using P/Invoke.
- **Reference material inside level folders:** `Level 20/Docs`, `Level 20/Resources`, `Level 21/Docs`, `Level 21/Ressources`, `Level 25/Docs`, `Level 25/Ressources`, `Level 26/Docs`, and `Level 26/Ressources`.

---

## Connect

- **GitHub:** [YounesJY](https://github.com/YounesJY)
- **LinkedIn:** [jayid-younes](https://www.linkedin.com/in/jayid-younes/)
- **Email:** [younes.jayid.23@gmail.com](mailto:younes.jayid.23@gmail.com)

## Acknowledgements

Thanks to **Dr. Mohammed Abu-Hadhoud** for the curriculum and course material.
