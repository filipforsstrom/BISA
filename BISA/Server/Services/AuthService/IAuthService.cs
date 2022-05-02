

namespace BISA.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponseDTO<string>> Login(UserLoginDTO user);

        Task<ServiceResponseDTO<string>> Register(UserRegisterDTO user);
       



    }
}
