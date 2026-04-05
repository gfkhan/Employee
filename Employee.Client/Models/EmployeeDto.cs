using System.Xml.Serialization;

namespace Employee.Client.Models;

public class EmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public string? JobTitle { get; set; }
    public decimal HourlyRate { get; set; }

    /// <summary>
    /// Addresses associated with this employee.
    /// </summary>
    public List<AddressDto> Addresses { get; set; } = new();
}