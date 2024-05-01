using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using C3P1.Client.Components.Apps.Cat;
using C3P1.Client.Services.Apps;
using Microsoft.EntityFrameworkCore;
using C3P1.Data;

namespace C3P1.WebApi.Apps
{
    [Authorize]
    [Route("api/apps/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catService;
        private readonly AppDbContext _context;

        public CatController(ICatService catService, AppDbContext context)
        {
            _catService = catService;
            _context = context;
        }

        [HttpGet("list/cats")]
        public async Task<ActionResult<IEnumerable<Cat>>> GetCatlist()
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

            // get tasklist from current user
            var result = await _catService.GetCatsAsync(currentUserId);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpGet("get/cat/{id:guid}")]
        public async Task<ActionResult<Cat?>> GetCat(Guid id)
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

            // get cat from id
            var result = await _catService.GetCatFromIdAsync(currentUserId, id);

            if (result == null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("add/cat")]
        public async Task<ActionResult<bool>> AddCat([FromBody] Cat cat)
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

            // add a new cat
            bool result = await _catService.AddCatAsync(currentUserId, cat);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete/cat/{id:Guid}")]
        public async Task<ActionResult<bool>> DeleteCat(Guid id)
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

            // delete cat from id
            var result = await _catService.DeleteCatAsync(currentUserId, id);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("update/cat")]
        public async Task<ActionResult<bool>> UpdateCat([FromBody] Cat cat)
        {
            // update cat
            var result = await _catService.UpdateCatAsync(cat);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("list/entries/{id:guid}")]
        public async Task<ActionResult<IEnumerable<CatEntry>>> GetEntries(Guid id)
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

            // list entries for catId and current user
            var result = await _catService.GetEntriesAsync(currentUserId, id);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return result;
            }
        }

        [HttpPost("add/entry")]
        public async Task<ActionResult<bool>> AddEntry([FromBody] CatEntry entry)
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

            // add a new cat
            bool result = await _catService.AddEntryAsync(currentUserId, entry);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("delete/entry/{id:guid}")]
        public async Task<ActionResult<bool>> DeleteEntry(Guid id)
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

            // delete cat from id
            var result = await _catService.DeleteEntryAsync(currentUserId, id);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("update/entry")]
        public async Task<ActionResult<bool>> UpdateEntry([FromBody] CatEntry entry)
        {
            // update cat
            var result = await _catService.UpdateEntryAsync(entry);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("list/entries/weight/{id:guid}")]
        public async Task<ActionResult<List<CatEntry>>> GetWeightData(Guid id)
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

            // get all entries containing weight data
            var result = await _catService.GetWeightDataAsync(currentUserId, id);

            return result;
        }
    }
}
