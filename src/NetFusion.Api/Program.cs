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
