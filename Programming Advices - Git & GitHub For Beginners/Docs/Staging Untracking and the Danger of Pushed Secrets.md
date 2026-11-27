# Staging, Untracking, and the Danger of Pushed Secrets

## 1. Introduction

This note covers **<mark>three critical Git concepts</mark>** that are often confused:

1. The difference between `git restore --staged` and `git rm --cached`.
2. Why `git restore --staged` fails in brand-new repositories.
3. Why deleting a pushed secret (like `.env`) with a new commit is an illusion, and what you must do instead.

---

## 2. The First Commit Problem: Why `HEAD` Matters

In Git, `HEAD` is a pointer to the **<mark>latest commit on your current branch</mark>**. 

When you run `git restore --staged <file>`, Git needs a reference point to know what "unstaged" means. It tries to pull the file's previous state from `HEAD`. 

If you just ran `git init`, created a file, and ran `git add <file>`, the file is in the Index, but **<mark>`HEAD` <u>does not</u> exist yet</mark>** because there are zero commits. Git **<mark><u>throws an error</u></mark>** (usually `fatal: could not resolve HEAD`).

### The Workaround

To unstage a file when `HEAD` does not exist, you must **<mark>tell Git to <u>stop tracking</u> the file in the Index <u>entirely</u></mark>**. You do this with:

```bash
git rm --cached <file>
```

---

## 3. `git restore --staged` vs `git rm --cached`

Both commands remove a file from the Index (Staging Area) and leave your physical file untouched on your hard drive. But **<mark><u>they behave differently</u> based on whether the file <u>has a history</u></mark>**.

| File State                            | `git restore --staged <file>`                                                                        | `git rm --cached <file>`                                                                |
|:------------------------------------- |:---------------------------------------------------------------------------------------------------- |:--------------------------------------------------------------------------------------- |
| **Brand new file** (No commits yet)   | **<mark><u>Fails</u></mark>** (No `HEAD` to pull from)                                               | **<mark>Works.</mark>** Removes it from the Index.                                      |
| **Tracked file** (Has commit history) | Unstages it. The Index reverts to matching `HEAD`. **<mark>The file<u> remains tracked</u></mark>**. | Removes it from the Index entirely. The file becomes **<mark><u>untracked</u></mark>**. |

### The Consequence of `git rm --cached` on Tracked Files

If a file is already tracked and committed, `git rm --cached <file>` removes it from the Index. When you make your next commit, **<mark>Git will <u>record a deletion</u></mark>**. 

**<mark>If you <u>push</u> this commit <u>to a remote</u></mark>**, the file will disappear from the Working Directory of everyone who pulls the repository. 

To stop tracking a file *without* deleting it for everyone else, you must:

1. `git rm --cached <file>`
2. Add the file to `.gitignore`
3. `git add .gitignore`
4. `git commit -m "Stop tracking <file> and add to gitignore"`

---

## 4. The Illusion of Deleting Pushed Secrets

If you accidentally commit and push a sensitive file (like `.env`), you might think the fix is to commit a deletion. 

> **<mark><u>This is a dangerous illusion.</u></mark>**

A deletion commit does not erase the file from history. It simply records that the file is gone *as of that specific commit*. The old commit still exists, and the file still exists inside it. 

Anyone who clones the repository can simply run:

```bash
git checkout <old-commit-id> -- .env
```

Or browse the commit history on GitHub, and they will have your secrets.

---

## 5. The Correct Workflow for Leaked Secrets

If a secret is pushed, you must follow these steps immediately:

### Step 1: Revoke and Rotate the Secret

**<mark><u>Assume the secret is compromised.</u></mark>** 
Do not wait. Change the database password, revoke the API key, generate a new JWT secret. Scrubbing Git history does not un-leak the past; it only prevents future leaks.

### Step 2: Remove the File from History (Optional, but recommended)

If you want to scrub the file from the repository's history so it doesn't sit there permanently, you must rewrite history using a tool like:

- **`git filter-repo`** (The modern standard)
- **BFG Repo-Cleaner**

Rewriting history changes the commit IDs for every commit after the leak. This means you must force-push, and every collaborator must delete their local copy and re-clone.

### Step 3: Prevent It from Happening Again

- Add `.env` to `.gitignore` immediately.
- Provide a `.env.example` template for other developers.
- Enable secret scanning/push protection on your remote platform (e.g., GitHub Advanced Security).

---

## 6. The Golden Rules

> **Never push secrets.**
> **If a secret is pushed, rotate it immediately.**
> **A deletion commit does not erase history.**
> **To unstage a file before the first commit, use `git rm --cached`.**

---

📌 **Summary**

| Question                                            | Answer                                                                                      |
|:--------------------------------------------------- |:------------------------------------------------------------------------------------------- |
| Why does `git restore --staged` fail in a new repo? | `HEAD` does not exist yet (no commits).                                                     |
| How to unstage a brand-new file?                    | `git rm --cached <file>`                                                                    |
| Difference between `--staged` and `--cached`?       | `--staged` reverts the Index to `HEAD`. `--cached` removes the file from tracking entirely. |
| What happens if you `rm --cached` a tracked file?   | The next commit records a deletion.                                                         |
| Does a deletion commit erase a secret from history? | No. The old commit still contains the file.                                                 |
| What is the first step after leaking a secret?      | Rotate/revoke it immediately.                                                               |
| How to scrub a secret from history?                 | `git filter-repo` or BFG Repo-Cleaner (requires force-pushing and re-cloning).              |
| The rule?                                           | Never push secrets. If you do, rotate first, scrub history second.                          |
