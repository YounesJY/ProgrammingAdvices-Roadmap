## Mutable and Immutable Types

    In C#, types are classified as either mutable or immutable based on whether their instances can be modified after they are created. Understanding the difference between mutable and immutable types is crucial for designing robust and maintainable code.

### Mutable Types:

- Definition: Mutable types are types whose instances can be modified after they are created.
  - Characteristics: Properties or fields of a mutable type can be changed.
  - <mark>Changes to an instance affect the state of that instance</mark>.
  - Examples include classes, arrays, and custom objects where properties can be modified.

Example of a mutable class in C#:

```csharp
public class MutablePerson
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Immutable Types:

- Definition: Immutable types are types whose instances cannot be modified after they are created.
  - Characteristics:Properties or fields of an immutable type cannot be changed after the instance is created.
  - <mark>Any operation that appears to modify the instance actually returns a new instance with the desired changes.</mark>
  - Examples include strings, tuples, and some built-in value types.

Example of an immutable class in C#:

```csharp
public class ImmutablePerson
{
    public string Name { get; }
    public int Age { get; }
    public ImmutablePerson(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

### Pros and Cons:

Mutable Types:

- - Pros:
    -  More flexible for certain scenarios.
    - <mark>Can be more memory-efficient if state changes frequently.</mark>
  - Cons: 
    - Prone to unintended side effects.
    - <mark>May require additional effort to maintain consistency (MultiThreading Env ?)</mark>.

Immutable Types:

- - Pros: 
    - <mark>Safer and less error-prone</mark> since instances cannot be modified.
    - Easier to reason about and maintain.
  - Cons:
    - <mark>Creating a new instance for each modification can be less memory-efficient for certain scenarios.</mark>

### Guidelines:

- - **<mark>Favor Immutability: Whenever possible, prefer using immutable types to reduce bugs related to unintended state changes.</mark>**
  - <mark><u>Use Mutability When Necessary</u></mark>: There are scenarios where mutability is more appropriate, such as when <mark>frequent state changes are expected or when performance is a critical concern</mark>.




### Example Usage:

```csharp
// Mutable example
MutablePerson person1 = new MutablePerson { Name = "Alice", Age = 30 };
person1.Age = 31; // Mutable state change
// Immutable example
ImmutablePerson person2 = new ImmutablePerson("Bob", 25);
// person2.Age = 26; // Compiler error - immutable type
ImmutablePerson newPerson = new ImmutablePerson(person2.Name, 26); // Creating a new instance with the desired change
```

In practice, the choice between mutable and immutable types depends on the specific requirements and constraints of your application and the use case at hand. Immutable types are often preferred in scenarios where predictability and avoiding unintended side effects are crucial.


