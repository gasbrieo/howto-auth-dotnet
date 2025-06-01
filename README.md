# HowTo.Auth.DotNet

![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gasbrieo_howto-auth-dotnet?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gasbrieo_howto-auth-dotnet?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge)
![GitHub last commit](https://img.shields.io/github/last-commit/gasbrieo/howto-auth-dotnet?style=for-the-badge)

## Overview

Basic example of authentication and authorization using ASP.NET Core Identity and JWT in .NET 9. Clean structure and tests included.

---

## Highlights

- ASP.NET Core 9 + Identity + JWT
- Role-based auth
- Clean Architecture (Core / UseCases / Infra / Presentation)
- Error responses with `ProblemDetails`
- Functional, integration and unit tests
- Swagger with token support
- Health checks endpoints (liveness/readiness) for monitoring
- CI workflow with SonarCloud

---

## Endpoints

| Method | Route               | Auth | Description                                                |
| ------ | ------------------- | ---- | ---------------------------------------------------------- |
| POST   | `/auth/register`    | ❌   | Register new user                                          |
| POST   | `/auth/login`       | ❌   | Login and get token                                        |
| GET    | `/users/me`         | ✅   | Get current user                                           |
| GET    | `/health/liveness`  | ❌   | Checks if the application is running                       |
| GET    | `/health/readiness` | ❌   | Checks if the application and its dependencies are healthy |

---

## Template Support

This project can be used as a custom template with the `.NET CLI`.

### 🛠️ Install template locally

```bash
dotnet new install .
```

### 📦 Generate a new project

```bash
dotnet new howto-auth -n HowTo.Useradmin
```

### 🧩 Available parameters

| Parameter  | Default             | Description                                                         |
| ---------- | ------------------- | ------------------------------------------------------------------- |
| `--CiName` | `howto-auth-dotnet` | Used to replace the project name inside CI files (e.g., SonarCloud) |

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
