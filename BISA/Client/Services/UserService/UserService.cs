namespace BISA.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> ChangePassword(UserChangePasswordViewModel userChangePassword)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("api/user/changePassword", userChangePassword);
            if(httpResponse != null)
            {
                var userMessage = await httpResponse.Content.ReadAsStringAsync();
                return userMessage;
            }

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
