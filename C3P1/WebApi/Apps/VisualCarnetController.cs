using C3P1.Client.Components.Apps.VisualCarnet;
using C3P1.Client.Services.Apps;
using C3P1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C3P1.WebApi.Apps
{
    [Authorize (Roles = "VisualCarnet")]
    [Route("/api/apps/[controller]")]
    [ApiController]
    public class VisualCarnetController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IVisualCarnetService _visualCarnetService;
        public VisualCarnetController(AppDbContext context, IVisualCarnetService visualCarnetService)
        {
            _context = context;
            _visualCarnetService = visualCarnetService;
        }

        [HttpGet("list/accounts")]
        public async Task<ActionResult<IEnumerable<FicCpt>>> GetAccountList()
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // get accounts
            var result = await _visualCarnetService.GetAccountListAsync(currentUserId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("add/account")]
        public async Task<ActionResult<bool>> AddAccount([FromBody] FicCpt account)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // add account
            var result = await _visualCarnetService.AddAccountAsync(currentUserId, account);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("remove/accounts")]
        public async Task<ActionResult<int>> RemoveAllAcounts()
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // delete accounts
            var result = await _visualCarnetService.RemoveAllAccountsAsync(currentUserId);

            return Ok(result);
        }

        [HttpGet("list/families")]
        public async Task<ActionResult<IEnumerable<FicFam>>> GetAccountList(Guid id)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // get accounts
            var result = await _visualCarnetService.GetFamilyListAsync(currentUserId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("add/family")]
        public async Task<ActionResult<bool>> AddAccount([FromBody] FicFam family)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // add account
            var result = await _visualCarnetService.AddFamilyAsync(currentUserId, family);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("remove/families")]
        public async Task<ActionResult<int>> RemoveAllFamilies()
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // delete accounts
            var result = await _visualCarnetService.RemoveAllFamiliesAsync(currentUserId);

            return Ok(result);
        }

        [HttpGet("list/subfamilies")]
        public async Task<ActionResult<IEnumerable<FicSfa>>> GetSubFamilyList(Guid id)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // get accounts
            var result = await _visualCarnetService.GetSubFamilyListAsync(currentUserId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("add/subfamily")]
        public async Task<ActionResult<bool>> AddAccount([FromBody] FicSfa subfamily)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // add account
            var result = await _visualCarnetService.AddSubFamilyAsync(currentUserId, subfamily);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("remove/subfamilies")]
        public async Task<ActionResult<int>> RemoveAllSubFamilies()
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // delete accounts
            var result = await _visualCarnetService.RemoveAllSubFamiliesAsync(currentUserId);

            return Ok(result);
        }


        [HttpGet("list/records")]
        public async Task<ActionResult<IEnumerable<WrkEcrLig>>> GetRecordList(Guid id)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // get accounts
            var result = await _visualCarnetService.GetRecordListAsync(currentUserId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("add/record")]
        public async Task<ActionResult<bool>> AddRecord([FromBody] WrkEcrLig record)
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // add account
            var result = await _visualCarnetService.AddRecordAsync(currentUserId, record);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("remove/records")]
        public async Task<ActionResult<int>> RemoveAllRecords()
        {
            // get user id
            var name = User.Identity?.Name;
            var user = await _context.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
            if (user == null)
            {
                // should not happen
                return BadRequest("Auth issue");
            }

            var currentUserId = Guid.Parse(user.Id);

            // delete accounts
            var result = await _visualCarnetService.RemoveAllRecordsAsync(currentUserId);

            return Ok(result);
        }
    }
}
