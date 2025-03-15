namespace FarSis.Models
{
    public class Department
    {
        public string Id { get; set; }

        // Ensure Name is required (if necessary) and prevent null issues
        public string Name { get; set; } = string.Empty;        
        public List<Document> Documents { get; set; } = new();
    }
}
