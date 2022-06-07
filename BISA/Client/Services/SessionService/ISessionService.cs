namespace BISA.Client.Services.SessionService
{
    public interface ISessionService
    {
        Task<bool> CheckFor401(HttpResponseMessage responseMessage);
    }
}
