namespace FarSis.Models
{
    public class Department
    {
        public int Id { get; set; }

        // Ensure Name is required (if necessary) and prevent null issues
        public string Name { get; set; } = string.Empty;

        // Initialize lists to avoid null reference exceptions
        public List<User> Users { get; set; } = new();
        public List<Document> Documents { get; set; } = new();
    }
}
