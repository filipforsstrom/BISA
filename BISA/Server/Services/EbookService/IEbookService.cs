namespace BISA.Server.Services.EbookService
{
    public interface IEbookService
    {
        Task<EbookDTO> GetEbook(int itemId);
        Task<EbookUpdateDTO> UpdateEbook(EbookUpdateDTO EbookToUpdate);
        Task<EbookCreateDTO> CreateEbook(EbookCreateDTO EbookToAdd);
    }
}
