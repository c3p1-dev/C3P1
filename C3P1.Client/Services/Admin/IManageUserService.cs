﻿using C3P1.Data;

namespace C3P1.Client.Services.Admin
{
    public interface IManageUserService
    {
        public Task<List<AppUser>> GetUsersAsync();
        public Task<List<AppUser>> GetUsersInRoleAsync(string role);
        public Task<List<string>> GetRolesAsync();
        public Task<List<string>> GetUserRolesAsync(AppUser user);
        public Task<bool> IsInRoleAsync(AppUser user, string role);
        public Task<bool> AddToRoleAsync(Guid userId, string role);
        public Task<bool> RemoveFromRoleAsync(Guid userId, string role);
        public Task<bool> DeleteUserAsync(AppUser user);
    }
}
