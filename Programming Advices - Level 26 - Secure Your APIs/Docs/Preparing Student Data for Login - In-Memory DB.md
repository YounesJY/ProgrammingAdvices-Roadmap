# Preparing Student Data for Login (In-Memory DB)

---

## 1. Introduction

So far, you clearly understand:

- ✅ What authentication is
- ✅ What JWT is
- ✅ Why APIs need identity
- ✅ How JWT represents identity

Now we take the first real preparation step before issuing JWTs:

🧱 **<mark>Preparing existing student data so the API can authenticate users.</mark>**

📌 This lesson prepares the **data layer only** — not login logic yet.

---

## 2. Why We Must Prepare Data Before Login

Authentication requires:

1️⃣ **<mark>Identity</mark>** (Who is the student?)
2️⃣ **<mark>Credentials</mark>** (Can we verify them securely?)

Your existing data contains only academic fields, so authentication is impossible without preparation.

---

## 3. Minimum Authentication Data Required

Each student <mark>**must have**</mark>:

- 🔹 **Email** → unique login identifier
- 🔹 **PasswordHash** → secure credential storage
- 🔹 **Role** → Student or Admin

---

## 4. Design Decision (Important)

<mark>To keep the course **<u>focused</u>**</mark>:

- 🔹 We assume known passwords
- 🔹 We do **NOT** build registration
- 🔹 We store <mark>**only**</mark> hashed passwords

📌 This simulates a real production database.

---

## 5. Step 1 — Extend the Student Model

We safely extend the existing Student entity.

📌 Nothing is removed.
📌 Existing endpoints keep working.

```csharp
namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        // Authentication-related fields
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
```

---

## 6. Step 2 — Install BCrypt (Required)

Before hashing passwords, we must install the BCrypt library.

### 📦 Install BCrypt Package

**Instructions:**

1. Go to **NuGet Package Manager** (right-click project → Manage NuGet Packages).
2. In the **Browse** tab, search for: `BCrypt.Net-Next`.
3. Select the first one (verified by Microsoft/maintained by the community).
4. Check your project, then click **Install**, then **Apply**.

📌 **This package provides:**

- Secure password hashing
- Automatic salt generation
- Safe password verification

> ⚠️ Without this step, <mark>**hashing code will not compile**</mark>.

---

## 7. Step 3 — Why We Store PasswordHash (Not Password)

Storing passwords directly is:

- ❌ Dangerous
- ❌ Unacceptable
- ❌ A security disaster

**<mark>Correct approach</mark>:**

- 🔹 Hash the password
- 🔹 Store only the hash
- 🔹 <mark>**Never**</mark> store the password itself

---

## 8. Step 4 — Hashing Strategy: Where Is the Salt?

We use **BCrypt**, which:

- 🔹 Automatically generates a unique salt
- 🔹 Embeds the salt inside the hash
- 🔹 Requires no manual salt handling

> 📌 **<mark>We store one value only</mark>:** `PasswordHash`.

---

## 9. Step 5 — Preparing In-Memory Student Data

### Assumed Passwords (For Learning Only)

| ID  | Name       | Email                    | Password    | Role    |
| --- | ---------- | ------------------------ | ----------- | ------- |
| 1   | Ali Ahmed  | `ali.ahmed@student.com`  | `password1` | Student |
| 2   | Fadi Khail | `fadi.khail@student.com` | `password2` | Student |
| 3   | Ola Jaber  | `ola.jaber@student.com`  | `password3` | Student |
| 4   | Alia Maher | `alia.maher@admin.com`   | `admin123`  | Admin   |

### ✅ Updated In-Memory Data Simulation

```csharp
using StudentApi.Models;
using BCrypt.Net;

namespace StudentApi.DataSimulation
{
    public class StudentDataSimulation
    {
        public static readonly List<Student> StudentsList = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "Ali Ahmed",
                Age = 20,
                Grade = 88,
                Email = "ali.ahmed@student.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1"),
                Role = "Student"
            },
            new Student
            {
                Id = 2,
                Name = "Fadi Khail",
                Age = 22,
                Grade = 77,
                Email = "fadi.khail@student.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password2"),
                Role = "Student"
            },
            new Student
            {
                Id = 3,
                Name = "Ola Jaber",
                Age = 21,
                Grade = 66,
                Email = "ola.jaber@student.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password3"),
                Role = "Student"
            },
            new Student
            {
                Id = 4,
                Name = "Alia Maher",
                Age = 19,
                Grade = 44,
                Email = "alia.maher@admin.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = "Admin"
            }
        };
    }
}
```

---

## 10. Characteristics

- 🧬 No registration endpoint
- 🧬 No JWT yet
- 🧬 Secure hashing with salt
- 🧬 Automatic salt management
- 🧬 Production-grade thinking

---

## 11. Conclusion

You have now:

- ✅ Installed BCrypt correctly
- ✅ Stored passwords securely with salt
- ✅ Prepared realistic authentication data
- ✅ Built a solid foundation for login & JWT

> 📌 **You <mark>never need</mark> to "get the salt" manually — <mark>BCrypt already did that for you.</mark>**

In the next lesson, we will finally implement:

🔐 **Login Endpoint — Verifying Passwords & Issuing JWT**

This is where preparation turns into real security 🚀.

---

## 📌 Summary

| Concept                   | Decision                                   |
| ------------------------- | ------------------------------------------ |
| **New fields added**      | `Email`, `PasswordHash`, `Role`            |
| **Hashing library**       | `BCrypt.Net-Next` (NuGet)                  |
| **Salt handling**         | Automatic — embedded in the hash by BCrypt |
| **Stored value**          | `PasswordHash` only, never plaintext       |
| **Roles used**            | `Student`, `Admin`                         |
| **Registration endpoint** | Not built — simulated data only            |
| **Existing endpoints**    | Unchanged — data extended, not replaced    |
| **Next step**             | Login endpoint + JWT issuance              |

**Core principle:** <mark>authentication needs **identity and credentials**</mark>. You just built the credential half — hashed, salted, and production-realistic — without touching any endpoint logic. The API is now ready to *verify* someone, even though it can't yet *issue* them anything. That is the next lesson.

---


