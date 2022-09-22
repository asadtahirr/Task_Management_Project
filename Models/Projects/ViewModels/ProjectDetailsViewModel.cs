namespace project_management_system.Models.Projects.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public int NumberOfProjectTasks { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
