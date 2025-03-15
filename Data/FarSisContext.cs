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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
