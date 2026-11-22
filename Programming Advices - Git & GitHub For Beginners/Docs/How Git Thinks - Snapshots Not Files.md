# How Git Thinks: Snapshots, Not Files

## 1. The Question

What is Git actually doing when you save your work?

The answer is one idea: **<mark>Git <u>does not track</u> edits — Git tracks <u>the state of your project</u> at points in time.</mark>** Once this clicks, branches, merges, and going back in time stop feeling like magic.

---

## 2. The Concept

When you commit, Git does not record "line 42 changed." It takes a **<mark><u>snapshot</u></mark>**: a complete picture of your entire project folder at that moment.

> **<mark>Commits are snapshots</mark>, and <mark>snapshots are <u>complete</u></mark>.**

Each commit adds one more snapshot to a growing timeline.

---

## 3. Mental Model: Git as a Camera

Imagine Git as a camera pointed at your project folder. Every commit, the camera clicks and captures the entire project.

Four consequences follow directly:

| Consequence                       | Meaning                                                             |
| --------------------------------- | ------------------------------------------------------------------- |
| Each snapshot is a complete state | **<mark>Not diffs</mark>** — the whole project, not just what moved |
| Any snapshot is reachable         | Travel back to last Tuesday, three commits ago, or v1.0             |
| Restoring is safe                 | Going back does not destroy recent work; it is its own snapshot     |
| Snapshots are comparable          | Full state at two moments → Git shows exactly what changed          |

---

## 4. The Insight: Conceptually Full, Physically Efficient

The obvious objection: *if every commit stores the whole project, **<mark>won't the repo explode?</mark>***

Git's trick:

| Property     | Reality                                                                                                                         |
| ------------ | ------------------------------------------------------------------------------------------------------------------------------- |
| Conceptually | Each snapshot is a complete state of the project                                                                                |
| Physically   | Unchanged files are **<mark><u>not</u> duplicated</mark>** — they are **<mark><u>shared</u> with the previous snapshot</mark>** |

If a file didn't change between commits, Git points to the existing copy. This is why a repo with thousands of commits stays small on disk.

You get the mental simplicity of "every commit is a full snapshot" without the storage cost.

---

## 5. Common Confusion

### 5.1 "Git stores differences"

**<mark><u>Wrong</u></mark>.** Older tools (SVN, CVS) stored **<mark>patches</mark>** — **<mark>a chain of changes <u>building on each other</u></mark>**. **<mark>Git <u>rejected</u> that model</mark>**.

The diffs you see in `git diff` are **<mark>computed <u>on the spot</u></mark>** by comparing two snapshots. The diff is not stored; it is calculated when asked.

### 5.2 "Committing overwrites my old work"

**<mark><u>Wrong</u></mark>.** A commit only **<mark>adds</mark>** a new snapshot. Every previous snapshot remains, reachable. Committing never destroys the past — which is what makes Git safe to use without fear.

---

## 6. The Rule

> **<mark>Commit small, meaningful units of work</mark> — and commit often.**

One logical change per commit:

| Change                                | Commit? |
| ------------------------------------- | ------- |
| Fixed a bug                           | Yes     |
| Added a function                      | Yes     |
| Renamed a variable across the project | Yes     |
| Cleaned up formatting                 | Yes     |

Don't wait until forty files have changed to make one giant commit. Large snapshots are **<mark>hard to understand and hard to roll back</mark>**.

**<mark>The payoff</mark>:** when something breaks, you roll back one small commit instead of losing a day's work. Six months later, the history reads as a story you understand.

---

## 7. Wrap-up

Git thinks in snapshots — complete, restorable pictures of the project taken at every commit.

- Every snapshot is a full, restorable state
- You can travel backward and forward without losing work
- You can compare any two snapshots to see exactly what changed
- Unchanged files are shared, so the repo stays small

Once the snapshot model clicks, Git stops feeling like magic. Everything later — branches, merges, rebases, tags — is just moving between snapshots or creating new ones.

Next: visualizing Git's timeline — commits, HEAD, and branches.

---

📌 **Summary**

| Question                                    | Answer                                                                            |
| ------------------------------------------- | --------------------------------------------------------------------------------- |
| What does Git store per commit?             | A complete snapshot of the project, not a diff                                    |
| Is each snapshot physically a full copy?    | No — unchanged files are shared between snapshots                                 |
| Where do diffs come from?                   | Computed on demand by comparing two snapshots                                     |
| Does committing overwrite old work?         | Never — commits only add; all previous snapshots remain                           |
| Why is the repo small despite many commits? | Content-addressed sharing of unchanged files                                      |
| What is the commit rule?                    | Small, meaningful units — one idea per commit                                     |
| What follows from the snapshot model?       | Branches, merges, rebases, tags are all just moving between or creating snapshots |
