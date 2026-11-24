# Working with Git Branches

## 1. Introduction

You don't always want to change the main version directly. Sometimes you want to try a **<mark>new idea, build a feature, fix a bug, or <u>experiment</u></mark>** — **<mark><u>without breaking the stable version</u></mark>**.

That is what branches exist for: a **<mark>separate line of work</mark>** inside the same project. You make changes safely, test them, and later decide whether to bring them back.

---

## 2. The Concept

A Git branch is a **<mark><u>separate</u> path of development</mark>**.

Most projects start with one branch, usually `main` (older repos: `master`). It holds the stable version you trust.

```bash
git branch login-feature
```

This creates a branch. **It does not move you onto it.** The branch simply exists.

```bash
git switch login-feature
```

Now you are on it. Any commit you make belongs to `login-feature`, and `main` stays where you left it.

**<mark><u>Shortcut</u></mark> — create and switch in one command:**

```bash
git switch -c login-feature
```

### Why branches matter

Without branches, everyone works directly on the stable version. Start adding a login system, it's half-finished, it has bugs — the stable version becomes messy and unsafe.

With branches, each piece of work has its own space:

| Branch          | Purpose             |
| --------------- | ------------------- |
| `main`          | Stable project      |
| `login-feature` | Work in progress    |
| `bug-fix`       | Fixing a problem    |
| `design-update` | Trying a new design |

---

## 3. Mental Model: The Road

The main branch is the **<mark><u>main road</u></mark>** — stable, working.

A branch is a **<mark><u>side</u> road</mark>** that breaks off from it:

- Idea works → connect the side road back to the main road
- Idea fails → delete the side road; the main road is untouched

Branches let you experiment safely.

---

## 4. Demo

![](https://uploads.teachablecdn.com/attachments/cd17d7d4aff84b1095fe9549e4bb7317.png)

### Step 1 — Check your current branch

```bash
git branch
```

```
* main
```

The `*` marks the current branch.

### Step 2 — Create a new branch

```bash
git branch greeting-feature
git branch
```

```
  greeting-feature
* main
```

The branch exists, but you are still on `main`.

### Step 3 — Switch to it

```bash
git switch greeting-feature
git branch
```

```
* greeting-feature
  main
```

You are now on `greeting-feature`.

### Step 4 — Create a file

```cpp
// greeting.cpp
#include <iostream>

int main() {
    std::cout << "Hello from the greeting feature!" << std::endl;
    return 0;
}
```

`git status` shows `greeting.cpp` as a new file.

### Step 5 — Stage and commit

```bash
git add greeting.cpp
git commit -m "Add greeting feature"
```

This commit is saved **on `greeting-feature`**, not on `main`.

### Step 6 — Switch back to main

```bash
git switch main
```

`greeting.cpp` disappears. That is normal — the file lives on `greeting-feature`, not on `main`.

### Step 7 — Switch back to the feature branch

```bash
git switch greeting-feature
```

`greeting.cpp` comes back. Git updates your working folder to match the branch you are on.

---

## 5. The Insight: A Branch Is a Pointer

> **<mark>A branch <u>is not</u> a copy of your project</mark>.**

Creating a branch does not duplicate the folder, the files, or the history. It creates a **<mark><u>lightweight pointer to a commit</u></mark>**.

That is why branch creation is instant, even on a huge project — Git is adding **<mark>a <u>small label</u></mark>**, not copying anything.

When you switch branches, Git **<mark><u>rebuilds</u> your working folder</mark>** to match that branch. A file can "disappear" on one branch and "reappear" on another because you are viewing a different point of the project — the file never moved.

> **<mark>A branch is a <u>movable label</u>, not a separate copy</mark>.**

---

## 6. Common Confusion

| Confusion                                      | Reality                                                                                   |
| ---------------------------------------------- | ----------------------------------------------------------------------------------------- |
| "I ran `git branch`, but I'm still on `main`!" | Correct — `git branch` only creates. Use `git switch` to move.                            |
| "My files disappeared after switching!"        | They didn't. They don't exist on the branch you switched to. Switch back and they return. |
| "A branch copied my whole project."            | No — a branch is a pointer to a commit. Nothing is duplicated.                            |
| "I committed to the wrong branch!"             | Happens when you forget to check. Always confirm with `git branch` before committing.     |

---

## 7. The Rules

- **<mark>Keep `main` <u>stable</u>.</mark>** Don't build unfinished features on it.
- **<mark><u>Check your branch</u> before you commit.</mark>** Run `git branch` and confirm the `*`.
- **<mark><u>One branch</u> = <u>one focused job</u>.</mark>** Don't mix unrelated work.
- **<mark>Name branches <u>clearly</u></mark>.** `login-feature` tells you what it is; `test123` does not.

---

## 8. Quick Command Reference

| Action             | Command                |
| ------------------ | ---------------------- |
| Create a branch    | `git branch <name>`    |
| Switch to a branch | `git switch <name>`    |
| Create and switch  | `git switch -c <name>` |
| List branches      | `git branch`           |
| Delete a branch    | `git branch -d <name>` |

---

## 9. Wrap-up

A branch is a separate line of work. It lets you experiment, build features, and fix bugs without touching the stable `main` version.

The key idea: **<mark>a branch is not a copy — it is <u>a pointer to a commit</u></mark>**, and Git rebuilds your folder to match whichever branch you stand on.

> **A branch is <mark>a safe workspace for a new idea</mark>.**

Next: Git Tags — marking important commits.

---

📌 **Summary**

| Question                                    | Answer                                                                            |
| ------------------------------------------- | --------------------------------------------------------------------------------- |
| What is a branch?                           | A separate line of development — a pointer to a commit                            |
| Does `git branch` switch to the new branch? | No — it only creates. Use `git switch` to move                                    |
| How to create and switch at once?           | `git switch -c <name>`                                                            |
| Does a branch copy files?                   | No — it's a lightweight pointer; nothing is duplicated                            |
| Why do files "disappear" when switching?    | Git rebuilds the working folder to match the target branch                        |
| Why is branch creation instant?             | Only a label is added, no files copied                                            |
| Default branch names                        | `main` (modern) or `master` (older)                                               |
| Best practices                              | Keep `main` stable; check branch before commit; one branch = one job; clear names |
| The insight                                 | A branch is a movable label, not a separate copy                                  |
