namespace BISA.Server.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<ItemDTO> GetMostPopularItem();
        Task<UserStatisticsDTO> GetMostActiveUser();

        Task<MostPopularAuthorDTO> GetMostPopularAuthor();
    }
}
