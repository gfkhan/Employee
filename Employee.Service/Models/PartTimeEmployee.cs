using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Service.Models
{
    public class PartTimeEmployee : Employee
    {
        private const int PartTimeWeeklyHours = 20;
        public override decimal CalculateSalary()
        {
            return HourlyRate * PartTimeWeeklyHours * 52; // Assuming 52 weeks in a year
        }
    }
}
