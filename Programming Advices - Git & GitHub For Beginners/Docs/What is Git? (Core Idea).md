# What is Git? (Core Idea)

## 1. Introduction

Version control and its two models are understood. Git is the concrete system: a **<mark>Distributed Version Control System (<u>DVCS</u>)</mark>**.

---

## 2. The Concept

Git is a DVCS that helps developers:

| Capability    | Meaning                                      |
| ------------- | -------------------------------------------- |
| Track changes | Every modification is recorded               |
| Save versions | Each commit preserves a project state        |
| Work safely   | Experiments are isolated and reversible      |
| Collaborate   | Changes flow between full copies of the repo |

What makes Git special: <mark>**<u>fast</u>**, **<u>powerful</u>**, **<u>works offline</u>**, **<u>keeps full history</u>**</mark>.

Instead of relying on one server, Git gives **<mark>every developer a <u>complete copy of the project</u></mark>**.

---

## 3. Mental Model: A Smart Timeline Manager

<img src="https://uploads.teachablecdn.com/attachments/622ad4fcfb4b44cf80d39924f7bd17ad.png" title="" alt="" width="539">

Git does not just save files — it saves **<mark><u>snapshots</u></mark>** of the project over time.

```
Commit 1 → Snapshot 1
Commit 2 → Snapshot 2
Commit 3 → Snapshot 3
Commit 4 → Snapshot 4
```

Each snapshot represents a **<mark>version of the project</mark>**.

---

## 4. What is a Hash?

A hash is a **<mark>unique <u>fingerprint</u> for content</mark>**.

| Input       | Hash     |
| ----------- | -------- |
| `"File 1"`  | `a1b2c3` |
| `"File 1!"` | `9f8e7d` |

**<mark>Same content</mark>** → **<mark>same hash</mark>**. Change anything → completely different hash.

---

## 5. How Git Stores a File

### 5.1 Create

```
file.txt: Hello
```

Git stores:

```
Hash: A1
Content: "Hello"
```

This stored object is a **<mark>blob</mark>** — the file content addressed by its hash.

### 5.2 Edit

```
file.txt: Hello World
```

Git does **<mark>not</mark>** overwrite the old version. It creates a new blob:

```
Old blob: A1 → "Hello"
New blob: B2 → "Hello World"
```

Git now holds **<mark>two versions</mark>** of the file.

---

## 6. The Big Idea: Snapshots, Not Changes

**<mark>Wrong mental model</mark>** (what beginners assume):

```
+ added "World"
```

**Correct model:**

```
Snapshot 1 → file = A1
Snapshot 2 → file = B2
```

**<mark>Each commit is a <u>full snapshot of the project</u></mark>** — not a patch.

---

## 7. Visual: Files → Hashes → Commits

### 7.1 Files become hashes

```
file1.txt → A1
file2.txt → X9
```

### 7.2 Commit 1

```
Commit C1:
  file1 → A1
  file2 → X9
```

### 7.3 Change file1

```
file1.txt → B2   (new hash)
file2.txt → X9   (unchanged)
```

### 7.4 Commit 2

```
Commit C2:
  file1 → B2
  file2 → X9   (same hash reused)
```

---

## 8. The Key Insight

Git is smart about storage:

| Condition      | Behavior                                                   |
| -------------- | ---------------------------------------------------------- |
| File unchanged | Same hash → **<mark><u>reused</u></mark>**, not duplicated |
| File changed   | New hash → new blob stored                                 |

Git **<mark><u>does not</u> duplicate unchanged files</mark>**. Space is saved automatically. A "full snapshot" per commit is conceptually full but physically deduplicated by content addressing.

---

## 9. Final One-Liner

> **<mark>Git <u>doesn't</u> store changes</mark> — <mark>it stores <u>snapshots</u></mark>, and each file is identified by a hash of its content.**

This is why Git is reliable, efficient, and easy to restore.

---

## 10. Common Confusion

> "Git is GitHub."

| Term       | What it is                                       |
| ---------- | ------------------------------------------------ |
| **Git**    | The tool — **<mark><u>the engine</u></mark>**    |
| **GitHub** | **<mark><u>A platform</u></mark>** that uses Git |

> "Git saves only changes, like patches."

Not exactly. Git computes and displays diffs for humans, but the storage model is **<mark>snapshots of content, <u>addressed by hash</u></mark>**. Diffs are derived, not the storage unit.

---

## 11. The Rule

> **<mark>Git</mark> = a distributed system that <mark>tracks project snapshots over time</mark>.**

---

## 12. Wrap-up

Git's core is understood. Next question: **why does Git feel confusing to beginners?**

---

📌 **Summary**

| Question                             | Answer                                                           |
| ------------------------------------ | ---------------------------------------------------------------- |
| What is Git?                         | A distributed version control system (DVCS)                      |
| What makes it special?               | Fast, powerful, offline-capable, full history per clone          |
| What is a hash?                      | A unique fingerprint of content; same content → same hash        |
| What does Git store per file?        | A blob, addressed by the hash of its content                     |
| Does Git store changes or snapshots? | Snapshots — each commit is a full project snapshot               |
| How does it avoid duplication?       | Unchanged files keep the same hash and are reused                |
| Is Git GitHub?                       | No — Git is the engine; GitHub is a platform built on it         |
| The rule?                            | Git is a distributed system tracking project snapshots over time |
