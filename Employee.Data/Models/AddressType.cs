namespace Employee.Data.Models
{
    /// <summary>
    /// Lookup entity representing the type of address an employee can have.
    /// </summary>
    public class AddressType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}