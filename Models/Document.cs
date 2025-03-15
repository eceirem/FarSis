namespace FarSis.Models
{
    public class Document
    {
        public int Id { get; set; }

        public DateTime Label = DateTime.Now;
        public String? DocumentText { get; set; }
        public User User { get; set; } = new();
        public Department Department { get; set; } = new();

        public Boolean CheckedByEditor = false;
        public int EditorId { get; set; }
    }
}
