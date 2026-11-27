# Understanding Undo in Git

## 1. Introduction

Mistakes are normal:

- Editing the wrong file
- Staging the wrong file
- Committing too early
- Needing an older version back

Git provides tools to undo safely, but the most important rule is:

> **<mark><u>Before</u> you undo anything, understand <u>where</u> the mistake happened.</mark>**

In Git, undoing is not one single command. **<mark><u>Different mistakes</u> live in <u>different places</u></mark>**, and each place has its own undo command.

So **<mark><u>the first question</u></mark>** is always: **<mark>where is the mistake right now?</mark>**

---

## 2. The Concept

<img title="" src="https://uploads.teachablecdn.com/attachments/63c8f00588c54fefb77d4a0291b4765b.png" alt="" width="471">

Your work moves through three places:

| Place                    | Meaning                                      |
| ------------------------ | -------------------------------------------- |
| **Working Directory**    | Where you edit files                         |
| **Staging Area** (Index) | Where files are prepared for the next commit |
| **Commit History**       | Where saved snapshots live                   |

Each place needs a different undo command.

---

## 3. Working Directory

The Working Directory is your project folder — where you open `main.cpp` and change the code.

At this point, the change is only in the working directory. **<mark>Not staged</mark>**. **<mark>Not committed</mark>**.

```bash
git status
```

```
modified: main.cpp
```

The file changed, but the change is not ready for commit.

**<mark>If the mistake is here</mark>:**

```bash
git restore main.cpp
```

**<mark>This <u>discards the uncommitted changes</u> in that file.</mark>**

---

## 4. Staging Area

The Staging Area is where you prepare files before committing.

```bash
git add main.cpp
```

This tells Git: *"This file is ready to be included in the next commit."*

If you staged the wrong file, the mistake is **<mark>not in the commit yet</mark>** — it is only in the staging area.

**<mark>If the mistake is here</mark>:**

```bash
git restore --staged main.cpp
```

This **<mark>removes the file <u>from staging</u></mark>** but **<mark><u>keeps</u> your changes in the file</mark>**. It does not delete your work — it only says *"not ready for commit yet."*

### Note: Brand-new repositories

In a fresh repo with no commits yet, `git restore --staged` may fail because `HEAD` does not exist yet. In that case:

```bash
git rm --cached main.cpp
```

This removes the file from staging but keeps it in the working directory.

---

## 5. Commit History

Commit History is where saved snapshots live.

```bash
git commit -m "Add greeting message"
```

Now the change is part of the project history.

**<mark>If the mistake is here</mark>**, you **<mark>need a <u>different type</u> of undo</mark>**:

| Command                  | Purpose              |
| ------------------------ | -------------------- |
| `git commit --amend`     | Fix the last commit  |
| `git revert <commit-id>` | Undo a commit safely |
| `git reset`              | Move back            |

These are covered step by step later in the module. For now, the goal is not to memorize commands. The goal is to understand:

> **<mark>The command <u>depends on where</u> the mistake happened.</mark>**

---

## 6. Mental Model: Submitting Homework

| Git place             | Homework analogy                                                                                                                                                              |
| --------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Working Directory** | Still writing in your notebook. You can erase, rewrite, fix before showing anyone.                                                                                            |
| **Staging Area**      | You selected the pages to submit. You can still remove a page from the selected group.                                                                                        |
| **Commit History**    | You already submitted a saved version. The teacher has a record. Fixing it requires a more careful action — a correction, or adjusting the last submission if not yet shared. |

---

## 7. Demo

### Step 1 — Create a file

```cpp
// main.cpp
#include <iostream>

int main()
{
    std::cout << "Hello Git" << std::endl;
    return 0;
}
```

`git status` shows the file as untracked.

### Step 2 — Stage the file

```bash
git add main.cpp
```

`git status` shows the file in the staging area, ready to be committed.

### Step 3 — Commit the file

```bash
git commit -m "Add main program"
```

The file is now saved in commit history.

### Step 4 — Make a new change

```cpp
std::cout << "Hello Git Course" << std::endl;
```

`git status` shows `modified: main.cpp`.

This change is in the **Working Directory**. To undo, use a working-directory undo command.

### Step 5 — Stage the change

```bash
git add main.cpp
```

The change is now staged. To undo, use a staging-area undo command.

### Step 6 — Commit the change

```bash
git commit -m "Update greeting message"
```

The change is committed. To undo, use a history undo command.

---

## 8. The Important Rule

> **<mark>Do not <u>randomly</u> use undo commands.<u> First ask</u>: where is the mistake?</mark>**

| Question                             | If yes                         |
| ------------------------------------ | ------------------------------ |
| Is it only in the Working Directory? | Use a working-directory undo   |
| Is it already in the Staging Area?   | Use a staging-area undo        |
| Is it already committed in History?  | Use a history undo — with care |

---

## 9. Quick Check

| Situation                             | Where is the change? |
| ------------------------------------- | -------------------- |
| Changed a file, did not run `git add` | Working Directory    |
| Ran `git add`, did not commit         | Staging Area         |
| Ran `git commit`                      | Commit History       |

---

## 10. Wrap-up

Undoing mistakes in Git depends on **where the mistake happened**.

- A change in the **Working Directory** needs one type of undo.
- A change in the **Staging Area** needs a different one.
- A change in **Commit History** needs a more careful one.

> **Before you undo anything, understand where the mistake happened.**

Next: undoing changes in the Working Directory using `git restore <file-name>`.

---

📌 **Summary**

| Question                                | Answer                                                        |
| --------------------------------------- | ------------------------------------------------------------- |
| Why are there multiple undo commands?   | Mistakes can happen in different places                       |
| What are the three places?              | Working Directory, Staging Area, Commit History               |
| How to undo in the Working Directory?   | `git restore <file>`                                          |
| How to undo in the Staging Area?        | `git restore --staged <file>`                                 |
| Does `restore --staged` delete changes? | No — it only unstages; the work stays in the file             |
| What if the repo has no commits yet?    | Use `git rm --cached <file>` instead of `restore --staged`    |
| How to undo a commit?                   | `--amend`, `revert`, `reset` — each for a different situation |
| The core rule                           | Before you undo, identify where the mistake lives             |
