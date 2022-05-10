namespace BISA.Server.Services.LoanService
{
    public interface ILoanService
    {
        Task<ServiceResponseDTO<List<LoanDTO>>> GetAllLoans();
        Task<ServiceResponseDTO<List<LoanDTO>>> GetMyLoans(int id);
        Task<ServiceResponseDTO<List<LoanDTO>>> AddLoan(List<ItemDTO> items);
        Task<ServiceResponseDTO<string>> ReturnLoan(int id);
    }
}
