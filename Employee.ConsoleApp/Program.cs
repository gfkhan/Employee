// See https://aka.ms/new-console-template for more information
using Employee.Service.Models;

var employeeType = EmplyeeType.Contractor;

// Get the integer value of the FullTime enum member
Console.WriteLine((int)employeeType);

// Get the name of the FullTime enum member
Console.WriteLine(employeeType.ToString());

// Get the name of an enum member by its integer value
Console.WriteLine(Enum.GetName(typeof(EmplyeeType), 1));

// Cast an integer to the EmplyeeType enum
var employeeTypeFromInt = (EmplyeeType)2;
Console.WriteLine(employeeTypeFromInt.ToString());

// Cast a string to the EmplyeeType enum
var employeeTypeFromString = (EmplyeeType)Enum.Parse(typeof(EmplyeeType), "PartTime");
Console.WriteLine(employeeTypeFromString.ToString());

// Iterate over the members of the EmplyeeType enum
foreach (var type in Enum.GetValues(typeof(EmplyeeType)))
{
    Console.WriteLine($"Employee Type: {type}, Value: {(int)type}");
}

