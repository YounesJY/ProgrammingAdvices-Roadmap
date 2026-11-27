# Fix the Last Commit (`git commit --amend`)

## 1. Introduction

You made a commit, **<mark>then realized</mark>**:

- The commit message is wrong.
- You forgot to add one file.
- You made a tiny mistake and don't want a new commit just for it.

Git lets you fix the last commit with:

```bash
git commit --amend
```

**<mark>*Amend* means <u>modify or improve something that already exists</u></mark>**. So `--amend` means: **<mark>fix the <u>last commit</u>.</mark>**

---

## 2. The Concept

Normally, `git commit -m "..."` creates a **<mark>new</mark>** commit in history.

`git commit --amend` **<mark><u>replaces</u></mark>** **<mark>the last commit with <u>a corrected version</u></mark>**.

> **<mark>It <u>does not</u> edit the old commit</mark>. <mark>It <u>creates a new commit</u> that <u>takes the place</u> of the previous one.</mark>**

---

## 3. The Critical Warning

> **<mark>Use `git commit --amend` <u>carefully</u>.</mark>**

- It changes the last commit.
- This is fine when **<mark>the commit is <u>only on your computer</u>.</mark>**
- **If the commit was already shared, changing it causes confusion** (the old commit's ID changes; anyone who pulled the old one now has diverging history).

The simple rule:

> **Use `--amend` <mark>to fix your most recent <u>local</u> commit</mark>. Do not use it on old commits, and do not use it if you are unsure what you are changing.**

---

## 4. Mental Model: Resubmitting Homework

You submit a page. Immediately you notice: the title is wrong, or you forgot an extra page.

If the teacher hasn't distributed it yet, you quickly replace it with a corrected version.

`--amend` says: **<mark>replace my last submitted snapshot with a corrected one.</mark>**

---

## 5. Demo 1 — Fix a Wrong Commit Message

### Step 1 — Clean repository

`git status` → `nothing to commit, working tree clean`.

### Step 2 — Make a change

Edit `main.cpp` and stage it:

```bash
git add main.cpp
```

### Step 3 — Commit with a wrong message

```bash
git commit -m "Fix bug"
```

But this did not fix a bug — it updated the greeting. **<mark>Wrong message</mark>**.

### Step 4 — Amend

```bash
git commit --amend -m "Update greeting message"
```

```bash
git log --oneline
```

```
Update greeting message
```

The old message is no longer the last commit message.

---

## 6. Demo 2 — Add a Forgotten File to the Last Commit

### Step 1 — Create a forgotten file

```cpp
// helper.cpp
#include <iostream>

void printHelperMessage()
{
    std::cout << "Helper file loaded" << std::endl;
}
```

`git status` shows it as untracked.

### Step 2 — Stage it

```bash
git add helper.cpp
```

### Step 3 — Amend without changing the message

```bash
git commit --amend --no-edit
```

`--no-edit` <mark>**means**</mark>: keep the same commit message.

### Step 4 — Verify

```bash
git status
```

```
nothing to commit, working tree clean
```

The forgotten file is now part of the last commit.

---

## 7. Two Amend Patterns

| Pattern                                                  | Command                                              | When                              |
| -------------------------------------------------------- | ---------------------------------------------------- | --------------------------------- |
| **<mark>Change <u>only the message</u></mark>**          | `git commit --amend -m "Better message"`             | Code is correct, message is wrong |
| **<mark><u>Add staged changes</u>, keep message</mark>** | `git add <file>` then `git commit --amend --no-edit` | Forgot a file; small correction   |

---

## 8. When to Use / Not Use

| Use `--amend` when                      | Do not use when                                           |
| --------------------------------------- | --------------------------------------------------------- |
| Fix the last commit message             | Mistake is not in the last commit                         |
| Forgot to add a file                    | You want to undo an older commit                          |
| Small correction right after committing | You want to remove many commits                           |
| Commit is still local                   | Commit was already shared and you're unsure of the effect |
| Want a cleaner last commit              | You only want to unstage a file                           |
| —                                       | You only want to discard a working-directory change       |

**<mark>For <u>older commits</u></mark>**, later lessons cover `git revert <commit-id>`.

---

## 9. Common Mistakes

### Mistake 1 — Thinking `--amend` edits any commit

It only fixes the **<mark>last</mark>** commit.

### Mistake 2 — Forgetting to stage before amending

```bash
git commit --amend --no-edit
```

With nothing staged, this **<mark><u>changes nothing</u></mark>** about the file contents — it just rewrites the commit (possibly with a different timestamp). To add a forgotten file, stage first:

```bash
git add helper.cpp
git commit --amend --no-edit
```

---

## 10. Platform Comments — Addressed

The comment section raised several important points worth preserving.

### 10.1 Question: Can we preserve the old commit ID by editing `.git` files?

A commenter asked whether it is possible to save the old commit ID, create a new commit, and then hand-edit files inside `.git` to force the new commit to use the old ID — keeping the chain intact even after sharing.

**Answer from the instructor (paraphrased and clarified):**

> **<mark>No</mark>.** A commit ID is a hash of the commit's content (tree, parent, author, message, timestamp). If any of those change, the ID changes. Editing files inside `.git/objects` to force an old ID is dangerous and practically impossible — the hash will no longer match the object it names, and Git will report corruption.

The deeper truth:

| Claim                                          | Reality                                                                                            |
| ---------------------------------------------- | -------------------------------------------------------------------------------------------------- |
| "We can edit `.git` to reuse an old ID"        | <mark>**Impossible**</mark> — the ID is derived from the content. Different content → different ID |
| "We can move a pointer to an old commit"       | **<mark>Yes</mark>** — that's what reset/branch does — but the commit itself is unchanged          |
| "We can force a new commit to carry an old ID" | **<mark>No</mark>** — the ID is not assignable                                                     |

This is **<mark>why</mark>** amending a shared commit causes problems: the new commit gets a new ID, and everyone who already pulled the old one now has a divergent history.

### 10.2 Question: Can I safely amend a pushed commit on an isolated branch?

**Instructor's answer:** Yes — **<mark>if you are the <u>only person working on that branch</u></mark>**, you can amend freely and force-push.

```bash
git push --force-with-lease
```

`--force-with-lease` **<mark>is <u>safer</u> than</mark>** `--force`: it refuses to overwrite the remote if someone else pushed to it since your last fetch.

| Option                        | Behavior                                                                                        |
| ----------------------------- | ----------------------------------------------------------------------------------------------- |
| `git push --force`            | Overwrites the remote branch **<mark><u>unconditionally</u></mark>**                            |
| `git push --force-with-lease` | Overwrites **<mark>only</mark>** if the remote **<mark>still matches what you last saw</mark>** |

> **<mark>Prefer `--force-with-lease`</mark>.** It prevents accidentally erasing someone else's work.

### 10.3 The editor problem: stuck in Vim

When you run `git commit --amend` without `-m`, Git opens your default editor. Many beginners get stuck in Vim.

**How to handle it:**

| Action                           | Keys               |
| -------------------------------- | ------------------ |
| Enter insert mode (start typing) | `i`                |
| Exit insert mode                 | `Esc`              |
| Save and quit                    | `:wq` then `Enter` |
| Quit without saving              | `:q!` then `Enter` |

Alternative: set a friendlier editor once.

```bash
git config --global core.editor "code --wait"
```

(or `"nano"`, `"notepad"`, etc.)

Or skip the editor entirely with `-m`:

```bash
git commit --amend -m "Corrected message"
```

---

## 11. Quick Practice

### Practice 1 — Fix message

```bash
# edit main.cpp
git add main.cpp
git commit -m "update"
git commit --amend -m "Update greeting message"
git log --oneline
```

### Practice 2 — Add forgotten file

```bash
# create helper.cpp
git add helper.cpp
git commit --amend --no-edit
git status
```

Clean working tree.

---

## 12. Wrap-up

`git commit --amend` fixes the **last commit only** — either its message or its contents (via staged changes).

| Goal                        | Command                                              |
| --------------------------- | ---------------------------------------------------- |
| Fix the message             | `git commit --amend -m "New message"`                |
| Add a forgotten staged file | `git add <file>` then `git commit --amend --no-edit` |

> **<mark>Use carefully</mark>, <mark>especially if the commit <u>was already pushed</u></mark>. <mark><u>Never assume</u></mark> the commit ID stays the same — it cannot.**

Next: restoring a file from an older commit with `git restore --source=<commit-id> <file>`.

---

📌 **Summary**

| Question                           | Answer                                                                   |
| ---------------------------------- | ------------------------------------------------------------------------ |
| What does `--amend` do?            | Replaces the last commit with a corrected version                        |
| Does it edit the old commit?       | No — it creates a new commit with a new ID in its place                  |
| How to fix the message?            | `git commit --amend -m "New message"`                                    |
| How to add a forgotten file?       | `git add <file>` then `git commit --amend --no-edit`                     |
| What does `--no-edit` mean?        | Keep the existing commit message                                         |
| Can I amend older commits?         | No — only the last one                                                   |
| Can I preserve the old commit ID?  | No — the ID is a hash of the content; changing content changes the ID    |
| Safe to amend a pushed commit?     | Only on a branch no one else uses, then `git push --force-with-lease`    |
| `--force` vs `--force-with-lease`? | `--force-with-lease` refuses if the remote changed since your last fetch |
| Stuck in Vim after amend?          | `i` to insert, `Esc`, `:wq` + Enter to save, `:q!` + Enter to quit       |
| The rule                           | `--amend` fixes the last commit only; the ID will change                 |
