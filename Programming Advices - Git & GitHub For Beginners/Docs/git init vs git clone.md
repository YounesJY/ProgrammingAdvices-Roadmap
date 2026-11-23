# git init vs git clone

## 1. Introduction

First question when working with real repositories: **<mark>how do you get a Git repository?</mark>**

---

## 2. The Concept

**<mark>Two ways</mark>**, same goal, **<mark>different starting point</mark>**:

| Command     | Purpose                     |
| ----------- | --------------------------- |
| `git init`  | Start a new repository      |
| `git clone` | Copy an existing repository |

---

## 3. Mental Model

| Command     | Analogy               |
| ----------- | --------------------- |
| `git init`  | Build a new house     |
| `git clone` | Buy an existing house |

You either **<mark>start from scratch</mark>** or **<mark>copy something that already exists</mark>**.

---

## 4. `git init` — Start a New Project

1. Create a folder for your project
2. Open a terminal inside it
3. Run:

```bash
git init
```

**Result:**

- Git creates a hidden `.git` folder
- Your project is now a Git repository

---

## 5. `git clone` — Copy an Existing Project

1. Copy the repository URL
2. Run:

```bash
git clone <repository-url>
```

Example:

```bash
git clone https://github.com/user/project.git
```

**Result:**

- Downloads the project
- Includes full history
- Ready to work immediately

---

## 6. What Happens Internally

| Command     | Effect                                                        |
| ----------- | ------------------------------------------------------------- |
| `git init`  | Creates an empty repository (`.git/` with no history)         |
| `git clone` | Copies the repository **<mark>including full history</mark>** |

`git clone` is effectively:

> **<mark>init + download everything</mark>**

Concretely, `clone` runs `init` in the target directory, sets up `origin` pointing at the source URL, <mark>**fetches all objects**</mark> (commits, trees, blobs), checks out the default branch, and configures tracking. That's why a fresh clone is immediately usable and a fresh `init` is not — the `init` repo has no objects, no `origin`, no history.

---

## 7. Common Mistakes

| Mistake                                                     | Consequence                                                    |
| ----------------------------------------------------------- | -------------------------------------------------------------- |
| Using `git init` instead of `clone` for an existing project | You lose history — the new repo starts empty                   |
| Cloning inside another repo                                 | Nested repositories — confusing and easy to commit incorrectly |

---

## 8. The Rule

> **`git init` = <mark>new project</mark>. `git clone` = <mark>existing project</mark>.**

---

## 9. Wrap-up

You now know how to start working with repositories. Next: how to check what's happening inside Git.

---

📌 **Summary**

| Question                              | Answer                                                                |
| ------------------------------------- | --------------------------------------------------------------------- |
| What does `git init` do?              | Creates an empty repository (`.git/` folder) in the current directory |
| What does `git clone` do?             | Copies an existing repository with its full history                   |
| Relationship between them?            | `clone` ≈ `init` + set `origin` + fetch all objects + checkout        |
| Which to use for a new project?       | `git init`                                                            |
| Which to use for an existing project? | `git clone`                                                           |
| Why not `init` an existing project?   | You lose history — the repo starts empty                              |
| Why not clone inside a repo?          | Nested repositories cause confusion                                   |
| The rule?                             | `init` = new; `clone` = existing                                      |
