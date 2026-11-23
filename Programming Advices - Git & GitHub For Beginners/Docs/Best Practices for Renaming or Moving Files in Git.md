# Best Practices for Renaming or Moving Files in Git

## 1. Introduction

As projects grow, files are renamed, moved into folders, and reorganized. Git usually detects these changes automatically, but the practices around *<mark>how</mark>* you commit them determine whether history stays clean.

---

## 2. The Concept

Git does **<mark><u>not</u></mark>** store a special "rename action". Instead, it compares the old file and the new file. **<mark><u>If</u> the content is <u>similar enough</u>, Git detects a rename</mark>**.

| Scenario                                   | Git's interpretation             |
| ------------------------------------------ | -------------------------------- |
| Content mostly unchanged, path changed     | `rename: old → new`              |
| Content heavily changed *and* path changed | `deleted: old` + `new file: new` |

Renames are **<mark><u>inferred</u></mark>**, not recorded. That is the key fact behind every rule below.

---

## 3. Mental Model: The Librarian

<img title="" src="https://uploads.teachablecdn.com/attachments/5684fb1739fa4320a1828d40b6b12399.png" alt="" width="498">

A librarian tracks books by appearance and content.

- Book moved to another shelf, looks the same → **<mark>same book, new location</mark>**
- Title changed, pages changed, content heavily rewritten → **new book**

Git's rename detection works the same way: **<mark><u>similarity of content</u> decides the interpretation.</mark>**

> <mark>**<u>Vist</u>**</mark>: [aburakab.github.io/courses/git-rename-detection.html](aburakab.github.io/courses/git-rename-detection.html)

---

## 4. Best Practices

### 4.1 Do not rename and heavily edit in the same commit

**<mark><u>Better workflow</u></mark>:**

| Commit | Action                                 |
| ------ | -------------------------------------- |
| 1      | Rename `greeting.cpp` → `messages.cpp` |
| 2      | Update the code inside the file        |

**<mark><u>Why</u></mark>:** Git detects renames via **<mark><u>content similarity</u></mark>**. If too many code changes accompany the rename, Git falls back to *delete + add* instead of *rename* — and history records the operation as two unrelated events.

### 4.2 Keep the file content mostly the same during the rename

Rename `greeting.cpp` → `messages.cpp` with identical content:

```cpp
// messages.cpp
#include <iostream>
#include <string>

void printGreeting(const std::string& user)
{
    std::cout << "Welcome, " << user << "!\n";
}
```

**<mark><u>Why</u></mark>:** near-identical content makes **<mark><u>rename detection</u> trivial</mark>** (typically reported as 100% similarity).

### 4.3 Use clear commit messages

```bash
git commit -m "Rename greeting.cpp to messages.cpp"
```

or:

```bash
git commit -m "Move utility files into src/helpers"
```

**<mark><u>Why</u></mark>:** history becomes readable; other developers understand the change at a glance; future debugging gets easier.

### 4.4 Check Git's interpretation before committing

```bash
git status
```

or:

```bash
git diff --staged --summary
```

Example output:

```
rename greeting.cpp => messages.cpp (100%)
```

**<mark><u>Why</u></mark>:** verify that Git understood the action as a rename — not a delete + add — before creating the commit.

### 4.5 Avoid large structural and code changes together

**Better:**

| Commit | Action                        |
| ------ | ----------------------------- |
| 1      | Move files into `src/` folder |
| 2      | Fix include paths             |
| 3      | Refactor the code             |

**Why:** smaller commits are easier to review, easier to debug, and safer to undo.

### 4.6 Fix include paths after moving C++ files

```cpp
#include "messages.hpp"
```

**<mark><u>Why</u></mark>:** moving files changes their location. If include paths still point to the old location, compilation fails.

---

## 5. Demo

### Step 1 — Commit the rename

```bash
git commit -m "Rename greeting.cpp to messages.cpp"
```

### Step 2 — Edit the file

```cpp
// messages.cpp
#include <iostream>
#include <string>

void printGreeting(const std::string& user)
{
    std::cout << "Welcome to the C++ Demo, "
              << user
              << "!\n";
}
```

### Step 3 — Commit the code changes

```bash
git commit -m "Improve greeting output formatting"
```

Two commits: rename first, edit second.

---

## 6. The Final Rule

> **Rename or move first. Commit it. Then edit the file in another commit.**

This keeps history clean and lets Git detect renames and moves correctly.

---

## 7. Why This Matters Today

This is the first lesson that teaches a **<mark>workflow</mark>**, not a command. The commands (`git status`, `git diff --staged --summary`) are things you already use. The new information is the **<mark>ordering discipline</mark>**:

- Rename and edit are two logical changes → two commits.
- Git's rename detection is similarity-based → content similarity must survive the commit.
- Structural moves and content changes are independent concerns → separate them.

Applied to your own repos: when you reorganize `Docs/` or `courseLessons/` in the Programming Advices repo, the rename commit and the content-edit commit should not be the same commit. Your existing "logical change per commit" discipline already implies this — this lesson makes the reason explicit.

---

📌 **Summary**

| Question                                   | Answer                                                         |
| ------------------------------------------ | -------------------------------------------------------------- |
| Does Git store renames?                    | No — renames are inferred from content similarity              |
| What decides rename vs delete+add?         | How similar the old and new file contents are                  |
| Should rename and edit share a commit?     | No — rename first, edit in a separate commit                   |
| How to verify rename detection?            | `git diff --staged --summary` shows `rename old => new (100%)` |
| What to fix after moving C++ files?        | Include paths                                                  |
| Why separate structural from code changes? | Easier review, debugging, and revert                           |
| What is the final rule?                    | Rename or move first → commit → then edit in another commit    |
| Why does this lesson matter?               | It teaches workflow ordering, not a new command                |
