using AutoMapper;
using Final.Authorization;
using Final.Entities;
using Final.Helpers;
using Final.Model.Auth;
using Final.Services;
using HomeZilla_Backend.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _authService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AuthController(
            IAuthRepo userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _authService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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
            return Ok(new { message = "Verify your account to Login" });
        }


        [HttpPost("Verify")]
        public async Task<ActionResult> VerifyEmail([FromBody] VerifyAccount VerifyData)
        {
            await _authService.Verify(VerifyData);
            return Ok(new { message = "User has been Verified successfully" });
        }
        
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            try
            {
                await _authService.ForgotPassword(request);
                return Ok(new { message = "Reset password after OTP verification!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPassword(request);
                return Ok(new { message = "Your Password has been changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutRequest request)
        {
            try
            {
                await _authService.Logout(request);
                return Ok(new { message = "Logged out successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
