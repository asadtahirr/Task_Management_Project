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

            string administratorRoleName = "Administrator";

            IdentityRole role1 = await RoleManager.FindByNameAsync(administratorRoleName);

            if (role1 == null)
            {
                role1 = new IdentityRole(administratorRoleName);

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

            string projectManagerRoleName = "Project Manager";

            IdentityRole role2 = await RoleManager.FindByNameAsync(projectManagerRoleName);

            if (role2 == null)
            {
                role2 = new IdentityRole(projectManagerRoleName);

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

            string developerRoleName = "Developer";

            IdentityRole role3 = await RoleManager.FindByNameAsync(developerRoleName);

            if (role3 == null)
            {
                role3 = new IdentityRole(developerRoleName);

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

                    bool isUser1Administrator = await UserManager.IsInRoleAsync(user1, administratorRoleName);

                    if (!isUser1Administrator)
                    {
                        IdentityResult assignUser1AsAdministrator = await UserManager.AddToRoleAsync(user1, administratorRoleName);

                        if (assignUser1AsAdministrator.Succeeded)
                        {
                            Console.WriteLine($"{userName1} is assigned to role {administratorRoleName}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName1} to role {administratorRoleName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not save user {userName1}");
                }
            }


            string userName2 = "pm1@pmsystem.com";

            User user2 = await UserManager.FindByNameAsync(userName2);

            if (user2 == null)      
            {
                user2 = new User()
                {
                    UserName = userName2,
                    Email = userName2,
                    EmailConfirmed = true,
                    FirstName = "Derral",
                    LastName = "Monias",
                };

                IdentityResult createUser2 = await UserManager.CreateAsync(user2, "Pass123$");

                if (createUser2.Succeeded)
                {
                    Console.WriteLine($"User {userName2} saved to database.");

                    bool isUser2ProjectManager = await UserManager.IsInRoleAsync(user2, projectManagerRoleName);

                    if (!isUser2ProjectManager)
                    {
                        IdentityResult assignUser2AsProjectManager = await UserManager.AddToRoleAsync(user2, projectManagerRoleName);

                        if (assignUser2AsProjectManager.Succeeded)
                        {
                            Console.WriteLine($"{userName2} is assigned to role {projectManagerRoleName}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName2} to role {projectManagerRoleName}");
                        }
                    }
                }            
                else
                {
                    Console.WriteLine($"Could not save user {userName2}");
                }
            }

            string userName3 = "pm2@pmsystem.com";

            User user3 = await UserManager.FindByNameAsync(userName3);

            if (user3 == null)
            {
                user3 = new User()
                {
                    UserName = userName3,
                    Email = userName3,
                    EmailConfirmed = true,
                    FirstName = "Daly",
                    LastName = "Harper",
                };

                IdentityResult createUser3 = await UserManager.CreateAsync(user3, "Pass123$");

                if (createUser3.Succeeded)
                {
                    Console.WriteLine($"User {userName3} saved to database.");

                    bool isUser3ProjectManager = await UserManager.IsInRoleAsync(user3, projectManagerRoleName);

                    if (!isUser3ProjectManager)
                    {
                        IdentityResult assignUser3AsProjectManager = await UserManager.AddToRoleAsync(user3, projectManagerRoleName);

                        if (assignUser3AsProjectManager.Succeeded)
                        {
                            Console.WriteLine($"{userName3} is assigned to role {projectManagerRoleName}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName3} to role {projectManagerRoleName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not save user {userName3}");
                }
            }

            string userName4 = "dev1@pmsystem.com";

            User user4 = await UserManager.FindByNameAsync(userName4);

            if (user4 == null)
            {
                user4 = new User()
                {
                    UserName = userName4,
                    Email = userName4,
                    EmailConfirmed = true,
                    FirstName = "Samuiel",
                    LastName = "Flett",
                };

                IdentityResult createUser4 = await UserManager.CreateAsync(user4, "Pass123$");

                if (createUser4.Succeeded)
                {
                    Console.WriteLine($"User {userName4} saved to database.");

                    bool isUser4Developer = await UserManager.IsInRoleAsync(user4, developerRoleName);

                    if (!isUser4Developer)
                    {
                        IdentityResult assignUser4AsDeveloper= await UserManager.AddToRoleAsync(user4, developerRoleName);

                        if (assignUser4AsDeveloper.Succeeded)
                        {
                            Console.WriteLine($"{userName4} is assigned to role {developerRoleName}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName4} to role {developerRoleName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not save user {userName4}");
                }
            }
            string userName5 = "dev2@pmsystem.com";

            User user5 = await UserManager.FindByNameAsync(userName5);

            if (user5 == null)
            {
                user5 = new User()
                {
                    UserName = userName5,
                    Email = userName5,
                    EmailConfirmed = true,
                    FirstName = "Jack",
                    LastName = "Bold",
                };

                IdentityResult createUser5 = await UserManager.CreateAsync(user5, "Pass123$");

                if (createUser5.Succeeded)
                {
                    Console.WriteLine($"User {userName5} saved to database.");

                    bool isUser5Developer = await UserManager.IsInRoleAsync(user5, developerRoleName);

                    if (!isUser5Developer)
                    {
                        IdentityResult assignUser5AsDeveloper = await UserManager.AddToRoleAsync(user5, developerRoleName);

                        if (assignUser5AsDeveloper.Succeeded)
                        {
                            Console.WriteLine($"{userName5} is assigned to role {developerRoleName}");
                        }
                        else
                        {
                            Console.WriteLine($"Could not assign {userName5} to role {developerRoleName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not save user {userName5}");
                }
            }
        }
    }
}


