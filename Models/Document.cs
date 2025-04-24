namespace FarSis.Models
{
    public class Document
    {
        public int Id { get; set; }
        public DateTime Label { get; set; }
        public string? DocumentText { get; set; }

        public int UserId { get; set; }  // belgeyi oluşturan user’ın ID’si
        public User? User { get; set; }  // Navigation property

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public bool CheckedByEditor = false;
        public int? EditorId { get; set; }

        public Document()
        {
            Label = DateTime.Now;
        }
    }
}
