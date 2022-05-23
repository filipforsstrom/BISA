namespace BISA.Server.Services.SearchService
{
    public interface ISearchService
    {
        Task<ServiceResponseDTO<List<ItemDTO>>> SearchByTitle(string title);
        Task<ServiceResponseDTO<List<ItemDTO>>> SearchByTags(string tag);
        Task<ServiceResponseDTO<List<ItemDTO>>> SearchByAll(string search);
    }
}
