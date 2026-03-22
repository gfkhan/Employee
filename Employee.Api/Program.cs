using Employee.Data;
using Employee.Service.Mapping;
using Employee.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// load configuration from appsettings.json (already included by default)
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext from Employee.Data
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register AutoMapper with the profile from Employee.Service
builder.Services.AddAutoMapper(typeof(EmployeeMappingProfile));

// Register the employee service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();