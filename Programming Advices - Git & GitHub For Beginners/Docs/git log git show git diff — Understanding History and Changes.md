# git log, git show, git diff — Understanding History and Changes

## 1. Introduction

Commits are created. Now: **<mark>how do you understand what happened in the project</mark>?** Git is not only for writing commits — it is for reading them.

You need to answer:

- What commits exist?
- What changed in each commit?
- What changed before committing?
- What is staged and ready to commit?
- What is still unstaged in the working directory?

Three commands cover this: `git log`, `git show`, `git diff`.

---

## 2. The Concept

| Command    | Purpose                                   |
| ---------- | ----------------------------------------- |
| `git log`  | Shows the list of commits                 |
| `git show` | Shows the details of one commit           |
| `git diff` | Shows the exact differences between files |

Simple idea:

> `git log` tells you **<mark>what commits happened</mark>**.
> `git show` opens **<mark>one commit</mark>** and shows what it saved.
> `git diff` compares changes **<mark>before/after</mark>** they are committed.

---

## 3. Mental Model: CCTV Recording Room

Every commit is a saved recording of the project at a moment in time.

| Command    | Analogy                                                 |
| ---------- | ------------------------------------------------------- |
| `git log`  | The recording list — all saved moments in order         |
| `git show` | Opening one recording — what happened inside one commit |
| `git diff` | Comparing the live view with a saved recording          |

Questions answered:

| Command    | Question                                 |
| ---------- | ---------------------------------------- |
| `git log`  | What saved versions exist?               |
| `git show` | What happened inside this saved version? |
| `git diff` | What changed right now?                  |

---

## 4. Connected Demo (C++)

Starting file `main.cpp`:

```cpp
#include <iostream>
#include <string>

void printGreeting(const std::string& user)
{
    std::cout << "Welcome, " << user << "!\n";
}

int main()
{
    printGreeting("Mohammed");
    return 0;
}
```

Assume this is already committed. History:

```
a1b2c3d Initial C++ greeting program
```

### Step 1 — View history

```bash
git log --oneline
```

```
a1b2c3d Initial C++ greeting program
```

Each line = one commit: short ID + message.

### Step 2 — Inspect one commit

```bash
git show a1b2c3d
```

```
commit a1b2c3d
Author: Mohammed <email@example.com>
Date: ...

Initial C++ greeting program

diff --git a/main.cpp b/main.cpp
new file mode 100644
--- /dev/null
+++ b/main.cpp
@@ -0,0 +1,12 @@
+#include <iostream>
+...
```

Because the file is new, every line starts with `+`. `/dev/null` means the file did not exist before.

### Step 3 — Make a change

Change:

```cpp
std::cout << "Welcome, " << user << "!\n";
```

to:

```cpp
std::cout << "Welcome to the C++ Demo, " << user << "!\n";
```

Change is uncommitted.

### Step 4 — Check status

```bash
git status
```

```
Changes not staged for commit:
  modified: main.cpp
```

`git status` tells you **<mark>the file changed</mark>** — **<mark><u>not</u></mark>** **<mark>which line</mark>**. For that, use `git diff`.

### Step 5 — See the exact unstaged change

```bash
git diff
```

```
diff --git a/main.cpp b/main.cpp
index 4f3a21b..8c9d123 100644
--- a/main.cpp
+++ b/main.cpp
@@ -3,7 +3,7 @@
 void printGreeting(const std::string& user)
 {
-    std::cout << "Welcome, " << user << "!\n";
+    std::cout << "Welcome to the C++ Demo, " << user << "!\n";
 }
```

`-` = removed from old version. `+` = added in new version. Compares **<mark>working directory vs last saved version</mark>**.

### Step 6 — Stage the change

```bash
git add main.cpp
```

### Step 7 — Run `git diff` again

```bash
git diff
```

**No output.** Because `git diff` shows **<mark>unstaged</mark>** changes by default — and the change is now staged.

> Important beginner point: after `git add`, plain `git diff` **<mark>is silent</mark>**.

### Step 8 — See the staged change

```bash
git diff --staged
```

Shows the same diff — but now it is what **<mark>will be saved in the next commit</mark>**.

| Command             | Shows            |
| ------------------- | ---------------- |
| `git diff`          | Unstaged changes |
| `git diff --staged` | Staged changes   |

### Step 9 — Commit

```bash
git commit -m "Update greeting message"
```

```
[master e5f6g7h] Update greeting message
 1 file changed, 1 insertion(+), 1 deletion(-)
```

### Step 10 — Updated history

```bash
git log --oneline
```

```
e5f6g7h Update greeting message
a1b2c3d Initial C++ greeting program
```

Newest on top.

### Step 11 — Inspect the new commit

```bash
git show e5f6g7h
```

Confirms what was saved inside the commit.

---

## 5. Practical Example 2 — Add a New Function

Add:

```cpp
void printGoodbye(const std::string& user)
{
    std::cout << "Goodbye, " << user << "!\n";
}
```

And call it in `main`:

```cpp
int main()
{
    printGreeting("Mohammed");
    printGoodbye("Mohammed");
    return 0;
}
```

`git diff` shows:

