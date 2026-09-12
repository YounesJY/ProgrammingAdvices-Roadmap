## C# Level 2 - Questions & Answers

## 1. String Prefixes

**Question:**
What is `@` and `$` and others when prefixing a string?

**Answer:**

| Prefix       | Name                | Purpose                                                                       |
| ------------ | ------------------- | ----------------------------------------------------------------------------- |
| `@`          | Verbatim String     | Ignores escape sequences (e.g., `\n`, `\t`). Useful for file paths and regex. |
| `$`          | Interpolated String | Allows embedding expressions inside `{ }`.                                    |
| `$@` or `@$` | Both combined       | Verbatim + Interpolated.                                                      |

**Examples:**

```csharp
string path = @"C:\Users\poste\Documents";  // No need to escape backslashes
string name = "Younes";
string greeting = $"Hello, {name}!";        // Interpolation
string combined = $@"Path: {path}";         // Both combined
```

## 2. Serialization

**Questions:**

- What is serialization and object state / data structure?
- Purpose? Store state or send over network? Data persistence, local storage, saving? Cross-language communication?
- Binary vs XML vs JSON? Performance vs Interoperability?

**Answer:**

> **Serialization** = <mark>Converting an object's state into a format that can be stored or transmitted, then reconstructed later (deserialization)</mark>.

**Purpose:**

| Use Case           | Description                                                  |
| ------------------ | ------------------------------------------------------------ |
| **Persistence**    | <mark>Save object state to disk</mark> (file, database)      |
| **Network**        | Send objects over the wire <mark>(APIs, sockets)</mark>      |
| **Cross-language** | <mark>Share data between different systems</mark> (JSON/XML) |
| **Caching**        | Store objects <mark>for quick retrieval</mark>               |

**Binary vs XML vs JSON:**

| Format     | Performance | Interoperability           | Human-Readable |
| ---------- | ----------- | -------------------------- | -------------- |
| **Binary** | Fastest     | Lowest (platform-specific) | No             |
| **XML**    | Slowest     | Highest (verbose)          | Yes            |
| **JSON**   | Fast        | High                       | Yes            |

---

## 3. `[Serializable]` Attribute

**Question:**
What is `[Serializable]`? Is it like an annotation in Java or is it an interface (marker interface)?

**Answer:**
`[Serializable]` is an **attribute**, not an interface.

> - <mark>In Java, you'd use `implements Serializable` (marker interface).</mark>
> - <mark>In C#, you use `[Serializable]` (attribute).</mark>

Both achieve the same goal: marking a class as serializable. But C# uses attributes (metadata) instead of marker interfaces.

```csharp
[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

> **Note:** In modern .NET (Core/5+), `[Serializable]` is mainly used for binary serialization <mark>(which is now discouraged)</mark>. JSON/XML serializers don't require it.

---

## 4. Mutable vs Immutable

**Questions:**

- Atomic vs Mutable in a multi-threaded environment?
- When to favor immutability vs mutability?

**Answer:**

| Aspect             | Immutable                                       | Mutable                                                                |
| ------------------ | ----------------------------------------------- | ---------------------------------------------------------------------- |
| **Thread Safety**  | ✅ Safe by default                               | ❌ <mark>Requires locks/synchronization</mark>                          |
| **Predictability** | ✅ Value never changes                           | ❌ <mark>Can change unexpectedly</mark>                                 |
| **Memory**         | ❌ New instance per change                       | ✅ <mark>Efficient for frequent updates</mark>                          |
| **Use Case**       | <mark>Shared data, keys, multi-threading</mark> | <mark>Frequent updates</mark>, <mark>performance-critical loops</mark> |

<mark><u>Rule of Thumb</u></mark>:

> - <mark>Favor **immutability**</mark> for safety and predictability.
> - <mark>Use **mutability**</mark> when performance and memory matter (e.g., `StringBuilder`, `List<T>`).

---

## 5. Windows Registry

**Questions:**

- What is the Windows Registry?
- Is it like a database? Keys & Subkeys?

**Answer:**

> The **Windows Registry** is a<mark> hierarchical database that stores low-level settings for the OS and applications</mark>.

**Structure:**

```
HKEY_CURRENT_USER
    └── SOFTWARE
        └── DVLD
            ├── username (value)
            └── password (value)
```

| Term                  | Description                                    |
| --------------------- | ---------------------------------------------- |
| <mark>**Hive**</mark> | Top-level root key (e.g., `HKEY_CURRENT_USER`) |
| **Key**               | Folder-like container (e.g., `SOFTWARE\DVLD`)  |
| **Subkey**            | Nested key inside another key                  |
| **Value**             | Actual data stored (string, int, binary)       |

**C# Example:**

```csharp
using Microsoft.Win32;

string keyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
Registry.SetValue(keyName, "username", "Younes");
string username = Registry.GetValue(keyName, "username", null) as string;
```

---

## 6. Event Log

**Question:**
What is the Event Log? Why is it useful?

**Answer:**

> The **Windows Event Log** is <mark>a centralized logging system for applications and the OS</mark>.

**Benefits:**

- **<mark>Centralized storage for logs</mark>**
- **<mark>Monitoring and troubleshooting</mark>**
- **<mark>Auditing and compliance</mark>**
- <mark>Can be viewed via Event Viewer (`eventvwr.msc`)</mark>

**C# Example:**

```csharp
using System.Diagnostics;

EventLog.WriteEntry("MyApp", "Application started", EventLogEntryType.Information);
```

**Verdict:**

> <mark>A massive addition for monitoring and troubleshooting. :)</mark>

---

## 7. App Config

![](C:/Users/poste/AppData/Roaming/marktext/images/2026-09-11-17-22-03-image.png)

**Questions:**

- What is App Config? Why use it?
- <mark>Remember JDBC hardcoded values and different stages of storing credentials? (hardcoded, separate class, read from properties file)</mark>
- <mark>From hardcoded to auto-deploy on client side due to different servers, connection strings... (will you change source code each time then build?)</mark>
- In C#/.NET, what is App Config?
- <mark>Naming difference from Dev to Deployment/Build on production?</mark>
- <mark>AppConfig vs Registry? Why?</mark>
- <mark>AppConfig vs appsettings.json? Why?</mark>

**Answer:**

**What:**
App Config (`App.config` or `appsettings.json`) is a configuration file that stores settings (connection strings, API keys, feature flags) outside the source code.

**Why:**

- No hardcoding
- Change settings without recompiling
- Different environments (Dev, Staging, Production) can have different configs

**Evolution of Configuration:**

| Stage | Approach                      | Problem                                 |
| ----- | ----------------------------- | --------------------------------------- |
| 1     | Hardcoded values              | Must recompile to change                |
| 2     | Separate class                | Still compiled into the app             |
| 3     | Properties file               | Better, but not standard in .NET        |
| 4     | App.config / appsettings.json | ✅ Standard, flexible, environment-aware |

**AppConfig vs Registry vs appsettings.json:**

| Feature         | App.config          | Registry                  | appsettings.json  |
| --------------- | ------------------- | ------------------------- | ----------------- |
| **Best For**    | .NET Framework apps | Windows-specific settings | .NET Core/5+ apps |
| **Portability** | Cross-platform      | Windows only              | Cross-platform    |
| **Readability** | XML (verbose)       | Binary/hive-based         | JSON (clean)      |
| **Deployment**  | Copied with app     | Written at install time   | Copied with app   |

**Naming Differences:**

- **Dev:** `App.config`
- **Build/Deploy:** `MyApp.exe.config` (renamed automatically)

---

## 8. StringBuilder

**Questions:**

- WHAT?
- WHY?
- HOW?
- WHEN?

**Answer:**

**WHAT:**
`StringBuilder` is a mutable string-like class in `System.Text`.

**WHY:**
Strings are immutable. Every concatenation creates a new string object. For heavy modifications (loops, building large strings), this is inefficient.

**HOW:**

```csharp
using System.Text;

StringBuilder sb = new StringBuilder();
sb.Append("Hello");
sb.Append(" ");
sb.Append("World");
string result = sb.ToString(); // "Hello World"
```

**WHEN:**

- Building large strings in loops
- Frequent modifications
- Performance-critical string manipulation

**Example (Performance Difference):**

```csharp
// ❌ Slow — creates 10,000 strings
string result = "";
for (int i = 0; i < 10000; i++)
    result += i.ToString();

// ✅ Fast — uses one buffer
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 10000; i++)
    sb.Append(i);
string result = sb.ToString();
```

**Rule of Thumb:**

- Few concatenations → `+` is fine
- Many concatenations (loops, large data) → `StringBuilder`

---

## Summary of Sections:

| #   | Topic                | Questions Covered                           |
| --- | -------------------- | ------------------------------------------- |
| 1   | String Prefixes      | `@`, `$`, others                            |
| 2   | Serialization        | What, why, binary vs XML vs JSON            |
| 3   | `[Serializable]`     | Attribute vs interface                      |
| 4   | Mutable vs Immutable | Multi-threading, when to use each           |
| 5   | Windows Registry     | Structure, keys, subkeys                    |
| 6   | Event Log            | Purpose, benefits                           |
| 7   | App Config           | What, why, vs Registry, vs appsettings.json |
| 8   | StringBuilder        | WHAT, WHY, HOW, WHEN                        |

---

This format keeps your original questions while adding structured answers. Want me to adjust any section? 🎯
