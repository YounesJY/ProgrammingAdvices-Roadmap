# Resolving Merge Conflicts — Step by Step

## 1. Introduction

Git merges changes automatically most of the time. But sometimes it stops and says:

> **"<mark>I <u>cannot decide</u> which version is correct.</mark>"**

This happens when two branches change the same part of the same file. This is a **<mark>merge conflict</mark>**.

This lesson resolves one conflict using a real C++ example.

---

## 2. The Concept

A merge conflict happens when Git **<mark>finds <u>two different changes</u> in the <u>same place</u></mark>**.

| Branch    | Line                                        |
| --------- | ------------------------------------------- |
| `main`    | `std::cout << "Hello from main branch";`    |
| `feature` | `std::cout << "Hello from feature branch";` |

**<mark>Git <u>cannot know</u> which one you want</mark>**. It pauses the merge and asks you to choose.

> Git does not delete your work.
> Git does not choose randomly.
> Git waits for you to fix the file manually.

---

## 3. Mental Model: Two Students, One Sentence

Two students edit the same sentence in a shared document.

| Student   | Writes                       |
| --------- | ---------------------------- |
| Student 1 | "Welcome to our C++ course." |
| Student 2 | "Welcome to our Git course." |

The teacher must decide: keep the first, keep the second, or combine both.

Git does the same:

> "I found two versions. Please choose the final one."

---

## 4. Demo — Create and Resolve a Conflict

### Goal

Create a repo, create a branch, make conflicting edits to the same line, merge, resolve the conflict, complete the merge.

### Step 1 — Create a project folder

```bash
mkdir GitConflictDemo
cd GitConflictDemo
```

### Step 2 — Initialize Git

```bash
git init
```

### Step 3 — Create a C++ file

```cpp
// main.cpp
#include <iostream>

int main()
{
    std::cout << "Welcome to our project." << std::endl;
    return 0;
}
```

### Step 4 — Check status

```bash
git status
```

Some Git versions create `main`, others create `master`. This lesson uses `main`. `main.cpp` is untracked.

### Step 5 — Stage and commit

```bash
git add main.cpp
git commit -m "Add initial C++ file"
```

### Step 6 — Create a branch

```bash
git switch -c feature-message
```

### Step 7 — Edit the file on the feature branch

Change:

```cpp
std::cout << "Welcome to our project." << std::endl;
```

to:

```cpp
std::cout << "Welcome to the feature branch." << std::endl;
```

### Step 8 — Commit the feature change

```bash
git add main.cpp
git commit -m "Update welcome message on feature branch"
```

### Step 9 — Switch back to main

```bash
git switch main
```

(use `git switch master` if your default branch is `master`)

### Step 10 — Edit the same line on main

Change:

```cpp
std::cout << "Welcome to our project." << std::endl;
```

to:

```cpp
std::cout << "Welcome to the main branch." << std::endl;
```

### Step 11 — Commit the main change

```bash
git add main.cpp
git commit -m "Update welcome message on main branch"
```

Both branches have now changed the same line differently.

### Step 12 — Try to merge

```bash
git merge feature-message
```

Git stops:

```
Auto-merging main.cpp
CONFLICT (content): Merge conflict in main.cpp
Automatic merge failed; fix conflicts and then commit the result.
```

### Step 13 — Check status

```bash
git status
```

```
You have unmerged paths.
  both modified:   main.cpp
```

### Step 14 — Open the conflicted file

```cpp
#include <iostream>

int main()
{
<<<<<<< HEAD
    std::cout << "Welcome to the main branch." << std::endl;
=======
    std::cout << "Welcome to the feature branch." << std::endl;
>>>>>>> feature-message
    return 0;
}
```

These markers are **not C++ code**. They are Git's way of showing both versions.

### What the markers mean

| Marker                    | Meaning                                               |
| ------------------------- | ----------------------------------------------------- |
| `<<<<<<< HEAD`            | Start of the version from the current branch (`main`) |
| `=======`                 | Separator between the two versions                    |
| `>>>>>>> feature-message` | End of the version from the branch being merged in    |

### Step 15 — Resolve the conflict

Three choices:

**Option 1 — Keep the main version:**

```cpp
std::cout << "Welcome to the main branch." << std::endl;
```

