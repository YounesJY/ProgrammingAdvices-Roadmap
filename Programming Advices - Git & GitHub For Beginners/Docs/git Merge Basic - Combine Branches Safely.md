# git Merge (Basic) — Combine Branches Safely

## 1. Introduction

A branch lets you **<mark>work on a new idea</mark>** without damaging the main branch. After the idea is finished, an important question remains:

> **<mark>How do we <u>bring the work from one branch back into another</u>?</mark>**

This is where `git merge` comes in.

---

## 2. The Concept

`git merge` combines changes from one branch into another branch.

The usual workflow:

1. Start from `main`.
2. Create a new branch.
3. Work on the new branch.
4. When the work is ready, go back to `main`.
5. Merge the branch into `main`.

> **<mark>Merge means</mark>: take the work from another branch and bring it into the branch you are currently standing on.**

---

## 3. Demo

### Step 1 — Make sure you are on main

```bash
git switch main
git branch
```

```
* main
  feature-login
```

The `*` means you are on `main`.

### Step 2 — Merge the feature branch

```bash
git merge feature-login
```

This means: *"Take the changes from `feature-login` and add them into my current branch."* Since you are on `main`, they merge into `main`.

### Step 3 — Check the result

```bash
git status
```

```
On branch main
nothing to commit, working tree clean
```

Merge complete, no pending changes.

### Step 4 — Check the history

```bash
git log --oneline
```

```
a7c3d21 Merge branch 'feature-login'
b8f91aa Add login function
c2d44ef Initial commit
```

The work from `feature-login` is now part of `main`.

---

## 4. Practical Example (C++)

Starting file:

```cpp
#include <iostream>

int main()
{
    std::cout << "Welcome to the app!" << std::endl;
    return 0;
}
```

Create a branch:

```bash
git switch -c feature-greeting
```

Edit the file:

```cpp
#include <iostream>

void printGreeting()
{
    std::cout << "Hello from the new feature!" << std::endl;
}

int main()
{
    std::cout << "Welcome to the app!" << std::endl;
    printGreeting();
    return 0;
}
```

Stage and commit:

```bash
git add main.cpp
git commit -m "Add greeting feature"
```

Return to `main`:

```bash
git switch main
```

Merge:

```bash
git merge feature-greeting
```

The greeting feature is now part of `main`.

---

## 5. The Most Important Rule

> **`git merge` <mark>always <u>merges into</u> the branch <u>you are currently standing on</u></mark>.**

```bash
git merge feature-greeting
```

does **<mark><u>not</u></mark>** mean *"Move me to `feature-greeting`."*

It means *"Bring `feature-greeting` into my current branch."*

**<mark><u>Always</u> check where you are <u>before merging</u></mark>**:

```bash
git branch
# or
git status
```

---

## 6. Fast-Forward Merge

A **<mark><u>fast-forward</u></mark>** happens when `main` **<mark><u>has not changed</u></mark>** while you were working on the feature branch.

```bash
git merge feature-greeting
```

```
Fast-forward
main.cpp | 5 +++++
1 file changed, 5 insertions(+)
```

Git **<mark><u>simply moves</u> `main` forward</mark>** to include the new commits. No conflict, **<mark><u>no</u> special merge commit <u>needed</u></mark>**.

```
Before:   A --- B  (main)
                \
                 C --- D  (feature-greeting)

After:    A --- B --- C --- D  (main, feature-greeting)
```

---

## 7. Merge Commit (The 3-Way Merge)

Sometimes both branches have new commits. Git **<mark><u>cannot</u> simply slide</mark>** a pointer forward, so it creates a **<mark>merge commit</mark>**.

```
          C --- D  (main)
         /
A --- B
         \
          E --- F  (feature-greeting)
```

To combine them, Git **<mark><u>must look</u> at three commits</mark>**:

| #   | Source                     | Role     |
| --- | -------------------------- | -------- |
| 1   | Common ancestor `B`        | Baseline |
| 2   | `main` tip `D`             | "Ours"   |
| 3   | `feature-greeting` tip `F` | "Theirs" |

This three-way comparison is the **<mark><u>3-way merge</u></mark>** algorithm. When it succeeds, Git produces a new commit `M` with **<mark>two parents</mark>** (`D` and `F`):

```
          C --- D --- M  (main)
         /           /
A --- B            /
         \        /
          E --- F  (feature-greeting)
```

`M` is the **<mark><u>merge commit</u></mark>**. The message is usually:

```
Merge branch 'feature-greeting'
```

---

## 8. The 3-Way Merge vs Merge Commit Confusion

A common question: *"Isn't what you call a merge commit actually the 3-way merge?"*

Both terms are correct — they describe different things.

| Term             | What it describes                                                                | Category                         |
| ---------------- | -------------------------------------------------------------------------------- | -------------------------------- |
| **3-way merge**  | The **<mark><u>algorithm</u></mark>** Git runs: compare ancestor + ours + theirs | **<mark>Mechanism</mark>** (how) |
| **Merge commit** | The **<mark><u>result</u></mark>**: a new commit with two parents                | **<mark>Artifact</mark>** (what) |

