namespace BISA.Server.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<LoanDTO>> GetAllLoans();
        Task<List<LoanDTO>> GetMyLoans();
        Task<List<LoanDTO>> AddLoan(List<CheckoutDTO> items);
        Task<string> ReturnLoan(int id);
    }
}
