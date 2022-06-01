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

        public async Task<ServiceResponseViewModel<string>> Login(UserLoginViewModel user)
        {
            ServiceResponseViewModel<string> responseViewModel = new();
            var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/login", user);
            if (httpResponse.IsSuccessStatusCode)
            {
                var token = await httpResponse.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);
                var authState = await _authStateProvider.GetAuthenticationStateAsync();

                responseViewModel.Success = true;
                return responseViewModel;
            }

            responseViewModel.Message = await httpResponse.Content.ReadAsStringAsync();
            return responseViewModel;
        }

        

        public async Task<ServiceResponseViewModel<string>> Register(UserRegisterViewModel userRegister)
        {
            ServiceResponseViewModel<string> responseViewModel = new();

            var httpResponse = await _httpClient.PostAsJsonAsync("api/auth/register", userRegister);
            if (httpResponse.IsSuccessStatusCode)
            {
                var token = await httpResponse.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);
                var authState = await _authStateProvider.GetAuthenticationStateAsync();


                responseViewModel.Success = true;
                return responseViewModel;
            }

            responseViewModel.Message = await httpResponse.Content.ReadAsStringAsync();
            return responseViewModel;

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            await _localStorage.RemoveItemAsync("checkout");
            await _authStateProvider.GetAuthenticationStateAsync();
        }
    }
}
