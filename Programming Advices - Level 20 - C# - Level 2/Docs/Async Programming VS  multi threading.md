## What is Async Programming? how it's Different from multi threading?

- **Concurrency Model:**
  - Synchronous Programming: Concurrency is <mark>achieved through sequential execution</mark>. <mark>Each task is completed before moving on to the next</mark>, and <mark>the program waits for each operation to finish</mark>.
    - > <mark><u>**Does not support concurrency**</u></mark>. Tasks are executed strictly in a sequential order where each task must be completed 100% before the next one begins, causing the program to block during idle or waiting periods.
  - Multithreading: Concurrency is <mark>achieved by creating multiple threads of execution within a process</mark>. Threads can run independently, and <mark>the operating system scheduler **decides when to switch between them**</mark>.
  - Asynchronous Programming: <mark>Concurrency is achieved through **non-blocking operations**</mark>. The program <mark>can continue executing other tasks **while waiting for** certain operations</mark> (e.g., I/O or network requests) to complete.
- **Parallelism:**
  - Synchronous Programming: Does **<mark>not inherently support parallelism</mark>**. <mark>Tasks are executed <u>sequentially</u></mark>.
  - Multithreading: Supports parallelism as <mark>multiple threads can execute tasks <u>simultaneously on multiple CPU cores</u></mark>.
  - Asynchronous Programming: Does not necessarily imply parallelism but enables efficient use of resources by <mark>allowing tasks to **<u>run concurrently without blocking the main thread</u>**</mark>.
- **Programming Model:**
  - Synchronous Programming: <mark>**Uses a straightforward, blocking model**</mark>. Tasks are executed **<u><mark>one after another in a linear fashion</mark></u>**.
  - Multithreading: Requires explicit creation and management of threads, often **<mark>involving synchronization mechanisms like locks</mark>**.
  - Asynchronous Programming: <mark>Utilizes non-blocking constructs like <u>**callbacks, promises, or async/await**</u></mark>. Facilitates the creation of non-blocking code, making it easier to handle multiple tasks concurrently.
- **Complexity and Safety:**
  - Synchronous Programming: Typically **<u>less complex and easier to understand</u>**, but **<u>can lead to blocking and potential inefficiencies</u>**.
  - Multithreading: <mark>Introduces complexities related to thread synchronization</mark>, shared data safety, and potential race conditions. <mark><u>**Requires careful management to avoid issues like deadlocks**</u></mark>.
  - Asynchronous Programming: Can be more readable for certain tasks, especially I/O-bound operations. However, <mark>**<u>managing callbacks and ensuring proper error handling can introduce complexity</u>**</mark>.
- **Resource Overhead:**
  - Synchronous Programming: Generally has lower resource overhead compared to multithreading.
  - Multithreading: <u>Can have higher resource overhead due to the creation and management of multiple threads</u>. Synchronization mechanisms add to complexity.
  - Asynchronous Programming: Tends to have lower resource overhead <u>**as it doesn't require creating and managing multiple threads**</u>.
- **Use Cases:**
  - Synchronous Programming: <mark>Well-suited for simpler applications or scenarios where blocking operations do not significantly impact performance</mark>.
  - Multithreading: <mark>Suitable for **<u>CPU-intensive tasks (CPU-Bound tasks) that can be parallelized</u>**, such as complex calculations or image processing</mark>.
  - Asynchronous Programming: <mark>Well-suited for **<u>scenarios where I/O operations are prevalent</u>**, such as networking, file I/O, or database queries</mark>.

In summary, the choice between synchronous programming, multithreading, and asynchronous programming <mark>**<u>depends on the nature of the tasks</u>**</mark>, the specific requirements of the application, and <mark><u>**the desired balance between simplicity, concurrency, and parallelism**</u></mark>. In many cases, **<u>*a combination of these techniques may be used to achieve optimal performance*</u>**.
