# Policy-Based Authorization & Advanced Rules

**<mark>Centralize Ownership Logic and Build Scalable Security</mark>**

---

## 1. Introduction

So far, you implemented ownership checks like this:

- Read the authenticated user ID from JWT
- Compare it with the `{id}` in the route
- Allow access if the user is the owner or an admin

> <mark>This approach is correct, but it **<u>does not scale</u>**.</mark>

As your API grows, repeating ownership logic inside controllers becomes:

- Hard to maintain
- Easy to forget
- Easy to implement inconsistently
- Difficult to audit

> <mark>This is where **policy-based authorization** becomes **<u>essential</u>**</mark>.

---

## 2. 🎯 Lesson Goal

By the end of this lesson, you will learn how to:

- 🔹 **<u>Centralize</u>** ownership logic
- 🔹 **<u>Use policies</u>** instead of inline checks
- 🔹 Build **<u>reusable, scalable</u>** authorization rules
- 🔹 Structure your project **<u>correctly</u>** for real-world APIs

---

## 3. 🔹 Why Policies Exist (The Real Problem They Solve)

Inline ownership checks inside controllers are:

- Correct, but duplicated
- Scattered across methods
- Hard to evolve when rules change

A **policy** moves security rules to <mark>**<u>one central place</u>**</mark>, so you can:

- <mark>**Reuse**</mark> the same rule in multiple endpoints
- Change the rule once and <mark>**affect the entire**</mark> API
- Keep controllers <mark>**<u>clean and focused</u>** on business logic</mark>

---

## 4. 🔹 What Is Policy-Based Authorization?

<mark>A **policy** is a **named security rule**</mark> evaluated by <mark>ASP.NET Core's **authorization system**</mark>.

Instead of writing this logic inside endpoints:

```csharp
if (!isAdmin && authenticatedStudentId != id)
    return Forbid();
```

You define the rule <mark>**<u>once</u>**</mark> and apply it through the authorization system:

```csharp
AuthorizeAsync(User, id, "StudentOwnerOrAdmin")
```

> Authorization becomes <mark>**centralized**, **consistent**, and **scalable**</mark>.

### How the evaluation actually works:

1️⃣ Controller receives the request
2️⃣ Controller knows the resource ID (`id`)
3️⃣ Controller asks the authorization system:

```csharp
AuthorizeAsync(User, id, "StudentOwnerOrAdmin")
```

4️⃣ ASP.NET Core then:

- Finds the <mark>**policy**</mark>
- Finds the <mark>**requirement**</mark>
- Finds the <mark>**handler**</mark>
- Passes the `id` to the handler
- Makes the final authorization decision

> 📌 **Important:**
> <mark>This is **resource-based authorization**</mark>, which <mark>**cannot be expressed correctly** using attributes alone</mark>.

This is exactly how ASP.NET Core <mark>**<u>is designed to handle</u> ownership checks**</mark>.

---

## 5. 🔹 Project Structure (<mark>**Very Important**</mark>)

Before writing any code, we must place it in the correct location.

**<mark>Authorization logic is security infrastructure</mark>, <mark><u>not business logic</u></mark>.**

### ✅ Recommended Project Structure

```text
StudentApi
│
├── Authorization
│   ├── StudentOwnerOrAdminRequirement.cs
│   └── StudentOwnerOrAdminHandler.cs
│
├── Controllers
│   ├── StudentsController.cs
│   └── AuthController.cs
│
├── Models
├── DataSimulation
├── Program.cs
```

📌 Everything related to authorization rules <mark>**lives in**</mark> the `Authorization` folder.

---

## 6. 🔹 Step 1 — Define the Ownership Requirement

📁 **Location:** `Authorization/StudentOwnerOrAdminRequirement.cs`

```csharp
using Microsoft.AspNetCore.Authorization;

// This class represents the authorization rule itself.
// It does NOT contain logic.
// It simply defines the requirement:
// "Owner OR Admin can access the student resource."
public class StudentOwnerOrAdminRequirement : IAuthorizationRequirement
{
}
```

📌 **<mark><u>Important concept</u></mark>:**

- This class <mark>is **<u>intentionally</u>** **empty**</mark>
- It represents <mark>**<u>what</u> rule exists**</mark>, <mark>not **<u>how</u> it is enforced**</mark>

---

## 7. 🔹 Step 2 — Create the Authorization Handler

📁 **Location:** `Authorization/StudentOwnerOrAdminHandler.cs`

```csharp
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// This authorization handler enforces the ownership rule for student resources.
// It checks whether the current user is either:
// - An Admin (full access), OR
// - The owner of the student record being requested
public class StudentOwnerOrAdminHandler
    : AuthorizationHandler<StudentOwnerOrAdminRequirement, int>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        StudentOwnerOrAdminRequirement requirement,
        int studentId)
    {
        // Admin override
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // Ownership check
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userId, out int authenticatedStudentId) &&
            authenticatedStudentId == studentId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
```

📌 **<mark><u>Important detail</u></mark>:**

- This <mark>is a **<u>*resource-based*</u>** handler</mark>
- The resource <mark>is the `studentId` </mark> being requested

---

## 8. 🔹 Step 3 — Register the Handler in `Program.cs`

```csharp
builder.Services.AddSingleton<IAuthorizationHandler, StudentOwnerOrAdminHandler>();
```

📌 This tells ASP.NET Core:

> "<mark>**When**</mark> this requirement <mark>is **evaluated**</mark>, <mark>**<u>use this handler</u>**</mark>."

---

