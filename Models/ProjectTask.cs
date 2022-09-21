namespace project_management_system.Models
{
    public enum Priority : int
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }

    public class ProjectTask : Domain
    {
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public decimal RequiredHours { get; set; }
        public bool Completed { get; set; }

        [InverseProperty("ProjectTasks")]
        public virtual Project Project { get; set; }
        [ForeignKey("AssignedDeveloper")]
        public string AssignedDeveloperId { get; set; }

        [InverseProperty("AssignedProjectTasks")]
        public virtual User AssignedDeveloper { get; set; }

        [InverseProperty("WatchedProjectTasks")]
        public virtual List<User> Watchers { get; set; }

        [InverseProperty("ProjectTask")]
        public virtual List<Comment> Comments { get; set; }
        public string CreatedById { get; set; }

        [InverseProperty("CreatedProjectTasks")]
        public virtual User CreatedBy { get; set; }

        public ProjectTask() : base()
        {
            Watchers = new List<User>();
            Comments = new List<Comment>();
        }
    }
}
