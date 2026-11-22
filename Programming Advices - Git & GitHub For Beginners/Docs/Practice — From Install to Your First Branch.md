# Practice — From Install to Your First Branch

## 1. Introduction

Time to start using Git. First: install it and configure your identity.

---

## 2. The Concept

Git needs two things before you start:

| Requirement                   | Why                               |
| ----------------------------- | --------------------------------- |
| Git installed on your machine | The tool itself                   |
| Your identity (name & email)  | Attached to every commit you make |

---

## 3. Mental Model

Git is like a notebook. **<mark>Every commit is a signed entry</mark>.** Git needs to know: *who wrote this change?*

---

## 4. Setup

### Step 1 — Install Git

1. Go to `https://git-scm.com`
2. Download Git for your system (Windows / Mac)
3. Install using default settings (recommended for beginners)

### Step 2 — Verify Installation

```bash
git --version
```

You should see the installed version.

### Step 3 — Configure Your Identity

```bash
git config --global user.name "Your Name"
git config --global user.email "your@email.com"
```

Example:

```bash
git config --global user.name "Mohammed"
git config --global user.email "mohammed@email.com"
```

### Step 4 — Verify Configuration

```bash
git config --list
```

You should see your name and email.

### Step 5 — Set Default Branch Name (Optional)

```bash
git config --global init.defaultBranch main
```

---

## 5. Starting Real Work

### Step 6 — Initialize a Repository

Create a project folder, then:

```bash
cd your-project-folder
git init
```

Git now starts tracking this folder.

**Mental model:** you just created a "notebook" to track your project.

### Step 7 — First Commit

Create a file (e.g., `main.cpp` or `index.html`), then:

```bash
git add .
git commit -m "First commit"
```

This saves your first snapshot.

**Mental model:** you wrote your first signed page in the notebook.

### Step 8 — Second Commit

Make a change in your file, then:

```bash
git add .
git commit -m "Second commit"
```

Now you have history — two snapshots.

**Mental model:** Git now remembers BEFORE and AFTER.

### Step 9 — Create Your First Branch

```bash
git branch feature-1
```

This creates a new branch. Switch to it:

```bash
git checkout feature-1
```

You are now working on a different path.

**Mental model:** you created another timeline to experiment safely.

---

## 6. Common Mistakes

| Mistake                                   | Consequence                          |
| ----------------------------------------- | ------------------------------------ |
| Forgetting `git add` before commit        | Nothing will be saved                |
| Unclear commit messages                   | History becomes unreadable           |
| Thinking a branch = a copy of the project | It's just a pointer, not a duplicate |

---

## 7. Rules

- Install & configure once
- Always commit **<mark>after a meaningful change</mark>**
- Use branches **<mark>to experiment safely</mark>**

---

## 8. Wrap-up

You can now:

- Install Git
- Configure identity
- Create a repo
- Make commits
- Create and switch branches

Next: understanding `git log` & history.

---

📌 **Summary**

| Question                       | Answer                                         |
| ------------------------------ | ---------------------------------------------- |
| What does Git need before use? | Installation and an identity (name + email)    |
| How to verify installation?    | `git --version`                                |
| How to set identity?           | `git config --global user.name` / `user.email` |
| How to verify config?          | `git config --list`                            |
| How to initialize a repo?      | `git init` in the project folder               |
| How to make a commit?          | `git add .` then `git commit -m "..."`         |
| How to create a branch?        | `git branch <name>`                            |
| How to switch to it?           | `git checkout <name>`                          |
| Top mistake                    | Forgetting `git add` — nothing gets committed  |
| What is a branch, really?      | A pointer, not a copy of the project           |
