# Undo Working Directory Changes

## 1. Introduction

**<mark>Sometimes you <u>edit a file</u> and realize <u>the change is wrong</u></mark>**. Or you were testing something and don't want to keep it.

You may have:

- Written bad code
- Deleted something by mistake
- Changed the wrong file
- Tried an idea and decided against it

Git can undo these changes — **<mark><u>only</u> if the mistake is still <u>in the Working Directory</u></mark>**. This lesson covers undoing before staging and before committing.

---

## 2. The Concept

When you edit a file, Git notices the change. At this point, the change is only in the **<mark>Working Directory</mark>**.

- Not staged.
- Not committed.

**<mark>The change is <u>still temporary</u></mark>**.

~~To remove it and **<mark>bring the file back to the <u>last committed version</u></mark>**:~~

> **<mark>Correction:</mark>** `git restore` pulls from the **<mark>Index (Staging Area)</mark>**.

```bash
git restore <file-name>
```

Example:

```bash
git restore main.cpp
```

**<mark><u>This tells Git</u></mark>**: *"Bring `main.cpp` back to how it looked in the last commit."*

---

## 3. The Very Important Warning

> **`git restore <file>` <mark>removes <u>your current changes</u> from that file</mark>.**

**<mark><u>If</u></mark>** you wrote new code and did not save it anywhere else, **<mark><u>Git will remove</u></mark>** it.

Before using this command, **<mark><u>always</u> ask</mark>**: *Do I really want to throw away these changes?*

It is **<mark><u>safe only when</u></mark>** you are sure the current file change is a mistake.

---

## 4. Mental Model: The Notebook

You are still writing in your notebook. You write something, look at it, and say: *"No, this is wrong."* You erase the page and return it to the last clean version.

That is what `git restore` does:

> *"Forget my current edits in this file. Bring back the last saved version from Git."*

---

## 5. Demo

### Step 1 — Start with a committed file

```cpp
// main.cpp
#include <iostream>

int main()
{
    std::cout << "Hello Git" << std::endl;
    return 0;
}
```

The file is already committed. `git status` shows:

```
nothing to commit, working tree clean
```

### Step 2 — Make a wrong change

```cpp
std::cout << "Wrong message" << std::endl;
```

`git status` shows:

```
modified: main.cpp
```

The change is only in the Working Directory — not staged, not committed.

### Step 3 — Review before undoing

```bash
git diff
```

```
-    std::cout << "Hello Git" << std::endl;
+    std::cout << "Wrong message" << std::endl;
```

The old line was removed; the new line was added. Now you know exactly what you are about to undo.

### Step 4 — Restore the file

```bash
git restore main.cpp
```

Open the file — the wrong message is gone. It is back to the committed version.

### Step 5 — Check status again

```bash
git status
```

```
nothing to commit, working tree clean
```

The unwanted change is removed.

---

## 6. When to Use It

| Situation                            | Use `git restore <file>`?           |
| ------------------------------------ | ----------------------------------- |
| Changed a file by mistake            | Yes                                 |
| Want to cancel current edits         | Yes                                 |
| Want the last committed version back | Yes                                 |
| File is modified but not staged      | Yes                                 |
| Still need the changes               | **No**                              |
| Not sure what changed                | **No** — run `git diff` first       |
| Already staged                       | **No** — use `git restore --staged` |
| Already committed                    | **No** — use history undo commands  |

For staged files:

```bash
git restore --staged <file-name>
```

Covered in the next lesson.

---

## 7. Quick Practice

1. Edit `main.cpp`: change `"Hello Git"` to `"Testing mistake"`.
2. Run `git status`.
3. Run `git diff`.
4. Run `git restore main.cpp`.
5. Check the file — it returns to the last committed version.

---

## 8. Common Mistake

Running `git restore main.cpp` **<mark><u>without checking</u> the change first</mark>**.

Better habit:

```bash
git diff
git restore main.cpp
```

**<mark>Review <u>first</u>, <u>then</u> restore.</mark>**

---

## 9. Commenters' Insights (Important Additions)

### 9.1 What `git restore` actually restores to

> *"When you run `git restore`, it goes back to the last existing commit."*

**<mark>Precisely</mark>**: it **<mark>restores the file ~~<u>to the version in</u> `HEAD`~~</mark>** — the commit the current branch points to. It **<mark>compares the current file <u>against</u> that snapshot</mark>** and removes the extra modifications.

> **<mark>Correction:</mark>** `git restore` pulls from the **<mark>Index (Staging Area)</mark>**.

### 9.2 Why it fails on untracked files

Because the untracked file **<mark><u>does not exist in</u> `HEAD`</mark>**. There is nothing to restore it to. `git restore` only operates on **<mark>tracked</mark>** files.

| File state                 | `git restore <file>` behavior                                                        |
| -------------------------- | ------------------------------------------------------------------------------------ |
| Tracked, modified          | Reverts to `HEAD` version                                                            |
| Tracked, staged + modified | Reverts **working tree** to `HEAD` (staged version remains until `--staged` is used) |
| **Untracked**              | Fails — no `HEAD` version to restore to                                              |

### 9.3 Removing untracked files: `git clean`

**<mark>To delete <u>untracked files</u> from the working directory in one shot</mark>**:

```bash
git clean -fd
```

| Flag | Meaning                                                          |
| ---- | ---------------------------------------------------------------- |
| `-f` | Force (required, since this permanently deletes untracked files) |
| `-d` | Also remove untracked **<mark>directories</mark>**               |

> **<mark>Warning</mark>:** `git clean` **<mark>is <u>destructive and irreversible</u></mark>**. Untracked files have no commit history. **<mark><u>Before running</u>, preview with</mark>**:
> 
> ```bash
> git clean -nd
> ```
> 
> `-n` = **<mark><u>dry run</u></mark>**, lists what would be deleted without deleting.

---

## 10. Wrap-up

The Working Directory is where current file edits live. If a file is modified but not staged and not committed, the change is still in the Working Directory.

To throw it away:

```bash
git restore <file-name>
```

> **Warning:** this removes uncommitted changes from that file. Always check with `git diff` first.

For untracked files, `git restore` does not apply — use `git clean -fd` (carefully).

---

📌 **Summary**

| Question                                 | Answer                                                                                                 |
| ---------------------------------------- | ------------------------------------------------------------------------------------------------------ |
| What does `git restore <file>` do?       | Reverts the file to the version in ~~`HEAD` (last commit)~~ **<mark>the Index (Staging Area)</mark>**. |
| Is this destructive?                     | Yes — uncommitted changes in that file are lost                                                        |
| How to review before restoring?          | `git diff`                                                                                             |
| Does it work on untracked files?         | No — they have no `HEAD` version                                                                       |
| How to remove untracked files?           | `git clean -fd` (preview with `-n`)                                                                    |
| What about staged files?                 | Use `git restore --staged <file>` (next lesson)                                                        |
| What if the change is already committed? | Use history undo commands (`--amend`, `revert`, `reset`)                                               |
| The safe habit                           | `git diff` → `git restore <file>`                                                                      |
