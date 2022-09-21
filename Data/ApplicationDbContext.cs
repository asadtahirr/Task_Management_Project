using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_management_system.Models;

namespace project_management_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<ProjectTask>()
                .HasOne<User>(e => e.CreatedBy)
                .WithMany(e => e.CreatedProjectTasks)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<ProjectTask>()
                .HasOne<Project>(e => e.Project)
                .WithMany(e => e.ProjectTasks)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity("ProjectUser", b =>
            {
                b.HasOne("project_management_system.Models.User", null)
                    .WithMany()
                    .HasForeignKey("AssignedProjectsId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("project_management_system.Models.Project", null)
                    .WithMany()
                    .HasForeignKey("AssignedProjectsId1")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("project_management_system.Models.Project", null)
                    .WithMany()
                    .HasForeignKey("AssignedDevelopersId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("project_management_system.Models.User", null)
                    .WithMany()
                    .HasForeignKey("AssignedDevelopersId1")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            builder.Entity("ProjectTaskUser", b =>
            {
                b.HasOne("project_management_system.Models.ProjectTask", null)
                        .WithMany()
                        .HasForeignKey("WatchedProjectTasksId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                b.HasOne("project_management_system.Models.User", null)
                    .WithMany()
                    .HasForeignKey("WatchersId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });
        }
    }
}
