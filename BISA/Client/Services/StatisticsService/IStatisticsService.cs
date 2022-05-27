using BISA.Shared.DTO;

namespace BISA.Client.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<ServiceResponseViewModel<ItemViewModel>> GetMostPopularItem();
        Task<ServiceResponseViewModel<UserStatisticsViewModel>> GetMostActiveUser();
        Task<ServiceResponseViewModel<MostPopularAuthorViewModel>> GetMostPopularAuthor();
    }
}
