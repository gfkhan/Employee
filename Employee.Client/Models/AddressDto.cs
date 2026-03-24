using System.Xml.Serialization;

namespace Employee.Client.Models;

/// <summary>
/// Client-side DTO for an employee address.
/// </summary>
public class AddressDto
{
    [XmlIgnore]
    public int Id { get; set; }

    [XmlIgnore]
    public int EmployeeId { get; set; }

    [XmlIgnore]
    public int AddressTypeId { get; set; }

    [XmlElement("AddressType")]
    public string? AddressTypeName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
}