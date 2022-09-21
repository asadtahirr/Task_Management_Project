using System.ComponentModel.DataAnnotations.Schema;

namespace project_management_system.Models
{
    public class Comment : Domain
    { 
        public string Body { get; set; }

        public string ProjectTaskId { get; set; }

        [InverseProperty("Comments")]
        public virtual ProjectTask ProjectTask { get; set; }
        public string CreatedById { get; set; }

        [InverseProperty("CreatedComments")]
        public virtual User CreatedBy { get; set; }

        public Comment() : base()
        {}
    }
}
