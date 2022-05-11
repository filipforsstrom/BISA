namespace BISA.Server.Services.EbookService
{
    public interface IEbookService
    {
        Task<ServiceResponseDTO<EbookDTO>> GetEbook(int itemId);
        Task<ServiceResponseDTO<EbookUpdateDTO>> UpdateEbook(EbookUpdateDTO EbookToUpdate);
        Task<ServiceResponseDTO<EbookCreateDTO>> CreateEbook(EbookCreateDTO EbookToAdd);
    }
}
