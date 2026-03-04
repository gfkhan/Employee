using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Service.Models
{
    public class FullTimeEmployee : Employee
    {
        private const int FullTimeWeeklyHours = 40;
        public override decimal CalculateSalary()
        {
            return HourlyRate * FullTimeWeeklyHours * 52; // Assuming 52 weeks in a year
        }
        public virtual decimal CalculateBonus()
        {
            return CalculateSalary() * 0.1m; // Example bonus calculation (10% of salary)
        }
    }
}