```
@@ -6,9 +6,15 @@ void printGreeting(const std::string& user)
     std::cout << "Welcome to the C++ Demo, " << user << "!\n";
 }

+void printGoodbye(const std::string& user)
+{
+    std::cout << "Goodbye, " << user << "!\n";
+}
+
 int main()
 {
     printGreeting("Mohammed");
+    printGoodbye("Mohammed");
     return 0;
 }
```

Stage and commit:

```bash
git add main.cpp
git commit -m "Add goodbye message"
```

History:

```
k9l8m7n Add goodbye message
e5f6g7h Update greeting message
a1b2c3d Initial C++ greeting program
```

---

## 6. Practical Example 3 — Compare Two Commits

```bash
git diff a1b2c3d e5f6g7h
```

Compares old commit (`a1b2c3d`) with newer (`e5f6g7h`). Answers: **<mark>what changed between these two points in history?</mark>**

---

## 7. Practical Example 4 — Show Only Commit Names

```bash
git log --oneline
```

Clean list of commits.

---

## 8. Practical Example 5 — Show More Details in History

```bash
git log
```

Includes full commit ID, author, date, message.

---

## 9. Practical Example 6 — Show One Commit Without Full Diff

```bash
git show --stat e5f6g7h
```

```
 main.cpp | 2 +-
 1 file changed, 1 insertion(+), 1 deletion(-)
```

Tells you **which file** changed and **how many lines** — without showing the full code change.

---

## 10. More Uses of git diff

### Example 1 — One file only (unstaged)

```bash
git diff -- main.cpp
```

Ignores changes in other files.

### Example 2 — One file only (staged)

```bash
git add main.cpp
git diff --staged -- main.cpp
```

`--staged` and `--cached` mean the same thing here.

### Example 3 — Compare two commits

```bash
git diff a1b2c3d e5f6g7h
```

### Example 4 — One file between two commits

```bash
git diff a1b2c3d e5f6g7h -- main.cpp
```

Answers: what changed in `main.cpp` between these two commits?

### Example 5 — Working directory vs last commit

```bash
git diff
```

The normal use: what did I change before staging?

### Example 6 — Staging area vs last commit

```bash
git diff --staged
```

What will be saved in the next commit?

### Example 7 — File names only

```bash
git diff --name-only
```

```
main.cpp
helpers.cpp
```

### Example 8 — Short summary

```bash
git diff --stat
```

```
main.cpp | 2 +-
1 file changed, 1 insertion(+), 1 deletion(-)
```

---

## 11. Quick Summary Table

| Command                                | Purpose                      |
| -------------------------------------- | ---------------------------- |
| `git diff`                             | Unstaged changes             |
| `git diff --staged`                    | Staged changes               |
| `git diff -- main.cpp`                 | Unstaged changes, one file   |
| `git diff --staged -- main.cpp`        | Staged changes, one file     |
| `git diff a1b2c3d e5f6g7h`             | Compare two commits          |
| `git diff a1b2c3d e5f6g7h -- main.cpp` | One file between two commits |
| `git diff --name-only`                 | Changed file names only      |
| `git diff --stat`                      | Short summary of changes     |

> `git diff` does not save changes. It only inspects and compares.

---

## 12. Simple Comparison

| Command    | Question it answers        |
| ---------- | -------------------------- |
| `git log`  | What commits were created? |
| `git show` | What did this commit save? |
| `git diff` | What changed in the file?  |

---

## 13. Common Beginner Confusion

| Confusion                                | Correction                                                                      |
| ---------------------------------------- | ------------------------------------------------------------------------------- |
| `git log` shows uncommitted changes      | No — use `git status` / `git diff`                                              |
| `git show` checks unsaved work           | No — it inspects saved commits                                                  |
| `git diff` saves changes                 | No — it only compares                                                           |
| `git diff` shows nothing after `git add` | Correct — use `git diff --staged`                                               |
| `git status` and `git diff` are the same | `status` says **which files** changed; `diff` says **what changed inside them** |

---

## 14. The Final Rule

> **`git log` — view the history.**
> **`git show` — inspect one saved commit.**
> **`git diff` — compare exact changes.**

---

## 15. Wrap-up

You can now read Git history:

| Question                          | Command             |
| --------------------------------- | ------------------- |
| What commits exist?               | `git log`           |
| What happened inside one commit?  | `git show`          |
| What exactly changed in the file? | `git diff`          |
| What will be committed next?      | `git diff --staged` |

You are no longer just creating commits — you are reading and understanding project history.

Next: best practices for renaming or moving files in Git.

---

📌 **Summary**

| Question                                                           | Answer                                                       |
| ------------------------------------------------------------------ | ------------------------------------------------------------ |
| What does `git log` show?                                          | The list of commits                                          |
| What does `git show` show?                                         | Details of one commit (info + diff)                          |
| What does `git diff` show?                                         | Exact differences between files                              |
| What is the difference between `git diff` and `git diff --staged`? | Unstaged vs staged changes                                   |
| Why does `git diff` show nothing after `git add`?                  | The change is now staged — use `--staged`                    |
| How to compare two commits?                                        | `git diff <old> <new>`                                       |
| How to diff one file only?                                         | `git diff -- <file>`                                         |
| How to see only file names?                                        | `git diff --name-only`                                       |
| How to see a summary?                                              | `git diff --stat`                                            |
| `git status` vs `git diff`?                                        | `status` = which files; `diff` = what changed inside them    |
| The final rule?                                                    | `log` = history; `show` = one commit; `diff` = exact changes |
