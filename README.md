# PizzaStore — C# and .NET Learning Lab

A hands-on learning project focused on C#, ASP.NET Core, backend development, and the engineering practices used to build, troubleshoot, and operate reliable services.

The project evolves incrementally, from basic HTTP APIs to database integration, automated testing, and production-oriented engineering concepts. The goal is to understand how the application works, not just make the code run.

## Project Structure

This repository contains two ASP.NET Core projects:

| Project | Purpose |
|---|---|
| `PizzaStore.Api` | Minimal API development, CRUD endpoints, and Entity Framework Core learning. |
| `PizzaStore.Controllers` | Controller-based API development, with a model, service layer, and in-memory pizza data. |

Keeping both implementations allows me to compare different approaches to building APIs in ASP.NET Core while preserving the work completed in each project.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Application programming language |
| .NET 8 | Runtime and application framework |
| ASP.NET Core Minimal APIs | Lightweight HTTP endpoint implementation |
| ASP.NET Core Controllers | Controller-based HTTP endpoint implementation |
| Entity Framework Core | Object-relational mapping and database access |
| SQLite | Planned persistent storage for the Minimal API project |
| Swashbuckle / Swagger | API documentation and interactive testing |
| Git and GitHub | Version control and project history |
| Postman | Manual HTTP request testing |

## Current Progress

### Completed or demonstrated

- Created and configured an ASP.NET Core Minimal API project.
- Implemented pizza CRUD endpoints in the Minimal API project.
- Added Swagger/OpenAPI support.
- Started integrating Entity Framework Core and created an initial migration.
- Investigated an application lifecycle issue caused by a missing `app.Run()`.
- Created a separate controller-based ASP.NET Core project.
- Added a `Pizza` model and an in-memory `PizzaService`.
- Implemented controller actions for retrieving, creating, updating, and deleting pizzas.
- Tested the controller API's GET endpoint in Postman and received `200 OK`.
- Tested pizza creation with POST in Postman and received `201 Created`.
- Troubleshot a `415 Unsupported Media Type` response by correcting the request's JSON content type.

### In progress

- Apply and verify the SQLite migration in `PizzaStore.Api`.
- Verify that database-backed CRUD operations work correctly.
- Test the controller API's PUT and DELETE endpoints.
- Improve validation and error handling.
- Add automated tests.

Progress reflects work completed so far; implementation and testing status may differ between the two projects.

## Running the Applications

Run commands from the directory of the project you want to start.

### Minimal API

```powershell
cd PizzaStore.Api
dotnet restore
dotnet build
dotnet run
```

The application previously ran at `http://localhost:5195`. The actual port may differ depending on the launch configuration.

If Swagger is enabled, open `/swagger` on the application's configured local address.

### Controller-based API

```powershell
cd PizzaStore.Controllers
dotnet restore
dotnet build
dotnet run
```

The controller API previously ran at `http://localhost:5108`. Check the startup output for the current address.

## API Endpoints

### Minimal API — `PizzaStore.Api`

The Minimal API exposes the following pizza routes:

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/pizzas` | Retrieve all pizzas |
| GET | `/pizzas/{id}` | Retrieve a pizza by ID |
| POST | `/pizzas` | Create a pizza |
| PUT | `/pizzas/{id}` | Update a pizza |
| DELETE | `/pizzas/{id}` | Delete a pizza |

The initial implementation used sample pizzas named Margherita, Pepperoni, and Hawaiian. Database-backed persistence must be verified separately.

### Controller API — `PizzaStore.Controllers`

The controller-based API uses the following routes:

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/Pizza` | Retrieve all pizzas |
| GET | `/Pizza/{id}` | Retrieve a pizza by ID |
| POST | `/Pizza` | Create a pizza |
| PUT | `/Pizza/{id}` | Update a pizza |
| DELETE | `/Pizza/{id}` | Delete a pizza |

The controller project currently uses an in-memory collection initialized with Classic Italian and Veggie. A successful POST during testing created a pizza named Hawaii with ID `3`.

The endpoints are implemented, but the PUT and DELETE operations still need dedicated testing. In-memory data is not persistent and resets when the application restarts.

## Entity Framework Core

The Minimal API project is being developed toward database-backed storage using Entity Framework Core and SQLite.

An initial migration was created with:

```powershell
dotnet ef migrations add InitialCreate
```

The next step is to apply the migration:

```powershell
dotnet ef database update
```

After applying it, verify that the database file and schema are correct, that CRUD operations use the database, and that records persist after restarting the application.

Creating a migration does not, by itself, confirm that the database has been updated or that the application is using persistent storage correctly.

## Troubleshooting Notes

### Application exits immediately

During development, the application built successfully but exited without displaying the usual server-startup message.

**Cause:** `app.Run()` had been accidentally removed.

**Fix:** Restore the application entry point:

```csharp
app.Run();
```

For a typical ASP.NET Core Minimal API, this runs the application host so the server can handle incoming requests.

**Lesson:** A successful build does not guarantee that an application will start and remain available at runtime.

### Postman returns `415 Unsupported Media Type`

The initial POST request to the controller API failed because the request was not configured with the correct content type for the JSON body.

**Fix:** Send a raw JSON body and use:

```http
Content-Type: application/json
```

The request subsequently succeeded with `201 Created`.

**Lesson:** Before changing server-side code, inspect the HTTP method, URL, headers, and request body.

### HTTPS redirection warning

During local development, ASP.NET Core logged:

```text
Failed to determine the https port for redirect.
```

The application was listening on HTTP, and the tested GET and POST requests still succeeded.

This warning indicates that HTTPS redirection could not determine which HTTPS port to use. The configured launch profile and HTTPS endpoint should be checked when investigating the warning.

References:

- [Microsoft Q&A — HTTPS redirection warning](https://learn.microsoft.com/en-us/answers/questions/1297509/how-to-fix-this-error-warn-microsoft-aspnetcore-ht)
- [Microsoft Learn — Kestrel web server](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-10.0)

## Learning Objectives

The project is intended to build practical understanding of:

1. C# syntax, types, methods, and application structure.
2. HTTP methods, routes, status codes, and request/response handling.
3. ASP.NET Core Minimal APIs and controllers.
4. Dependency injection and separation of responsibilities.
5. Entity Framework Core, migrations, and database persistence.
6. Debugging application failures and configuration issues.
7. Validation, error handling, and automated testing.
8. Logging, deployment, CI/CD, containers, and Kubernetes as later stages.

## Next Steps

1. Apply and verify the SQLite migration in `PizzaStore.Api`.
2. Confirm that database-backed CRUD operations work as expected.
3. Test PUT and DELETE in `PizzaStore.Controllers`, including unsuccessful requests.
4. Add validation and appropriate error responses.
5. Introduce automated tests.
6. Continue developing a deeper understanding of the code and framework behavior.
7. Gradually evolve the repository into a **Production Service Reliability Lab**.

## Project Philosophy

**Understand the code before adding complexity.**

Each stage should produce working software, a clear explanation of how it works, and evidence that its behavior has been tested.

Production-oriented features should be introduced incrementally, with a focus on understanding their purpose, limitations, and operational impact.
