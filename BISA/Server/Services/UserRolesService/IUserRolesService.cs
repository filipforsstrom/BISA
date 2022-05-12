namespace BISA.Server.Services.UserRolesService
{
    public interface IUserRolesService
    {
        Task<ServiceResponseDTO<string>> PromoteToStaff(UserRoleDTO user);
        Task<ServiceResponseDTO<string>> PromoteToAdmin(UserRoleDTO user);
        Task<ServiceResponseDTO<string>> DemoteStaff(UserRoleDTO user);
        Task<ServiceResponseDTO<string>> DemoteAdmin(UserRoleDTO user);

    }
}
