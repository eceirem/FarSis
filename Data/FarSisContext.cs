using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FarSis.Models;

namespace FarSis.Data
{
    public class FarSisContext : IdentityDbContext<ApplicationUser>  // IdentityDbContext handles user management
    {
        public FarSisContext(DbContextOptions<FarSisContext> options)
            : base(options)
        {
        }

        // Define DbSets for your other models (Department and Document)
        //public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Department> Departments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Id property to be database-generated
            modelBuilder.Entity<Document>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd(); // Let the database generate the Id PROBLEM BURADA ECE

            // Configure the relationship between Document and Department
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Department) // Document has one Department
                .WithMany(d => d.Documents) // Department has many Documents
                .HasForeignKey(d => d.DepartmentId); // Foreign key is DepartmentId

            base.OnModelCreating(modelBuilder);
        }
    }
}
