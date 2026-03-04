// See https://aka.ms/new-console-template for more information
using Employee.Service.Models;

FullTimeEmployee employee = new FullTimeEmployee
{
    Id = 1,
    FirstName = "Alice",
    LastName = "Smith",
    HourlyRate = 50
};


DisplayEmployeeInfo(employee, CalculateSalary, CalculateBonus);


static void DisplayEmployeeInfo(Employee.Service.Models.Employee employee, 
    Func<decimal, decimal> calcSalary, 
    Func<decimal, decimal> calcBonus)
{
    decimal salary = calcSalary(employee.HourlyRate);
    Console.WriteLine($"ID: {employee.Id}");
    Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
    Console.WriteLine($"Hourly Rate: {employee.HourlyRate:C}");
    Console.WriteLine($"Salary: {salary:C}");
    Console.WriteLine($"Bonus: {calcBonus(salary):C}");
}
static decimal CalculateSalary(decimal rate)
{
   return rate * 40 * 52; // Assuming 40 hours per week and 4 weeks in a month
}

static decimal CalculateBonus(decimal salary)
{
    return salary * 0.1m; // Assuming 40 hours per week and 4 weeks in a month
}