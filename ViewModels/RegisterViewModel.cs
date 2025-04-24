using System.ComponentModel.DataAnnotations;
namespace FarSis.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public int DepartmentId { get; set; } // Id olarak alacağız
    }
}