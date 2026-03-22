using ServiceModels = Employee.Service.Models;

namespace Employee.Service.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Returns all employees mapped to service models.
        /// </summary>
        Task<IEnumerable<ServiceModels.Employee>> GetAllAsync();

        /// <summary>
        /// Returns a single employee by id mapped to the service model, or null if not found.
        /// </summary>
        Task<ServiceModels.Employee?> GetByIdAsync(int id);
    }
}