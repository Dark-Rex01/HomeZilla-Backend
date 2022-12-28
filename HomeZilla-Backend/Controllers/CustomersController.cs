using Final.Authorization;
using Final.Entities;
using HomeZilla_Backend.Models.Customers;
using HomeZilla_Backend.Repositories.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeZilla_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IJwtUtils _jwtUtils;

        public CustomersController(ICustomerRepo customerRepo, IJwtUtils jwtUtils)
        {
            _customerRepo = customerRepo;
            _jwtUtils = jwtUtils;
        }

        [HttpGet("Get-User-Data"), Authorize(Role.Customer)]
        public async Task<ActionResult<CustomerUserData>> GetUserInfo()
        {
            var response = await _customerRepo.GetUserData(_jwtUtils.GetUserId(HttpContext));
            return Ok(response);
        }

        [HttpPut("Update-User-Data"), Authorize(Role.Customer)]
        public async Task<ActionResult<CustomerUserData>> UpdateCustomerData([FromBody] CustomerUpdateData Data)
        {
            var Response = await _customerRepo.UpdateUserData(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPut("Change-Password"), Authorize(Role.Customer)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword Data)
        {
            await _customerRepo.ChangePassword(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new {message =  "Password Updated Successfully"});
        }

        [HttpGet("Current-Order"), Authorize(Role.Customer)]
        public async Task<IActionResult> CurrentOrder([FromQuery] OrderQuery Data)
        {
            var Response = await _customerRepo.CurrentOrder(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpGet("Past-Order"), Authorize(Role.Customer)]
        public async Task<IActionResult> PastOrder([FromQuery] OrderQuery Data)
        {
            var Response = await _customerRepo.PastOrder(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(Response);
        }

        [HttpPut("Update-Profile"), Authorize(Role.Customer)]
        public async Task<IActionResult> UpdateProfile([FromForm]ProfilePic Data)
        {
            await _customerRepo.UpdateProfile(Data, _jwtUtils.GetUserId(HttpContext));
            return Ok(new {message = "Updated Successfully"});
        }

        [HttpDelete("Delete-Profile"), Authorize(Role.Customer)]
        public async Task<IActionResult> DeleteProfile()
        {
            await _customerRepo.DeleteProfile(_jwtUtils.GetUserId(HttpContext));
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}
