namespace Employee.Service.Models
{
    /// <summary>
    /// Service-layer DTO for an employee address.
    /// </summary>
    public class AddressDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int AddressTypeId { get; set; }
        public string? AddressTypeName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }
}