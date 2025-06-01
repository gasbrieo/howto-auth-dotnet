# HowTo.Auth.DotNet

![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/gasbrieo_howto-auth-dotnet?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge)
![Sonar Coverage](https://img.shields.io/sonar/coverage/gasbrieo_howto-auth-dotnet?server=https%3A%2F%2Fsonarcloud.io&style=for-the-badge)
![GitHub last commit](https://img.shields.io/github/last-commit/gasbrieo/howto-auth-dotnet?style=for-the-badge)
![GitHub Release](https://img.shields.io/github/v/release/gasbrieo/howto-auth-dotnet?style=for-the-badge)

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
- CI workflow with SonarCloud

---

## Endpoints

| Method | Route            | Auth | Description          |
|--------|------------------|------|----------------------|
| POST   | `/auth/register` | ❌   | Register new user    |
| POST   | `/auth/login`    | ❌   | Login and get token  |
| GET    | `/me`            | ✅   | Get current user     |

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

