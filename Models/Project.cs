using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class Project : Domain
    {
        public string Title { get; set; }

        [InverseProperty("Project")]
        public virtual List<ProjectTask> ProjectTasks { get; set; }

        [InverseProperty("AssignedProjects")]
        public virtual List<User> AssignedDevelopers { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }

        [InverseProperty("CreatedProjects")]
        public virtual User CreatedBy { get; set; }

        public Project() : base()
        {
            ProjectTasks = new List<ProjectTask>();
            AssignedDevelopers = new List<User>();
        }
    }
}
