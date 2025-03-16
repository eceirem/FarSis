using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FarSis.Models;

namespace FarSis.Data
{
    public class FarSisContext : DbContext
    {
        public FarSisContext (DbContextOptions<FarSisContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Document and Department
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Department) // Document has one Department
                .WithMany(d => d.Documents) // Department has many Documents
                .HasForeignKey(d => d.DepartmentId); // Foreign key is DepartmentId

            base.OnModelCreating(modelBuilder);
        }
    }
}
