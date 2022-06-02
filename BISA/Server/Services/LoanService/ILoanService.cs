namespace BISA.Server.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<LoanDTO>> GetAllLoans();
        Task<ServiceResponseDTO<List<LoanDTO>>> GetMyLoans();
        Task<ServiceResponseDTO<List<LoanDTO>>> AddLoan(List<CheckoutDTO> items);
        Task<ServiceResponseDTO<string>> ReturnLoan(int id);
    }
}
