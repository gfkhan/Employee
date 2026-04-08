using Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Employee> Employees { get; set; } = null!;
        public DbSet<Models.Address> Addresses { get; set; } = null!;
        public DbSet<Models.AddressType> AddressTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TPH inheritance with a discriminator column
            modelBuilder.Entity<Models.Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
                entity.Property(e => e.JobTitle).HasMaxLength(150);
                entity.Property(e => e.HourlyRate).HasPrecision(18, 2);

                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.HourlyRate).HasPrecision(18, 2);

            });

            // Discriminator / TPH mapping for derived types
            modelBuilder.Entity<Models.Employee>()
                .HasDiscriminator<string>("EmployeeType")
                .HasValue<Models.Employee>("Employee")
                .HasValue<Models.FullTimeEmployee>("FullTime")
                .HasValue<Models.PartTimeEmployee>("PartTime")
                .HasValue<Models.Manager>("Manager")
                .HasValue<Models.Supervisor>("Supervisor")
                .HasValue<Models.ContractEmployee>("Contract");

            // Configure properties specific to ContractEmployee
            modelBuilder.Entity<Models.ContractEmployee>(entity =>
            {
                entity.Property(c => c.StartDate).IsRequired();
                entity.Property(c => c.EndDate).IsRequired();
            });

            // Configure AddressType lookup table
            modelBuilder.Entity<Models.AddressType>(entity =>
            {
                entity.ToTable("AddressTypes");
                entity.HasKey(at => at.Id);
                entity.Property(at => at.Name).HasMaxLength(50).IsRequired();

                // Seed the two known types
                entity.HasData(
                    new Models.AddressType { Id = 1, Name = "Residential" },
                    new Models.AddressType { Id = 2, Name = "Business" }
                );
            });

            // Configure Address entity
            modelBuilder.Entity<Models.Address>(entity =>
            {
                entity.ToTable("Addresses");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Street).HasMaxLength(250);
                entity.Property(a => a.City).HasMaxLength(100);
                entity.Property(a => a.State).HasMaxLength(100);
                entity.Property(a => a.ZipCode).HasMaxLength(20);
                entity.Property(a => a.Country).HasMaxLength(100);

                entity.Property(a => a.CreatedDate).IsRequired();
                entity.Property(a => a.CreatedBy).HasMaxLength(100);
                entity.Property(a => a.UpdatedBy).HasMaxLength(100);

                // Relationship: Employee (1) → Addresses (many)
                entity.HasOne(a => a.Employee)
                      .WithMany(e => e.Addresses)
                      .HasForeignKey(a => a.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: AddressType (1) → Addresses (many)
                entity.HasOne(a => a.AddressType)
                      .WithMany(at => at.Addresses)
                      .HasForeignKey(a => a.AddressTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}