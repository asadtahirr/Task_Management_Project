namespace project_management_system.Models
{
    public class Comment 
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public string TaskId { get; set; }
        public virtual ProjectTask Task { get; set; }
        public string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public Comment()
        {
            Id = Guid.NewGuid().ToString(); 
            CreatedAt = DateTime.Now;
        }
    }
}
