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
        public async Task<IActionResult> Index(string sortOrder, int? pageNumber)
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

            @ViewData["CurrentSort"] = sortOrder;
            ViewData["RequiredHoursSort"] = String.IsNullOrEmpty(sortOrder) ? "RequiredHours" : "";
            ViewData["PrioritySort"] = sortOrder == "Priority" ? "PriorityDesc" : "Priority";
            /*var projects = from p in DbContext.Projects
                            select p;*/

            switch (sortOrder)
            {
                case "RequiredHours":
                    foreach (var project in viewModel.Projects)
                    {
                        project.ProjectTasks = project.ProjectTasks.OrderBy(p => p.RequiredHours).ToList();
                    }
                    break;
                case "PriorityDesc":
                    foreach (var project in viewModel.Projects)
                    {
                        project.ProjectTasks = project.ProjectTasks.OrderByDescending(p => p.Priority).ToList();
                    }
                    break;
                case "Priority":
                    foreach (var project in viewModel.Projects)
                    {
                        project.ProjectTasks = project.ProjectTasks.OrderBy(p => p.Priority).ToList();
                    }
                    break;
                default:
                    foreach (var project in viewModel.Projects)
                    {
                        project.ProjectTasks = project.ProjectTasks.OrderByDescending(p => p.RequiredHours).ToList();
                    }
                    break;
            }
            int pageSize = 10;
            return DbContext.Projects != null ?
                          View(await PaginatedList<ProjectDetailsViewModel>.CreateAsync(viewModel.Projects.AsQueryable(), pageNumber ?? 1, pageSize)) :
                          Problem("Entity set 'ApplicationDbContext.Projects'  is null.");
            //return View(viewModel.Projects);
        }
    }
}
