using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Service.Models
{
    public class ContractEmployee : Employee
    {
        private const int ContractWeeklyHours = 40;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override decimal CalculateSalary()
        {
            return HourlyRate * ContractWeeklyHours * (decimal)(EndDate - StartDate).TotalDays; // Assuming 52 weeks in a year
        }
    }
}