## 9. 🔹 Step 4 — Register the Policy in `Program.cs`

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StudentOwnerOrAdmin", policy =>
        policy.Requirements.Add(new StudentOwnerOrAdminRequirement()));
});
```

Your API now <mark>has a **reusable, named policy**</mark>.

---

## 10. 🔹 Step 5 — Use the Policy in the Student Endpoint

📁 **Location:** `StudentsController`

```csharp
[HttpGet("{id}", Name = "GetStudentById")]
public async Task<ActionResult<Student>> GetStudentById(
    int id,
    [FromServices] IAuthorizationService authorizationService)
{
    if (id < 1)
        return BadRequest("Invalid student id.");

    var student = StudentDataSimulation.StudentsList
        .FirstOrDefault(s => s.Id == id);

    if (student == null)
        return NotFound("Student not found.");

    var authResult = await authorizationService.AuthorizeAsync(
        User,
        id,
        "StudentOwnerOrAdmin");

    if (!authResult.Succeeded)
        return Forbid(); // 403

    return Ok(student);
}
```

📌 **<mark><u>Why</u> no `[Authorize(Policy = "...")]` attribute</mark>?**

Because:

- Attributes <mark>**cannot pass resource data**</mark> (`id`)
- Ownership <mark>is **dynamic**</mark>
- Resource-based authorization <mark>requires **runtime evaluation**</mark>

> This is <mark>**<u>the correct and recommended</u>**</mark> approach.

---

## 11. 🔹 Why This Design Scales

With this structure:

- Security rules live <mark>in **one place**</mark>
- Controllers <mark>stay **clean**</mark>
- Ownership logic <mark>is **reusable**</mark>
- Rules <mark>are **easy to audit and change**</mark>

If tomorrow the rule becomes:

- Admin **OR** Manager
- Student **OR** Guardian
- Student **only during active enrollment**

> <mark>You change **one handler**</mark>, not 10 controllers.

---

## 12. 🔹 Why Not to Make Just a Helper Method?

A <mark>**<u>common question</u>**</mark> is:

> "Why not write a <mark>**centralized helper method**</mark> and call it from controllers?"

While helper methods may seem simpler, <mark>they create **<u>serious long-term</u> problems**</mark>.

### Helper methods:

- ❌ Are <mark>**invisible**</mark> to the authorization system
- ❌ Cannot be <mark>**audited** or **composed**</mark>
- ❌ <mark>**Hide**</mark> security <mark><u>**intent**</u></mark>
- ❌ <mark>Scale **poorly**</mark> as rules evolve

### Policy-based authorization:

- ✅ Makes security <mark>intent **explicit**</mark>
- ✅ Centralizes rules in **one place**
- ✅ Allows <mark>**auditing** and **composition**</mark>
- ✅ Integrates with the <mark>framework's **security pipeline**</mark>

> Even if a policy is forgotten on an endpoint, <mark>the policy **<u>still exists</u>**</mark>, is auditable, and is structurally correct.

> With helper methods, there is <mark>no concept of "**missing protection**"</mark> — <mark>only **hidden mistakes**</mark>.

📌 **Policies don't prevent forgetting. <mark>They <u>prevent chaos</u></mark>.**

> Security <mark>is not about **writing code that works**</mark>.
> It's about writing <mark>systems that **<u>fail safely</u>** and **<u>predictably</u>**</mark>.
> 
> Policy-based authorization <mark>**is not about convenience**</mark>.
> It is about:
> 
> - **Structure**
> - **Visibility**
> - **Consistency**
> - **Professional-grade security design**

---

## 13. 🛠️ Final Summary of Interconnections

- 🔹 Inline checks work, but **don't scale**
- 🔹 Policies **centralize** security logic
- 🔹 Handlers **enforce** rules consistently
- 🔹 Ownership becomes **reusable**
- 🔹 This is **professional API authorization**

---

## 14. 🏁 Conclusion

You have now moved from:

> **Authorization inside endpoints**

to:

> **<mark>Authorization as a centralized system</mark>**

This is how real-world, long-lived APIs stay secure.

▶️ **Next Lesson:** 🧱 Applying Policies Across the Student API (Ownership + Admin Rules)

You will:

- Apply this policy consistently
- Test it in Swagger and the console app
- Complete your authorization layer correctly

---

## 📌 Summary

| Concept              | Role                                                   |
| -------------------- | ------------------------------------------------------ |
| **Requirement**      | Declares *what* rule exists (no logic)                 |
| **Handler**          | Declares *how* the rule is enforced (the logic)        |
| **Policy**           | A named combination of requirements                    |
| **`AuthorizeAsync`** | Triggers the policy at runtime with the resource       |
| **`[FromServices]`** | Injects `IAuthorizationService` into the action method |

| Question                               | Answer                                         |
| -------------------------------------- | ---------------------------------------------- |
| Why not `[Authorize(Policy = "...")]`? | Attributes cannot pass the resource `id`       |
| Why not a helper method?               | Invisible, unauditable, hides intent           |
| Where does the logic live?             | In the handler, in the `Authorization/` folder |
| Where is it registered?                | `Program.cs` (handler + policy)                |
| Where is it invoked?                   | `StudentsController` via `AuthorizeAsync`      |

**Core principle:** inline `if` checks work <mark>for **one endpoint**</mark>. Policies work <mark>for **<u>every endpoint</u>, forever**</mark>. A policy <mark>is not **a convenience**</mark> — it is the difference between *"security that happens to work"* and *"security that is <mark>**provably consistent <u>across the entire</u> API**</mark>."*

---


