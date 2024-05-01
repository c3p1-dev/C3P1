using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C3P1.WebApi.Apps
{
    [Authorize (Roles = "VisualCarnet")]
    [Route("/api/apps/[controller]")]
    [ApiController]
    public class VisualCarnetController : ControllerBase
    {
    }
}
