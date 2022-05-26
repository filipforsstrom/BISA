namespace BISA.Server.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<ServiceResponseDTO<ItemDTO>> GetMostPopularItem();
        Task<ServiceResponseDTO<UserStatisticsDTO>> GetMostActiveUser();

        Task<ServiceResponseDTO<MostPopularAuthorDTO>> GetMostPopularAuthor();
    }
}
