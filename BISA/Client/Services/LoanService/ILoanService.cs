namespace BISA.Client.Services.LoanService
{
    public interface ILoanService
    {
        Task<List<LoanViewModel>> GetAllLoans();
        Task<ServiceResponseViewModel<List<LoanViewModel>>> GetMyLoans();
        Task<List<LoanViewModel>> AddLoan(List<ItemViewModel> items);
        Task<string> ReturnLoan(int id);
    }
}
