using System;

namespace Employee.Data.Models
{
    // Mirrors Employee.Service.Models.ContractEmployee
    public class ContractEmployee : Employee
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}