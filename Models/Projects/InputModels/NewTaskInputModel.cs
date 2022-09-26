namespace project_management_system.Models.Projects.InputModels
{
    public class NewTaskInputModel
    {
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public decimal RequiredHours { get; set; }
        public string AssignedDeveloperEmail { get; set; }
    }
}
