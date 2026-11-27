# Unstage Files

## 1. Introduction

You staged a file, then realized: *"I'm not ready to commit this,"* or *"I added the wrong file."*

**<mark>Staging <u>is not</u> final</mark>**. When a file is staged, it is ready for the next commit — but if you change your mind, you can remove it from the Staging Area **<mark><u>without deleting your work</u></mark>**.

---

## 2. The Concept

```bash
git add main.cpp
```

Moves the current version of `main.cpp` into the Staging Area.

To remove it from staging:

```bash
git restore --staged main.cpp
```

This tells Git: *"Remove `main.cpp` from the next commit, but keep my changes in the file."*

> **<mark>Critical idea</mark>:** `git restore --staged <file>` does **<mark>not</mark>** delete your edits. **<mark>It <u>only removes</u> the file from <u>staging</u></mark>**. Your code stays changed in the Working Directory.

---

## 3. Mental Model: The "Ready to Commit" Box

| Command                | Action                              |
| ---------------------- | ----------------------------------- |
| `git add`              | Put this file in the ready box      |
| `git restore --staged` | Take this file out of the ready box |

**<mark>The file <u>is not</u> destroyed</mark>**. It is still on your desk (the Working Directory).

---

## 4. Demo

### Step 1 — Clean repository

`git status` → `nothing to commit, working tree clean`.

### Step 2 — Make a change

Edit `main.cpp`. `git status` shows:

```
Changes not staged for commit:
  modified: main.cpp
```

### Step 3 — Stage the file

```bash
git add main.cpp
```

`git status` shows:

```
Changes to be committed:
  modified: main.cpp
```

### Step 4 — Realize it is not ready

```bash
git restore --staged main.cpp
```

### Step 5 — Check status

```bash
git status
```

```
Changes not staged for commit:
  modified: main.cpp
```

The file is no longer staged. But the change still exists. **<mark>Your code <u>was not</u> deleted.</mark>**

---

## 5. Compare the Two Commands (Critical)

| Command                         | Effect                                                                                                     | When to use                   |
| ------------------------------- | ---------------------------------------------------------------------------------------------------------- | ----------------------------- |
| `git restore main.cpp`          | **<mark><u>Removes the change</u></mark>** from the Working Directory (**<mark>deletes your edit</mark>**) | Throw the change away         |
| `git restore --staged main.cpp` | Removes from Staging Area, **<mark><u>keeps the edit</u></mark>**                                          | **<mark>Unstage only</mark>** |

> **<mark>The word `--staged` is the key.</mark>** It means: work with the Staging Area, not the Working Directory.

---

## 6. Common Use Case: After `git add .`

You changed `main.cpp` and `notes.txt`, then ran `git add .`. Both are staged. You want to commit only `main.cpp`.

```bash
git restore --staged notes.txt
```

Now:

| File        | State                   |
| ----------- | ----------------------- |
| `main.cpp`  | Staged                  |
| `notes.txt` | Unstaged (changes kept) |

This lets you make a clean, focused commit.

---

## 7. When to Use / Not Use

| Use `git restore --staged` when         | Do not use when                          |
| --------------------------------------- | ---------------------------------------- |
| You staged the wrong file               | You want to delete the change completely |
| You want to commit something else first | Already committed                        |
| You want to review before committing    | File is not staged                       |
| You ran `git add .` and added too much  | You want to restore an older commit      |
| You want to unstage but keep the edits  | —                                        |

For deleting an unstaged change: `git restore <file>`.

---

## 8. Common Mistake

> *"Unstage means undo the file change."*

**<mark>No</mark>.** Unstage means: remove from the Staging Area. It **<mark><u>does not delete</u></mark>** the edit.

After `git restore --staged main.cpp`, the file is still modified. To also delete the edit, run `git restore main.cpp` — but only if you really want to remove the change.

---

## 9. Quick Practice

1. Edit `main.cpp`.
2. `git add main.cpp`
3. `git status` — confirm it is staged.
4. `git restore --staged main.cpp`
5. `git status` — confirm it is unstaged.
6. Open the file — your changes are still there.

---

## 10. Wrap-up

A staged file is ready for the next commit. If you staged the wrong file, remove it from the Staging Area:

```bash
git restore --staged <file-name>
```

This does not delete your work. The file stays changed in the Working Directory.

The most important difference:

| Command                       | Deletes the change? |
| ----------------------------- | ------------------- |
| `git restore <file>`          | Yes                 |
| `git restore --staged <file>` | No — only unstages  |

Next: fixing the last commit with `git commit --amend`.

---

📌 **Summary**

| Question                              | Answer                                                                        |
| ------------------------------------- | ----------------------------------------------------------------------------- |
| How to unstage a file?                | `git restore --staged <file>`                                                 |
| Does it delete the change?            | No — the edit stays in the Working Directory                                  |
| Difference from `git restore <file>`? | `--staged` unstages; without it, the edit is discarded                        |
| Common case?                          | After `git add .`, unstage the files you don't want in this commit            |
| Does it work if already committed?    | No — use history undo commands                                                |
| The key insight                       | `--staged` operates on the Staging Area; without it, on the Working Directory |
