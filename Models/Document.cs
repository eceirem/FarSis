namespace FarSis.Models
{
    public class Document
    {
        public int Id { get; set; }

        // Use DateTime for Label
        public DateTime Label { get; private set; }

        public string? DocumentText { get; set; }
        public User? User { get; set; }

        // DepartmentId to store the selected department's ID
        public int? DepartmentId { get; set; }

        // Navigation property for Department
        public Department? Department { get; set; }

        public bool CheckedByEditor { get; set; } = false;
        public int EditorId { get; set; }

        // Constructor to initialize the Label with the current timestamp
        public Document()
        {
            Label = DateTime.Now; // Set the Label only once when the object is created
        }
    }
}