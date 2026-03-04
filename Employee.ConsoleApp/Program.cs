// See https://aka.ms/new-console-template for more information
using Employee.Service.Models;

Manager manager = new Manager
{
    Id = 1,
    FirstName = "Alice",
    LastName = "Smith",
    HourlyRate = 50
};

Supervisor supervisor = new Supervisor
{
    Id = 2,
    FirstName = "Tom",
    LastName = "Brown",
    HourlyRate = 50
};
Console.WriteLine("Manager Salary:"+ manager.CalculateSalary());
Console.WriteLine("Manager Bonus:" + manager.CalculateBonus());

Console.WriteLine("Supervisor Salary:" + supervisor.CalculateSalary());
Console.WriteLine("Supervisor Bonus:" + supervisor.CalculateBonus());
