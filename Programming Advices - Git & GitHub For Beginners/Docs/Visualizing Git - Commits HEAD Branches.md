# Visualizing Git: Commits, HEAD, Branches

## 1. Introduction

The previous lesson established that Git stores snapshots, not diffs. But snapshots alone are just a pile of saves. To use Git, you need to see how snapshots connect — and who is pointing at what.

Three characters make Git make sense.

---

## 2. The Three Concepts

| Concept                 | What it is                                          |
| ----------------------- | --------------------------------------------------- |
| **<mark>Commit</mark>** | A snapshot, linked to its parent commits in a chain |
| **<mark>HEAD</mark>**   | A pointer that says "you are here"                  |
| **<mark>Branch</mark>** | A movable label pointing at a specific commit       |

Every Git diagram, every command, and every "what just happened?" moment reduces to these three.

---

## 3. Mental Model: Trains and Sticky Notes

<img src="https://uploads.teachablecdn.com/attachments/09902d91c0354d1680a26df6dd19d532.png" title="" alt="" width="786">

<img src="https://uploads.teachablecdn.com/attachments/9f09e6738f8f4c8c864cd0635e50cf96.jpg" title="" alt="" width="576">

| Git concept | Train analogy                                                     |
| ----------- | ----------------------------------------------------------------- |
| Commit      | A train car; each new commit hitches a new car to the end         |
| Parent link | Each car remembers the car behind it — that's how order is known  |
| HEAD        | A flag the conductor carries, marking the car you're standing on  |
| Branch      | A sticky note slapped on a car, labeled `main` or `feature-login` |

When you commit on a branch, **<mark>the sticky note <u>slides forward</u> to the new car</mark>**. A second track branching off to the side, with its own sticky note, is all a branch really is: **<mark>a name stuck to a commit</mark>**.

---

## 4. Branch vs Tag

|                  | Branch                                            | Tag                            |
| ---------------- | ------------------------------------------------- | ------------------------------ |
| Type             | **<mark><u>Moving label</u></mark>**              | **<mark>Fixed label</mark>**   |
| Points to        | **<mark>Latest commit in a line of work</mark>**  | One specific commit, forever   |
| Moves on commit? | **<mark><u>Yes</u></mark>** — follows you forward | No — stays put                 |
| Purpose          | Active line of development                        | Snapshot marker (e.g., `v1.0`) |

---

## 5. The Insight: A Branch Is a Pointer, Not a Copy

> A branch is not a copy of your code. **<mark><u>It is a pointer</u></mark>** — **<mark><u>literally</u></mark>** 41 bytes on disk.

When you switch branches, Git does not duplicate folders or copy files. It:

1. Looks at which commit the branch points to.
2. Rebuilds your working folder to match that snapshot.
3. Moves HEAD to say "you're on this branch now."

That is why switching branches **<mark>feels instant <u>even</u> on huge projects</mark>**. **You are not moving the code — <mark>you are moving <u>where you stand</u></mark>.**

---

## 6. Common Confusion: Deleting a Branch

> "If I delete a branch, do I lose my commits?"

Usually **<mark>no</mark>**. Deleting a branch **<mark>removes <u>the sticky note</u></mark>**. The commits remain in Git's database.

**<mark>The catch</mark>:** if nothing else points to those commits (no other branch, no tag, no HEAD), Git will eventually consider them **<mark>orphaned and <u>clean them up</u></mark>**.

> **Commits are safe <mark><u>as long as</u> something points to them</mark>.**

This is why beginners think they "lost their work" after deleting a branch. The work is often still there — **<mark>they lost <u>the label</u> that made it findable</mark>**.

---

## 7. The Rule

> **In Git, names point to commits. Commits point at parents. <mark>That's the entire map</mark>.**

Once you see the graph this way, commands stop feeling mysterious.

---

## 8. Wrap-up

The mental picture that carries the rest of Git:

- A chain of **<mark>commits</mark>**
- A **<mark>HEAD</mark>** pointer telling you where you are
- **<mark>Branches</mark>** as lightweight labels you can peel off and slap down anywhere

Next: put the picture in motion — make real commits, create a branch, and watch HEAD slide around the graph.

**Resources from the lesson:**

- Git Visualizer: `https://aburakab.github.io/courses/git-visualizer.html`
- Source: `https://github.com/aburakab/courses`

---

📌 **Summary**

| Question                               | Answer                                                                 |
| -------------------------------------- | ---------------------------------------------------------------------- |
| What are the three core concepts?      | Commits, HEAD, branches                                                |
| What is a commit?                      | A snapshot linked to its parent commits                                |
| What is HEAD?                          | A pointer to the current position in history                           |
| What is a branch?                      | A movable label pointing at a commit                                   |
| Does a branch copy code?               | No — it is a pointer (41 bytes on disk)                                |
| Why is branch switching instant?       | Git rebuilds the working folder from the snapshot; it moves no files   |
| What happens when a branch is deleted? | The label is removed; commits survive if something else points to them |
| When are commits lost?                 | When nothing points to them — then Git eventually cleans them up       |
| The rule?                              | Names point to commits; commits point at parents                       |
