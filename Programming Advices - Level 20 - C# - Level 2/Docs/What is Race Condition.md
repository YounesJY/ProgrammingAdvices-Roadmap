## What is Race Condition?



    A race condition is a situation in concurrent programming where <mark>**the behavior of a program <u>depends on the relative timing of events</u>**</mark>, such as the order in which threads are scheduled to run.

In other words, a race condition <mark>occurs when the correctness of a program's execution depends on the **unpredictable interleaving of operations** from multiple threads</mark>.

Race conditions can lead to unexpected and undesirable outcomes, including:

- Data corruption,
- Application crashes
- or other forms of incorrect behavior.

They are particularly common in multithreaded or parallel programming, where multiple threads execute concurrently and may access shared resources or variables.
