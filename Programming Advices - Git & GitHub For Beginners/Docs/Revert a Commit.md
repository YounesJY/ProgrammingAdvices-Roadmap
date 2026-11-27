# Revert a Commit

## 1. Introduction

You made a commit and later discover:

- It introduced a bug.
- It added the wrong feature.
- It changed something that should not have been changed.

You **<mark>want to <u>undo the effect</u></mark>** of that commit **<mark><u>without deleting history</u></mark>**.

Git provides **<mark><u>a safe</u></mark>** command for this:

```bash
git revert <commit-id>
```

**<mark>It <u>does not erase</u> the old commit</mark>**. It creates a **<mark><u>new commit</u> that <u>cancels the changes</u></mark>** from the old one.

---

## 2. The Concept

| Command      | What it does to history                                                                                       |
| ------------ | ------------------------------------------------------------------------------------------------------------- |
| `git reset`  | **<mark><u>Moves the branch</u></mark>** backward; **<mark>removes commits</mark>** from visible history      |
| `git revert` | **<mark><u>Keeps all history</u></mark>**; adds a new commit that **<mark><u>reverses an old one</u></mark>** |

`git revert` says:

> *Keep the old commit. Add a new commit that undoes its changes.*

The history stays clear and safe.

---

## 3. The Key Idea

> **`git revert` <mark><u>does not</u> delete the commit.</mark>**

It adds a new commit that says: *"Undo the changes from that earlier commit."*

This is why `git revert` **<mark>is <u>safer than</u></mark>** `git reset` when the commit **<mark>was <u>already shared</u></mark>**. Everyone can see that **<mark>a mistake happened and <u>how it was corrected</u></mark>**.

---

## 4. Mental Model: The Notebook

History is a notebook where every page is recorded.

If one page has a mistake, `git revert` does not rip the page out. It adds a new page that says: *"Correction — cancel the changes from that earlier page."*

The notebook still shows what happened, and it also shows how the mistake was fixed.

---

## 5. Demo

### Step 1 — Version 1

```cpp
std::cout << "Version 1" << std::endl;
```

```bash
git add main.cpp
git commit -m "Add version 1"
```

### Step 2 — A bad change

```cpp
std::cout << "Broken version" << std::endl;
```

```bash
git add main.cpp
git commit -m "Add broken message"
```

### Step 3 — View history

```bash
git log --oneline
```

```
b2c3d4e Add broken message
a1b2c3d Add version 1
```

The bad commit is `b2c3d4e`.

### Step 4 — Revert it

```bash
git revert b2c3d4e
```

Git opens an editor with the default message:

```
Revert "Add broken message"
```

Save and close. Git creates a new commit that undoes the bad one.

### Step 5 — Check the file

`main.cpp` is back to "Version 1". The broken message is gone.

### Step 6 — Check history

```bash
git log --oneline
```

```
Revert "Add broken message"
b2c3d4e Add broken message
a1b2c3d Add version 1
```

The bad commit is **<mark><u>still there</u></mark>**, with a new commit above it that cancels it.

---

## 6. The Critical Detail: Revert Inverts All Changes

This is the most important thing to remember — and the point the commenter emphasized:

> **<mark>Revert <u>inverts ALL additions and deletions</u> made by that specific commit.</mark>**

It takes the commit you point to and applies the **<mark><u>exact opposite</u></mark>** of every line it added or removed. It does this as a new commit *on top* of your current state.

### 6.1 What it means for Additions

If the target commit **<mark>added</mark>** a line, revert **<mark><u>removes</u></mark>** that line.

### 6.2 What it means for Deletions

If the target commit **<mark>deleted</mark>** a line, revert **<mark><u>re-adds</u></mark>** that line.

### 6.3 The catch: It reverts the WHOLE commit

You cannot tell `git revert` to "only undo this one line from that commit." It will undo **<mark><u>everything</u></mark>** that commit did.

> **If you only want to undo *some* of the changes from a commit, <mark><u>do not use</u></mark> `revert` directly.** It will blindly reverse the entire commit — additions and deletions alike. 

**<mark>If you need to undo <u>only part of a commit</u></mark>**, you must either:

- Revert the commit, then manually edit the file to re-apply the parts you wanted to keep, and amend that revert commit.
- Or use `git revert -n` (no-commit), adjust the staged changes manually before committing.

---

## 7. Revert vs Reset