They are not alternatives. The 3-way merge is the **<mark>process</mark>**; the merge commit is the **<mark>product</mark>**.

| Statement                                   | Correct?         | Why                                                              |
| ------------------------------------------- | ---------------- | ---------------------------------------------------------------- |
| "A 3-way merge creates a merge commit."     | Yes              | The mechanism **<mark>produces</mark>** the artifact             |
| "Every merge commit is from a 3-way merge." | In practice, yes | Fast-forward merges **<mark>do not</mark>** create merge commits |
| "A 3-way merge is a type of commit."        | No               | It is **<mark>an algorithm</mark>**, not a commit                |

**Summary of the relationship:**

> **3-way merge (algorithm) → produces → merge commit (artifact).**

The terms get used interchangeably because, in practice, whenever the branches have diverged, all three happen together: fast-forward is impossible → Git uses the 3-way algorithm → the result is a merge commit.

---

## 9. Comparison: Fast-Forward vs 3-Way Merge

|                     | Fast-Forward                                                                      | 3-Way Merge                                                             |
| ------------------- | --------------------------------------------------------------------------------- | ----------------------------------------------------------------------- |
| When                | Target branch **<mark><u>hasn't</u> moved</mark>** since the branch point         | **<mark><u>Both</u></mark>** branches have **<mark>new commits</mark>** |
| What Git does       | **<mark><u>Slides</u></mark>** the target branch **<mark>pointer forward</mark>** | Compares ancestor + ours + theirs                                       |
| New commit created? | No                                                                                | **<mark><u>Yes</u></mark>** — a merge commit (two parents)              |
| History shape       | **<mark><u>Linear</u></mark>**                                                    | **<mark><u>Branched</u></mark>** (diamond)                              |

---

## 10. What if There Is a Conflict?

A conflict happens when Git cannot automatically decide which change to keep.

Example — both branches changed the same line differently:

| Branch             | Line                                          |
| ------------------ | --------------------------------------------- |
| `main`             | `std::cout << "Welcome to the main app!"`     |
| `feature-greeting` | `std::cout << "Welcome to the greeting app!"` |

When you merge, Git **<mark>stops and reports a <u>conflict</u></mark>**.

> **A conflict <mark><u>does not</u> mean Git is broken</mark>. It means Git <mark>needs your help <u>to choose the correct version</u>.</mark>**

Conflicts are covered in a separate lesson.

---

## 11. Deleting the Branch After Merge

After the branch is merged, you can delete it **<mark><u>if</u> it is <u>no longer needed</u></mark>**:

```bash
git branch -d feature-greeting
```

This deletes the branch safely only if it has already been merged.

> **<mark>It <u>does not</u> delete the work</mark>.** The work is already inside `main`. **<mark><u>Only</u> the branch label is removed</mark>**.

---

## 12. Common Mistake

**<mark>Merging while <u>standing on the wrong branch</u>.</mark>**

```bash
git switch feature-greeting
git merge main
```

This brings `main` **<mark>into</mark>** `feature-greeting`. Usually, when the feature is finished, you want the opposite:

```bash
git switch main
git merge feature-greeting
```

Always ask:

1. Where am I standing?
2. Which branch do I want to bring in?

---

## 13. Basic Merge Workflow

```bash
git switch main
git merge feature-branch
git status
git log --oneline
```

Optional cleanup:

```bash
git branch -d feature-branch
```

---

## 14. Wrap-up

`git merge` combines work from one branch into another.

> **Git merges into the branch you are currently on.**

Before merging, always check your current branch.

- **Fast-forward**: `main` hasn't moved → pointer slides forward, no merge commit.
- **3-way merge**: both branches diverged → Git compares ancestor + ours + theirs → creates a **merge commit** with two parents.

If the feature is finished and merged, delete the branch safely with `git branch -d`.

Next: what happens when Git cannot merge automatically, and how to resolve merge conflicts.

---

📌 **Summary**

| Question                                   | Answer                                                        |
| ------------------------------------------ | ------------------------------------------------------------- |
| What does `git merge` do?                  | Combines changes from another branch into the current branch  |
| Which branch does it merge into?           | The one you are currently standing on                         |
| How to check before merging?               | `git branch` or `git status`                                  |
| What is a fast-forward merge?              | Git moves the current branch pointer forward; no merge commit |
| When does fast-forward happen?             | When the target branch hasn't moved since the branch point    |
| What is a 3-way merge?                     | The algorithm comparing ancestor + ours + theirs              |
| What is a merge commit?                    | The commit with two parents produced by a 3-way merge         |
| Are 3-way merge and merge commit the same? | No — one is the mechanism, the other is the artifact          |
| When is a merge commit created?            | When both branches have new commits since divergence          |
| What does a conflict mean?                 | Git cannot decide automatically and needs human input         |
| How to delete a merged branch?             | `git branch -d <name>` (only if merged)                       |
| Does deleting a branch delete the work?    | No — the work lives in `main`; only the label is removed      |
| Common mistake                             | Merging while on the wrong branch                             |
| The core rule                              | Git merges into the branch you are currently on               |
