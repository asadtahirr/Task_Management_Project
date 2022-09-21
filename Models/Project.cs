using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class Project
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public virtual List<ProjectTask> Tasks { get; set; }
        public virtual List<User> Developers { get; set; }

        [ForeignKey("AssignedTo")]
        public string AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public Project()
        {
            Id = Guid.NewGuid().ToString(); 
            CreatedAt = DateTime.Now;
            Tasks = new List<ProjectTask>();
            Developers = new List<User>();
        }
    }
}
