using BISA.Shared.DTO;

namespace BISA.Client.Services.UserRoleService
{
    public interface IUserRoleService
    {
        Task<ServiceResponseViewModel<string>> PromoteToStaff(UserRoleDTO user);
        Task<ServiceResponseViewModel<string>> DemoteToUser(UserRoleDTO user);
        Task<ServiceResponseViewModel<string>> PromoteToAdmin(UserRoleDTO user);
        Task<ServiceResponseViewModel<string>> RevokeAdmin(UserRoleDTO user);
        Task<ServiceResponseViewModel<UserRoleDTO>> SearchUser(string username);
    }
}
