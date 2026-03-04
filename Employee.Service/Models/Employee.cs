using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Service.Models
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string JobTitle { get; set; }
        public decimal HourlyRate { get; set; }
        // Additional properties can be added as needed

        public abstract decimal CalculateSalary();
    }
}
