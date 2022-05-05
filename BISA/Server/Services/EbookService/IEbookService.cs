namespace BISA.Server.Services.EbookService
{
    public interface IEbookService
    {
        Task<ServiceResponseDTO<EbookDTO>> GetEbook(int Itemid);
        Task<ServiceResponseDTO<string>> UpdateEbook(EbookDTO EbookToUpdate);
        Task<ServiceResponseDTO<string>> AddEbook(EbookDTO EbookToAdd);
    }
}
