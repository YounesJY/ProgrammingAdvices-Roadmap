# git commit — Seeing What Changed

## 1. Introduction

A commit is made. Now: **<mark>how do you look inside it</mark>**? What files were touched? What lines changed? **<mark>Git's <u>inspection commands</u></mark>** read the history you've been building.

---

## 2. The Concept

| Command          | What it does                                         |
| ---------------- | ---------------------------------------------------- |
| `git show`       | Opens a single commit and shows everything inside it |
| `git log`        | Lists commits in order                               |
| `git log --stat` | Same list, plus a summary of what changed            |
| `git log -p`     | Same list, plus the full diff for each commit        |

These commands **<mark>do not change anything</mark>**. They only read history.

---

## 3. Mental Model: Commits as a Journal

| Command             | Analogy                                                 |
| ------------------- | ------------------------------------------------------- |
| `git log`           | Table of contents — entries with titles, dates, authors |
| `git log --oneline` | Condensed table of contents — titles only               |
| `git show`          | Opening one entry and reading the full page             |
| `git log -p`        | Flipping through every entry in full, newest to oldest  |

---

## 4. The Insight: Reading a Diff

Git shows changes in a **<mark>diff</mark>** format:

```
- old version of a line
+ new version of a line
```

| Prefix      | Meaning                       |
| ----------- | ----------------------------- |
| `-` (red)   | Line was removed              |
| `+` (green) | Line was added                |
| none        | Unchanged — shown for context |

This format is identical everywhere: terminal, GitHub, editors. **<mark>Learn it <u>once</u>, read changes <u>anywhere</u>.</mark>**

---

## 5. git log vs git log --oneline

### 5.1 `git log` — full detail

```
commit a4f1d3c8b9e2f1a7d6c5b4e3f2a1098765432109
Author: You <you@example.com>
Date:   Mon Apr 8 14:22:31 2026 +0300

    Update greeting message
```

Full commit ID, author, date, message. Use when you need **<mark><u>who did what and when</u></mark>**.

### 5.2 `git log --oneline` — one line per commit

```
a4f1d3c Update greeting message
9f3a2c1 Initial commit
```

Short ID and message. Use for a **<mark>quick overview</mark>** of the timeline.

> **Rule of thumb:** start with `git log --oneline` to scan. When something catches your eye, copy its short ID into `git show <id>`. Reach for plain `git log` only when you need author or date.

---

## 6. Demo

### Step 1 — See your most recent commit

```bash
git show
```

With no arguments, it opens the latest commit:

```
commit a4f1d3c8b...
Author: You <you@example.com>
Date:   Mon Apr 8 14:22:31 2026 +0300

    Update greeting message

diff --git a/main.cpp b/main.cpp
--- a/main.cpp
+++ b/main.cpp
@@ -1,6 +1,6 @@
 #include <iostream>

 int main() {
-    std::cout << "Hello, World!" << std::endl;
+    std::cout << "Welcome to my program!" << std::endl;
     return 0;
 }
```

Top half = commit info. Bottom half = the diff. Press `q` to exit.

### Step 2 — See a specific older commit

```bash
git log --oneline
git show 9f3a2c1
```

The first 7 characters of the ID are usually enough.

### Step 3 — Quick summary instead of full diffs

```bash
git log --stat
```

Per-commit summary:

```
 main.cpp | 2 +-
 1 file changed, 1 insertion(+), 1 deletion(-)
```

Fast "what was touched" overview without reading every line.

### Step 4 — Full diffs for every commit

```bash
git log -p
```

Every commit with its complete diff, newest first. Space/arrows to scroll, `q` to quit.

### Step 5 — Compare any two commits

```bash
git diff 9f3a2c1 a4f1d3c
```

First ID = "before," second = "after." Git shows exactly what's different.

---

## 7. Common Confusion

### 7.1 `git log` vs `git show`

| Command    | What it is                                         |
| ---------- | -------------------------------------------------- |
| `git log`  | The list of commits — like a table of contents     |
| `git show` | The contents of one commit — like reading the page |

### 7.2 "I need a GUI to see changes"

No. The terminal gives everything, and the diff format is the same one every other tool uses underneath.

### 7.3 "The output fills the screen — I'm stuck"

Git uses a pager. Press `q` to quit.

---

## 8. The Rule

> **<mark>Before you save again</mark>, <mark>look at what you just committed</mark>.**

Run `git show` on the latest commit. Read the diff. Confirm it's exactly what you intended — no leftover test code, no commented-out experiments, no unintended files. **<mark>A two-second look here <u>saves hours of cleanup later</u>.</mark>**

---

## 9. Wrap-up

The full loop: stage with `git add`, save with `git commit`, inspect with `git show`, `git log`, and `git diff`. History is not a black box — it's a readable record.

Next: writing professional commit messages.

---

📌 **Summary**

| Question                            | Answer                                           |
| ----------------------------------- | ------------------------------------------------ |
| What does `git show` do?            | Opens one commit: info + full diff               |
| What does `git log` show?           | Commit list with full ID, author, date, message  |
| What does `git log --oneline` show? | One line per commit: short ID + message          |
| What does `git log --stat` add?     | Per-commit summary of files/lines changed        |
| What does `git log -p` add?         | Full diff for every commit                       |
| How to compare two commits?         | `git diff <before> <after>`                      |
| How to read a diff?                 | `-` removed, `+` added, no prefix = context      |
| Why 7-character IDs?                | Usually enough to be unique in practice          |
| How to exit the pager?              | `q`                                              |
| The rule?                           | Read `git show` on every commit before moving on |
