namespace CRM_test.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public ProjectStatus Status { get; set; }

        public int? ClientId { get; set; }

        public Client? Client { get; set; }

        public DateTime? dtNotify { get; set; }
    }
}
