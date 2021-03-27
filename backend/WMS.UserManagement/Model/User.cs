﻿using Microsoft.AspNetCore.Identity;

namespace WMS.UserManagement.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
