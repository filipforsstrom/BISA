namespace BISA.Client.Services.UserService
{
    public interface IUserService
    {
        Task<string> ChangePassword(UserChangePasswordViewModel userChangePassword);

    }
}
