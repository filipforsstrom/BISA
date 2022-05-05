namespace BISA.Server.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<ServiceResponseDTO<ItemDTO>> GetMostPopularItem();
    }
}
