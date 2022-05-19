namespace BISA.Client.Services.EbookService
{
    public interface IEbookService
    {
        Task<EbookViewModel> GetEbook(int itemId);
        //Task<EbookViewModel> UpdateEbook(EbookUpdateDTO EbookToUpdate);
        //Task<EbookViewModel> CreateEbook(EbookCreateDTO EbookToAdd);
    }
}
