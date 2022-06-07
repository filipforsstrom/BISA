using Microsoft.AspNetCore.Components;

namespace BISA.Client.Services.SessionService
{
    public class SessionService : ISessionService
    {
        private readonly IAuthService _authService;
        private readonly NavigationManager _navigationManager;

        public SessionService(IAuthService authService, NavigationManager navigationManager)
        {
            _authService = authService;
            _navigationManager = navigationManager;
        }

        public async Task<bool> CheckFor401(HttpResponseMessage responseMessage)
        {
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _authService.Logout();
                string loingUrl = "/login";
                _navigationManager.NavigateTo(loingUrl, forceLoad: true);
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
