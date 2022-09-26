﻿using Microsoft.AspNetCore.Identity;

namespace project_management_system.Models.Users.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
