// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Collections.Generic;
using Employee.Data;
using Employee.Data.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();

using var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;

try
{
    var db = provider.GetRequiredService<ApplicationDbContext>();

    // Apply pending migrations
    db.Database.Migrate();

    // Seed only if empty
    if (!db.Employees.Any())
    {
        var now = DateTime.UtcNow;
        var seedEmployees = new List<Employee.Data.Models.Employee>
        {
            new Manager
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice.smith@example.com",
                PhoneNumber = "555-0101",
                HireDate = now.AddYears(-3),
                JobTitle = "Engineering Manager",
                HourlyRate = 80m
            },
            new Supervisor
            {
                FirstName = "Tom",
                LastName = "Brown",
                Email = "tom.brown@example.com",
                PhoneNumber = "555-0102",
                HireDate = now.AddYears(-2),
                JobTitle = "QA Supervisor",
                HourlyRate = 48m
            },
            new FullTimeEmployee
            {
                FirstName = "Linda",
                LastName = "Garcia",
                Email = "linda.garcia@example.com",
                PhoneNumber = "555-0103",
                HireDate = now.AddYears(-1),
                JobTitle = "Developer",
                HourlyRate = 45m
            },
            new PartTimeEmployee
            {
                FirstName = "Ethan",
                LastName = "Wright",
                Email = "ethan.wright@example.com",
                PhoneNumber = "555-0104",
                HireDate = now.AddMonths(-6),
                JobTitle = "Support (PT)",
                HourlyRate = 25m
            },
            new ContractEmployee
            {
                FirstName = "Olivia",
                LastName = "Nguyen",
                Email = "olivia.nguyen@example.com",
                PhoneNumber = "555-0105",
                HireDate = now.AddMonths(-2),
                JobTitle = "Contractor",
                HourlyRate = 60m,
                StartDate = now.AddMonths(-2),
                EndDate = now.AddMonths(10)
            }
        };

        db.Employees.AddRange(seedEmployees);
        db.SaveChanges();

        Console.WriteLine($"Seeded {seedEmployees.Count} employee records.");
    }
    else
    {
        Console.WriteLine("Employees table already contains data; seeding skipped.");
    }
}
catch (Exception ex)
{
    // Print full exception so you can paste it here if there is still a problem
    Console.WriteLine("Error during migration/seeding:");
    Console.WriteLine(ex.ToString());
    throw;
}
