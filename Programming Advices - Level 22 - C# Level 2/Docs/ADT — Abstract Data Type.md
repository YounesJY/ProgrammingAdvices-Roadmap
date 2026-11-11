# ADT — Abstract Data Type

## 1. Definition

An **Abstract Data Type (ADT)** is <mark>**a logical description**</mark> of a data structure: what data it holds, what operations it supports, what those operations do, and what invariants hold. It says nothing about how the data is stored or how the operations are implemented.

- **ADT** = abstract specification (what)
- **Data structure** = concrete implementation (how)
- A data structure **implements** an ADT

---

## 2. What an ADT Defines

- **What data it holds** (conceptually)
- **What operations are allowed**
- **What those operations do** (behavioral contract)
- **What invariants hold**

---

## 3. What an ADT Does Not Define

- The underlying storage (array, linked list, hash table, tree)
- The time and space complexity of operations
- Language-level implementation details

---

## 4. Classic Examples

| ADT              | Operations (contract)                   | Possible implementations                          |
| ---------------- | --------------------------------------- | ------------------------------------------------- |
| List             | insert, remove, get, size               | Array, linked list, dynamic array                 |
| Stack            | push, pop, peek, isEmpty                | Array-backed, linked-list-backed                  |
| Queue            | enqueue, dequeue, peek, isEmpty         | Array-backed, linked-list-backed, circular buffer |
| Map / Dictionary | put, get, remove, containsKey           | Hash table, balanced tree, sorted array           |
| Set              | add, remove, contains, union, intersect | Hash set, tree set, bit vector                    |
| Priority Queue   | insert, extractMin/Max, peek            | Binary heap, Fibonacci heap, sorted list          |

---

## 5. Why ADT Matters

- **<mark>Multiple implementations, one contract</mark>.** A Stack ADT can be built on an array or a linked list. Callers should not care which. That is the point of abstraction.
- **<mark>Trade-offs live in the implementation</mark>.** The same ADT can have different complexity depending on the implementation. A Map as a hash table gives O(1) average lookup; as a balanced tree it gives O(log n) but keeps order. **<mark><u>The ADT does not choose for you</u></mark>** — the implementation does.
- **Interface before code.** In C#, an ADT maps naturally to an interface or abstract class. `IStack<T>` describes the contract; `ArrayStack<T>` and `LinkedStack<T>` implement it.
- **Design vocabulary.** When someone says "use a set here", they mean the ADT (membership, uniqueness), not a specific `HashSet<T>`. The implementation choice <mark>**comes after**</mark> the ADT choice.

---

## 6. ADT vs Data Structure

- **ADT** = abstract specification (what)
- **Data structure** = concrete implementation (how)

Stack is an ADT. `System.Collections.Generic.Stack<T>` is a data structure that implements it (array-backed).

### Quick check

> Is `List<T>` an ADT or a data structure?

`List<T>` is a concrete data structure (array-backed dynamic list) that implements the List ADT. <mark>**The ADT itself is the contract**</mark>: insert, remove, index access, size. `LinkedList<T>` implements the same List ADT with a different data structure.

---

## 7. Why ADT Sits in Course 12

ADT is the mental model that makes Course 22 work.

- When the course says "implement `IList`", you are implementing an ADT contract.
- When it says "choose between `List<T>` and `LinkedList<T>`", you are choosing between two data structures for the same ADT **<mark>based on trade-offs</mark>**.

Without the ADT concept, the interface-implementation arc in Level 22 **<mark>has no foundation</mark>**.

---

📌 **Summary**

| Question                     | Answer                                                                                                 |
| ---------------------------- | ------------------------------------------------------------------------------------------------------ |
| What is an ADT?              | A logical spec: data + operations + behavior + invariants, without implementation                      |
| What does it define?         | Operations, behavioral contract, invariants                                                            |
| What does it not define?     | Storage, complexity, language-level details                                                            |
| Relation to data structure   | ADT is the contract; a data structure implements it                                                    |
| C# mapping                   | Interface or abstract class (e.g., `IList<T>`, `IDictionary<TKey,TValue>`)                             |
| Where it appears in Level 22 | Every "implement `IEnumerable` / `ICollection` / `IList` / `IDictionary` / `ISet`" lesson              |
| Why it matters               | Separates *<mark>what</mark>* from *<mark>how</mark>*; makes trade-offs a design decision, not a guess |


