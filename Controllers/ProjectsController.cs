using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Projects.ViewModels;

namespace project_management_system.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        public ApplicationDbContext DbContext { get; set; }
        public UserManager<User> UserManager { get; set; }

        public ProjectsController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            DbContext = dbContext;
            UserManager = userManager;
        }

        [HttpGet,Authorize]
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
            return View(viewModel.Projects);
        }

        [HttpPost]
        public async Task<IActionResult> AdjustRequiredHours(string id, int NewRequiredhours)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                User user = await UserManager.GetUserAsync(User);
                if (task != null)
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

        [HttpPost]
        public async Task<IActionResult> MarkCompleted(string id)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                task.Completed = true;
                await DbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComments(string taskId, string Body)
        {
            try
            {
                User user = await UserManager.GetUserAsync(User);

                Comment comment = new Comment();

                ProjectTask projectTask = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);

                if (projectTask != null)
                {
                    comment.CreatedById = user.Id;
                    comment.CreatedBy = user;
                    comment.TaskId = projectTask.Id;
                    comment.ProjectTask = projectTask;
                    comment.Body = Body;
                    projectTask.Comments.Add(comment);
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

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
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

        [HttpGet]
        public IActionResult Comment(string id)
        {
            try
            {
                ViewData["TaskId"] = id;

                return View();
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet,AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewProjects()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> createProject(string Title)
        {
            
            try
            {
                User user = await UserManager.GetUserAsync(User);
                Project project = new Project();
                project.Title = Title;
                project.CreatedById = user.Id;
                DbContext.Projects.Add(project);
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult NewTask(string id)
        {
            ViewData["ProjectId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> createProjectTask(string Title, int RequiredHours, Priority Priority, string id)
        {      
            try
            {
                User user = await UserManager.GetUserAsync(User);
                ProjectTask projectTask = new ProjectTask();
                projectTask.Title = Title;
                projectTask.RequiredHours = RequiredHours;
                projectTask.Priority = Priority;
                projectTask.ProjectId = id;
                projectTask.CreatedById = user.Id;
                projectTask.AssignedDeveloperId = user.Id;
                DbContext.ProjectTasks.Add(projectTask);
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AdjustRequiredHours(string id, int NewRequiredhours)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);
                User user = await UserManager.GetUserAsync(User);
                if (task != null)
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

        [HttpPost]
        public async Task<IActionResult> MarkCompleted(string id)
        {
            try
            {
                ProjectTask task = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == id);

                User user = await UserManager.GetUserAsync(User);

                if (task.AssignedDeveloper == user && task != null)
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
        
        [HttpPost]
        public async Task<IActionResult> AddComments(string taskId, string Body)
        {
            try
            {
                User user = await UserManager.GetUserAsync(User);

                Comment comment = new Comment();

                ProjectTask projectTask = await DbContext.ProjectTasks.FirstOrDefaultAsync(t => t.Id == taskId);

                if (projectTask != null)
                {
                    comment.CreatedById = user.Id;
                    comment.CreatedBy = user;
                    comment.TaskId = projectTask.Id;
                    comment.ProjectTask = projectTask;
                    comment.Body = Body;
                    projectTask.Comments.Add(comment);
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

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
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

        [HttpGet]
        public IActionResult Comment(string id)
        {
            try
            {
                ViewData["TaskId"] = id;

                return View();
            }
            catch
            {
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
