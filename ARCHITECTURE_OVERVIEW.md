# NetFusion.CleanArch – Solution Overview

---

## 1️⃣ Solution & Project Structure

```
NetFusion.CleanArch.sln
src/
 ├── NetFusion.Api/                  # ASP.NET Core API (controllers, minimal APIs)
 │    ├── Controllers/
 │    │    └── WeatherController.cs
 │    ├── Program.cs
 │    └── appsettings.json
 │
 ├── NetFusion.Application/          # Application layer (CQRS, MediatR)
 │    ├── Commands/
 │    │    └── CreateOrderCommand.cs
 │    ├── Queries/
 │    │    └── GetOrdersQuery.cs
 │    ├── Handlers/
 │    │    ├── CreateOrderHandler.cs
 │    │    └── GetOrdersHandler.cs
 │    ├── Validators/
 │    │    └── CreateOrderValidator.cs
 │    └── DependencyInjection.cs
 │
 ├── NetFusion.Domain/               # Domain model (Entities, Aggregates, Events)
 │    ├── Entities/
 │    │    └── Order.cs
 │    ├── ValueObjects/
 │    │    └── OrderId.cs
 │    ├── Events/
 │    │    └── OrderCreatedEvent.cs
 │    └── Interfaces/
 │         └── IOrderRepository.cs
 │
 ├── NetFusion.Infrastructure/       # Data & external services
 │    ├── Persistence/
 │    │    ├── AppDbContext.cs
 │    │    └── EfOrderRepository.cs
 │    ├── Migrations/
 │    └── DependencyInjection.cs
 │
 └── NetFusion.CrossCutting/         # Shared utilities
      ├── Logging/
      │    └── SerilogExtensions.cs
      ├── Exceptions/
      │    └── ExceptionMiddleware.cs
      └── DependencyInjection.cs
 │
tests/
 ├── NetFusion.UnitTests/
 │    └── OrderTests.cs
 ├── NetFusion.IntegrationTests/
 │    └── OrderApiTests.cs
 └── NetFusion.AcceptanceTests/
      └── OrderAcceptance.feature
```

---

## 2️⃣ Boilerplate Code Snippets

### ✅ API Layer (`NetFusion.Api/Program.cs`)
```csharp
using NetFusion.Application;
using NetFusion.Infrastructure;
using NetFusion.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddApplication();       // Application DI
builder.Services.AddInfrastructure();    // EF, DB, etc.
builder.Services.AddCrossCutting();      // Logging, Middleware

var app = builder.Build();

// Middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
```

---

### ✅ Application Layer (CQRS Command Example)
```csharp
// Command
public record CreateOrderCommand(string CustomerName, decimal Amount) : IRequest<Guid>;

// Handler
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repo;

    public CreateOrderHandler(IOrderRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerName, request.Amount);
        await _repo.AddAsync(order);
        return order.Id.Value;
    }
}

// Validator
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}
```

---

### ✅ Domain Layer (Entity + Event)
```csharp
public class Order
{
    public OrderId Id { get; private set; } = new(Guid.NewGuid());
    public string CustomerName { get; private set; }
    public decimal Amount { get; private set; }

    private readonly List<object> _events = new();
    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public Order(string customerName, decimal amount)
    {
        CustomerName = customerName;
        Amount = amount;
        _events.Add(new OrderCreatedEvent(Id.Value, amount));
    }
}

public record OrderId(Guid Value);

public record OrderCreatedEvent(Guid OrderId, decimal Amount);
```

---

### ✅ Infrastructure Layer (EF Core Repo)
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

public class EfOrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public EfOrderRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();
    }
}
```

---

### ✅ CrossCutting Layer (Serilog Logging)
```csharp
public static class SerilogExtensions
{
    public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
    {
        builder.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });
        return builder;
    }
}
```

---

## 3️⃣ README Additions

### 📦 Solution Diagram

```
+-------------------+
|   NetFusion.Api   |
+-------------------+
         |
         v
+------------------------+
| NetFusion.Application  |
+------------------------+
         |
         v
+-------------------+
| NetFusion.Domain  |
+-------------------+
         |
         v
+------------------------+
| NetFusion.Infrastructure |
+------------------------+
         |
         v
+------------------------+
| NetFusion.CrossCutting  |
+------------------------+
```

### 🚀 Useful Commands

- Build solution: `dotnet build NetFusion.CleanArch.sln`
- Run API: `dotnet run --project src/NetFusion.Api/NetFusion.Api.csproj`
- Run tests: `dotnet test NetFusion.CleanArch.sln`
- Add migration: `dotnet ef migrations add InitialCreate --project src/NetFusion.Infrastructure/`

---

## 4️⃣ Optional Advanced Add-ons

- **Global Exception Middleware** for unified error handling.
- **Serilog logging** with file/console sinks.
- **FluentValidation** for request validation.
- **MediatR** for CQRS pattern.
- **Dockerfile** for API project.
- **GitHub Actions** for CI/CD.
- **Swagger/OpenAPI** for API documentation.
- **Feature folders** for scalable API structure.
- **Acceptance tests** using SpecFlow or Gherkin.

---

> This file provides a full overview, code snippets, and advanced ideas to help you build and extend your Clean Architecture solution with .NET.
