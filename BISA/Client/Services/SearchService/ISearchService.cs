namespace BISA.Client.Services.SearchService
{
    public interface ISearchService
    {
        Task<List<ItemViewModel>> GetByTitle(string title);
    }
}
