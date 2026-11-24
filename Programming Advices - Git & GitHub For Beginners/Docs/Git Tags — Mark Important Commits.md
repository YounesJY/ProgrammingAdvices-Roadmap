# Git Tags — Mark Important Commits

## 1. Introduction

Every commit saves a version of the project. But sometimes **<mark>one commit is <u>more important than the others</u></mark>**:

- The first stable version
- A checkpoint you want to keep
- The version before a big change
- The version you call `v1.0`
- A version you may want to return to later

**<mark>Git <u>tags</u> mark these commits in history</mark>**.

---

## 2. The Concept

A tag is a **<mark><u>fixed name</u> that <u>points to a specific commit</u></mark>**.

```bash
git tag v1.0
```

This creates a tag called `v1.0` on the current commit.

> "This commit is important. **<mark>I want <u>to remember it</u> with a <u>clear name</u></mark>**."

---

## 3. Mental Model: Branches Move, Tags Stay

<img title="" src="https://uploads.teachablecdn.com/attachments/089d819964864892a677a4e84b96ba05.png" alt="" width="264">

Think of history as a road. Each commit is a point on it.

```
A ---- B ---- C
              ↑
             main
```

Tag commit C:

```
A ---- B ---- C
              ↑
             v1.0
```

Continue working — `main` **<mark>moves forward</mark>**, **<mark>the tag <u>does not</u></mark>**:

```
A ---- B ---- C ---- D ---- E
                            ↑
                           main
              ↑
             v1.0
```

> **<mark>The most important idea</mark>: <mark>Branches <u>move</u>. Tags <u>stay fixed</u></mark>.**

---

## 4. Branch vs Tag

|              | Branch                         | Tag                                             |
| ------------ | ------------------------------ | ----------------------------------------------- |
| Purpose      | Active work                    | **<mark><u>Mark</u> an important point</mark>** |
| Moves?       | Yes — with every new commit    | **<mark><u>No</u></mark>** — stays fixed        |
| Created with | `git branch` / `git switch -c` | `git tag`                                       |
| Example      | `main`, `login-feature`        | `v1.0`, `v2.0`                                  |

---

## 5. Demo

### Step 1 — Create a project

```bash
mkdir GitTagsDemo
cd GitTagsDemo
git init
```

### Step 2 — Create a C++ file

```cpp
// main.cpp
#include <iostream>

int main()
{
    std::cout << "Version 1 of the app" << std::endl;
    return 0;
}
```

### Step 3 — First commit

```bash
git status
git add main.cpp
git commit -m "Create first version of app"
```

### Step 4 — Create a tag

```bash
git tag v1.0
```

Marks the current commit as `v1.0`.

### Step 5 — List tags

```bash
git tag
```

```
v1.0
```

### Step 6 — Continue working

```cpp
// main.cpp
#include <iostream>

int main()
{
    std::cout << "Version 2 of the app" << std::endl;
    std::cout << "New feature added" << std::endl;
    return 0;
}
```

```bash
git add main.cpp
git commit -m "Add new feature"
```

```
A ---- B
↑      ↑
v1.0   main
```

Commit A is tagged. Commit B is the current `main` tip.

---

## 6. Show Tag Details

```bash
git show v1.0
```

Shows the commit the tag points to: ID, author, date, message, and changes.

---

## 7. View the Project at a Tag

```bash
git switch --detach v1.0
```

This moves you to the commit marked by `v1.0`. Opening `main.cpp` shows the **old** version.

### Detached HEAD

`git switch --detach` puts you in **<mark><u>detached HEAD</u></mark>** mode — <mark>**you are looking at <u>a commit directly</u>, not standing on a branch**</mark>.

Safe for:

- Reading old code
- Testing an old version
- Comparing versions
- Checking what the project looked like at that tag

> **<mark><u>Not the place</u> to continue normal work</mark>**.

To return to `main`:

```bash
git switch main
```

---

## 8. Create a Branch from a Tag

To fix something from an old tagged version:

```bash
git switch -c fix-v1.0 v1.0
```

Creates `fix-v1.0` starting at tag `v1.0`. You are no longer on the tag — you are on a new branch born from it.

---

## 9. Types of Tags

| Type            | Command                                    | Stores                                                 | Use for                                                    |
| --------------- | ------------------------------------------ | ------------------------------------------------------ | ---------------------------------------------------------- |
| **Lightweight** | `git tag v1.0`                             | Just a pointer to a commit                             | Practice, **<mark><u>simple local checkpoints</u></mark>** |
| **Annotated**   | `git tag -a v1.0 -m "Release version 1.0"` | Tag name, message, tagger name, date, commit reference | **<mark><u>Real project releases</u></mark>**              |

> **Recommended:** **<mark><u>lightweight</u> for practice</mark>**; **<mark><u>annotated</u> for important versions</mark>**.

---

## 10. Delete a Local Tag

```bash
git tag -d v1.0
```

Removes the tag from your machine. **<mark><u>Does not</u> delete the commit</mark>** — the commit remains; only the name is removed.

---

## 11. Remote Servers (Later)

This lesson works locally. **<mark>Pushing tags to GitHub is covered later</mark>**. For now: create, list, inspect, view old versions via tags, and branch from tags — all local.

---

## 12. Common Mistakes

| Mistake                                    | Reality                                                                                                                           |
| ------------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------- |
| Thinking tags move like branches           | **<mark>Tags <u>stay fixed</u></mark>**; branches move with commits                                                               |
| Thinking a tag is a branch                 | **<mark>A tag is <u>a label</u></mark>**. Use `git switch --detach <tag>` to view, `git switch -c <branch> <tag>` to work from it |
| Thinking deleting a tag deletes the commit | `git tag -d` **<mark>removes <u>only the name</u></mark>**; the commit remains                                                    |

---

## 13. The Bottom Line

A Git tag is a **<mark><u>fixed label</u> for <u>an important commit</u></mark>**. Use it for stable versions, releases, milestones, checkpoints, and versions before big changes.

> **<mark>Branches <u>move</u>. Tags <u>stay fixed</u>.</mark>**

Next: `git stash` — saving changes temporarily.

---

📌 **Summary**

| Question                               | Answer                                            |
| -------------------------------------- | ------------------------------------------------- |
| What is a tag?                         | A fixed name pointing to a specific commit        |
| Does a tag move with new commits?      | No — it stays on its commit                       |
| Does a branch move?                    | Yes — with every new commit                       |
| How to create a lightweight tag?       | `git tag v1.0`                                    |
| How to create an annotated tag?        | `git tag -a v1.0 -m "Release version 1.0"`        |
| How to list tags?                      | `git tag`                                         |
| How to inspect a tag?                  | `git show v1.0`                                   |
| How to view the project at a tag?      | `git switch --detach v1.0` (enters detached HEAD) |
| How to work from a tag?                | `git switch -c fix-v1.0 v1.0`                     |
| How to return to main?                 | `git switch main`                                 |
| Does deleting a tag delete the commit? | No — only the tag name is removed                 |
| Recommended tag type for releases?     | Annotated (`-a -m`)                               |
| The core rule                          | Branches move. Tags stay fixed.                   |
