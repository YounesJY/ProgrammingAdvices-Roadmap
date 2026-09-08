## Conditional Attribute Examples

### **Conditional Attributes?**

﻿In C#, the term "conditional attribute" is commonly associated with the `[Conditional]` attribute. The `[Conditional]` attribute is <mark>used in C# to conditionally include or exclude methods from compilation **<u>based on the specified compilation symbols</u>**</mark>. **<u>It doesn't affect the runtime behavior of the code</u>** but rather <mark>influences whether or not a particular method **<u>is included in the compiled output</u>**</mark>.

Below is a simple C# program that demonstrates the use of the `Conditional` attribute. In this example, the `DebugMethod` will only be compiled and executed if the `DEBUG` compilation symbol is defined.

```csharp
using System;
using System.Diagnostics;
public class MyClass
{
    [Conditional("DEBUG")]
    public void DebugMethod()
    {
        Console.WriteLine("Debug method executed.");
    }
    public void NormalMethod()
    {
        Console.WriteLine("Normal method executed.");
    }
}
class Program
{
    static void Main()
    {
        MyClass myClass = new MyClass();
        // Call the methods
        myClass.DebugMethod();  // This will only be executed in DEBUG builds
        myClass.NormalMethod(); // This will always be executed
        Console.ReadLine();
    }
}
```

Here's how you can compile and run this program:

- In a development environment like Visual Studio, you can set the build configuration to "Debug" or "Release" to see the difference. The `DebugMethod` will be included only in the "Debug" build.
- Alternatively, you can use the `#define` directive to manually define the `DEBUG` symbol in your code, like this:

<mark>#define DEBUG</mark>

Place the `#define DEBUG` line at the beginning of your code file, and then both `DebugMethod` and `NormalMethod` will be included in the compilation.

<mark>Remember that the `Conditional` attribute affects the inclusion of the method at compile-time</mark>, not **<u>runtime</u>**. So, if the `DEBUG` symbol is not defined during compilation, the `DebugMethod` calls will not be present in the compiled code.

**Custom Symbol Example:**

Example of using the `Conditional` attribute in combination with trace statements, you might be interested in incorporating conditional compilation of trace statements based on the presence of a compilation symbol. Here's a simple example:

```csharp
#define TRACE_ENABLED
using System;
using System.Diagnostics;
public class TraceExample
{
    [Conditional("TRACE_ENABLED")]
    public static void LogTrace(string message)
    {
        Console.WriteLine($"[TRACE] {message}");
    }
    public static void Main()
    {
        LogTrace("This trace message will only be included if TRACE_ENABLED is defined.");
        Console.WriteLine("Rest of the program.");
        Console.ReadLine();
    }
}
```

In this example:

- The `TRACE_ENABLED` compilation symbol is defined at the beginning of the file using `#define`.
- The `LogTrace` method is marked with the `Conditional("TRACE_ENABLED")` attribute. This means that the method calls will only be included in the compiled code if the `TRACE_ENABLED` symbol is defined during compilation.
- In the `Main` method, a trace message is logged using `LogTrace`, and it will only be included if `TRACE_ENABLED` is defined.

You can experiment with this example by commenting out or removing the `#define TRACE_ENABLED` line. When the symbol is not defined, the trace messages will be excluded during compilation.

Remember, the `Conditional` attribute is a compile-time directive, so it affects what gets compiled into the executable based on the presence or absence of the specified compilation symbols.
