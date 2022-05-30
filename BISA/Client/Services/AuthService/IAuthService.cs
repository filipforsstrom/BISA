using BISA.Shared.DTO;
using BISA.Shared.ViewModels;

namespace BISA.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponseViewModel<string>> Login(UserLoginViewModel user);
        Task<ServiceResponseViewModel<string>> Register(UserRegisterViewModel userRegister);

        Task Logout();
    }
}
