using Microsoft.AspNetCore.Identity;

namespace project_management_system.Models.Users.ViewModels
{
    public class AssignRoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public UserDetailsViewModel User { get; set; }
    }
}
