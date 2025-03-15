using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace FarSis.Models
{
    public class User
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public String? Email { get; set; }
        [Required]
        public Department Department { get; set; } = new Department();
        public List<Document> Documents { get; set; } = new();
    }
}
