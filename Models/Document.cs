namespace FarSis.Models
{
    public class Document
    {
        public string Id { get; set; } // Change this to string to match the type of ApplicationUser Id

        public DateTime Label { get; set; }
        public String? DocumentText { get; set; }
        public ApplicationUser? User { get; set; } = new();
        public Department Department { get; set; } = new();
        public string? DepartmentId { get; set; }

        public bool CheckedByEditor = false;
        public int EditorId { get; set; }

        // Constructor to initialize the Label with the current timestamp
        public Document()
        {
            Label = DateTime.Now; // Set the Label only once when the object is created
        }
    }
}