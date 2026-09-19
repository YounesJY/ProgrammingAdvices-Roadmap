# Multithreading, Concurrency & Parallelism — Q&A

---

## 1. Foundational Concepts

### Q: Multithreading vs Multitasking — same thing or inherently different?

**A:** They're **different**, though they overlap:

| Concept            | Meaning                                                                                                                                      |
| ------------------ | -------------------------------------------------------------------------------------------------------------------------------------------- |
| **Multitasking**   | <mark>OS runs multiple **processes** apparently at the same time</mark> (time-slicing on a single core, or truly parallel on multiple cores) |
| **Multithreading** | <mark>A **single process** runs multiple threads concurrently</mark>                                                                         |

**Layering:**

```
Multitasking       → multiple processes
  └── Multithreading → multiple threads inside one process
```

<mark>Multitasking is about **processes**; multithreading is about **threads within one process**.</mark>

---

### Q: Concurrent vs Parallel programming?

**A:** These are **not** the same, and the distinction matters:

| Term           | Meaning                                                                                                               | Example                             |
| -------------- | --------------------------------------------------------------------------------------------------------------------- | ----------------------------------- |
| **Concurrent** | Multiple tasks **in progress** at once, but not necessarily executing at the same instant (interleaved / time-sliced) | One chef switching between 3 dishes |
| **Parallel**   | Multiple tasks **literally executing at the same instant** on different cores                                         | 3 chefs each cooking one dish       |

**Key insight:**

- <mark>Concurrency is about **structure**</mark> (<mark>dealing with</mark> many things at once)
- <mark>Parallelism is about **execution**</mark> (<mark>doing many</mark> things at once)

You can have:

- <mark>Concurrent <u>but</u> **not** parallel</mark> (single-core, time-slicing)
- <mark>Parallel **<u>only if</u>** you have multiple cores</mark>
- <mark>**<u>Both at the same time</u>**</mark>

---

### Q: Synchronous vs Multithreading vs Asynchronous — how do they relate?

**A:** Think of them as **three execution models**:

| Model              | Description                                                                                                 | Analogy                                                                                         |
| ------------------ | ----------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------- |
| **Synchronous**    | <mark><u>**Sequential**</u></mark> — <mark>one task at a time</mark>, <mark>top to bottom</mark>            | One worker doing tasks one after another                                                        |
| **Multithreading** | Multiple workers <mark>running in parallel <u>on different threads</u></mark>                               | Hiring 5 workers, each doing a job at once                                                      |
| **Asynchronous**   | <mark>**<u>One worker</u>**</mark>, but <mark>switches tasks <u>while waiting</u></mark> (e.g., during I/O) | One worker putting a task aside while waiting for the oven, working on something else meanwhile |

**Important:**

