using BISA.Shared.DTO;
using BISA.Shared.ViewModels;

namespace BISA.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginViewModel user);
        Task<string> Register(UserRegisterViewModel userRegister);

        Task Logout();
    }
}
