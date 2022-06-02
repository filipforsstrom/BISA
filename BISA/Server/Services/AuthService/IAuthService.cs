namespace BISA.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginDTO user);
        Task<string> Register(UserRegisterDTO userRegister);
    }
}
