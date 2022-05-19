using BISA.Shared.DTO;
using BISA.Shared.ViewModels;
using System.Net.Http.Json;

namespace BISA.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient , ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider )
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<string> Login(UserLoginViewModel user)
        {
            string messageToUi;
            var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/login", user);
            if (httpResponse.IsSuccessStatusCode)
            {
                var token = await httpResponse.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);
                var authState = await _authStateProvider.GetAuthenticationStateAsync();

                messageToUi = "success";
                return messageToUi;
            }

            messageToUi = await httpResponse.Content.ReadAsStringAsync();
            return messageToUi;
        }

        

        public async Task<string> Register(UserRegisterViewModel userRegister)
        {   string messageToUi;
            var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);
            if (httpResponse.IsSuccessStatusCode)
            {
               var response = await httpResponse.Content.ReadAsStringAsync();

                messageToUi="success";
                return response;
            }

            return await httpResponse.Content.ReadAsStringAsync();

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await _authStateProvider.GetAuthenticationStateAsync();
        }
    }
}
