# git commit (Snapshots & Messages)

## 1. Introduction

`git add` **<mark><u>selected</u> what to save</mark>**. `git commit` **<mark>locks it in</mark>** — turns staged work into a permanent point in the project's history.

---

## 2. The Concept

`git commit` takes everything currently in the **<mark>staging area</mark>** and saves it as a commit: a permanent snapshot stored inside the repository.

Every commit carries:

| Component                       | What it is                                                                                                            |
| ------------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| **<mark>Tree</mark>**           | The snapshot's table of contents — every file and folder by name, pointing to content. Subfolders become nested trees |
| **<mark>Blobs</mark>**          | Stored content of each file. New/modified files get fresh blobs; unchanged files reuse existing blobs                 |
| **<mark>Message</mark>**        | Description of what changed and why                                                                                   |
| **<mark>Commit ID</mark>**      | A unique string (e.g. `9f3a2c1…`) so Git can find this commit forever                                                 |
| **<mark>Parent pointer</mark>** | The commit that came before this one — links history together                                                         |

Once committed, the snapshot is part of history and stays there, restorable exactly as it was.

---

## 3. Mental Model

`git commit` is like **<mark>publishing a new version of your work</mark>**:

| Step         | Equivalent            |
| ------------ | --------------------- |
| `git add`    | Preparing the article |
| `git commit` | Publishing it         |

Once published, you can share it, point others to it, and return to it later exactly as it was.

---

## 4. The Insight: The Parent Pointer

A commit is more than a snapshot — it stores a pointer to its **<mark>parent</mark>**.

```
commit A ← commit B ← commit C
```

Each commit knows the one before it. Walk the arrows backwards from the latest commit and **<mark>you can rebuild the entire history</mark>**, all the way to the first commit.

The **<mark>commit ID</mark>** isn't random. It is derived from:

- the snapshot's contents
- the message
- the author
- the parent's ID

If anyone changes an old commit by a single letter, its ID changes and Git notices. **<mark>History is tamper-evident</mark>.**

---

## 5. Demo

Starting with `main.cpp`:

```cpp
#include <iostream>
int main() {
    std::cout << "Hello, World!" << std::endl;
    return 0;
}
```

### Step 1 — Confirm something is staged

```bash
git status
```

Expected:

```
Changes to be committed:
  new file:   main.cpp
```

If missing, run `git add main.cpp` first.

### Step 2 — Create the commit

```bash
git commit -m "Initial commit"
```

The `-m` flag writes the message inline.

### Step 3 — Check the result

```bash
git status
```

Expected:

```
nothing to commit, working tree clean
```

Everything is saved; the folder matches the latest commit.

### Step 4 — Change and commit again

Edit the greeting line to:

```cpp
std::cout << "Welcome to my program!" << std::endl;
```

Then:

```bash
git add .
git commit -m "Update greeting message"
```

Now two snapshots exist in history.

### Step 5 — See what happened

```bash
git log --oneline
```

Expected:

```
a4f1d3c Update greeting message
9f3a2c1 Initial commit
```

Newest on top, each with its short ID. That's the project's timeline.

---

## 6. Common Confusion

### 6.1 "A commit saves everything in my folder"

No. A commit saves **<mark>only what was <u>staged</u></mark>**.

Edit five files, `git add` two → only those two enter the commit. The other three remain in the working directory, uncommitted.

### 6.2 Lazy commit messages

`"fix"`, `"update"`, `"asdf"`, `"."` look harmless now. Three weeks later, when something breaks, you'll scroll through history trying to identify which `"fix"` caused it. **<mark>Write for the future reader — <u>that reader is you</u>.</mark>**

---

## 7. The Rule

> **`git commit` <mark>turns staged changes into a permanent snapshot</mark> — and <mark>the message is part of the snapshot</mark>, not an afterthought.**

A good commit is **<mark>small, focused, and describes what changed and why in one clear line</mark>**. If you can't summarize it in a sentence, the commit is doing too much — split it.

---

## 8. Wrap-up

Stage with `git add`, snapshot with `git commit`. Each commit becomes a permanent, restorable, shareable point in history. Next: viewing commits and history.

---

📌 **Summary**

| Question                             | Answer                                                                           |
| ------------------------------------ | -------------------------------------------------------------------------------- |
| What does `git commit` do?           | Saves everything in staging as a permanent snapshot                              |
| What does a commit contain?          | Tree, blobs, message, ID, parent pointer                                         |
| What is a tree?                      | The snapshot's table of contents; subfolders are nested trees                    |
| What is a blob?                      | The stored content of a file, addressed by hash                                  |
| What determines the commit ID?       | Contents + message + author + parent ID                                          |
| Does a commit save the whole folder? | No — only what was staged                                                        |
| What does `git log --oneline` show?  | Commits, newest on top, with short IDs                                           |
| What makes a good message?           | One clear line: what changed and why                                             |
| The rule?                            | Commit turns staged changes into a permanent snapshot; the message is part of it |
