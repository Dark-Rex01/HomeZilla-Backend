using AutoMapper;
using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.Model.Auth;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using Final.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;
using Microsoft.EntityFrameworkCore;
using Final.MailServices;
using HomeZilla_Backend.Models.Auth;
using Realms.Sync;
using Final.Services;
using System.Security.Authentication;

namespace HomeZilla_Backend.Repositories.Auth
{

    public class AuthRepo : IAuthRepo
    {
        private HomezillaContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private Authentication _user = new();
        private readonly IMailService _mailer;
        private static MailTemplates mailTemplate = new MailTemplates();

        public AuthRepo(
            HomezillaContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IMailService mailer)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _mailer = mailer;
        }


        // Login
        public async Task<string> Authenticate(AuthenticateRequest Request)
        {
            var User = await _context.Authentication.SingleOrDefaultAsync(x => x.Email == Request.Email);
            if (User == null || !User.IsVerified || !BCrypt.Net.BCrypt.Verify(Request.Password, User.PasswordHash))
            {
                throw new AuthenticationException("Email Or Password is Incorrect");
            }
            // authentication successful
            var Token = _jwtUtils.GenerateToken(User);
            return Token;
        }

        // Register User
        public async Task Register(RegisterRequest Request)
        {

            // validate
            if (!await _context.Authentication.AnyAsync(x => x.Email == Request.Email))
            {
                _user = _mapper.Map<RegisterRequest, Authentication>(Request);
                _user.CreatedAt = DateTime.Now;

                // hash password
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                _user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Request.Password, salt);

                _user.OTP = GenerateOtp();
                _user.OTPExpiresAt = DateTime.Now.AddMinutes(30);
                _user.VerifiedAt = DateTime.MinValue;

                _context.Authentication.Add(_user);
                await _context.SaveChangesAsync();


                if (Request.UserRole == Role.Customer.ToString())
                {
                    Customer? customer = new();
                    customer = _mapper.Map<RegisterRequest, Customer>(Request);
                    var res = _context.Authentication.Where(x => x.Email == Request.Email).FirstOrDefault();
                    customer.CustomerUserID = res.AuthId;
                    _context.Customer.Add(customer);
                    await _context.SaveChangesAsync();
                }


                if (Request.UserRole == Role.Provider.ToString())
                {
                    Provider? provider = new();
                    provider = _mapper.Map<RegisterRequest, Provider>(Request);
                    Authentication? res = new();
                    res = _context.Authentication.Where(x => x.Email == Request.Email).FirstOrDefault();
                    provider.ProviderUserID = res.AuthId;
                    _context.Provider.Add(provider);
                    await _context.SaveChangesAsync();
                }

                string Template = mailTemplate.GetOtpTemplate(_user.OTP, 30);
                await _mailer.Send(_user.Email, "Account Verification OTP", Template);
            }

            else
            {
                throw new BadHttpRequestException("Email already registered");
            }

        }


        //Verification
        public async Task Verify(VerifyAccount VerifyData)
        {
            var UserData = _context.Authentication.SingleOrDefault(x => x.OTP == VerifyData.OTP && x.Email == VerifyData.Email && DateTime.Now <= x.OTPExpiresAt);
            if (UserData == null) throw new AuthenticationException("Invalid OTP or Expired");
            if (UserData.IsVerified)
            {
                throw new BadHttpRequestException("User Already Verified");
            }
            else
            {
                UserData.VerifiedAt = DateTime.Now;
                UserData.IsVerified = true;
                await _context.SaveChangesAsync();
            }

        }

        //Forgot Password 

        public async Task ForgotPassword(ForgotPasswordRequest Request)
        {
            var User = await _context.Authentication.SingleOrDefaultAsync(
                x => x.Email == Request.Email && x.IsVerified);

            if (User == null) throw new KeyNotFoundException("User Not Found");

            User.OTP = GenerateOtp();
            User.OTPExpiresAt = DateTime.Now.AddMinutes(5);
            string Template = mailTemplate.GetPasswordResetTemplate(User.OTP, 5);
            await _mailer.Send(User.Email, "Password Reset OTP", Template);
            _context.Authentication.Update(User);
            await _context.SaveChangesAsync();
        }

        //Reset password

        public async Task ResetPassword(ResetPasswordRequest Request)
        {
            var User = await _context.Authentication.SingleOrDefaultAsync(x => x.OTP == Request.OTP && x.Email == Request.Email); ;
            if (User == null || DateTime.Now > User.OTPExpiresAt)
            {
                throw new AuthenticationException("Invalid OTP");
            }
            else
            {
                User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Request.Password);
                User.PasswordResetAt = DateTime.Now;
                _context.Authentication.Update(User);
                await _context.SaveChangesAsync();
            }
        }

        //OTP Generation

        private int GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp;
        }

        public async Task<Authentication> GetUser(string email = "")
        {
            Authentication? Result = new();
            if (email != "")
            {
                Result = await _context.Authentication.Where(u => u.Email == email).FirstOrDefaultAsync();
            }
            return Result!;
        }

    }
}

