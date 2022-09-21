using Microsoft.AspNetCore.Identity;
using project_management_system.Models;

namespace project_management_system.Data
{
    public class SeedData
    {
        private static UserManager<User> UserManager { get; set; }
        private static RoleManager<IdentityRole> RoleManager { get; set; }

        public static async Task InsertNewData(IServiceProvider? serviceProvider)
        {
            UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleName1 = "Administrator";

            IdentityRole role1 = await RoleManager.FindByNameAsync(roleName1);

            if (role1 == null)
            {
                role1 = new IdentityRole(roleName1);

                IdentityResult createRole1 = await RoleManager.CreateAsync(role1);

                if (createRole1.Succeeded)
                {
                    Console.WriteLine("Administrator role has been created.");
                }
                else
                {
                    Console.WriteLine("Fatal!! Failed to create a role for Administrator.");
                }
            }

            string roleName2 = "Project Manager";

            IdentityRole role2 = await RoleManager.FindByNameAsync(roleName2);

            if (role2 == null)
            {
                role2 = new IdentityRole(roleName2);

                IdentityResult createRole2 = await RoleManager.CreateAsync(role2);

                if (createRole2.Succeeded)
                {
                    Console.WriteLine("Project Manager role has been created.");
                }
                else
                {
                    Console.WriteLine("Fatal!! Failed to create a role for Project Manager.");
                }
            }

            string roleName3 = "Developer";

            IdentityRole role3 = await RoleManager.FindByNameAsync(roleName3);

            if (role3 == null)
            {
                role3 = new IdentityRole(roleName3);

                IdentityResult createRole3 = await RoleManager.CreateAsync(role3);

                if (createRole3.Succeeded)
                {
                    Console.WriteLine("Developer role has been Created.");
                }
                else
                {
                    Console.WriteLine("Fatal!! Failed to create a role for Developer.");
                }
            }

            string userName1 = "admin@pmsystem.com";

            User user1 = await UserManager.FindByNameAsync(userName1);

            if (user1 == null)
            {
                user1 = new User()
                {
                    UserName = userName1,
                    Email = userName1,
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Snow",
                };

                IdentityResult saveUser1 = await UserManager.CreateAsync(user1, "Pass123$");

                if (saveUser1.Succeeded)
                {
                    Console.WriteLine($"User {userName1} saved to database.");

                    bool isUser1Administrator = await UserManager.IsInRoleAsync(user1, roleName1);

                    if (!isUser1Administrator)
                    {
                        IdentityResult assignUser1AsAdministrator = await UserManager.AddToRoleAsync(user1, roleName1);

                        if (assignUser1AsAdministrator.Succeeded)
                        {
                            Console.WriteLine($"{userName1} is assigned to role {roleName1}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName1} to role {roleName1}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not save user {userName1}");
                }
            }
        }
    }
}


