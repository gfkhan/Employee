using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Service.Models
{
    public class Manager : FullTimeEmployee
    {
        public override decimal CalculateBonus()
        {
            return base.CalculateBonus() + 5000; // Managers get an additional fixed bonus
        }
    }
}
