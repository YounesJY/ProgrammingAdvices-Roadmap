# Quick Reference — Undoing Mistakes

**⚠️ <mark>Correction Applied:</mark>** The original course text states that `git restore <file>` restores from the "last committed version" (`HEAD`). As established in our deep dive, **<mark>this is <u>technically inaccurate</u>.</mark>** `git restore` pulls from the **<mark>Index (Staging Area)</mark>**. If the file is unstaged, the Index matches `HEAD`, so the result *looks* the same. But if you have staged changes, `git restore` will pull the staged version back into your Working Directory, not the last commit. The notes below have been corrected to reflect the true mechanics. (If you want to force a restore directly from `HEAD`, bypassing the Index, use `git restore --source=HEAD <file>`).

---

## 1. The Core Philosophy

The most important idea is not memorizing commands. It is knowing **<mark><u>where</u> the mistake happened</mark>**.

Before using any undo command, ask:

- Is it in the Working Directory?
- Is it in the Staging Area?
- Is it in the last commit?
- Is it in an older commit?
- Is it already shared history?

When you know where the mistake happened, choosing the correct command becomes much easier.

> **<mark><u>Understand first</u>. Undo second.</mark>**

---

## 2. The Safety Checks

**<mark><u>Before</u> undoing anything</mark>**, **<mark><u>always run</u></mark>**:

```bash
git status
```

This tells you:

- Which files are modified.
- Which files are staged.
- Which files are untracked.
- Whether the working tree is clean.

**<mark><u>To see exactly what changed</u></mark>**, use:

| Command                | What it shows                                 |
|:---------------------- |:--------------------------------------------- |
| `git diff`             | Unstaged changes (Working Directory vs Index) |
| `git diff --staged`    | Staged changes (Index vs HEAD)                |
| `git log --oneline`    | Short commit history                          |
| `git show <commit-id>` | Details of a specific commit                  |

---

## 3. Command Decision Guide

### 3.1 The Mistake is in the Working Directory (Not Staged)

You edited a file but haven't run `git add`.

```bash
git restore <file-name>
```

