namespace project_management_system.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public virtual List<Task> Tasks { get; set; }
        public virtual List<User> Developers { get; set; }
        public string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public Project()
        {
            Id = Guid.NewGuid().ToString(); 
            CreatedAt = DateTime.Now;
            Tasks = new List<Task>();
            Developers = new List<User>();
        }
    }
}
