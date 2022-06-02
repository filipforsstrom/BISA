namespace BISA.Server.Services.SearchService
{
    public interface ISearchService
    {
        Task<List<ItemDTO>> SearchByTitle(string title);
        Task<List<ItemDTO>> SearchByTags(string tag);
        Task<List<ItemDTO>> SearchByAll(string search);
    }
}
