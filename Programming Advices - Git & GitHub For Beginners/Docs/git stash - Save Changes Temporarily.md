# git stash (Save Changes Temporarily)

## 1. Introduction

You are working on a file, and **<mark><u>suddenly</u> need to switch tasks</mark>**: change branches, fix something urgent, pull updates, test something quickly.

**<mark>But your current work is <u>not ready</u> for a commit</mark>**.

> **<mark>How do you <u>save unfinished changes without committing them</u>?</mark>**

That is `git stash`.

---

## 2. The Concept

`git stash` **<mark><u>temporarily</u> saves uncommitted changes</mark>** and **<mark><u>cleans</u> the working directory</mark>**.

> "Git, keep these changes safe for me. I will come back to them later."

After stashing, the project returns to a clean state. You can bring the changes back whenever you need.

---

## 3. Mental Model: The Storage Box

You are working at a desk covered in papers. Suddenly you need a clean desk.

You put the papers in a box. The desk is clean. Later, you open the box and continue.

| Desk analogy | Git equivalent      |
| ------------ | ------------------- |
| Your desk    | Working directory   |
| Your papers  | Uncommitted changes |
| The box      | The stash           |

`git stash` hides unfinished work without losing it.

---

## 4. Demo

Starting `main.cpp`:

```cpp
#include <iostream>
int main()
{
    std::cout << "Hello Git!" << std::endl;
    return 0;
}
```

Edit it:

```cpp
#include <iostream>
int main()
{
    std::cout << "Hello Git!" << std::endl;
    std::cout << "I am learning git stash." << std::endl;
    return 0;
}
```

`git status` shows `modified: main.cpp`. The change is not ready to commit.

### Step 1 — Save temporarily

```bash
git stash
```

```
Saved working directory and index state WIP on main: abc1234 Initial commit
```

```bash
git status
```

```
nothing to commit, working tree clean
```

**<mark>The changes <u>are not</u> deleted</mark>** — they are inside the stash.

### Step 2 — View saved stashes

```bash
git stash list
```

```
stash@{0}: WIP on main: abc1234 Initial commit
```

`stash@{0}` is the latest stash.

### Step 3 — Bring changes back

```bash
git stash apply
```

Your unfinished work returns. `git status` shows `modified: main.cpp` again.

> **<mark>`apply` <u>restores the changes</u> but <u>keeps the stash in the list</u>.</mark>**

### Step 4 — Apply and remove

```bash
git stash pop
```

Restores the changes **<mark><u>and</u></mark>** removes the stash entry.

| Command           | Restore? | Keep stash? |
| ----------------- | -------- | ----------- |
| `git stash apply` | Yes      | Yes         |
| `git stash pop`   | Yes      | No          |

### Step 5 — Multiple stashes

```bash
git stash
# later
git stash
git stash list
```

```
stash@{0}: WIP on main: def5678 Add calculator function
stash@{1}: WIP on main: abc1234 Initial commit
```

The newest stash is always `stash@{0}`; older ones shift down.

### Step 6 — Apply a specific stash

```bash
git stash apply "stash@{1}"
```

Useful when more than one stash exists.

### Step 7 — Delete a specific stash

```bash
git stash drop "stash@{0}"
```

### Step 8 — Delete all stashes

```bash
git stash clear
```

> **<mark><u>Careful</u> — removes all stashes</mark>**.

---

## 5. Important Note About Untracked Files

**<mark><u>By default</u></mark>**, `git stash` saves changes to **<mark><u>tracked</u></mark>** files only.

A new file (`helper.cpp`) that Git has never tracked is **<mark><u>not</u></mark>** included by default.

To include it:

```bash
git stash -u
```

or:

```bash
git stash --include-untracked
```

---

## 6. Cleaning the Repository After Stash

After `git stash`, the repository may still not look clean — because untracked files stay behind.

Example:

```
main.cpp        modified
helper.cpp      untracked
```

`git stash` stashes `main.cpp` changes but leaves `helper.cpp` in place. `git status` still shows:

```
Untracked files:
  helper.cpp
```

Use `git stash -u` to include untracked files and fully clean the tree.

---

## 7. Practical Scenario

You are working on a new feature in `main.cpp`, not ready to commit.

Your teacher asks you to switch to another branch and fix a small mistake.

Git may stop you if your changes could be overwritten by the branch switch.

```bash
git stash -u
git switch -c bug-fix
# fix the problem
git add .
git commit -m "Fix bug"
git switch main
git merge bug-fix
git stash pop
```

You paused your work safely, handled the urgent task, then continued.

---

## 8. Common Commands

| Command                       | Purpose                                           |
| ----------------------------- | ------------------------------------------------- |
| `git stash`                   | Save current uncommitted changes temporarily      |
| `git stash list`              | Show all saved stashes                            |
| `git stash show "stash@{0}"`  | Preview a stash                                   |
| `git stash apply`             | Restore the latest stash, keep it in the list     |
| `git stash pop`               | Restore the latest stash, remove it from the list |
| `git stash apply "stash@{1}"` | Apply a specific stash                            |
| `git stash drop "stash@{0}"`  | Delete a specific stash                           |
| `git stash clear`             | Delete all stashes                                |
| `git stash -u`                | Stash tracked and untracked files                 |

---

## 9. When to Use git stash

- Work is not ready for a commit
- Need to switch branches quickly
- Want to clean the working directory temporarily
- Need to pull or test without losing current changes

## 10. When Not to Use git stash

**<mark><u>Do not</u> use stash as a replacement for commits.</mark>**

| Situation                             | Use          |
| ------------------------------------- | ------------ |
| Work is important and complete enough | `git commit` |
| Work is temporary or unfinished       | `git stash`  |

**<mark>A commit is <u>part of history</u>. A stash is <u>temporary</u>.</mark>**

---

## 11. Wrap-up

`git stash` temporarily saves uncommitted changes and cleans the working directory, without losing the work.

> **`git stash` is like a pause button for your current work.**

---

📌 **Summary**

| Question                       | Answer                                                                 |
| ------------------------------ | ---------------------------------------------------------------------- |
| What does `git stash` do?      | Saves uncommitted changes temporarily and cleans the working directory |
| Where do the changes go?       | Into the stash list — not deleted, not committed                       |
| How to view stashes?           | `git stash list`                                                       |
| How to restore and keep?       | `git stash apply`                                                      |
| How to restore and remove?     | `git stash pop`                                                        |
| How to apply a specific stash? | `git stash apply "stash@{n}"`                                          |
| How to delete one stash?       | `git stash drop "stash@{n}"`                                           |
| How to delete all stashes?     | `git stash clear`                                                      |
| What about untracked files?    | Default stash skips them — use `git stash -u`                          |
| When to use stash?             | Unfinished work, branch switching, temporary cleanup                   |
| When not to use stash?         | As a replacement for commits                                           |
| The mental model               | A pause button for current work                                        |
