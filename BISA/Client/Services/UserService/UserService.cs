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

        public async Task<ServiceResponseViewModel<string>> DeleteUser(string id)
        {
            var response = new ServiceResponseViewModel<string>();

            var httpResponse = await _httpClient.DeleteAsync($"api/user/{id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                response.Success = true;
                response.Message = "User deleted"; 
            }
            else
            {
                response.Success = false;
                response.Message = await httpResponse.Content.ReadAsStringAsync();
            }
            return response;
        }
    }
}
