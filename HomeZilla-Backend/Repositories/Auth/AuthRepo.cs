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
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using Final.MailServices;
using HomeZilla_Backend.Models.Auth;

namespace Final.Services
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

        public async Task<string> Authenticate(AuthenticateRequest Request)
        {
            var User = await _context.Authentication.SingleOrDefaultAsync(x => x.Email == Request.Email);
            if(User == null || !User.IsVerified || !BCrypt.Net.BCrypt.Verify(Request.Password,User.PasswordHash) )
            {
                throw new BadHttpRequestException("Credentials are incorrect");
            }
            // authentication successful
            var Token = _jwtUtils.GenerateToken(User);
            return Token;
        }

        // Register User
        public async Task Register(RegisterRequest Request)
        {
            
            // validate
            if(! await _context.Authentication.AnyAsync(x => x.Email == Request.Email))
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
                    Customer customer = new();
                    customer = _mapper.Map<RegisterRequest, Customer>(Request);
                    var res = _context.Authentication.Where(x => x.Email == Request.Email).FirstOrDefault();
                    customer.CustomerUserID= res.AuthId;
                    _context.Customer.Add(customer);
                    await _context.SaveChangesAsync();
                }


                if(Request.UserRole == Role.Provider.ToString())
                {
                    Provider provider = new();
                    provider = _mapper.Map<RegisterRequest, Provider>(Request);
                    var res = _context.Authentication.Where(x => x.Email == Request.Email).FirstOrDefault();
                    provider.Id= res.AuthId;
                    _context.Provider.Add(provider);
                    await _context.SaveChangesAsync();
                }
            }

            else
            {
                throw new KeyNotFoundException("Email already registered");
            }

        }
    
      
        //verification
        public async Task Verify(VerifyAccount VerifyData)
        {
            var UserData = _context.Authentication.SingleOrDefault(x => x.OTP== VerifyData.OTP && (x.Email == VerifyData.Email && DateTime.Now <= x.OTPExpiresAt));
            if (UserData == null) throw new KeyNotFoundException("Invalid OTP or Expired");
            if(UserData.IsVerified)
            {
                throw new KeyNotFoundException("User Already Verified");
            }
            else
            {
                UserData.VerifiedAt = DateTime.Now;
                UserData.IsVerified = true;
                await _context.SaveChangesAsync();
            }
            
        }

        //forgot password 

        public async Task ForgotPassword(ForgotPasswordRequest request)
        {
            var user = _context.Authentication.SingleOrDefault(
                x => (x.Email == request.email) && !x.IsDeleted);

            if (user == null) throw new BadHttpRequestException("User not found");

            user.OTP = GenerateOtp();
            Console.WriteLine(user.OTP);
            user.OTPExpiresAt = DateTime.Now.AddMinutes(5);
            Console.WriteLine(user.OTPExpiresAt);
            string template = mailTemplate.GetPasswordResetTemplate(user.OTP, 10);
            await _mailer.Send(user.Email, "Password Reset Email Send", template);
            _context.Authentication.Update(user);
            await _context.SaveChangesAsync();
        }

        //reset password

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            var user = GetUserByOtp(request.Otp);
            if(user == null || user.OTPExpiresAt < DateTime.Now)
            {
                throw new BadHttpRequestException("Invalid OTP");
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            user.PasswordResetAt = DateTime.Now;
            user.OTP = null;
            user.OTPExpiresAt = DateTime.MinValue;

            _context.Authentication.Update(user);
            await _context.SaveChangesAsync();

        }

        //logout

        public async Task Logout(LogoutRequest request)
        {
            request.Token = null;
            await _context.SaveChangesAsync();
        }

        //OTP Generation

        private Authentication GetUserByOtp(string otp)
        {
            return _context.Authentication.SingleOrDefault(x => x.OTP == otp);
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }

        public async Task<Authentication> GetUser(string email ="")
        {
            Authentication? result = new();
            if(email != "")
            {
                result = await _context.Authentication.Where(u => u.Email == email).FirstOrDefaultAsync();
            }
            return result!;
        }
        
        }
    }

