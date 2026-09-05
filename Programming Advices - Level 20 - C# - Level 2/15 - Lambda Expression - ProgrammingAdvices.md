## Lambda Expression

Lambda expressions are often preferred over normal (named) functions in specific scenarios because they offer some advantages, such as <mark>**conciseness, inline definition, and the ability to create anonymous functions.**</mark> Here are a few reasons why you might choose to use lambda expressions over normal functions:

1. **<u>Conciseness</u>:** Lambda expressions are typically more concise than defining a separate function, especially for small, simple operations. This leads to more readable and less verbose code.
2. **Anonymous Functions:** Lambda expressions allow you to create anonymous functions on the fly without needing to declare a separate named function. <mark>This is particularly useful when you need a function for a one-time or specific purpose</mark>.
3. **<u>Inline Definitions</u>:** Lambda expressions can be defined inline within a statement or method call, making it easy to pass functions as arguments to methods or use them in LINQ queries without defining separate functions.
4. **<u>Readability</u>:** In some cases, a lambda expression's compact and in-place definition can improve code readability, as it keeps the code closer to the point where the function is used.
5. **<u>Functional Programming</u>:** Lambda expressions <mark>align with functional programming principles</mark>, which can lead to more expressive and declarative code in certain contexts, such as <mark>LINQ queries and event handling</mark>.
6. **<u>Flexibility</u>:** Lambda expressions can capture variables from their surrounding scope (known as closures), allowing you to create more flexible and context-aware functions.

While lambda expressions offer these advantages, it's important to note that there are cases where defining a separate named function is more appropriate. <mark>You would typically use named functions when you need to reuse the same logic in multiple places</mark>, promote code organization, or when the function is more complex and requires detailed documentation.

In summary, lambda expressions are a valuable tool in C# for creating concise, **inline, and anonymous functions**, which can improve code readability and maintainability in specific scenarios, such as LINQ queries, event handlers, and small, one-off operations.



> 
