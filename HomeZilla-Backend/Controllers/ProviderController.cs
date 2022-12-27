using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeZilla_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
            [HttpGet("/Get-User-Data")]
            public async Task<IActionResult> GetUserInfo()
            {
                return Ok();
            }
    }
}
