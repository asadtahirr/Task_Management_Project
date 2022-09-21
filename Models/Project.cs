using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class Project : Domain
    {
        public string Title { get; set; }

        [InverseProperty("Project")]
        public virtual List<ProjectTask> ProjectTasks { get; set; }

        [InverseProperty("AssignedProjects")]
        public virtual List<User> Developers { get; set; }

        [ForeignKey("AssignedTo")]
        public string AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }

        [InverseProperty("CreatedProjects")]
        public virtual User CreatedBy { get; set; }

        public Project() : base()
        {
            ProjectTasks = new List<ProjectTask>();
            Developers = new List<User>();
        }
    }
}
