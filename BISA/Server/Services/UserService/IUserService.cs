using BISA.Shared.Entities;

namespace BISA.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<UserEntity>> GetUser(int id);
        Task<ServiceResponseDTO<UserChangePasswordDTO>> ChangePassword(UserChangePasswordDTO userChangePassword);
        Task<ServiceResponseDTO<UserEntity>> DeleteUser(int id);
    }
}
