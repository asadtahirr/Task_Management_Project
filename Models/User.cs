using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [InverseProperty("AssignedTo")] 
        public virtual List<Project> AssignedProjects { get; set; }
        [InverseProperty("AssignedDeveloper")]
        public virtual List<ProjectTask> AssignedTasks { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual List<Project> CreatedProjects { get; set; }
        /*[InverseProperty("TaskCreatedDeveloper")]
        public virtual List<ProjectTask> CreatedTasks { get; set; }*/
        public virtual List<ProjectTask> WatchedTasks { get; set; }
        public virtual List<Comment> CreatedComments { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;

            AssignedProjects = new List<Project>();
            AssignedTasks = new List<ProjectTask>();
            CreatedProjects = new List<Project>();
            //CreatedTasks = new List<ProjectTask>();
            WatchedTasks = new List<ProjectTask>();
            CreatedComments = new List<Comment>();
        }
    }
}
