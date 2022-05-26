using BISA.Shared.Entities;

namespace BISA.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponseDTO<UserRoleDTO>> GetUser(string id);
        Task<ServiceResponseDTO<string>> ChangePassword(UserChangePasswordDTO userChangePassword);
        Task<ServiceResponseDTO<string>> DeleteUser(string id);
    }
}
