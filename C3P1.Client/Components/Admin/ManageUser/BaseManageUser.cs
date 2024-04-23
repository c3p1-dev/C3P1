using Blazorise;
using C3P1.Client.Services.Admin;
using C3P1.Data;
using Microsoft.AspNetCore.Components;

namespace C3P1.Client.Components.Admin.ManageUser
{
    public abstract class BaseManageUser : ComponentBase
    {
        protected List<AppUserWithRoles>? users;
        protected IEnumerable<AppUserWithRoles> Users
        {
            get
            {
                if (users != null)
                {
                    var query = from u in users select u;
                    return query;
                }
                else
                {
                    return new List<AppUserWithRoles>();
                }
            }
        }

        [Inject]
        IManageUserService? manageUserService { get; set; }
        [Inject]
        IMessageService? messageService { get; set; }

        protected async Task LoadData()
        {
            // create blank user list first time
            if (users == null)
            {
                users = new List<AppUserWithRoles>();
            }
            else // then clear the list next times
            {
                users.Clear();
            }

            var regularUsers = await manageUserService!.GetUsersAsync();
            var adminUsers = await manageUserService!.GetUsersInRoleAsync("Admin");

            foreach (var user in adminUsers)
            {
                regularUsers.Remove(user);
            }

            var result = adminUsers.Concat(regularUsers);

            foreach (var user in result)
            {
                AppUserWithRoles a = new AppUserWithRoles();
                a.User = user;
                a.Roles = await manageUserService!.GetUserRolesAsync(user);

                users.Add(a);
            }
        }

        protected async Task MakeAdmin(Guid userId)
        {
            await manageUserService!.AddToRoleAsync(userId, "Admin");
            await LoadData();

            await InvokeAsync(StateHasChanged);
        }

        protected async Task MakeRegular(Guid userId)
        {
            await manageUserService!.RemoveFromRoleAsync(userId, "Admin");
            await LoadData();

            await InvokeAsync(StateHasChanged);
        }
        protected async Task DeleteUser(AppUser user)
        {
            if (await messageService!.Confirm($"Are you sure you want to delete {user.Email} ?", "Confirmation"))
            {
                await manageUserService!.DeleteUserAsync(user);
                await LoadData();

                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