- **Async ≠ multi-threaded.** Async can be implemented on a single thread (event-loop style, like JavaScript's `async/await`).
- **Multithreading ≠ async.** Threads run in parallel but can still block.

---

### Q: Is real-world work typically Multithreads + Async combined?

**A:** Yes — the **practical model** in most modern apps is:

```
Async (for I/O) + Threads/Tasks (for CPU work) + a thread pool managing the threads
```

- **I/O-bound work** → async (don't block a thread while waiting)
- **CPU-bound work** → background threads / `Task.Run` / `Parallel.For`
- <mark>**<u>Both combined → responsive, scalable apps</u>**</mark>

---

### Q: Async vs Multithread — cost and when to use each?

**A:**

| Aspect          | Async                                                         | Multithread                                           |
| --------------- | ------------------------------------------------------------- | ----------------------------------------------------- |
| **Best for**    | <mark>I/O-bound</mark> work (file, network, DB)               | <mark>CPU-bound</mark> work (computation)             |
| **Cost**        | <mark><u>Low</u></mark> — no new thread, just a state machine | Higher — thread creation, context switches            |
| **Scalability** | Very high (thousands of operations)                           | <mark>**Limited by cores and thread overhead**</mark> |
| **Blocking**    | Never blocks the caller                                       | Blocking the thread is a risk                         |
| **Complexity**  | Easier at high scale                                          | Harder due to sync issues                             |

<mark><u>**Rule of thumb:**</u></mark>

- **I/O-bound** → `async/await`
- **CPU-bound** → `Task.Run` / `Parallel.For` / threads

---

## 2. Threads vs Tasks

### Q: Threads vs Tasks in C#/Java — Virtual vs Platform threads?

**A:**

| Aspect         | Thread (raw)                                    | Task / Future                                                     |
| -------------- | ----------------------------------------------- | ----------------------------------------------------------------- |
| **Level**      | <mark><u>**Low-level OS primitive**</u></mark>  | <mark><u>**High-level abstraction**</u></mark>                    |
| **Created by** | `new Thread(...)`                               | `Task.Run(...)` / `ExecutorService`                               |
| **Managed by** | OS scheduler                                    | Task Scheduler / Thread Pool                                      |
| **Lifetime**   | Explicit `Start()` and `Join()`                 | Returned as a future/handle                                       |
| **Best for**   | <mark>Long-running <u>dedicated </u>work</mark> | <mark>Short-lived</mark>, <mark>many concurrent operations</mark> |

**Virtual vs Platform threads:**

| Type         | Platform Thread   | Virtual Thread                                                                       |
| ------------ | ----------------- | ------------------------------------------------------------------------------------ |
| **Backing**  | 1 OS thread       | Many virtual threads → 1 OS thread                                                   |
| **Cost**     | Expensive (MBs)   | Cheap (KBs)                                                                          |
| **Language** | Classic Java / C# | Java 21+ (`Thread.ofVirtual()`); C# uses `Task` + async, not truly "virtual threads" |
| **Use case** | CPU work          | Massive I/O concurrency                                                              |

> <mark>**C# doesn't have true virtual threads** — it uses `Task` + `async/await` + thread pool to achieve similar scalability.</mark>

---

### Q: Is Thread low-level and Task an abstraction?

**A:** Yes, exactly.

```
Task (high-level, abstraction)
   ↓ runs on
Thread Pool (managed pool of reusable threads)
   ↓ uses
Thread (low-level OS primitive)
   ↓ scheduled by
OS Scheduler
```

- **Thread** = raw OS handle
- **Task** = abstraction over a unit of work; may run on a pooled thread, may not even need a thread (async I/O)
- **TPL** (Task Parallel Library) = the library that provides `Task`, `Parallel`, `PLINQ`, etc.

> <mark>**Recommendation:** Prefer **Tasks over raw Threads** — they're safer, cheaper, and integrate with async.</mark>

---

### Q: What is TPL?

**A:** **TPL = Task Parallel Library** — <mark>.NET's high-level API for parallelism and concurrency</mark>. It provides:

- `Task` / `Task<T>` — represent async/parallel work
- `Parallel.For`, `Parallel.ForEach`, `Parallel.Invoke`
- <mark>`PLINQ` — parallel LINQ</mark>
- Continuations, cancellation, aggregation

**In Java**, the equivalent is `java.util.concurrent` (ExecutorService, CompletableFuture, ForkJoinPool).

---

### Q: What is a Thread Pool and why does it exist?

**A:** <mark>A **thread pool** is a **pre-created set of reusable threads** managed by the runtime</mark>.

**Why?**

- Creating a thread is **expensive** (~1 MB stack, OS bookkeeping)
- If you spawn a thread per request, you'll destroy performance under load
- Reusing threads = **less overhead, better throughput**

**How it works:**

```
Task → Thread Pool Queue → [Thread1, Thread2, ..., ThreadN]
                             ↑ reuse, not create
```

<mark>**<u>Rule</u>**</mark>: For short-lived work, <mark>let the pool manage threads</mark> (via `Task.Run`). For long-running work (e.g., a server loop), create a dedicated thread.

---

### Q: What is `Task.Run` related to threads and the thread pool?

**A:** `Task.Run` = "queue this work to the thread pool."

```csharp
Task.Run(() => ComputeSomething());
```

What happens:

1. A `Task` is created
2. It's **queued to the thread pool**
3. A pooled thread picks it up and runs it
4. The `Task` completes and the thread returns to the pool

**Why use it?**

- <mark>Offload **CPU-bound** work off the UI thread</mark>
- Take advantage of the pool's efficiency
- Get a `Task` handle you can `await`

**Don't use `Task.Run` for I/O-bound work** — use native async APIs (`HttpClient.GetAsync`, `File.ReadAllTextAsync`).

---

### Q: Pooled vs Background threads — why the distinction?

**A:**

| Type                  | Behavior                              | Use                                           |
| --------------------- | ------------------------------------- | --------------------------------------------- |
| **Foreground Thread** | Keeps the process alive until it ends | Critical work the app shouldn't exit without  |
| **Background Thread** | Process can exit even if it's running | Non-critical work (logging, monitoring, etc.) |
| **Pooled Thread**     | Reusable, managed by the thread pool  | Short tasks, tasks from `Task.Run`            |

**<mark>Pooled threads are usually background threads</mark>**, so they don't prevent the process from exiting. That's why apps can exit even with pending pooled work — a common gotcha.

---

### Q: Should I prefer abstractions over low-level threads?

**A:** <mark><u>Yes, **almost always**</u></mark>.

| Use                         | Recommended                         |
| --------------------------- | ----------------------------------- |
| Short tasks, many at once   | `Task.Run` / `ExecutorService`      |
| CPU-bound parallelism       | `Parallel.For` / `Parallel.ForEach` |
| I/O-bound                   | `async/await`                       |
| Long-running dedicated work | Raw `Thread` (rare)                 |

**Reasons:**

> - <mark>Less error-prone</mark>
> - <mark>Better performance (thread reuse)</mark>
> - <mark>Composable with `await`, cancellation, etc.</mark>

<mark><u>Only drop</u> to raw threads when you need **precise control**</mark> (e.g., a dedicated thread with specific priority or affinity).

---

## 3. Race Conditions & Locking

### Q: What is a race condition?

**A:** A **race condition** occurs when two or more threads access **shared state** concurrently, and the final result depends on the **unpredictable timing/order** of their operations.

**Classic example:**

```csharp
static int counter = 0;

// Two threads doing:
counter++;  // Read → Modify → Write (3 steps, not atomic!)
```

Possible outcomes:

- Thread A reads 0, Thread B reads 0
- A writes 1, B writes 1
- Final value = **1 instead of 2**

---

### Q: Thread racing during access to shared resources, and dependency on raced thread results?

**A:** Two aspects:

1. **Racing to access shared resources** — both threads want to write to the same memory/file/DB row
2. **Dependency on raced results** — one thread's outcome influences another's; if the first "loses," the second may compute a wrong result

**Solutions:**

- **Locks** (`lock`, `Monitor`, `Mutex`) — mutual exclusion
- **Atomic operations** (`Interlocked` in C#, `AtomicInteger` in Java)
- **Immutable data** — no shared mutable state to race over
- **Thread-safe collections** (`ConcurrentDictionary`, `ConcurrentQueue`)

---

### Q: When using a `lock` statement, do all threads get blocked at that line except the one inside the critical section?

**A:** Yes — but with nuance.

**How `lock` works:**

```csharp
lock (syncObject)
{
    // critical section — only ONE thread at a time
}
```

- **One thread** enters and executes the block
- All other threads that **reach the `lock`** **block and wait** (they don't skip)
- Once the holder exits, one waiting thread is let in — repeat

**Does this slow things down?** Yes:

- Blocked threads are **idle** (wasted CPU time)
- Heavy contention → **serialization** of work
- Lock granularity matters — too coarse = slow; too fine = buggy

**Optimization options:**

- **Reader-writer locks** (`ReaderWriterLockSlim`) — multiple readers, single writer
- **Lock-free structures** (`Interlocked`, `ConcurrentQueue`)
- **Reduce critical section size** — do only what's needed inside the lock

**They don't skip** the critical section — they wait. If you wanted skipping, you'd use `Monitor.TryEnter` with a timeout.

---

### Q: Atomic vs Mutable/Immutable in a multi-threaded environment?

**A:**

| Type          | Thread-safe?          | Notes                                                        |
| ------------- | --------------------- | ------------------------------------------------------------ |
| **Mutable**   | ❌ Not by default      | Requires locks/atomic operations                             |
| **Immutable** | ✅ Yes by default      | Can't change — no race possible                              |
| **Atomic**    | ✅ Yes (per operation) | Single indivisible operation (e.g., `Interlocked.Increment`) |

**<mark>Rule of thumb</mark>:**

- Prefer **immutability** when sharing across threads
- Use **atomics** for simple counters/flags
- Use **locks** when mutable state must be shared

---

### Q: Lock mechanisms and mutual exclusion?

**A:**

**<mark><u>Mutual exclusion</u></mark>** = ensuring only **one thread** accesses a resource at a time.

**Common primitives:**

| Primitive              | Scope         | Use                                           |
| ---------------------- | ------------- | --------------------------------------------- |
| `lock` / `Monitor`     | Same process  | Simple mutual exclusion                       |
| `Mutex`                | Cross-process | Shared across processes                       |
| `Semaphore`            | Same process  | Limit concurrent access (e.g., max 3 threads) |
| `SemaphoreSlim`        | Same process  | Async-friendly semaphore                      |
| `ReaderWriterLockSlim` | Same process  | Many readers, one writer                      |
| `Interlocked`          | Same process  | Atomic operations (lock-free)                 |

**Choosing:**

- Mutual exclusion within a process → `lock`
- Cross-process → `Mutex`
- Rate-limiting concurrency → `Semaphore`

---

### Q: Database connection pooling — is it a mutual exclusion mechanism?

**A:** **No.** Connection pooling is a **resource reuse** mechanism, not mutual exclusion.

**What it does:**

- Keeps a pool of open DB connections
- Hands one to a caller when needed
- Returns it to the pool when done

**Why:**

- Opening a DB connection is **expensive** (network, auth, handshake)
- Reusing connections = **huge performance gain**

**Inside the pool**, yes — a **lock/semaphore** protects the pool's internal data structure so two threads don't grab the same connection. But the pool itself isn't "mutual exclusion" — it's a **resource manager**.

**Analogy:** A parking lot is not a lock; it's a **pool of parking spots**. A gate at the entrance might use a lock, but the lot is the pool.

---

## 4. Tasks: Creation, Scheduling, Continuations

### Q: What is TaskFactory, and why does it exist?

**A:** `TaskFactory` is an object that <mark>**encapsulates how tasks are created and scheduled**</mark>.

**What it provides:**

- Task creation (`StartNew`)
- Scheduling options (`TaskScheduler`)
- Continuations (`ContinueWhenAll`, `ContinueWhenAny`)
- Configuration (cancellation, creation options)

**Why?**

- Lets you **standardize** how tasks are created across your app
- Lets you plug in a **custom scheduler** (e.g., UI thread scheduler)
- Groups related configuration together

**Example:**

```csharp
var factory = new TaskFactory(
    CancellationToken.None,
    TaskCreationOptions.None,
    TaskContinuationOptions.None,
    TaskScheduler.Default);

factory.StartNew(() => Work());
```

---

### Q: TaskFactory and TaskScheduling — is the OS the default one choosing task order? Does that lead to race conditions?

**A:** Not quite the OS — it's the **TaskScheduler** (default = thread pool scheduler), which sits **above** the OS scheduler.

**Flow:**

```
Your code → Task → TaskScheduler → Thread Pool → OS Scheduler → CPU
```

- The **TaskScheduler** decides *when* and *where* tasks run
- The **OS Scheduler** decides which thread runs on which core

**Does this cause race conditions?** Not directly — but the **non-deterministic order** of task execution **exposes** race conditions when tasks access shared state. The problem isn't the scheduler; it's the **unsynchronized access** to shared data.

---

### Q: What is TaskScheduler and why customize it?

**A:** A `TaskScheduler` decides how tasks get executed.

- **Default** — uses the thread pool
- **UI scheduler** — runs tasks on the UI thread (needed for WPF/WinForms)
- **Custom** — for specialized scenarios (limited concurrency, priority-based)

**Why customize?**

- UI thread safety (control updates only on UI thread)
- Limiting concurrency (e.g., max N tasks at once)
- Setting priorities

---

## 5. Parallel Class

### Q: What is the `Parallel` class? What abstraction does it provide?

**A:** `Parallel` is <mark>a **high-level abstraction** for running data-parallel or task-parallel work</mark>:

- `Parallel.For` — parallel for loop
- `Parallel.ForEach` — parallel foreach
- `Parallel.Invoke` — run multiple actions in parallel

**It:**

- <mark>Uses the thread pool</mark>
- **Partitions work automatically**
- <mark>**Handles synchronization** for you</mark>
- <mark>**Aggregates exceptions** (as `AggregateException`)</mark>
- <mark>**Cancels** on request</mark>

> You give it a loop or a list; <mark>it figures out how to split the work and run it in parallel</mark>.

---

### Q: What's the downside of the `Parallel` class? What's the trade-off?

**A:** It hides a lot of complexity — but that hiding is the trade-off:

| Benefit                 | Trade-off                                                     |
| ----------------------- | ------------------------------------------------------------- |
| Simple API              | <mark>Less control over scheduling</mark>                     |
| Auto-partitioning       | <mark>Not ideal for all workloads</mark>                      |
| Auto exception handling | Exceptions aggregated, <mark>harder to trace</mark>           |
| Uses pool threads       | <mark>Not suitable for async I/O</mark>                       |
| Good for CPU-bound work | <mark>**Bad** for I/O-bound work</mark> (blocks pool threads) |

**<mark><u>When to avoid it:</u></mark>**

- I/O-bound work → use `async/await` instead
- When order matters → use sequential
- When work is tiny → overhead may dominate

**Is it recommended?** Yes, **for CPU-bound parallel workloads** — it's the safest "easy" way to get parallelism. But if your workload is small or I/O-bound, it's the wrong tool.

---

### Q: `Parallel.For` vs normal `for` (manual threads) vs `Parallel.ForEach` vs `Parallel.Invoke`?

**A:**

| Construct              | Use Case                                  | Granularity         |
| ---------------------- | ----------------------------------------- | ------------------- |
| **`for` loop**         | Sequential work                           | One thread          |
| **Manual threads**     | Full control, rare                        | One thread per unit |
| **`Parallel.For`**     | CPU-bound loop over an index range        | Data-parallel       |
| **`Parallel.ForEach`** | CPU-bound loop over a collection          | Data-parallel       |
| **`Parallel.Invoke`**  | Run a set of distinct actions in parallel | Task-parallel       |

**Rules of thumb:**

- <mark>Prefer `Parallel.For`/`ForEach` over manual threads — less error-prone</mark>
- Use `Parallel.Invoke` <mark>when the work items are **different**</mark> (not a collection)
- Use a normal `for` when the workload is too small to benefit from parallelism

---

### Q: `Parallel.Invoke` is super abstracted — does everything for you. What's the trade-off?

**A:**

**What it does:**

```csharp
Parallel.Invoke(
    () => Task1(),
    () => Task2(),
    () => Task3()
);
```

Runs all three in parallel, waits for all to finish, aggregates exceptions.

**Trade-offs:**

| Benefit                       | Trade-off                                                                 |
| ----------------------------- | ------------------------------------------------------------------------- |
| Simple to write               | No control over thread assignment                                         |
| Auto-waits for completion     | All three must use thread pool threads                                    |
| Auto-aggregates exceptions    | <mark>If they need to share data, you handle synchronization</mark>       |
| Great for independent actions | <mark>If actions need async I/O, blocking pool threads is wasteful</mark> |

**Verdict:** Great for independent, CPU-bound tasks.<mark> **Not** the right tool for I/O-bound work or actions with complex dependencies</mark>.

---

## 6. Callbacks and Delegates

### Q: Callback functions vs delegates in this context?

**A:**

| Concept                         | Meaning                                                                   |
| ------------------------------- | ------------------------------------------------------------------------- |
| **Delegate** (C#)               | A type-safe reference to a method (the mechanism)                         |
| **Functional Interface** (Java) | A single-method interface (<mark>Java's delegate equivalent</mark>)       |
| **Callback**                    | <mark>A general concept</mark> — "call this code when X happens"          |
| **Lambda**                      | A concise way to write <mark>an inline function used as a callback</mark> |

**Relationship:**

- <mark>A **callback** is the **idea**</mark> — "notify me when done"
- A **delegate** or **functional interface** <mark>is the **mechanism** used to pass the callback</mark>
- A **lambda** is often how you **write** the callback concisely

**Same pattern in both languages:**

```csharp
// C# — delegate
someTask.ContinueWith(t => Console.WriteLine("Done"));

// Java — functional interface
someFuture.thenAccept(result -> System.out.println("Done"));
```

Both are passing **code as a parameter** — the callback.

---

## 7. Runnable / Callable / Lambdas Across Languages

### Q: Runnable and Callable in both languages? Lambda vs delegates in both languages?

**A:**

| Concept                       | C#                      | Java                                       |
| ----------------------------- | ----------------------- | ------------------------------------------ |
| **Void callback (no result)** | `Action`, `ThreadStart` | `Runnable`                                 |
| **Callback with a result**    | `Func<T>`               | `Callable<T>`                              |
| **Inline callback syntax**    | Lambda `() => {}`       | Lambda `() -> {}`                          |
| **Anonymous class fallback**  | `delegate() { }`        | `new Runnable() { public void run() { } }` |
| **Passing code as a param**   | Delegate                | Functional interface                       |

**Examples:**

```csharp
// C#
Thread t = new Thread(() => DoWork());      // ThreadStart delegate
Func<int> f = () => 42;                     // Func delegate
```

```java
// Java
Thread t = new Thread(() -> doWork());      // Runnable
Callable<Integer> c = () -> 42;             // Callable
```

**The mental model is identical**: both languages let you pass **a block of code** where the API expects a callable unit.

---

## 8. Consolidated Mental Model

```
                    ┌───────────────────────────────┐
                    │       High-level APIs         │
                    │  Task / Parallel / PLINQ      │  ← prefer this
                    └───────────────┬───────────────┘
                                    │
                    ┌───────────────▼───────────────┐
                    │       Thread Pool             │  ← reuse, cheap
                    └───────────────┬───────────────┘
                                    │
                    ┌───────────────▼───────────────┐
                    │       Threads (OS)            │  ← raw primitive
                    └───────────────┬───────────────┘
                                    │
                    ┌───────────────▼───────────────┐
                    │       OS Scheduler            │
                    └───────────────────────────────┘
```

**<mark><u>Rules of thumb</u></mark>:**

- **I/O-bound** → `async/await`
- **CPU-bound** → `Task.Run`, `Parallel.For`
- **Simple, small work** → sequential
- **Long-running dedicated** → raw `Thread`
- **Shared mutable state** → lock or avoid it (immutability)
- **Cross-process** → IPC (pipes, sockets, shared memory)
- **Reuse expensive resources** → pool (thread pool, connection pool)

---

## 9. Summary Table of All Questions

| #   | Topic                          | Key Takeaway                         |
| --- | ------------------------------ | ------------------------------------ |
| 1   | Multitasking vs Multithreading | Processes vs threads                 |
| 2   | Concurrent vs Parallel         | Structure vs execution               |
| 3   | Sync vs Threads vs Async       | Three execution models               |
| 4   | Real-world approach            | Async + Threads + Pool               |
| 5   | Async vs Multithread           | I/O vs CPU                           |
| 6   | Threads vs Tasks               | Low-level vs abstraction             |
| 7   | Thread pools                   | Reuse for performance                |
| 8   | Task.Run                       | Queue to pool for CPU work           |
| 9   | Pooled vs Background           | Lifetime distinction                 |
| 10  | Prefer abstractions            | Task over Thread                     |
| 11  | Race conditions                | Shared state + timing                |
| 12  | Lock behavior                  | Mutual exclusion, blocking           |
| 13  | Atomic vs Mutable/Immutable    | Safety vs flexibility                |
| 14  | Lock mechanisms                | Lock, Mutex, Semaphore, Interlocked  |
| 15  | Connection pool                | Resource reuse, not mutual exclusion |
| 16  | TaskFactory                    | Config + scheduler for tasks         |
| 17  | TaskScheduler                  | Above OS scheduler                   |
| 18  | Parallel class                 | Data/task parallelism                |
| 19  | Parallel trade-offs            | Simple but less control              |
| 20  | Parallel.For vs for vs Invoke  | Data-parallel vs task-parallel       |
| 21  | Parallel.Invoke trade-offs     | Easy but blocks pool                 |
| 22  | Callbacks vs delegates         | Idea vs mechanism                    |
| 23  | Runnable/Callable              | Java's delegate equivalents          |
| 24  | Mental model                   | Layered stack, prefer high-level     |

---
