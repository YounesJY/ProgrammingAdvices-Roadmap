# Restore a File from an Older Commit

## 1. Introduction

Sometimes **<mark>you <u>do not want</u> to undo <u>the whole project</u></mark>**. You only want to bring back **<mark><u>one file</u></mark>** from the past:

- You changed `main.cpp` too much.
- The old version of a file was better.
- You deleted important code from one file.
- You want to compare current work with an older version.

Git allows restoring one file from an older commit **<mark><u>without moving the whole project</u> backward</mark>**.

```bash
git restore --source=<commit-id> <file-name>
```

Example:

```bash
git restore --source=a1b2c3d main.cpp
```

---

## 2. The Concept

**<mark>Every commit is <u>a full snapshot</u> of the project</mark>**. Git remembers **<mark>how every tracked file <u>looked at that moment</u></mark>**.

To bring back an old version of one file:

> *Take this file from that old commit and put it in my current Working Directory.*

| What the command does                                                | What it does not do                        |
| -------------------------------------------------------------------- | ------------------------------------------ |
| Copies the chosen file from an old commit into the Working Directory | Move the whole project back to that commit |
| Leaves all newer commits intact                                      | Delete any history                         |

---

## 3. The Warning

This command **<mark><u>replaces</u></mark>** the current version of the file in the Working Directory with the older version.

**<mark><u>Before</u> using it</mark>**, always ask: *Do I really want this file to look like the old version?*

**<mark><u>If</u> you have current changes <u>you still need</u></mark>**, **<mark><u>save them first</u></mark>** — commit them, stash them, or copy them.

---

## 4. Demo

### Step 1 — Version 1

```cpp
#include <iostream>

int main()
{
    std::cout << "Version 1" << std::endl;
    return 0;
}
```

```bash
git add main.cpp
git commit -m "Add version 1"
```

### Step 2 — Version 2

```cpp
std::cout << "Version 2" << std::endl;
```

```bash
git add main.cpp
git commit -m "Update to version 2"
```

### Step 3 — Version 3

```cpp
std::cout << "Version 3" << std::endl;
```

```bash
git add main.cpp
git commit -m "Update to version 3"
```

### Step 4 — View history

```bash
git log --oneline
```

```
c3d4e5f Update to version 3
b2c3d4e Update to version 2
a1b2c3d Add version 1
```

Copy the commit ID for the version you want. Here we want version 1: `a1b2c3d`.

### Step 5 — Restore the file

```bash
git restore --source=a1b2c3d main.cpp
```

`main.cpp` now contains "Version 1" again.

### Step 6 — Check status

```bash
git status
```

```
Changes not staged for commit:
  modified: main.cpp
```

Git did **<mark>not</mark>** create a commit. It **<mark><u>only</u> changed the Working Directory</mark>**.

### Step 7 — Commit the restored file (if you want to keep it)

```bash
git add main.cpp
git commit -m "Restore main file to version 1"
```

---

## 5. What Exactly Happened

| Before restore                     | After restore                          |
| ---------------------------------- | -------------------------------------- |
| History: V1 → V2 → V3              | History: **V1 → V2 → V3** (unchanged)  |
| Working Directory: `main.cpp` = V3 | Working Directory: `main.cpp` = **V1** |

The history is intact. Git **<mark>did not delete</mark>** V2 or V3. It only copied the V1 version of the file into the current Working Directory.

---

## 6. The Difference

```bash
git restore --source=a1b2c3d main.cpp
```

**<mark>Does not</mark>** mean: *Move the whole project back to `a1b2c3d`.*

**<mark>Means</mark>:** *Bring only `main.cpp` from `a1b2c3d`.*

That is why it is **<mark><u>safer</u></mark>** than moving the whole project backward.

---

## 7. When to Use / Not Use

| Use when                                  | Do not use when                            |
| ----------------------------------------- | ------------------------------------------ |
| Bring back an older version of one file   | Undo the whole last commit                 |
| A single file was changed too much        | Move the entire project to an older commit |
| Important code was deleted from one file  | Current changes are unsaved and needed     |
| You don't want to reset the whole project | —                                          |

