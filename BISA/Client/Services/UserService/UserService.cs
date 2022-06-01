namespace BISA.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponseViewModel<string>> ChangePassword(UserChangePasswordViewModel userChangePassword)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var httpResponse = await _httpClient.PostAsJsonAsync("api/user/changePassword", userChangePassword);
            if (httpResponse.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = await httpResponse.Content.ReadAsStringAsync();
                return serviceResponse;
            }

            serviceResponse.Success = false;
            serviceResponse.Message = await httpResponse.Content.ReadAsStringAsync();
            return serviceResponse;
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
