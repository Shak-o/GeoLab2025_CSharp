using EmployeeWebApp.Middlewares;
using EmployeeWebApp.Models;
using EmployeeWebApp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IEmployeeStorage, EmployeeStorage>();

builder.Host.UseSerilog();

builder.Configuration.AddJsonFile("appsecrets.json");

var app = builder.Build();

app.UseMiddleware<BasicLogger>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Do not use in PRODUCTION
app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();