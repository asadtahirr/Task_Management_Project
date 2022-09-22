using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Projects.ViewModels;

namespace project_management_system.Controllers
{
    public class ProjectsController : Controller
    {
        public ApplicationDbContext DbContext { get; set; }

        public ProjectsController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;  
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel();

            viewModel.Projects = await DbContext.Projects
                .Select(p => new ProjectDetailsViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                CreatedById = p.CreatedById,
                CreatedByName = p.CreatedBy.UserName,
                NumberOfProjectTasks = p.ProjectTasks.Count,
                ProjectTasks = p.ProjectTasks

            }).ToListAsync();
          
            return View(viewModel);
        }
    }
}
