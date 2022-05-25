using BISA.Shared.DTO;

namespace BISA.Client.Services.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly HttpClient _httpClient;

        public UserRoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponseViewModel<string>> DemoteToUser(UserRoleDTO user)
        {
            var response = new ServiceResponseViewModel<string>();

            var apiResponse = await _httpClient.DeleteAsync($"api/UserRoles/DeleteStaff/{user.Id}");
            var content = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
            response.Message = content;
            return response;
        }

        public async Task<ServiceResponseViewModel<string>> PromoteToAdmin(UserRoleDTO user)
        {
            var response = new ServiceResponseViewModel<string>();

            var apiResponse = await _httpClient.PostAsJsonAsync<UserRoleDTO>("api/UserRoles/PromoteToAdmin", user);
            var content = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
            response.Message = content;
            return response;
        }

        public async Task<ServiceResponseViewModel<string>> PromoteToStaff(UserRoleDTO user)
        {
            var response = new ServiceResponseViewModel<string>();

            var apiResponse = await _httpClient.PostAsJsonAsync("api/UserRoles/PromoteToStaff", user);
            var content = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {                
                response.Success = true;                
            }
            else
            {
                response.Success = false;
            }
            response.Message = content;
            return response;
        }

        public async Task<ServiceResponseViewModel<string>> RevokeAdmin(UserRoleDTO user)
        {
            var response = new ServiceResponseViewModel<string>();

            var apiResponse = await _httpClient.DeleteAsync($"api/UserRoles/DeleteAdmin/{user.Id}");
            var content = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }
            response.Message = content;
            return response;
        }

        public async Task<ServiceResponseViewModel<UserRoleDTO>> SearchUser(string username)
        {
            var response = new ServiceResponseViewModel<UserRoleDTO>();

            var apiResponse = await _httpClient.GetAsync($"api/user/{username}");
            
            if (apiResponse.IsSuccessStatusCode)
            {
                var content = await apiResponse.Content.ReadFromJsonAsync<UserRoleDTO>();
                response.Success = true;
                response.Data = content;
                return response;
            }
            else
            {
                var message = await apiResponse.Content.ReadAsStringAsync();
                response.Message = message;
                response.Success = false;
            }
            
            return response;
        }
    }
}
