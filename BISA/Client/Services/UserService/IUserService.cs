namespace BISA.Client.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponseViewModel<string>> ChangePassword(UserChangePasswordViewModel userChangePassword);
        Task<ServiceResponseViewModel<string>> DeleteUser(string id);
    }
}
