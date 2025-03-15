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
    }
}
