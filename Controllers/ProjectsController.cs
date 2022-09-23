using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Projects.ViewModels;
using System.Linq;

namespace project_management_system.Controllers
{

    public class ProjectsController : Controller
    {
        public ApplicationDbContext DbContext { get; set; }
        private UserManager<User> UserManager { get; set; }

        public ProjectsController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            DbContext = dbContext;
            UserManager = userManager;
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
                    foreach(var project in viewModel.Projects)
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
            //return View(viewModel);
        }
        
        [HttpPost,Authorize]
        public async Task<IActionResult> AdjustRequiredHours(string id, int NewRequiredhours)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                User user = await UserManager.GetUserAsync(User);
                if(task.AssignedDeveloper == user && task != null)
                {
                    task.RequiredHours = NewRequiredhours;
                    await DbContext.SaveChangesAsync();
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost,Authorize]
        public async Task<IActionResult> MarkCompleted(string id)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                User user = await UserManager.GetUserAsync(User);
                if(task.AssignedDeveloper == user && task != null)
                {
                    task.Completed = true;
                    user.AssignedProjectTasks.Remove(task);
                }
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddComments(string taskId, string commentContennt)
        {
            try
            {
                User user = await UserManager.GetUserAsync(User);
                Comment comment = new Comment();
                ProjectTask projectTask = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);
                if(projectTask != null)
                {
                    comment.CreatedById = user.Id;
                    comment.CreatedBy = user;
                    comment.TaskId = projectTask.Id;
                    comment.ProjectTask = projectTask;
                    comment.Body = commentContennt;
                    user.CreatedComments.Add(comment);
                    await DbContext.SaveChangesAsync();
                }
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index"); 
        }

        [HttpGet,Authorize]
        public async Task<IActionResult> CheckAssignedProjects()
        {
            try
            {
                User user = await UserManager.GetUserAsync(User);
                List<Project> assignedProjects = DbContext.Projects.Where(p => p.AssignedDevelopers.Contains(user)).ToList();
                return View(assignedProjects);
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet,Authorize]
        public async Task<IActionResult> WatchingTask(string taskId)
        {
            try
            {
                ProjectTask projectTask = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);
                return View(projectTask);
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost,Authorize]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            try
            {
                ProjectTask projectTask = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);
                User user = await UserManager.GetUserAsync(User);
                if (projectTask.AssignedDeveloper == user)
                {
                    DbContext.ProjectTasks.Remove(projectTask);
                }
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
