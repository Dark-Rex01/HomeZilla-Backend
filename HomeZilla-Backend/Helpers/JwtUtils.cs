using Final.Entities;
using Final.Helpers;
using HomeZilla_Backend.Models.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Final.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(Authentication user);
        public JwtData? ValidateToken(string token);
        public Guid GetUserId(HttpContext httpContext);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _config;

        public JwtUtils(IOptions<AppSettings> appSettings, IConfiguration config)
        {
            _appSettings = appSettings.Value;
            _config = config;
        }

        public string GenerateToken(Authentication user)
        {
            // generate token that is valid for 7 days
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("role", user.UserRole.ToString()),
                new Claim("name", user.UserName),
                new Claim("id", user.AuthId.ToString())

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value));



            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public JwtData? ValidateToken(string token)
        {
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var PayLoad = new JwtData();
                PayLoad.Id = new Guid(jwtToken.Claims.First(claim => claim.Type == "id").Value);
                PayLoad.Role = (Role)Enum.Parse(typeof(Role), jwtToken.Claims.First(claim => claim.Type == "role").Value);
                // return user id from JWT token if validation successful
                return PayLoad;
            }
            catch
            {
                // return null if validation fails
                return null;
            }

        }

        public Guid GetUserId(HttpContext httpContext)
        {
            var Data = (JwtData?)httpContext.Items["User"];
            return Data.Id;
        }
    }
}
