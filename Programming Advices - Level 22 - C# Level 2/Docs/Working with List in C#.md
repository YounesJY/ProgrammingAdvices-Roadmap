# Working with `List<int>` in C#

## 1. Code

```csharp
using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        numbers.Add(4);
        numbers.Add(5);

        Console.WriteLine("Number of items in the list: " + numbers.Count);

        Console.WriteLine(numbers[0]);
        Console.WriteLine(numbers[1]);
        Console.WriteLine(numbers[2]);
        Console.WriteLine(numbers[3]);
        Console.WriteLine(numbers[4]);

        Console.WriteLine("Changing the value of item 2 of the list to 500:");
        numbers[1] = 500;
        Console.WriteLine(numbers[1]);

        Console.ReadKey();
    }
}
```

**Output**

```
Number of items in the list: 5
1
2
3
4
5
Changing the value of item 2 of the list to 500:
500
```

---

## 2. Key Concepts

| Concept          | Code                | Explanation                               |
| ---------------- | ------------------- | ----------------------------------------- |
| Initialization   | `new List<int>()`   | Creates an empty list of integers         |
| Adding elements  | `numbers.Add(x)`    | Appends to the end; `Count` grows by one  |
| `Count` property | `numbers.Count`     | Current number of elements                |
| Index access     | `numbers[i]`        | Reads the element at zero-based index `i` |
| Modifying        | `numbers[1] = 500`  | Replaces the element at index 1           |
| Output           | `Console.WriteLine` | Prints to the console                     |
| Pause            | `Console.ReadKey()` | Blocks until a key is pressed             |

---

## 3. Notes

- **`Add` is O(1) amortized.** Occasional resizes copy the internal array, but the average cost per insert stays constant.
- **Index access is O(1).** `List<T>` is array-backed, so `numbers[i]` is a direct memory read.
- **`numbers[1] = 500` modifies in place.** No allocation, no resize — a slot overwrite.
- **`Count` is a property, not a method.** No parentheses.
- **Out-of-range index throws `ArgumentOutOfRangeException`.** The example stays in range; that is the failure mode to expect.
- **Modifying during iteration is a separate hazard.** The example does not iterate, so it is safe. `foreach` with an `Add` or `Remove` inside throws `InvalidOperationException`.

---

📌 **Summary**

| Operation           | Syntax                     | Complexity                           |
| ------------------- | -------------------------- | ------------------------------------ |
| Create              | `new List<int>()`          | O(1)                                 |
| Add                 | `numbers.Add(x)`           | O(1) amortized                       |
| Count               | `numbers.Count`            | O(1)                                 |
| Read by index       | `numbers[i]`               | O(1)                                 |
| Write by index      | `numbers[i] = x`           | O(1)                                 |
| Out-of-range access | `numbers[i]`, `i >= Count` | Throws `ArgumentOutOfRangeException` |

---
