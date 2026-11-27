# Understanding the Index, HEAD, and Diff Comparisons

## 1. The Problem

Terminal output like `git diff`, `git status`, and `git diff --staged` is confusing until the mental model of the index is exact. The output only makes sense once you know **<mark>what the index holds at every moment</mark>** and **<mark>what each command compares</mark>**.

This note fixes the model and shows what **<mark><u>each command actually reads</u></mark>**.

---

## 2. The Three Structures

| Structure                | What it is                                              | How many            | Changes on                        |
| ------------------------ | ------------------------------------------------------- | ------------------- | --------------------------------- |
| **Working tree**         | Your files on disk                                      | One, right now      | Every save                        |
| **Index** (staging area) | Flat list: path → blob hash — the next commit candidate | One, always present | `git add`, `git restore --staged` |
| **HEAD**                 | A pointer to a commit                                   | One pointer         | commit, checkout, reset           |

History lives in **<mark>commits</mark>**, linked by parents. The index is **<mark><u>not</u></mark>** a history and **<mark><u>not</u></mark>** a copy of HEAD. It is its own independent snapshot.

"Index", "staging area", and "cache" are **<mark><u>three names</u> for the <u>same structure</u></mark>**: `.git/index` on disk. `--staged` and `--cached` are the same flag.

---

## 3. Does a Commit Clear the Index?

**<mark><u>No</u></mark>.** A commit:

1. Reads the index.
2. Writes its content as a **<mark><u>tree object</u></mark>** (immutable).
3. Creates a **<mark><u>commit object</u></mark>** pointing to that tree and the previous commit.
4. Moves HEAD to the new commit.

The index is **<mark><u>not modified</u></mark>**. It stays full. Immediately after the commit, index content equals HEAD's tree — which is why both diffs are silent. The index was never emptied.

---

## 4. What Each Command Compares

| Command                | Compares                                     | Shows                                       |
| ---------------------- | -------------------------------------------- | ------------------------------------------- |
| `git diff`             | Working tree vs index                        | Unstaged changes                            |
| `git diff --staged`    | HEAD's tree vs index                         | Staged changes                              |
| `git status`           | Runs **<mark>both</mark>** comparisons       | Both sections at once                       |
| `git ls-files --stage` | **<mark>Reads <u>the index only</u></mark>** | Index entries: mode, blob hash, stage, path |

`git status` is not one comparison — it is **<mark>two</mark>**, presented in one view:

| `git status` section          | Comparison                                 |
| ----------------------------- | ------------------------------------------ |
| Changes to be committed       | Index vs HEAD                              |
| Changes not staged for commit | Working tree vs index                      |
| Untracked files               | Present in working tree, absent from index |

Each diff is **<mark>bidirectional</mark>**: `-` lines come from one side, `+` lines from the other. It is not "only what the working tree has."

---

## 5. How to See the Index Directly

`git diff --staged` shows **<mark>differences</mark>**. To see the index's **<mark><u>actual entries</u></mark>**, use:

```bash
git ls-files --stage
```

Output format:

```
<mode> <blob-hash> <stage> <path>
```

Example:

```
100644 ea20428b9cc0cbcaae805d925f27a986ecc70b09 0       index.html
100644 11d8cfd00d3a6f4826615c059f1260205309a4da 0       notes.html
```

This is the direct read of the index: which paths it tracks and which blob each points to.

| Command                | Answers                                                                  |
| ---------------------- | ------------------------------------------------------------------------ |
| `git diff --staged`    | "What changes are about to be committed?"                                |
| `git ls-files --stage` | "What is in the index right now, and what blob does each path point to?" |

Useful `ls-files` filters:

| Flag                | Meaning                            |
| ------------------- | ---------------------------------- |
| `-s` / `--stage`    | Show mode, blob hash, stage number |
| `-o` / `--others`   | Untracked files                    |
| `-m` / `--modified` | Modified in working tree           |
| `-d` / `--deleted`  | Deleted in working tree            |

---

## 6. The State Machine

| Moment                              | WT vs index | Index vs HEAD     | `git diff`    | `git diff --staged` |
| ----------------------------------- | ----------- | ----------------- | ------------- | ------------------- |
| After commit                        | equal       | equal             | silent        | silent              |
| After editing a file                | differ      | equal             | **non-empty** | silent              |
| After `git add <file>`              | equal       | differ            | silent        | **non-empty**       |
| After `git commit`                  | equal       | equal             | silent        | silent              |
| After `git restore --staged <file>` | differ      | equal (that file) | **non-empty** | silent (that file)  |

**<mark><u>The rules</u></mark>**:

- `git diff` silent ⇔ working tree = index
- `git diff --staged` silent ⇔ index = HEAD's tree

---

