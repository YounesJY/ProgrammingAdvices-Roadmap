# git status (The Most Important Command)

## 1. Introduction

Always need to know one thing: **<mark>what is happening in my project right now?</mark>** Before you add, commit, or push anything, check the current situation.

That command is `git status`.

---

## 2. The Concept

`git status` shows the current state of the repository. It tells you about:

| Area              | What it reports                    |
| ----------------- | ---------------------------------- |
| Working Directory | Files changed but not staged       |
| Staging Area      | Files ready to be committed        |
| Untracked files   | Files Git sees but is not tracking |
| Clean state       | Whether anything is pending        |

It is the **<mark><u>primary visibility tool</u></mark>** in Git.

---

## 3. Mental Model: The Car Dashboard

Before driving, **<mark><u>the dashboard</u></mark>** tells you: fuel level, engine state, warnings, readiness.

`git status` tells you:

- What changed?
- What is ready?
- What still needs action?
- What Git can see right now?

> **<mark><u>Without</u></mark> `git status`, <mark>you are using Git <u>blindly</u></mark>.**

---

## 4. Scope: What It Covers

Git files move through three places:

```
Working Directory  →  Staging Area  →  Repository
```

`git status` is responsible for **<mark>the first two</mark>**:

```
Working Directory  →  Staging Area
```

It is **<mark>not</mark>** primarily about committed history.

---

## 5. What git status Tells You

- Which branch you are on
- Which files are new and untracked
- Which files were modified
- Which files are staged and ready to commit
- Which files changed but are not staged
- Whether the working directory is clean

It answers: **<mark>what is happening right now, <u>before</u> my next commit?</mark>**

---

## 6. What git status Does Not Tell You

| Missing information                  | Use instead            |
| ------------------------------------ | ---------------------- |
| Full commit history                  | `git log`              |
| Details of a past commit             | `git show <commit-id>` |
| Exact line-by-line changes           | `git diff`             |
| What is already committed, in detail | `git log` / `git show` |

The division of labor:

> **`git status` <mark>shows <u>what is happening now</u></mark>.**
> **`git log` <mark>shows <u>what happened before</u></mark>.**
> **`git diff` <mark>shows <u>the exact changes</u></mark>.**

---

## 7. Demo

### Step 1 — Run status

```bash
git status
```

### Step 2 — New repository output

```
On branch main
No commits yet
nothing to commit (create/copy files and use "git add" to track)
```

Meaning: on `main`, no commits yet, no tracked files, nothing ready to save.

### Step 3 — Add a new file

Create `main.cpp`, then:

```bash
git status
```

```
Untracked files:
  main.cpp
```

Meaning: Git sees the file, but is not tracking it. Git is saying: *"I see this file, but you did not ask me to track it yet."*

To track:

```bash
git add main.cpp
```

### Step 4 — Stage the file

```bash
git add main.cpp
git status
```

```
Changes to be committed:
  new file: main.cpp
```

Meaning: the file is staged — **<mark><u>ready to be saved</u> in the next commit</mark>**, but not committed yet.

### Step 5 — Modify the file

Edit `main.cpp`, then:

```bash
git status
```

```
Changes not staged for commit:
  modified: main.cpp
```

Meaning: Git noticed the change, but the new edit is only in the Working Directory. Stage it again with `git add main.cpp`.

### Step 6 — Commit the file

```bash
git commit -m "Add main file"
git status
```

```
On branch main
nothing to commit, working tree clean
```

Meaning: no new changes, nothing staged, nothing pending. The working tree matches the latest commit.

---

## 8. The Three Sections You Will See

| Section                           | Meaning                                                 |
| --------------------------------- | ------------------------------------------------------- |
| **Untracked files**               | File exists in the folder, but Git is not tracking it   |
| **Changes not staged for commit** | File was changed, but the change is not ready to commit |
| **Changes to be committed**       | File is staged and ready to be committed                |

---

## 9. The Normal Flow

```bash
git status                          # check what's happening
git add main.cpp                    # stage the file
git status                          # confirm staging
git commit -m "Add main file"       # commit
git status                          # confirm clean
```

Check → stage → check → commit → check.

---

## 10. Common Mistakes

| Mistake                                     | Reality                                             |
| ------------------------------------------- | --------------------------------------------------- |
| Ignoring `git status`                       | You commit blindly and wonder why a file was missed |
| Thinking Git saves everything automatically | Two steps are required: `git add` then `git commit` |
| Forgetting to check after `git add`         | You may stage the wrong file or wrong directory     |
| Thinking `git status` shows history         | It does not — use `git log`                         |

> `git status` = current situation. `git log` = commit history.

---

## 11. The Golden Rule

> **<mark><u>Always run</u></mark> `git status` before and after important Git actions.**

The habit prevents most beginner mistakes.

---

## 12. Wrap-up

`git status` shows what is happening in the project right now: what changed, what is new, what is staged, what is not, what is ready, and whether the tree is clean. It does not show what was already committed — that is `git log`'s job.

---

📌 **Summary**

| Question                             | Answer                                                       |
| ------------------------------------ | ------------------------------------------------------------ |
| What does `git status` show?         | Current state: working directory + staging area              |
| What are its three main sections?    | Untracked files, changes not staged, changes to be committed |
| Does it show commit history?         | No — use `git log`                                           |
| Does it show exact line changes?     | No — use `git diff`                                          |
| What does "working tree clean" mean? | The working directory matches the latest commit              |
| What does "untracked" mean?          | Git sees the file but is not tracking it yet                 |
| What does "staged" mean?             | Ready to be saved in the next commit                         |
| Why check after `git add`?           | To confirm the correct file was staged                       |
| The golden rule?                     | Run `git status` before and after important actions          |
| Division of labor?                   | `status` = present, `log` = past, `diff` = exact changes     |