**<mark>For <u>the whole project</u></mark>**, later lessons cover `git revert` (safe undo) and `git reset` (move the branch pointer).

---

## 8. Finding the Correct Commit

```bash
git log --oneline
```

```
c3d4e5f Update to version 3
b2c3d4e Update to version 2
a1b2c3d Add version 1
```

**<mark><u>Pick</u> the commit that contains <u>the version</u> you want</mark>**. **<mark><u>Inspect it first</u> if unsure</mark>**:

```bash
git show a1b2c3d
```

---

## 9. Extra Example — Restore a Deleted File

```bash
git log --oneline
git restore --source=a1b2c3d helper.cpp
git add helper.cpp
git commit -m "Restore helper file"
```

The **<mark>file <u>comes back</u> into the Working Directory</mark>** and is preserved as a new commit.

---

## 10. Combining with `--amend` (Important Pattern)

A common scenario: **<mark>you just made a commit, and <u>you realize</u> one file in it is an older version <u>you want to replace</u></mark>**. Instead of creating a new commit to undo the file, you can restore and amend in one flow:

```bash
# 1. Restore the file from an older commit into the Working Directory
git restore --source=<old-commit-id> <file-name>

# 2. Stage the restored version
git add <file-name>

# 3. Amend the last commit — the staged file replaces the version in it
git commit --amend --no-edit
```

Result: **<mark><u>the last commit</u> now contains <u>the restored file</u></mark>**, and history shows no extra commit. The last commit's ID changes (a new commit with a new hash replaces the old one).

| Step                              | What changes                                      |
| --------------------------------- | ------------------------------------------------- |
| `git restore --source=... <file>` | Working Directory: file replaced with old version |
| `git add <file>`                  | Staging Area: old version staged                  |
| `git commit --amend --no-edit`    | Last commit replaced: contains the old version    |

> **<mark>Reminder</mark>:** `--amend` **<mark><u>does not</u></mark>** edit the last commit — it **<mark><u>replaces</u></mark>** it with a new commit that has a new commit ID. If that commit was already pushed, use `git push --force-with-lease` (**<mark>only on a branch <u>no one else</u> uses</mark>**).

---

## 11. Common Mistakes

| Mistake                                              | Reality                                                                                              |
| ---------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| "Restoring from an old commit deletes newer commits" | No — newer commits stay; only the file is copied                                                     |
| "It moves the whole project back"                    | No — only the specified file changes                                                                 |
| "I don't need to check history first"                | Restoring from the wrong commit is easy — always check `git log --oneline` and `git show <id>` first |
| "I can just amend without staging"                   | You must `git add` the restored file first; otherwise `--amend` will not pick it up                  |

---

## 12. Wrap-up

```bash
git restore --source=<commit-id> <file-name>
```

- Does not move the whole project back.
- Copies one file from an older commit into the Working Directory.
- The file appears as modified — stage and commit to keep it.
- Can be combined with `--amend` to fix the **last commit** without creating an extra one.

> **<mark>Use it when you want <u>one file from the past</u></mark>, not the whole project.**

Next: `git reset` — soft vs mixed vs hard.

---

📌 **Summary**

| Question                                         | Answer                                                                |
| ------------------------------------------------ | --------------------------------------------------------------------- |
| What does `git restore --source=<id> <file>` do? | Copies that file from the specified commit into the Working Directory |
| Does it move the whole project?                  | No — only the specified file                                          |
| Does it delete newer commits?                    | No — history stays intact                                             |
| What does `git status` show after?               | The file as `modified`                                                |
| How to keep the restored version?                | `git add` then `git commit`                                           |
| How to fix the last commit instead?              | `git add <file>` then `git commit --amend --no-edit`                  |
| Does `--amend` preserve the commit ID?           | No — it creates a new commit with a new hash                          |
| How to find the commit ID?                       | `git log --oneline`, inspect with `git show <id>`                     |
| Extra use case                                   | Restore a file that was accidentally deleted                          |
| The rule                                         | Use for one file from the past — not for moving the whole project     |
