namespace BISA.Client.Services.EbookService
{
    public interface IEbookService
    {
        Task<EbookViewModel> GetEbook(int itemId);
        Task<string> UpdateEbook(EbookViewModel ebookToUpdate);
        Task<string> CreateEbook(EbookViewModel ebookToCreate);
    }
}
