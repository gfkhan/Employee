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
        }
    }
}