# NetFusion.CleanArch

## Project Overview
NetFusion.CleanArch is a modular .NET solution implementing Clean Architecture principles. It separates concerns into distinct layers for maintainability, scalability, and testability. The solution includes API, Application, Domain, Infrastructure, and Cross-Cutting modules, with comprehensive unit, integration, and acceptance tests.

### Solution Structure
- **NetFusion.Api**: ASP.NET Core Web API controllers.
- **NetFusion.Application**: Application logic, commands, queries, handlers, and validators.
- **NetFusion.Domain**: Domain models, aggregates, entities, value objects, events, interfaces, and services.
- **NetFusion.Infrastructure**: Persistence, migrations, and infrastructure concerns.
- **NetFusion.CrossCutting**: Logging, exceptions, and shared utilities.
- **tests**: Contains unit, integration, and acceptance tests.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or update connection string for your DB)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Getting Started

### 1. Clone the Repository
```pwsh
git clone https://github.com/mahmoudAli-lab/NetFusion.CleanArch.git
cd NetFusion.CleanArch
```

### 2. Restore Dependencies
```pwsh
dotnet restore
```

### 3. Update Database Connection
Edit the connection string in `appsettings.json` under `NetFusion.Api` or `NetFusion.Infrastructure` as needed.

### 4. Apply Migrations (if using EF Core)
```pwsh
dotnet ef database update --project src/NetFusion.Infrastructure --startup-project src/NetFusion.Api
```

### 5. Run the API
```pwsh
dotnet run --project src/NetFusion.Api
```
The API will be available at `https://localhost:5001` or `http://localhost:5000`.

## Testing

### Run All Tests
```pwsh
dotnet test
```

### Run Specific Test Project
```pwsh
dotnet test tests/NetFusion.UnitTests
```

## Project Highlights
- **Clean Architecture**: Strict separation of concerns.
- **Domain-Driven Design**: Aggregates, entities, value objects, domain events.
- **CQRS**: Commands, queries, and handlers.
- **Validation**: FluentValidation for input validation.
- **Logging & Exception Handling**: Centralized cross-cutting concerns.
- **Extensive Testing**: Unit, integration, and acceptance tests.

## Contributing
1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Open a pull request.

## License
This project is licensed under the MIT License.

## Contact
For questions or support, open an issue on GitHub or contact the owner at [GitHub Profile](https://github.com/mahmoudAli-lab).
