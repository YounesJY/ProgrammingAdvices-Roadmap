# Reset Basics

## 1. Introduction

Sometimes you commit something and realize:

- You committed too early.
- You want to undo the last commit but keep the code.
- You want to remove the commit and delete the changes completely.

Git uses `git reset` for this. **<mark>It can be <u>safe or dangerous</u>, depending on <u>the mode</u></mark>**.

---

## 2. The Concept

`git reset` **<mark>moves your current branch <u>backward</u> to an <u>older commit</u></mark>**.

The important question is: **<mark><u>what happens</u> <u>to the changes</u> after moving back?</mark>**

There are three modes:

| Mode                     | What happens to the changes                               |
| ------------------------ | --------------------------------------------------------- |
| `git reset --soft <id>`  | Kept **<mark>staged</mark>**                              |
| `git reset --mixed <id>` | Kept **<mark>unstaged</mark>** (in the Working Directory) |
| `git reset --hard <id>`  | **<mark>Deleted</mark>** entirely                         |

`--mixed` is **<mark><u>the default</u></mark>**: `git reset <id>` = `git reset --mixed <id>`.

---

## 3. The Warning

> **`git reset --hard` <mark><u>can delete your work</u></mark>.**

Before using it, always check:

```bash
git status
git log --oneline
```

> **If you are not sure, <mark><u>do not</u> use</mark> `--hard`.**

---

## 4. Mental Model

Your history is a stack of saved papers. Each commit is one paper.

```
Commit 3
Commit 2
Commit 1
```

`git reset` moves you from Commit 3 back to Commit 2. Then Git asks: **what should happen to Commit 3's changes?**

- `--soft`: keep them ready to commit
- `--mixed`: keep them in your files, not ready to commit
- `--hard`: throw them away

A useful phrasing from the comments:

| Mode                 | Analogy                                                                     |
| -------------------- | --------------------------------------------------------------------------- |
| `--soft` / `--mixed` | Going back in time but **<mark><u>still remembering</u> the future</mark>** |
| `--hard`             | Going back in time and **<mark><u>forgetting</u> the future</mark>**        |

---

## 5. Demo Setup

### Version 1

```cpp
std::cout << "Version 1" << std::endl;
```

```bash
git add main.cpp
git commit -m "Add version 1"
```

### Version 2

```cpp
std::cout << "Version 2" << std::endl;
```

```bash
git add main.cpp
git commit -m "Update to version 2"
```

### Version 3

```cpp
std::cout << "Version 3" << std::endl;
```

```bash
git add main.cpp
git commit -m "Update to version 3"
```

### History

```bash
git log --oneline
```

```
c3d4e5f Update to version 3
b2c3d4e Update to version 2
a1b2c3d Add version 1
```

We will reset back to `b2c3d4e` (version 2).

---

## 6. `git reset --soft`

> **<mark>But there is a catch:</mark>** `git reset --soft` can go back further than 1 commit.
> If you do `git reset --soft HEAD~3`, it undoes the last 3 commits and **<mark><u>combines</u> all of their changes into the Staging Area <u>at once</u></mark>**. It doesn't just go back one step.
> 
> **<mark><u>However</u></mark>**, in the specific case of `HEAD~1`, yes — it is literally the exact snapshot of your Staging Area right before you hit Enter on that commit.

> **<mark>Pending topic</mark>:** What happens to the Staging Area (Index) when doing `git reset --soft HEAD~n` (n > 1) — specifically how multiple commits' changes are combined (squashed) into a single staged state, how tree/commit IDs are handled, and what happens if files were created and then deleted across those commits.

```bash
git reset --soft b2c3d4e
```

Result:

![](/home/unisjy/.config/marktext/images/2026-11-25-17-35-55-image.png)

```bash
git status
```

```
Changes to be committed:
  modified: main.cpp
```

- Version 3 commit **<mark><u>removed from history</u></mark>**.
- Version 3 change **<mark><u>staged</u></mark>**, ready to commit again.

**Use when:**

- You **<mark>committed <u>too early</u></mark>**.
- You want to rewrite the last commit message.
- You want **<mark><u>everything staged again</u></mark>**.

---

## 7. `git reset --mixed`

```bash
git reset --mixed b2c3d4e
```

(or simply `git reset b2c3d4e` — this is the default)

Result:

<img title="" src="file:///home/unisjy/.config/marktext/images/2026-11-25-17-40-49-image.png" alt="" data-align="inline" width="942">

```bash
git status
```

```
Changes not staged for commit:
  modified: main.cpp
```

- Version 3 commit **<mark><u>removed from history</u></mark>**.
- Version 3 change **<mark><u>kept</u> in the file</mark>**.
- **<mark><u>Not staged</u></mark>**.

**Use when:**

- You want the changes back in the Working Directory.
- You want to **<mark><u>review before</u> staging again</mark>**.
- You want to **<mark><u>split</u> one commit <u>into smaller ones</u></mark>**.

---

## 8. `git reset --hard`

```bash
git reset --hard b2c3d4e
```

Result:

![](/home/unisjy/.config/marktext/images/2026-11-25-17-57-17-image.png)

```bash
git status
```

```
nothing to commit, working tree clean
```

Opening `main.cpp` shows "Version 2". **<mark>Version 3 <u>is gone</u> from the file</mark>**.

- Version 3 commit **<mark><u>removed</u></mark>**.
- Changes **<mark>deleted</mark>** from **<mark><u>both</u></mark>** Working Directory and Staging Area.

