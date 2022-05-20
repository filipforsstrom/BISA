using BISA.Shared.DTO;

namespace BISA.Client.Services.SearchService
{
    public interface ISearchService
    {
        Task<List<ItemViewModel>> GetByTitle(SearchDTO search);
        Task<List<ItemViewModel>> GetByTags(SearchDTO search);
        Task<List<ItemViewModel>> GetByAll(SearchDTO search);
    }
}
