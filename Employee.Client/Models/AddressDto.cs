using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Employee.Client.Models;

/// <summary>   
/// Client-side DTO for an employee address.
/// </summary>
public class AddressDto
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public int EmployeeId { get; set; }

    [JsonIgnore]
    public int AddressTypeId { get; set; }
    [JsonPropertyName("addressType")]
    public string? AddressTypeName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
}