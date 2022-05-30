using BISA.Shared.DTO;

namespace BISA.Client.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<LoanViewModel>> GetAllLoans();
        Task<ServiceResponseViewModel<List<LoanViewModel>>> GetMyLoans();
        Task<ServiceResponseViewModel<List<LoanDTO>>> AddLoan(List<CheckoutDTO> items);
        Task<ServiceResponseViewModel<string>> ReturnLoan(int id);
    }
}
