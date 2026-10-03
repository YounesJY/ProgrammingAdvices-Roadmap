# P/Invoke — Declaring Native Functions in C#

---

## 1. What the declaration is

```csharp
[DllImport("user32.dll")]
static extern int GetSystemMetrics(int nIndex);
```

This is a <mark>**method declaration without a body**</mark> — the C# <mark>equivalent of a C/C++ function prototype</mark> in a header file.

If you've written C++ and used forward declarations to avoid ordering conflicts:

```cpp
// C++ header
int Compute(int x);        // declaration only, no body
```

...you already understand the pattern. <mark>C# uses the same idea, <u>with one addition</u></mark>: the `[DllImport]` attribute <mark>tells the runtime **which native library** contains the implementation</mark>.

---

## 2. Anatomy of the declaration

| Part                                | Meaning                                                    |
| ----------------------------------- | ---------------------------------------------------------- |
| `[DllImport("user32.dll")]`         | Attribute: "the implementation lives in user32.dll"        |
| `static`                            | Not tied to an instance — call it as `GetSystemMetrics(0)` |
| `extern`                            | "No body here. The body is outside C#."                    |
| `int GetSystemMetrics(int nIndex);` | Signature only — no `{ ... }`                              |

The `extern` keyword is what signals to the compiler: <mark>*this method's body is not defined in managed code.*</mark>

Without `[DllImport]`, an `extern` method has nowhere to go — the runtime can't find the native symbol. The attribute is what makes the declaration meaningful.

---

## 3. What happens at runtime

<mark><u>**Nothing at compile time except metadata**</u></mark>. At the **first call**, the CLR (Common Language Runtime) performs a series of steps:

1. Reads the `[DllImport]` metadata → finds the DLL name.
2. Loads the DLL into the process (if not already loaded).
3. Resolves the exported symbol (`GetSystemMetrics`).
4. **Marshals** managed arguments into their native form (`int` → `int`, `string` → `char*`, etc.).
5. Calls the native function.
6. **Marshals** the return value back to managed form.

> <mark>This entire mechanism is called **P/Invoke** (Platform Invocation Services).</mark>

---

## 4. C# extern vs. C++ prototype

| Aspect                     | C++ prototype                                                                 | C# `extern` + `[DllImport]`                           |
| -------------------------- | ----------------------------------------------------------------------------- | ----------------------------------------------------- |
| Where implementation lives | Another `.cpp` file in the same program                                       | An external native DLL (Windows API, C library, etc.) |
| Resolved at                | Link time                                                                     | Runtime (first call)                                  |
| Purpose                    | <mark>Avoid ordering conflicts</mark>, separate interface from implementation | <mark>Call native code from managed code</mark>       |
| Body                       | Defined in another translation unit                                           | <mark>Not in managed code at all</mark>               |
| Attribute needed           | No                                                                            | Yes — `[DllImport]`                                   |

The C# version is a superset: it's a declaration without a body (like a C++ prototype) **plus** a runtime binding instruction (where the implementation lives).

---

## 5. A minimal pattern

```csharp
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);

    static void Main()
    {
        int screenWidth = GetSystemMetrics(0);   // SM_CXSCREEN
        int screenHeight = GetSystemMetrics(1);  // SM_CYSCREEN
        Console.WriteLine($"{screenWidth}x{screenHeight}");
    }
}
```

`System.Runtime.InteropServices` is where `DllImport` and marshalling types live.

---

## 6. Common pitfalls

- **Wrong DLL name** → `DllNotFoundException` at runtime.
- **Wrong function name** → `EntryPointNotFoundException` at runtime.
- **Wrong argument types** → data corruption or crashes (the runtime can't validate what the native function expects).
- **Missing `CharSet`** for string parameters → wrong encoding, mojibake, or crashes. Use `[DllImport("x.dll", CharSet = CharSet.Unicode)]` for wide-char APIs.
- **Calling conventions** — some native functions use `StdCall`, some `Cdecl`. Default is `StdCall` on Windows, but if the DLL expects `Cdecl`, you need `CallingConvention = CallingConvention.Cdecl`.

---

## 7. Summary

| Concept       | Meaning                                                                             |
| ------------- | ----------------------------------------------------------------------------------- |
| `[DllImport]` | Tells the runtime where the native implementation lives                             |
| `extern`      | "This method has no managed body"                                                   |
| Signature     | Matches the native function's signature exactly                                     |
| P/Invoke      | The runtime mechanism that loads, resolves, marshals, and calls the native function |
| Analogous to  | A C++ prototype, but with a runtime binding step                                    |

---

---

## Side question — Serialization vs. Marshalling

They're related but not the same. The confusion is understandable because both involve "converting data between representations."

|                  | **Serialization**                                                       | **Marshalling**                                                                                                   |
| ---------------- | ----------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------- |
| **What it does** | <mark>Converts an object into a transportable/persistable format</mark> | <mark>Converts data between two runtime representations</mark>, usually across a boundary                         |
| **Boundary**     | Between a program and a storage/transport medium                        | Between two execution contexts (managed ↔ native, process ↔ process, machine ↔ machine)                           |
| **Format**       | Explicit (JSON, XML, binary) — you can inspect it                       | Often implicit — the intermediate form isn't a documented artifact                                                |
| **Purpose**      | Save, send, cache, version                                              | Call a function, cross a language/runtime boundary                                                                |
| **Examples**     | `JsonSerializer.Serialize(obj)`, XML serialization                      | Marshalling `string` → `char*` when calling a Win32 API via P/Invoke, <mark>marshalling across an RPC call</mark> |
| **Reversible?**  | <mark>Usually yes</mark> — you can deserialize back                     | <mark>Sometimes yes, but not always in the same form</mark>                                                       |
| **Persistent?**  | Yes — you can write it to disk                                          | Usually transient — lives only during the operation                                                               |

**The one-line distinction:**

> Serialization converts an object to a **format** (JSON, XML, binary) so it can be stored or transmitted. Marshalling converts data between **runtime representations** across a boundary (managed to native, process to process).

**Where they overlap:** some serializers internally marshal data. And some RPC systems marshal by serializing. <mark>But they're conceptually distinct</mark> — serialization has a *format*, marshalling has a *boundary*.

**Concrete example tying back to P/Invoke:** when you call `GetSystemMetrics(0)`, an `int` crosses from managed code to native code. That's **marshalling** — <mark>it's a boundary crossing</mark>. There's no JSON, no XML, no persistent format. <mark>Just a value translated from one runtime's representation to another's</mark>.

---
