namespace FarSis.Models
{
    public class Document
    {
        public string Id { get; set; } // Change this to string to match the type of ApplicationUser Id

        public String? Label = string.Empty; //header
        public String? DocumentText { get; set; }
        public ApplicationUser User { get; set; } = new(); 
        public string DepartmentId { get; set; }

        public bool CheckedByEditor = false;
        public int EditorId { get; set; }
    }
}
