namespace FarSis.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }         
        public List<Document>? Documents { get; set; }

    }
}
