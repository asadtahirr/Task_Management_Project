using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_management_system.Models;

namespace project_management_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
    }
}
