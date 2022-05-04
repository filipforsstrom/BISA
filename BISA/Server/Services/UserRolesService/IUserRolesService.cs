namespace BISA.Server.Services.UserRolesService
{
    public interface IUserRolesService
    {
        Task<ServiceResponseDTO<string>> PromoteToStaff(int id);
        Task<ServiceResponseDTO<string>> PromoteToAdmin(int id);
        Task<ServiceResponseDTO<string>> DemoteStaff(int id);
        Task<ServiceResponseDTO<string>> DemoteAdmin(int id);


    }
}
