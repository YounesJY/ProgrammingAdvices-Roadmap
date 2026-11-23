# git add (Staging Concept)

## 1. Introduction

The three states are known: **<mark>Working → Staging → Repository</mark>**. This lesson focuses on the most misunderstood step: **<mark>staging</mark>**.

---

## 2. The Concept

`git add` moves changes from:

> **<mark>Working Directory → Staging Area</mark>**

It does **<mark><u>not</u></mark>** save your work. It **<mark><u>prepares</u></mark>** your next snapshot.

You are telling Git: **"<mark>Include THIS in my next commit.</mark>"**

---

## 3. Mental Model

`git add` is like **<mark><u>selecting</u> photos <u>before</u> uploading</mark>**:

| Photo action                    | Git equivalent                 |
| ------------------------------- | ------------------------------ |
| You take many photos            | You make many changes          |
| You choose which ones to upload | `git add` selects what goes in |
| Upload                          | `git commit`                   |

---

## 4. The Workflow

### Step 1 — Check Current State

```bash
git status
```

Example output:

```
Untracked files:
  main.cpp
```

### Step 2 — Add File to Staging

```bash
git add main.cpp
git status
```

Output:

```
Changes to be committed:
  new file: main.cpp
```

The file is now staged.

### Step 3 — Modify File After Staging

Edit `main.cpp` again, then:

```bash
git status
```

You will see:

- Changes **<mark>staged</mark>**
- New changes **<mark>not staged</mark>**

Git tracks both **<mark>separately</mark>**. The staged version is frozen; new edits are a second, unstaged change on top.

### Step 4 — Add All Changes in Current Directory

```bash
git add .
```

Adds all files in the current directory (and below).

### Step 5 — Add Specific Files Only

```bash
git add main.cpp main2.cpp
```

You stay in control.

### Step 6 — Add All Files in Repo

```bash
git add -A
```

Stages all changes across the whole repository, including deletions.

### Step 7 — Remove File from Staging

```bash
git restore --staged main.cpp
```

Moves the file from **Staging Area → Working Directory**. The changes are **not deleted**.

### Step 8 — Discard Changes from Working Directory

```bash
git restore main.cpp
```

Restores `main.cpp` to the last committed version.

> **Careful:** your current changes will be **deleted**.

### Step 9 — Restore After Staging and Editing Again

Stage the file, edit it again, then `git status` shows both staged and unstaged changes.

| Goal                                 | Command                         |
| ------------------------------------ | ------------------------------- |
| Remove only the staged version       | `git restore --staged main.cpp` |
| Delete only the new unstaged changes | `git restore main.cpp`          |

---

## 5. Common Mistakes

| Mistake                                      | Reality                                                    |
| -------------------------------------------- | ---------------------------------------------------------- |
| Thinking `git add` saves changes             | It does not — `git commit` does                            |
| Always using `git add .` blindly             | You may stage unwanted files, or be in the wrong directory |
| Thinking `git restore --staged` deletes work | It does not — it only removes changes from staging         |

---

## 6. The Rule

> **`git add` = <mark>choose what goes into the next snapshot</mark>.**
> **`git restore --staged` = <mark>unstage changes</mark>.**
> **`git restore` = <mark>discard working changes</mark>.**

---

## 7. Wrap-up

Staging and restoring are now clear:

- You can choose what goes into the next snapshot
- You can undo staging when needed

Next: turning staged changes into a real snapshot.

---

📌 **Summary**

| Question                                  | Answer                                                                              |
| ----------------------------------------- | ----------------------------------------------------------------------------------- |
| What does `git add` do?                   | Moves changes from working directory to staging area                                |
| Does `git add` save work?                 | No — it prepares the next commit                                                    |
| `git add .` vs `git add -A`               | `.` = current directory; `-A` = whole repo including deletions                      |
| How to unstage a file?                    | `git restore --staged <file>`                                                       |
| How to discard working changes?           | `git restore <file>` — deletes them                                                 |
| What if a file is staged *and* re-edited? | Git tracks both: the staged version and the new unstaged edits separately           |
| The rule?                                 | `add` = select for next snapshot; `restore --staged` = unstage; `restore` = discard |
