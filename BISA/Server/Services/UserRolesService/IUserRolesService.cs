namespace BISA.Server.Services.UserRolesService
{
    public interface IUserRolesService
    {
        Task<string> PromoteToStaff(UserRoleDTO user);
        Task<string> PromoteToAdmin(UserRoleDTO user);
        Task<string> DemoteStaff(string id);
        Task<string> DemoteAdmin(string id);
    }
}
