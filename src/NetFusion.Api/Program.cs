using MediatR;
using NetFusion.Application;
using NetFusion.Infrastructure;
using NetFusion.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application, Infrastructure, CrossCutting DI
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCrossCutting();

var app = builder.Build();
// Apply EF Core migrations at startup (Docker-friendly)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NetFusion.Infrastructure.Persistence.AppDbContext>();
    db.Database.Migrate();
}

// Middlewares
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Swagger in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
