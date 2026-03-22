using System.Collections.Generic;
using System.Threading.Tasks;
using Employee.Data;
using Employee.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// alias the model to avoid collision with the 'Employee' namespace
using EmployeeModel = Employee.Data.Models.Employee;

namespace Employee.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetAll()
        {
            var employees = await _db.Employees.AsNoTracking().ToListAsync();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeModel>> GetById(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee is null) return NotFound();
            return Ok(employee);
        }
    }
}