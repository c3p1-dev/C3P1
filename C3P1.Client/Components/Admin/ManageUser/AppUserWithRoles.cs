﻿using C3P1.Data;

namespace C3P1.Client.Components.Admin.ManageUser
{
    public class AppUserWithRoles
    {
        public AppUser? User { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsAdmin()
        {
            return IsInRole("Admin");
        }
        public bool IsInRole(string role)
        {
            if (User == null || Roles == null || Roles.Count == 0)
            {
                return false;
            }
            else
            {
                if (Roles.Contains(role))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
