using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class User : IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Developers")]
        public virtual List<Project> AssignedProjects { get; set; }

        [InverseProperty("AssignedDeveloper")]
        public virtual List<ProjectTask> AssignedProjectTasks { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual List<Project> CreatedProjects { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual List<ProjectTask> CreatedProjectTasks { get; set; }

        [InverseProperty("Watchers")]
        public virtual List<ProjectTask> WatchedProjectTasks { get; set; }

        [InverseProperty("CreatedBy")]
        public virtual List<Comment> CreatedComments { get; set; }

        public User() : base()
        {
            CreatedAt = DateTime.Now;
            AssignedProjects = new List<Project>();
            AssignedProjectTasks = new List<ProjectTask>();
            CreatedProjects = new List<Project>();
            CreatedProjectTasks = new List<ProjectTask>();
            WatchedProjectTasks = new List<ProjectTask>();
            CreatedComments = new List<Comment>();
        }
    }
}
