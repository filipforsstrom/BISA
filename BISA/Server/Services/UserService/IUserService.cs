namespace BISA.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<UserChangePasswordDTO>> ChangePassword(UserChangePasswordDTO userChangePassword);
    }
}
