
using C3P1.Data;
using System.Net.Http.Json;

namespace C3P1.Client.Services.Admin
{
    public class ManageUserClientService : IManageUserService
    {
        private readonly HttpClient _httpClient;
        public ManageUserClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AppUser>> GetUsersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AppUser>>("api/admin/manageuser/list/users");

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<AppUser>();
            }
        }
        public async Task<List<AppUser>> GetUsersInRoleAsync(string role)
        {
            var result = await _httpClient.GetFromJsonAsync<List<AppUser>>("api/admin/manageuser/list/inrole/" + role);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<AppUser>();
            }
        }
        public async Task<List<string>> GetRolesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("api/admin/manageuser/list/roles");

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<string>();
            }
        }

        public async Task<List<string>> GetUserRolesAsync(AppUser user)
        {
            var result = await _httpClient.PostAsJsonAsync<AppUser>("api/admin/manageuser/user/roles", user);
            var roles = await result.Content.ReadFromJsonAsync<List<string>>();

            if (roles != null)
            {
                return roles;
            }
            else
            {
                return new List<string>();
            }
        }
        public async Task<bool> IsInRoleAsync(AppUser user, string role)
        {
            var result = await _httpClient.PostAsJsonAsync("api/admin/manageuser/user/inrole/" + role, user);

            var success = await result.Content.ReadAsStringAsync();

            if (success == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> AddToRoleAsync(Guid userId, string role)
        {
            var result = await _httpClient.PostAsJsonAsync("api/admin/manageuser/user/addrole/" + role, userId);

            var success = await result.Content.ReadAsStringAsync();

            if (success == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> RemoveFromRoleAsync(Guid userId, string role)
        {
            var result = await _httpClient.PostAsJsonAsync("api/admin/manageuser/user/removerole/" + role, userId);

            var success = await result.Content.ReadAsStringAsync();

            if (success == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(AppUser user)
        {
            var result = await _httpClient.PostAsJsonAsync<AppUser>("api/admin/manageuser/user/delete", user);

            var success = await result.Content.ReadAsStringAsync();

            if (success == "true")
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
