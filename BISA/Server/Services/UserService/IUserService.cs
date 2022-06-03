using BISA.Shared.Entities;

namespace BISA.Server.Services.UserService
{
    public interface IUserService
    {
        Task<UserRoleDTO> GetUser(string id);
        Task<string> ChangePassword(UserChangePasswordDTO userChangePassword);
        Task DeleteUser(string id);
    }
}
