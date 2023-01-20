using Final.Authorization;
using Final.Entities;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Models.Providers;
using HomeZilla_Backend.Repositories.Customers;
using HomeZilla_Backend.Repositories.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeZilla_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {

        private readonly IProviderRepo _providerRepo;
        private readonly IJwtUtils _jwtUtils;

        public ProvidersController(IProviderRepo providerRepo, IJwtUtils jwtUtils)
        {
            _providerRepo = providerRepo;
            _jwtUtils = jwtUtils;
        }

        [HttpGet("Get-User-Data"), Authorize(Role.Provider)]
        public async Task<ActionResult<ProviderUserData>> GetUserInfo()
        {
            var Response = await _providerRepo.GetUserData(_jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPut("Update-User-Data"), Authorize(Role.Provider)]
        public async Task<ActionResult<ProviderUserData>> UpdateCustomerData([FromBody] ProviderUpdateData Data)
        {
            var Response = await _providerRepo.UpdateUserData(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPut("Change-Password"), Authorize(Role.Provider)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword Data)
        {
            await _providerRepo.ChangePassword(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Password Updated Successfully" });
        }

        [HttpGet("Current-Order"), Authorize(Role.Provider)]
        public async Task<IActionResult> CurrentOrder([FromQuery] OrderQuery Data)
        {
            var Response = await _providerRepo.CurrentOrder(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpGet("Past-Order"), Authorize(Role.Provider)]
        public async Task<IActionResult> PastOrder([FromQuery] OrderQuery Data)
        {
            var Response = await _providerRepo.PastOrder(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPut("Update-Profile"), Authorize(Role.Provider)]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfilePic Data)
        {
            await _providerRepo.UpdateProfile(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpDelete("Delete-Profile"), Authorize(Role.Provider)]
        public async Task<IActionResult> DeleteProfile()
        {
            await _providerRepo.DeleteProfile(_jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Deleted Successfully" });
        }


        [HttpGet("Get-Service"), Authorize(Role.Provider)]
        public async Task<ActionResult<List<GetService>>> GetService()
        {
            var Response = await _providerRepo.GetService(_jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPost("Add-Service"), Authorize(Role.Provider)]
        public async Task<IActionResult> AddService([FromBody] AddService Data)
        {
            await _providerRepo.AddService(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new {message = "Added a Service"});
        }

        [HttpPut("Update-Service"), Authorize(Role.Provider)]
        public async Task<IActionResult> UpdateService([FromBody] UpdateService Data)
        {
            await _providerRepo.UpdateService(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Updated a Service" });
        }


        [HttpDelete("Delete-Service"),Authorize(Role.Provider)]
        public async Task<IActionResult> DeleteService([FromBody] DeleteService Data)
        {
            await _providerRepo.DeleteService(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Deleted a Service" });
        }

        [HttpGet("Check-Service"), Authorize(Role.Provider)]
        public async Task<IActionResult> CheckService()
        {
            var Response = await _providerRepo.CheckService(_jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }
        [HttpGet("Get-Location")]   
        public async Task<IActionResult> GetLocation()
        {
            var Response = await _providerRepo.GetLocation();
            return Ok(Response);
        }
        
    }
}