- **What it does:** Overwrites the Working Directory with the version currently in the **<mark><u>Index</u></mark>**. (**<mark>If you <u>haven't staged anything</u></mark>**, this effectively reverts to the last commit).
- **Keeps changes?** **<mark>No</mark>**, it deletes your unstaged edits.
- **Rewrites history?** **<mark>No</mark>**.

### 3.2 The Mistake is in the Staging Area (Staged, Not Committed)

You ran `git add`, but you don't want this file in the next commit.

```bash
git restore --staged <file-name>
```

- **What it does:** Reverts the Index **<mark>to match `HEAD`</mark>**. It unstages the file.
- **Keeps changes?** **<mark><u>Yes</u></mark>**, your edits remain in the Working Directory.
- **Rewrites history?** **<mark>No</mark>**.

*(**<mark>Note</mark>**: If this is a brand-new repo with no commits yet, `HEAD` doesn't exist and this command **<mark><u>fails</u></mark>**. Use `git rm --cached <file-name>` instead).*

### 3.3 The Mistake is in the Last Commit Message

You committed, but the message is wrong.

```bash
git commit --amend -m "Better message"
```

- **What it does:** Replaces the last commit with a new one.
- **Keeps changes?** **<mark><u>Yes</u></mark>**.
- **Rewrites history?** **<mark><u>Yes</u></mark>** (changes the commit ID).
- **Safe for shared history?** **<mark><u>Be careful</u></mark>**. If pushed, you must force-push.

### 3.4 You Forgot a File in the Last Commit

You committed, but left out a file.

```bash
git add <file-name>
git commit --amend --no-edit
```

- **What it does:** Adds the staged file to the previous commit without changing the message.
- **Rewrites history?** **<mark><u>Yes</u></mark>**.
- **Safe for shared history?** **<mark><u>Be careful</u></mark>**.

### 3.5 Need One File from an Old Commit

You want to bring back a single file's older version.

```bash
git restore --source=<commit-id> <file-name>
```

- **What it does:** Copies that file from the old commit directly into your Working Directory.
- **Keeps changes?** **<mark><u>Yes</u></mark>** (it doesn't move the whole project back).
- **Rewrites history?** **<mark>No</mark>**.
- **Safe for shared history?** **<mark><u>Yes</u></mark>**.

### 3.6 Undo a Local Commit (Keep Changes)

You committed too early and want to undo the commit, but keep your work.

| Command                    | Where do the changes go?                                          |
|:-------------------------- |:----------------------------------------------------------------- |
| `git reset --soft HEAD~1`  | Stay **<mark>staged</mark>** (ready to commit again immediately). |
| `git reset --mixed HEAD~1` | Go back to the **<mark>Working Directory</mark>** (unstaged).     |

- **Rewrites history?** **<mark><u>Yes</u></mark>** (moves the branch pointer).
- **Safe for shared history?** **<mark><u>No</u></mark>**.

### 3.7 Undo a Local Commit (Delete Changes)

You want to undo the commit and **<mark><u>throw the work away</u></mark>**.

```bash
git reset --hard HEAD~1
```

- **What it does:** Moves the branch back and overwrites the Working Directory to match the target commit.
- **Keeps changes?** **<mark><u>No</u></mark>**.
- **Rewrites history?** **<mark><u>Yes</u></mark>**.
- **Safe for shared history?** **<mark><u>No</u></mark>**.
- **⚠️ <mark>DANGER</mark>:** This deletes your uncommitted work. (Though `git reflog` can often recover it).

### 3.8 Undo a Commit Safely (Shared History)

You need to undo a committed mistake, but the commit is already pushed/shared.

```bash
git revert <commit-id>
```

- **What it does:** Creates a **<mark><u>new commit</u></mark>** that applies the exact opposite changes of the target commit.
- **Keeps history?** **<mark><u>Yes</u></mark>**, the bad commit remains visible.
- **Safe for shared history?** **<mark><u>Yes</u></mark>**.

*(If a conflict happens during a revert, fix the file, `git add <file>`, then `git revert --continue`. **<mark><u>To cancel</u></mark>**, `git revert --abort`).*

---

## 4. Quick Comparison Table

| Situation                     | Command                                    | Keeps Changes?     | Rewrites History? | Safe for Shared? |
|:----------------------------- |:------------------------------------------ |:------------------ |:----------------- |:---------------- |
| Edited wrong file (unstaged)  | `git restore <file>`                       | No                 | No                | Yes              |
| Staged wrong file             | `git restore --staged <file>`              | Yes                | No                | Yes              |
| Wrong last commit message     | `git commit --amend -m "..."`              | Yes                | Yes               | Be careful       |
| Forgot file in last commit    | `git add` + `git commit --amend --no-edit` | Yes                | Yes               | Be careful       |
| Need one file from old commit | `git restore --source=<id> <file>`         | Yes                | No                | Yes              |
| Undo commit, keep staged      | `git reset --soft HEAD~1`                  | Yes (staged)       | Yes               | No               |
| Undo commit, keep unstaged    | `git reset --mixed HEAD~1`                 | Yes (unstaged)     | Yes               | No               |
| Undo commit, delete changes   | `git reset --hard HEAD~1`                  | **No**             | Yes               | No               |
| Undo commit safely            | `git revert <id>`                          | Creates correction | No                | Yes              |

---

## 5. The Most Dangerous Commands

| Command              | Why it is dangerous                                                                   |
|:-------------------- |:------------------------------------------------------------------------------------- |
| `git reset --hard`   | Deletes uncommitted work in the Working Directory.                                    |
| `git commit --amend` | Rewrites the last commit. Causes conflicts **<mark><u>if already pushed</u></mark>**. |
| `git reset`          | Moves the branch pointer backward. **<mark>Rewrites local history</mark>**.           |

> **<mark><u>Rule of thumb:</u></mark>** The more powerful the undo command, the more careful you must be. Check `git status` and `git log --oneline` before pulling the trigger.

---

## 6. Final Memory Trick

- Working Directory mistake? → `git restore <file>`
- Staging Area mistake? → `git restore --staged <file>`
- Last commit mistake? → `git commit --amend`
- One file from the past? → `git restore --source=<id> <file>`
- Undo local commit and keep changes? → `git reset --soft` or `--mixed`
- Undo local commit and delete changes? → `git reset --hard`
- Undo committed change safely? → `git revert <id>`

---

## 7. Wrap-up

The goal is not to use the strongest command. The goal is to use the **<mark>correct</mark>** command.

Always start with `git status`. Then decide where the mistake happened. If it's in the Working Directory, use a Working Directory command. If it's in the Staging Area, use a Staging Area command. If it's in Commit History, choose carefully between `amend`, `reset`, `restore --source`, or `revert`. 

> **<mark><u>Check first</u>. <u>Understand first</u>. Undo second.</mark>**

---

📌 **Summary**

| Question                                        | Answer                                               |
|:----------------------------------------------- |:---------------------------------------------------- |
| How to start any undo?                          | `git status`                                         |
| How to undo an unstaged edit?                   | `git restore <file>`                                 |
| How to unstage a file?                          | `git restore --staged <file>`                        |
| How to fix the last commit message?             | `git commit --amend -m "..."`                        |
| How to add a forgotten file to the last commit? | `git add <file>` then `git commit --amend --no-edit` |
| How to get one file from an old commit?         | `git restore --source=<id> <file>`                   |
| How to undo a local commit but keep staged?     | `git reset --soft HEAD~1`                            |
| How to undo a local commit but keep unstaged?   | `git reset --mixed HEAD~1`                           |
| How to undo a local commit and delete changes?  | `git reset --hard HEAD~1` (dangerous)                |
| How to undo a shared commit safely?             | `git revert <id>`                                    |
