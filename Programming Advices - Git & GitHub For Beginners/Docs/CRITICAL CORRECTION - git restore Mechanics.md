# CRITICAL CORRECTION: git restore Mechanics

**<mark><u>Read this first</u> to overwrite <u>the inaccurate mental model</u> from the course text.</mark>**

The course lesson "Undo Working Directory Changes" states that `git restore <file>` brings the file back to the **last commit (`HEAD`)**. 

**<mark>This is technically <u>inaccurate</u>.</mark>** 

`git restore <file>` pulls from the **<mark><u>Index (Staging Area)</u></mark>**, *not* `HEAD`. Because the index usually matches `HEAD` for unstaged files, the result *looks* the same. But **<mark>they are <u>mechanically different</u></mark>**. 

Here is the absolute, corrected ground truth.

---

## 1. The Three Areas

1. **Working Directory (WT):** Your actual files on disk.
2. **Index (Staging Area):** The buffer for the next commit.
3. **HEAD:** The last commit snapshot.

---

## 2. The True Comparisons

Git is always comparing exactly two of these three areas:

| Command             | Left Side | Right Side        | What it shows    |
|:------------------- |:--------- |:----------------- |:---------------- |
| `git diff`          | Index     | Working Directory | Unstaged changes |
| `git diff --staged` | HEAD      | Index             | Staged changes   |

---

## 3. The True `git restore` Mechanics

**<mark><u>The flag</u></mark>** you use determines **<mark><u>where</u> the data <u>comes from</u></mark>** and **<mark><u>where it goes</u></mark>**.

> **`--source` sets where the data comes from; `--staged` and `--worktree` set where it goes. The default source is the index, or `HEAD` when `--staged` is used.**

| Command                            | Source (From)                 | Destination (To)  | Effect                                                                                   |
|:---------------------------------- |:----------------------------- |:----------------- |:---------------------------------------------------------------------------------------- |
| `git restore <file>`               | **<mark><u>Index</u></mark>** | Working Directory | Discards WT edits; pulls the staged version back into your files.                        |
| `git restore --staged <file>`      | **<mark><u>HEAD</u></mark>**  | Index             | Unstages the file; Index goes back to matching the last commit.                          |
| `git restore --source=HEAD <file>` | **HEAD**                      | Working Directory | **Bypasses the Index completely.** Forcefully overwrites your file with the last commit. |
| `git restore --source=<id> <file>` | **Commit ID**                 | Working Directory | Overwrites your file with an old commit's version.                                       |

---

## 4. The Exact Scenarios (Why this matters)

#### Scenario A: No Staging Involved

* You edit a file.
* You run `git restore <file>`.
* **Result:** **<mark>It <u>looks like</u> it restored from `HEAD`</mark>**. Why? Because **<mark>you <u>didn't run</u> `git add`</mark>**, so the Index and `HEAD` **<mark><u>are identical</u></mark>**.

### Scenario B: You Staged Changes First

* You edit a file (e.g., to "Version 2").
* You run `git add <file>` (Index now has "Version 2").
* You edit the file *again* (e.g., to "Version 3").
* You run `git restore <file>`.
* **Result:** It restores to **<mark>"Version 2"</mark>** (from the Index), *not* to `HEAD`. Your later edit is gone, but your staged change remains.

### Scenario C: You Want to Force-Delete Everything to `HEAD`

* You edit a file.
* You run `git add <file>`.
* You run `git restore --source=HEAD <file>`.
* **Result:** **<mark>It pulls <u>directly from</u> `HEAD`</mark>**, **<mark><u>bypassing</u> the Index</mark>**. Your Working Directory reverts to the last commit, **<mark>but your Index <u>still holds the staged changes</u></mark>**.

---

## 5. Why the Course Text Was Confusing

The course says: *"git restore brings the file back to the last commit."* 
It says this because **90% of the time**, a beginner edits a file and immediately runs `git restore` **<mark>without staging</mark>**. Since **<mark>the Index <u>still matches</u> `HEAD`</mark>**, copying from the Index ***<mark><u>looks exactly the same</u></mark>*** as copying from `HEAD`. 

But **<mark><u>the moment you introduce</u></mark>** staging (`git add`), the Index and `HEAD` split apart. That is when the true mechanics matter.

---

## 6. The Golden Rule

> **`git restore` (without flags) <mark>reads from the Index</mark>.**
> **If you want to read directly from a commit, you must use `--source`.**

---

📌 **Summary**

| Question                                        | Answer                                                                    |
|:----------------------------------------------- |:------------------------------------------------------------------------- |
| Does `git restore <file>` restore from `HEAD`?  | **<mark><u>No</u></mark>.** It restores from the Index (Staging Area).    |
| When does it look like it restores from `HEAD`? | When the file is **<mark><u>unstaged</u></mark>** (Index matches `HEAD`). |
| How to restore directly from the last commit?   | `git restore --source=HEAD <file>`                                        |
| What does `git restore --staged <file>` do?     | Pulls from `HEAD` to overwrite the Index.                                 |
