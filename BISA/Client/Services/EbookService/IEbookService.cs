namespace BISA.Client.Services.EbookService
{
    public interface IEbookService
    {
        Task<ServiceResponseViewModel<EbookViewModel>> GetEbook(int itemId);
        Task<ServiceResponseViewModel<string>> UpdateEbook(EbookViewModel ebookToUpdate);
        Task<ServiceResponseViewModel<string>> CreateEbook(EbookViewModel ebookToCreate);
    }
}
