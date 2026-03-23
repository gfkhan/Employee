using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Employee.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EmployeeDTO = Employee.Service.Models.Employee;

namespace Employee.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee is null) return NotFound();
            return Ok(employee);
        }
    }
}