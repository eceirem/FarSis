﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace FarSis.Models
{
    public class User : IdentityUser<int> // Change IdentityUser to IdentityUser<int> to match the Id type
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public String? Email { get; set; }
        [Required]
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public List<Document>? Documents { get; set; } 
    }
}