**Option 2 — Keep the feature version:**

```cpp
std::cout << "Welcome to the feature branch." << std::endl;
```

**Option 3 — Combine both:**

```cpp
std::cout << "Welcome to the main project with the new feature." << std::endl;
```

This lesson uses Option 3. We are not blindly choosing one side — we are creating the final correct version.

Final file:

```cpp
#include <iostream>

int main()
{
    std::cout << "Welcome to the main project with the new feature." << std::endl;
    return 0;
}
```

> **Important:** remove all conflict markers (`<<<<<<< HEAD`, `=======`, `>>>>>>> feature-message`). Leaving them breaks compilation.

### Step 16 — Stage the resolved file

```bash
git add main.cpp
```

This tells Git: *"The conflict in this file is resolved."*

### Step 17 — Complete the merge

```bash
git commit
```

Git opens an editor with a default message:

```
Merge branch 'feature-message'
```

Save and close — or write it inline:

```bash
git commit -m "Resolve merge conflict between main and feature message"
```

### Step 18 — Check the history

```bash
git log --oneline --graph
```

```
*   a1b2c3d Resolve merge conflict between main and feature message
|\
| * e5f6g7h Update welcome message on feature branch
* | c8d9e0f Update welcome message on main branch
|/
* 123abcd Add initial C++ file
```

Both branches were merged together. The conflict was resolved and Git created a merge commit.

### Step 19 — Check the final file

```cpp
#include <iostream>

int main()
{
    std::cout << "Welcome to the main project with the new feature." << std::endl;
    return 0;
}
```

This is the final version after resolution.

---

## 5. Common Mistakes

### Mistake 1 — Leaving conflict markers in the file

```cpp
<<<<<<< HEAD
std::cout << "Welcome to the main branch." << std::endl;
=======
std::cout << "Welcome to the feature branch." << std::endl;
>>>>>>> feature-message
```

**<mark>These <u>are not</u> valid C++</mark>**. Remove them.

### Mistake 2 — Forgetting to stage the resolved file

Running `git commit` without `git add` first **<mark>will fail</mark>** — Git still thinks the file is unresolved. `git add` is what tells Git the conflict is resolved.

### Mistake 3 — Thinking Git broke the file

**<mark>Git <u>did not</u> break anything</mark>**. It showed you both versions. You choose the final one.

### Mistake 4 — Choosing one side without reading

**<mark><u>Do not blindly</u> delete one side</mark>**. Read both. Sometimes the correct resolution is combining them.

---

## 6. The Bottom Line

A merge conflict is not an error to fear. It means:

> **Git found two different changes in the same place.**

To resolve:

1. `git status` — find the conflicted file.
2. Open the file.
3. Choose the final correct version.
4. Remove the conflict markers.
5. Stage the file: `git add <file>`
6. Complete the merge: `git commit`

---

## 7. Wrap-up

A merge conflict happens when two branches edit the same part of the same file.

We created a real conflict in a C++ file, saw the markers:

```
<<<<<<< HEAD
=======
>>>>>>> branch-name
```

and learned that `HEAD` means the current branch.

Most importantly:

> **Git does not solve every decision for you. When Git cannot decide, you choose the correct version.**

Next: how to avoid merge conflicts with better branch habits and regular merging.

---

📌 **Summary**

| Question                                          | Answer                                                       |
| ------------------------------------------------- | ------------------------------------------------------------ |
| What is a merge conflict?                         | Two branches changed the same part of the same file          |
| Does Git choose randomly?                         | No — it pauses and asks you                                  |
| What do `<<<<<<<`, `=======`, `>>>>>>>` mean?     | Start of current version, separator, end of incoming version |
| What does `HEAD` mean in the conflict?            | The branch you are currently on                              |
| How to resolve?                                   | Edit the file, remove markers, stage, commit                 |
| Why must you run `git add` after fixing?          | It tells Git the conflict is resolved                        |
| What if you leave markers in?                     | The code is broken — it will not compile                     |
| What does `git log --oneline --graph` show after? | A merge commit with both branch lines visible                |
| Should you always pick one side?                  | No — sometimes combining both is correct                     |
| The rule                                          | When Git cannot decide, you choose the correct version       |
