using AutoMapper;
using Employee.Data;
using Microsoft.EntityFrameworkCore;

using ServiceModels = Employee.Service.Models;

namespace Employee.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ServiceModels.Employee>> GetAllAsync()
        {
            var dataEmployees = await _db.Employees
                .Include(e => e.Addresses)
                    .ThenInclude(a => a.AddressType)
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<IEnumerable<ServiceModels.Employee>>(dataEmployees);
        }

        /// <inheritdoc />
        public async Task<ServiceModels.Employee?> GetByIdAsync(int id)
        {
            var dataEmployee = await _db.Employees
                .Include(e => e.Addresses)
                    .ThenInclude(a => a.AddressType)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (dataEmployee is null) return null;
            return _mapper.Map<ServiceModels.Employee>(dataEmployee);
        }
    }
}