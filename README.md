# HowTo Auth (.NET)

This project demonstrates how to implement modern authentication in ASP.NET Core using **Microsoft Identity** with **JWT Bearer tokens**, following Clean Architecture principles.

It is part of a series of practical authentication implementations across multiple stacks.

---

## 🔐 Features

- ASP.NET Core 8 with Identity
- JWT token generation and validation
- Role-based authorization
- Clean Architecture (Core, UseCases, Infrastructure, Presentation)
- FluentValidation for request validation
- Swagger (OpenAPI) with JWT support
- xUnit integration and unit testing
- SQLite (in-memory or file-based) support

---

## 📦 Project Structure

```
src/
├── Core            # Domain entities and interfaces
├── UseCases        # Business logic: Register, Login, etc.
├── Infrastructure  # Identity + EF Core + repositories
└── Presentation    # Controllers, DTOs, Middleware, Program.cs
tests/
├── Unit            # Use case testing
└── Integration     # Full integration tests with WebApplicationFactory
```

---

## 📌 Endpoints

| Method | Route           | Access       | Description                         |
|--------|------------------|--------------|-------------------------------------|
| POST   | `/auth/register` | Public       | Register new user                   |
| POST   | `/auth/login`    | Public       | Login and receive JWT token         |
| GET    | `/me`            | Authenticated| Get current user profile            |
| GET    | `/admin/users`   | Admin only   | Protected route with role-based auth|

---

## 📎 JWT Token Payload

```json
{
  "sub": "userId",
  "email": "user@example.com",
  "role": "Admin",
  "iat": 1717171717,
  "exp": 1717175317
}
```

---

## 🧪 Running Tests

```bash
dotnet test
```

Includes:
- ✅ Unit tests for each use case
- ✅ Integration tests with authorization scenarios

---

## 🚀 How to Run

```bash
dotnet run --project src/Presentation
```

Swagger will be available at: `https://localhost:{port}/swagger`

---

## 📚 Why this matters

Understanding authentication is critical for modern applications. This project demonstrates:

- How to properly use ASP.NET Identity without scaffolding
- How to structure code for testability and maintainability
- How to protect APIs using JWT and role-based access control

---
