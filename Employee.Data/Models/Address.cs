namespace Employee.Data.Models
{
    /// <summary>
    /// Represents a physical address associated with an employee.
    /// An employee can have multiple addresses (Residential, Business, etc.).
    /// </summary>
    public class Address : BaseEntity
    {
        public int EmployeeId { get; set; }

        public int AddressTypeId { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        // Navigation properties
        public Employee Employee { get; set; } = null!;
        public AddressType AddressType { get; set; } = null!;
    }
}