## 7. Walkthrough: The Rename Case

The session log, step by step.

### 7.1 Start

```
git status  → nothing to commit, working tree clean
ls          → index.html  notes.html
```

All three agree. `notes.html` is in HEAD, index, and working tree.

### 7.2 `mv notes.html project-notes.txt`

Rename **on disk only**.

| Structure    | `notes.html`  | `project-notes.txt`     |
| ------------ | ------------- | ----------------------- |
| HEAD         | present       | absent                  |
| Index        | still present | still absent            |
| Working tree | **gone**      | **present (untracked)** |

`git status`:

```
Changes not staged for commit:
        deleted:    notes.html
Untracked files:
        project-notes.txt
```

`git ls-files --stage` still shows `notes.html` in the index — `mv` never touched it.

### 7.3 `git add project-notes.txt` — only the addition staged

| Structure    | `notes.html`      | `project-notes.txt` |
| ------------ | ----------------- | ------------------- |
| HEAD         | present           | absent              |
| Index        | **still present** | **now present**     |
| Working tree | gone              | present             |

`git ls-files --stage`:

```
100644 ea20428b…  index.html
100644 11d8cfd0…  notes.html
100644 11d8cfd0…  project-notes.txt
```

The index now has **<mark>both</mark>** paths pointing to the **<mark><u>same blob</u></mark>** `11d8cfd0…`.

`git status`:

```
Changes to be committed:
        new file:   project-notes.txt       ← index vs HEAD: addition
Changes not staged for commit:
        deleted:    notes.html              ← WT vs index: deletion
```

`git diff --staged --summary`:

```
create mode 100644 project-notes.txt
```

Only an **<mark>add</mark>**. No rename — because the index still matches HEAD for `notes.html`. Rename detection needs a **<mark><u>pair</u></mark>** (one deletion + one addition with the same blob hash in the same comparison). Only one half exists here.

### 7.4 `git add notes.html` — the deletion staged

`git add` on a deleted file stages the deletion.

`git ls-files --stage`:

```
100644 ea20428b…  index.html
100644 11d8cfd0…  project-notes.txt
```

`notes.html` is **<mark><u>gone</u> from the index</mark>**. `project-notes.txt` is present, pointing to the same blob.

`git diff --staged --summary`:

```
rename notes.html => project-notes.txt (100%)
```

The diff now sees:

- HEAD: `notes.html` present, `project-notes.txt` absent
- Index: `notes.html` gone, `project-notes.txt` present with blob `11d8cfd0…`
- One deletion, one addition, identical blob hash → **<mark><u>pair</u> → <u>rename</u></mark>**

---

## 8. Why Rename Detection Needs Both Halves

Rename detection is **<mark><u>post-processing on a diff</u></mark>**, not a comparison between two named files:

1. Compute the diff (deletions, additions).
2. For each deletion and each addition, compare blob hashes.
3. If identical (or similarity above threshold), present them as one `rename` entry.

If only the addition is in the index, there is no deletion in that comparison to pair with. If only the deletion is staged, there is no addition. **<mark><u>Both halves</u></mark>** must be in the **<mark>same comparison</mark>** (HEAD vs index) for the pairing to happen.

Git's storage is always "old path gone, new path added." The `rename` label is the reader-friendly view.

---

## 9. The Rule

> **The index is a full snapshot of the next commit, always present. A commit reads it, writes it as a tree, and moves HEAD — it never clears the index.**
> 
> **`git diff` = WT vs index. `git diff --staged` = HEAD vs index. `git status` shows both. `git ls-files --stage` <mark><u>reads the index directly</u></mark>.**
> 
> **<mark>Rename detection pairs <u>a delete and an add with matching content</u>, both present in the same comparison.</mark>**

---

📌 **Summary**

| Question                                                       | Answer                                                                        |
| -------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| Is the index cleared by a commit?                              | No — HEAD moves to a commit whose tree equals the index                       |
| What does `git diff` compare?                                  | Working tree vs index                                                         |
| What does `git diff --staged` compare?                         | HEAD's tree vs index                                                          |
| What does `git status` compare?                                | Both — that's why it has staged and unstaged sections                         |
| How to read the index directly?                                | `git ls-files --stage`                                                        |
| Why did the rename not appear after staging only the addition? | Rename detection needs both the delete and the add in the same comparison     |
| Why did `git add notes.html` trigger the rename?               | It staged the deletion, completing the pair                                   |
| What is the storage truth of a rename?                         | Always delete + add; `rename` is a display label                              |
| Are index and staging area different?                          | No — `.git/index`; `--staged` = `--cached`                                    |
| The core rule?                                                 | Index = next commit snapshot; `diff` = WT↔index; `diff --staged` = index↔HEAD |
