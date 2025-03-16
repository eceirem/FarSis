using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FarSis.Models
{
    public class ApplicationUser : IdentityUser  // Inherits Id, Email, etc. from IdentityUser
    {
        public string DepartmentId { get; set; } = string.Empty; // Department ID for the user      

        // One-to-many relationship with documents
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}