|                          | `git reset`                              | `git revert`                              |
| ------------------------ | ---------------------------------------- | ----------------------------------------- |
| History                  | Moves backward                           | Stays intact                              |
| Commit removed?          | **<mark><u>Yes</u></mark>**              | No                                        |
| New commit created?      | No                                       | Yes                                       |
| Safe for shared commits? | No                                       | **<mark><u>Yes</u></mark>**               |
| What it does             | **<mark><u>Rewrites</u> history</mark>** | **<mark><u>Adds a correction</u></mark>** |

> **`reset` = move history backward. `revert` = add a correction commit.**

---

## 8. When to Use / Not Use

| Use `git revert` when                       | Do not use when                                               |
| ------------------------------------------- | ------------------------------------------------------------- |
| Undoing a commit safely                     | Changes are only in the Working Directory (use `git restore`) |
| Keeping history clear                       | Changes are only staged (use `git restore --staged`)          |
| The commit is already shared                | Fixing the last commit message (use `--amend`)                |
| Everyone should see the mistake and its fix | You want to rewrite local history (use `git reset`)           |
| Undoing **one specific** commit             | You only want to undo **part** of a commit                    |

---

## 9. Extra Examples

### 9.1 Revert the last commit

```bash
git revert HEAD
```

`HEAD` means the current commit — the one you are standing on. **<mark>Reverts <u>the latest commit</u></mark>**.

### 9.2 Revert an older commit

History:

```
d4e5f6g Add final message
c3d4e5f Add broken feature
b2c3d4e Add helper function
a1b2c3d Add version 1
```

```bash
git revert c3d4e5f
```

**<mark>Git tries to undo <u>only the changes</u> from <u>that commit</u></mark>**. If later commits changed the same lines, Git **<mark><u>may</u> ask you <u>to resolve a conflict</u></mark>** — that is normal.

---

## 10. Handling Conflicts During Revert

A revert conflict happens when:

- The commit being reverted changed a line.
- Later commits also changed the same line.
- Git cannot decide automatically.

**<mark><u>Resolution</u> steps</mark>**:

```bash
# open the conflicted file, choose the correct code
git add <file-name>
git revert --continue
```

**<mark><u>To cancel</u> the revert</mark>**:

```bash
git revert --abort
```

---

## 11. Common Mistakes

| Mistake                                   | Reality                                                                          |
| ----------------------------------------- | -------------------------------------------------------------------------------- |
| "`git revert` goes back to an old commit" | No — it does not move you backward; it adds a new canceling commit               |
| "`git revert` deletes history"            | No — the old commit remains visible                                              |
| "`git revert` can undo part of a commit"  | No — it inverts the **<mark>entire</mark>** commit (all additions and deletions) |

---

## 12. Wrap-up

`git revert <commit-id>` keeps the old commit and creates a new commit that cancels its changes.

It is the safe way to undo a committed mistake — especially a shared one — because it preserves the full story: what was added, what was wrong, and how it was corrected.

> **`reset` moves history backward. `revert` adds a correction commit.**

**<mark><u>Practice resources</u> from the lesson:</mark>**

- **<mark>Undo & Fix visualizer</mark>**: `https://aburakab.github.io/courses/git-areas-visualizer.html`
- **<mark>Revert conflicts visualizer</mark>**: `https://aburakab.github.io/courses/git-revert-conflicts.html`

Next: a full practice lesson on fixing different mistakes step by step.

---

📌 **Summary**

| Question                                      | Answer                                                                   |
| --------------------------------------------- | ------------------------------------------------------------------------ |
| What does `git revert <id>` do?               | Creates a new commit that cancels the specified commit's changes         |
| Does it delete the old commit?                | No — it stays in history                                                 |
| Safe for shared commits?                      | Yes                                                                      |
| Difference from `reset`?                      | `reset` rewrites history; `revert` adds a correction                     |
| Does it revert *all* changes from the commit? | Yes — every addition and deletion is inverted                            |
| Can it undo only part of a commit?            | No — use `revert -n` and edit manually if needed                         |
| How to revert the latest commit?              | `git revert HEAD`                                                        |
| What if a conflict occurs?                    | Fix the file, `git add`, `git revert --continue`                         |
| How to cancel a revert in progress?           | `git revert --abort`                                                     |
| The core rule                                 | Revert keeps history, inverts the whole commit, and creates a new commit |
