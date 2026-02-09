using EmployeeWebApp.Infrastructure;
using EmployeeWebApp.MiddleWares;
using EmployeeWebApp.Models;
using EmployeeWebApp.Options;
using EmployeeWebApp.Persistance;
using EmployeeWebApp.Services;
using Microsoft.EntityFrameworkCore;
using RestEase.HttpClientFactory;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "Logs/logs-.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRestEaseClient<IOpenWeatherMapApi>("http://api.openweathermap.org");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddTransient<IEmployeeStorage, EmployeeDbStorage>();
builder.Services.AddDbContext<EmployeeDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDb")));
builder.Services.AddSingleton<EmployeeCacheService>();

// Scoped - ერთხელ იქმნება რექვესთის scope ის განმავლობაში
// Transient - ყოველ ჯერზე როცა კონსტრუქტორში მოვთხოვთ ახალი სერვისის ინსტანსი იქმნება
// Singleton - მთელი აპლიკაციის სიცოცხლის განმავლობაში იქმნება 1 ინსტანსი
builder.Services.AddScoped<TestScopedService>();
builder.Services.AddTransient<TestTransientService>();
builder.Services.AddSingleton<TestSingletonService>();

builder.Configuration.AddJsonFile("appsecrets.json");

builder.Services.Configure<EmployeeOptions>(builder.Configuration.GetSection("EmployeeOption"));

builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
//app.UseMiddleware<SimpleAuthMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// Do not use in PRODUCTION
app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();