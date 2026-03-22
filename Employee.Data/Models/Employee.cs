using System;

namespace Employee.Data.Models
{
    // Data model mirrors Employee.Service.Models.Employee (service model)
    public class Employee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string? JobTitle { get; set; }
        public decimal HourlyRate { get; set; }
    }
}