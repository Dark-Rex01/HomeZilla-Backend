using Final.Model.Auth;
using Final.Services;
using HomeZilla_Backend.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _authService;

        public AuthController(IAuthRepo userService)
        {
            _authService = userService;
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest Request)
        {
             var Token = await _authService.Authenticate(Request);
             HttpContext.Response.Headers.Add("Authorization", Token);
             return Ok(new { message = "Login Successful"});        
        }

        

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest Request)
        {
            await _authService.Register(Request);
            return StatusCode(201, "Created Successfully");
        }


        [HttpPost("Verify")]
        public async Task<ActionResult> VerifyEmail([FromBody] VerifyAccount VerifyData)
        {
            await _authService.Verify(VerifyData);
            return Ok(new { message = "User has been Verified successfully" });
        }
        
        [HttpPost("Forgot-Password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest Request)
        {
            await _authService.ForgotPassword(Request);
            return Ok(new { message = "Reset OTP is Sent!" });
        }

        [HttpPut("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest Request)
        {
            await _authService.ResetPassword(Request);
            return Ok(new { message = "Your Password has been changed Successfully" });
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Headers.Remove("Authorization");
            return NoContent();
        }

    }
}
