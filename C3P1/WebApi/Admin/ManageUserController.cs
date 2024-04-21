using C3P1.Data;
using C3P1.Client.Components.Apps.Tasklist;
using C3P1.Client.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C3P1.WebApi.Admin
{
    [Authorize (Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageUserService _manageUserService;
        public ManageUserController(IManageUserService manageUserService)
        {
            _manageUserService = manageUserService;
        }
        // GET : api/admin/[controller]
        [HttpGet("list/user")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
        {
            var result = await _manageUserService.GetUsersAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        // GET : api/admin/[controller]/list/inrole/{role}
        [HttpGet("list/inrole/{role}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetUsersInRoleAsync(string role)
        {
            var result = await _manageUserService.GetUsersInRoleAsync(role);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET : api/admin/[controller]/list/roles
        [HttpGet("list/roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetRolesAsync()
        {
            var result = await _manageUserService.GetRolesAsync();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST : api/admin/[controller]user/roles
        [HttpPost("user/roles")]
        public async Task<ActionResult<List<string>>> GetUserRolesAsync([FromBody] AppUser user)
        {
            var result = await _manageUserService.GetUserRolesAsync(user);

            return Ok(result);
        }

        // POST : api/admin/[controller]/user/inrole/{role}
        [HttpPost("user/inrole/{role}")]
        public async Task<ActionResult<bool>> IsInRoleAsync([FromBody] AppUser user, string role)
        {
            bool result = await _manageUserService.IsInRoleAsync(user, role);

            return Ok(result);
        }

        // POST : api/admin/[controller]/user/addrole/{role}
        [HttpPost("user/addrole/{role}")]
        public async Task<ActionResult<bool>> AddToRoleAsync([FromBody] Guid userId, string role)
        {
            bool result = await _manageUserService.AddToRoleAsync(userId, role);

            return Ok(result);
        }

        // POST : api/admin/[controller]/user/removerole/{role}
        [HttpPost("user/removerole/{role}")]
        public async Task<ActionResult<bool>> RemoveFromRoleAsync([FromBody] Guid userId, string role)
        {
            bool result = await _manageUserService.RemoveFromRoleAsync(userId, role);

            return Ok(result);
        }

        // POST : api/admin/[controller]/user/delete
        [HttpPost("user/delete")]
        public async Task<ActionResult<bool>> DeleteUserAsync([FromBody] AppUser user)
        {
            bool result = await _manageUserService.DeleteUserAsync(user);

            return result;
        }
    }
}
