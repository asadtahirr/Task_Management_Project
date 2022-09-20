namespace project_management_system.Models
{
    public class ProjectTask
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public decimal RequiredHours { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Project Project { get; set; }
        public string AssignedDeveloperId { get; set; }
        public virtual User AssignedDeveloper { get; set; }
        public virtual List<User> Watchers { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public ProjectTask()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Watchers = new List<User>();
            Comments = new List<Comment>();
        }
    }
}
