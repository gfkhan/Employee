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
    CreateSampleEmployeesIfNoneExist(db);
    CreateSampleAddressesIfNoneExist(db);
}
catch (Exception ex)
{
    // Print full exception so you can paste it here if there is still a problem
    Console.WriteLine("Error during migration/seeding:");
    Console.WriteLine(ex.ToString());
    throw;
}

static void CreateSampleEmployeesIfNoneExist(ApplicationDbContext db)
{
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
}

static void CreateSampleAddressesIfNoneExist(ApplicationDbContext db)
{
    if (db.Addresses.Any())
    {
        Console.WriteLine("Addresses already exist — skipping seed.");
        return;
    }

    var now = DateTime.UtcNow;
    var user = Environment.UserName;

    // AddressTypeId: 1 = Residential, 2 = Business (seeded via HasData in DbContext)
    const int residential = 1;
    const int business = 2;

    // Look up existing employees by email to get their Ids
    var alice = db.Employees.FirstOrDefault(e => e.Email == "alice.smith@example.com");
    var tom = db.Employees.FirstOrDefault(e => e.Email == "tom.brown@example.com");
    var linda = db.Employees.FirstOrDefault(e => e.Email == "linda.garcia@example.com");
    var ethan = db.Employees.FirstOrDefault(e => e.Email == "ethan.wright@example.com");
    var olivia = db.Employees.FirstOrDefault(e => e.Email == "olivia.nguyen@example.com");

    var seedAddresses = new List<Address>();

    // Alice Smith — Residential + Business
    if (alice is not null)
    {
        seedAddresses.Add(new Address
        {
            EmployeeId = alice.Id, AddressTypeId = residential,
            Street = "123 Maple Drive", City = "Seattle", State = "WA",
            ZipCode = "98101", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
        seedAddresses.Add(new Address
        {
            EmployeeId = alice.Id, AddressTypeId = business,
            Street = "500 Corporate Blvd, Suite 200", City = "Seattle", State = "WA",
            ZipCode = "98104", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
    }

    // Tom Brown — Residential + Business
    if (tom is not null)
    {
        seedAddresses.Add(new Address
        {
            EmployeeId = tom.Id, AddressTypeId = residential,
            Street = "456 Oak Avenue", City = "Bellevue", State = "WA",
            ZipCode = "98004", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
        seedAddresses.Add(new Address
        {
            EmployeeId = tom.Id, AddressTypeId = business,
            Street = "500 Corporate Blvd, Suite 300", City = "Seattle", State = "WA",
            ZipCode = "98104", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
    }

    // Linda Garcia — Residential only
    if (linda is not null)
    {
        seedAddresses.Add(new Address
        {
            EmployeeId = linda.Id, AddressTypeId = residential,
            Street = "789 Pine Street, Apt 4B", City = "Redmond", State = "WA",
            ZipCode = "98052", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
    }

    // Ethan Wright — Residential only
    if (ethan is not null)
    {
        seedAddresses.Add(new Address
        {
            EmployeeId = ethan.Id, AddressTypeId = residential,
            Street = "321 Birch Lane", City = "Kirkland", State = "WA",
            ZipCode = "98033", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
    }

    // Olivia Nguyen — Residential + Business
    if (olivia is not null)
    {
        seedAddresses.Add(new Address
        {
            EmployeeId = olivia.Id, AddressTypeId = residential,
            Street = "654 Cedar Court", City = "Tacoma", State = "WA",
            ZipCode = "98402", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
        seedAddresses.Add(new Address
        {
            EmployeeId = olivia.Id, AddressTypeId = business,
            Street = "900 Innovation Way", City = "Tacoma", State = "WA",
            ZipCode = "98405", Country = "USA",
            CreatedDate = now, CreatedBy = user
        });
    }

    if (seedAddresses.Count > 0)
    {
        db.Addresses.AddRange(seedAddresses);
        db.SaveChanges();
        Console.WriteLine($"Seeded {seedAddresses.Count} address records.");
    }
    else
    {
        Console.WriteLine("No employees found to seed addresses for.");
    }
}