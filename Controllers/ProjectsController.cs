using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Projects.ViewModels;

namespace project_management_system.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext DbContext { get; set; }
        private UserManager<User> UserManager { get; set; }

        public ProjectsController(
            ApplicationDbContext dbContext,
            UserManager<User> userManager
        ) {
            DbContext = dbContext;
            UserManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> NewProject()
        {
            IList<User> developerUsers = await UserManager
                .GetUsersInRoleAsync("Developer");

            List<UserDetailsViewModel> developers = developerUsers.Select(
                    u => new UserDetailsViewModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.UserName,
                        Email = u.Email,
                    }
                ).ToList();

            NewProjectViewModel viewModel = new NewProjectViewModel()
            {
                Developers = developers
            };

            return View(viewModel);
        }
    }
}
