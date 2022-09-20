namespace project_management_system.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual List<Project> AssignedProjects { get; set; }
        public virtual List<Task> AssignedTasks { get; set; }
        public virtual List<Project> CreatedProjects { get; set; }
        public virtual List<Task> CreatedTasks { get; set; }
        public virtual List<Task> WatchedTasks { get; set; }
        public virtual List<Comment> CreatedComments { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;

            AssignedProjects = new List<Project>();
            AssignedTasks = new List<Task>();
            CreatedProjects = new List<Project>();
            CreatedTasks = new List<Task>();
            WatchedTasks = new List<Task>();
            CreatedComments = new List<Comment>();
        }
    }
}