**Use only when:**

- You are sure **<mark>you<u> do not need</u> the changes.</mark>**
- You **<mark>want the project to exactly <u>match an older commit</u>.</mark>**

---

## 9. Comparison

![https://uploads.teachablecdn.com/attachments/c122d9c4d2fb469d826156c421707e2d.png](https://uploads.teachablecdn.com/attachments/c122d9c4d2fb469d826156c421707e2d.png)

| Mode      | Commit removed? | Staged?              | In file?                   |
| --------- | --------------- | -------------------- | -------------------------- |
| `--soft`  | Yes             | **<mark>Yes</mark>** | Yes                        |
| `--mixed` | Yes             | No                   | Yes                        |
| `--hard`  | Yes             | No                   | **<mark><u>No</u></mark>** |

### Memory trick from the comments

| Mode      | First letter  | Matches          |
| --------- | ------------- | ---------------- |
| `--soft`  | **S**         | **S**taged       |
| `--mixed` | **M** → **W** | **W**orking tree |

> ***<mark><u>S</u>oft is <u>S</u>taged. <u>M</u>ixed is <u>W</u>orking Tree.</mark>***

---

## 10. When to Use / Not Use

| Use `git reset` when                       | Do not use when                                               |
| ------------------------------------------ | ------------------------------------------------------------- |
| Moving the branch back                     | You do not know where your changes are                        |
| Undoing recent commits                     | You are guessing                                              |
| You understand what happens to the changes | The commit was shared with others                             |
| The commits are local only                 | You only want one file from the past (use `restore --source`) |

For undoing a shared commit safely, use `git revert` (next lesson).

---

## 11. Common Mistake

**<mark>Using `git reset --hard` <u>just because</u> Git <u>feels confusing</u></mark>**. It can remove current work.

Better habit:

```bash
git status
git log --oneline
```

**<mark>Then <u>decide carefully</u></mark>**.

For smaller operations:

| Goal                    | Command                       |
| ----------------------- | ----------------------------- |
| Unstage a file          | `git restore --staged <file>` |
| Discard one file change | `git restore <file>`          |

**<mark><u>Do not</u></mark> use `reset` for every small mistake.**

---

## 12. The Reflog Safety Net (From the Comments)

**<mark>Even after</mark>** `git reset --hard`, the deleted commits are **<mark><u>not gone immediately</u></mark>**. Git keeps them **<mark>in the reflog <u>for a while</u></mark>** (default 90 days).

```bash
git reflog
```

Example output:

```
c3d4e5f HEAD@{0}: reset: moving to b2c3d4e
c3d4e5f HEAD@{1}: commit: Update to version 3
b2c3d4e HEAD@{2}: commit: Update to version 2
a1b2c3d HEAD@{3}: commit: Add version 1
```

To restore the "lost" version 3:

```bash
git reset --hard HEAD@{1}
```

> **`reset --hard` <mark><u>is not</u> permanent</mark> — `reflog` <mark><u>can bring</u> the commit back</mark>.** This is not a substitute for care, but it is a genuine safety net.

---

## 13. Quick Practice / Mini Challenge

**Scenario:** You made a commit with message `"update"`, then realized the message is bad. You want to undo the commit but keep changes staged.

**Answer:**

```bash
git reset --soft HEAD~1
git commit -m "Update greeting message"
```

---

## 14. What Is `HEAD~1`?

`HEAD~1` means **<mark><u>one</u> commit <u>before</u> the current commit</mark>**.

| Syntax   | Meaning            |
| -------- | ------------------ |
| `HEAD`   | Current commit     |
| `HEAD~1` | One commit back    |
| `HEAD~2` | Two commits back   |
| `HEAD~3` | Three commits back |

`git reset --soft HEAD~1` = undo the last commit, keep changes staged.

---

## 15. Wrap-up

| Command                  | Effect                                  |
| ------------------------ | --------------------------------------- |
| `git reset --soft <id>`  | Removes commits, keeps changes staged   |
| `git reset --mixed <id>` | Removes commits, keeps changes unstaged |
| `git reset --hard <id>`  | Removes commits, deletes changes        |

> **<mark>Be <u>very careful</u></mark> with `git reset --hard`. It can delete your current changes — though `git reflog` can often recover them.**

Next: undoing a shared commit safely with `git revert`.

---

📌 **Summary**

| Question                                             | Answer                                                                       |
| ---------------------------------------------------- | ---------------------------------------------------------------------------- |
| What does `git reset` do?                            | Moves the current branch backward to a target commit                         |
| What does `--soft` do?                               | Removes commits, keeps changes staged                                        |
| What does `--mixed` do?                              | Removes commits, keeps changes unstaged (default)                            |
| What does `--hard` do?                               | Removes commits, deletes changes from files                                  |
| Memory trick                                         | **S**oft = **S**taged; **M**ixed = **W**orking tree                          |
| Which is dangerous?                                  | `--hard`                                                                     |
| How to undo the last commit but keep changes staged? | `git reset --soft HEAD~1`                                                    |
| What is `HEAD~1`?                                    | One commit before the current                                                |
| Is `--hard` permanent?                               | Not immediately — `git reflog` can restore, then `git reset --hard HEAD@{n}` |
| When to use reset?                                   | Moving a branch back on local, unshared history                              |
| When not to use?                                     | Shared commits (use `revert`), one file only (use `restore --source`)        |
