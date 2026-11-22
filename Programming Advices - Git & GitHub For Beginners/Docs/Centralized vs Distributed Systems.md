# Centralized vs Distributed Systems

## 1. Introduction

Version control systems split into two models: **<mark>Centralized (CVCS)</mark>** and **<mark>Distributed (DVCS)</mark>**. The difference is not cosmetic — it determines where history lives and what happens when the server is unavailable.

---

## 2. The Repository

<img src="https://uploads.teachablecdn.com/attachments/5d566d5fcb9e4118974aac5cae68d3a2.png" title="" alt="" width="419">

A **<mark>repository (<u>repo</u>)</mark>** is the store of the project's files **<mark>and its <u>full history</u></mark>**. Every version control model is built around a repository; the models differ in *where* that repository lives and *how many copies* exist.

---

## 3. Similarities (CVCS & DVCS)

<img src="https://uploads.teachablecdn.com/attachments/60661125bd6340feb417b1aafdce70b3.png" title="" alt="" width="472">

Both models:

- Track changes over time
- Keep a history of versions
- Allow rollback to previous states
- Support collaboration between developers
- Use a repository as the unit of storage

The difference is architectural, not functional.

<img src="https://uploads.teachablecdn.com/attachments/149607941c4f458aafc2fa8b9fd62c66.png" title="" alt="" width="481">

---

## 4. Centralized Version Control (CVCS)

**<mark>One main server (<u>single source of trust</u>)</mark>.** All developers connect to it, fetch files from it, and send changes back to it.

```
Developer → Server ← Developer
              ↑
           Developer
```

| Property         | Detail                                         |
| ---------------- | ---------------------------------------------- |
| Source of truth  | The server                                     |
| History location | Only on the server                             |
| Local copy       | Working files only (typically no full history) |
| Server down      | Nobody can commit, branch, or inspect history  |
| Network          | Required for nearly every operation            |

The server is a single point of failure and a single point of contention.

---

## 5. Distributed Version Control (DVCS)

**<mark>Every developer has a <u>full copy of the repository</u></mark>** — all files, the full history, every version.

```
Developer (full copy)
Developer (full copy)
Developer (full copy)
```

| Property         | Detail                                                        |
| ---------------- | ------------------------------------------------------------- |
| Source of truth  | Each clone is a complete repository                           |
| History location | With every developer                                          |
| Local copy       | Full repo: files + history                                    |
| Server down      | Work continues normally; commit, branch, log all work locally |
| Network          | Needed only to share changes                                  |

Each developer works independently, then shares changes when convenient.

---

## 6. Mental Model

<img src="https://uploads.teachablecdn.com/attachments/ddacca4f287c4aec92ba18e51f72507f.png" title="" alt="" width="378">

| Model       | Picture                    | Implication                          |
| ----------- | -------------------------- | ------------------------------------ |
| Centralized | Hub with spokes            | Everything depends on the hub        |
| Distributed | Several complete timelines | Everyone holds the complete timeline |

---

## 7. The Insight

<img src="https://uploads.teachablecdn.com/attachments/ec1891f9ee944b3cbdd8d02bdbeda336.png" title="" alt="" width="445">

The key difference is **<mark><u>where</u> the history lives</mark>**:

- **Centralized** → history lives **<mark><u>on the server</u></mark>**
- **Distributed** → history lives **<mark><u>with everyone</u></mark>**

Consequences of history being local:

| Property       | Why it follows                                                   |
| -------------- | ---------------------------------------------------------------- |
| More flexible  | Commit, branch, and inspect history offline                      |
| More powerful  | Full history enables local search, bisect, blame without network |
| More resilient | Server loss doesn't lose history — any clone can restore it      |

---

## 8. Common Confusion

<img src="https://uploads.teachablecdn.com/attachments/c970fb14970545d6850280cba08840b6.png" title="" alt="" width="448">

> "Distributed means no server."

**<mark><u>False</u></mark>**. A server (like GitHub) still exists in a distributed model. What changes:

- It is **<mark>not required</mark>** to work
- It is used **<mark>mainly <u>for sharing</u></mark>** — a rendezvous point, not the source of truth

GitHub in a DVCS is a convenience, not a dependency.

---

## 9. The Rule

<img src="https://uploads.teachablecdn.com/attachments/b46a17e1102d4959b66b292f768b7fa3.png" title="" alt="" width="435">

> **Centralized = <mark><u>one source</u></mark>. Distributed = <mark><u>everyone</u> has the source</mark>.**

---

## 10. Wrap-up

The two models are understood. Next: **<mark><u>Git</u></mark>** — the most popular distributed system.

---

📌 **Summary**

| Question                    | Answer                                                          |
| --------------------------- | --------------------------------------------------------------- |
| What is a repository?       | The store of a project's files and full history                 |
| What is CVCS?               | One central server holds the history; developers connect to it  |
| What is DVCS?               | Every developer holds a full copy, including full history       |
| Key difference?             | Where the history lives — server vs. every clone                |
| Does DVCS mean no server?   | No — a server exists for sharing, but is not required to work   |
| Why is DVCS more resilient? | Any clone can restore the full history if the server is lost    |
| The rule?                   | Centralized = one source; distributed = everyone has the source |
