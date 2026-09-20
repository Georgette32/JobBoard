# JobBoard API

A backend Web API for managing jobs, applications, and recruiter authentication.

## Features

* Recruiter registration and login
* JWT Authentication and Authorization
* Create and manage job data
* Close an active job
* Only the job owner can close it
* Prevent applications to closed jobs
* Preserve existing applications after closing a job
* Password hashing
* SQL Server database using Entity Framework Core

## Project Structure

```text
JobBoard
├── JobBoard.Domain
├── JobBoard.Application
├── JobBoard.Infrastructure
└── JobbBoard.API
```

### Technologies

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger

## Main Endpoint

### Close Job

```http
POST /api/jobs/{jobId}/close
```

The authenticated recruiter must be the owner of the job.

### Authentication

Register:

```http
POST /api/auth/register
```

Login:

```http
POST /api/auth/login
```

The login endpoint returns a JWT token that is used to access protected endpoints.

## Database

The application uses SQL Server with Entity Framework Core migrations.

Run the database migrations before starting the API.

## Running the Project

1. Set `JobbBoard.API` as the startup project.
2. Configure the SQL Server connection string in `appsettings.json`.
3. Run the project.
4. Open Swagger to test the API.

## Architecture

The project follows a layered architecture:

* **Domain** — Entities and business rules.
* **Application** — Services, interfaces, DTOs, and application logic.
* **Infrastructure** — EF Core, database context, repositories, and persistence.
* **API** — Controllers, authentication, authorization, and HTTP endpoints.
