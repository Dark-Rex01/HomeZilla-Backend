using Final.Model.Auth;
using HomeZilla_Backend.Models.Auth;
using System.Threading.Tasks;

namespace Final.Services
{
    public interface IAuthRepo
    {
        Task<string> Authenticate(AuthenticateRequest model);
        Task Register(RegisterRequest Request);
        Task ForgotPassword(ForgotPasswordRequest Request);
        Task ResetPassword(ResetPasswordRequest Request);
        Task Verify(VerifyAccount VerifyData);
    }

}
