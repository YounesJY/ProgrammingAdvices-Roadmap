# Running & Verifying the Existing Student API

---

## 1. Introduction

Before adding any security, we must ensure one thing:

✅ The existing Student API project works perfectly. You can download it from the attached files below.

Security must never hide bugs or fix broken logic.

<mark><u>**We secure only working systems.**</u></mark>

<mark>**This lesson ensures your project is <u>clean, running, and verified</u>.**</mark>

---

## 2. Step 1 — Run the Existing Project

📋 **Instructions**

- 🔹 Download the project from the attached files below
- 🔹 Open the Student API solution
- 🔹 Build the project
- 🔹 Run the API

✅ **Valid:**

- No compilation errors
- Application starts successfully

❌ **Invalid:**

- Build errors
- Runtime exceptions

---

## 3. Step 2 — Open Swagger

📋 **Instructions**

- 🔹 Open the browser
- 🔹 Navigate to `/swagger`

You should see all Student endpoints listed.

---

## 4. Step 3 — Verify All Endpoints Exist

Confirm the presence of:

| Method | Endpoint                     |
| ------ | ---------------------------- |
| GET    | `/api/Students/All`          |
| GET    | `/api/Students/Passed`       |
| GET    | `/api/Students/AverageGrade` |
| GET    | `/api/Students/{id}`         |
| POST   | `/api/Students`              |
| PUT    | `/api/Students/{id}`         |
| DELETE | `/api/Students/{id}`         |

---

## 5. Step 4 — Test Read Endpoints

📋 **Instructions**

- 🔹 Call `GET /api/Students/All`
- 🔹 Call `GET /api/Students/Passed`

**Observe:**

- 🔹 Status code `200 OK`
- 🔹 Correct JSON response

---

## 6. Step 5 — Test Write Endpoints (Carefully)

📋 **Instructions**

- 🔹 Call `POST /api/Students`
- 🔹 Call `PUT /api/Students/{id}`
- 🔹 Call `DELETE /api/Students/{id}`

⚠️ **Warning:** These calls modify real data.

---

## 7. Characteristics

- 🧬 API is fully open
- 🧬 No identity checks
- 🧬 No access restrictions
- 🧬 All endpoints trust the caller

---

## 8. Interconnection

- 🔹 Verified endpoints → safe starting point
- 🔹 Open access → security necessity
- 🔹 Clean build → reliable security work

---

## 9. Conclusion

You now have:

- ✅ <mark>A running API</mark>
- ✅ <mark>**Verified endpoints**</mark>
- ✅ <mark>**A clear security problem**</mark>

In the next lesson, <mark>**we will prove the danger by performing a security audit on your own API**</mark>. We will <mark>attack the API on purpose and officially **<u>start the security audit phase</u>** 🔐</mark>.

---

## 📌 Summary

| Step | Action                   | Expected Result                  |
| ---- | ------------------------ | -------------------------------- |
| 1    | Build & run the solution | No errors, app starts            |
| 2    | Open `/swagger`          | All Student endpoints listed     |
| 3    | Verify endpoint list     | 7 endpoints present              |
| 4    | Test read endpoints      | `200 OK` + correct JSON          |
| 5    | Test write endpoints     | Data modified — no auth required |
| —    | Baseline state           | Fully open, zero access control  |
