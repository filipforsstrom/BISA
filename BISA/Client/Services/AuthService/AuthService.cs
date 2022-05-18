using BISA.Shared.DTO;
using BISA.Shared.ViewModels;
using System.Net.Http.Json;

namespace BISA.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public Task<ServiceResponseDTO<string>> Login(UserLoginViewModel user)
        //{
        //    throw new NotImplementedException();
        //}



        public async Task<string> Register(UserRegisterViewModel userRegister)
        {
            

            var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);
            if (httpResponse.IsSuccessStatusCode)
            {
               var response = await httpResponse.Content.ReadAsStringAsync();

                return response;
            }

            return httpResponse.Content.ToString();

        }
    }
}
