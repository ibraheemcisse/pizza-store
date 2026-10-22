# PizzaStore API — C# and .NET Learning Project

A learning project built with C# and ASP.NET Core Minimal APIs. The goal is to understand backend development, database integration, HTTP APIs, and the engineering practices needed to build and operate reliable services.

## Current Progress

- [x] Created an ASP.NET Core Minimal API project.
- [x] Configured the application to run locally.
- [x] Implemented pizza CRUD endpoints.
- [x] Added Swagger/OpenAPI support for exploring and testing the API.
- [x] Started learning database integration with Entity Framework Core.
- [x] Installed the required EF Core tooling and design package.
- [x] Successfully created the initial EF Core migration.
- [x] Troubleshot an application lifecycle issue caused by a missing `app.Run()`.
- [ ] Apply the database migration and verify database persistence.
- [ ] Verify CRUD operations against the database rather than relying on in-memory data.
- [ ] Add validation and improve error handling.
- [ ] Add automated tests.
- [ ] Continue learning ASP.NET Core controllers and production engineering concepts.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Application programming language |
| .NET 8 | Application runtime and framework |
| ASP.NET Core Minimal APIs | HTTP endpoint implementation |
| Entity Framework Core | Database access and object-relational mapping |
| SQLite | Planned local database |
| Swashbuckle.AspNetCore 6.5.0 | Swagger/OpenAPI documentation |
| EF Core migrations | Managing database schema changes |

## Running the Application

From the project directory:

```bash
dotnet restore
dotnet build
dotnet run
```

The application was previously running at:

`http://localhost:5195`

The port may differ depending on the current launch configuration.

## API Endpoints

The application exposes pizza CRUD endpoints.

| HTTP method | Endpoint | Purpose |
|---|---|---|
| GET | `/pizzas` | Retrieve pizzas |
| GET | `/pizzas/{id}` | Retrieve a pizza by ID |
| POST | `/pizzas` | Create a pizza |
| PUT | `/pizzas/{id}` | Update a pizza |
| DELETE | `/pizzas/{id}` | Delete a pizza |

The API initially returned three sample pizzas:

- Margherita
- Pepperoni
- Hawaiian

These were the initial sample records; database-backed persistence still needs verification.

## Swagger / OpenAPI

Swagger provides an interface for inspecting and testing the available HTTP endpoints.

Previously available at:

`http://localhost:5195/swagger`

Use Swagger to send requests, inspect responses, and verify endpoint behavior while developing.

## Entity Framework Core

The project has progressed from a basic Minimal API toward database-backed application development.

The initial migration was successfully created using:

```bash
dotnet ef migrations add InitialCreate
```

The next database setup step is:

```bash
dotnet ef database update
```

After applying the migration, verify that the SQLite database exists, the schema is correct, and records persist after restarting the application.

## Troubleshooting Notes

### Application exits immediately

During development, the application exited without displaying the usual server-startup message.

**Cause:** `app.Run()` had been accidentally removed.

**Fix:** Restore the application entry point:

```csharp
app.Run();
```

For a typical ASP.NET Core Minimal API, this starts the application host and keeps the server running to handle requests.

A successful build does not guarantee that an application will remain running or behave correctly at runtime.

## Learning Objectives

This project is intended to develop practical understanding of:

1. C# syntax, types, methods, and application structure.
2. HTTP methods, routes, status codes, and request/response handling.
3. ASP.NET Core Minimal APIs and dependency injection.
4. EF Core, database contexts, migrations, and persistence.
5. Debugging and diagnosing application failures.
6. Error handling, validation, and automated testing.
7. Configuration, logging, and service reliability.
8. Deployment, CI/CD, containers, and Kubernetes as later stages.

## Next Steps

1. Apply and verify the SQLite migration.
2. Confirm that all CRUD operations work with the database.
3. Understand the EF Core code and the purpose of each important line.
4. Add basic validation and appropriate HTTP error responses.
5. Write tests for the API's behavior.
6. Continue the course with ASP.NET Core controllers.
7. Gradually evolve the project toward a Production Service Reliability Lab.

## Project Philosophy

**Understand the code before adding complexity.**

Each stage should produce working software, a clear explanation of how it works, and evidence that its behavior has been tested. Production-oriented features should be introduced incrementally rather than added without understanding their purpose.
