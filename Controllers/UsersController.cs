using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_system.Data;
using project_management_system.Models;
using project_management_system.Models.Users.ViewModels;
using System.Collections.Generic;

namespace project_management_system.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<User> UserManager;
        private ApplicationDbContext DbContext;

        public UsersController(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            UserManager = userManager;
            DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserDetailsViewModel> users = await UserManager
                                        .Users
                                        .OrderBy(e => e.CreatedAt)
                                        .Select(
                                            e => new UserDetailsViewModel()
                                            {
                                                Id = e.Id,
                                                FirstName = e.FirstName,
                                                LastName = e.LastName,
                                                Username = e.UserName,
                                                Email = e.Email
                                            }
                                        )
                                        .ToListAsync();

            IndexViewModel viewModel = new IndexViewModel()
            {
                Users = users
            };

            return View(viewModel);
        }

    }
}
