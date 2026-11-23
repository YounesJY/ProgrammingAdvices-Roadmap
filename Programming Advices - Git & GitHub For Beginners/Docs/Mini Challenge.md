# Mini Challenge — Create Repo → Add Files → Commit → Check History

## 1. Goal

A complete basic Git flow from scratch. By the end you will have done every core command in sequence, on a real repo.

---

## 2. The Flow at a Glance

```
init → status → create files → status → add → status → commit → log
  → modify → status → diff → add → diff --staged → commit → log
  → rename → status → add → diff --staged --summary → commit
  → move+edit → status → add → diff --staged --summary → commit
```

---

## 3. Steps

### Part A — Setup

| #   | Action                        | Command            |
| --- | ----------------------------- | ------------------ |
| 1   | Create folder `MyGitPractice` | —                  |
| 2   | Enter it                      | `cd MyGitPractice` |
| 3   | Initialize                    | `git init`         |
| 4   | Check state                   | `git status`       |

`.git/` is created. Git now tracks the folder.

### Part B — First Files and First Commit

| #   | Action                                               | Command                                 |
| --- | ---------------------------------------------------- | --------------------------------------- |
| 5   | Create `index.html`, `notes.txt` with simple content | —                                       |
| 6   | Check state — files are **untracked**                | `git status`                            |
| 7   | Stage all                                            | `git add .`                             |
| 8   | Check state — files are **staged**                   | `git status`                            |
| 9   | First commit                                         | `git commit -m "Initial project files"` |
| 10  | View history                                         | `git log --oneline`                     |

`--oneline` = one line per commit. Not "online internet."

### Part C — Modify, Diff, Stage, Commit

| #   | Action                                              | Command                             |
| --- | --------------------------------------------------- | ----------------------------------- |
| 11  | Edit `notes.txt` — add `Learning Git step by step.` | —                                   |
| 12  | Check state — `notes.txt` modified                  | `git status`                        |
| 13  | View unstaged diff                                  | `git diff`                          |
| 14  | Stage only that file                                | `git add notes.txt`                 |
| 15  | View staged diff                                    | `git diff --staged`                 |
| 16  | Second commit                                       | `git commit -m "Update notes file"` |
| 17  | View history                                        | `git log --oneline`                 |
| 18  | Inspect one commit                                  | `git show <COMMIT_ID>`              |

### Part D — Second File, Same Cycle

| #   | Action                                                             | Command                             |
| --- | ------------------------------------------------------------------ | ----------------------------------- |
| 19  | Edit `index.html` — add `<p>This project is for Git practice.</p>` | —                                   |
| 20  | Check state                                                        | `git status`                        |
| 21  | View diff                                                          | `git diff`                          |
| 22  | Stage                                                              | `git add index.html`                |
| 23  | View staged diff                                                   | `git diff --staged`                 |
| 24  | Third commit                                                       | `git commit -m "Update index page"` |
| 25  | History with file summary                                          | `git log --oneline --stat`          |

### Part E — Rename (content unchanged)

| #   | Action                                     | Command                                                |
| --- | ------------------------------------------ | ------------------------------------------------------ |
| 26  | Rename `notes.txt` → `project-notes.txt`   | `mv notes.txt project-notes.txt` (or `ren` on Windows) |
| 27  | Check state — Git shows delete + untracked | `git status`                                           |
| 28  | Stage                                      | `git add .`                                            |
| 29  | Verify rename detection                    | `git diff --staged --summary`                          |
| 30  | Commit                                     | `git commit -m "Rename notes file"`                    |

Expected summary:

```
rename notes.txt => project-notes.txt (100%)
```

### Part F — Move + Heavy Edit (the interesting case)

| #   | Action                                                                                | Command                                                         |
| --- | ------------------------------------------------------------------------------------- | --------------------------------------------------------------- |
| 31  | Move `project-notes.txt` → `docs/project-notes.txt`, then rewrite most of its content | `mkdir docs` then `mv project-notes.txt docs/project-notes.txt` |
| 32  | Check state                                                                           | `git status`                                                    |
| 33  | Stage                                                                                 | `git add .`                                                     |
| 34  | Verify how Git interpreted it                                                         | `git diff --staged --summary`                                   |
| 35  | Commit                                                                                | `git commit -m "Move notes file and update content"`            |
| 36  | History with summary                                                                  | `git log --oneline --stat`                                      |

Git may report this as **renamed** or as **deleted + created**. There is no fixed "50%" threshold — Git uses a similarity score, and the result depends on the amount and type of change.

---

## 4. What to Notice

| Observation                                | Why it matters                                               |
| ------------------------------------------ | ------------------------------------------------------------ |
| Untracked vs modified                      | Two different states — `git status` distinguishes them       |
| Working Directory vs Staging Area          | Files move between them via `git add`                        |
| `git diff` vs `git diff --staged`          | Unstaged vs staged — the most common beginner trap           |
| Each commit is a snapshot                  | `git log --oneline` shows the timeline                       |
| `--stat` adds file summary                 | You see *which* files changed per commit                     |
| `git show` opens one commit                | Info + full diff                                             |
| Rename detection is similarity-based       | Content changes during rename can break detection            |
| Status after `mv` shows delete + untracked | Normal — Git records what exists, not what you *meant* to do |

---

## 5. End State

You will have:

- A repo with multiple files
- At least 3 commits
- One modified text file committed
- One modified HTML file committed
- Hands-on time with: `init`, `status`, `add .`, `add <file>`, `commit -m`, `diff`, `diff --staged`, `log --oneline`, `log --oneline --stat`, `show`
- A visible, growing project history

---

## 6. Recommended Order for You

Given your daily Git usage, run this challenge once **as written** — even the trivial steps. The value is not the commands; it's verifying that your mental model of each state transition matches Git's actual output at every step. The parts worth slowing down on:

| Step     | Why slow down                                                       |
| -------- | ------------------------------------------------------------------- |
| 13 vs 15 | Confirm what `diff` and `diff --staged` each show                   |
| 27       | Notice that a plain `mv` produces delete + untracked before staging |
| 29       | Confirm Git reports `rename ... (100%)`                             |
| 34       | Confirm how a heavy edit during a move changes Git's interpretation |

If any of those four surprises you, that's exactly the gap the course is filling.

---

📌 **Summary**

| Question                                            | Answer                                                                     |
| --------------------------------------------------- | -------------------------------------------------------------------------- |
| What does this challenge cover?                     | The full basic flow: init → add → commit → log → diff → rename → move      |
| How many commits at the end?                        | At least 3 (plus rename and move commits)                                  |
| What's the key comparison?                          | `git diff` (unstaged) vs `git diff --staged` (staged)                      |
| What does `--oneline` mean?                         | One line per commit — not "online"                                         |
| What does `--stat` add to log?                      | Per-commit file-change summary                                             |
| What happens to a rename with heavy edits?          | Git may see it as delete + add, not rename                                 |
| Why does `status` show delete+untracked after `mv`? | Git records actual state, not intent                                       |
| Which steps deserve the most attention?             | The four diff/rename verification steps                                    |
| The end goal?                                       | Hands-on confirmation that your mental model matches Git's actual behavior |
