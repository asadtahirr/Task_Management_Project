using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Users.InputModels;
using project_management_system.Models.Users.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace project_management_system.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private UserManager<User> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private ApplicationDbContext DbContext;

        public UsersController(
            UserManager<User> userManager,
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager
        )
        {
            UserManager = userManager;
            DbContext = dbContext;
            RoleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserDetailsViewModel> users = await DbContext
                                        .Users
                                        .GroupJoin(DbContext.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new { user, userRole })
                                        .SelectMany(result => result.userRole.DefaultIfEmpty(), (result, userRole) => new { result.user, userRole })
                                        .GroupJoin(DbContext.Roles,
                                                   result => result.userRole.RoleId,
                                                   role => role.Id,
                                                   (result, role) => new { result.user, role })
                                        .SelectMany(result => result.role.DefaultIfEmpty(), (result, role) => new { result.user, role })
                                        .OrderBy(result => result.user.CreatedAt)
                                        .Select(
                                            result => new UserDetailsViewModel()
                                            {
                                                Id = result.user.Id,
                                                FirstName = result.user.FirstName,
                                                LastName = result.user.LastName,
                                                Username = result.user.UserName,
                                                Email = result.user.Email,
                                                Role = result.role.Name
                                            }
                                        )
                                        .Where(u => u.Role != "Administrator")
                                        .ToListAsync();

            IndexViewModel viewModel = new IndexViewModel()
            {
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            List<IdentityRole> roles = await RoleManager.Roles.Where(role => role.Name != "Administrator").ToListAsync();

            User user = await UserManager.FindByIdAsync(id);

            AssignRoleViewModel viewModel = new AssignRoleViewModel()
            {
                Roles = roles,
                User = new UserDetailsViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(
            string id,
            AssignRoleInputModel inputModel
        )
        {
            User user = await UserManager.FindByIdAsync(id);

            IdentityResult addRoleToUser = await UserManager.AddToRoleAsync(user, inputModel.Role);

            if (addRoleToUser.Succeeded)
            {
                return Redirect("~/users/index");
            }
            else
            {
                return Redirect($"~/users/assignrole/{id}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnassignRole(string id)
        {
            User user = await UserManager.FindByIdAsync(id);
            
            IList<string> userRoles = await UserManager.GetRolesAsync(user);

            IdentityResult removeRolesFromUser = await UserManager.RemoveFromRolesAsync(
                user,
                userRoles
            );

            if (removeRolesFromUser.Succeeded)
            {
                return Redirect("~/users/index");
            }
            else
            {
                Console.WriteLine($"Error: Could not unassign roles from user with id {id}");

                return Redirect($"~/users/index");
            }
        }
    }
}
