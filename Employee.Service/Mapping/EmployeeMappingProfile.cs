using AutoMapper;

// Aliases to disambiguate Data vs Service namespaces
using DataModels = Employee.Data.Models;
using ServiceModels = Employee.Service.Models;

namespace Employee.Service.Mapping
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            // Base mapping with polymorphic includes
            CreateMap<DataModels.Employee, ServiceModels.FullTimeEmployee>();

            CreateMap<DataModels.FullTimeEmployee, ServiceModels.FullTimeEmployee>();

            CreateMap<DataModels.PartTimeEmployee, ServiceModels.PartTimeEmployee>();

            CreateMap<DataModels.ContractEmployee, ServiceModels.ContractEmployee>();

            CreateMap<DataModels.Manager, ServiceModels.Manager>();

            CreateMap<DataModels.Supervisor, ServiceModels.Supervisor>();

            // Root map: Data.Employee → closest concrete Service type
            // Uses Include so AutoMapper resolves the correct derived map at runtime
            CreateMap<DataModels.Employee, ServiceModels.Employee>()
                .Include<DataModels.FullTimeEmployee, ServiceModels.FullTimeEmployee>()
                .Include<DataModels.PartTimeEmployee, ServiceModels.PartTimeEmployee>()
                .Include<DataModels.ContractEmployee, ServiceModels.ContractEmployee>()
                .Include<DataModels.Manager, ServiceModels.Manager>()
                .Include<DataModels.Supervisor, ServiceModels.Supervisor>();
        }
    }
}